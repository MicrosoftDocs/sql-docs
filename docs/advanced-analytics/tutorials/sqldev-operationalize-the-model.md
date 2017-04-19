---
title: "Step 6: Operationalize the Model (In-Database Advanced Analytics Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "04/19/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
dev_langs: 
  - "R"
  - "TSQL"
ms.assetid: 52b05828-11f5-4ce3-9010-59c213a674d1
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Step 6: Operationalize the Model (In-Database Advanced Analytics Tutorial)
In this step, you'll learn to *operationalize* the model using a stored procedure. This stored procedure can be called directly by other applications, to make predictions on new observations.  
  
In this step, you'll learn two methods for calling an R model from a stored procedure:  
  
-   **Batch scoring mode**: Use a SELECT query to provide multiple rows of data. The stored procedure returns a table of observations corresponding to the input cases.  
  
-   **Individual scoring mode**: Pass a set of individual parameter values as input.  The stored procedure returns a single row or value.  
  
First, let's see how scoring works in general.  
  
## Basic Scoring  
The stored procedure _PredictTip_ illustrates the basic syntax for wrapping a prediction call in a stored procedure.  
  
```  
CREATE PROCEDURE [dbo].[PredictTip] @inquery nvarchar(max)  
AS  
BEGIN  
  
  DECLARE @lmodel2 varbinary(max) = (SELECT TOP 1 model  
  FROM nyc_taxi_models);  
  EXEC sp_execute_external_script @language = N'R',  
                                  @script = N'  
mod <- unserialize(as.raw(model));  
print(summary(mod))  
OutputDataSet<-rxPredict(modelObject = mod, data = InputDataSet, outData = NULL,   
          predVarNames = "Score", type = "response", writeModelVars = FALSE, overwrite = TRUE);  
str(OutputDataSet)  
print(OutputDataSet)  
',  
                                  @input_data_1 = @inquery,  
                                  @params = N'@model varbinary(max)',  
                                  @model = @lmodel2  
  WITH RESULT SETS ((Score float));  
  
END  
  
GO  
  
```  
  
-   The  SELECT statement gets the serialized model from the database, and stores the model in the R variable `mod` for further processing using R.  
  
-   The new cases that will be scored are obtained from the [!INCLUDE[tsql](../../includes/tsql-md.md)] query specified in `@inquery`, the first parameter to the stored procedure. As the query data is read, the rows are saved in the default data frame, `InputDataSet`. This data frame is passed to the `rxPredict` function in R, which generates the scores.  
  
    `OutputDataSet<-rxPredict(modelObject = mod, data = InputDataSet, outData = NULL,            predVarNames = "Score", type = "response", writeModelVars = FALSE, overwrite = TRUE);`  
  
    Because a data.frame can contain a single row, you can use the same code for batch or single scoring.  
  
-   The value returned by the `rxPredict` function is  a **float** that represents the probability that a tip  (of any amount) will be given.  
  
## Batch scoring using a SELECT query  
Now let's see how batch scoring works.  
  
1.  Let's start by getting a smaller set of input data to work with.  
  
    ```  
    select top 10 a.passenger_count as passenger_count,   
        a.trip_time_in_secs as trip_time_in_secs,  
        a.trip_distance as trip_distance,  
        a.dropoff_datetime as dropoff_datetime,    
        dbo.fnCalculateDistance(pickup_latitude, pickup_longitude, dropoff_latitude,dropoff_longitude) as direct_distance   
    from  
    (  
        select medallion, hack_license, pickup_datetime, passenger_count,trip_time_in_secs,trip_distance,    
            dropoff_datetime, pickup_latitude, pickup_longitude, dropoff_latitude, dropoff_longitude  
        from nyctaxi_sample  
    )a  
    left outer join  
    (  
    select medallion, hack_license, pickup_datetime  
    from nyctaxi_sample  
    tablesample (70 percent) repeatable (98052)  
    )b  
    on a.medallion=b.medallion and a.hack_license=b.hack_license and a.pickup_datetime=b.pickup_datetime  
    where b.medallion is null  
    ```  
  
    This query creates a "top 10" list of trips with passenger count and other features needed to make a prediction.  
  
 **Results**  
  
