---
title: "Prerequisites for Data Science Walkthroughs (SQL Server R Services) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/28/20176"
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
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Prerequisites for Data Science Walkthroughs

We recommend that you do most of these walkthroughs on an R workstation that can connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer on the same network. You can also run the walkthrough on a computer that has both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and an R development environment.

Some tutorials contain all code and are intended to be run in SQL Server Management Studio or other SQL development environment. For these, you still need to install and enable the machine learning services on the SQL Server instance where you will run the R or Python code.

## Install SQL Server Machine Learning Services (In-Database)

Requires one of the following environments:

+ SQL Server 2016 with R Services (In-Database)
+ sQL Server 2017 Machine Learning Services

For detailed information about setup, see [Set up  SQL Server R Services (In-Database](https://msdn.microsoft.com/library/mt696069.aspx).

> [!IMPORTANT]
> Older versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] do not support integration with R. However, you can use older SQL databases as an ODBC data source.

## Install an R development environment (optional)

Some of these tutorials require that you run R or Python code from a client computer. The easiest way to work with R code is to install an R development environment, such as R Tools for Visual Studio; however, you can use any tool that can send R commands to a remote SQL Server.

- **R Tools for Visual Studio** is a free plug-in that provides Intellisense, debugging, and support for Microsoft R Server and SQL Server R Services. To download, see [R Tools for Visual Studio](https://www.visualstudio.com/features/rtvs-vs.aspx).  
    
- **Microsoft R Client** is a lightweight development tool that supports development in R and includes the ScaleR packages. To get it, see [Get Started with Microsoft R Client](https://msdn.microsoft.com/microsoft-r/r-client-get-started).
  
- **RStudio** is one of the more popular environments for R development. For more information, see [https://www.rstudio.com/products/RStudio/](https://www.rstudio.com/products/RStudio/).  
  
    You cannot complete this tutorial using a generic installation of RStudio or any other environment; you must also install the R packages and connectivity libraries for Microsoft R Open. For more information, see [Set Up a Data Science Client](https://msdn.microsoft.com/library/mt696067.aspx).  

- R tools (R.exe, RTerm.exe, RScripts.exe) are installed by default when you install [!INCLUDE[rsql_rro-noversion](../../includes/rsql-rro-noversion-md.md)]. If you do not wish to install an IDE you can use these tools.  

## Get permissions to connect to SQL Server

You will need to connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to run scripts and upload data. To do this, you must have a valid login on the database server.  You can use either a SQL login or integrated Windows authentication.

Ask the database administrator to create an account for you on the server with the following privileges on the database where you will be using R:

- Create database, tables, functions, and stored procedures
- Insert data into tables
- EXECUTE ANY EXTERNAL SCRIPT (database level permission)

## Next step

[Prepare the Data](/walkthrough-prepare-the-data.md)
