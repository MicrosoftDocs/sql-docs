---
title: "Operationalizing Your R Code | Microsoft Docs"
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
ms.assetid: f15696b1-2479-4e5f-ac5e-4beaf958a043
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Operationalizing Your R Code
  
  Database developers are tasked with integrating multiple technologies and bringing together the results so that they can be shared throughout the enterprise. The database developer works with application developers, SQL developers, and data scientists to design solutions, recommend data management methods, and architect and deploy solutions. 

  Traditionally, integration of machine learning solutions has meant extensive recoding to support performance and integration. However, operationalizing  R and Python code is made much easier in Microsoft Machine Learning Services, because the code can be run in SQL Server and called using stored procedures.   
  
-   **Interact with R scripts using [!INCLUDE[tsql](../../includes/tsql-md.md)].** Application and database developers, as well as analysts who author reports, can invoke an R script by calling it from a system stored procedure.  
  
     For more information about basic syntax, see [sp_execute_external_script &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) and [Using R Code in T-SQL](../../advanced-analytics/r-services/using-r-code-in-transact-sql-sql-server-r-services.md).  
 
    For an example of how you can deploy R code into production by using stored procedures, see [In-Database Analytics for SQL Developers](../../advanced-analytics/r-services/in-database-advanced-analytics-for-sql-developers-tutorial.md).
  
     Integration with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also means that you can execute R scripts using [!INCLUDE[tsql](../../includes/tsql-md.md)] and embed the results in your application. For example, you can create a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report that runs an R script and then display the plots along with the predictions in the report.  
  
-   **Performance and scale.** Although the open source R language is known to have limitations, the RevoScaleR package APIs provided by [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] can operate on large datasets and benefit from multi-threaded, multi-core, multi-process in-database computations.  
  
     If your R solution uses complex aggregations or involves large datasets, you can leverage SQL’s highly efficient in-memory aggregations and columnstore indexes, and let the R code handle the statistical computations and scoring.  
  
-   **Standard development and management tools.** No additional tools for administration or deployment required -- all R jobs can be called by invoking a stored procedure.  
  
     Moreover, the same R code that you run against [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data can be used against other data sources, such as Hadoop.  
  
 This section describes the concepts you need to understand to successfully convert R solutions and deploy them to production using [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)].  
  
## In This Section

[Working with R Data Types](../../advanced-analytics/r-services/working-with-r-data-types.md)

[Converting R Code for use in R Services](../../advanced-analytics/r-services/converting-r-code-for-use-in-r-services.md)

##  <a name="bkmk_RelatedTasks"></a> Related Tasks  
  
[Performance Tuning for SQL Server R Services](../../advanced-analytics/r-services/sql-server-r-services-performance-tuning.md)
 
## See Also  
 [SQL Server R Services Features and Tasks](../../advanced-analytics/r-services/sql-server-r-services-features-and-tasks.md)   
 [sp_execute_external_script &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md)  
  
  
