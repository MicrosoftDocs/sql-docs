---
title: Convert R code for stored procedures - SQL Server Machine Learning Services
description: Migrate R code to a SQL Server stored procedure for solution deployment and data access to relational data on SQL Server.
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
ms.author: dphansen
ms.author: davidph
manager: cgronlun
---
# Convert R code for execution in SQL Server (In-Database) instances
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article provides high-level guidance on how to modify R code to work in SQL Server. 

When you move R code from R Studio or another environment to SQL Server, most often the code works without further modification: for example, if the code is simple, such as a function that takes some inputs and returns a value. It is also easier to port solutions that use the **RevoScaleR** or **MicrosoftML** packages, which support execution in different execution contexts with minimal changes.

However, your code might require substantial changes if any of the following apply:

+ You use R libraries that access the network or that cannot be installed on SQL Server.
+ The code makes separate calls to data sources outside SQL Server, such as Excel worksheets, files on shares, and other databases. 
+ You want to run the code in the *@script* parameter of [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) and also parameterize the stored procedure.
+ Your original solution includes multiple steps that might be more efficient in a production environment if executed independently, such as data preparation or feature engineering vs. model training, scoring, or reporting.
+ You want to improve optimize performance by changing libraries, using parallel execution, or offloading some processing to SQL Server. 

## Step 1. Plan requirements and resources

**Packages**

+ Determine which packages are needed and ensure that they work on SQL Server.
 
+ Install packages in advance, in the default package library used by Machine Learning Services. User libraries are not supported.

**Data sources** 

