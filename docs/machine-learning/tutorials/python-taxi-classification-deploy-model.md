---
title: "Python tutorial: Run predictions in SQL stored procedures"
titleSuffix: SQL machine learning
description: In part five of this five-part tutorial series, you'll operationalize embedded Python script in SQL stored procedures with T-SQL functions with SQL machine learning.
ms.service: sql
ms.subservice: machine-learning

ms.date: 09/17/2021
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||>=azuresqldb-mi-current"
---

# Python tutorial: Run predictions using Python embedded in a stored procedure
[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

In part five of this five-part tutorial series, you'll learn how to *operationalize* the models that you trained and saved in the previous part.

In this scenario, operationalization means deploying the model to production for scoring. The integration with SQL Server makes this fairly easy, because you can embed Python code in a stored procedure. To get predictions from the model based on new inputs, just call the stored procedure from an application and pass the new data.

This part of the tutorial demonstrates two methods for creating predictions based on a Python model: batch scoring and scoring row by row.

+ **Batch scoring:** To provide multiple rows of input data, pass a SELECT query as an argument to the stored procedure. The result is a table of observations corresponding to the input cases.
+ **Individual scoring:** Pass a set of individual parameter values as input.  The stored procedure returns a single row or value.

All the Python code needed for scoring is provided as part of the stored procedures.

In this article, you'll:

> [!div class="checklist"]
> + Create and use stored procedures for batch scoring
> + Create and use stored procedures for scoring a single row

In [part one](python-taxi-classification-introduction.md), you installed the prerequisites and restored the sample database.

In [part two](python-taxi-classification-explore-data.md), you explored the sample data and generated some plots.

In [part three](python-taxi-classification-create-features.md), you learned how to create features from raw data by using a Transact-SQL function. You then called that function from a stored procedure to create a table that contains the feature values.

In [part four](python-taxi-classification-train-model.md), you loaded the modules and called the necessary functions to create and train the model using a SQL Server stored procedure.

## Batch scoring

The first two stored procedures created using the following scripts illustrate the basic syntax for wrapping a Python prediction call in a stored procedure. Both stored procedures require a table of data as inputs.

+ The name of the model to use is provided as input parameter to the stored procedure. The stored procedure loads the serialized  model from the database table `nyc_taxi_models`.table, using the SELECT statement in the stored procedure.
+ The serialized model is stored in the Python variable `mod` for further processing using Python.
+ The new cases that need to be scored are obtained from the [!INCLUDE[tsql](../../includes/tsql-md.md)] query specified in `@input_data_1`. As the query data is read, the rows are saved in the default data frame, `InputDataSet`.
+ Both stored procedure use functions from `sklearn` to calculate an accuracy metric, AUC (area under curve). Accuracy metrics such as AUC can only be generated if you also provide the target label (the _tipped_ column). Predictions do not need the target label (variable `y`), but the accuracy metric calculation does.

  Therefore, if you don't have target labels for the data to be scored, you can modify the stored procedure to remove the AUC calculations, and return only the tip probabilities from the features (variable `X` in the stored procedure).

### PredictTipSciKitPy

Run the following T-SQL statements to create the stored procedure `PredictTipSciKitPy`. This stored procedure requires a model based on the scikit-learn package, because it uses functions specific to that package.

The data frame containing inputs is passed to the `predict_proba` function of the logistic regression model, `mod`. The `predict_proba` function (`probArray = mod.predict_proba(X)`) returns a **float** that represents the probability that a tip (of any amount) will be given.

```sql
DROP PROCEDURE IF EXISTS PredictTipSciKitPy;
GO

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

Run the following T-SQL statements to create the stored procedure `PredictTipRxPy`. This stored procedure uses the same inputs and creates the same type of scores as the previous stored procedure, but it uses functions from the **revoscalepy** package provided with SQL Server machine learning.

```sql
DROP PROCEDURE IF EXISTS PredictTipRxPy;
GO

CREATE PROCEDURE [dbo].[PredictTipRxPy] (@model varchar(50), @inquery nvarchar(max))
AS
BEGIN
DECLARE @lmodel2 varbinary(max) = (select model from nyc_taxi_models where name = @model);
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
probList = probArray["tipped_Pred"].values 

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

+ The query that retrieves the data for scoring
+ The name of a trained model

By passing those arguments to the stored procedure, you can select a particular model or change the data used for scoring.

1. To use the **scikit-learn** model for scoring, call the stored procedure **PredictTipSciKitPy**, passing the model name and query string as inputs.

   ```sql
   DECLARE @query_string nvarchar(max) -- Specify input query
     SET @query_string='
     select tipped, fare_amount, passenger_count, trip_time_in_secs, trip_distance,
     dbo.fnCalculateDistance(pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as direct_distance
     from nyctaxi_sample_testing'
   EXEC [dbo].[PredictTipSciKitPy] 'SciKit_model', @query_string;
   ```

   The stored procedure returns predicted probabilities for each trip that was passed in as part of the input query. 

   If you're using SSMS (SQL Server Management Studio) for running queries, the probabilities will appear as a table in the **Results** pane. The **Messages** pane outputs the accuracy metric (AUC or area under curve) with a value of around 0.56.

2. To use the **revoscalepy** model for scoring, call the stored procedure **PredictTipRxPy**, passing the model name and query string as inputs.

   ```sql
   DECLARE @query_string nvarchar(max) -- Specify input query
     SET @query_string='
     select tipped, fare_amount, passenger_count, trip_time_in_secs, trip_distance,
     dbo.fnCalculateDistance(pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as direct_distance
     from nyctaxi_sample_testing'
   EXEC [dbo].[PredictTipRxPy] 'revoscalepy_model', @query_string;
   ```

## Single-row scoring

Sometimes, instead of batch scoring, you might want to pass in a single case, getting values from an application, and returning a single result based on those values. For example, you could set up an Excel worksheet, web application, or report to call the stored procedure and pass to it inputs typed or selected by users.

In this section, you'll learn how to create single predictions by calling two stored procedures:

+ **PredictTipSingleModeSciKitPy** is designed for single-row scoring using the scikit-learn model.
+ **PredictTipSingleModeRxPy** is designed for single-row scoring using the revoscalepy model.
+ If you haven't trained a model yet, return to [part five](python-taxi-classification-train-model.md)!

Both models take as input a series of single values, such as passenger count, trip distance, and so forth. A table-valued function, `fnEngineerFeatures`, is used to convert latitude and longitude values from the inputs to a new feature, direct distance. [Part four](python-taxi-classification-create-features.md) contains a description of this table-valued function.

Both stored procedures create a score based on the Python model.

> [!NOTE]
>
> It's important that you provide all the input features required by the Python model when you call the stored procedure from an external application. To avoid errors, you might need to cast or convert the input data to a Python data type, in addition to validating data type and data length.

### PredictTipSingleModeSciKitPy

The following stored procedure `PredictTipSingleModeSciKitPy` performs scoring using the **scikit-learn** model.

```sql
DROP PROCEDURE IF EXISTS PredictTipSingleModeSciKitPy;
GO

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
DECLARE @lmodel2 varbinary(max) = (select model from nyc_taxi_models where name = @model);
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

The following stored procedure `PredictTipSingleModeRxPy` performs scoring using the **revoscalepy** model.

```sql
DROP PROCEDURE IF EXISTS PredictTipSingleModeRxPy;
GO

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
DECLARE @lmodel2 varbinary(max) = (select model from nyc_taxi_models where name = @model);
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
probList = probArray["tipped_Pred"].values

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

After the stored procedures have been created, it's easy to generate a score based on either model. Open a new **Query** window and provide parameters for each of the feature columns. 

The seven required values for these feature columns are, in order:

+ *passenger_count*
+ *trip_distance*
+ *trip_time_in_secs*
+ *pickup_latitude*
+ *pickup_longitude*
+ *dropoff_latitude*
+ *dropoff_longitude*

For example:

+ To generate a prediction by using the **revoscalepy** model, run this statement:
  
  ```sql
  EXEC [dbo].[PredictTipSingleModeRxPy] 'revoscalepy_model', 1, 2.5, 631, 40.763958,-73.973373, 40.782139,-73.977303
  ```

+ To generate a score by using the **scikit-learn** model, run this statement:

  ```sql
  EXEC [dbo].[PredictTipSingleModeSciKitPy] 'SciKit_model', 1, 2.5, 631, 40.763958,-73.973373, 40.782139,-73.977303
  ```

The output from both procedures is a probability of a tip being paid for the taxi trip with the specified parameters or features.

## Conclusion

In this tutorial series, you've learned how to work with Python code embedded in stored procedures. The integration with [!INCLUDE[tsql](../../includes/tsql-md.md)] makes it much easier to deploy Python models for prediction and to incorporate model retraining as part of an enterprise data workflow.

## Next steps

In this article, you:

> [!div class="checklist"]
> + Created and used stored procedures for batch scoring
> + Created and used stored procedures for scoring a single row

For more information about Python, see [Python extension in SQL Server](../concepts/extension-python.md).
