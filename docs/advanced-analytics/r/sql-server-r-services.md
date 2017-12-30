---
title: "SQL Server Machine Learning Services| Microsoft Docs"
ms.date: "12/04/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: ba1dea65-40ea-484a-b767-53680c954934
caps.latest.revision: 38
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Active"
---
# SQL Server Machine Learning Services

SQL Server 2017 Machine Learning Services (previously SQL Server 2016 R Services) provides a platform for developing and deploying intelligent applications that uncover new insights. Because machine learning is integrated with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can keep analytics close to the data and eliminate the costs and security risks associated with data movement.
  
+ In SQL Server 2016, you can easily develop and deploy solutions based on the popular R language for data science. 

    Features include the [Microsoft R](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) libraries, that provide new scalability and performance for your R solutions, and state-of-the-art algorithms in [MicrosoftML](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/microsoftml-package).
+ In SQL Server 2017, you can use both R and Python to develop and operationalize data science solutions. 

    The [revoscalepy](../python/what-is-revoscalepy.md) library for Python provides remote compute contexts and scalability comparable to those in RevoScaleR.

SQL Server supports improved performance, security, and manageability for machine learning workloads through a comprehensive set of tools and technologies. You can deploy R or Python solutions using convenient, familiar SQL methodology and tools. Just use [!INCLUDE[tsql](../../includes/tsql-md.md)] to call the R or Python runtimes from your production applications, to build models or retrieve visuals. If you have already trained a model, you can generate predictions from it using only T-SQL, through [native scoring](../sql-native-scoring.md).

## Machine learning in SQL Server 2017

+ Install **Machine Learning Services (In-Database)** during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup to enable secure execution of R or Python scripts on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer.
  
    When you select this feature, extensions are installed in the database engine to support execution of code written in R or Python. A new service is created, the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)], to manage communications between the external runtimes and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.
  
+ Install **Microsoft Machine Learning Server (Standalone)** on a separate computer if you don't need to use SQL Server as the compute context. Machine Learning Server includes the same machine learning components, plus the ability to execute scalable, distributed machine learning jobs as a web service.
  
+ Install [Microsoft R Client](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client) on remote computers to develop solutions that can be deployed to SQL Server, or to Machine Learning Server on Windows, Linux, or Hadoop.

## Machine learning in SQL Server 2016

+ Install **R Services (In-Database)** during setup of SQL Server 2016 to enable secure execution of R scripts on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer.
  
    When you select this feature, you get the ability to run R script using the SQL Server as the compute context, or to run R script in a stored procedure.
  
+ Install **Microsoft R Server (Standalone)** from SQL Server 2016 setup, to install the R components on a separate computer that you can use for developing or deploying R solutions.

## How to get started

SQL Server setup provides two options:

+ Install the in-database analytics feature that is integrated with SQL Server, or
+ Install the standalone version of Machine Learning Server (or Microsoft R Server) that supports at-scale machine learning without an instance of SQL Server.

If you need to run your R code in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], either by using stored procedures or by using the SQL Server instance as the compute context, you must install [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] as described in the [setup guide](../../advanced-analytics/r/set-up-sql-server-r-services-in-database.md). This option provides maximum data security and integration with SQL Server tools.

Microsoft Machine Learning Server (Standalone) is a separate option designed for using the [Microsoft R](https://docs.microsoft.com/machine-learning-server/r-reference/introducing-r-server-r-package-reference) and [related Python libraries](../python/what-is-revoscalepy.md) on a Windows computer that is not running SQL Server. This option requires an Enterprise Edition license for SQL Server.
    
We recommend that you install Machine Learning Server (Standalone) on a laptop or other remote computer used for development, and use that computer to create and test machine learning solutions that can easily be deployed to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is running Machine Learning Services \(In-Database\) or another supported compute context.
  
You can also use the **mrsdeploy** package that is installed with Machine Learning Server to publish and distribute R and Python jobs as a web service.

## Additional resources

+ [Getting started with SQL Server Machine Learning Services](../../advanced-analytics/r/getting-started-with-sql-server-r-services.md)
 
    Describes common scenarios for uses of R with SQL Server

+ [Set up SQL Server Machine Learning Services or R Services In-Database](../../advanced-analytics/r/set-up-sql-server-r-services-in-database.md)

    Install R and associated database components as part of SQL Server setup
  
+ [SQL Server and R tutorials](../../advanced-analytics/tutorials/sql-server-r-tutorials.md)

    Learn how to create SQL Server data sources in your R code, and how to use remote compute contexts. Other tutorials aimed at SQL developers demonstrate how to train and deploy an R model in SQL Server.
