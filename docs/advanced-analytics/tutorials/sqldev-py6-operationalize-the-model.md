---
title: "Step 6: Operationalize the Model| Microsoft Docs"
ms.custom: ""
ms.date: "04/28/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "python-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2017"
dev_langs: 
  - "Python"
  - "TSQL"
ms.assetid: 
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Step 6: Operationalize the Model

In this step, you'll learn to *operationalize* the **scikit-learn** model that you trained and saved in the previous step. Operationalize in this case means "deploying the model to production for scoring". This is easy to do if your Python code is contained in a stored procedure. You can then call the stored procedure from applications, to make predictions on new observations.

You'll learn two methods for calling a Python model from a stored procedure:

- **Batch scoring mode**: Use a SELECT query to provide multiple rows of data. The stored procedure returns a table of observations corresponding to the input cases.
-   **Individual scoring mode**: Pass a set of individual parameter values as input.  The stored procedure returns a single row or value.

First, let's see how scoring works in general.

## Basic Scoring

The stored procedure _PredictTipSciKitPy_ illustrates the basic syntax for wrapping a Python prediction call in a stored procedure.

```
CREATE PROCEDURE [dbo].[PredictTipSciKitPy] @inquery nvarchar(max)
AS
BEGIN

  DECLARE @lmodel2 varbinary(max) = (SELECT TOP 1
    model
  FROM nyc_taxi_models);
  EXEC sp_execute_external_script @language = N'Python',
                                  @script = N'
import pickle;
import numpy;
import pandas;
from sklearn import metrics

# Load model from DB table
mod = pickle.loads(model)

# Create features and target. Target (y) is not necessary for prediction, but for model performance evaluation
X = InputDataSet[["passenger_count", "trip_distance", "trip_time_in_secs", "direct_distance"]]
y = numpy.ravel(InputDataSet[["tipped"]])

probArray = mod.predict_proba(X)
probList = []
for i in range(len(probArray)):
    probList.append((probArray[i])[1])

# Calculate model performance in the form of AUC
probArray = numpy.asarray(probList)
fpr, tpr, thresholds = metrics.roc_curve(y, probArray)
aucResult = metrics.auc(fpr, tpr)
print ("AUC is: " + str(aucResult))

# Create output data frame
OutputDataSet = pandas.DataFrame(data = probList, columns = ["predictions"])
',
                                  @input_data_1 = @inquery,
                                  @params = N'@model varbinary(max)',
                                  @model = @lmodel2
  WITH RESULT SETS ((Score float));

END

GO
```
The stored procedure performs the following steps:

- The  SELECT statement in the stored procedure loads a serialized model from the database table `nyc_taxi_models`, and stores the model in the Python variable `mod` for further processing using Python.

- The new cases that need to be scored are obtained from the [!INCLUDE[tsql](../../includes/tsql-md.md)] query specified in `@inquery`, the first parameter to the stored procedure. As the query data is read, the rows are saved in the default data frame, `InputDataSet`. This data frame is passed to the `predict_proba` function of the Logistic Regression scikit-learn model, `mod`, in Python, which returns a **float** that represents the probability that a tip  (of any amount) will be given.

    `probArray = mod.predict_proba(X)`

- The stored procedure also calculates an accuracy metric, AUC (area under curve). Accuracy metrics such as AUC can only be generated if you also provide the target label (i.e. the tipped column). Predictions do not need the target label (variable `y`), but the accuracy metric calculation does.

  Therefore, if you don't have target labels for the data to be scored, you can modify the stored procedure to remove the AUC calculations, and simply return the tip probabilities from the features (in variable `X` in above stored procedure).

## Batch scoring using a SELECT query

Now let's see how batch scoring works.

1. Get input data used for scoring.
  
    ```
    select tipped, fare_amount, passenger_count, trip_time_in_secs, trip_distance,
    dbo.fnCalculateDistance(pickup_latitude, pickup_longitude, dropoff_latitude, dropoff_longitude) as direct_distance
    from nyctaxi_sample
    tablesample (10 percent) repeatable (98052)
    ```
  
    This query samples and gets 10% of the data from the taxi trip and fare table for prediction.
  
