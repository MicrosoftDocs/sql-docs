---
title: "SQL Server Machine Learning Services| Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "08/20/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: ba1dea65-40ea-484a-b767-53680c954934
caps.latest.revision: 38
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# SQL Server Machine Learning Services

  SQL Server 2017 Machine Learning Services (previously SQL Server 2016 R Services) provides a platform for developing and deploying intelligent applications that uncover new insights. You can use the rich and powerful R language and the many packages from the community to create models and generate predictions using your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data.
  
  Because machine learning is integrated with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can keep analytics close to the data and eliminate the costs and security risks associated with data movement.
  
SQL Server supports the open source R language with a comprehensive set of tools and technologies that offer superior performance, security, reliability, and manageability. You can deploy R solutions using convenient, familiar SQL tools, and your production applications can call the R runtime and retrieve predictions and visuals using [!INCLUDE[tsql](../../includes/tsql-md.md)]. You also get the [Microsoft R](https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler) libraries to improve the scale and performance of your R solutions, including RevoScaleR, revoscalepy, and MicrososftML.
  
Through SQL Server setup, you can install both server and client components.
  
## Machine learning in SQL Server 2017

+ Install **Machine Learning Services (In-Database)** during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup to enable secure execution of R or Python scripts on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer.
  
    When you select this feature, extensions are installed in the database engine to support execution of code written in R or Python. A new service is created, the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)], to manage communications between the external runtimes and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.
  
+ Install **Microsoft Machine Learning Server (Standalone)** on a separate computer if you don't need to use SQL Server as the compute context. Machine Learning Server includes the same machine learning components, plus the mrsdeploy package for scalable, distributed execution of machine learning jobs as a web service.
  
+    Install [Microsoft R Client](https://docs.microsoft.com/r-server/r-client/what-is-microsoft-r-client) on remote computers to develop solutions that can be deployed to SQL Server, or to Machine Learning Server on Windows, Linux, or Hadoop.

## Machine learning in SQL Server 2016

+ Install **R Services (In-Database)** during setup of SQL Server 2016 to enable secure execution of R scripts on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer.
  
    When you select this feature, you get the ability to run R script using the SQL Server as the compute context, or to run R script in a stored procedure.
  
+   Install **Microsoft R Server (Standalone)** from SQL Server 2016 setup to set up the R components on separate computer that you use for developin R solutions.


## Which type of machine learning service do I need?

+ If you need to run your R code in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], either by using stored procedures or by using the SQL Server instance as the compute context, you must install [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] as described [here](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md).

+ Microsoft Machine Learning Server (Standalone) is a separate option designed for using the [Microsoft R](https://docs.microsoft.com/r-server/r-reference/introducing-r-server-r-package-reference) and [related Python libraries](../python/what-is-revoscalepy.md) on a Windows computer that is not running SQL Server. It does, however, require an Enterprise Edition license for SQL Server.
    
    We recommend that you install Microsoft Machine Learning Server (Standalone) on a laptop or other remote computer used for development, and use that computer to create and test machine learning solutions that can easily be deployed to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is running Machine Learning Services \(In-Database\) or another supported compute context.
  
    You can also use the **mrsdeploy** package that is installed with Machine Learning Server to publish and distribute R and Python jobs as a web service.

## Additional resources

+ [Getting Started with SQL Server R Services](../../advanced-analytics/r/getting-started-with-sql-server-r-services.md)
 
    Describes common scenarios for uses of R with SQL Server

+ [Set Up SQL Server R Services In-Database](../../advanced-analytics/r/set-up-sql-server-r-services-in-database.md)

    Install R and associated database components as part of SQL Server setup
  
+ [SQL Server R Tutorials](../../advanced-analytics/tutorials/sql-server-r-tutorials.md)

    Learn how to create SQL Server data sources in your R code, and how to use remote compute contexts. Other tutorials aimed at SQL developers demonstrate how to train and deploy an R model in SQL Server.
