---
title: "Tutorial: Deploy a predictive model in R"
titleSuffix: SQL Database Machine Learning Services
description: In part four of this four-part tutorial, you'll deploy a predictive model in R with SQL machine learning services.
ms.prod: sql
ms.technology: machine-learning
ms.topic: tutorial
author: cawrites
ms.author: chadam
ms.reviewer: garye
ms.date: 04/24/2020
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-current||=sqlallproducts-allversions"
---

# Tutorial: Deploy a predictive model in R with SQL machine learning services

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

In this article, using the R scripts you developed in parts one and two, you'll learn how to:

In part four of this four-part tutorial, you'll deploy a predictive model, developed in R, into a SQL database using SQL machine learning services.

You'll create a stored procedure with an embedded R script that makes predictions using the model. Because your model executes in the SQL database, it can easily be trained against data stored in the database.

In this article, using the R scripts you developed in parts two and three, you'll learn how to:

> [!div class="checklist"]

> * Create a stored procedure that generates the machine learning model
> * Store the model in a database table
> * Create a stored procedure that makes predictions using the model
> * Execute the model with new data

In [part two](r-tutorial-predictive-model-prepare-data.md), you learned how to import a sample database and then prepare the data to be used for training a predictive model in R.

In [part three](r-tutorial-predictive-model-train.md), you learned how to create and train multiple machine learning models in R, and then choose the most accurate one.

## Prerequisites

* Part four of this tutorial series assumes you have fulfilled the prerequisites of [**part one**](r-tutorial-predictive-model-introduction.md), and completed the steps in [**part two**](r-tutorial-predictive-model-prepare-data.md) and [**part three**](r-tutorial-predictive-model-train.md).

## Create a stored procedure that generates the model

In part three of this tutorial series, you decided that a decision tree (dtree) model was the most accurate. Now, using the R scripts you developed, create a stored procedure (`generate_rental_rx_model`) that trains and generates the dtree model using rpart from the R package.

Run the following commands in Azure Data Studio.

```sql
-- Stored procedure that trains and generates an R model using the rental_data and a decision tree algorithm
DROP PROCEDURE IF EXISTS generate_rental_rx_model;
GO
CREATE PROCEDURE generate_rental_rx_model (@trained_model VARBINARY(max) OUTPUT)
AS
BEGIN
    EXECUTE sp_execute_external_script @language = N'R'
        , @script = N'
require("rxdtree");

rental_train_data$Holiday <- factor(rental_train_data$Holiday);
rental_train_data$Snow    <- factor(rental_train_data$Snow);
rental_train_data$WeekDay <- factor(rental_train_data$WeekDay);

#Create a dtree model and train it using the training data set
model_dtree <- rxDTree(RentalCount ~ Month + Day + WeekDay + Snow + Holiday, data = rental_train_data);
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

1. Create a table (`rental_rx_models`) for storing the model.

    ```sql
    USE TutorialDB;
    DROP TABLE IF EXISTS rental_rx_models;
    GO
    CREATE TABLE rental_rx_models (
          model_name VARCHAR(30) NOT NULL DEFAULT('default model') PRIMARY KEY
        , model VARBINARY(MAX) NOT NULL
        );
    GO
    ```

1. Save the model to the table as a binary object, with the model name "rxDTree".

    ```sql
    -- Save model to table
    TRUNCATE TABLE rental_rx_models;
    
    DECLARE @model VARBINARY(MAX);
    
    EXECUTE generate_rental_rx_model @model OUTPUT;
    
    INSERT INTO rental_rx_models (
          model_name
        , model
        )
    VALUES (
         'rxDTree'
        , @model
        );
    
    SELECT *
    FROM rental_rx_models;
    ```

## Create a stored procedure that makes predictions

Create a stored procedure (`predict_rentalcount_new`) that makes predictions using the trained model and a set of new data.

```sql
-- Stored procedure that takes model name and new data as input parameters and predicts the rental count for the new data
DROP PROCEDURE IF EXISTS predict_rentalcount_new;
GO
CREATE PROCEDURE predict_rentalcount_new (
      @model_name VARCHAR(100)
    , @input_query NVARCHAR(MAX)
    )
AS
BEGIN
    DECLARE @rx_model VARBINARY(MAX) = (
            SELECT model
            FROM rental_rx_models
            WHERE model_name = @model_name
            );

    EXECUTE sp_execute_external_script @language = N'R'
        , @script = N'
require("RevoScaleR");

#Convert types to factors
rentals$Holiday <- factor(rentals$Holiday);
rentals$Snow    <- factor(rentals$Snow);
rentals$WeekDay <- factor(rentals$WeekDay);

#Before using the model to predict, we need to unserialize it
rental_model <- unserialize(rx_model);

#Call prediction function
rental_predictions <- rxPredict(rental_model, rentals);
'
        , @input_data_1 = @input_query
        , @input_data_1_name = N'rentals'
        , @output_data_1_name = N'rental_predictions'
        , @params = N'@rx_model varbinary(max)'
        , @rx_model = @rx_model
    WITH RESULT SETS(("RentalCount_Predicted" FLOAT));
END;
GO
```

## Execute the model with new data

Now you can use the stored procedure `predict_rentalcount_new` to predict the rental count from new data.

```sql
-- Use the predict_rentalcount_new stored procedure with the model name and a set of features to predict the rental count
EXECUTE dbo.predict_rentalcount_new @model_name = 'rxDTree'
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

You have successfully created, trained, and deployed a model in an SQL database. You then used that model in a stored procedure to predict values based on new data.


## Clean up resources

When you've finished using the TutorialDB database, delete it from your SQL Database server.

## Next steps

In part four of this tutorial series, you completed these steps:

* Create a stored procedure that generates the machine learning model
* Store the model in a database table
* Create a stored procedure that makes predictions using the model
* Execute the model with new data
