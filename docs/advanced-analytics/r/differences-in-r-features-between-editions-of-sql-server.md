---
title: "SQL Server Machine Learning Services - Feature availability across editions | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2018"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "HeidiSteen"
ms.author: "heidist"
manager: "cgronlun"
ms.workload: "Inactive"
---

# Feature availability across editions of SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
 
 Machine learning features are available in SQL Server 2016 and SQL Server 2017. This article lists the editions providing the feature, describes limitations that apply in specific editions, and lists capabilities available only in certain editions.


## SQL Server 2017 features

Enterprise and Developer editions have the same feature coverage so that you can build solutions for an Enterprise installation without incurring the same cost. Although the editions are functionally equivalent, use of the Developer Edition is not supported for production environments.

The difference between basic and advanced integration is scale. Advanced integration can use all available cores for parallel processing of data sets at any size your computer can accommodate. Basic integration is limited to 2 cores and to data sets fitting in memory. 

Basic and advanced integration applies to the (In-Database) instances. A standalone server is not a database engine instance feature and is offered as an installation option only in Developer and Enterprise editions.

|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express 
|-------------|----------------|--------------|---------|------------------------------------|------------------------|  
|Basic R integration|Yes|Yes|Yes|Yes|No|   
|Advanced R integration|Yes|No|No|No|No| 
|Basic Python integration|Yes|Yes|Yes|Yes|No|
|Advanced Python integration|Yes|No|No|No|No| 
|Machine Learning Server (Standalone)|Yes|No|No|No|No|   

 > [!NOTE]
 > Only a (Standalone) server offers the [operationalization](https://docs.microsoft.com/machine-learning-server/what-is-operationalization) features that are included in a Microsoft (non-SQL-branded) R Server or Machine Learning Server installation. Operationalization includes web service deployment and hosting capabilities.
>
> For an (In-Database) installation, the equivalent approach to operationalizing solutions is leveraging the capabilities of the database engine, when you convert code to a function that can run in a stored procedure.


## SQL Server 2016 R features

SQL Server 2016 includes R integration only. In SQL Server 2016, basic and advanced R integration are equivalent to SQL Server 2017.

## R feature availability in Azure SQL Database
  
After an initial test release, R Services was removed from Azure SQL Database, pending further development. 

## Performance expectations for Enterprise Edition

Performance of machine learning solutions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is expected to generally surpass implementations using conventional R, given the same hardware. That is because, in SQL Server, R solutions can be run using server resources and sometimes distributed to multiple processes using the **RevoScaleR** functions. 

Users can also expect to see considerable differences in performance and scalability for the same machine learning solution if run in Enterprise Edition vs. Standard Edition. Reasons include support for parallel processing, increased threads available for machine learning, and streaming (or chunking), which allows the RevoScaleR functions to handle more data than can fit in memory. 

However, performance even on identical hardware can be affected by many factors outside the R or Python code. These factors include competing demands on server resources, the type of query plan that is created, schema changes, the need to update statistics or create a new query plan, fragmentation, and more. It is possible that a stored procedure containing R or Python code might run in seconds under one workload, but take minutes when there are other services running.  Therefore, we recommend that you monitor multiple aspects of server performance, including networking for remote compute contexts, when measuring machine learning performance.

We also recommend that you configure [Resource Governor](../../relational-databases/resource-governor/resource-governor.md) (available in Enterprise Edition) to customize the way that external script jobs are prioritized or handled under heavy server workloads. You can define classifier functions to specify the source of the external script job and prioritize certain workloads, limit the amount of memory used by SQL queries,  and control the number of parallel processes used on a workload basis.

## See also

+ [Editions and components of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md)
+ [Editions and components of SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md)
+ [Tips on computing with big data in R (Machine Learning Server)](https://docs.microsoft.com/machine-learning-server/r/tutorial-large-data-tips)
