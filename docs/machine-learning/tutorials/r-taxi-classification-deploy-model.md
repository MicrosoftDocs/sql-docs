---
title: "R tutorial: Run predictions in SQL stored procedures"
titleSuffix: SQL machine learning
description: In part five of this five-part tutorial series, you'll operationalize embedded R script in SQL stored procedures with T-SQL functions with SQL machine learning.
ms.service: sql
ms.subservice: machine-learning

ms.date: 10/15/2020
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||>=azuresqldb-mi-current"
---

# R tutorial: Run predictions in SQL stored procedures
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

In part five of this five-part tutorial series, you'll learn to *operationalize* the model that you trained and saved in the previous part by using the model to predict potential outcomes. The model is wrapped in a stored procedure which can be called directly by other applications.

This article demonstrates two ways to perform scoring:

+ **Batch scoring mode**: Use a SELECT query as an input to the stored procedure. The stored procedure returns a table of observations corresponding to the input cases.

+ **Individual scoring mode**: Pass a set of individual parameter values as input.  The stored procedure returns a single row or value.

In this article, you'll:

> [!div class="checklist"]
> + Create and use stored procedures for batch scoring
> + Create and use stored procedures for scoring a single row

In [part one](r-taxi-classification-introduction.md), you installed the prerequisites and restored the sample database.

In [part two](r-taxi-classification-explore-data.md), you reviewed the sample data and generated some plots.

In [part three](r-taxi-classification-create-features.md), you learned how to create features from raw data by using a Transact-SQL function. You then called that function from a stored procedure to create a table that contains the feature values.

In [part four](r-taxi-classification-train-model.md), you loaded the modules and called the necessary functions to create and train the model using a SQL Server stored procedure.

## Basic scoring

The stored procedure **RPredict** illustrates the basic syntax for wrapping a `PREDICT` call in a stored procedure.

```sql
CREATE PROCEDURE [dbo].[RPredict] (@model varchar(250), @inquery nvarchar(max))
AS 
BEGIN 

DECLARE @lmodel2 varbinary(max) = (SELECT model FROM nyc_taxi_models WHERE name = @model);  
EXEC sp_execute_external_script @language = N'R',
  @script = N' 
    mod <- unserialize(as.raw(model));
    print(summary(mod))
    OutputDataSet <- data.frame(predict(mod, InputDataSet, type = "response"));
    str(OutputDataSet)
    print(OutputDataSet)
    ',
  @input_data_1 = @inquery,
  @params = N'@model varbinary(max)',
  @model = @lmodel2 
  WITH RESULT SETS (("Score" float));
END
GO
```

+ The SELECT statement gets the serialized model from the database, and stores the model in the R variable `mod` for further processing using R.

+ The new cases for scoring are obtained from the [!INCLUDE[tsql](../../includes/tsql-md.md)] query specified in `@inquery`, the first parameter to the stored procedure. As the query data is read, the rows are saved in the default data frame, `InputDataSet`. This data frame is passed to the [PREDICT](../../t-sql/queries/predict-transact-sql.md) function which generates the scores.
  
  `OutputDataSet <- data.frame(predict(mod, InputDataSet, type = "response"));`
  
  Because a data.frame can contain a single row, you can use the same code for batch or single scoring.
  
+ The value returned by the `PREDICT` function is  a **float** that represents the probability that the driver gets a tip of any amount.

## Batch scoring (a list of predictions)

A more common scenario is to generate predictions for multiple observations in batch mode. In this step, let's see how batch scoring works.

