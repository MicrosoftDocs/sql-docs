---
title: "Python tutorial: Deploy model"
description: In part four of this four-part tutorial series, you'll deploy a Python model to predict ski rentals into a SQL Server database using Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning
ms.date: 09/03/2019
ms.topic: tutorial
author: dphansen
ms.author: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Python Tutorial: Deploy a linear regression model to SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

In part four of this four-part tutorial series, you'll deploy a linear regression model developed in Python into a SQL Server database using Machine Learning Services.

In this article, you'll learn how to:

> [!div class="checklist"]
> * Create a stored procedure that generates the machine learning model
> * Store the model in a database table
> * Create a stored procedure that makes predictions using the model
> * Execute the model with new data

In [part one](python-ski-rental-linear-regression.md), you learned how to restore the sample database.

In [part two](python-ski-rental-linear-regression-prepare-data.md), you learned how to load the data from SQL Server into a Python data frame, and prepare the data in Python.

In [part three](python-ski-rental-linear-regression-train-model.md), you learned how to train a linear regression machine learning model in Python.

## Prerequisites

* Part four of this tutorial assumes you have completed [part one](python-ski-rental-linear-regression.md) and its prerequisites.

## Create a stored procedure that generates the model

Now, using the Python scripts you developed, create a stored procedure **generate_rental_py_model** that trains and generates the linear regression model using LinearRegression from  scikit-learn.

Run the following T-SQL statement in Azure Data Studio to create the stored procedure to train the model.

```sql
-- Stored procedure that trains and generates a Python model using the rental_data and a decision tree algorithm
DROP PROCEDURE IF EXISTS generate_rental_py_model;
go
CREATE PROCEDURE generate_rental_py_model (@trained_model varbinary(max) OUTPUT)
AS
BEGIN
    EXECUTE sp_execute_external_script
      @language = N'Python'
    , @script = N'
from sklearn.linear_model import LinearRegression
import pickle

df = rental_train_data

# Get all the columns from the dataframe.
columns = df.columns.tolist()

# Store the variable well be predicting on.
target = "RentalCount"

# Initialize the model class.
lin_model = LinearRegression()

# Fit the model to the training data.
lin_model.fit(df[columns], df[target])

# Before saving the model to the DB table, convert it to a binary object
trained_model = pickle.dumps(lin_model)'

, @input_data_1 = N'select "RentalCount", "Year", "Month", "Day", "WeekDay", "Snow", "Holiday" from dbo.rental_data where Year < 2015'
, @input_data_1_name = N'rental_train_data'
, @params = N'@trained_model varbinary(max) OUTPUT'
, @trained_model = @trained_model OUTPUT;
END;
GO
```

## Store the model in a database table

Create a table in the TutorialDB database and then save the model to the table.

1. Run the following T-SQL statement in Azure Data Studio to create a table called **dbo.rental_py_models** which is used to store the model.

    ```sql
    USE TutorialDB;
    DROP TABLE IF EXISTS dbo.rental_py_models;
    GO
    CREATE TABLE dbo.rental_py_models (
        model_name VARCHAR(30) NOT NULL DEFAULT('default model') PRIMARY KEY,
        model VARBINARY(MAX) NOT NULL
    );
    GO
    ```

1. Save the model to the table as a binary object, with the model name **linear_model**.

    ```sql
    DECLARE @model VARBINARY(MAX);
    EXEC generate_rental_py_model @model OUTPUT;
    
    INSERT INTO rental_py_models (model_name, model) VALUES('linear_model', @model);
    ```

## Create a stored procedure that makes predictions

1. Create a stored procedure **py_predict_rentalcount** that makes predictions using the trained model and a set of new data. Run the T-SQL below in Azure Data Studio.

    ```sql
    DROP PROCEDURE IF EXISTS py_predict_rentalcount;
    GO
    CREATE PROCEDURE py_predict_rentalcount (@model varchar(100))
    AS
    BEGIN
    	DECLARE @py_model varbinary(max) = (select model from rental_py_models where model_name = @model);
    
    	EXEC sp_execute_external_script
    				@language = N'Python',
    				@script = N'
    
    # Import the scikit-learn function to compute error.
    from sklearn.metrics import mean_squared_error
    import pickle
    import pandas as pd
    
    rental_model = pickle.loads(py_model)
    
    df = rental_score_data
    
    # Get all the columns from the dataframe.
    columns = df.columns.tolist()
    
    # Variable you will be predicting on.
    target = "RentalCount"
    
    # Generate the predictions for the test set.
    lin_predictions = rental_model.predict(df[columns])
    print(lin_predictions)
    
    # Compute error between the test predictions and the actual values.
    lin_mse = mean_squared_error(lin_predictions, df[target])
    #print(lin_mse)
    
    predictions_df = pd.DataFrame(lin_predictions)
    
    OutputDataSet = pd.concat([predictions_df, df["RentalCount"], df["Month"], df["Day"], df["WeekDay"], df["Snow"], df["Holiday"], df["Year"]], axis=1)
    '
    , @input_data_1 = N'Select "RentalCount", "Year" ,"Month", "Day", "WeekDay", "Snow", "Holiday"  from rental_data where Year = 2015'
    , @input_data_1_name = N'rental_score_data'
    , @params = N'@py_model varbinary(max)'
    , @py_model = @py_model
    with result sets (("RentalCount_Predicted" float, "RentalCount" float, "Month" float,"Day" float,"WeekDay" float,"Snow" float,"Holiday" float, "Year" float));
    
    END;
    GO
    ```

1. Create a table for storing the predictions.

    ```sql
    DROP TABLE IF EXISTS [dbo].[py_rental_predictions];
    GO

    CREATE TABLE [dbo].[py_rental_predictions](
     [RentalCount_Predicted] [int] NULL,
     [RentalCount_Actual] [int] NULL,
     [Month] [int] NULL,
     [Day] [int] NULL,
     [WeekDay] [int] NULL,
     [Snow] [int] NULL,
     [Holiday] [int] NULL,
     [Year] [int] NULL
    ) ON [PRIMARY]
    GO
    ```

1. Execute the stored procedure to predict rental counts

    ```sql
    --Insert the results of the predictions for test set into a table
    INSERT INTO py_rental_predictions
    EXEC py_predict_rentalcount 'linear_model';

    -- Select contents of the table
    SELECT * FROM py_rental_predictions;
    ```

You have successfully created, trained, and deployed a model in an SQL Server Machine Learning Services. You then used that model in a stored procedure to predict values based on new data.

## Next steps

In part four of this tutorial series, you completed these steps:

* Create a stored procedure that generates the machine learning model
* Store the model in a database table
* Create a stored procedure that makes predictions using the model
* Execute the model with new data

To learn more about using Python in SQL Server Machine Learning Services, see:

+ [Python tutorials for SQL Server Machine Learning Services](sql-server-python-tutorials.md)
