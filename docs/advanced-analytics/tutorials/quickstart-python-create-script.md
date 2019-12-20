---
title: "Quickstart: Create Python scripts"
description: Create and run simple Python scripts in a SQL Server instance with SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/18/2019  
ms.topic: quickstart
author: garyericson
ms.author: garye
ms.reviewer: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---

# Quickstart: Create and run simple Python scripts with SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

In this quickstart, you'll create and run a set of simple Python scripts using [SQL Server Machine Learning Services](../what-is-sql-server-machine-learning.md). You'll learn how to wrap a well-formed Python script in the stored procedure [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) and execute the script in a SQL Server instance.

## Prerequisites

- This quickstart requires access to an instance of SQL Server with [SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) with the Python language installed.

- You also need a tool for running SQL queries that contain Python scripts. You can run these scripts using any database management or query tool, as long as it can connect to a SQL Server instance, and run a T-SQL query or stored procedure. This quickstart uses [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/sql-server-management-studio-ssms).

## Run a simple script

To run a Python script, you'll pass it as an argument to the system stored procedure, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).
This system stored procedure starts the Python runtime in the context of SQL Server, passes data to Python, manages Python user sessions securely, and returns any results to the client.

In the following steps, you'll run this example Python script in your SQL Server instance:

```python
a = 1
b = 2
c = a/b
d = a*b
print(c, d)
```

1. Open a new query window in **SQL Server Management Studio** connected to your SQL Server instance.

1. Pass the complete Python script to the `sp_execute_external_script` stored procedure.

   The script is passed through the `@script` argument. Everything inside the `@script` argument must be valid Python code.

    ```sql
    EXECUTE sp_execute_external_script @language = N'Python'
        , @script = N'
    a = 1
    b = 2
    c = a/b
    d = a*b
    print(c, d)
    '
    ```

1. The correct result is calculated and the Python `print` function returns the result to the **Messages** window.

   It should look something like this.

    **Results**

    ```text
    STDOUT message(s) from external script:
    0.5 2
    ```

## Run a Hello World script

A typical example script is one that just outputs the string "Hello World". Run the following command.

```sql
EXECUTE sp_execute_external_script @language = N'Python'
    , @script = N'OutputDataSet = InputDataSet'
    , @input_data_1 = N'SELECT 1 AS hello'
WITH RESULT SETS(([Hello World] INT));
GO
```

Inputs to the `sp_execute_external_script` stored procedure include:

| | |
|-|-|
| @language | defines the language extension to call, in this case Python |
| @script | defines the commands passed to the Python runtime<br>Your entire Python script must be enclosed in this argument, as Unicode text. You could also add the text to a variable of type **nvarchar** and then call the variable |
| @input_data_1 | data returned by the query, passed to the Python runtime, which returns the data to SQL Server as a data frame |
|WITH RESULT SETS | clause defines the schema of the returned data table for SQL Server, in this case adding "Hello World" as the column name and **int** for the data type |

The command outputs the following text:

| Hello World |
|-------------|
| 1 |

## Use inputs and outputs

By default, `sp_execute_external_script` accepts a single dataset as input, which typically you supply in the form of a valid SQL query. It then returns a single Python data frame as output.

For now, let's use the default input and output variables of `sp_execute_external_script`: **InputDataSet** and **OutputDataSet**.

1. Create a small table of test data.

    ```sql
    CREATE TABLE PythonTestData (col1 INT NOT NULL)
    
    INSERT INTO PythonTestData
    VALUES (1);
    
    INSERT INTO PythonTestData
    VALUES (10);
    
    INSERT INTO PythonTestData
    VALUES (100);
    GO
    ```

1. Use the `SELECT` statement to query the table.
  
    ```sql
    SELECT *
    FROM PythonTestData
    ```

    **Results**

    ![Contents of the PythonTestData table](./media/select-pythontestdata.png)

1. Run the following Python script. It retrieves the data from the table using the `SELECT` statement, passes it through the Python runtime, and returns the data as a data frame. The `WITH RESULT SETS` clause defines the schema of the returned data table for SQL, adding the column name *NewColName*.

    ```sql
    EXECUTE sp_execute_external_script @language = N'Python'
        , @script = N'OutputDataSet = InputDataSet;'
        , @input_data_1 = N'SELECT * FROM PythonTestData;'
    WITH RESULT SETS(([NewColName] INT NOT NULL));
    ```

    **Results**

    ![Output from Python script that returns data from a table](./media/python-output-pythontestdata.png)

