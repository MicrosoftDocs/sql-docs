---
title: "Quickstart: Python data types"
description: In this quickstart, learn how to work with data types and data objects in Python and SQL Server with SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/04/2019  
ms.topic: quickstart
author: garyericson
ms.author: garye
ms.reviewer: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Quickstart: Handle data types and objects using Python in SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This quickstart shows how to use data structures when using Python in SQL Server Machine Learning Services.

SQL Server relies on the Python **pandas** package, which is great for working with tabular data. However, you cannot pass a scalar from Python to SQL Server and expect it to "just work". In this quickstart, you'll review some basic data type definitions, to prepare you for additional issues that you might run across when passing tabular data between Python and SQL Server.

Concepts to know up front include:

- A data frame is a table with _multiple_ columns.
- A single column of a data frame is a list-like object called a series.
- A single value of a data frame is called a cell and is accessed by index.

How would you expose the single result of a calculation as a data frame, if a data.frame requires a tabular structure? One answer is to represent the single scalar value as a series, which is easily converted to a data frame. 

> [!NOTE]
> When returning dates, Python in SQL uses DATETIME which has a restricted date range of 1753-01-01(-53690) through 9999-12-31(2958463). 

## Prerequisites

- This quickstart requires access to an instance of SQL Server with [SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) with the Python language installed.

- You also need a tool for running SQL queries that contain Python scripts. You can run these scripts using any database management or query tool, as long as it can connect to a SQL Server instance, and run a T-SQL query or stored procedure. This quickstart uses [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/sql-server-management-studio-ssms).

## Scalar value as a series

This example does some simple math and converts a scalar into a series.

1. A series requires an index, which you can assign manually, as shown here, or programmatically.

   ```sql
   EXECUTE sp_execute_external_script @language = N'Python'
       , @script = N'
   a = 1
   b = 2
   c = a/b
   print(c)
   s = pandas.Series(c, index =["simple math example 1"])
   print(s)
   '
   ```

   Because the series hasn't been converted to a data.frame, the values are returned in the Messages window, but you can see that the results are in a more tabular format.

   **Results**

   ```text
   STDOUT message(s) from external script: 
   0.5
   simple math example 1    0.5
   dtype: float64
   ```

1. To increase the length of the series, you can add new values, using an array. 

   ```sql
   EXECUTE sp_execute_external_script @language = N'Python'
       , @script = N'
   a = 1
   b = 2
   c = a/b
   d = a*b
   s = pandas.Series([c,d])
   print(s)
   '
   ```

   If you do not specify an index, an index is generated that has values starting with 0 and ending with the length of the array.

   **Results**

   ```text
   STDOUT message(s) from external script: 
   0    0.5
   1    2.0
   dtype: float64
   ```

1. If you increase the number of **index** values, but don't add new **data** values, the data values are repeated to fill the series.

   ```sql
   EXECUTE sp_execute_external_script @language = N'Python'
       , @script = N'
   a = 1
   b = 2
   c = a/b
   s = pandas.Series(c, index =["simple math example 1", "simple math example 2"])
   print(s)
   '
   ```

   **Results**

   ```text
   STDOUT message(s) from external script: 
   0.5
   simple math example 1    0.5
   simple math example 2    0.5
   dtype: float64
   ```

## Convert series to data frame

Having converted the scalar math results to a tabular structure, you still need to convert them to a format that SQL Server can handle.

1. To convert a series to a data.frame, call the pandas [DataFrame](https://pandas.pydata.org/pandas-docs/stable/dsintro.html#dataframe) method.

   ```sql
   EXECUTE sp_execute_external_script @language = N'Python'
       , @script = N'
   import pandas as pd
   a = 1
   b = 2
   c = a/b
   d = a*b
   s = pandas.Series([c,d])
   print(s)
   df = pd.DataFrame(s)
   OutputDataSet = df
   '
   WITH RESULT SETS((ResultValue FLOAT))
   ```

   The result is shown below. Even if you use the index to get specific values from the data.frame, the index values aren't part of the output.

   **Results**

   |ResultValue|
   |------|
   |0.5|
   |2|

## Output values into data.frame

Now you'll output specific values from two series of math results in a data.frame. The first has an index of sequential values generated by Python. The second uses an arbitrary index of string values.

1. The following example gets a value from the series using an integer index.

   ```sql
   EXECUTE sp_execute_external_script @language = N'Python'
       , @script = N'
   import pandas as pd
   a = 1
   b = 2
   c = a/b
   d = a*b
   s = pandas.Series([c,d])
   print(s)
   df = pd.DataFrame(s, index=[1])
   OutputDataSet = df
   '
   WITH RESULT SETS((ResultValue FLOAT))
   ```

   **Results**

   |ResultValue|
   |------|
   |2.0|

   Remember that the auto-generated index starts at 0. Try using an out of range index value and see what happens.

1. Now get a single value from the other data frame using a string index.

   ```sql
   EXECUTE sp_execute_external_script @language = N'Python'
       , @script = N'
   import pandas as pd
   a = 1
   b = 2
   c = a/b
   s = pandas.Series(c, index =["simple math example 1", "simple math example 2"])
   print(s)
   df = pd.DataFrame(s, index=["simple math example 1"])
   OutputDataSet = df
   '
   WITH RESULT SETS((ResultValue FLOAT))
   ```

   **Results**

   |ResultValue|
   |------|
   |0.5|

   If you try to use a numeric index to get a value from this series, you get an error.

## Next steps

To learn about writing advanced Python functions in SQL Server, follow this quickstart:

> [!div class="nextstepaction"]
> [Write advanced Python functions with SQL Server Machine Learning Services](quickstart-python-functions.md)

For more information on using Python in SQL Server Machine Learning Services, see the following articles:

- [Create and score a predictive model in Python](quickstart-python-train-score-model.md)
- [What is SQL Server Machine Learning Services (Python and R)?](../what-is-sql-server-machine-learning.md)
