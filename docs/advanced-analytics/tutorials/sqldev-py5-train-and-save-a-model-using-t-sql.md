---
title: "Step 5: Train and save a Python model using T-SQL | Microsoft Docs"
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
# Step 5: Train and save a Python model using T-SQL

This article is part of a tutorial, [In-database Python analytics for SQL developers](sqldev-in-database-python-for-sql-developers.md). 

In this step, you learn how to train a machine learning model using the Python packages **scikit-learn** and **revoscalepy**. These Python libraries are already installed with SQL Server Machine Learning Services.

You load the modules and call the necessary functions to create and train the model using a SQL Server stored procedure. The model requires the data features you engineered in earlier lessons. Finally, you save the trained model to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.

> [!IMPORTANT]
> There have been several changes in the **revoscalepy** package, which required small changes in the code for this tutorial. See the [changelist](sqldev-py6-operationalize-the-model.md#changes) at the end of this tutorial. 
> 
> If you installed Python Services using a prerelease version of SLq Server 2017, we recommend that you upgrade to the latest version. 

## Split the sample data into training and testing sets

1. You can use the stored procedure **TrainTestSplit** to divide the data in the nyctaxi\_sample table into two parts: nyctaxi\_sample\_training and nyctaxi\_sample\_testing. 

    This stored procedure should already be created for you, but you can run the following code to create it:

    ```SQL
    CREATE PROCEDURE [dbo].[TrainTestSplit] (@pct int)
    AS
    
    DROP TABLE IF EXISTS dbo.nyctaxi_sample_training
    SELECT * into nyctaxi_sample_training FROM nyctaxi_sample WHERE (ABS(CAST(BINARY_CHECKSUM(medallion,hack_license)  as int)) % 100) < @pct
    
    DROP TABLE IF EXISTS dbo.nyctaxi_sample_testing
    SELECT * into nyctaxi_sample_testing FROM nyctaxi_sample
    WHERE (ABS(CAST(BINARY_CHECKSUM(medallion,hack_license)  as int)) % 100) > @pct
    GO
    ```

2. To divide your data using a custom split, run the stored procedure, and type an integer that represents the percentage of data allocated to the training set. For example, the following statement would allocate 60% of data to the training set.

    ```SQL
    EXEC TrainTestSplit 60
    GO
    ```

## Build a logistic regression model

After the data has been prepared, you can use it to train a model. You do this by calling a stored procedure that runs some Python code, taking as input the training data table. For this tutorial, you create two models, both binary classification models:

+ The stored procedure **TrainTipPredictionModelRxPy** creates a tip prediction model using the **revoscalepy** package.
+ The stored procedure **TrainTipPredictionModelSciKitPy** creates a tip prediction model using the **scikit-learn** package.

Each stored procedure uses the input data you provide to create and train a logistic regression model. All Python code is wrapped in the system stored procedure, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

To make it easier to retrain the model on new data, you wrap the call to sp_execute_exernal_script in another stored procedure, and pass in the new training data as a parameter. This section will walk you through that process.

### TrainTipPredictionModelSciKitPy

1.  In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], open a new **Query** window and run the following statement to create the stored procedure _TrainTipPredictionModelSciKitPy_.  The stored procedure contains a definition of the input data, so you don't need to provide an input query.

    ```SQL
    DROP PROCEDURE IF EXISTS TrainTipPredictionModelSciKitPy;
    GO

    CREATE PROCEDURE [dbo].[TrainTipPredictionModelSciKitPy] (@trained_model varbinary(max) OUTPUT)
    AS
    BEGIN
      EXEC sp_execute_external_script
      @language = N'Python',
      @script = N'
      import numpy
      import pickle
      from sklearn.linear_model import LogisticRegression
      
      ##Create SciKit-Learn logistic regression model
      X = InputDataSet[["passenger_count", "trip_distance", "trip_time_in_secs", "direct_distance"]]
      y = numpy.ravel(InputDataSet[["tipped"]])
      
      SKLalgo = LogisticRegression()
      logitObj = SKLalgo.fit(X, y)
      
      ##Serialize model
      trained_model = pickle.dumps(logitObj)
      ',
      @input_data_1 = N'
      select tipped, fare_amount, passenger_count, trip_time_in_secs, trip_distance, 
      dbo.fnCalculateDistance(pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as direct_distance
      from nyctaxi_sample_training
      ',
      @input_data_1_name = N'InputDataSet',
      @params = N'@trained_model varbinary(max) OUTPUT',
      @trained_model = @trained_model OUTPUT;
      ;
    END;
    GO
    ```

