---
title: "Step 5: Train and Save a Model using T-SQL | Microsoft Docs"
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
ms.assetid: 3282e8ed-b515-4ed5-8543-fcef68629a92
caps.latest.revision: 10
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Step 5: Train and Save a Model using T-SQL

In this step, you'll learn how to train a machine learning model by using R. The R packages are already installed with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] so you can call the algorithm from a stored procedure. You'll train the model using the data features you just created, and then save the trained model in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.

## Building an R Model using Stored Procedures

All calls to the R runtime that is installed with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] are done by using the system stored procedure, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). However, if you need to retrain a model, it is probably easier to encapsulate the call to  sp_execute_exernal_script in another stored procedure.

In this section, you'll create a stored procedure that can be used to build a model using the data you just prepared. This stored procedure defines the input data and uses an R package to create a logistic regression model.

#### To create the stored procedure

1.  In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], open a new Query window and run the following statement to create the stored procedure _TrainTipPredictionModel_.

    Note that, because the stored procedure already includes a definition of the input data, you don't need to provide an input query.

    ```
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
  
    This stored procedure performs these activities as part of model training:
  
    -   To ensure that some data is left over to test the model, 70% of the data are randomly selected from the taxi data table.
  
    -   The SELECT query uses the custom scalar function _fnCalculateDistance_ to calculate the direct distance between the pick-up and drop-off locations.  the results of the query are stored in the default R input variable, `InputDataset`.
  
    -   the R script calls the `rxLogit` function, which is one of the enhanced R functions included with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], to create the logistic regression model.
  
        The binary variable _tipped_ is used as the *label* or outcome column,  and the model is fit using these feature columns:  _passenger_count_, _trip_distance_, _trip_time_in_secs_, and _direct_distance_.
  
    -   The trained model, saved in the R variable `logitObj`, is serialized and put in a data frame for output to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. That output is inserted into the database table _nyc_taxi_models_, so that you can use it for future predictions.
  
2.  Run the statement to create the stored procedure.
  
#### To create the R model using the stored procedure
  
1.  In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], run this statement.
  
    ```
    EXEC TrainTipPredictionModel
    ```
  
    Processing of the data and fitting the model  might take a while. Messages that would be piped to R's **stdout** stream are displayed in the **Messages** window of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. For example:
  
  
*STDOUT message(s) from external script: Rows Read: 1193025, Total Rows Processed: 1193025, Total Chunk Time: 0.093 seconds*
  
Successive chunks of data are read and processed. You might also see messages specific to the individual function, `rxLogit`, indicating the variable and test metrics.
  
2.  Open the table *nyc_taxi_models*. You can see that one new row has been added, which contains the serialized model in the column _model_.

    *model*
    *0x580A00000002000302020....*

In the next step you'll use the trained model to create predictions.

## Next Step

[Step 6: Operationalize the Model](../../advanced-analytics/r-services/step-6-operationalize-the-model-in-database-advanced-analytics-tutorial.md)

## Previous Step

[Step 4: Create Data Features using T-SQL](../../advanced-analytics/r-services/step-4-create-data-features-using-t-sql-in-database-advanced-analytics-tutorial.md)

## See Also

[In-Database Advanced Analytics for SQL Developers &#40;Tutorial&#41;](../../advanced-analytics/r-services/in-database-advanced-analytics-for-sql-developers-tutorial.md)
[SQL Server R Services Tutorials](../../advanced-analytics/r-services/sql-server-r-services-tutorials.md)
  
  
  

