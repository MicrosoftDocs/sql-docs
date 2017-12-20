---
title: "Step 6: Operationalize the Python model using SQL Server| Microsoft Docs"
ms.custom: ""
ms.date: "10/17/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: 
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "tutorial"
applies_to: 
  - "SQL Server 2017"
dev_langs: 
  - "Python"
  - "TSQL"
ms.assetid: 
caps.latest.revision: 2
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Step 6: Operationalize the Python model using SQL Server

This article is part of a tutorial, [In-database Python analytics for SQL developers](sqldev-in-database-python-for-sql-developers.md). 

In this step, you learn to *operationalize* the models that you trained and saved in the previous step.

In this scenario, operationalization means deploying the model to production for scoring. The integration with SQL Server makes this fairly easy, because you can embed Python code in a stored procedure. To get predictions from the model based on new inputs, just call the stored procedure from an application and pass the new data.

This lesson demonstrates two methods for creating predictions based on a Python model: batch scoring, and scoring row by row.

- **Batch scoring:** To provide multiple rows of input data, pass a SELECT query as an argument to the stored procedure. The result is a table of observations corresponding to the input cases.
- **Individual scoring:** Pass a set of individual parameter values as input.  The stored procedure returns a single row or value.

All the Python code needed for scoring is provided as part of the stored procedures.

| Stored procedure name | Batch or single | Model source|
|----|----|----|
|PredictTipRxPy|batch| revoscalepy model|
|PredictTipSciKitPy|batch |scikit-learn model|
|PredictTipSingleModeRxPy|single row| revoscalepy model|
|PredictTipSingleModeSciKitPy|single row| scikit-learn model|

## Batch scoring

The first two stored procedures illustrate the basic syntax for wrapping a Python prediction call in a stored procedure. Both stored procedures require a table of data as inputs.

- The name of the exact model to use is provided as input parameter to the stored procedure. The stored procedure loads the serialized  model from the database table `nyc_taxi_models`.table, using the SELECT statement in the stored procedure.
- The serialized model is stored in the Python variable `mod` for further processing using Python.
- The new cases that need to be scored are obtained from the [!INCLUDE[tsql](../../includes/tsql-md.md)] query specified in `@input_data_1`. As the query data is read, the rows are saved in the default data frame, `InputDataSet`.
- Both stored procedure use functions from `sklearn` to calculate an accuracy metric, AUC (area under curve). Accuracy metrics such as AUC can only be generated if you also provide the target label (the _tipped_ column). Predictions do not need the target label (variable `y`), but the accuracy metric calculation does.

    Therefore, if you don't have target labels for the data to be scored, you can modify the stored procedure to remove the AUC calculations, and return only the tip probabilities from the features (variable `X` in the stored procedure).

### PredictTipSciKitPy

The stored procedure should have already been created for you. If you can't find it, run the following T-SQL statements to create the stored procedures.

This stored procedure requires a model based on the scikit-learn package, because it uses functions specific to that package:

+ The data frame containing inputs is passed to the `predict_proba` function of the logistic regression model, `mod`. The `predict_proba` function (`probArray = mod.predict_proba(X)`) returns a **float** that represents the probability that a tip (of any amount) will be given.

```SQL
CREATE PROCEDURE [dbo].[PredictTipSciKitPy] (@model varchar(50), @inquery nvarchar(max))
AS
BEGIN
  DECLARE @lmodel2 varbinary(max) = (select model from nyc_taxi_models where name = @model);
  EXEC sp_execute_external_script
    @language = N'Python',
    @script = N'
        import pickle;
        import numpy;
        from sklearn import metrics
        
        mod = pickle.loads(lmodel2)
        
        X = InputDataSet[["passenger_count", "trip_distance", "trip_time_in_secs", "direct_distance"]]
        y = numpy.ravel(InputDataSet[["tipped"]])
        
        probArray = mod.predict_proba(X)
        probList = []
        for i in range(len(probArray)):
          probList.append((probArray[i])[1])
        
        probArray = numpy.asarray(probList)
        fpr, tpr, thresholds = metrics.roc_curve(y, probArray)
        aucResult = metrics.auc(fpr, tpr)
        print ("AUC on testing data is: " + str(aucResult))
        
        OutputDataSet = pandas.DataFrame(data = probList, columns = ["predictions"])
        ',	
    @input_data_1 = @inquery,
    @input_data_1_name = N'InputDataSet',
    @params = N'@lmodel2 varbinary(max)',
    @lmodel2 = @lmodel2
  WITH RESULT SETS ((Score float));
END
GO
```

### PredictTipRxPy

This stored procedure uses the same inputs and creates the same type of scores as the previous stored procedure, but uses functions from the **revoscalepy** package provided with SQL Server machine learning.

