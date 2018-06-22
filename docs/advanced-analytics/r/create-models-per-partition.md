---
title: Create partition-based models (SQL Server Machine Learning Services) | Microsoft Docs
ms.custom: sqlseattle
ms.prod: sql
ms.technology: machine-learning
  
ms.date: 06/22/2018
ms.topic: conceptual
ms.author: heidist
author: HeidiSteen
manager: cgronlun
---
---
# Create partition-based models in SQL Server Machine Learning Services
[!INCLUDE[appliesto-ssvnex-xxxx-xxxx-xxx-md-winonly](../../includes/tsql-appliesto-ssvnext-xxxx-xxxx-xxx.md)]

**(Not for production workloads)**

One of the more common approaches for executing R or Python code on SQL data is providing script as an input parameter to the [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) stored procedure. In this CTP release, SQL Server vNext adds new parameters to `sp_execute_external_script` to process partitions with the external script executing once for every partition:

| Parameter | Usage |
|-----------|-------|
| **input_data_1_partition_by_columns** | Specifies which columns to partition by. |
| **input_data_1_order_by_columns** | Specifes which columns to order by.  |

Partitions are an organizational mechanism for stratified data that naturally segments into a given classification scheme. Common examples include partitioning by geographic region, by date and time, by age or gender, and so forth. Given the existence of partitioned data, you might want to execute script over the entire data set, with the ability to model, train, and score partitions that remain intact over all these operations. Calling `sp_execute_external_script` with the new parameters allows you to do just that.

You can run this operation in parallel by combining `partition_by` with `@parallel`. If the input query can be parallelized, set `@parallel=1` as part of your arguments to `sp_execute_external_script`. By default, the query optimizer operates under `@parallel=1` on tables having more than 256 rows.

When the scenario is training, one advantage is that any arbitrary training script, even those using non-Microsoft-rx algorithms, can be parallelized by also using the @parallel parameter. Typically, you would have to use RevoScaleR algorithms (with the rx prefix) to obtain parallelism in training scenarios in SQL Server. But with the new parameter, you can parallelize a script that calls functions not specifically engineered with that capability.

## R Example


## Sample data

This example uses the [NYC Taxi and Limousine Commission] (http://www.nyc.gov/html/tlc/html/about/trip_record_data.shtml) public data set. Data is stored in five partitions, resulting in five individual models. 

Make sure you have enough memory. If memory is insufficient, you can stream the data using the rowsPerRead parameter.

### Train models

Create a stored procedure that trains models on each partition of the data, using parallelism for faster time to completion.
You can also use non-Rx functions to train per partition
 
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

--Train on entire dataset
exec train_rxLogIt_per_partition N'
SELECT payment_type, tipped, passenger_count, trip_time_in_secs, trip_distance, d.direct_distance
  FROM dbo.nyctaxi_sample CROSS APPLY [CalculateDistance](pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as d
  OPTION(MAXDOP 2)
';
go

--Train on 80 percent of the dataset.
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

 
### Score data using per-partition models

You can use the same parameters for scoring. The following sample contains an R script that will score using the correct model for the partition it is currently processing.

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

--create a table to store the predictions in
create table prediction_results (tipped_Pred int,
						  payment_type varchar(5), 
						  tipped int, 
						  passenger_count int, 
						  trip_distance float, 
						  trip_time_in_secs int, 
						  direct_distance float);

truncate table prediction_results
go
--Execute stored procedure and insert predictions in table
insert into prediction_results (tipped_Pred,
							   payment_type, 
							   tipped, 
							   passenger_count, 
							   trip_distance, 
							   trip_time_in_secs, 
							   direct_distance)
execute [predict_per_partition] 
go

--view predictions
select * from prediction_results;
```

## Next steps

To apply this technique to your own data, review instructions on how to structure partitioned data sets, call external scripts in stored procedures, and use the RevoScaleR `rx` functions.