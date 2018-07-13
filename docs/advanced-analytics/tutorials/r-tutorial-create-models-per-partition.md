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

In SQL Server vNext CTP 2.0, partition-based modeling is the ability to create and train models over partitioned data. For stratified data that naturally segments into a given classification scheme - such as geographic regions, date and time, age or gender - you can execute script over the entire data set, with the ability to model, train, and score over partitions that remain intact over all these operations. 

Partition-based modeling is enabled through two new parameters on [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql):

+ **input_data_1_partition_by_columns**, which specifies a column to partition by. In this tutorial, the partition column is the payment method.
+ **input_data_1_order_by_columns** specifies which columns to order by. 

In this tutorial, learn partition-based modeling using the classic NYC taxi sample data and R script. 

> [!div class="checklist"]
> * Partition based on a payment_type column. Values in this column segment data, one partition for each payment types.
> * Create and train models on each partition and store the objects in the database.
> * Predict the probability of tip outcomes over each partition model, using sample data reserved for that purpose.

## Prerequisites
 
To complete this tutorial, you must have SQL Server, sample data, and a tool for T-SQL query execution such as SQL Server Management Studio. 

### System resources

The data set is large and training operations are resource-intensive. If possible, use a system having more than 4 GB RAM. Alternatively, you can use smaller data sets to work around resource constraints. Instructions for reducing the data set are inline. 

### SQL Server database engine with Machine Learning Services

SQL Server vNext CTP 2.0 or later, with Machine Learning Services installed and configured, is required. You can check server version in Management Studio by executing `SELECT @@Version` as a T-SQL query. Output should be "Microsoft SQL Server vNext (CTP 2.0) - 15.0.x".

### Tools for query execution

You can download and install SQL Server Management Studio, or use any tool that connects to a relational database and runs T-SQL script. Make sure you can connect to a database engine instance that has Machine Learning Services.

### Sample data

Data originates from the [NYC Taxi and Limousine Commission](http://www.nyc.gov/html/tlc/html/about/trip_record_data.shtml) public data set. If you completed other tutorials, you might have the database already.


### R packages

This tutorial uses R installed with Machine Learning Services. You can verify R installation by returning a well-formatted list of all R packages currently installed with your database engine instance:

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

Once prerequisites are met, expand the database objects in Object Explorer to review the available tables. 

You'll use data from table X and Y, and add new stored procedures and models as you step through this tutorial.

## Define a procedure for creating and training per-partition models

This tutorial wraps all R script in a stored procedure. The following script creates an input dataset, builds a classification model to predict a tip outcome, and stores it in the database.

Among the objects created by this script, you'll see **input_data_1_partition_by_columns** and **input_data_1_order_by_columns**.  Recall that these objects are the mechanism by which partitioned modeling occurs. The parameters are passed as inputs to `sp_execute_external_script` to process partitions with the external script executing once for every partition.

Create a stored procedure that trains models on each partition of the data, [using parallelism](#parallel) for faster time to completion.


```sql
USE [nyctaxiDB]
GO

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

<a name="parallel"></a>

### Parallel execution

Notice that the `sp_execute_external_script` inputs include `@parallel=1`, used to enable parallel processing. In contrast with previous releases, in SQL Server vNext CTP 2.0, setting `@parallel=1` delivers a stronger hint to the query optimizer, making parallel execution a much more likely outcome.

By default, the query optimizer tends to operate under `@parallel=1` on tables having more than 256 rows, but if you can handle this explicitly by setting `@parallel=1` as shown in this script.

> [!Tip]
> For training workoads, you can use `@parallel` with any arbitrary training script, even those using non-Microsoft-rx algorithms. Typically, only RevoScaleR algorithms (with the rx prefix) offer parallelism in training scenarios in SQL Server. But with the new parameter, you can parallelize a script that calls functions, including open-source R functions, not specifically engineered with that capability. This works because partitions have affinity to specific threads, so all operations called in a script execute on a per-partition basis, on the given thread.

## Run the procedure and train the model

In this section, the script trains the model that you created and saved in the previous step. The examples below demonstrate two approaches for training your model: using an entire data set, or a partial data. Training is a resource-intensive operation. If system resources, especially memory, are insufficient for the load, use a subset of the data. The second example provides the syntax.

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

 
## Define a procedure for predicting outcomes

You can use the same parameters for scoring. The following sample contains an R script that will score using the correct model for the partition it is currently processing.

As before, create a stored procedure to wrap your R code.

```sql
USE [nyctaxiDB]
GO

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

## Create a table to store predictions

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

## Run the procedure and save predictions
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

To apply [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) and this technique to your own data, review the guidance on how to structure partitioned data sets, call external scripts in stored procedures, and use the RevoScaleR or revoscalepy `rx` functions.

<!--
## Old intro

**(Not for production workloads)**

One of the more common approaches for executing R or Python code on SQL data is providing script as an input parameter to the [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) stored procedure. In this CTP release, SQL Server vNext adds new parameters to `sp_execute_external_script` to process partitions with the external script executing once for every partition:

| Parameter | Usage |
|-----------|-------|
| **input_data_1_partition_by_columns** | Specifies which columns to partition by. |
| **input_data_1_order_by_columns** | Specifies which columns to order by.  |

Partitions are an organizational mechanism for stratified data that naturally segments into a given classification scheme. Common examples include partitioning by geographic region, by date and time, by age or gender, and so forth. Given the existence of partitioned data, you might want to execute script over the entire data set, with the ability to model, train, and score partitions that remain intact over all these operations. Calling `sp_execute_external_script` with the new parameters allows you to do just that.

You can run this operation in parallel by combining `partition_by` with `@parallel`. If the input query can be parallelized, set `@parallel=1` as part of your arguments to `sp_execute_external_script`. By default, the query optimizer operates under `@parallel=1` on tables having more than 256 rows.

When the scenario is training, one advantage is that any arbitrary training script, even those using non-Microsoft-rx algorithms, can be parallelized by also using the @parallel parameter. Typically, you would have to use RevoScaleR algorithms (with the rx prefix) to obtain parallelism in training scenarios in SQL Server. But with the new parameter, you can parallelize a script that calls functions not specifically engineered with that capability.
-->
