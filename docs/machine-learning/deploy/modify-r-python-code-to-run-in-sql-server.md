---
title: "Modify R/Python code to run in SQL Server"
description: Learn how to modify your R or Python code to run as a SQL Server stored procedure to improve performance when accessing SQL data.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 05/24/2022
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom:
- seo-lt-2019
- contperf-fy21q3
- event-tier1-build-2022
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Modify R/Python code to run in SQL Server (In-Database) instances
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

This article provides high-level guidance on how to modify R or Python code to run as a SQL Server stored procedure to improve performance when accessing SQL data.

When you move R/Python code from a local IDE or other environment to SQL Server, the code generally works without further modification. This is especially true for simple code, such as a function that takes some inputs and returns a value. It's also easier to port solutions that use the **RevoScaleR**/**revoscalepy** packages, which support execution in different execution contexts with minimal changes. Note that **MicrosoftML** applies to [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], and [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], and does not appear in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

However, your code might require substantial changes if any of the following apply:

+ You use libraries that access the network or that cannot be installed on SQL Server.
+ The code makes separate calls to data sources outside SQL Server, such as Excel worksheets, files on shares, and other databases.
+ You want to parameterize the stored procedure and run the code in the *\@script* parameter of [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).
+ Your original solution includes multiple steps that might be more efficient in a production environment if executed independently, such as data preparation or feature engineering vs. model training, scoring, or reporting.
+ You want to optimize performance by changing libraries, using parallel execution, or offloading some processing to SQL Server.

## Step 1. Plan requirements and resources

### Packages

+ Determine which packages are needed and ensure that they work on SQL Server.

+ Install packages in advance, in the default package library used by Machine Learning Services. User libraries are not supported.

### Data sources