1. Start by getting a smaller set of input data to work with. This query creates a "top 10" list of trips with passenger count and other features needed to make a prediction.
  
   ```sql
   SELECT TOP 10 a.passenger_count AS passenger_count, a.trip_time_in_secs AS trip_time_in_secs, a.trip_distance AS trip_distance, a.dropoff_datetime AS dropoff_datetime, dbo.fnCalculateDistance(pickup_latitude, pickup_longitude, dropoff_latitude,dropoff_longitude) AS direct_distance
   
   FROM (SELECT medallion, hack_license, pickup_datetime, passenger_count,trip_time_in_secs,trip_distance, dropoff_datetime, pickup_latitude, pickup_longitude, dropoff_latitude, dropoff_longitude FROM nyctaxi_sample)a

   LEFT OUTER JOIN

   (SELECT medallion, hack_license, pickup_datetime FROM nyctaxi_sample TABLESAMPLE (70 percent) REPEATABLE (98052)    )b

   ON a.medallion=b.medallion AND a.hack_license=b.hack_license 
   AND a.pickup_datetime=b.pickup_datetime
   WHERE b.medallion IS NULL
   ```

   **Sample results**

   ```text
   passenger_count   trip_time_in_secs    trip_distance  dropoff_datetime          direct_distance
   1                 283                  0.7            2013-03-27 14:54:50.000   0.5427964547
   1                 289                  0.7            2013-02-24 12:55:29.000   0.3797099614
   1                 214                  0.7            2013-06-26 13:28:10.000   0.6970098661
   ```

2. Create a stored procedure called **RPredictBatchOutput** in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].

   ```sql
   CREATE PROCEDURE [dbo].[RPredictBatchOutput] (@model varchar(250), @inquery nvarchar(max))
   AS
   BEGIN
   DECLARE @lmodel2 varbinary(max) = (SELECT model FROM nyc_taxi_models WHERE name = @model);
   EXEC sp_execute_external_script 
     @language = N'R',
     @script = N'
       mod <- unserialize(as.raw(model));
       print(summary(mod))
       OutputDataSet <- data.frame(predict(mod, InputDataSet, type = "response"));
       str(OutputDataSet)
       print(OutputDataSet)
     ',
     @input_data_1 = @inquery,
     @params = N'@model varbinary(max)',
     @model = @lmodel2
     WITH RESULT SETS ((Score float));
   END
   ```

3. Provide the query text in a variable and pass it as a parameter to the stored procedure:

   ```sql
   -- Define the input data
   DECLARE @query_string nvarchar(max)
   SET @query_string='SELECT TOP 10 a.passenger_count as passenger_count, a.trip_time_in_secs AS trip_time_in_secs, a.trip_distance AS trip_distance, a.dropoff_datetime AS dropoff_datetime, dbo.fnCalculateDistance(pickup_latitude, pickup_longitude, dropoff_latitude,dropoff_longitude) AS direct_distance FROM  (SELECT medallion, hack_license, pickup_datetime, passenger_count,trip_time_in_secs,trip_distance, dropoff_datetime, pickup_latitude, pickup_longitude, dropoff_latitude, dropoff_longitude FROM nyctaxi_sample  )a   LEFT OUTER JOIN (SELECT medallion, hack_license, pickup_datetime FROM nyctaxi_sample TABLESAMPLE (70 percent) REPEATABLE (98052))b ON a.medallion=b.medallion AND a.hack_license=b.hack_license AND a.pickup_datetime=b.pickup_datetime WHERE b.medallion is null'
   
   -- Call the stored procedure for scoring and pass the input data
   EXEC [dbo].[RPredictBatchOutput] @model = 'RTrainLogit_model', @inquery = @query_string;
   ```
  
The stored procedure returns a series of values representing the prediction for each of the top 10 trips. However, the top trips are also single-passenger trips with a relatively short trip distance, for which the driver is unlikely to get a tip.

> [!TIP]
> Rather than returning just the "yes-tip" and "no-tip" results, you could also return the probability score for the prediction, and then apply a WHERE clause to the _Score_ column values to categorize the score as "likely to tip" or "unlikely to tip", using a threshold value such as 0.5 or 0.7. This step is not included in the stored procedure but it would be easy to implement.

## Single-row scoring of multiple inputs

