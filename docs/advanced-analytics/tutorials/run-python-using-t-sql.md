---
title: Run Python using T-SQL on SQL Server | Microsoft Docs
description: Learn the basics for running Python code using T-SQL and stored procedures on a SQL Server database engine instance for which Python integration is enabled.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/15/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Run Python using T-SQL
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article explains how you can run Python code in SQL Server 2017. It walks you through the basics of moving data between SQL Server and Python: requirements, data structures, inputs, and outputs. It also explains how to wrap well-formed Python code in a stored procedure [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) to build, train, and use machine learning models in SQL Server.

## Prerequisites

To run the example code in these exercises, you must first install SQL Server 2017 and enable Machine Learning Services on the instance, as described in [Install SQL Server 2017 Machine Learning Services (In-Database)](../install/sql-machine-learning-services-windows-install.md). 

You should also install [SQL Server Management Studio](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms). Alternatively, you can use another database management or query tool, as long as it can connect to a server and database, and run a T-SQL query or stored procedure.

When your environment is ready, return to this page to learn how to execute Python code in the context of a stored procedure. 

## Verify Python exists

The following steps confirm that Python is enabled and the SQL Server Launchpad service is running.

1. Check whether Python integration is installed on the database engine instance. You should find **python.exe** in a folder called **PYTHON_SERVICES** at C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\. This is the Python executable that SQL Server uses to run Python code.

2. Check whether external scripting is enabled. In Management Studio, run the following statement:

    ```sql
    sp_configure 'external scripts enabled'
    ```

    If **run_value** is 1, the machine learning feature is installed and ready to use.

    A common cause of errors is that the [SQL Server Launchpad service](../concepts/extensibility-framework.md#launchpad), which manages communication between SQL Server and Python, has stopped. You can view the Launchpad status by using the Windows **Services** panel, or by opening SQL Server Configuration Manager. If the service has stopped, restart it.

3. Verify that the Python runtime is working and communicating with SQL Server. To do this, open a new **Query** window in SQL Server Management Studio, and connect to the instance where Python was installed.

    ```sql
    EXEC sp_execute_external_script @language = N'Python', 
    @script = N'print(3+4)'
    ```

    If all is well, you should see a result message like this one

    ```text
    STDOUT message(s) from external script: 
    7
    ```

3. If you get errors, there are a variety of things you can do to ensure that the server and Python can communicate. 

    You must add the Windows user group `SQLRUserGroup` as a login on the instance, to ensure that Launchpad can provide communication between Python and SQL Server. (The same group is used for both R and Python code execution.) For more information, see [Enabled implied authentication](../security/add-sqlrusergroup-to-database.md).
    
    Additionally, you might need to enable network protocols that have been disabled, or open the firewall so that SQL Server can communicate with external clients. For more information, see [Troubleshooting setup](../common-issues-external-script-execution.md).

### Call revoscalepy functions

To verify that **revoscalepy** is available, run a script that includes [rx_summary](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-summary) that produces a statistical summary data. This script demonstrates how to retrieve a sample .xdf data file from built-in samples included in revoscalepy. The RxOptions function provides the **sampleDataDir** parameter that returns the location of the sample files.

Because rx_summary returns an object of type `class revoscalepy.functions.RxSummary.RxSummaryResults`, which contains multiple elements, you can use pandas to extract just the data frame in a tabular format.

```SQL
EXEC sp_execute_external_script @language = N'Python', 
@script = N'
import os
from pandas import DataFrame
from revoscalepy import rx_summary
from revoscalepy import RxXdfData
from revoscalepy import RxOptions

sample_data_path = RxOptions.get_option("sampleDataDir")
print(sample_data_path)

ds = RxXdfData(os.path.join(sample_data_path, "AirlineDemoSmall.xdf"))
summary = rx_summary("ArrDelay + DayOfWeek", ds)
print(summary)
dfsummary = summary.summary_data_frame
OutputDataSet = dfsummary
'
WITH RESULT SETS  ((ColName nvarchar(25) , ColMean float, ColStdDev  float, ColMin  float,   ColMax  float, Col_ValidObs  float, Col_MissingObs int))
```

## Basic Python interaction

There are two ways to run Python code in SQL Server:

+ Add a Python script as an argument of the system stored procedure, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

+ From a [remote Python client](../python/setup-python-client-tools-sql.md), connect to SQL Server, and execute code using the SQL Server as the compute context. This requires [revoscalepy](../python/what-is-revoscalepy.md).

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
+ The code must follow all Pythonic rules regarding indentation, variable names, and so forth. When you get an error, check your white space and casing.
+ If you are using any libraries that are not loaded by default, you must use an import statement at the beginning of your script to load them. SQL Server adds several product-specific libraries. For more information, see [Python libraries](../python/python-libraries-and-data-types.md).
+ If the library is not already installed, stop and install the Python package outside of SQL Server, as described here: [Install new Python packages on SQL Server](../python/install-additional-python-packages-on-sql-server.md)

## Inputs and outputs

By default, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) accepts a single input dataset, which typically you supply in the form of a valid SQL query. 

