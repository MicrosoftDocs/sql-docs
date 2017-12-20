---
title: "Differences in machine learning features between editions of SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "11/16/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 8b33a3e2-04d3-4bad-9335-9568ae09db0b
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---

# Differences in machine learning features between editions of SQL Server
 
 Support for machine learning is available in SQL Server 2016 and SQL Server 2017. This article lists the editions that support the feature, describes additional limitations that apply in specific editions, and lists capabilities available only in certain editions.

 > [!NOTE]
 > In general, SQL Server Machine Learning does not include the [operationalization](https://docs.microsoft.com/machine-learning-server/what-is-operationalization) features that are included in Microsoft R Server or Machine Learning Server.
 > 
 > If you need these features, you can install Microsoft R Server or Machine Learning Server separately, to support deployment of predictive models as a web service. 

## Summary of differences

-   **Enterprise Edition**
    
     SQL Server 2017 includes Machine Learning Services (In-Databases. SQL Server 2016 includes R Services. This feature supports in-database analytics in SQL Server, including the use of SQL Server as a compute context.
     
     SQL Server 2017 includes Microsoft Machine Learning Server (Standalone). SQL Server 2016 includes Microsoft R Server (Standalone). This feature supports operationalization of machine learning that does not require the use of SQL Server as a compute context.

     There are no restrictions on these features in Enterprise Edition, which provides optimized performance and scalability through parallelization and streaming. This edition also maximizes the use of platform support for streaming and parallel execution. This means that, unlike Standard Edition, input data does not need to fit in memory, but can be streamed.
     
     In-database analytics using SQL Server supports resource governance of external scripts to customize server resource usage.
     
     Newer editions of Microsoft R Server and Machine Learning Server include an improved version of the operationalization engine that supports rapid, secure deployment and sharing of R solutions. For more information, see [Operationalize analytics with Machine Learning Server](https://docs.microsoft.com/machine-learning-server/what-is-operationalization).

-   **Developer Edition**

     Same capabilities as Enterprise Edition; however, Developer Edition cannot be used in production environments.  
  
-   **Standard Edition**

     Has all the capabilities of in-database analytics included with Enterprise Edition, except for resource governance. Performance and scale are also limited: the data that can be processed must fit in server memory, and processing is limited to a single compute thread, even when using the **RevoScaleR** functions.
  
-   **Express and Web Editions**
  
     Only Express Edition with Advanced Services includes the machine learning features. The performance limitations are similar to Standard Edition. 
     
     Web Edition is not intended for tasks such as creating machine learning models. However, you can use the PREDICT function to perform scoring using models trained elsewhere.

-   **Azure SQL Database**
  
     After an initial test release, R Services is currently **not** available in Azure SQL Database, pending further development. 

### External script languages supported

The following machine learning languages are supported for all editions:

+ SQL Server 2017: R and Python
+ SQL Server 2016: R only

Microsoft R Open is included with all editions.

Microsoft R Client can work with all editions.

## Machine learning in Enterprise Edition

Performance of machine learning solutions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is expected to generally surpass implementations using conventional R, given the same hardware. That is because, in SQL Server, R solutions can be run using server resources and sometimes distributed to multiple processes using the **RevoScaleR** functions. 

Performance has not been assessed for Python solutions, as the feature is still under development, but some of the same benefits are expected to apply.

Users can also expect to see considerable differences in performance and scalability for the same machine learning solution if run in Enterprise Edition vs. Standard Edition. Reasons include support for parallel processing, increased threads available for machine learning, and streaming (or chunking), which allows the RevoScaleR functions to handle more data than can fit in memory. 

However, performance even on identical hardware can be affected by many factors outside the R or Python code. These factors include competing demands on server resources, the type of query plan that is created, schema changes, the need to update statistics or create a new query plan, fragmentation, and more. It is possible that a stored procedure containing R or Python code might run in seconds under one workload, but take minutes when there are other services running.  Therefore, we recommend that you monitor multiple aspects of server performance, including networking for remote compute contexts, when measuring machine learning performance.

We also recommend that you configure [Resource Governor](../../relational-databases/resource-governor/resource-governor.md) (available in Enterprise Edition) to customize the way that external script jobs are prioritized or handled under heavy server workloads. You can define classifier functions to specify the source of the external script job and prioritize certain workloads, limit the amount of memory used by SQL queries,  and control the number of parallel processes used on a workload basis.

## Machine learning in Developer Edition

Developer Edition provides performance equivalent to that of Enterprise Edition.

Use of Developer Edition is not supported for production environments.

## Machine learning in Standard Edition

Even Standard Edition should offer some performance benefit, in comparison to standard R packages, given the same hardware configuration.

Standard Edition does not support Resource Governor. Standard Edition also provides limited performance and scalability in comparison to Enterprise and Developer editions.

All the **RevoScaleR** functions and packages are included with Standard Edition, but the service that launches and manages R scripts is limited in the number of processes it can use. Moreover, data processed by the script must fit in memory.

The same restrictions apply to solutions that use **revoscalepy**.

## Machine learning in Express Edition with Advanced Services

Express Edition is subject to the same limitations as Standard Edition.

## Machine learning in Web Edition

Web edition does not support execution of R or Python scripts. However, you can use the [PREDICT](../../t-sql/queries/predict-transact-sql.md) function to perform [native scoring](../sql-native-scoring.md) on a model that has been trained on a different SQL Server or R Server instance and then saved in the required binary format.

## Next steps

For more information, see:

+ [Editions and components of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md)
+ [Editions and components of SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md)

For more information about other features in SQL Server, see:

+ [Editions and supported features of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md) 

For more information about how you can optimize your solution for large data sets, see [tips on computing with big data in R](https://docs.microsoft.com/machine-learning-server/r/tutorial-large-data-tips) documentation.
