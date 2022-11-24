---
title: "Quickstart: Python data structures"
titleSuffix: SQL machine learning
description: In this quickstart, learn how to work with data structures and data objects in Python using SQL machine learning.
ms.service: sql
ms.subservice: machine-learning
ms.date: 09/28/2020
ms.topic: quickstart
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom:
  - seo-lt-2019
  - intro-quickstart
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Quickstart: Data structures and objects using Python with SQL machine learning
[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

In this quickstart, you'll learn how to use data structures and data types when using Python in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md), [Azure SQL Managed Instance Machine Learning Services](/azure/azure-sql/managed-instance/machine-learning-services-overview), or on [SQL Server Big Data Clusters](../../big-data-cluster/machine-learning-services.md). You'll learn about moving data between Python and SQL Server, and the common issues that might occur.

SQL machine learning relies on the Python **pandas** package, which is great for working with tabular data. However, you cannot pass a scalar from Python to your database and expect it to *just work*. In this quickstart, you'll review some basic data structure definitions, to prepare you for additional issues that you might run across when passing tabular data between Python and the database.

Concepts to know up front include:

- A data frame is a table with _multiple_ columns.
- A single column of a data frame is a list-like object called a series.
- A single value of a data frame is called a cell and is accessed by index.

How would you expose the single result of a calculation as a data frame, if a data.frame requires a tabular structure? One answer is to represent the single scalar value as a series, which is easily converted to a data frame.

> [!NOTE]
> When returning dates, Python in SQL uses DATETIME which has a restricted date range of 1753-01-01(-53690) through 9999-12-31(2958463).

## Prerequisites

You need the following prerequisites to run this quickstart.

- A SQL database on one of these platforms:
  - [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md). To install, see the [Windows installation guide](../install/sql-machine-learning-services-windows-install.md) or the [Linux installation guide](../../linux/sql-server-linux-setup-machine-learning.md?toc=%2Fsql%2Fmachine-learning%2Ftoc.json).
  - SQL Server Big Data Clusters. See how to [enable Machine Learning Services on SQL Server Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
  - Azure SQL Managed Instance Machine Learning Services. For information, see the [Azure SQL Managed Instance Machine Learning Services overview](/azure/azure-sql/managed-instance/machine-learning-services-overview).

- A tool for running SQL queries that contain Python scripts. This quickstart uses [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md).

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

Having converted the scalar math results to a tabular structure, you still need to convert them to a format that SQL machine learning can handle.

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

To learn about writing advanced Python functions with SQL machine learning, follow this quickstart:

> [!div class="nextstepaction"]
> [Write advanced Python functions](quickstart-python-functions.md)