Other types of input can be passed as SQL variables: for example, you can pass a trained model as a variable, using a serialization function such as [pickle](https://docs.python.org/3.0/library/pickle.html) or [rx_serialize_model](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-serialize-model) to write the model in a binary format.

The stored procedure returns a single Python [pandas](http://pandas.pydata.org/pandas-docs/stable/index.html) data frame as output, but you can also output scalars and models as variables. For example, you can output a trained model as a binary variable and pass that to a T-SQL INSERT statement, to write that model to a table. You can also generate plots (in binary format) or scalars (individual values, such as the date and time, the time elapsed to train the model, and so forth).

For now, let's look at just the default input and output variables of sp_execute_external_script: `InputDataSet` and `OutputDataSet`. 

1. Run the following code to do some math and output the results.

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

2. You should get an error, because the Python code generates a scalar, not a data frame.

    **Results**

    ```text
    line 43, in transform
        raise TypeError('OutputDataSet should be of type pandas.DataFrame')
    ```

3. Now see what happens when you pass a tabular dataset to Python, using the default input variable `InputDataSet`. 

    ```sql
    EXECUTE sp_execute_external_script 
    @language = N'Python', 
    @script = N'
    OutputDataSet = InputDataSet
    ',
    @input_data_1 = N'SELECT 1 as Col1'
    ```

    The stored procedure returns a data.frame automatically, without you having to do anything extra in your Python code.

    **Results**

    | no columnname|
    |------|
    | 1|

    By default, the single tabular input dataset has the name, `InputDataSet`. However, you can change that name by adding a line like this: `@input_data_1_name = N'myResultName'`.

    Column names used by Python are never preserved in the output. Although the input query specified the column name `Col1`, that name is not returned, nor would any column headings used by your Python script. To specify a column name and data type when you return the data to SQL Server, use the T-SQL `WITH RESULT SETS` clause.

4. This example provides new names for the input and output variables.

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

    The WITH RESULT SET clause defines the schema for the output, since Python column names are never returned with the data.frame.

    **Results**

    | ResultValue|
    |------|
    | 1|

5. Now let's look at a typical Python error. Change the line in the previous example from `@input_data_1_name = N'MyInput'` to `@input_data_1_name = N'myinput'`.

    Python errors are passed to you as messages, by the satellite service used by SQL Server. Messages can be long and include SQL Server errors or Launchpad errors in addition to Python errors, so be patient in digging through the text. The key message is in this line:

    ```text
    MyOutput = MyInput
    NameError: name 'MyInput' is not defined
    ```

    Recall that Python, like R, is case-sensitive. Therefore, when you get any kind of error, be sure to check your variable names, and look for issues with spacing, indentation, and data types.

## Python data structures

SQL Server relies on the Python **pandas** package, which is great for working with tabular data. However, you've already seen that you cannot pass a scalar from Python to SQL Server and expect it to "just work". In this section, we'll review some basic data type definitions, to prepare you for additional issues that you might run across when passing tabular data between Python and SQL Server.

+ A data frame is a table with _multiple_ columns.
+ A single column of a DataFrame, is a list-like object called a Series.
+ A single value is a cell of a data frame and has to be called by index.

So how would you expose the single result of a calculation as a data frame, if a data.frame requires a tabular structure? One answer is to represent the single scalar value as a series, which is easily converted to a data frame. 

1. This example does some simple math and converts a scalar into a series. A series requires an index, which you can assign manually, as shown here, or programmatically.

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

2. Because the series hasn't been converted to a data.frame, the values are returned in the Messages window, but you can see that the results are in a more tabular format.

    **Results**

    ```text
    STDOUT message(s) from external script: 
    0.5
    simple math example 1    0.5
    dtype: float64
    ```

3. To increase the length of the series, you can add new values, using an array. 

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

    If you do not specify an index, an index is generated that has values starting with 0 and ending with the length of the array.

    **Results**

    ```text
    STDOUT message(s) from external script: 
    0    0.5
    1    2.0
    dtype: float64
    ```

4. If you increase the number of **index** values, but don't add new **data** values, the data values are repeated to fill the series.

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

Having converted our scalar math results to a tabular structure, we still need to convert them to a format that SQL Server can handle. 

1. To convert a series to a data.frame, call the pandas [DataFrame](http://pandas.pydata.org/pandas-docs/stable/dsintro.html#dataframe) method.

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

2. Note that the index values aren't output, even if you use the index to get specific values from the data.frame.

    **Results**

    |ResultValue|
    |------|
    |0.5|
    |2|

### Output values into data.frame using an index

Let's see how conversion to a data.frame works with our two series containing the results of simple math operations. The first has an index of sequential values generated by Python. The second uses an arbitrary index of string values.

1. This example gets a value from the series that uses an integer index.

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

2. Now let's get a single value from the other data frame that has a string index. 

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

    If you try to use a numeric index to get a value from this series, you get an error.

This exercise was intended to give you an idea of how to work with different Python data structures and ensure you get the right result as a data frame. You might have concluded that outputting a single value as a data frame is more trouble than its worth! Fortunately, you can easily pass all kinds of values in and out of the stored procedure as variables. That's covered in the next lesson.

## Tips

+ Among programming languages, Python is one of the most flexible with regard to single quotes vs. double quotation marks; they're pretty much interchangeable. 

    However, T-SQL uses single quotes for only certain things, and the `@script` argument uses single quotes to enclose the Python code as a Unicode string. Therefore, you might need to review your Python code and change some single quotes to double quotes.

+ Can't find the stored procedure, `sp_execute_external_script`? It means you probably haven't finished configuring the instance to support external script execution. After running SQL Server 2017 setup and selecting Python as the machine learning language, you must also explicitly enable the feature using `sp_configure`, and then restart the instance. 

    For details, see [Install SQL Server 2017 Machine Learning Services (In-Database)](../install/sql-machine-learning-services-windows-install.md).

## Next steps

[Set up the Iris demo dataset](demo-data-iris-in-sql.md)