Sometimes you want to pass in multiple input values and get a single prediction based on those values. For example, you could set up an Excel worksheet, web application, or Reporting Services report to call the stored procedure and provide inputs typed or selected by users from those applications.

In this section, you learn how to create single predictions using a stored procedure that takes multiple inputs, such as passenger count, trip distance, and so forth. The stored procedure creates a score based on the previously stored R model.
  
If you call the stored procedure from an external application, make sure that the data matches the requirements of the R model. This might include ensuring that the input data can be cast or converted to an R data type, or validating data type and data length.

1. Create a stored procedure **RPredictSingleRow**.
  
   ```sql
   CREATE PROCEDURE [dbo].[RPredictSingleRow] @model varchar(50), @passenger_count int = 0, @trip_distance float = 0, @trip_time_in_secs int = 0, @pickup_latitude float = 0, @pickup_longitude float = 0, @dropoff_latitude float = 0, @dropoff_longitude float = 0
   AS
   BEGIN
   DECLARE @inquery nvarchar(max) = N'SELECT * FROM [dbo].[fnEngineerFeatures](@passenger_count, @trip_distance, @trip_time_in_secs,  @pickup_latitude, @pickup_longitude, @dropoff_latitude, @dropoff_longitude)';
   DECLARE @lmodel2 varbinary(max) = (SELECT model FROM nyc_taxi_models WHERE name = @model);
   EXEC sp_execute_external_script  
     @language = N'R',
     @script = N'  
       mod <- unserialize(as.raw(model));  
       print(summary(mod));  
       OutputDataSet <- data.frame(predict(mod, InputDataSet, type = "response"));
       str(OutputDataSet);
       print(OutputDataSet); 
       ',  
     @input_data_1 = @inquery,  
     @params = N'@model varbinary(max),@passenger_count int,@trip_distance float,@trip_time_in_secs int ,  @pickup_latitude float ,@pickup_longitude float ,@dropoff_latitude float ,@dropoff_longitude float', @model = @lmodel2, @passenger_count =@passenger_count, @trip_distance=@trip_distance, @trip_time_in_secs=@trip_time_in_secs, @pickup_latitude=@pickup_latitude, @pickup_longitude=@pickup_longitude, @dropoff_latitude=@dropoff_latitude, @dropoff_longitude=@dropoff_longitude  
     WITH RESULT SETS ((Score float));  
   END
   ```

2. Try it out, by providing the values manually.
  
   Open a new **Query** window, and call the stored procedure, providing values for each of the parameters. The parameters represent feature columns used by the model and are required.

   ```sql
   EXEC [dbo].[RPredictSingleRow] @model = 'RTrainLogit_model',
   @passenger_count = 1,
   @trip_distance = 2.5,
   @trip_time_in_secs = 631,
   @pickup_latitude = 40.763958,
   @pickup_longitude = -73.973373,
   @dropoff_latitude =  40.782139,
   @dropoff_longitude = -73.977303
   ```

   Or, use this shorter form supported for [parameters to a stored procedure](../../relational-databases/stored-procedures/specify-parameters.md):
  
   ```sql
   EXEC [dbo].[RPredictSingleRow] 'RTrainLogit_model', 1, 2.5, 631, 40.763958,-73.973373, 40.782139,-73.977303
   ```

3. The results indicate that the probability of getting a tip is low (zero) on these top 10 trips, since all are single-passenger trips over a relatively short distance.

## Conclusions

Now that you have learned to embed R code in stored procedures, you can extend these practices to build models of your own. The integration with [!INCLUDE[tsql](../../includes/tsql-md.md)] makes it much easier to deploy R models for prediction and to incorporate model retraining as part of an enterprise data workflow.

## Next steps

In this article, you:

> [!div class="checklist"]
> + Created and used stored procedures for batch scoring
> + Created and used stored procedures for scoring a single row

For more information about R, see [R extension in SQL Server](../concepts/extension-r.md).