3.  To create predictions, you'll provide the query text (above) in a variable and pass it as a parameter to the stored procedure, using a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement like this.
  
    ```
    -- Specify input query
    DECLARE @query_string nvarchar(max)
    SET @query_string='
    select tipped, fare_amount, passenger_count, trip_time_in_secs, trip_distance,
        dbo.fnCalculateDistance(pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as direct_distance
        from nyctaxi_sample
        tablesample (10 percent) repeatable (98052)
        '

    -- Call stored procedure for scoring, passing in the query string
    EXEC [dbo].[PredictTipSciKitPy] @inquery = @query_string;
    ```

4.  The stored procedure returns predicted probabilities for each trip passed in with the input query. If you are using SSMS (SQL Server Management Studio) for running queries, the probabilities will appear as a table in the `results` pane. In the `messages` pane, the accuracy metric (AUC or area under curve) will be output (around 0.56 for the logistic regression model).


## Score Individual Rows

Sometimes, instead of batch scoring, you may want to pass in individual values from an application and get a single scored result based on those values. For example, you could set up an Excel worksheet, web application, or Reporting Services report to call the stored procedure and provide inputs typed or selected by users.

In this section, you'll learn how to create single predictions using a stored procedure.

1.  Take a minute to review the code of the stored procedure _PredictTipSingleModeSciKitPy_, which was included as part of the download.
  
    ```
    CREATE PROCEDURE [dbo].[PredictTipSingleModeSciKitPy] @passenger_count int = 0,
    @trip_distance float = 0,
    @trip_time_in_secs int = 0,
    @pickup_latitude float = 0,
    @pickup_longitude float = 0,
    @dropoff_latitude float = 0,
    @dropoff_longitude float = 0
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
    DECLARE @lmodel2 varbinary(max) = (SELECT TOP 1
        model
    FROM nyc_taxi_models);
    EXEC sp_execute_external_script @language = N'Python',
                                    @script = N'

    import pickle;
    import numpy;
    import pandas;

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

 The above stored procedure does the following:

    - Takes multiple single values as input, such as passenger count, trip distance, etc.
    -  Uses a table-valued function, `fnEngineerFeatures`, which takes input values and converts the latitudes and longitudes to direct distance. Refer to section 4 for description about this table-valued function.
    - If you call the stored procedure from an external application, make sure that the data matches the required input features of the Python model. This might include ensuring that the input data can be cast or converted to Python data type, or validating data type and data length.
    - The stored procedure creates a score based on the stored Python model.
  
2.  Try it out, by providing the values manually.
  
    Open a new **Query** window, and call the stored procedure, typing parameters for each of the feature columns.
  
    ```
    EXEC [dbo].[PredictTipSingleModeSciKitPy] 1, 2.5, 631, 40.763958,-73.973373, 40.782139,-73.977303
    ```
  
    The seven values are for these feature columns , in order:
  
    -   *passenger_count*
    -   *trip_distance*
    -   *trip_time_in_secs*
    -   *pickup_latitude*
    -   *pickup_longitude*
    -   *dropoff_latitude*
    -   *dropoff_longitude*
  
3.  The ouput from the stored procedure, `PredictTipSingleModeSciKitPy`, is a probability of a tip being paid for the taxi trip with the above parameters or features.
  
## Conclusions

In this tutorial, you've learned how to work with Python code embedded in stored procedures. The integration with [!INCLUDE[tsql](../../includes/tsql-md.md)] makes it much easier to deploy Python models for prediction and to incorporate model retraining as part of an enterprise data workflow.

## Previous Step
[Step 6: Operationalize the Model](sqldev-py6-operationalize-the-model.md)

## See Also

[Machine Learning Services with Python](../python/sql-server-python-services.md)

