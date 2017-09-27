---
title: "Prerequisites for the data science walkthrough for SQL Server and R | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "08/23/2017"
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
# Prerequisites for the data science walkthrough for SQL Server and R

We recommend that you do this walkthrough on a laptop or other computer that has the Microsoft R libraries installed. You must be able to connect, on the same network, to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer with machine learning services and the R language enabled.

You can run the walkthrough on a computer that has both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and an R development environment but we don't recommend this configuration for a production environment.

## Install machine learning for SQL Server

You must have access to an instance of SQL Server with support for R installed, using either of the following:

+ Machine Learning Services (In-Database) for SQL Server 2017
+ SQL Server 2016 R Services

For more information, see [Set up  SQL Server R Services (In-Database](../r/set-up-sql-server-r-services-in-database.md).

> [!IMPORTANT]
> Be sure to use [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] or later. Previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] do not support integration with R. However, you can use older SQL databases as an ODBC data source.

## Install an R development environment

For this walkthrough, we recommend that you use an R development environment. Here are some suggestions:

- **R Tools for Visual Studio** (RTVS) is a free plug-in that provides Intellisense, debugging, and support for Microsoft R. YOu can use it with both R Server and SQL Server Machine Learning Services. To download, see [R Tools for Visual Studio](https://www.visualstudio.com/features/rtvs-vs.aspx).

- **Microsoft R Client** is a lightweight development tool that supports development in R using the ScaleR packages. To get it, see [Get Started with Microsoft R Client](https://msdn.microsoft.com/microsoft-r/r-client-get-started).

- **RStudio** is one of the more popular environments for R development. For more information, see [https://www.rstudio.com/products/RStudio/](https://www.rstudio.com/products/RStudio/).

    You cannot complete this tutorial using a generic installation of RStudio or other environment; you must also install the R packages and connectivity libraries for Microsoft R Open. For more information, see [Set Up a Data Science Client](../r/set-up-a-data-science-client.md).

- Basic R tools (R.exe, RTerm.exe, RScripts.exe) are also installed by default when you install [!INCLUDE[rsql_rro-noversion](../../includes/rsql-rro-noversion-md.md)]. If you do not wish to install an IDE, you can use these tools.

## Get permissions on the SQL Server instance and database

To connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to run scripts and upload data, you must have a valid login on the database server.  You can use either a SQL login or integrated Windows authentication. Ask the database administrator to configure the following permissions for the account, in the database where you use R:

- Create database, tables, functions, and stored procedures
- Write data into tables
- Ability to run R script (`GRANT EXECUTE ANY EXTERNAL SCRIPT to <user>`)

For this walkthrough, we have used the SQL login **RTestUser**. We generally recommend that you use Windows integrated authentication, but using the SQL login is simpler for some demo purposes.

## Change list

+ This sample was originally developed using SQL Server 2016 R Services. However, breaking changes were introduced in the Microsoft R components for 2016 SP1. Specifically, the _varsToDrop_ and _varsToKeep_ parameters were no longer supported for SQL Server data sources. Therefre, if you downloaded a version of the tutorial prior to SP1, it will no longer work with post-SP1 builds.

+ The current version of the sample has been tested using a pre-release build of SQL Server 2017 Machine Learning Services (RC1 and RC2). In general, almost all steps should run without modification between 2016 SP1 and 2017.

## Next lesson

[Prepare the data using PowerShell](/walkthrough-prepare-the-data.md)