+ If you intend to embed your code in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), identify primary and secondary data sources.

  + **Primary** data sources are large datasets, such as model training data, or input data for predictions. Plan to map your largest dataset to the input parameter of [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

  + **Secondary** data sources are typically smaller data sets, such as lists of factors, or additional grouping variables.
  
  Currently, sp_execute_external_script supports only a single dataset as input to the stored procedure. However, you can add multiple scalar or binary inputs.

  Stored procedure calls preceded by EXECUTE cannot be used as an input to [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). You can use queries, views, or any other valid SELECT statement.

+ Determine the outputs you need. If you run code using sp_execute_external_script, the stored procedure can output only one data frame as a result. However, you can also output multiple scalar outputs, including plots and models in binary format, as well as other scalar values derived from code or SQL parameters.

### Data types

For a detailed look at the data type mappings between R/Python and SQL Server, see these articles:
+ [Data type mappings between R and SQL Server](../r/r-libraries-and-data-types.md)
+ [Data type mappings between Python and SQL Server](../python/python-libraries-and-data-types.md)

Take a look at the data types used in your R/Python code and do the following:

+ Make a checklist of possible data type issues.

  All R/Python data types are supported by SQL Server Machine Learning Services. However, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports a greater variety of data types than does R or Python. Therefore, some implicit data type conversions are performed when moving [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data to and from your code. You might need to explicitly cast or convert some data.

  NULL values are supported. However, R uses the `na` data construct to represent a missing value, which is similar to a null.

+ Consider eliminating dependency on data that cannot be used by R: for example, rowid and GUID data types from SQL Server cannot be consumed by R and will generate errors.

## Step 2. Convert or repackage code

How much you change your code depends on whether you intend to submit the code from a remote client to run in the SQL Server compute context, or intend to deploy the code as part of a stored procedure. The latter can provide better performance and data security, though it imposes some additional requirements.

+ Define your primary input data as a SQL query wherever possible to avoid data movement.

+ When running code in a stored procedure, you can pass through multiple **scalar** inputs. For any parameters that you want to use in the output, add the **OUTPUT** keyword.

  For example, the following scalar input `@model_name` contains the model name, which is also later modified by the R script, and output in its own column in the results:

  ```sql
  -- declare a local scalar variable which will be passed into the R script
  DECLARE @local_model_name AS NVARCHAR (50) = 'DefaultModel';

  -- The below defines an OUTPUT variable in the scope of the R script, called model_name
  -- Syntactically, it is defined by using the @model_name name. Be aware that the sequence
  -- of these parameters is very important. Mandatory parameters to sp_execute_external_script
  -- must appear first, followed by the additional parameter definitions like @params, etc.
  EXECUTE sp_execute_external_script @language = N'R', @script = N'
    model_name <- "Model name from R script"
    OutputDataSet <- data.frame(InputDataSet$c1, model_name)'
    , @input_data_1 = N'SELECT 1 AS c1'
    , @params = N'@model_name nvarchar(50) OUTPUT'
    , @model_name = @local_model_name OUTPUT;

  -- optionally, examine the new value for the local variable:
  SELECT @local_model_name;
  ```

+ Any variables that you pass in as parameters of the stored procedure [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) must be mapped to variables in the code. By default, variables are mapped by name. All columns in the input dataset must also be mapped to variables in the script.
  
  For example, assume your R script contains a formula like this one:

  ```R
  formula <- ArrDelay ~ CRSDepTime + DayOfWeek + CRSDepHour:DayOfWeek
  ```
  
  An error is raised if the input dataset does not contain columns with the matching names ArrDelay, CRSDepTime, DayOfWeek, CRSDepHour, and DayOfWeek.

+ In some cases, an output schema must be defined in advance for the results.

  For example, to insert the data into a table, you must use the **WITH RESULT SET** clause to specify the schema.

  The output schema is also required if the script uses the argument `@parallel=1`. The reason is that multiple processes might be created by SQL Server to run the query in parallel, with the results collected at the end. Therefore, the output schema must be prepared before the parallel processes can be created.
  
  In other cases, you can omit the result schema by using the option **WITH RESULT SETS UNDEFINED**. This statement returns the dataset from the script without naming the columns or specifying the SQL data types.

+ Consider generating timing or tracking data using T-SQL rather than R/Python.

  For example, you could pass the system time or other information used for auditing and storage by adding a T-SQL call that's passed through to the results, rather than generating similar data in the script.

### Improve performance and security

::: moniker range=">=sql-server-2016||>=sql-server-linux-ver15"
+ Avoid writing predictions or intermediate results to a file. Write predictions to a table instead to avoid data movement.
::: moniker-end

+ Run all queries in advance, and review the SQL Server query plans to identify tasks that can be performed in parallel.

  If the input query can be parallelized, set `@parallel=1` as part of your arguments to [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

  Parallel processing with this flag is typically possible any time that SQL Server can work with partitioned tables or distribute a query among multiple processes and aggregate the results at the end. Parallel processing with this flag is typically not possible if you're training models using algorithms that require all data to be read, or if you need to create aggregates.

+ Review your code to determine if there are steps that can be performed independently, or performed more efficiently, by using a separate stored procedure call. For example, you might get better performance by doing feature engineering or feature extraction separately and saving the values to a table.

+ Look for ways to use T-SQL rather than R/Python code for set-based computations.

  ::: moniker range=">=sql-server-2016||>=sql-server-linux-ver15"
  For example, this R solution shows how user-defined T-SQL functions and R can perform the same feature engineering task: [Data Science End-to-End Walkthrough](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md).
  ::: moniker-end

+ Consult with a database developer to determine ways to improve performance by using SQL Server features such as [memory-optimized tables](../../relational-databases/in-memory-oltp/introduction-to-memory-optimized-tables.md), or, if you have Enterprise Edition, [Resource Governor](../../relational-databases/resource-governor/resource-governor.md).

+ If you're using R, then if possible replace conventional R functions with **RevoScaleR** functions that support distributed execution. For more information, see [Comparison of Base R and RevoScaleR Functions](/machine-learning-server/r-reference/revoscaler/revoscaler-compared-to-base-r).

## Step 3. Prepare for deployment

+ Notify the administrator so that packages can be installed and tested in advance of deploying your code.

  In a development environment, it might be okay to install packages as part of your code, but this is a bad practice in a production environment.

  User libraries are not supported, regardless of whether you're using a stored procedure or running R/Python code in the SQL Server compute context.

### Package your R/Python code in a stored procedure

+ Create a T-SQL user-defined function, embedding your code using the [sp-execute-external-script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) statement.

+ If you have complex R code, use the R package **sqlrutils** to convert your code. This package is designed to help experienced R users write good stored procedure code.
  You rewrite your R code as a single function with clearly defined inputs and outputs, then use the **sqlrutils** package to generate the input and outputs in the correct format. The **sqlrutils** package generates the complete stored procedure code for you, and can also register the stored procedure in the database.

  For more information and examples, see [sqlrutils (SQL)](../r/ref-r-sqlrutils.md).

### Integrate with other workflows

+ Leverage T-SQL tools and ETL processes. Perform feature engineering, feature extraction, and data cleansing in advance as part of data workflows.

  When you're working in a dedicated development environment, you might pull data to your computer, analyze the data iteratively, and then write out or display the results.
  However, when standalone code is migrated to SQL Server, much of this process can be simplified or delegated to other SQL Server tools.

+ Use secure, asynchronous visualization strategies.

  Users of SQL Server often cannot access files on the server, and SQL client tools typically do not support the R/Python graphics devices. If you generate plots or other graphics as part of the solution, consider exporting the plots as binary data and saving to a table, or writing.

+ Wrap prediction and scoring functions in stored procedures for direct access by applications.

## Next steps

To view examples of how R and Python solutions can be deployed in SQL Server, see these tutorials:

### R tutorials

+ [Develop a predictive model in R with SQL machine learning](../tutorials/r-predictive-model-introduction.md)

+ [Predict NYC taxi fares with binary classification](../tutorials/r-taxi-classification-introduction.md)

::: moniker range=">=sql-server-2016||>=sql-server-linux-ver15"
+ [SQL development for R data scientists](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md)
::: moniker-end

### Python tutorials

+ [Predict ski rental with linear regression with SQL machine learning](../tutorials/python-ski-rental-linear-regression.md)

+ [Predict NYC taxi fares with binary classification](../tutorials/python-taxi-classification-introduction.md)
