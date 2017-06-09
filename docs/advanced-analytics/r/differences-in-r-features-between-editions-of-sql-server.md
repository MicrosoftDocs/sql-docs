---
title: "Differences in R Features between Editions of SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "06/09/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 8b33a3e2-04d3-4bad-9335-9568ae09db0b
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---

# Differences in R Features between Editions of SQL Server
 
 Support for machine learning is available in the following editions of SQL Server 2016 and SQL Server 2017. 

  
-   **Enterprise Edition**  
    
     Includes R Services, for in-database analytics in SQL Server. Also includes R Server (Standalone), which can be used to connect to a variety of databases and pull data for analysis at scale, but does not run in-database.  In SQL Server 2017, the equivalent features are Machine Learning Services (In-Database) and Machine Learning Server (Standalone).

     No restrictions. Optimized performance and scalability through parallelization and streaming. Supports analysis of large datasets that do not fit in the available memory, by using enhanced R packages, streaming, and parallel execution.  

     
     Newer editions of Microsoft R Server include an improved version of the operationalization engine (formerly known as DeployR) that supports rapid, secure deployment and sharing of R solutions. For more information, see [Operationalize](https://msdn.microsoft.com/microsoft-r/operationalize/about).
  
     In-database analytics in SQL Server supports resource governance of external scripts to customize server resource usage.  
  
-   **Developer Edition**  

    Same capabilities as Enterprise Edition; however, Developer Edition cannot be used in production environments.  

  
-   **Standard Edition**  
  

     Has all the capabilities of in-database analytics included with Enterprise Edition, except for resource governance. Performance and scale is also limited: the data that can be processed must fit in server memory, and processing is limited to a single compute thread, even when using the **RevoScaleR** functions.


  
-   **Express And Web Editions**  
  
     Only Express Edition with Advanced Services includes the machine learning features. The performance limitations are similar to Standard Edition.  

For all editions, the following machine learning languages are supported:

+ SQL Server 2016: R 
+ SQL Server 2017: R and Python

For more information about other product features, see [Editions and Supported Features for SQL Server 2016](../../sql-server/editions-and-supported-features-for-sql-server-2016.md) 
 
Microsoft R Open is included with all editions.
Microsoft R Client can work with all editions.
  
## Enterprise Edition  

Performance of machine learning solutions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is expected to generally be better than any conventional implementation using R, given the same hardware. That is because, in SQL Server, R solutions can be run using server resources and sometimes distributed to multiple processes using the **RevoScaleR** functions. Performance has not been assessed for Python solutions, as the feature is still under development, but some of the same benefits are expected to apply.  

  
 Users can also expect to see considerable differences in performance and scalability for the same machine learning solution if run in Enterprise Edition vs. Standard Edition. Reasons include support for parallel processing, streaming, and increased threads available for R worker processing.  
  
 However, performance even on identical hardware can be affected by many factors outside the R or Python code. These factors  include competing demands on server resources, the type of query plan that is created, schema changes, the need to update statistics or create a new query plan, fragmentation, and more. It is possible that a stored procedure containing R or Python code might run in seconds under one workload, but take minutes when there are other services running.  Therefore, we recommend that you monitor multiple aspects of server performance, including networking for remote compute contexts, when measuring machine learning performance.  

We also recommend that you configure [Resource Governor](../../relational-databases/resource-governor/resource-governor.md) (available in Enterprise Edition) to customize the way that external script jobs are prioritized or handled under heavy server workloads. You can define classifier functions to specify the source of the external script job and prioritize certain workloads, limit the amount of memory used by SQL queries,  and control the number of parallel processes used on a workload basis.  
  
## Developer Edition  

Developer Edition provides performance equivalent to that of Enterprise Edition; however, use of Developer Edition is not supported for production environments.  
  
  
## Standard Edition  

Even Standard Edition should offer some performance benefit, in comparison to standard R packages, given the same hardware configuration.  
  
 However, Standard Edition does not support Resource Governor. Using resource governance is the best way to customize server resources to support varied workloads such as model training and scoring.  
  
 Standard Edition also provides limited performance and scalability in comparison to Enterprise and Developer Editions. All the **ScaleR** functions and packages are included with Standard Edition, but the service that launches and manages R scripts is limited in the number of processes it can use. Moreover, data processed by the script must fit in memory.  The same restrictions apply to solutions that use **revoscalepy**.
  
  
## Express Edition with Advanced Services  

Express Edition is subject to the same limitations as Standard Edition.  
  
## See Also  

[Editions and Supported Features for SQL Server 2016](../../sql-server/editions-and-supported-features-for-sql-server-2016.md) 