> [!NOTE] 
> The code for this stored procedure has changed slightly between early release versions and the RTM version, to reflect changes to the revoscalepy package. See the [Changes](#changes) table for details.

```SQL
CREATE PROCEDURE [dbo].[PredictTipRxPy] (@model varchar(50), @inquery nvarchar(max))
AS
BEGIN
  DECLARE @lmodel2 varbinary(max) = (select model from nyc_taxi_models2 where name = @model);

  EXEC sp_execute_external_script 
    @language = N'Python',
    @script = N'
      import pickle;
      import numpy;
      from sklearn import metrics
      from revoscalepy.functions.RxPredict import rx_predict;
      
      mod = pickle.loads(lmodel2)
      X = InputDataSet[["passenger_count", "trip_distance", "trip_time_in_secs", "direct_distance"]]
      y = numpy.ravel(InputDataSet[["tipped"]])
      
      probArray = rx_predict(mod, X)
      prob_list = prob_array["tipped_Pred"].values 
      
      probArray = numpy.asarray(probList)
      fpr, tpr, thresholds = metrics.roc_curve(y, probArray)
      aucResult = metrics.auc(fpr, tpr)
      print ("AUC on testing data is: " + str(aucResult))
      OutputDataSet = pandas.DataFrame(data = probList, columns = ["predictions"])
      ',	
    @input_data_1 = @inquery,
    @input_data_1_name = N'InputDataSet',
    @params = N'@lmodel2 varbinary(max)',
    @lmodel2 = @lmodel2
  WITH RESULT SETS ((Score float));
END
GO
```

## Run batch scoring using a SELECT query

The stored procedures **PredictTipSciKitPy** and **PredictTipRxPy** require two input parameters: 

- The query that retrieves the data for scoring
- The name of a trained model

By passing those arguments to the stored procedure, you can select a particular model or change the data used for scoring.

1. To use the **scikit-learn** model for scoring, call the stored procedure **PredictTipSciKitPy**, passing the model name and query string as inputs.

    ```SQL
    DECLARE @query_string nvarchar(max) -- Specify input query
      SET @query_string='
      select tipped, fare_amount, passenger_count, trip_time_in_secs, trip_distance,
      dbo.fnCalculateDistance(pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as direct_distance
      from nyctaxi_sample_testing'
    EXEC [dbo].[PredictTipSciKitPy] 'SciKit_model', @query_string;
    ```

    The stored procedure returns predicted probabilities for each trip that was passed in as part of the input query. 
    
    If you are using SSMS (SQL Server Management Studio) for running queries, the probabilities will appear as a table in the **Results** pane. The **Messages** pane outputs the accuracy metric (AUC or area under curve) with a value of around 0.56.

2. To use the **revoscalepy** model for scoring, call the stored procedure **PredictTipRxPy**, passing the model name and query string as inputs.

    ```SQL
    EXEC [dbo].[PredictTipRxPy] 'revoscalepy_model', @query_string;
    ```

## Single-row scoring

Sometimes, instead of batch scoring, you might want to pass in a single case, getting values from an application, and returning a single result based on those values. For example, you could set up an Excel worksheet, web application, or report to call the stored procedure and pass to it inputs typed or selected by users.

In this section, you'll learn how to create single predictions by calling two stored procedures:

+ [PredictTipSingleModeSciKitPy](#PredictTipSingleModeSciKitPy) is designed for single-row scoring using the scikit-learn model.
+ [PredictTipSingleModeRxPy](#PredictTipSingleModeRxPy) is designed for single-row scoring using the revoscalepy model.
+ If you haven't trained a model yet, return to [Step 5](sqldev-py5-train-and-save-a-model-using-t-sql.md)!

Both models take as input a series of single values, such as passenger count, trip distance, and so forth. A table-valued function, `fnEngineerFeatures`, is used to convert latitude and longitude values from the inputs to a new feature, direct distance. [Lesson 4](sqldev-py4-create-data-features-using-t-sql.md) contains a description of this table-valued function.

Both stored procedures create a score based on the Python model.

> [!NOTE]
> 
> It is important that you provide all the input features required by the Python model when you call the stored procedure from an external application. To avoid errors, you might need to cast or convert the input data to a Python data type, in addition to validating data type and data length.

### PredictTipSingleModeSciKitPy

Take a minute to review the code of the stored procedure that performs scoring using the **scikit-learn** model.

```SQL
CREATE PROCEDURE [dbo].[PredictTipSingleModeSciKitPy] (@model varchar(50), @passenger_count int = 0,
  @trip_distance float = 0,
  @trip_time_in_secs int = 0,
  @pickup_latitude float = 0,
  @pickup_longitude float = 0,
  @dropoff_latitude float = 0,
  @dropoff_longitude float = 0)
AS
BEGIN
  DECLARE @inquery nvarchar(max) = N'
  SELECT * FROM [dbo].[fnEngineerFeatures]( 
    @passenger_count,
    @trip_distance,
    @trip_time_in_secs,
    @pickup_latitude,
    @pickup_longitude,
    @dropoff_latitude,
    @dropoff_longitude)
    '
  DECLARE @lmodel2 varbinary(max) = (select model from nyc_taxi_models2 where name = @model);
  EXEC sp_execute_external_script 
    @language = N'Python',
    @script = N'
      import pickle;
      import numpy;
      
      # Load model and unserialize
      mod = pickle.loads(model)
      
      # Get features for scoring from input data
      X = InputDataSet[["passenger_count", "trip_distance", "trip_time_in_secs", "direct_distance"]]
      
      # Score data to get tip prediction probability as a list (of float)
      probList = []
      probList.append((mod.predict_proba(X)[0])[1])
      
      # Create output data frame
      OutputDataSet = pandas.DataFrame(data = probList, columns = ["predictions"])
    ',
    @input_data_1 = @inquery,
    @params = N'@model varbinary(max),@passenger_count int,@trip_distance float,
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
GO
```

### PredictTipSingleModeRxPy

The following stored procedure performs scoring using the **revoscalepy** model.

```SQL
CREATE PROCEDURE [dbo].[PredictTipSingleModeRxPy] (@model varchar(50), @passenger_count int = 0,
  @trip_distance float = 0,
  @trip_time_in_secs int = 0,
  @pickup_latitude float = 0,
  @pickup_longitude float = 0,
  @dropoff_latitude float = 0,
  @dropoff_longitude float = 0)
AS
BEGIN
  DECLARE @inquery nvarchar(max) = N'
    SELECT * FROM [dbo].[fnEngineerFeatures]( 
      @passenger_count,
      @trip_distance,
      @trip_time_in_secs,
      @pickup_latitude,
      @pickup_longitude,
      @dropoff_latitude,
      @dropoff_longitude)
    '
  DECLARE @lmodel2 varbinary(max) = (select model from nyc_taxi_models2 where name = @model);
  EXEC sp_execute_external_script 
    @language = N'Python',
    @script = N'
      import pickle;
      import numpy;
      from revoscalepy.functions.RxPredict import rx_predict;
      
      # Load model and unserialize
      mod = pickle.loads(model)
      
      # Get features for scoring from input data
      X = InputDataSet[["passenger_count", "trip_distance", "trip_time_in_secs", "direct_distance"]]
      
      # Score data to get tip prediction probability as a list (of float)
      
      probArray = rx_predict(mod, X)
      
      probList = []
      prob_list = prob_array["tipped_Pred"].values
      
      # Create output data frame
      OutputDataSet = pandas.DataFrame(data = probList, columns = ["predictions"])
      ',
    @input_data_1 = @inquery,
    @params = N'@model varbinary(max),@passenger_count int,@trip_distance float,
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
GO
```

### Generate scores from models

After the stored procedures have been created, it is easy to generate a score based on either model. Just open a new **Query** window, and type or paste parameters for each of the feature columns. The seven required values are for these feature columns, in order:
    
+ *passenger_count*
+ *trip_distance*
v*trip_time_in_secs*
+ *pickup_latitude*
+ *pickup_longitude*
+ *dropoff_latitude*
+ *dropoff_longitude*

1. To generate a prediction by using the **revoscalepy** model, run this statement:
  
    ```SQL
    EXEC [dbo].[PredictTipSingleModeRxPy] 'revoscalepy_model', 1, 2.5, 631, 40.763958,-73.973373, 40.782139,-73.977303
    ```

2. To generate a score by using the **scikit-learn** model, run this statement:

    ```SQL
    EXEC [dbo].[PredictTipSingleModeSciKitPy] 'linear_model', 1, 2.5, 631, 40.763958,-73.973373, 40.782139,-73.977303
    ```

The output from both procedures is a probability of a tip being paid for the taxi trip with the specified parameters or features.

### <a name="changes"></a> Changes

This section lists changes to the code used in this tutorial. These changes were made to reflect the latest **revoscalepy** version. For API help, see [Python function library reference](https://docs.microsoft.com/machine-learning-server/python-reference/introducing-python-package-reference).

| Change details | Notes|
| ----|----|
| deleted `import pandas` in all samples| pandas now loaded by default|
| function `rx_predict_ex` changed to `rx_predict`| RTM and pre-release versions require `rx_predict_ex`|
| function `rx_logit_ex` changed to `rx_logit`| RTM and pre-release versions require `rx_logit_ex`|
| ` probList.append(probArray._results["tipped_Pred"])` changed to `prob_list = prob_array["tipped_Pred"].values`| updates to API|

If you installed Python Services using a prerelease version of SQL Server 2017, we recommend that you upgrade. You can also upgrade just the Python and R components by using the latest release of Machine Learning Server. For more information, see [Using binding to upgrade an instance of SQL Server](../r/use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

## Conclusions

In this tutorial, you've learned how to work with Python code embedded in stored procedures. The integration with [!INCLUDE[tsql](../../includes/tsql-md.md)] makes it much easier to deploy Python models for prediction and to incorporate model retraining as part of an enterprise data workflow.

## Previous step

[Step 5: Train and save a Python model](sqldev-py5-train-and-save-a-model-using-t-sql.md)

## See also

[Machine Learning Services with Python](../python/sql-server-python-services.md)
