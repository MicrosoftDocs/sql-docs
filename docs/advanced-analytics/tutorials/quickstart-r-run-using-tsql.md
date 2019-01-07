---
title: Quickstart for a "Hello World" basic R code execution in T-SQL - SQL Server Machine Learning
description: Quickstart for R script in SQL Server. Learn the basics of calling R script using the sp_execute_external_script system stored procedure in a hello-world exercise.
ms.prod: sql
ms.technology: machine-learning

ms.date: 01/04/2019
ms.topic: quickstart
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Quickstart: "Hello world" R script in SQL Server 
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In this quickstart, you learn key concepts by running a "Hello World" R script inT-SQL, with an introduction to the **sp_execute_external_script** system stored procedure. 

## Prerequisites

A previous quickstart, [Verify R exists in SQL Server](quickstart-r-verify.md), provides information and links for setting up the R environment required for this quickstart.

## Basic R interaction

There are two ways you can run R code in SQL Database:

+ Add R script as an argument of the system stored procedure, [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql).
+ From a [remote R client](https://docs.microsoft.com/sql/advanced-analytics/r/set-up-a-data-science-client), connect to your SQL database, and execute code using the SQL Database as the compute context.

The following exercise is focused on the first interaction model: how to pass R code to a stored procedure.

1. Run a simple script to see how an R script executes in your SQL database.

    ```sql
    EXECUTE sp_execute_external_script
    @language = N'R',
    @script = N'
    a <- 1
    b <- 2
    c <- a/b
    d <- a*b
    print(c, d)'
    ```

2. Assuming that you have everything set up correctly the correct result is calculated, and the R `print` function returns the result to the **Messages** window.

    **Results**

    ```text
    STDOUT message(s) from external script: 
    0.5 2
    ```

    While getting **stdout** messages is useful when testing your code, more often you need to return the results in tabular format, so that you can use it in an application or write it to a table. See [Quickstart: Handle inputs and outputs using R in SQL Server](rtsql-working-with-inputs-and-outputs.md) for more information.

Remember, everything inside the `@script` argument must be valid R code.

## Run a Hello World script

The following exercise runs another simple R scripts.

```sql
EXEC sp_execute_external_script
  @language =N'R',
  @script=N'OutputDataSet<-InputDataSet',
  @input_data_1 =N'SELECT 1 AS hello'
  WITH RESULT SETS (([Hello World] int));
GO
```

Inputs to this stored procedure include:

+ *@language* parameter defines the language extension to call, in this case, R.
+ *@script* parameter defines the commands passed to the R runtime. Your entire R script must be enclosed in this argument, as Unicode text. You could also add the text to a variable of type **nvarchar** and then call the variable.
+ *@input_data_1* is data returned by the query, passed to the R runtime, which returns the data to SQL Server as a data frame.
+ WITH RESULT SETS clause defines the schema of the returned data table for SQL Server, adding "Hello World" as the column name, **int** for the data type.

**Results**

| Hello World |
|-------------|
| 1 |

## Next steps

Now that you have run a couple of simple R scripts, take a closer look at structuring inputs and outputs.

> [!div class="nextstepaction"]
> [Quickstart: Handle inputs and outputs](quickstart-r-inputs-and-outputs.md)
