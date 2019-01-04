---
title: Quickstart for working with inputs and outputs in Python - SQL Server Machine Learning
description: In this quickstart for Python script in SQL Server, learn how to structure inputs and outputs to the sp_execute_external_script system stored procedure.
ms.prod: sql
ms.technology: machine-learning

ms.date: 01/04/2019  
ms.topic: quickstart
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Quickstart: Handle inputs and outputs using Python in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This quickstart shows how to handle inputs and outputs when using Python in SQL Server Machine Learning Services.

By default, [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) accepts a single input dataset, which typically you supply in the form of a valid SQL query.

Other types of input can be passed as SQL variables: for example, you can pass a trained model as a variable, using a serialization function such as [pickle](https://docs.python.org/3.0/library/pickle.html) or [rx_serialize_model](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-serialize-model) to write the model in a binary format.

The stored procedure returns a single Python [pandas](https://pandas.pydata.org/pandas-docs/stable/index.html) data frame as output, but you can also output scalars and models as variables. For example, you can output a trained model as a binary variable and pass that to a T-SQL INSERT statement, to write that model to a table. You can also generate plots (in binary format) or scalars (individual values, such as the date and time, the time elapsed to train the model, and so forth).

## Prerequisites

A previous quickstart, [Verify Python exists in SQL Server](quickstart-python-verify.md), provides information and links for setting up the Python environment required for this quickstart.

## Create the source data

Create a small table of test data by running the following T-SQL statement:

```sql
CREATE TABLE PythonTestData (col1 INT NOT NULL)
INSERT INTO PythonTestData VALUES (1);
INSERT INTO PythonTestData VALUES (10);
INSERT INTO PythonTestData VALUES (100);
GO
```

When the table has been created, use the following statement to query the table:
  
```sql
SELECT * FROM PythonTestData
```

**Results**

![Contents of the PythonTestData table](./media/select-pythontestdata.png)

## Inputs and outputs

Let's look at the default input and output variables of sp_execute_external_script: `InputDataSet` and `OutputDataSet`.

1. You can get the data from the table as input to your R script. Run the statement below. It gets the data from the table, makes a round trip through the R runtime, and returns the values with the column name *NewColName*.

    The data returned by the query is passed to the R runtime, which returns the data to SQL Database as a data frame. The WITH RESULT SETS clause defines the schema of the returned data table for SQL Database.

    ```sql
    EXECUTE sp_execute_external_script
        @language = N'Python'
        , @script = N'OutputDataSet = InputDataSet;'
        , @input_data_1 = N'SELECT * FROM PythonTestData;'
    WITH RESULT SETS (([NewColName] INT NOT NULL));
    ```

    **Results**

    ![Output from Python script that returns data from a table](./media/python-output-pythontestdata.png)

2. Let's change the name of the input or output variables. The script above used the default input and output variable names, _InputDataSet_ and _OutputDataSet_. To define the input data associated with _InputDatSet_, you use the *@input_data_1* variable.

    In this script, the names of the output and input variables for the stored procedure have been changed to *SQL_out* and *SQL_in*:

    ```sql
    EXECUTE sp_execute_external_script
      @language = N'Python'
      , @script = N'SQL_out = SQL_in'
      , @input_data_1 = N' SELECT 12 as Col;'
      , @input_data_1_name  = N'SQL_in'
      , @output_data_1_name =  N'SQL_out'
      WITH RESULT SETS (([NewColName] INT NOT NULL));
    ```

    The case of the input and output variables in `@input_data_1_name` and `@output_data_1_name` have to match the case of the ones in the Python code in `@script`, as Python is case-sensitive.

    Also, the order of the parameters is important. You must specify the required parameters *@input_data_1* and *@output_data_1* first, in order to use the optional parameters *@input_data_1_name* and *@output_data_1_name*.

    Only one input dataset can be passed as a parameter, and you can return only one dataset. However, you can call other datasets from inside your Python code and you can return outputs of other types in addition to the dataset. You can also add the OUTPUT keyword to any parameter to have it returned with the results. 

    The `WITH RESULT SETS` statement defines the schema for the data which is used in SQL Server. You need to provide SQL compatible data types for each column you return from Python. You can use the schema definition to provide new column names too as you do not need to use the column names from the Python data.frame.

3. You can also generate values using the Python script and leave the input query string in _@input_data_1_ blank.

    ```sql
    EXECUTE sp_execute_external_script
    @language = N'Python'
    , @script = N'
    import pandas as pd
    mytextvariable = pandas.Series(["hello", " ", "world"]);
    OutputDataSet = pd.DataFrame(mytextvariable);'
    , @input_data_1 = N''
    WITH RESULT SETS (([Col1] CHAR(20) NOT NULL));
    ```

    **Results**

    ![Query results using @script as input](./media/python-data-generated-output.png)

## Next steps

Examine some of the problems that you might encounter when passing tabular data between Python and SQL Server.

> [!div class="nextstepaction"]
> [Quickstart: Python data structures in SQL Server](quickstart-python-data-structures.md)