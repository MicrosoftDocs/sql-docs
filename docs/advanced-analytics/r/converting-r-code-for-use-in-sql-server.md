---
title: "Converting R Code for Use in R Services | Microsoft Docs""
ms.date: "12/13/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "R"
ms.assetid: 0b11ab52-b2f9-4a4f-b1ab-68ba09c8adcc
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---
# Converting R code for execution in-database

When you move R code from R Studio or another environment to SQL Server, very often the code works without further modification. This is particularly true if the code is simple, such as a function that takes some inputs and returns a value. It is also easier to port solutions that use the **RevoScaleR** or **MicrosoftML** packages, which support execution in different execution contexts with minimal changes.

Your code might require substantial changes if:

+ You use R libraries that access the network or that cannot be installed on SQL Server.
+ The code makes separate calls to data sources outside SQL Server, such as Excel worksheets, files on shares, and other databases. 
+ You want to run the code in the *@script* parameter of [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) and also parameterize the stored procedure.
+ Your original solution includes multiple steps, such as data preparation plus training plus scoring, which you intend to separate for use in a production environment.
+ You want to improve performance by changing libraries, using parallel execution, or offloading some processing to SQL Server. 

This article provides high-level guidance about known issues and requirements.

## Step 1. Plan requirements and resources

**Data sources:** If you intend to embed your R code in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), identify primary and secondary data sources. 

