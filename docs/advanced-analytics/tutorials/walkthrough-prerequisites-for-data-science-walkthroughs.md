---
title: "Prerequisites for Data Science Walkthroughs (SQL Server R Services) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "11/22/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
dev_langs: 
  - "R"
ms.assetid: 0b0582b8-8843-4787-94a8-2e28bdc04fb2
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Prerequisites for Data Science Walkthroughs (SQL Server R Services)
We recommend that you do the walkthroughs on an R workstation that can connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer on the same network. You can also run the walkthrough on a computer that has both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and an R development environment. 
  
  
## Install SQL Server 2016 R Services (In-Database)  
You must have access to an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]  with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] installed. For more information about how to set up [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], see [Set up  SQL Server R Services (In-Database](https://msdn.microsoft.com/library/mt696069.aspx).  
  
  
> [!IMPORTANT]  
> Be sure to use [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] or later. Previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] do not support integration with R. However, you can use older SQL databases as an ODBC data source.  
  
## Install an R Development Environment  
To complete this walkthrough on your computer, you will need an R development environment, or any other command line tool that can run R commands.    
  
- **R Tools for Visual Studio** is a free plug-in that provides Intellisense, debugging, and support for Microsoft R Server and SQL Server R Services. To download, see [R Tools for Visual Studio](https://www.visualstudio.com/features/rtvs-vs.aspx).  
    
- **Microsoft R Client** is a lightweight development tool that supports development in R using the ScaleR packages. To get it, see [Get Started with Microsoft R Client](https://msdn.microsoft.com/microsoft-r/r-client-get-started).
  
- **RStudio** is one of the more popular environments for R development. For more information, see [https://www.rstudio.com/products/RStudio/](https://www.rstudio.com/products/RStudio/).  
  
    However, you cannot complete this tutorial using a generic installation of RStudio or other environment; you must also install the R packages and connectivity libraries for Microsoft R Open. For more information, see [Set Up a Data Science Client](https://msdn.microsoft.com/library/mt696067.aspx).  

- R tools (R.exe, RTerm.exe, RScripts.exe) are installed by default when you install [!INCLUDE[rsql_rro-noversion](../../includes/rsql-rro-noversion-md.md)]. If you do not wish to install an IDE you can use these tools.  
  
  
## Get Permissions to Connect to SQL Server  
In this walkthrough, you will connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to run scripts and upload data. To do this, you must have a valid login on the database server.  You can use either a SQL login or integrated Windows authentication. Ask the database administrator to create an account for you on the server with the following privileges on the database where you will be using R:  
  
-   Create database, tables, functions, and stored procedures    
-   Insert data into tables  
  
  
## Start the Walkthrough  
[Lesson 1: Prepare the Data &#40;Data Science End-to-End Walkthrough&#41;](../../advanced-analytics/r-services/lesson-1-prepare-the-data-data-science-end-to-end-walkthrough.md)  
  
  
  