*passenger_count   trip_time_in_secs    trip_distance  dropoff_datetime   direct_distance*  
*1  283 0.7 2013-03-27 14:54:50.000   0.5427964547*  
*1  289 0.7 2013-02-24 12:55:29.000   0.3797099614*  
*1  214 0.7 2013-06-26 13:28:10.000   0.6970098661*  
*1  276 0.7 2013-06-27 06:53:04.000   0.4478814362*  
*1  282 0.7 2013-02-21 07:59:54.000   0.5340645749*  
*1  260 0.7 2013-03-27 15:40:49.000   0.5513900727*  
*1  230 0.7 2013-02-05 09:47:59.000   0.5161578519*  
*1  250 0.7 2013-05-08 14:35:51.000   0.5626440915*  
*1  280 0.7 2013-05-11 12:22:01.000   0.5598517959*  
*1  308 0.7 2013-04-10 08:06:00.000   0.4478814362*  
  
You'll use this query as input to the stored procedure, _PredictTipBatchMode_, which was provided as part of the download.  
  
2.  First, take a minute to review the code of the stored procedure _PredictTipBatchMode_ in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
    ```  
    /****** Object:  StoredProcedure [dbo].[PredictTipBatchMode]  ******/  
    SET ANSI_NULLS ON  
    GO  
    SET QUOTED_IDENTIFIER ON  
    GO  
  
    CREATE PROCEDURE [dbo].[PredictTipBatchMode] @inquery nvarchar(max)  
    AS  
    BEGIN  
  
      DECLARE @lmodel2 varbinary(max) = (SELECT TOP 1 model  
      FROM nyc_taxi_models);  
      EXEC sp_execute_external_script @language = N'R',  
                                      @script = N'  
    mod <- unserialize(as.raw(model));  
    print(summary(mod))  
    OutputDataSet<-rxPredict(modelObject = mod, data = InputDataSet, outData = NULL,   
              predVarNames = "Score", type = "response", writeModelVars = FALSE, overwrite = TRUE);  
    str(OutputDataSet)  
    print(OutputDataSet)  
    ',  
                                      @input_data_1 = @inquery,  
                                      @params = N'@model varbinary(max)',  
                                      @model = @lmodel2  
      WITH RESULT SETS ((Score float));  
  
    END  
    ```  
  
3.  To create predictions,  you'll provide the query text in a variable and pass it as a parameter to the stored procedure, using a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement like this.  
  
    ```  
    -- Specify input query  
  
    DECLARE @query_string nvarchar(max)  
    SET @query_string='  
    select top 10 a.passenger_count as passenger_count,   
        a.trip_time_in_secs as trip_time_in_secs,  
        a.trip_distance as trip_distance,  
        a.dropoff_datetime as dropoff_datetime,    
        dbo.fnCalculateDistance(pickup_latitude, pickup_longitude, dropoff_latitude,dropoff_longitude) as direct_distance   
    from  
        select medallion, hack_license, pickup_datetime, passenger_count,trip_time_in_secs,trip_distance,    
            dropoff_datetime, pickup_latitude, pickup_longitude, dropoff_latitude, dropoff_longitude  
        from nyctaxi_sample  
    )a  
    left outer join  
    (  
    select medallion, hack_license, pickup_datetime  
    from nyctaxi_sample  
    tablesample (70 percent) repeatable (98052)  
    )b  
    on a.medallion=b.medallion and a.hack_license=b.hack_license and a.pickup_datetime=b.pickup_datetime  
    where b.medallion is null'  
  
    -- Call stored procedure for scoring  
    EXEC [dbo].[PredictTip] @inquery = @query_string;  
  
    ```  
  
4.  The stored procedure returns a series of values representing the prediction for each of the "top 10 trips". Looking back at the input values, all of the "top 10 trips" are single-passenger trips with a relatively short trip distance. Based on the data, the driver is very unlikely to get a tip on such trips.  
  
    Rather than returning just the yes-tip/no-tip results, you could also return the probability score for the prediction, and then apply  a WHERE clause to the _Score_ column values to categorize the score as "likely to tip" or "unlikely to tip", using a threshold value such as 0.5 or 0.7. This step is not included in the stored procedure but it would be easy to implement.  
  