+ **Primary** data sources are large datsets, such as model training data, or input data for predictions. Plan to map your largest dataset to the input parameter of [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

+ **Secondary** data sources are typically smaller data sets, such as lists of factors, additional grouping variables, and so forth. Currently, the stored procedure sp_execute_external_script supports only a single input.

Stored procedure calls preceded by EXECUTE **cannot** be used as an input to [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). You can use queries, views, or any other valid SELECT statement.

Where possible, modify input data statements to work directly in the database, and avoid data movement. For example, rather than moving data to a local CSV file, or making repeated ODBC calls, use SQL queries or views that can be run directly against the database. this provides better performance as well as greater data security.

**Data types:** Make a checklist of possible data type issues. 

+ All R data types are supported. However, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports a greater variety of data types than does R, so some implicit data type conversions are performed when sending [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data to R and vice versa. 

+ NULL values are supported. However, R uses the `na` data construct to represent a missing value, which is similar to a null.

+ You might also need to explicitly cast or convert some data. 

+ For more information, see [R Libraries and Data Types](../r/r-libraries-and-data-types.md).

**Parameters:**  Identify any parameters that need to get data from outside R. 

+ All variables that you pass in as parameters of the stored procedure [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) must be mapped to variables in the R code. By default, variables are mapped by name.

+ Determine the outputs you need. In [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], R code is limited to outputting a single dataset as a data.frame object. However, you can also output multiple scalar outputs, including plots in binary format, and models in varbinary format.

## Step 2. Convert code

If your code is relatively simple, you can embed it in a T-SQL user-defined function, as described in these samples:

    - Data science deep dive simulation
    - UDF for computation

If the code is more complex, use the R package **sqlrutils** to convert your code. This package is designed to help experienced R users write good stored procedure code. It requires that your R code first be reduced to a single function with clearly defined inputs and outputs. After this has been done, you use the package to generate the input and outputs in the correct format, and convert the entire package into a single stored procedure.
    
For more information and examples, see [SqlRUtils](../r/generating-an-r-stored-procedure-for-r-code-using-the-sqlrutils-package.md).

**Performance:**

+ Run queries in advance and review the SQL Server query plans to identify tasks that can be performed in parallel. 

+ Review your R code to determine if there are steps that can be performed independently, or performed more efficiently, by using a separate stored procedure call. For example, you might get better performance by doing feature engineering or feature extraction separately, and saving the values to a table.

+ Look for ways to use T-SQL rather than R code for set-based computations. 

    For example, this R solution uses both user-defined T-SQL functions and R for the same feature engineering tasks, but the UDf provides better performance: [Data Science End-to-End Walkthrough](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md).

+ If possible, replace conventional R functions with **ScaleR** functions that support distributed execution. For more information, see [Comparison of Base R and Scale R Functions](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler-compared-to-base-r).

+ If you are running R as part of a stored procedure, you can pass through multiple **scalar** inputs. For any data that you want to use in the output, but don't plan to handle or transform the data in R, just mark the parameter with the **OUTPUT** keyword.

    For example, you could pass a model name, the system time, or other information used for auditing and storage, but that is not used by the R script. 

+ All columns in the input dataset must be mapped to variables in the R script. Variables are mapped automatically by name. For example, assume your R script contains a formula like this one:
    
    ```R
    formula <- ArrDelay ~ CRSDepTime + DayOfWeek + CRSDepHour:DayOfWeek
    ```
    
     An error is raised if the input dataset does not contain columns with the matching names ArrDelay, CRSDepTime, DayOfWeek, CRSDepHour, and DayOfWeek.

+ If the input query can be parallelized, set `@parallel=1` as part of your arguments to [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). 

    Parallel processing with this flag is typically possible any time that SQL Server can work with partitioned tables or distribute a query among multiple processes and aggregate the results at the end. Parallel processing with this flag is typically not possible if you are training models using algorithms that require all data to be read, or if you need to create aggregates.

+ To output the dataset returned by the R script, you can use the **WITH RESULT SETS UNDEFINED** option. However, if you need to insert the data into a tabe=le, use the WITH RESULT SET clause to spcifiy the schema. Any other variables in the R script that you want to output must be explicitly mapped to SQL output parameters.

+ If your R script uses the argument `@parallel=1`, you must define the output schema. The reason is that multiple processes might be created by SQL Server to run the query in parallel, with the results collected at the end. Therefore, the output schema must be defined before the parallel processes can be created.

+ Avoid writing predictions or intermediate results to file. Write predictions to a table instead, to avoid data movement.

### Step 3. Prepare for deployment

When you are working in a dedicated R development environment such as [!INCLUDE[rsql_rtvs_md](../../includes/rsql-rtvs-md.md)] or RStudio, the typical workflow is to pull data to your computer, analyze the data iteratively, and then write out or display the results. However, when standalone R code is migrated to SQL Server, much of this process can be simplified or delegated to other SQL Server tools. Moreover, doing so can improve performance in many cases.

+ Make a note of dependencies, such as required packages, in advance. In a development environment, it might be okay to install packages as part of your code, but this is a bad practice in a production environment. Notify the administrator so that packages can be installed and tested in advance of deploying your code.

+ Document the schema of the results expected in each section of the code.

+ Define input data as a SQL query. Avoid data movement. |
+ Summarize and visualize data; Plots can be exported as images or sent to a remote workstation.|
+ Use R in-database if you don't want to alter your code, but look at optimizing your queries. Investigate whether it might be more efficient to call T-SQL functions or custom UDFs.
+ Data cleansing:  Perform feature engineering, feature extraction, and data cleansing in advance as part of data workflows.
+ Wrap predict functions in stored procedures for direct access by applications.

### Other resources

To view examples of how R code can be run in SQL Server, see these walkthroughs:

+ [In-Database Analytics for the SQL Developer](../tutorials/sqldev-in-database-r-for-sql-developers.md)
  Demonstrates how you can make your R code more modular by wrapping it  in stored procedures

+ [End-to-End Data Science Solution](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md)
  Includes a comparison of feature engineering in R and T-SQL




## Restrictions



### Dependencies

 + Avoid installing packages from R code. On SQL Server, packages should be installed in advance.
 
  Be sure to install packages into the default package library used by Machine Learning Services. For more information, see [R Package Management for SQL Server](../r/r-package-management-for-sql-server-r-services.md)
