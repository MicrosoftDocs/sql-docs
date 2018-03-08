---
title: "SQL Server Machine Learning Services - Feature availability across editions | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2018"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---

# Feature availability across editions of SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
 
 Machine learning features are available in SQL Server 2016 and SQL Server 2017. This article lists the editions providing the feature, describes limitations that apply in specific editions, and lists capabilities available only in certain editions.

 > [!NOTE]
 > In general, SQL Server Machine Learning (In-Database) does not include the [operationalization](https://docs.microsoft.com/machine-learning-server/what-is-operationalization) features that are included in a standalone R Server or Machine Learning Server installation. Operationalization includes web service deployment and hosting, and thus competes for the same resources as other SQL Server operations.
 > 
 > For this reason, we recommend that you can install SQL Server 2016 R Server or SQL Server 2017 Machine Learning Server (Standalone) on a different physcial server to support deployment of predictive models as a web service. 

## SQL Server 2017 Machine Learning Services (In-Database) and (Standalone)

The Developer Edition provides performance equivalent to that of Enterprise Edition. Use of Developer Edition is not supported for production environments.

|Feature      |Enterprise      |Standard      |Web      |Express with Advanced Services      |Express                 | 
|-------------|----------------|--------------|---------|------------------------------------|------------------------|
| R interpreter & proprietary packages | Yes | Yes | No | No | No | 
| Python interpreter & client libraries | Yes | Yes | No | No | No | 
| Data chunking (process large amounts of data, in excess of what fits in memory) | Yes | No | No | No | No |
| Scaleable processing (more than 2 processors) | Yes | No | No | No | No |
| Resource Governor | Yes | No | No | No | No |
| [PREDICT](../../t-sql/queries/predict-transact-sql.md) function to perform [native scoring](../sql-native-scoring.md) on a pre-trained model. saved in the required binary format. | Yes | Yes | Yes | Yes | Yes |
| R Client compatible | Yes | Yes | No | No | No | 
| Microsoft R Open || Yes | Yes | No | No | No | 
| Anaconda Python 3.5 | Yes | Yes | No | No | No | 

## SQL Server 2016 R Services (In-Database) and R Server (Standalone)

Feature availability is the same as 2017, minus Python support which was not available in the first 2016 release.

## R feature availability in Azure SQL Database
  
After an initial test release, R Services is currently **not** available in Azure SQL Database, pending further development. 

## Performance expectations for Enterprise Edition

Performance of machine learning solutions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is expected to generally surpass implementations using conventional R, given the same hardware. That is because, in SQL Server, R solutions can be run using server resources and sometimes distributed to multiple processes using the **RevoScaleR** functions. 

Performance has not been assessed for Python solutions, as the feature is still under development, but some of the same benefits are expected to apply.

Users can also expect to see considerable differences in performance and scalability for the same machine learning solution if run in Enterprise Edition vs. Standard Edition. Reasons include support for parallel processing, increased threads available for machine learning, and streaming (or chunking), which allows the RevoScaleR functions to handle more data than can fit in memory. 

However, performance even on identical hardware can be affected by many factors outside the R or Python code. These factors include competing demands on server resources, the type of query plan that is created, schema changes, the need to update statistics or create a new query plan, fragmentation, and more. It is possible that a stored procedure containing R or Python code might run in seconds under one workload, but take minutes when there are other services running.  Therefore, we recommend that you monitor multiple aspects of server performance, including networking for remote compute contexts, when measuring machine learning performance.

We also recommend that you configure [Resource Governor](../../relational-databases/resource-governor/resource-governor.md) (available in Enterprise Edition) to customize the way that external script jobs are prioritized or handled under heavy server workloads. You can define classifier functions to specify the source of the external script job and prioritize certain workloads, limit the amount of memory used by SQL queries,  and control the number of parallel processes used on a workload basis.

## See also

+ [Editions and components of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md)
+ [Editions and components of SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md)
+ [Tips on computing with big data in R (Machine Learning Server)](https://docs.microsoft.com/machine-learning-server/r/tutorial-large-data-tips)