2. Run the following SQL statements to insert the trained model into table nyc\_taxi_models.

    ```SQL
    DECLARE @model VARBINARY(MAX);
    EXEC TrainTipPredictionModelSciKitPy @model OUTPUT;
    INSERT INTO nyc_taxi_models (name, model) VALUES('SciKit_model', @model);
    ```

    Processing of the data and fitting the model might take a couple of mins. Messages that would be piped to Python's **stdout** stream are displayed in the **Messages** window of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. For example:

    *STDOUT message(s) from external script:*
  *C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\lib\site-packages\revoscalepy*

3. Open the table *nyc\_taxi_models*. You can see that one new row has been added, which contains the serialized model in the column _model_.

    *linear_model*
    *0x800363736B6C6561726E2E6C696E6561....*

### TrainTipPredictionModelRxPy

This stored procedure uses the new **revoscalepy** package, which is a new package for Python. It contains objects, transformation, and algorithms similar to those provided for the R language's **RevoScaleR** package. 

By using **revoscalepy**, you can create remote compute contexts, move data between compute contexts, transform data, and train predictive models using popular algorithms such as logistic and linear regression, decision trees, and more. For more information, see [what is revoscalepy?](../python/what-is-revoscalepy.md)

1. In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], open a new **Query** window and run the following statement to create the stored procedure _TrainTipPredictionModelRxPy_.  Because the stored procedure already includes a definition of the input data, you don't need to provide an input query.

    ```SQL
    DROP PROCEDURE IF EXISTS TrainTipPredictionModelRxPy;
    GO

    CREATE PROCEDURE [dbo].[TrainTipPredictionModelRxPy] (@trained_model varbinary(max) OUTPUT)
    AS
    BEGIN
    EXEC sp_execute_external_script 
      @language = N'Python',
      @script = N'
    import numpy
    import pickle
    from revoscalepy.functions.RxLogit import rx_logit
    
    ## Create a logistic regression model using rx_logit function from revoscalepy package
    logitObj = rx_logit("tipped ~ passenger_count + trip_distance + trip_time_in_secs + direct_distance", data = InputDataSet);
    
    ## Serialize model
    trained_model = pickle.dumps(logitObj)
    ',
    @input_data_1 = N'
    select tipped, fare_amount, passenger_count, trip_time_in_secs, trip_distance, 
    dbo.fnCalculateDistance(pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as direct_distance
    from nyctaxi_sample_training
    ',
    @input_data_1_name = N'InputDataSet',
    @params = N'@trained_model varbinary(max) OUTPUT',
    @trained_model = @trained_model OUTPUT;
    ;
    END;
    GO
    ```

    This stored procedure performs the following steps as part of model training:

    - The SELECT query applies the custom scalar function _fnCalculateDistance_ to calculate the direct distance between the pick-up and drop-off locations. The results of the query are stored in the default Python input variable, `InputDataset`.
    - The binary variable _tipped_ is used as the *label* or outcome column, and the model is fit using these feature columns:  _passenger_count_, _trip_distance_, _trip_time_in_secs_, and _direct_distance_.
    - The trained model is serialized and stored in the Python variable `logitObj`. By adding the T-SQL keyword OUTPUT, you can add the variable as an output of the stored procedure. In the next step, that variable is used to insert the binary code of the model into a database table _nyc_taxi_models_. This mechanism makes it easy to store and re-use models.

2. Run the stored procedure as follows to insert the trained **revoscalepy** model into the table _nyc\_taxi\_models.

    ```SQL
    DECLARE @model VARBINARY(MAX);
    EXEC TrainTipPredictionModelRxPy @model OUTPUT;
    
    INSERT INTO nyc_taxi_models (name, model) VALUES('revoscalepy_model', @model);
    ```

    Processing of the data and fitting the model might take a while. Messages that would be piped to Python's **stdout** stream are displayed in the **Messages** window of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. For example:

    *STDOUT message(s) from external script:*
  *C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\lib\site-packages\revoscalepy*

3. Open the table *nyc_taxi_models*. You can see that one new row has been added, which contains the serialized model in the column _model_.

    *rx_model*
    *0x8003637265766F7363616c....*

In the next step, you use the trained models to create predictions.

## Next step

[Step 6: Operationalize the Python model using SQL Server](sqldev-py6-operationalize-the-model.md)

## Previous step

[Step 4: Create data features using T-SQL](sqldev-py5-train-and-save-a-model-using-t-sql.md)