+ If you intend to embed your R code in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), identify primary and secondary data sources. 

    + **Primary** data sources are large datasets, such as model training data, or input data for predictions. Plan to map your largest dataset to the input parameter of [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

    + **Secondary** data sources are typically smaller data sets, such as lists of factors, or additional grouping variables. 
    
    Currently, sp_execute_external_script supports only a single dataset as input to the stored procedure. However, you can add multiple scalar or binary inputs.

    Stored procedure calls preceded by EXECUTE cannot be used as an input to [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). You can use queries, views, or any other valid SELECT statement.

+ Determine the outputs you need. If you run R code using sp_execute_external_script, the stored procedure can output just one data frame as a result. However, you can also output multiple scalar outputs, including plots and models in binary format, as well as other scalar values derived from R code or SQL parameters.

**Data types**

+ Make a checklist of possible data type issues.

    All R data types are supported by SQL Server machine Learning Services. However, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports a greater variety of data types than does R. Therefore, some implicit data type conversions are performed when sending [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data to R, and vice versa. You might need to explicitly cast or convert some data. 

    NULL values are supported. However, R uses the `na` data construct to represent a missing value, which is similar to a null.

+ Consider eliminating dependency on data that cannot be used by R: for example, rowid and GUID data types from SQL Server cannot be consumed by R and generate errors.

    For more information, see [R Libraries and Data Types](../r/r-libraries-and-data-types.md).

## Step 2. Convert or repackage code

How much you change your code depends on whether you intend to submit the R code from a remote client to run in the SQL Server compute context, or intend to deploy the code as part of a stored procedure, which can provide better performance and data security. Wrapping your code in a stored procedure imposes some additional requirements. 

+ Define your primary input data as a SQL query wherever possible, to avoid data movement.

+ When running R in a stored procedure, you can pass through multiple **scalar** inputs. For any parameters that you want to use in the output, add the **OUTPUT** keyword. 

    For example, the following scalar input `@model_name` contains the model name, which is also output in its own column in the results:

    ```sql
    EXEC sp_execute_external_script @model_name="DefaultModel" OUTPUT, @language=N'R', @script=N'R code here'
    ``` 

+ Any variables that you pass in as parameters of the stored procedure [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) must be mapped to variables in the R code. By default, variables are mapped by name.

    All columns in the input dataset must also be mapped to variables in the R script.  For example, assume your R script contains a formula like this one:

    ```R
    formula <- ArrDelay ~ CRSDepTime + DayOfWeek + CRSDepHour:DayOfWeek
    ```
    
    An error is raised if the input dataset does not contain columns with the matching names ArrDelay, CRSDepTime, DayOfWeek, CRSDepHour, and DayOfWeek.

+ In some cases, an output schema must be defined in advance for the results.

    For example, to insert the data into a table, you must use the **WITH RESULT SET** clause to specify the schema.

    The output schema is also required if the R script uses the argument `@parallel=1`. The reason is that multiple processes might be created by SQL Server to run the query in parallel, with the results collected at the end. Therefore, the output schema must be prepared before the parallel processes can be created.
    
    In other cases, you can omit the result schema by using the option **WITH RESULT SETS UNDEFINED**. This statement returns the dataset from the R script without naming the columns or specifying the SQL data types.

+ Consider generating timing or tracking data using T-SQL rather than R.

    For example, you could pass the system time or other information used for auditing and storage by adding a T-SQL call that is passed through to the results, rather than generating similar data in the R script. 

**Improve performance and security**

+ Avoid writing predictions or intermediate results to file. Write predictions to a table instead, to avoid data movement.

+ Run all queries in advance, and review the SQL Server query plans to identify tasks that can be performed in parallel.

    If the input query can be parallelized, set `@parallel=1` as part of your arguments to [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). 

    Parallel processing with this flag is typically possible any time that SQL Server can work with partitioned tables or distribute a query among multiple processes and aggregate the results at the end. Parallel processing with this flag is typically not possible if you are training models using algorithms that require all data to be read, or if you need to create aggregates.

+ Review your R code to determine if there are steps that can be performed independently, or performed more efficiently, by using a separate stored procedure call. For example, you might get better performance by doing feature engineering or feature extraction separately, and saving the values to a table.

+ Look for ways to use T-SQL rather than R code for set-based computations.

    For example, this R solution shows how user-defined T-SQL functions and R can perform the same feature engineering task: [Data Science End-to-End Walkthrough](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md).

+ If possible, replace conventional R functions with **ScaleR** functions that support distributed execution. For more information, see [Comparison of Base R and Scale R Functions](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler-compared-to-base-r).

+ Consult with a database developer to determine ways to improve performance by using SQL Server features such as [memory-optimized tables](https://docs.microsoft.com/sql/relational-databases/in-memory-oltp/introduction-to-memory-optimized-tables), or, if you have Enterprise Edition, [Resource Governor](https://docs.microsoft.com/sql/relational-databases/resource-governor/resource-governor)).

    For more information, see [SQL Server Optimization Tips and Tricks for Analytics Services](https://gallery.cortanaintelligence.com/Tutorial/SQL-Server-Optimization-Tips-and-Tricks-for-Analytics-Services)

### Step 3. Prepare for deployment

+ Notify the administrator so that packages can be installed and tested in advance of deploying your code. 

    In a development environment, it might be okay to install packages as part of your code, but this is a bad practice in a production environment. 

    User libraries are not supported, regardless of whether you are using a stored procedure or running R code in the SQL Server compute context.

**Package your R code in a stored procedure**

+ If your code is relatively simple, you can embed it in a T-SQL user-defined function without modification, as described in these samples:

    + [Create an R function that runs in rxExec](../tutorials/deepdive-create-a-simple-simulation.md)
    + [Feature engineering using T-SQL and R](../tutorials/sqldev-create-data-features-using-t-sql.md)

+ If the code is more complex, use the R package **sqlrutils** to convert your code. This package is designed to help experienced R users write good stored procedure code. 

    The first step is to rewrite your R code as a single function with clearly defined inputs and outputs.

    Then, use the **sqlrutils** package to generate the input and outputs in the correct format. The **sqlrutils** package generates the complete stored procedure code for you, and can also register the stored procedure in the database. 

    For more information and examples, see [sqlrutils (SQL)](ref-r-sqlrutils.md).

**Integrate with other workflows**

+ Leverage T-SQL tools and ETL processes. Perform feature engineering, feature extraction, and data cleansing in advance as part of data workflows.

    When you are working in a dedicated R development environment such as [!INCLUDE[rsql_rtvs_md](../../includes/rsql-rtvs-md.md)] or RStudio, you might pull data to your computer, analyze the data iteratively, and then write out or display the results. 
    
    However, when standalone R code is migrated to SQL Server, much of this process can be simplified or delegated to other SQL Server tools. 

+ Use secure, asynchronous visualization strategies.

    Users of SQL Server often cannot access files on the server, and SQL client tools typically do not support the R graphics device. If you generate plots or other graphics as part of the solution, consider exporting the plots as binary data and saving to a table, or writing.

+ Wrap prediction and scoring functions in stored procedures for direct access by applications.

### Other resources

To view examples of how an R solution can be deployed in SQL Server, see these samples:

+ [Build a predictive model for  ski rental business using R and SQL Server](https://microsoft.github.io/sql-ml-tutorials/R/rentalprediction/)

+ [In-Database Analytics for the SQL Developer](../tutorials/sqldev-in-database-r-for-sql-developers.md)
  Demonstrates how you can make your R code more modular by wrapping it  in stored procedures

+ [End-to-End Data Science Solution](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md)
  Includes a comparison of feature engineering in R and T-SQL
