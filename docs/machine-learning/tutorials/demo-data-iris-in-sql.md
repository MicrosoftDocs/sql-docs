---
description: "Iris demo data for Python and R tutorials with SQL machine learning"
title: Iris demo data set for tutorials
titleSuffix: SQL machine learning
Description: Create a database containing the Iris dataset and predictive models. This dataset is used in R and Python tutorials with SQL machine learning.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 05/26/2020
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Iris demo data for Python and R tutorials with SQL machine learning
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

In this exercise, create a database to store data from the [Iris flower data set](https://en.wikipedia.org/wiki/Iris_flower_data_set) and models based on the same data. Iris data is included in both the R and Python distributions, and is used in machine learning tutorials for SQL machine learning.

To complete this exercise, you should have [SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md) or another tool that can run T-SQL queries.

Tutorials and quickstarts using this data set include the following:

+ [Quickstart: Create and score a predictive model in Python](quickstart-python-train-score-model.md)

## Create the database

1. Start SQL Server Management Studio, and open a new **Query** window.  

2. Create a new database for this project, and change the context of your **Query** window to use the new database.

    ```sql
    CREATE DATABASE irissql
    GO
    USE irissql
    GO
    ```

3. Add some empty tables: one to store the data, and one to store the trained models. The **iris_models** table is used for storing serialized models generated in other exercises.

    The following code creates the table for the training data.

    ```sql
    DROP TABLE IF EXISTS iris_data;
    GO
    CREATE TABLE iris_data (
      id INT NOT NULL IDENTITY PRIMARY KEY
      , "Sepal.Length" FLOAT NOT NULL, "Sepal.Width" FLOAT NOT NULL
      , "Petal.Length" FLOAT NOT NULL, "Petal.Width" FLOAT NOT NULL
      , "Species" VARCHAR(100) NOT NULL, "SpeciesId" INT NOT NULL
    );
    ```

4. Run the following code to create the table used for storing the trained model. To save Python (or R) models in SQL Server, they must be serialized and stored in a column of type **varbinary(max)**.

    ```sql
    DROP TABLE IF EXISTS iris_models;
    GO

    CREATE TABLE iris_models (
      model_name VARCHAR(50) NOT NULL DEFAULT('default model') PRIMARY KEY,
      model VARBINARY(MAX) NOT NULL
    );
    GO
    ```

    In addition to the model contents, typically, you would also add columns for other useful metadata, such as the model's name, the date it was trained, the source algorithm and parameters, source data, and so forth. For now we'll keep it simple and use just the model name.

## Populate the table

You can obtain built-in Iris data from either R or Python. You can use Python or R to load the data into a data frame, and then insert it into a table in the database. Moving training data from an external session into a table is a multistep process:

+ Design a stored procedure that gets the data you want.
+ Execute the stored procedure to actually get the data.
+ Construct an INSERT statement to specify where the retrieved data should be saved.

1. On systems with Python integration, create the following stored procedure that uses Python code to load the data.

    ```sql
    CREATE PROCEDURE get_iris_dataset
    AS
    BEGIN
    EXEC sp_execute_external_script @language = N'Python', 
    @script = N'
    from sklearn import datasets
    iris = datasets.load_iris()
    iris_data = pandas.DataFrame(iris.data)
    iris_data["Species"] = pandas.Categorical.from_codes(iris.target, iris.target_names)
    iris_data["SpeciesId"] = iris.target
    ', 
    @input_data_1 = N'', 
    @output_data_1_name = N'iris_data'
    WITH RESULT SETS (("Sepal.Length" float not null, "Sepal.Width" float not null, "Petal.Length" float not null, "Petal.Width" float not null, "Species" varchar(100) not null, "SpeciesId" int not null));
    END;
    GO
    ```

    When you run this code, you should get the message "Commands completed successfully." All this means is that the stored procedure has been created according to your specifications.

2. Alternatively, on systems having R integration, create a procedure that uses R instead.

    ```sql
    CREATE PROCEDURE get_iris_dataset
    AS
    BEGIN
    EXEC sp_execute_external_script @language = N'R', 
    @script = N'
    library(RevoScaleR)
    data(iris)
    iris$SpeciesID <- c(unclass(iris$Species))
    iris_data <- iris
    ', 
    @input_data_1 = N'', 
    @output_data_1_name = N'iris_data'
    WITH RESULT SETS (("Sepal.Length" float not null, "Sepal.Width" float not null, "Petal.Length" float not null, "Petal.Width" float not null, "Species" varchar(100) not null, "SpeciesId" int not null));
    END;
    GO
    ```

3. To actually populate the table, run the stored procedure and specify the table where the data should be written. When run, the stored procedure executes the Python or R code, which loads the built-in Iris data set, and then inserts the data into the **iris_data** table.

    ```sql
    INSERT INTO iris_data ("Sepal.Length", "Sepal.Width", "Petal.Length", "Petal.Width", "Species", "SpeciesId")
    EXEC dbo.get_iris_dataset;
    ```

    If you're new to T-SQL, be aware that the INSERT statement only adds new data; it won't check for existing data, or delete and rebuild the table. To avoid getting multiple copies of the same data in a table, you can run this statement first: `TRUNCATE TABLE iris_data`. The T-SQL [TRUNCATE TABLE](../../t-sql/statements/truncate-table-transact-sql.md) statement deletes existing data but keeps the structure of the table intact.

## Query the data

As a validation step, run a query to confirm the data was uploaded.

1. In Object Explorer, under Databases, right-click the **irissql** database, and start a new query.

2. Run some simple queries:

    ```sql
    SELECT TOP(10) * FROM iris_data;
    SELECT COUNT(*) FROM iris_data;
    ```

## Next steps

In the following quickstart, you will create a machine learning model and save it to a table, and then use the model to generate predicted outcomes.

+ [Quickstart:Create and score a predictive model in Python](quickstart-python-train-score-model.md)