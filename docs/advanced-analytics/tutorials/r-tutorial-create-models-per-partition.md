---
title: Tutorial on creating, training and scoring partition-based models in R (SQL Server Machine Learning Services) | Microsoft Docs
ms.custom: sqlseattle
ms.prod: sql
ms.technology: machine-learning
  
ms.date: 06/26/2018
ms.topic: tutorial
ms.author: heidist
author: HeidiSteen
manager: cgronlun
#customer intent: As an R developer, I want to model/train/score partitioned data to avoid manually subsetting data.
---
# Tutorial: Create partition-based models in R on SQL Server
[!INCLUDE[appliesto-ssvnex-xxxx-xxxx-xxx-md-winonly](../../includes/tsql-appliesto-ssvnext-xxxx-xxxx-xxx.md)]

**(Not for production workloads)**

Partition-based modeling is the ability to create and train models over stratified data that naturally segments into a given classification scheme. Classic examples include data that slices by geography, date, time, gender, age, but you could use any arbitrary value if it is useful in your analysis. 

In this tutorial, learn the following tasks using sample data and R script:

> [!div class="checklist"]
> * Partition and order data based on a column
> * Create and train models on each partition using sample data. The model calculates the probability of getting a tip.
> * Predict the probability of a tip using sample data reserved for that purpose

Partition-based modeling is enabled through [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql). Using `sp_execute_external_script` and a few new parameters, you can partition and order data programmatically in R, without having to manually create and manage "micro" data and models on an individual basis. Once partitioned data sets are established, you can call familiar R functions to create and train models used for future predictions.

New parameters on `sp_execute_external_script` for partition-based models include the following:

| Parameter | Usage |
|-----------|-------|
| **input_data_1_partition_by_columns** | Specifies which columns to partition by. |
| **input_data_1_order_by_columns** | Specifes which columns to order by.  |

## Prerequisites
 
To complete this tutorial, you must have SQL Server, sample data, and a tool for T-SQL query execution such as SQL Server Management Studio. Make sure you have enough memory. If memory is insufficient, you can stream the data using the rowsPerRead parameter.

### Tools for query execution

You can download and install SQL Server Management Studio, or use any tool that connects to a relational database and runs T-SQL script. Make sure you can connect to a database engine instance that has Machine Learning Services.

### SQL Server database engine with Machine Learning Services

SQL Server vNext CTP 2.0 or later, with Machine Learning Services installed and configured, is required. You can check server version in Management Studio by executing `SELECT @@Version` as a T-SQL query. Output should be "Microsoft SQL Server vNext (CTP 2.0) - 15.0.x".

### Sample data

