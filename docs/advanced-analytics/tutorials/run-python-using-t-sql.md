---
title: "Run Python using T-SQL | Microsoft Docs"
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
caps.latest.revision: 2
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
---
# Run Python using T-SQL
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This tutorial explains how you can run Python code in SQL Server 2017. It walks you through the process of moving data between SQL Server and Python, and explains how to wrap well-formed Python code in a stored procedure [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) to build, train, and use machine learning models in SQL Server.

## Prerequisites

To complete this tutorial, you must first install SQL Server 2017 and enable Machine Learning Services on the instance, as described in [this article](../python/setup-python-machine-learning-services.md). 

You should also install [SQL Server Management Studio](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms). Alternatively, you can use another database management or query tool, as long as it can connect to a server and database, and run a T-SQL query or stored procedure.

After you have completed setup, return to this tutorial, to learn how to execute Python code in the context of a stored procedure. 

## Overview

This tutorial includes four lessons:

+ The basics of moving data between SQL Server and Python: learn the basic requirements, data structures, inputs, and outputs.
+ Practice using stored procedures for simple Python tasks, like loading sample data.
+ Use stored procedures to create a Python machine learning model, and generate scores from the model.
+ An optional lesson for users who intend to run Python from a remote client, using SQL Server as the _compute context_. Includes code for building a model; however, requires that you are already somewhat familiar with Python environments and Python tools.

Additional Python samples specific to SQL Server 2017 are provided here: [SQL Server Python tutorials](../tutorials/sql-server-python-tutorials.md)

## Verify that Python is enabled and the Launchpad is running

1. In Management Studio, run this statement to make sure the service has been enabled.

    ```sql
    sp_configure 'external scripts enabled'
    ```

    If **run_value** is 1, the machine learning feature is installed and ready to use.

    A common cause of errors is that the Launchpad, which manages communication between SQL Server and Python, has stopped. You can view the Launchpad status by using the Windows **Services** panel, or by opening SQL Server Configuration Manager. If the service has stopped, restart it.

2. Next, verify that the Python runtime is working and communicating with SQL Server. To do this, open a new **Query** window in SQL Server Management Studio, and connect to the instance where Python was installed.

    ```sql
    EXEC sp_execute_external_script @language = N'Python', 
    @script = N'print(3+4)'
    ```

    If all is well, you should see a result message like this one

    ```text
    STDOUT message(s) from external script: 
    7
    ```

    If you get errors, there are a variety of things you can do to ensure that the server and Python can communicate. For example, typically you must add the Windows user group `SQLRUserGroup` as a login on the instance, to ensure that Launchpad can provide communication between Python and SQL Server. (The same group is used for both R and Python code execution.) For more information, see [Enabled implied authentication](../r/add-sqlrusergroup-to-database.md).
    
    Additionally, you might need to enable network protocols that have been disabled, or open the firewall so that SQL Server can communicate with external clients. For more information, see [Troubleshooting setup](../common-issues-external-script-execution.md).

## Basic Python interaction

There are two ways to run Python code in SQL Server:

+ Add a Python script as an argument of the system stored procedure, **sp_execute_external_script**
+ From a remote Python client, connect to SQL Server, and execute code using the SQL Server as the compute context. This requires [revoscalepy](../python/what-is-revoscalepy.md).

The primary goal of this tutorial is to ensure that you can use Python in a stored procedure.

For now, assuming that you have everything set up correctly, and Python and SQL Server are talking to each other, run some simple code to see how data is passed back and forth between SQL Server and Python.

```sql
execute sp_execute_external_script 
@language = N'Python', 
@script = N'
a = 1
b = 2
c = a/b
d = a*b
print(c, d)
'
```

+ Everything inside the `@script` argument must be valid Python code. That means following all Pythonic rules regarding indentation, variable names, and so forth. When you get an error, check your white space and casing.
+ If you are using any libraries not loaded by default, you must use an import statement at the beginning of your script to load them. If the library is not installed, stop, and install the Python package outside of SQL Server, as described here: [Install new Python packages on SQL Server](../python/install-additional-python-packages-on-sql-server.md)

SQL Server passes the code to Python, and returns results and messages.


**Results**

```text
STDOUT message(s) from external script: 
0.5 2
```

Although the correct result was calculated, the Python `print` function just returns the result to the **Messages** windows. this can be handy when testing your code, but what you really want is to return your result in tabular format, so that you can use it in an application or write it to a table.

## Inputs and outputs