1. Now change the names of the input and output variables. The default input and output variable names are **InputDataSet** and **OutputDataSet**, the following script changes the names to **SQL_in** and **SQL_out**:

    ```sql
    EXECUTE sp_execute_external_script @language = N'Python'
        , @script = N'SQL_out = SQL_in;'
        , @input_data_1 = N'SELECT 12 as Col;'
        , @input_data_1_name  = N'SQL_in'
        , @output_data_1_name = N'SQL_out'
    WITH RESULT SETS(([NewColName] INT NOT NULL));
    ```

    Note that Python is case-sensitive. The input and output variables used in the Python script (**SQL_out**, **SQL_in**) need to match the names defined with `@input_data_1_name` and `@output_data_1_name`, including case.

   > [!TIP]
   > Only one input dataset can be passed as a parameter, and you can return only one dataset. However, you can call other datasets from inside your Python code and you can return outputs of other types in addition to the dataset. You can also add the OUTPUT keyword to any parameter to have it returned with the results.

1. You can also generate values just using the Python script with no input data (`@input_data_1` is set to blank).

   The following script outputs the text "hello" and "world".

   ```sql
   EXECUTE sp_execute_external_script @language = N'Python'
       , @script = N'
   import pandas as pd
   mytextvariable = pandas.Series(["hello", " ", "world"]);
   OutputDataSet = pd.DataFrame(mytextvariable);
   '
       , @input_data_1 = N''
   WITH RESULT SETS(([Col1] CHAR(20) NOT NULL));
   ```

   **Results**

   ![Query results using @script as input](./media/python-data-generated-output.png)

> [!NOTE]
> Python uses leading spaces to group statements. So when the imbedded Python script spans multiple lines, as in the preceding script, don't try to indent the Python commands to be in line with the SQL commands. For example, this script will produce an error:

  ```text
  EXECUTE sp_execute_external_script @language = N'Python'
      , @script = N'
      import pandas as pd
      mytextvariable = pandas.Series(["hello", " ", "world"]);
      OutputDataSet = pd.DataFrame(mytextvariable);
      '
      , @input_data_1 = N''
  WITH RESULT SETS(([Col1] CHAR(20) NOT NULL));
  ```

## Check Python version

If you would like to see which version of Python is installed in your SQL Server instance, run the following script.

```sql
EXECUTE sp_execute_external_script @language = N'Python'
    , @script = N'
import sys
print(sys.version)
'
GO
```

The Python `print` function returns the version to the **Messages** window. In the example output below, you can see that in this case, Python version 3.5.2 is installed.

**Results**

```text
STDOUT message(s) from external script: 
3.5.2 |Continuum Analytics, Inc.| (default, Jul  5 2016, 11:41:13) [MSC v.1900 64 bit (AMD64)]
```

## List Python packages

Microsoft provides a number of Python packages pre-installed with SQL Server Machine Learning Services in your SQL Server instance.

To see a list of which Python packages are installed, including version, run the following script.

```SQL
EXECUTE sp_execute_external_script @language = N'Python'
    , @script = N'
import pkg_resources
import pandas
dists = [str(d) for d in pkg_resources.working_set]
OutputDataSet = pandas.DataFrame(dists)
'
WITH RESULT SETS(([Package] NVARCHAR(max)))
GO
```

The list is from `pkg_resources.working_set` in Python and returned to SQL as a data frame.

**Results**

:::image type="content" source="media/python-package-list.png" alt-text="List of installed Python packages":::

## Next steps

To learn how to use data structures when using Python in SQL Server Machine Learning Services, follow this quickstart:

> [!div class="nextstepaction"]
> [Handle data types and objects using Python in SQL Server Machine Learning Services](quickstart-python-data-structures.md)

For more information on using Python in SQL Server Machine Learning Services, see the following articles:

- [Write advanced Python functions with SQL Server Machine Learning Services](quickstart-python-functions.md)
- [Create and score a predictive model in Python with SQL Server Machine Learning Services](quickstart-python-train-score-model.md)
- [What is SQL Server Machine Learning Services (Python and R)?](../what-is-sql-server-machine-learning.md)
