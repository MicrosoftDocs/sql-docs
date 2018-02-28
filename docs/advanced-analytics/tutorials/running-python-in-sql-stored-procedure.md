---
title: "Running Python in a stored procedure | Microsoft Docs"
titleSuffix: "SQL Server"
ms.custom: ""
ms.date: "02/28/2018"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: 
ms.technology: 
ms.tgt_pltfrm: ""
ms.topic: "tutorial"
applies_to: 
  - "SQL Server 2017"
dev_langs: 
  - "Python"
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
---
# Running Python in a stored procedure
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In a [previous lesson](run-python-using-t-sql.md), you learned how to make Python talk to SQL Server. iIn this lesson, you learn how to use a stored procedure to get data using Python code, and write that data to a SQL Server table.

The system stored procedure [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) provides the wrapper that passes SQL variables and SQL datasets into Python. It also handles the results output by Python and passes them to SQL Server in a format compatible with SQL data types.

Let's see how this works.

## Prepare the database and tables

Open a new **Query** window in SQL Server Management Studio.  

Although it is possible to set up a remote client and run Python code using Visual Studio Code, Visual Studio, PyCharm, or other tools, to simplify the scenario, all code here should be run as part of a stored procedure.

Begin by creating a new database for this project, and change the context of your **Query** window to use the new database.

```sql
CREATE DATABASE sqlpy;
GO;
USE sqlpy;
GO;
```

> [!TIP] 
> If you're new to SQL Server, or are working on a server you own, a common mistake is tolog in and start working without noticing that you are in the **master** database. To be sure that you are using the correct database, always specify the context using the `USE <database name>` statement.

Add some empty tables: one to store the data, and one to store the models you train. Later you populate the tables using Python.

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

 > [!TIP]
 > If you are new to T-SQL, it pays to memorize the `DROP...IF` statement. When you try to  create a table and one already exists, SQL Server returns an error: "There is already an object named 'iris_data' in the database." One way to avoid such errors is to delete any existing tables or other objects as part of your code.

The following code creates the table used for storing the trained model. 

```sql
DROP TABLE IF EXISTS iris_models;
GO

CREATE TABLE iris_models (
  model_name VARCHAR(50) NOT NULL DEFAULT('default model') PRIMARY KEY,
  model VARBINARY(MAX) NOT NULL
);
GO
```

To save Python (or R) models in SQL Server, they must be serialized and stored in a column of type **varbinary(max)**. In addition to the model contents, typically, you would also add columns for other useful metadata, such as the model's name, the date it was trained, the source algorithm and parameters, source data, and so forth. 

## Populate the table

To move the training data from Python into a SQL Server table:

+ You design a stored procedure that gets the data you want.
+ You execute the stored procedure to actually get the data.
+ You use an INSERT statement to specify where the retrieved data should be saved.

Here is a stored procedure that reads the famous Iris dataset from the built-in Python data sets.

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

To actually populate the table, run the stored procedure and specify the table where the data should be written.

```sql
INSERT INTO iris_data ("Sepal.Length", "Sepal.Width", "Petal.Length", "Petal.Width", "Species", "SpeciesId")
EXEC dbo.get_iris_dataset;
```

+ The T-SQL INSERT statement only adds new data; it doesn't check for existing data, or delete and rebuild the table. So to avoid getting many copies of the same data in a table, you can run this statement first: `TRUNCATE TABLE iris_data`. The T-SQL [TRUNCATE TABLE](https://docs.microsoft.com/sql/t-sql/statements/truncate-table-transact-sql) statement deletes existing data but keeps the structure of the table intact.

+ To modify the stored procedure later, you don't need to drop and recreate it. Instead, use the [ALTER PROCEDURE](https://docs.microsoft.com/sql/t-sql/statements/alter-procedure-transact-sql) statement. 

To verify that the data was loaded correctly, you can run some simple queries:

```sql
SELECT TOP(10) * FROM iris_data;
SELECT COUNT(*) FROM iris_data;
```

In the next lesson, you create a machine learning model and save it to a table.

### Further reading about stored procedures

If you are new to SQL Server, you might find stored procedures complicated at first. But a stored procedure is a powerful and flexible interface for passing data between applications and the server. By using a stored procedure, you can dynamically define inputs, which makes it easy to pass in new model names, new parameters, and new data, without altering your Python code.

For an overview of how stored procedures work, see [Stored Procedures (Database Engine)](https://docs.microsoft.com/sql/relational-databases/stored-procedures/stored-procedures-database-engine), or this tutorial: [Writing Transact-SQL Statements](https://docs.microsoft.com/sql/t-sql/tutorial-writing-transact-sql-statements).

There are also some good tutorials at community sites such as [SQL Server Central](http://www.sqlservercentral.com/) or [SQL Team](http://www.sqlteam.com/).

As you think about how you can best encapsulate Python code in T-SQL, also consider using these features:

+ Defining default values for the stored procedure
+ Using the OUTPUT keyword to pass through input variables
+ Creating schema definitions using WITH RESULTS to ensure that data consumed by applications has the right data types and column names
+ Providing hints to improve batch processing
+ Impersonating a different user to test your code, using the EXECUTE AS clause

## Next steps

[Training and scoring from a Python model in SQL Server](../tutorials/train-score-using-python-in-tsql.md)