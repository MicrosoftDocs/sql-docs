---
title: "Differences in R Features between Editions of SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "06/05/2017"
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
# Differences in Machine Learning Features between Editions of SQL Server
 
 Machine learning services are available in the following editions of SQL Server 2016 and SQL Server 2017. Note that SQL Server 2016 R Services supports the R language only; in SQL Server 2017 support for the Python language was introduced.  
  
-   **Enterprise Edition**  
    
     SQL Server 2016 Enterprise Edition includes both R Services, for in-database analytics in SQL Server, as well as R Server (Standalone) on Windows, which can be used to connect to a variety of databases and pull data for analysis at scale, but which does not run in-database.  In SQL Server 2017, the equivalent feature names are Machine Learning Services (In-Database) and Machine Learning Server (Standalone). 

     No restrictions. Optimized performance and scalability through parallelization and streaming. Supports analysis of large datasets that do not fit in the available memory, by using the **RevoScaleR** package and other scalable R and Python libraries.  
     
     Newer editions of Microsoft R Server include an improved version of the operationalization engine (formerly known as DeployR) that supports rapid, secure deployment and sharing of R solutions. For more information, see [Operationalize](https://msdn.microsoft.com/microsoft-r/operationalize/about).
  
     In-database analytics in SQL Server supports resource governance of external scripts to customize server resource usage.  
  
-   **Developer Edition**  

    Same capabilities as Enterprise Edition; however, Developer Edition cannot be used in production environments.  

  
  
-   **Standard Edition**  
  
     Has all the capabilities of in-database analytics included with Enterprise Edition, except for the flexible resource governance. Performance and scale is also limited: the data that can be processed has to fit in server memory, and processing is limited to a single compute thread, even when using the **RevoScaleR** libraries.
  
-   **Express And Web Editions**  
  
     Only the Express Edition with Advanced Services includes R Services (or Machine Learning Services). The performance limitations are similar to Standard Edition.  Web Edition incudes R Services with limited functionality, similar to Standard Edition.
  
 For more information about other product features, see [Editions and Supported Features for SQL Server 2016](../../sql-server/editions-and-supported-features-for-sql-server-2016.md) 
 
> [!NOTE]
>
> + Microsoft R Open is included with all editions.
> + Microsoft R Client can work with all editions.
  
## Enterprise Edition  

Performance of R solutions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is expected to generally be better than any conventional R implementation, given the same hardware, because R can be run using server resources and sometimes distributed to multiple processes using the **ScaleR** functions.  
  
 Users can also expect to see considerable differences in performance and scalability for the same R functions if run in Enterprise Edition vs. Standard Edition. Reasons include support for parallel processing, streaming, and increased threads available for R worker processing.  
  
 However, performance even on identical hardware can be affected by many factors outside the R code, including competing demands on server resources, the type of query plan that is created, schema changes, the need to update statistics or create a new query plan, fragmentation, and more. It is possible that a stored procedure containing R code might run in seconds under one workload, but take minutes when there are other services running.  Therefore, we recommend that you monitor multiple aspects of server performance, including networking for remote compute contexts, when quantifying R job performance.  

We also recommend that you configure [Resource Governor](../../relational-databases/resource-governor/resource-governor.md) (available in Enterprise Edition) to customize the way that R jobs are prioritized or handled under heavy server workloads. You can define classifier functions to specify the source of the R job and prioritize certain workloads, limit the amount of memory used by SQL queries,  and control the number of parallel processes used on a workload basis.  
  
## Developer Edition  

Developer Edition provides performance equivalent to that of Enterprise Edition; however, use of Developer Edition is not supported for production environments.  
  
  
## Standard Edition  

Even Standard Edition should offer some performance benefit, in comparison to standard R packages, given the same hardware configuration.  
  
 However, Standard Edition does not support Resource Governor. Using resource governance is the best way to customize server resources to support varied R workloads such as model training and scoring.  
  
 Standard Edition also provides limited performance and scalability in comparison to Enterprise and Developer Editions. Specifically, all of the **ScaleR** functions and packages are included with Standard Edition, but the service that launches and manages R scripts is limited in the number of processes it can use. Moreover, data processed by the script must fit in memory.  
  
  
## Express Edition with Advanced Services and Web Edition  

Express Edition and Web Edition are subject to the same limitations as Standard Edition.
  
## See Also  [Editions and Supported Features for SQL Server 2016](../../sql-server/editions-and-supported-features-for-sql-server-2016.md) 

  
