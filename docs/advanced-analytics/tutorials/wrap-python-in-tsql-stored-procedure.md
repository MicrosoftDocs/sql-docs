---
title: Wrap Python code in a stored procedure | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/15/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Wrap Python code in a stored procedure
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In this lesson, learn how to embed Python code in a stored procedure, get data from the Python sample datasets, and write that data to a SQL Server table.

The system stored procedure [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) provides the wrapper that passes SQL variables and SQL datasets into Python. It also handles the results output by Python and passes them to SQL Server in a format compatible with SQL data types.

Let's see how this works.

## Prepare the database and tables

Although it is possible to set up a remote client and run Python code using Visual Studio Code, Visual Studio, PyCharm, or other tools, to simplify the scenario, all code in this lesson should be run as part of a stored procedure.

1. Start SQL Server Management Studio, and open a new **Query** window.  

2. Create a new database for this project, and change the context of your **Query** window to use the new database.

    ```sql
    CREATE DATABASE sqlpy
    GO
    USE sqlpy
    GO
    ```

    > [!TIP] 
    > If you're new to SQL Server, or are working on a server you own, a common mistake is to log in and start working without noticing that you are in the **master** database. To be sure that you are using the correct database, always specify the context using the `USE <database name>` statement.

3. Add some empty tables: one to store the data, and one to store the models you train. Later you populate the tables using Python.

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

    If you are new to T-SQL, it pays to memorize the `DROP...IF` statement. When you try to  create a table and one already exists, SQL Server returns an error: "There is already an object named 'iris_data' in the database." One way to avoid such errors is to delete any existing tables or other objects as part of your code.

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

To move the training data from Python into a SQL Server table is a multistep process:

+ You design a stored procedure that gets the data you want.
+ You execute the stored procedure to actually get the data.
+ You use an INSERT statement to specify where the retrieved data should be saved.

1. Create the following stored procedure that includes Python code. 

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

2. To actually populate the table, run the stored procedure and specify the table where the data should be written. When run, the stored procedure executes the Python code, which loads the Iris dataset from the built-in Python sample data.

    ```sql
    INSERT INTO iris_data ("Sepal.Length", "Sepal.Width", "Petal.Length", "Petal.Width", "Species", "SpeciesId")
    EXEC dbo.get_iris_dataset;
    ```

    If you're new to T-SQL, be aware that the INSERT statement only adds new data; it won't check for existing data, or delete and rebuild the table. To avoid getting multiple copies of the same data in a table, you can run this statement first: `TRUNCATE TABLE iris_data`. The T-SQL [TRUNCATE TABLE](https://docs.microsoft.com/sql/t-sql/statements/truncate-table-transact-sql) statement deletes existing data but keeps the structure of the table intact.

    > [!TIP]
    > To modify the stored procedure later, you don't need to drop and recreate it. Use the [ALTER PROCEDURE](https://docs.microsoft.com/sql/t-sql/statements/alter-procedure-transact-sql) statement. 

3. To verify that the data was loaded correctly, you can run some simple queries:

    ```sql
    SELECT TOP(10) * FROM iris_data;
    SELECT COUNT(*) FROM iris_data;
    ```

In the next lesson, you create a machine learning model and save it to a table.

## Next lesson

[Train a Python model and generate scores in SQL Server](../tutorials/train-score-using-python-in-tsql.md)