## Score Individual Rows  
Sometimes you want to pass in individual values from an application and get a single result based on those values. For example, you could set up an Excel worksheet, web application, or Reporting Services report to call the stored procedure and provide inputs typed or selected by users.  
  
In this section, you'll learn how to create single predictions using  a stored procedure.  
  
1.  Take a minute to review the code of the stored procedure _PredictTipSingleMode_, which was included as part of the download.  
  
    ```  
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
      SELECT * FROM [dbo].[fnEngineerFeatures](@passenger_count,  
    @trip_distance,  
    @trip_time_in_secs,  
    @pickup_latitude,  
    @pickup_longitude,  
    @dropoff_latitude,  
    @dropoff_longitude)  
        '  
      DECLARE @lmodel2 varbinary(max) = (SELECT TOP 1 model  
      FROM nyc_taxi_models);  
      EXEC sp_execute_external_script @language = N'R',  
                                      @script = N'  
    mod <- unserialize(as.raw(model));  
    print(summary(mod))  
    OutputDataSet<-rxPredict(modelObject = mod, data = InputDataSet, outData = NULL,   
              predVarNames = "Score", type = "response", writeModelVars = FALSE, overwrite = TRUE);  
    str(OutputDataSet)  
    print(OutputDataSet)  
    ',  
    @input_data_1 = @inquery,  
    @params = N'@model varbinary(max),@passenger_count int,@trip_distance float,@trip_time_in_secs int ,  
    @pickup_latitude float ,@pickup_longitude float ,@dropoff_latitude float ,@dropoff_longitude float',  
    @model = @lmodel2,  
    @passenger_count =@passenger_count ,  
    @trip_distance=@trip_distance,  
    @trip_time_in_secs=@trip_time_in_secs,    
    @pickup_latitude=@pickup_latitude,  
    @pickup_longitude=@pickup_longitude,  
    @dropoff_latitude=@dropoff_latitude,  
    @dropoff_longitude=@dropoff_longitude  
      WITH RESULT SETS ((Score float));  
  
    END  
  
    ```  
  
    -   This stored procedure takes multiple single values as input, such as passenger count, trip distance, and so forth.  
  
        If you call the stored procedure  from an external application, make sure that the data matches the requirements of the R model. This might include ensuring that the input data can be cast or converted to an R data type, or validating data type and data length. For more information, see [Working with R Data Types](https://msdn.microsoft.com/library/mt590948.aspx).  
  
    -   The stored procedure creates a score based on the stored R model.  
  
2.  Try it out, by providing the values manually.  
  
    Open a new **Query** window, and call the stored procedure, typing parameters for each of the feature columns.  
  
    ```  
  
    EXEC [dbo].[PredictTipSingleMode] 1, 2.5, 631, 40.763958,-73.973373, 40.782139,-73.977303  
  
    ```  
  
    The values are for these feature columns , in order:  
  
    -   *trip_time_in_secs*  
    -   *pickup_latitude*  
    -   *pickup_longitude*  
    -   *dropoff_latitude*  
    -   *dropoff_longitude*  
  
3.  The results indicate that the probability of getting a tip is very low on these top 10 trips, all of which are single-passenger trips over a relatively short distance.  
  
## Conclusions  
In this tutorial, you've learned how to work with R code embedded in stored procedures. The integration with [!INCLUDE[tsql](../../includes/tsql-md.md)] makes it much easier to deploy R models for prediction and to incorporate model retraining as part of an enterprise data workflow.  
  
  
## Previous Step  
[Step 4: Create Data Features using T-SQL](../../advanced-analytics/r-services/step-4-create-data-features-using-t-sql-in-database-advanced-analytics-tutorial.md)  
  
## See Also  
[In-Database Advanced Analytics for SQL Developers &#40;Tutorial&#41;](../../advanced-analytics/r-services/in-database-advanced-analytics-for-sql-developers-tutorial.md)  
[SQL Server R Services Tutorials](../../advanced-analytics/r-services/sql-server-r-services-tutorials.md)  
  
  
  

