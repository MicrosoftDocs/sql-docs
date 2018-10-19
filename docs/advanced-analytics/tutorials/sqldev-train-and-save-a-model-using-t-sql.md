---
title: Lesson 3 Train and save a model using R and T-SQL (SQL Server Machine Learning) | Microsoft Docs
description: Tutorial showing how to embed R in SQL Server stored procedures and T-SQL functions 
ms.prod: sql
ms.technology: machine-learning

ms.date: 06/07/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Lesson 3: Train and save a model using T-SQL
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article is part of a tutorial for SQL developers on how to use R in SQL Server.

In this lesson, you'll learn how to train a machine learning model by using R. You'll train the model using the data features you created in the previous lesson, and then save the trained model in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. In this case, the R packages are already installed with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], so everything can be done from SQL.

## Create the stored procedure

When calling R from T-SQL, you use the system stored procedure, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). However, for processes that you repeat often, such as retraining a model, it is easier to encapsulate the call to  `sp_execute_exernal_script` in another stored procedure.

1.  First, create a stored procedure that contains the R code to build the tip prediction model. In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], open a new **Query** window and run the following statement to create the stored procedure _TrainTipPredictionModel_. This stored procedure defines the input data and uses an R package to create a logistic regression model.

    ```SQL
    CREATE PROCEDURE [dbo].[TrainTipPredictionModel]
    
    AS
    BEGIN
      DECLARE @inquery nvarchar(max) = N'
        select tipped, fare_amount, passenger_count,trip_time_in_secs,trip_distance,
        pickup_datetime, dropoff_datetime,
        dbo.fnCalculateDistance(pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as direct_distance
        from nyctaxi_sample
        tablesample (70 percent) repeatable (98052)
    '
      -- Insert the trained model into a database table
      INSERT INTO nyc_taxi_models
      EXEC sp_execute_external_script @language = N'R',
                                      @script = N'
    
    ## Create model
    logitObj <- rxLogit(tipped ~ passenger_count + trip_distance + trip_time_in_secs + direct_distance, data = InputDataSet)
    summary(logitObj)
    
    ## Serialize model and put it in data frame
    trained_model <- data.frame(model=as.raw(serialize(logitObj, NULL)));
    ',
      @input_data_1 = @inquery,
      @output_data_1_name = N'trained_model'
      ;
    
    END
    GO
    ```

    - However, to ensure that some data is left over to test the model, 70% of the data are randomly selected from the taxi data table.
    
    - The SELECT query uses the custom scalar function _fnCalculateDistance_ to calculate the direct distance between the pick-up and drop-off locations.  the results of the query are stored in the default R input variable, `InputDataset`.
  
    - The R script calls the `rxLogit` function, which is one of the enhanced R functions included with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], to create the logistic regression model.
  
        The binary variable _tipped_ is used as the *label* or outcome column,  and the model is fit using these feature columns:  _passenger_count_, _trip_distance_, _trip_time_in_secs_, and _direct_distance_.
  
    -   The trained model, saved in the R variable `logitObj`, is serialized and put in a data frame for output to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. That output is inserted into the database table _nyc_taxi_models_, so that you can use it for future predictions.
  
2.  Run the statement to create the stored procedure, if it doesn't already exist.

## Generate the R model using the stored procedure

Because the stored procedure already includes a definition of the input data, you don't need to provide an input query.

1. To generate the R model, call the stored procedure without any other parameters:

    ```SQL
    EXEC TrainTipPredictionModel
    ```

2. Watch the **Messages** window of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] for messages that would be piped to R's **stdout** stream, like this message: 

    "STDOUT message(s) from external script: Rows Read: 1193025, Total Rows Processed: 1193025, Total Chunk Time: 0.093 seconds"

    You might also see messages specific to the individual function, `rxLogit`, displaying the variables and test metrics generated as part of model creation.

3.  When the statement has completed, open the table *nyc_taxi_models*. Processing of the data and fitting the model might take a while.

    You can see that one new row has been added, which contains the serialized model in the column _model_.

    ```
    model
    ------
    0x580A00000002000302020....
    ```

In the next step you'll use the trained model to create predictions.

## Next lesson

[Lesson 4: Operationalize the model](../tutorials/sqldev-operationalize-the-model.md)

## Previous lesson

[Lesson 2: Create data features using R and T-SQL functions](..//tutorials/sqldev-create-data-features-using-t-sql.md)