By default, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) accepts a single input dataset, which you might supply in the form of a valid SQL query. You can pass other types of input as a SQL variable: for example, you can pass a trained model as a variable, using a serialization function such as [pickle](https://docs.python.org/3.0/library/pickle.html) or [rx_serialize_model](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-serialize-model) to write the model in a binary format.

The stored procedure can return a single Python [pandas](http://pandas.pydata.org/pandas-docs/stable/index.html) data frame as output. However you can add other types of outputs as variables, in addition to this single data frame. For example, you might output a trained model as a variable and use T-SQL to immediately save that model to a table. You can also generate plots or scalars. 

For now, let's look at just the default input and output variables, `InputDataSet` and `OutputDataSet`. The following code does some math and outputs the results.

```sql
execute sp_execute_external_script 
@language = N'Python', 
@script = N'
a = 1
b = 2
c = a/b
print(c)
OutputDataSet = c
'
WITH RESULT SETS ((ResultValue float))
```

You should get an error, because the Python code generates a scalar, not a data frame.

**Results**

```text
 line 43, in transform
    raise TypeError('OutputDataSet should be of type pandas.DataFrame')
```

Now see what happens when you pass a tabular dataset to Python, using the default input variable `InputDataSet`. The stored procedure returns a data.frame automatically, without you having to do anything extra in your Python code.

```sql
EXECUTE sp_execute_external_script 
@language = N'Python', 
@script = N'
OutputDataSet = InputDataSet
',
@input_data_1 = N'SELECT 1 as Col1'
```

**Results**

| no columnname|
|------|
| 1|

A couple of notes on this stored procedure:

+ The variable for the input dataset has the default name, `@input_data_1`, but you can change that name by adding a line like his: `@input_data_1_name = N'myResultName'`. 
+ Column names used by Python are never preserved in the output. So even if your query specified the column name `Col1`, that name is not returned, nor would any column headings used by your Python script. To specify a column name and data type when you return the data to SQL Server, use the T-SQL `WITH RESULT SETS` clause.

Here's an example that names a new input and output variable and provides a schema for the output.

```sql
execute sp_execute_external_script 
@language = N'Python', 
@script = N'
MyOutput = MyInput
',
@input_data_1_name = N'MyInput',
@input_data_1 = N'SELECT 1 as Col1',
@output_data_1_name = N'MyOutput'
WITH RESULT SETS ((ResultValue int))
```

**Results**

| ResultValue|
|------|
| 1|

Finally, just for fun, go back and change the line `@input_data_1_name = N'MyInput'` to `@input_data_1_name = N'myinput'`.

This change results in an error from Python, passed on to you by the satellite service used by SQL Server. The key message is in this line:

```text
MyOutput = MyInput
NameError: name 'MyInput' is not defined
```

Recall that Python, like R, is case-sensitive. Therefore, when you get any kind of error, be sure to check your variable names, and look for issues with spacing, indentation, and data types.

## Python data structures

SQL Server relies on the Python **pandas** package, which is great for working with tabular data. However, you've already seen that you cannot pass a scalar from Python to SQL Server and expect it to "just work".

In this section, we'll review some basic data type definitions, to prepare you for additional issues that you might run across when passing tabular data between Python and SQL Server.

+ A data frame is a table with _multiple_ columns.
+ A single column of a DataFrame, is a list-like object called a Series.
+ A single value is a cell of a data frame and has to be called by index.

So how would you expose the single result of a calculation as a data frame, if a data.frame requires a tabular structure? One answer is to represent the single scalar value as a series, which is easily converted to a data frame. 

### Convert scalar to series

This example does some simple math and converts the scalar into a series. A series requires an index, which you can assign manually, as shown here, or programmatically.

```sql
execute sp_execute_external_script 
@language = N'Python', 
@script = N'
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

To increase the length of the series, you can add new values, using an array. 

```sql
execute sp_execute_external_script 
@language = N'Python', 
@script = N'
a = 1
b = 2
c = a/b
d = a*b
s = pandas.Series([c,d])
print(s)
'
```

If you do not specify an index, **pandas** generates an index having values starting with 0 and ending with the length of the array.

**Results**

```text
STDOUT message(s) from external script: 
0    0.5
1    2.0
dtype: float64
```

If you increase the number of **index** values, but don't add new **data** values, the data values are repeated to fill the series.

```sql
execute sp_execute_external_script 
@language = N'Python', 
@script = N'
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

### Convert series to data frame

Having converted our scalar math results to a tabular structure, we still need to convert them to a format that SQL Server can handle. To convert a series to a data.frame, call the pandas [DataFrame](http://pandas.pydata.org/pandas-docs/stable/dsintro.html#dataframe) method.

```sql
execute sp_execute_external_script 
@language = N'Python', 
@script = N'
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
WITH RESULT SETS (( ResultValue float ))
```

***Results**

|ResultValue|
|------|
|0.5|
|2|

Note that the index values aren't output, even if you use the index to get specific values from the data.frame.

### Output values into data.frame using an index

Let's see how this works with our two series containing the results of simple math operations: 

+ The first has an index of sequential values generated by Python
+ The second uses an arbitrary index of string values.

This example gets a value from the series that uses an integer index.

```sql
EXECUTE sp_execute_external_script 
@language = N'Python', 
@script = N'
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
WITH RESULT SETS (( ResultValue float ))
```

Remember that the auto-generated index starts at 0. Try using an out of range index value and see what happens.

Now let's get a single value from the other data frame that has a string index. If you try to reference a numeric index on this series, you get an error.

```sql
EXECUTE sp_execute_external_script 
@language = N'Python', 
@script = N'
import pandas as pd
a = 1
b = 2
c = a/b
s = pandas.Series(c, index =["simple math example 1", "simple math example 2"])
print(s)
df = pd.DataFrame(s, index=["simple math example 1"])
OutputDataSet = df
'
WITH RESULT SETS (( ResultValue float ))
```

**Results**

|ResultValue|
|------|
|0.5|

From this exercise, you can see that outputting a single value as a data frame might be more trouble than its worth. Fortunately, you can easily pass all kinds of values in and out of the stored procedure as variables. That's covered in the next lesson. 

## Tips

+ Among programming languages, Python is one of the most flexible with regard to single quotes vs. double quotation marks; they're pretty much interchangeable. 

    However, T-SQL uses single quotes for only certain things, and the `@script` argument uses single quotes to enclose the Python code as a Unicode string. Therefore, you might need to review your Python code and change some single quotes to double quotes.

+ Can't find the stored procedure, `sp_execute_external_script`? It means you probably haven't finished configuring the instance to support external script execution. After running SQL Server 2017 setup and selecting Python as the machine learning language, you must also explicitly enable the feature using `sp_configure`, and then restart the instance. 

    For details, see [Setup Machine Learning Services with Python](../python/setup-python-machine-learning-services.md).

## Next steps

[Wrap Python code in a SQL stored procedure](wrap-python-in-tsql-stored-procedure.md)