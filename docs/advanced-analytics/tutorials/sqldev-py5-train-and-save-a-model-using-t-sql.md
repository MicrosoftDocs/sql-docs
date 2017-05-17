---
title: "Step 5: Train and Save a Model using T-SQL | Microsoft Docs"
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
  - "SQL Server 20172"
dev_langs: 
  - "Python"
  - "TSQL"
ms.assetid: 
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Step 5: Train and Save a Model using T-SQL

In this step, you'll learn how to train a machine learning model by using Python. The Python libraries are already installed with SQL Server Machine Learning Services, so you can load the modules and call the necessary functions from within a stored procedure. You'll train the model using the data features you just created, and then save the trained model in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.

## Build a Python Model using Stored Procedures

All calls to the Python runtime that is installed with SQL Server are done by using the system stored procedure, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). However, if you need to retrain a model, it is probably easier to encapsulate the call to  sp_execute_exernal_script in another stored procedure.

In this section, you'll create a stored procedure that can be used to train a model using the data you just prepared. This stored procedure defines the input data and uses a **scikit-learn** function to train a logistic regression model.

### Create the stored procedure

1.  In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], open a new Query window and run the following statement to create the stored procedure _TrainTipPredictionModelSciKitPy_.  Because the stored procedure already includes a definition of the input data, you don't need to provide an input query.
  
```
CREATE PROCEDURE [dbo].[TrainTipPredictionModelSciKitPy]

AS
BEGIN
  DECLARE @inquery nvarchar(max) = N'
	select tipped, fare_amount, passenger_count, trip_time_in_secs, trip_distance,
    dbo.fnCalculateDistance(pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as direct_distance
    from nyctaxi_sample
    tablesample (70 percent) repeatable (98052)
'
  -- Insert the trained model into a database table
  INSERT INTO nyc_taxi_models
  EXEC sp_execute_external_script @language = N'Python',
                                  @script = N'

from sklearn.linear_model import LogisticRegression
import numpy
import pickle

## Create model
X = InputDataSet[["passenger_count", "trip_distance", "trip_time_in_secs", "direct_distance"]]
y = numpy.ravel(InputDataSet[["tipped"]])

SKLalgo = LogisticRegression()
logitObj = SKLalgo.fit(X, y)

## Serialize model and put it in data frame
trained_model = pandas.DataFrame(data = [pickle.dumps(logitObj)], columns = ["model"])
',
                                  @input_data_1 = @inquery,
                                  @output_data_1_name = N'trained_model'
  ;

END

GO
```

This stored procedure performs the following steps as part of model training:

- 70% of the data are randomly selected from the taxi data table for training.
- The SELECT query uses the custom scalar function _fnCalculateDistance_ to calculate the direct distance between the pick-up and drop-off locations. The results of the query are stored in the default Python input variable, `InputDataset`.
- The Python script calls the scikit-learn's LogisticRegression function, which is included with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], to create the logistic regression model.
- The binary variable _tipped_ is used as the *label* or outcome column,  and the model is fit using these feature columns:  _passenger_count_, _trip_distance_, _trip_time_in_secs_, and _direct_distance_.
- The trained model, contained in the Python variable `logitObj`, is serialized and put in a data frame for output [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. That output is inserted into the database table _nyc_taxi_models_ as a new row, so that you can retrieve and use it for future predictions.

2.  Run the statement to create the stored procedure.

### Create the Python model using the stored procedure

1. In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], run this statement.
  
    ```
    EXEC TrainTipPredictionModelSciKitPy
    ```
  
    Processing of the data and fitting the model might take a while. Messages that would be piped to Python's **stdout** stream are displayed in the **Messages** window of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. For example:
  
  
*STDOUT message(s) from external script:*
*C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\lib\site-packages\revoscalepy*

2. Open the table *nyc_taxi_models*. You can see that one new row has been added, which contains the serialized model in the column _model_.

*model*
*0x800363736B6C6561726E2E6C696E6561....*

In the next step you'll use the trained model to create predictions.

## Next Step

[Step 6: Operationalize the Model](sqldev-py6-operationalize-the-model.md)

## Previous Step

[Step 4: Create Data Features using T-SQL](sqldev-py5-train-and-save-a-model-using-t-sql.md)

## See Also

[Machine Learning Services with Python](../python/sql-server-python-services.md)

