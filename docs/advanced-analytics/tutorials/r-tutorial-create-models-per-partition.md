---
title: Tutorial on creating, training and scoring partition-based models in R - SQL Server Machine Learning Services
description: Learn how to model, train, and use partitioned data that is created dynamically when using the partition-based modeling capabilites of SQL Server machine learning.
ms.custom: sqlseattle
ms.prod: sql
ms.technology: machine-learning
  
ms.date: 10/02/2018
ms.topic: tutorial
ms.author: heidist
author: HeidiSteen
manager: cgronlun
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
#customer intent: As an R developer, I want to model/train/score partitioned data to avoid manually subsetting data.
---
# Tutorial: Create partition-based models in R on SQL Server
[!INCLUDE[appliesto-ssvnex-xxxx-xxxx-xxx-md-winonly](../../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

In SQL Server 2019, partition-based modeling is the ability to create and train models over partitioned data. For stratified data that naturally segments into a given classification scheme - such as geographic regions, date and time, age or gender - you can execute script over the entire data set, with the ability to model, train, and score over partitions that remain intact over all these operations. 

Partition-based modeling is enabled through two new parameters on [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql):

+ **input_data_1_partition_by_columns**, which specifies a column to partition by.
+ **input_data_1_order_by_columns** specifies which columns to order by. 

In this tutorial, learn partition-based modeling using the classic NYC taxi sample data and R script. The partition column is the payment method.

> [!div class="checklist"]
> * Partitions are based on payment types (5).
> * Create and train models on each partition and store the objects in the database.
> * Predict the probability of tip outcomes over each partition model, using sample data reserved for that purpose.

## Prerequisites
 
To complete this tutorial, you must have the following:

+ Sufficient system resources. The data set is large and training operations are resource-intensive. If possible, use a system having at least 8 GB RAM. Alternatively, you can use smaller data sets to work around resource constraints. Instructions for reducing the data set are inline. 

+ A tool for T-SQL query execution, such as [SQL Server Management Studio](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms).

+ [NYCTaxi_Sample.bak](https://sqlmldoccontent.blob.core.windows.net/sqlml/NYCTaxi_Sample.bak), which you can [download and restore](demo-data-nyctaxi-in-sql.md) to your local database engine instance. File size is approximately 90 MB.

+ SQL Server 2019 preview database engine instance, with Machine Learning Services and R integration.

Check version by executing **`SELECT @@Version`** as a T-SQL query in a query tool. Output should be "Microsoft SQL Server 2019 (CTP 2.0) - 15.0.x".

Check availability of R packages by returning a well-formatted list of all R packages currently installed with your database engine instance:

```sql
EXECUTE sp_execute_external_script
  @language=N'R',
  @script = N'str(OutputDataSet);
  packagematrix <- installed.packages();
  Name <- packagematrix[,1];
  Version <- packagematrix[,3];
  OutputDataSet <- data.frame(Name, Version);',
  @input_data_1 = N''
WITH RESULT SETS ((PackageName nvarchar(250), PackageVersion nvarchar(max) ))
```

## Connect to the database

Start Management Studio and connect to the database engine instance. In Object Explorer, verify the [NYCTaxi_Sample database](demo-data-nyctaxi-in-sql.md) exists. 

## Create CalculateDistance

The demo database comes with a scalar function for calculating distance, but our stored procedure works better with a table-valued function. Run the following script to create the **CalculateDistance** function used in the [training step](#training-step) later on.

To confirm the function was created, check the \Programmability\Functions\Table-valued Functions under the **NYCTaxi_Sample** database in Object Explorer.

```sql
USE NYCTaxi_sample
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[CalculateDistance] (
	@Lat1 FLOAT
	,@Long1 FLOAT
	,@Lat2 FLOAT
	,@Long2 FLOAT
	)
	-- User-defined function calculates the direct distance between two geographical coordinates.
RETURNS TABLE
AS
RETURN

SELECT COALESCE(3958.75 * ATAN(SQRT(1 - POWER(t.distance, 2)) / nullif(t.distance, 0)), 0) AS direct_distance
FROM (
	VALUES (CAST((SIN(@Lat1 / 57.2958) * SIN(@Lat2 / 57.2958)) + (COS(@Lat1 / 57.2958) * COS(@Lat2 / 57.2958) * COS((@Long2 / 57.2958) - (@Long1 / 57.2958))) AS DECIMAL(28, 10)))
	) AS t(distance)
GO
 ```

## Define a procedure for creating and training per-partition models

This tutorial wraps R script in a stored procedure. In this step, you create a stored procedure that uses R to create an input dataset, build a classification model for predicting tip outcomes, and then stores the model in the database.

Among the parameter inputs used by this script, you'll see **input_data_1_partition_by_columns** and **input_data_1_order_by_columns**. Recall that these parameters are the mechanism by which partitioned modeling occurs. The parameters are passed as inputs to [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) to process partitions with the external script executing once for every partition. 

For this stored procedure, [use parallelism](#parallel) for faster time to completion.

After you run this script, you should see **train_rxLogIt_per_partition** in \Programmability\Stored Procedures under the **NYCTaxi_Sample** database in Object Explorer. You should also see a new table used for storing models: **dbo.nyctaxi_models**.

```sql
USE NYCTaxi_Sample
GO

CREATE
	OR

ALTER PROCEDURE [dbo].[train_rxLogIt_per_partition] (@input_query NVARCHAR(max))
AS
BEGIN
	DECLARE @start DATETIME2 = SYSDATETIME()
		,@model_generation_duration FLOAT
		,@model VARBINARY(max)
		,@instance_name NVARCHAR(100) = @@SERVERNAME
		,@database_name NVARCHAR(128) = db_name();

	EXEC sp_execute_external_script @language = N'R'
		,@script = 
		N'
	
	# Make sure InputDataSet is not empty. In parallel mode, if one thread gets zero data, an error occurs
	if (nrow(InputDataSet) > 0) {
	# Define the connection string
	connStr <- paste("Driver=SQL Server;Server=", instance_name, ";Database=", database_name, ";Trusted_Connection=true;", sep="");
	
	# build classification model to predict a tip outcome
	duration <- system.time(logitObj <- rxLogit(tipped ~ passenger_count + trip_distance + trip_time_in_secs + direct_distance, data = InputDataSet))[3];

	# First, serialize a model to and put it into a database table
    modelbin <- as.raw(serialize(logitObj, NULL));

    # Create the data source. To reduce data size, add rowsPerRead=500000 to cut the dataset by half.
    ds <- RxOdbcData(table="ml_models", connectionString=connStr);

    # Store the model in the database
    model_name <- paste0("nyctaxi.", InputDataSet[1,]$payment_type);
    
	rxWriteObject(ds, model_name, modelbin, version = "v1",
    keyName = "model_name", valueName = "model_object", versionName = "model_version", overwrite = TRUE, serialize = FALSE);
	} 
	
	'
		,@input_data_1 = @input_query
		,@input_data_1_partition_by_columns = N'payment_type'
		,@input_data_1_order_by_columns = N'passenger_count'
		,@parallel = 1
		,@params = N'@instance_name nvarchar(100), @database_name nvarchar(128)'
		,@instance_name = @instance_name
		,@database_name = @database_name
	WITH RESULT SETS NONE
END;
GO
```

<a name="parallel"></a>

### Parallel execution

Notice that the [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) inputs include **@parallel=1**, used to enable parallel processing. In contrast with previous releases, in SQL Server 2019, setting **@parallel=1** delivers a stronger hint to the query optimizer, making parallel execution a much more likely outcome.

By default, the query optimizer tends to operate under **@parallel=1** on tables having more than 256 rows, but if you can handle this explicitly by setting **@parallel=1** as shown in this script.

> [!Tip]
> For training workoads, you can use **@parallel** with any arbitrary training script, even those using non-Microsoft-rx algorithms. Typically, only RevoScaleR algorithms (with the rx prefix) offer parallelism in training scenarios in SQL Server. But with the new parameter, you can parallelize a script that calls functions, including open-source R functions, not specifically engineered with that capability. This works because partitions have affinity to specific threads, so all operations called in a script execute on a per-partition basis, on the given thread.

<a name="training-step"></a>

## Run the procedure and train the model

In this section, the script trains the model that you created and saved in the previous step. The examples below demonstrate two approaches for training your model: using an entire data set, or a partial data. 

Expect this step to take awhile. Training is computationally intensive, taking many minutes to complete. If system resources, especially memory, are insufficient for the load, use a subset of the data. The second example provides the syntax.

```sql
--Example 1: train on entire dataset
EXEC train_rxLogIt_per_partition N'
SELECT payment_type, tipped, passenger_count, trip_time_in_secs, trip_distance, d.direct_distance
  FROM dbo.nyctaxi_sample CROSS APPLY [CalculateDistance](pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as d
';
GO
```

```sql
--Example 2: Train on 20 percent of the dataset to expedite processing.
EXEC train_rxLogIt_per_partition N'
  SELECT tipped, payment_type, passenger_count, trip_time_in_secs, trip_distance, d.direct_distance
  FROM dbo.nyctaxi_sample TABLESAMPLE (20 PERCENT) REPEATABLE (98074)
  CROSS APPLY [CalculateDistance](pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as d
';
GO
```

> [!NOTE]
> If you are running other workloads, you can append `OPTION(MAXDOP 2)` to the SELECT statement if you want to limit query processing to just 2 cores.

## Check results

The result in the models table should be five different models, based on five partitions segmented by the five payment types. Models are in the **ml_models** data source.

```sql
SELECT *
FROM ml_models
```
 
## Define a procedure for predicting outcomes

You can use the same parameters for scoring. The following sample contains an R script that will score using the correct model for the partition it is currently processing.

As before, create a stored procedure to wrap your R code.

```sql
USE NYCTaxi_Sample
GO

-- Stored procedure that scores per partition. 
-- Depending on the partition being processed, a model specific to that partition will be used
CREATE
	OR

ALTER PROCEDURE [dbo].[predict_per_partition]
AS
BEGIN
	DECLARE @predict_duration FLOAT
		,@instance_name NVARCHAR(100) = @@SERVERNAME
		,@database_name NVARCHAR(128) = db_name()
		,@input_query NVARCHAR(max);

	SET @input_query = 'SELECT tipped, passenger_count, trip_time_in_secs, trip_distance, d.direct_distance, payment_type
						  FROM dbo.nyctaxi_sample TABLESAMPLE (1 PERCENT) REPEATABLE (98074)
						  CROSS APPLY [CalculateDistance](pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as d'

	EXEC sp_execute_external_script @language = N'R'
		,@script = 
		N'
	
	if (nrow(InputDataSet) > 0) {

    #Get the partition that is currently being processed
	current_partition <- InputDataSet[1,]$payment_type;

	#Create the SQL query to select the right model
	query_getModel <- paste0("select model_object from ml_models where model_name = ", "''", "nyctaxi.",InputDataSet[1,]$payment_type,"''", ";")
	

	# Define the connection string
	connStr <- paste("Driver=SQL Server;Server=", instance_name, ";Database=", database_name, ";Trusted_Connection=true;", sep="");
		
	#Define data source to use for getting the model
	ds <- RxOdbcData(sqlQuery = query_getModel, connectionString = connStr)

	# Load the model
	modelbin <- rxReadObject(ds, deserialize = FALSE)
	# unserialize model
	logitObj <- unserialize(modelbin);

	# predict tipped or not based on model
	predictions <- rxPredict(logitObj, data = InputDataSet, overwrite = TRUE, type = "response", writeModelVars = TRUE
		, extraVarsToWrite = c("payment_type"));		
	OutputDataSet <- predictions
	
	} else {
		OutputDataSet <- data.frame(integer(), InputDataSet[,]);		
	}
	'
		,@input_data_1 = @input_query
		,@parallel = 1
		,@input_data_1_partition_by_columns = N'payment_type'
		,@params = N'@instance_name nvarchar(100), @database_name nvarchar(128)'
		,@instance_name = @instance_name
		,@database_name = @database_name
	WITH RESULT SETS((
				tipped_Pred INT
				,payment_type VARCHAR(5)
				,tipped INT
				,passenger_count INT
				,trip_distance FLOAT
				,trip_time_in_secs INT
				,direct_distance FLOAT
				));
END;
GO
```

## Create a table to store predictions

```sql
CREATE TABLE prediction_results (
	tipped_Pred INT
	,payment_type VARCHAR(5)
	,tipped INT
	,passenger_count INT
	,trip_distance FLOAT
	,trip_time_in_secs INT
	,direct_distance FLOAT
	);

TRUNCATE TABLE prediction_results
GO
```

## Run the procedure and save predictions

```sql
INSERT INTO prediction_results (
	tipped_Pred
	,payment_type
	,tipped
	,passenger_count
	,trip_distance
	,trip_time_in_secs
	,direct_distance
	)
EXECUTE [predict_per_partition]
GO
```

## View predictions

Because the predictions are stored, you can run a simple query to return a result set.

```sql
SELECT *
FROM prediction_results;
```

## Next steps

In this tutorial, you used [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) to iterate operations over partitioned data. For a closer look at calling external scripts in stored procedures and using RevoScaleR functions, continue with the following tutorial.

> [!div class="nextstepaction"]
> [walkthrough for R and SQL Server](walkthrough-data-science-end-to-end-walkthrough.md)

<!--
## Old intro

**(Not for production workloads)**

One of the more common approaches for executing R or Python code on SQL data is providing script as an input parameter to the [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) stored procedure. In this CTP release, SQL Server 2019 adds new parameters to `sp_execute_external_script` to process partitions with the external script executing once for every partition:

| Parameter | Usage |
|-----------|-------|
| **input_data_1_partition_by_columns** | Specifies which columns to partition by. |
| **input_data_1_order_by_columns** | Specifies which columns to order by.  |

Partitions are an organizational mechanism for stratified data that naturally segments into a given classification scheme. Common examples include partitioning by geographic region, by date and time, by age or gender, and so forth. Given the existence of partitioned data, you might want to execute script over the entire data set, with the ability to model, train, and score partitions that remain intact over all these operations. Calling `sp_execute_external_script` with the new parameters allows you to do just that.

You can run this operation in parallel by combining `partition_by` with `@parallel`. If the input query can be parallelized, set `@parallel=1` as part of your arguments to `sp_execute_external_script`. By default, the query optimizer operates under `@parallel=1` on tables having more than 256 rows.

When the scenario is training, one advantage is that any arbitrary training script, even those using non-Microsoft-rx algorithms, can be parallelized by also using the @parallel parameter. Typically, you would have to use RevoScaleR algorithms (with the rx prefix) to obtain parallelism in training scenarios in SQL Server. But with the new parameter, you can parallelize a script that calls functions not specifically engineered with that capability.
-->
