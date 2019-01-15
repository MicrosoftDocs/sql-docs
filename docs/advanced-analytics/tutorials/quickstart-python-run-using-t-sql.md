---
title: Quickstart for a "Hello World" basic Python code execution in T-SQL - SQL Server Machine Learning
description: Quickstart for Python script in SQL Server. Learn the basics of calling Python script using the sp_execute_external_script system stored procedure in a hello-world exercise.
ms.prod: sql
ms.technology: machine-learning

ms.date: 01/11/2019
ms.topic: quickstart
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Quickstart: "Hello world" Python script in SQL Server 
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In this quickstart, you learn key concepts by running a "Hello World" Python script inT-SQL, with an introduction to the **sp_execute_external_script** system stored procedure. 

## Prerequisites

A previous quickstart, [Verify Python exists in SQL Server](quickstart-python-verify.md), provides information and links for setting up the Python environment required for this quickstart.

## Basic Python interaction

There are two ways to run Python code in SQL Server:

+ Add a Python script as an argument of the system stored procedure, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

+ From a [remote Python client](../python/setup-python-client-tools-sql.md), connect to SQL Server, and execute code using the SQL Server as the compute context. This requires [revoscalepy](../python/ref-py-revoscalepy.md).

The following exercise is focused on the first interaction model: how to pass Python code to a stored procedure.

1. Run some simple code to see how data is passed back and forth between SQL Server and Python.

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

2. Assuming that you have everything set up correctly, and Python and SQL Server are talking to each other, the correct result is calculated, and the Python `print` function returns the result to the **Messages** windows.

    **Results**

    ```text
    STDOUT message(s) from external script: 
    0.5 2
    ```

    While getting **stdout** messages is handy when testing your code, more often you need to return the results in tabular format, so that you can use it in an application or write it to a table.

For now, remember these rules:

+ Everything inside the `@script` argument must be valid Python code. 
+ The code must follow all Python rules regarding indentation, variable names, and so forth. When you get an error, check your white space and casing.
+ Among programming languages, Python is one of the most flexible with regard to single quotes vs. double quotation marks; they're pretty much interchangeable. However, T-SQL uses single quotes for only certain things, and the `@script` argument uses single quotes to enclose the Python code as a Unicode string. Therefore, you might need to review your Python code and change some single quotes to double quotes.
+ If you are using any libraries that are not loaded by default, you must use an import statement at the beginning of your script to load them. SQL Server adds several product-specific libraries. For more information, see [Python libraries](../python/python-libraries-and-data-types.md).
+ If the library is not already installed, stop and install the Python package outside of SQL Server, as described here: [Install new Python packages on SQL Server](../python/install-additional-python-packages-on-sql-server.md)

## Run a Hello World script

The following exercise runs another simple Python scripts.

```sql
EXEC sp_execute_external_script
@language =N'Python',
@script=N'OutputDataSet = InputDataSet',
@input_data_1 =N'SELECT 1 AS hello'
WITH RESULT SETS (([Hello World] int));
GO
```

Inputs to this stored procedure include:

+ *@language* parameter defines the language extension to call, in this case, Python.
+ *@script* parameter defines the commands passed to the Python runtime. Your entire Python script must be enclosed in this argument, as Unicode text. You could also add the text to a variable of type **nvarchar** and then call the variable.
+ *@input_data_1* is data returned by the query, passed to the Python runtime, which returns the data to SQL Server as a data frame.
+ WITH RESULT SETS clause defines the schema of the returned data table for SQL Server, adding "Hello World" as the column name, **int** for the data type.

**Results**

| Hello World |
|-------------|
| 1 |

## Next steps

Now that you have run a couple of simple Python scripts, take a closer look at structuring inputs and outputs.

> [!div class="nextstepaction"]
> [Quickstart: Handle inputs and outputs](quickstart-python-run-using-t-sql.md)
