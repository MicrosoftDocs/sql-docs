---
title: "7. Deploy and Use the Model | Microsoft Docs"
ms.custom: ""
ms.date: "06/28/2017"
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
ms.assetid: f28a7aac-6d08-4781-ad28-b48d18cc16a0
caps.latest.revision: 18
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# 7. Deploy and Use the R Model

In this step, you will use your R models in a production environment, by wrapping the persisted model in a stored procedure. You can then  invoke the stored procedure from R or any application programming language that supports [!INCLUDE[tsql](../../includes/tsql-md.md)] (such as C#, Java, Python, etc), to use the model to make predictions on new observations.

This sample illustrates two ways that you can call a  model for scoring:

- **Batch scoring mode** lets you create multiple predictions based on input from a SELECT query.

- **Individual scoring mode** lets you create predictions one at a time, by passing a set of feature values for an individual case to the stored procedure, which returns a single prediction or other value as the result.

You'll learn how to create predictions using both the individual scoring and batch scoring methods.

## Batch scoring

For convenience, you can use a stored procedure that was created when you initially ran the PowerShell script in the beginning of this walkthrough. This stored procedure does the following:

-   Gets a set of input data as a SQL query
-   Calls the trained logistic regression model that you saved in the previous lesson
-   Predicts the probability that the driver will get a tip

### Use the stored procedure PredictTipBatchMode

1. Take a minute to look over the script that defines the stored procedure, **PredictTipBatchMode**. It illustrates several aspects of how a model can be operationalized using [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)].
  
    ```tsql
    CREATE PROCEDURE [dbo].[PredictTipBatchMode]
    @input nvarchar(max)
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

    + Note the SELECT statement that calls the stored model. You can store any trained model in a SQL table, by using a column of type **varbinary(max)**. In this code, the model is retrieved from the table, stored in the SQL variable _@lmodel2_, and passed as the parameter *mod* to the system stored procedure [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).
    + The input data used for scoring is passed as a string to the stored procedure.
  
        To define input data for this particular model, create a query that returns valid data. As data is retrieved from the database, it is stored in a data frame called *InputDataSet*. All the rows in this data frame are used for batch scoring.
        + *InputDataSet* is the default name for input data to the [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) procedure; you can define another variable name if needed.
        + To generate the scores, the stored procedure calls the rxPredict function from the **RevoScaleR** library.
    + The return value for the stored procedure, *Score*, is a predicted probability that the driver will be given a tip.
  
2.  Optionally, you could easily apply some kind of filter to the returned values to categorize the return values into "yes - tip " or "no tip" groups.  For example, a probability of less than 0.5 would mean no tip is likely.
  
3.  Call the stored procedure in batch mode:
  
    ```R
    input = "N' 
	  SELECT TOP 10 
	      a.passenger_count AS passenger_count,
          a.trip_time_in_secs AS trip_time_in_secs,
          a.trip_distance AS trip_distance,
          a.dropoff_datetime AS dropoff_datetime,
          dbo.fnCalculateDistance(
			pickup_latitude, 
			pickup_longitude, 
			dropoff_latitude,
			dropoff_longitude) AS direct_distance
      FROM
	  
      (SELECT medallion, hack_license, pickup_datetime,
      passenger_count,trip_time_in_secs,trip_distance,
      dropoff_datetime, pickup_latitude, pickup_longitude,
      dropoff_latitude, dropoff_longitude from nyctaxi_sample)a
	  
      LEFT OUTER JOIN
 
      ( SELECT medallion, hack_license, pickup_datetime 
	    FROM nyctaxi_sample  tablesample (1 percent) repeatable (98052)  )b

      ON a.medallion=b.medallion 
	    AND a.hack_license=b.hack_license 
	    AND a.pickup_datetime=b.pickup_datetime

      WHERE b.medallion is null
    '"  
    q<-paste("EXEC PredictTipBatchMode @inquery = ", input, sep="")
    sqlQuery (conn, q)
    ```

## Single row scoring

Instead of using a query to pass the input values to the saved R model, you might want to provide the features as arguments to the stored procedure.

1.  Take a minute to review the following code for the stored procedure, **PredictTipSingleMode**, which should already be created in your database.

    ```tsql
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
      @params = N'@model varbinary(max),
          @passenger_count int,
          @trip_distance float,
          @trip_time_in_secs int ,
          @pickup_latitude float ,
          @pickup_longitude float ,
          @dropoff_latitude float ,
          @dropoff_longitude float',
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
  
    This stored procedure takes feature values as input, such as passenger count and trip distance, scores these features using the stored R model, and outputs a score.

2. In SQL Server Management Studio, you can use the [!INCLUDE[tsql](../../includes/tsql-md.md)] **EXEC** to call the stored procedure, and pass it the required inputs.

    ```tsql
    EXEC [dbo].[PredictTipSingleMode] 1, 2.5, 631, 40.763958,-73.973373, 40.782139,-73.977303
    ```

    The values passed in here are, respectively, for the variables _passenger_count_, _trip_distance_, _trip_time_in_secs_, _pickup_latitude_, _pickup_longitude_, _dropoff_latitude_, and _dropoff_longitude_.
  
2.  To run this same call from R code, you simply define an R variable that contains the entire stored procedure call.
  
    ```R
    q = "EXEC PredictTipSingleMode 1, 2.5, 631, 40.763958,-73.973373, 40.782139,-73.977303 "
    ```
  
    The values passed in here are, respectively, for the variables _passenger_count_, _trip_distance_, _trip_time_in_secs_, _pickup_latitude_, _pickup_longitude_, _dropoff_latitude_, and _dropoff_longitude_.

3. To generate scores from your R code, call the *sqlQuery* function of the **RODBC** package, and pass the connection string and the string variable containing the the stored procedure call.
  
    ```R
    # predict with stored procedure in single mode
    sqlQuery (conn, q)
    ```
  
    For more information about **RODBC**, see [http://www.inside-r.org/packages/cran/RODBC/docs/sqlQuery](http://www.inside-r.org/packages/cran/RODBC/docs/sqlQuery).

## Wrap-up

Now that you have learned how to work with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data and persist trained R models to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], it should be relatively easy for you to create some additional models based on this data set. For example, you might try creating models like these:

-   A regression model that predicts the tip amount
-   A multiclass classification model that predicts whether the tip will be big, medium, or small

We also recommend that you check out some of these additional samples and resources:

+ [Data science scenarios and solution templates](data-science-scenarios-and-solution-templates.md)
+ [In-database advanced analytics for the SQL developer](sqldev-in-database-r-for-sql-developers.md)
+ [How-to guides for data analysis in Microsoft R](https://docs.microsoft.com/r-server/r/how-to-introduction)
+ [Tutorials and sample data for Microsoft R](https://docs.microsoft.com/r-server/r/tutorial-introduction)

## Previous step

[6. Build and Save a Model](/walkthrough-build-and-save-the-model.md)