Data originates from the [NYC Taxi and Limousine Commission](http://www.nyc.gov/html/tlc/html/about/trip_record_data.shtml) public data set. 

For this tutorial, we have prepared partitioned data in a sample database, which you can download from Github. Data is stored in five partitions, resulting in five individual models. It contains segmented data sufficient for both training and scoring purposes.

The database filename is NYCTaxiDB.BAK. Copy the file to the Backup folder (C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\Backup) and then restore the file to the database engine instance.

### R packages

This tutorial uses R installed with Machine Learning Services. You can return a well-formatted list of all R packages currently installed by running the following T-SQL script:

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

Once prerequisites are met, expand the database objects in Object Explorer to review database contents.

*More information to come, including how to verify the data is partitioned, per this comment "Data is stored in five partitions, resulting in five individual models"*

## Create and store the partitioned model

This tutorial wraps all R script in a stored procedure. The following script creates an input dataset, builds a classification model to predict a tip outcome, and stores it in the database.

Among the objects created by this script, you'll see **input_data_1_partition_by_columns** and **input_data_1_order_by_columns**.  Recall that these objects are the mechanism by which partitioned modeling occurs. The parameters are passed as inputs to `sp_execute_external_script` to process partitions with the external script executing once for every partition.

You can run this operation in parallel by combining `partition_by` with `@parallel=1`. If the input query can be parallelized, set `@parallel=1` as part of your arguments to `sp_execute_external_script`. By default, the query optimizer operates under `@parallel=1` on tables having more than 256 rows, but if you want to handle this explicitly, this script includes the parameter as a demonstration.

> [!Tip]
> For training workoads, you can use `@parallel` with any arbitrary training script, even those using non-Microsoft-rx algorithms. Typically, only RevoScaleR algorithms (with the rx prefix) offer parallelism in training scenarios in SQL Server. But with the new parameter, you can parallelize a script that calls functions not specifically engineered with that capability.

 
```sql
USE [nyctaxiDB]
GO
```

Create a stored procedure that trains models on each partition of the data, using parallelism for faster time to completion.
You can also use non-Rx functions to train per partition.


```sql
CREATE OR ALTER procedure [dbo].[train_rxLogIt_per_partition] (
	@input_query nvarchar(max)
)
as
begin
	declare @start datetime2 = SYSDATETIME(), @model_generation_duration float, @model varbinary(max)
		  , @instance_name nvarchar(100) = @@SERVERNAME
		  , @database_name nvarchar(128) = db_name();
	
	
	exec sp_execute_external_script
		@language = N'R'
		, @script = N'
	
	# We need to make sure that the InputDataSet is not empty. 
	# When executing in parallel mode, it could be that one thread is getting zero data, which will give an error
	if (nrow(InputDataSet) > 0) {
	# Define the connection string
	connStr <- paste("Driver=SQL Server;Server=", instance_name, ";Database=", database_name, ";Trusted_Connection=true;", sep="");
	
	# build classification model to predict tipped or not
	duration <- system.time(logitObj <- rxLogit(tipped ~ passenger_count + trip_distance + trip_time_in_secs + direct_distance, data = InputDataSet))[3];

	# First, serialize a model and put it into a database table
    modelbin <- as.raw(serialize(logitObj, NULL));

    # Create the data source
    ds <- RxOdbcData(table="ml_models", connectionString=connStr);

    # Store the model in the database
    model_name <- paste0("nyctaxi.", InputDataSet[1,]$payment_type);
    
	rxWriteObject(ds, model_name, modelbin, version = "v1",
    keyName = "model_name", valueName = "model_object", versionName = "model_version", overwrite = TRUE, serialize = FALSE);
	} 
	
	'
		, @input_data_1 = @input_query
		, @input_data_1_partition_by_columns = N'payment_type'
		, @input_data_1_order_by_columns = N'passenger_count'
        , @parallel = 1 
        , @params = N'@instance_name nvarchar(100), @database_name nvarchar(128)'
       	, @instance_name = @instance_name
		, @database_name = @database_name
        
		
	WITH RESULT SETS NONE
end;

go
```

## Train the model

The following scripts train the model. Examples demonstrate two approaches: using an entire data set or partial data.

*How does this training relate to the previous training?  Is this doing the training, or invoking training logic?*

```sql
--Example 1: train on entire dataset
exec train_rxLogIt_per_partition N'
SELECT payment_type, tipped, passenger_count, trip_time_in_secs, trip_distance, d.direct_distance
  FROM dbo.nyctaxi_sample CROSS APPLY [CalculateDistance](pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as d
  OPTION(MAXDOP 2)
';
go
```

```sql
--Example 2: Train on 80 percent of the dataset.
exec train_rxLogIt_per_partition N'
  SELECT tipped, payment_type, passenger_count, trip_time_in_secs, trip_distance, d.direct_distance
  FROM dbo.nyctaxi_sample TABLESAMPLE (80 PERCENT) REPEATABLE (98074)
  CROSS APPLY [CalculateDistance](pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as d
  OPTION(MAXDOP 2)
';
go
```

**Results**

The result in the models table should be 5 different models since we had 5 partitions (payment types)
select * from ml_models.

 
## Score data using per-partition models

You can use the same parameters for scoring. The following sample contains an R script that will score using the correct model for the partition it is currently processing.

```sql
USE [nyctaxiDB]
GO
```

As before, create a stored procedure to wrap your R code.

```sql
-- Stored procedure that scores per partition. 
-- Depending on the partition being processed, a model specific to that partition will be used
CREATE OR ALTER procedure [dbo].[predict_per_partition] 
as
begin
	declare @predict_duration float
		  , @instance_name nvarchar(100) = @@SERVERNAME
		  , @database_name nvarchar(128) = db_name(), @input_query nvarchar(max);
	
	set @input_query = 'SELECT tipped, passenger_count, trip_time_in_secs, trip_distance, d.direct_distance, payment_type
						  FROM dbo.nyctaxi_sample TABLESAMPLE (1 PERCENT) REPEATABLE (98074)
						  CROSS APPLY [CalculateDistance](pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as d'
  
	exec sp_execute_external_script
		@language = N'R'
		, @script = N'
	
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
		, @input_data_1 = @input_query
		, @parallel = 1
		, @input_data_1_partition_by_columns = N'payment_type'
		, @params = N'@instance_name nvarchar(100), @database_name nvarchar(128)'
       	, @instance_name = @instance_name
		, @database_name = @database_name
	
		WITH RESULT SETS ((tipped_Pred int,
						  payment_type varchar(5), 
						  tipped int, 
						  passenger_count int, 
						  trip_distance float, 
						  trip_time_in_secs int, 
						  direct_distance float));
end;
go
```

## Create a table to store prediction

```sql
create table prediction_results (tipped_Pred int,
						  payment_type varchar(5), 
						  tipped int, 
						  passenger_count int, 
						  trip_distance float, 
						  trip_time_in_secs int, 
						  direct_distance float);

truncate table prediction_results
go
```

## Execute stored procedure and insert predictions in table

```sql
insert into prediction_results (tipped_Pred,
							   payment_type, 
							   tipped, 
							   passenger_count, 
							   trip_distance, 
							   trip_time_in_secs, 
							   direct_distance)
execute [predict_per_partition] 
go
```

## View predictions

Because the predictions are stored, you can run a simple query to return a result set.

```sql
select * from prediction_results;
```

## Next steps

To apply [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) amd this technique to your own data, review the guidance on how to structure partitioned data sets, call external scripts in stored procedures, and use the RevoScaleR or revoscalepy `rx` functions.

## Old intro

**(Not for production workloads)**

One of the more common approaches for executing R or Python code on SQL data is providing script as an input parameter to the [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) stored procedure. In this CTP release, SQL Server vNext adds new parameters to `sp_execute_external_script` to process partitions with the external script executing once for every partition:

| Parameter | Usage |
|-----------|-------|
| **input_data_1_partition_by_columns** | Specifies which columns to partition by. |
| **input_data_1_order_by_columns** | Specifes which columns to order by.  |

Partitions are an organizational mechanism for stratified data that naturally segments into a given classification scheme. Common examples include partitioning by geographic region, by date and time, by age or gender, and so forth. Given the existence of partitioned data, you might want to execute script over the entire data set, with the ability to model, train, and score partitions that remain intact over all these operations. Calling `sp_execute_external_script` with the new parameters allows you to do just that.

You can run this operation in parallel by combining `partition_by` with `@parallel`. If the input query can be parallelized, set `@parallel=1` as part of your arguments to `sp_execute_external_script`. By default, the query optimizer operates under `@parallel=1` on tables having more than 256 rows.

When the scenario is training, one advantage is that any arbitrary training script, even those using non-Microsoft-rx algorithms, can be parallelized by also using the @parallel parameter. Typically, you would have to use RevoScaleR algorithms (with the rx prefix) to obtain parallelism in training scenarios in SQL Server. But with the new parameter, you can parallelize a script that calls functions not specifically engineered with that capability.