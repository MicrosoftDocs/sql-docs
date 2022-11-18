---
title: "Tutorial: Deploy a predictive model in R"
titleSuffix: SQL machine learning
description: In part four of this four-part tutorial, you'll deploy a predictive model in R with SQL machine learning.
ms.service: sql
ms.subservice: machine-learning
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: garye, jroth
ms.date: 05/21/2020
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Tutorial: Deploy a predictive model in R with SQL machine learning
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
In part four of this four-part tutorial series, you'll deploy a machine learning model developed in R into SQL Server Machine Learning Services or on Big Data Clusters.
::: moniker-end
::: moniker range="=sql-server-2017"
In part four of this four-part tutorial series, you'll deploy a machine learning model developed in R into SQL Server using Machine Learning Services.
::: moniker-end
::: moniker range="=sql-server-2016"
In part four of this four-part tutorial series, you'll deploy a machine learning model developed in R into SQL Server using SQL Server R Services.
::: moniker-end
::: moniker range="=azuresqldb-mi-current"
In part four of this four-part tutorial series, you'll deploy a machine learning model developed in R into Azure SQL Managed Instance using Machine Learning Services.
::: moniker-end

In this article, you'll learn how to:

> [!div class="checklist"]
> * Create a stored procedure that generates the machine learning model
> * Store the model in a database table
> * Create a stored procedure that makes predictions using the model
> * Execute the model with new data

In [part one](r-predictive-model-introduction.md), you learned how to restore the sample database.

In [part two](r-predictive-model-prepare-data.md), you learned how to import a sample database and then prepare the data to be used for training a predictive model in R.

In [part three](r-predictive-model-train.md), you learned how to create and train multiple machine learning models in R, and then choose the most accurate one.

## Prerequisites

Part four of this tutorial assumes you fulfilled the prerequisites of [**part one**](r-predictive-model-introduction.md) and completed the steps in [**part two**](r-predictive-model-prepare-data.md) and [**part three**](r-predictive-model-train.md).

## Create a stored procedure that generates the model

In part three of this tutorial series, you decided that a decision tree (dtree) model was the most accurate. Now, using the R scripts you developed, create a stored procedure (`generate_rental_model`) that trains and generates the dtree model using rpart from the R package.

Run the following commands in Azure Data Studio.

```sql
USE [TutorialDB]
DROP PROCEDURE IF EXISTS generate_rental_model;
GO
CREATE PROCEDURE generate_rental_model (@trained_model VARBINARY(max) OUTPUT)
AS
BEGIN
    EXECUTE sp_execute_external_script @language = N'R'
        , @script = N'
rental_train_data$Month   <- factor(rental_train_data$Month);
rental_train_data$Day     <- factor(rental_train_data$Day);
rental_train_data$Holiday <- factor(rental_train_data$Holiday);
rental_train_data$Snow    <- factor(rental_train_data$Snow);
rental_train_data$WeekDay <- factor(rental_train_data$WeekDay);

#Create a dtree model and train it using the training data set
library(rpart);
model_dtree <- rpart(RentalCount ~ Month + Day + WeekDay + Snow + Holiday, data = rental_train_data);
#Serialize the model before saving it to the database table
trained_model <- as.raw(serialize(model_dtree, connection=NULL));
'
        , @input_data_1 = N'
            SELECT RentalCount
                 , Year
                 , Month
                 , Day
                 , WeekDay
                 , Snow
                 , Holiday
            FROM dbo.rental_data
            WHERE Year < 2015
            '
        , @input_data_1_name = N'rental_train_data'
        , @params = N'@trained_model varbinary(max) OUTPUT'
        , @trained_model = @trained_model OUTPUT;
END;
GO
```

## Store the model in a database table

Create a table in the TutorialDB database and then save the model to the table.

1. Create a table (`rental_models`) for storing the model.

    ```sql
    USE TutorialDB;
    DROP TABLE IF EXISTS rental_models;
    GO
    CREATE TABLE rental_models (
          model_name VARCHAR(30) NOT NULL DEFAULT('default model') PRIMARY KEY
        , model VARBINARY(MAX) NOT NULL
        );
    GO
    ```

1. Save the model to the table as a binary object, with the model name "DTree".

    ```sql
    -- Save model to table
    TRUNCATE TABLE rental_models;
    
    DECLARE @model VARBINARY(MAX);
    
    EXECUTE generate_rental_model @model OUTPUT;
    
    INSERT INTO rental_models (
          model_name
        , model
        )
    VALUES (
         'DTree'
        , @model
        );
    
    SELECT *
    FROM rental_models;
    ```

## Create a stored procedure that makes predictions

Create a stored procedure (`predict_rentalcount_new`) that makes predictions using the trained model and a set of new data.

```sql
-- Stored procedure that takes model name and new data as input parameters and predicts the rental count for the new data
USE [TutorialDB]
DROP PROCEDURE IF EXISTS predict_rentalcount_new;
GO
CREATE PROCEDURE predict_rentalcount_new (
      @model_name VARCHAR(100)
    , @input_query NVARCHAR(MAX)
    )
AS
BEGIN
    DECLARE @model VARBINARY(MAX) = (
            SELECT model
            FROM rental_models
            WHERE model_name = @model_name
            );

    EXECUTE sp_execute_external_script @language = N'R'
        , @script = N'
#Convert types to factors
rentals$Month   <- factor(rentals$Month);
rentals$Day     <- factor(rentals$Day);
rentals$Holiday <- factor(rentals$Holiday);
rentals$Snow    <- factor(rentals$Snow);
rentals$WeekDay <- factor(rentals$WeekDay);

#Before using the model to predict, we need to unserialize it
rental_model <- unserialize(model);

#Call prediction function
rental_predictions <- predict(rental_model, rentals);
rental_predictions <- data.frame(rental_predictions);
'
        , @input_data_1 = @input_query
        , @input_data_1_name = N'rentals'
        , @output_data_1_name = N'rental_predictions'
        , @params = N'@model varbinary(max)'
        , @model = @model
    WITH RESULT SETS(("RentalCount_Predicted" FLOAT));
END;
GO
```

## Execute the model with new data

Now you can use the stored procedure `predict_rentalcount_new` to predict the rental count from new data.

```sql
-- Use the predict_rentalcount_new stored procedure with the model name and a set of features to predict the rental count
EXECUTE dbo.predict_rentalcount_new @model_name = 'DTree'
    , @input_query = '
        SELECT CONVERT(INT,  3) AS Month
             , CONVERT(INT, 24) AS Day
             , CONVERT(INT,  4) AS WeekDay
             , CONVERT(INT,  1) AS Snow
             , CONVERT(INT,  1) AS Holiday
        ';
GO
```

You should see a result similar to the following.

```results
RentalCount_Predicted
332.571428571429
```

You have successfully created, trained, and deployed a model in a database. You then used that model in a stored procedure to predict values based on new data.


## Clean up resources

When you've finished using the TutorialDB database, delete it from your server.

## Next steps

In part four of this tutorial series, you learned how to:

* Create a stored procedure that generates the machine learning model
* Store the model in a database table
* Create a stored procedure that makes predictions using the model
* Execute the model with new data

To learn more about using R in Machine Learning Services, see:

* [Run simple R scripts](quickstart-r-create-script.md)
* [R data structures, types and objects](quickstart-r-data-types-and-objects.md)
* [R functions](quickstart-r-functions.md)
