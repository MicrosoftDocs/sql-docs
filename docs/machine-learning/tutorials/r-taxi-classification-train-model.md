---
title: "R tutorial: Train and save model"
titleSuffix: SQL machine learning
description: In part four of this five-part tutorial series, you'll train and save a model in R using Transact-SQL on SQL Server with SQL machine learning.
ms.service: sql
ms.subservice: machine-learning

ms.date: 10/15/2020
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||>=azuresqldb-mi-current"
---

# R tutorial: Train and save model
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

In part four of this five-part tutorial series, you'll learn how to train a machine learning model by using R. You'll train the model using the data features you created in the previous part, and then save the trained model in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. In this case, the R packages are already installed with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], so everything can be done from SQL.

In this article, you'll:

> [!div class="checklist"]
> + Create and train a model using a SQL stored procedure
> + Save the trained model to a SQL table

In [part one](r-taxi-classification-introduction.md), you installed the prerequisites and restored the sample database.

In [part two](r-taxi-classification-explore-data.md), you reviewed the sample data and generate some plots.

In [part three](r-taxi-classification-create-features.md), you learned how to create features from raw data by using a Transact-SQL function. You then called that function from a stored procedure to create a table that contains the feature values.

In [part five](r-taxi-classification-deploy-model.md), you'll learn how to operationalize the models that you trained and saved in part four.

## Create the stored procedure

When calling R from T-SQL, you use the system stored procedure, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). However, for processes that you repeat often, such as retraining a model, it is easier to encapsulate the call to `sp_execute_external_script` in another stored procedure.

1. In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], open a new **Query** window.

2. Run the following statement to create the stored procedure **RTrainLogitModel**. This stored procedure defines the input data and uses **glm** to create a logistic regression model.

   ```sql
   CREATE PROCEDURE [dbo].[RTrainLogitModel] (@trained_model varbinary(max) OUTPUT)
   
   AS
   BEGIN
     DECLARE @inquery nvarchar(max) = N'
       select tipped, fare_amount, passenger_count,trip_time_in_secs,trip_distance,
       pickup_datetime, dropoff_datetime,
       dbo.fnCalculateDistance(pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) as direct_distance
       from nyctaxi_sample
       tablesample (70 percent) repeatable (98052)
   '
   
     EXEC sp_execute_external_script @language = N'R',
                                     @script = N'
   ## Create model
   logitObj <- glm(tipped ~ passenger_count + trip_distance + trip_time_in_secs + direct_distance, data = InputDataSet, family = binomial)
   summary(logitObj)
   
   ## Serialize model 
   trained_model <- as.raw(serialize(logitObj, NULL));
   ',
     @input_data_1 = @inquery,
     @params = N'@trained_model varbinary(max) OUTPUT',
     @trained_model = @trained_model OUTPUT; 
   END
   GO
   ```

   + To ensure that some data is left over to test the model, 70% of the data are randomly selected from the taxi data table for training purposes.

   + The SELECT query uses the custom scalar function *fnCalculateDistance* to calculate the direct distance between the pick-up and drop-off locations. The results of the query are stored in the default R input variable, `InputDataset`.
  
   + The R script calls the R function **glm** to create the logistic regression model.
  
     The binary variable _tipped_ is used as the *label* or outcome column,  and the model is fit using these feature columns:  _passenger_count_, _trip_distance_, _trip_time_in_secs_, and _direct_distance_.
  
   + The trained model, saved in the R variable `logitObj`, is serialized and returned as an output parameter.

## Train and deploy the R model using the stored procedure

Because the stored procedure already includes a definition of the input data, you don't need to provide an input query.

1. To train and deploy the R model, call the stored procedure and insert it into the database table _nyc_taxi_models_, so that you can use it for future predictions:

   ```sql
   DECLARE @model VARBINARY(MAX);
   EXEC RTrainLogitModel @model OUTPUT;
   INSERT INTO nyc_taxi_models (name, model) VALUES('RTrainLogit_model', @model);
   ```

2. Watch the **Messages** window of [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] for messages that would be piped to R's **stdout** stream, like this message: 

   "STDOUT message(s) from external script: Rows Read: 1193025, Total Rows Processed: 1193025, Total Chunk Time: 0.093 seconds"

3. When the statement has completed, open the table *nyc_taxi_models*. Processing of the data and fitting the model might take a while.

   You can see that one new row has been added, which contains the serialized model in the column _model_ and the model name **RTrainLogit_model** in the column _name_.

   ```text
   model                        name
   ---------------------------- ------------------
   0x580A00000002000302020....  RTrainLogit_model
   ```

In the next part of this tutorial you'll use the trained model to generate predictions.

## Next steps

In this article, you:

> [!div class="checklist"]
> + Created and trained a model using a SQL stored procedure
> + Saved the trained model to a SQL table

> [!div class="nextstepaction"]
> [R tutorial: Run predictions in SQL stored procedures](r-taxi-classification-deploy-model.md)
