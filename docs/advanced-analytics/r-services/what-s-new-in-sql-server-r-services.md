---
title: "What&#39;s New in SQL Server R Services | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "2017-01-20"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 6aff043a-8b37-4f3f-9827-10a671e1ad1c
caps.latest.revision: 36
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# What&#39;s New in SQL Server R Services
  [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] is a feature in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and SQL Server vNext that supports enterprise-scale data science.  R is the most popular programming language for advanced analytics, and offers an incredibly rich set of packages and a vibrant and fast-growing developer community. [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] helps you embrace the highly popular open source R language in your business. 
  
 > [!TIP]
 > Already have SQL Server 2016 R Services?
 > Now you can install the latest version of Microsoft R Server on your 2016 instances, to take advantage of more frequent updates to R components. For more information, see [Microsoft R Server 9.0.1](https://msdn.microsoft.com/microsoft-r/rserver-whats-new).  

## What's New in SQL Server vNext
  
+ Introducing the **MicrosoftML** package

   MicrosoftML is a new machine learning package for R from the Microsoft R Server and Microsoft Data Science teams. MicrosoftML brings increased speed, performance and scale for handling a large corpus of text data and high-dimensional categorical data in R models with just a few lines of code. In addition, Microsoft R Server customers will get access to five fast, highly accurate learners that are included in Azure Machine Learning. 
   
   For more information, see [Using the MicrosoftML Package in SQL Server R Services](../../advanced-analytics/r-services/using-the-microsoftml-package-with-sql-server-r-services.md).
   
+ Easier package management for data scientists

  You no longer have to rely on the database administrator to install the R packages you need on SQL Server. New package install and uninstall functions in **RevoScaleR** let you easily install and update packages in R Services from a client computer. 
  
  For the database administrator, new roles are included in [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] for managing permissions associated with packages, both on the instance level and database level. 
  
  For more information, see [R Package Management for SQL Server R Services](../../advanced-analytics/r-services/r-package-management-for-sql-server-r-services.md). 
     
+ New functions in **RevoScaleR** for reading and writing R model objects

  RevoScaleR now includes new serialization functions and a more compact model storage format, to make loading and reading a model fast. 
  
  For more information, see [Save and Load R Objects from SQL Server using ODBC](../../advanced-analytics/r-services/save-and-load-r-objects-from-sql-server-using-odbc.md). 

+ **sqlrutils** package for easier SQL integration

  This R package helps you generate the SQL stored procedure call for your R code. The generated SQL stored procedures can then be used in SQL Server R Services. Examples are provided to help you consolidate your R code into a function that can be parameterized in a SQL stored procedure.
  
  For more information, see [Generating a Stored Procedure for R Code using sqlrutils](../../advanced-analytics/r-services/generating-an-r-stored-procedure-for-r-code-using-the-sqlrutils-package.md). 
  

+ **olapR** package for easy SSAS connectivity

   This new package provides a new dimension of connectivity for R and SQL Server Analysis Services, making it easier to use OLAP data for analysis in R. You can run existing MDX queries and get back an R data frame, or build simple MDX statements by defining cube axes and slicers in R code. 
   
   For more information, see [Using Data from OLAP Cubes in R](../../advanced-analytics/r-services/using-data-from-olap-cubes-in-r.md).
   

  
## Features in SQL Server 2016 R Services and SQL Server vNext  
  
- The **RevoScaleR** package for fast, parallelizable machine learning with R.

-   Supports both SQL logins and integrated Windows authentication.  
    
-   Significant performance improvements, including optimization of the SQL Satellite processes, which connect R and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], support for paging of data to enable high-volume data usage, and streaming to enable fast processing of  billions of rows. 
  
-   Use SQL Server resource pools to manage memory used by R processes. For more information see [CREATE EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-resource-pool-transact-sql.md).  
  

### Tools and setup

-   Easy setup of all components. The SQL Server setup wizard can install either **SQL Server R Services (In-Database)** or **Microsoft R Server (Standalone)**.   When you run the setup wizard, choose R Services if you are setting up a SQL Server instance, and choose R Server (Standalone) if you are setting up a data science workstation.   For more information on  setup options, see [Set up SQL Server R Services &#40;In-Database&#41;](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md) or [Create a Standalone R Server](../../advanced-analytics/r-services/create-a-standalone-r-server.md).  

-   If you don't need to use data in SQL Server, consider [!INCLUDE[rsql_platform](../../includes/rsql-platform-md.md)], which runs on a wide variety of platforms, and provides enterprise scale and performance to the popular open source R language. [!INCLUDE[rsql_platform](../../includes/rsql-platform-md.md)]. For details, see [R Server &#40;Standalone&#41;](../../advanced-analytics/r-services/r-server-standalone.md)  or [Introducing R Server](https://msdn.microsoft.com/microsoft-r/rserver) on MSDN.

- To upgrade your SQL Server 2016 instance to use Microsoft R Server 9.0.1, use the [SqlBindR.exe utility](https://msdn.microsoft.com/library/mt791781.aspx).  

- [Microsoft R Client](https://msdn.microsoft.com/microsoft-r/r-client-install) is a free R environment that includes all the tools and libraries you'll need to building R solutions that run on either R Services or R Server.  

-   R Tools for Visual Studio is a free plug-in for Visual Studio with rich support for R, including standard R interactive and variables windows, Intellisense for R functions, debugging, and R Markdown, complete with export to Word and HTML.  For more information, see [R Tools for Visual Studio](https://www.visualstudio.com/vs/rtvs/).  

## Learn More
  
-  Resources are available for both data scientists who want to learn about SQL Server integration, and SQL developers who want to create R solutions using T-SQL and the familiar environment of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. 
   + [SQL Server R Services Tutorials](https://msdn.microsoft.com/library/mt591993.aspx)
   + [Free ebook: Data Science with SQL Server 2016](https://mva.microsoft.com/ebooks/)
 
+ If you need ready-made solutions, the [machine learning templates](https://blogs.technet.microsoft.com/machinelearning/2016/03/23/machine-learning-templates-with-sql-server-2016-r-services/) from the Microsoft data science team demonstrate practical solutions for common analytical tasks, including predictive maintenance and churn prevention.
 

  
## See Also  
[What's New in SQL Server vNext](../../sql-server/what-s-new-in-sql-server-vnext.md)
  