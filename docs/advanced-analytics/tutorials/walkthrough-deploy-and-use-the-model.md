---
title: Deploy an R model for predictions on SQL Server - SQL Server Machine Learning
description: Tutorial showing how to deploy an R model on SQL Server for in-database analytics.
ms.prod: sql
ms.technology: machine-learning

ms.date: 11/26/2018  
ms.topic: tutorial
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Deploy the R model and use it in SQL Server (walkthrough)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In this lesson, learn how to deploy R models in a production environment by calling a trained model from a stored procedure. You can invoke the stored procedure from R or any application programming language that supports [!INCLUDE[tsql](../../includes/tsql-md.md)] (such as C#, Java, Python, and so forth) and use the model to make predictions on new observations.

This article demonstrates the two most common ways to use a model in scoring:

> [!div class="checklist"]
> * **Batch scoring mode** generates multiple predictions
> * **Individual scoring mode** generates predictions one at a time

## Batch scoring

Create a stored procedure, *PredictTipBatchMode*, that generates multiple predictions, passing a SQL query or table as input. A table of results is returned, which you might insert directly into a table or write to a file.

- Gets a set of input data as a SQL query
- Calls the trained logistic regression model that you saved in the previous lesson
- Predicts the probability that the driver gets any non-zero tip

1. In Management Studio, open a new query window and run the following T-SQL script to create the PredictTipBatchMode stored procedure.
  
    ```sql
    USE [NYCTaxi_Sample]
    GO

    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO

    IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PredictTipBatchMode')
    DROP PROCEDURE v
    GO

    CREATE PROCEDURE [dbo].[PredictTipBatchMode] @input nvarchar(max)
    AS
    BEGIN
      DECLARE @lmodel2 varbinary(max) = (SELECT TOP 1 model  FROM nyc_taxi_models);
      EXEC sp_execute_external_script @language = N'R',
         @script = N'
           mod <- unserialize(as.raw(model));
           print(summary(mod))
           OutputDataSet<-rxPredict(modelObject = mod,
             data = InputDataSet,
             outData = NULL,
             predVarNames = "Score", type = "response",
             writeModelVars = FALSE, overwrite = TRUE);
           str(OutputDataSet)
           print(OutputDataSet)',
      @input_data_1 = @input,
      @params = N'@model varbinary(max)',
      @model = @lmodel2
      WITH RESULT SETS ((Score float));
    END
    ```

    + You use a SELECT statement to call the stored model from a SQL table. The model is retrieved from the table as **varbinary(max)** data, stored in the SQL variable _\@lmodel2_, and passed as the parameter *mod* to the system stored procedure [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

    + The data used as inputs for scoring is defined as a SQL query and stored as a string in the SQL variable _\@input_. As data is retrieved from the database, it is stored in a data frame called *InputDataSet*, which is just the default name for input data to the [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) procedure; you can define another variable name if needed by using the parameter *_\@input_data_1_name_*.

    + To generate the scores, the stored procedure calls the rxPredict function from the **RevoScaleR** library.

    + The return value, *Score*, is the probability, given the model, that driver gets a tip. Optionally, you could easily apply some kind of filter to the returned values to categorize the return values into "tip" and "no tip" groups.  For example, a probability of less than 0.5 would mean a tip is unlikely.
  
2.  To call the stored procedure in batch mode, you define the query required as input to the stored procedure. Below is the SQL query, which you can run in SSMS to verify that it works.

    ```sql
    SELECT TOP 10
      a.passenger_count AS passenger_count,
      a.trip_time_in_secs AS trip_time_in_secs,
      a.trip_distance AS trip_distance,
      a.dropoff_datetime AS dropoff_datetime,
      dbo.fnCalculateDistance( pickup_latitude, pickup_longitude, dropoff_latitude, dropoff_longitude) AS direct_distance
      FROM 
        (SELECT medallion, hack_license, pickup_datetime, passenger_count,trip_time_in_secs,trip_distance, dropoff_datetime, pickup_latitude, pickup_longitude, dropoff_latitude, dropoff_longitude 
        FROM nyctaxi_sample)a 
      LEFT OUTER JOIN
      ( SELECT medallion, hack_license, pickup_datetime
      FROM nyctaxi_sample  tablesample (1 percent) repeatable (98052)  )b
      ON a.medallion=b.medallion
      AND a.hack_license=b.hack_license
      AND a.pickup_datetime=b.pickup_datetime
      WHERE b.medallion is null
    ```

3. Use this R code to create the input string from the SQL query:

    ```R
    input <- "N'SELECT TOP 10 a.passenger_count AS passenger_count, a.trip_time_in_secs AS trip_time_in_secs, a.trip_distance AS trip_distance, a.dropoff_datetime AS dropoff_datetime, dbo.fnCalculateDistance(pickup_latitude, pickup_longitude, dropoff_latitude, dropoff_longitude) AS direct_distance FROM (SELECT medallion, hack_license, pickup_datetime, passenger_count,trip_time_in_secs,trip_distance, dropoff_datetime, pickup_latitude, pickup_longitude, dropoff_latitude, dropoff_longitude FROM nyctaxi_sample)a LEFT OUTER JOIN ( SELECT medallion, hack_license, pickup_datetime FROM nyctaxi_sample  tablesample (1 percent) repeatable (98052)  )b ON a.medallion=b.medallion AND a.hack_license=b.hack_license AND  a.pickup_datetime=b.pickup_datetime WHERE b.medallion is null'";
    q <- paste("EXEC PredictTipBatchMode @inquery = ", input, sep="");
    ```

4. To run the stored procedure from R, call the **sqlQuery** method of the **RODBC** package and use the SQL connection `conn` that you defined earlier:

    ```R
    sqlQuery (conn, q);
    ```

    If you get an ODBC error, check for syntax errors and whether you have the right number of quotation marks. 
    
    If you get a permissions error, make sure the login has the ability to execute the stored procedure.

## Single row scoring

Individual scoring mode generates predictions one at a time, passing a set of individual values to the stored procedure as input. The values correspond to features in the model, which the model uses to create a prediction, or generate another result such as a probability value. You can then return that value to the application, or user.

When calling the model for prediction on a row-by-row basis, you pass a set of values that represent features for each individual case. The stored procedure then returns a single prediction or probability. 

The stored procedure *PredictTipSingleMode* demonstrates this approach. It takes as input multiple parameters representing feature values (for example, passenger count and trip distance), scores these features using the stored R model, and outputs the tip probability.

1. Run the following Transact-SQL statement to create the stored procedure.

    ```sql
    USE [NYCTaxi_Sample]
    GO

    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO

    IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'PredictTipSingleMode')
    DROP PROCEDURE v
    GO

    CREATE PROCEDURE [dbo].[PredictTipSingleMode] @passenger_count int = 0,
    @trip_distance float = 0,
    @trip_time_in_secs int = 0,
    @pickup_latitude float = 0,
    @pickup_longitude float = 0,
    @dropoff_latitude float = 0,
    @dropoff_longitude float = 0
    AS
    BEGIN
      DECLARE @inquery nvarchar(max) = N'
        SELECT * FROM [dbo].[fnEngineerFeatures](@passenger_count, @trip_distance, @trip_time_in_secs, @pickup_latitude, @pickup_longitude, @dropoff_latitude, @dropoff_longitude)'
      DECLARE @lmodel2 varbinary(max) = (SELECT TOP 1 model FROM nyc_taxi_models);

      EXEC sp_execute_external_script @language = N'R',  @script = N'
            mod <- unserialize(as.raw(model));
            print(summary(mod))
            OutputDataSet<-rxPredict(
              modelObject = mod,
              data = InputDataSet,
              outData = NULL,
              predVarNames = "Score",
              type = "response",
              writeModelVars = FALSE,
              overwrite = TRUE);
            str(OutputDataSet)
            print(OutputDataSet)
            ',
      @input_data_1 = @inquery,
      @params = N'
      -- passthrough columns
      @model varbinary(max) ,
      @passenger_count int ,
      @trip_distance float ,
      @trip_time_in_secs int ,
      @pickup_latitude float ,
      @pickup_longitude float ,
      @dropoff_latitude float ,
      @dropoff_longitude float',
      -- mapped variables
      @model = @lmodel2 ,
      @passenger_count =@passenger_count ,
      @trip_distance=@trip_distance ,
      @trip_time_in_secs=@trip_time_in_secs ,
      @pickup_latitude=@pickup_latitude ,
      @pickup_longitude=@pickup_longitude ,
      @dropoff_latitude=@dropoff_latitude ,
      @dropoff_longitude=@dropoff_longitude
      WITH RESULT SETS ((Score float));
    END
    ```

2. In SQL Server Management Studio, you can use the [!INCLUDE[tsql](../../includes/tsql-md.md)] **EXEC** procedure (or **EXECUTE**) to call the stored procedure, and pass it the required inputs. For example, try running this statement in Management Studio:

    ```sql
    EXEC [dbo].[PredictTipSingleMode] 1, 2.5, 631, 40.763958,-73.973373, 40.782139,-73.977303
    ```

    The values passed in here are, respectively, for the variables _passenger\_count_, _trip_distance_, _trip\_time\_in\_secs_, _pickup\_latitude_, _pickup\_longitude_, _dropoff\_latitude_, and _dropoff\_longitude_.

3. To run this same call from R code, you simply define an R variable that contains the entire stored procedure call, like this one:

    ```R
    q2 = "EXEC PredictTipSingleMode 1, 2.5, 631, 40.763958,-73.973373, 40.782139,-73.977303 ";
    ```

    The values passed in here are, respectively, for the variables _passenger\_count_, _trip\_distance_, _trip\_time\_in\_secs_, _pickup\_latitude_, _pickup\_longitude_, _dropoff\_latitude_, and _dropoff\_longitude_.

4. Call `sqlQuery` (from the **RODBC** package) and pass the connection string, together with the string variable containing the stored procedure call.

    ```R
    # predict with stored procedure in single mode
    sqlQuery (conn, q2);
    ```

    >[!TIP]
    > R Tools for Visual Studio (RTVS) provides great integration with both SQL Server and R. See this article for more examples of using RODBC with a SQL Server connection: [Working with SQL Server and R](https://docs.microsoft.com/visualstudio/rtvs/sql-server)

## Next steps

Now that you have learned how to work with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data and persist trained R models to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], it should be relatively easy for you to create new models based on this data set. For example, you might try creating these additional models:

+ A regression model that predicts the tip amount
+ A multiclass classification model that predicts whether the tip is big, medium, or small

You might also want to explore these additional samples and resources:

+ [Data science scenarios and solution templates](data-science-scenarios-and-solution-templates.md)
+ [In-database advanced analytics](sqldev-in-database-r-for-sql-developers.md)
+ [Machine Learning Server How-to guides](https://docs.microsoft.com/machine-learning-server/r/how-to-introduction)
+ [Machine Learning Server Additional Resources](https://docs.microsoft.com//machine-learning-server/resources-more)
