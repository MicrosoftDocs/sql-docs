---
title: "Converting R Code for Use in R Services | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "05/31/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "R"
ms.assetid: 0b11ab52-b2f9-4a4f-b1ab-68ba09c8adcc
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Converting R Code for Use in R Services
When you move R code from R Studio or another environment to SQL Server, often the code will work without further modification when added to the *@script* parameter of [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). This is particularly true if you have developed your solution using the ScaleR functions, making it relatively simple to change execution contexts.    
    
However, typically you will want to modify your R code to run in SQL Server, both to take advantage of the tighter integration with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and to avoid expensive transfer of data.   
   
   
## Resources  
  
To view examples of how R code can be run in SQL Server, see these walkthroughs:   
+ [In-Database Analytics for the SQL Developer](../../advanced-analytics/r-services/in-database-advanced-analytics-for-sql-developers-tutorial.md)    
  Demonstrates how you can make your R code more modular by wrapping it  in stored procedures  
+ [End-to-End Data Science Solution](../../advanced-analytics/r-services/data-science-end-to-end-walkthrough.md)    
  Includes a comparison of feature engineering in R and T-SQL

## Changes to the Data Science Process in SQL Server  
  
When you are working in [!INCLUDE[rsql_rtvs_md](../../includes/rsql-rtvs-md.md)], RStudio, or other environment, a typical workflow is to pull data to your computer, analyze the data iteratively, and then write out or display the results. However, when standalone R code is migrated to SQL Server, much of this process can be simplified or delegated to other SQL Server tools:

| External code | R in SQL Server |
|-------|-------|
| Ingest data| Define input data as a SQL query inside database | 
| Summarize and visualize data| No change; plots can be exported as images or sent to a remote workstation|
|Feature engineering| You can use  R in-database, but it can be more efficient to call T-SQL functions or custom UDFs|
|Data cleansing part of analysis process| Data cleansing can be delegated to ETL processes and performed in advance|
|Output predictions to file| Output predictions to table or wrap `predict` in stored procedures for direct access by applications|
  

  
## Best Practices  
  
+ Make a note of dependencies, such as required packages, in advance. In a development and testing environment, it might be okay to install packages as part of your code, but this is a bad practice in a production environment. Notify the administrator so that packages can be installed and tested in advance of deploying your code.  
+ Make a checklist of possible data type issues. Document the schemas of the results expected in each section of the code.  
+ Identify primary data sources such as model training data, or input data for predictions vs. secondary sources such as factors, additional grouping variables, and so forth. Map your largest dataset to the input parameter of [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).  
+ Change your input data statements to work directly in the database. Rather than moving data to a local CSV file, or making repeated ODBC calls, you'll get better performance by using SQL queries or views that can be run directly against the database, avoiding data movement.  
+ Use SQL Server query plans to identify tasks that can be performed in parallel. If the input query can be parallelized, set `@parallel=1` as part of your arguments to sp_execute_external_script. Parallel processing with this flag is typically possible any time that SQL Server can work with partitioned tables or distribute a query among multiple processes and aggregate the results at the end. 
   Parallel processing with this flag is typically not possible if you are training models using algorithms that require all data to be read, or if you need to create aggregates. 
+ Where possible, replace conventional R functions with **ScaleR** functions that support distributed execution. For more information, see [Comparison of Functions in Scale R and CRAN R](http://msdn.microsoft.com/library/8f8c91d7-50c3-4ca1-9427-a83d9d2ecd22).
+ Review your R code to determine if there are steps that can be performed independently, or performed more efficiently, by using a separate stored procedure call. For example, you might decide to perform feature engineering or feature extraction separately and add the values in a new column. Use T-SQL  rather than R code for set-based computations. For one example of an R solution that compares performance of UDFs and R for feature engineering tasks, see this tutorial: [Data Science End-to-End Walkthrough](../../advanced-analytics/r-services/data-science-end-to-end-walkthrough.md).  
  
    
## Restrictions    
 Keep in mind the following restrictions when converting your R code:    
   
**Data types**    
-   All R data types are supported. However, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports a greater variety of data types than does R, so some implicit data type conversions are performed when sending [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data to R and vice versa. You might also need to explicitly cast or covert some data.    
    
- NULL values are supported. R uses the `na` data construct to represent missing values, which is similar to nulls.    
    
- For more information, see [Working with R Data Types](../../advanced-analytics/r-services/working-with-r-data-types.md).    
 
 **Inputs and outputs**   
+ You can define only one input dataset as part of the stored procedure parameters. This input query for the stored procedure can be any valid single  SELECT statement. We recommend that you use this input for the biggest dataset, and get smaller datasets if needed by using calls to RODBC. 

+ Stored procedure calls preceded by EXECUTE cannot be used as an input to sp_execute_external_script.    
    
+ All columns in the input dataset must be mapped to variables in the R script. Variables are mapped automatically by name. For example, say your R script contains a formula like this one:    
    
    ```    
    formula <- ArrDelay ~ CRSDepTime + DayOfWeek + CRSDepHour:DayOfWeek    
    ```    
    
     An error will be raised if the input dataset does not contains columns with the matching names ArrDelay, CRSDepTime, DayOfWeek, CRSDepHour, and DayOfWeek.    

+ You can also provide multiple scalar parameters as input. However, any variables that you pass in as parameters of the stored procedure [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) must be mapped to variables in the R code. By default, variables are mapped by name.
+ To include scalar input variables in the output of the R code, just append the **OUTPUT** keyword when you define the variable.             
+ In [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], your R code is limited to outputting a single dataset as a data.frame object. However, you can also output multiple scalar outputs, including plots in binary format, and models in varbinary format.    
    
+ You can usually output the dataset returned by the R script without having to specify names of the output columns, by using the WITH RESULT SETS UNDEFINED option. However, any variables in the R script that you want to output must be mapped to SQL output parameters.
    
+ If your R script uses the argument `@parallel=1`, you must define the output schema. The reason is that multiple processes might be created by SQL Server to run the query in parallel, with the results collected at the end. Therefore, the ouput schema must be defined before the parallel processes can be created.

 **Dependencies**
 + Avoid installing packages from R code. On SQL Server, packages should be installed in advance, and you must use the default package library used by R Services.  
  
  
  


