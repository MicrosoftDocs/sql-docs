---
title: End-to-end data science walkthrough for R and SQL Server | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# End-to-end data science walkthrough for R and SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In this walkthrough, you develop an end-to-end solution for predictive modeling based on R feature support in either SQL Server 2016 or SQL Server 2017.

This walkthrough is based on a popular set of public data, the New York City taxi dataset. You use a combination of R code, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data, and custom SQL functions to build a classification model that indicates the probability that the driver might get a tip on a particular taxi trip. You also deploy your R model to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and use server data to generate scores based on the model.

This example can be extended to all kinds of real-life problems, such as predicting customer responses to sales campaigns, or predicting spending or attendance at events. Because the model can be invoked from a stored procedure, you can easily embed it in an application.

Because the walkthrough is designed to introduce R developers to [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], R is used wherever possible. However, this does not mean that R is necessarily the best tool for each task. In many cases, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might provide better performance, particularly for tasks such as data aggregation and feature engineering.  Such tasks can particularly benefit from new features in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], such as memory optimized columnstore indexes. We try to point out possible optimizations along the way.

## Target audience

This walkthrough is intended for R or SQL developers. It provides an introduction into how R can be integrated into enterprise workflows using [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)].  You should be familiar with database operations, such as creating databases and tables, importing data, and running queries.

+ All SQL and R scripts are included.
+ You might need to modify strings in the scripts, to run in your environment. You can do this with any code editor, such as [Visual Studio Code](https://code.visualstudio.com/Download).

## Prerequisites

We recommend that you do this walkthrough on a laptop or other computer that has the Microsoft R libraries installed. You must be able to connect, on the same network, to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer with SQL Server and the R language enabled.

You can run the walkthrough on a computer that has both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and an R development environment but we don't recommend this configuration for a production environment.

If you want to run R commands from a remote computer, such as a laptop or other networked computer, you must install the Microsoft R Open libraries. You can install either Microsoft R Client or Microsoft R Server. The remote computer must be able to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.

If you need to put client and server on the same computer, be sure to install a separate set of Microsoft R libraries for use in sending R script from a "remote" client. Do not use the R libraries that are installed for use by the SQL Server instance for this purpose.

## Add R to SQL Server

You must have access to an instance of SQL Server with support for R installed. This walkthrough was originally developed for SQL Server 2016 and tested on 2017, so you should be able to use either of the following SQL Server versions. (There are some small differences in the RevoScaleR functions between the releases.)

+ [Install SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md)
+ [Install SQL Server 2016 R Services](../install/sql-r-services-windows-install.md).

## Install an R development environment

For this walkthrough, we recommend that you use an R development environment. Here are some suggestions:

- **R Tools for Visual Studio** (RTVS) is a free plug-in that provides Intellisense, debugging, and support for Microsoft R. YOu can use it with both R Server and SQL Server Machine Learning Services. To download, see [R Tools for Visual Studio](https://www.visualstudio.com/vs/rtvs/).

- **Microsoft R Client** is a lightweight development tool that supports development in R using the RevoScaleR package. To get it, see [Get Started with Microsoft R Client](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client).

- **RStudio** is one of the more popular environments for R development. For more information, see [https://www.rstudio.com/products/RStudio/](https://www.rstudio.com/products/RStudio/).

    You cannot complete this tutorial using a generic installation of RStudio or other environment; you must also install the R packages and connectivity libraries for Microsoft R Open. For more information, see [Set Up a Data Science Client](../r/set-up-a-data-science-client.md).

- Basic R tools (R.exe, RTerm.exe, RScripts.exe) are also installed by default when you install R in SQL Server or R Client. If you do not wish to install an IDE, you can use these tools.

## Get permissions on the SQL Server instance and database

To connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to run scripts and upload data, you must have a valid login on the database server.  You can use either a SQL login or integrated Windows authentication. Ask the database administrator to configure the following permissions for the account, in the database where you use R:

- Create database, tables, functions, and stored procedures
- Write data into tables
- Ability to run R script (`GRANT EXECUTE ANY EXTERNAL SCRIPT to <user>`)

For this walkthrough, we have used the SQL login **RTestUser**. We generally recommend that you use Windows integrated authentication, but using the SQL login is simpler for some demo purposes.

## Next steps

[Prepare the data using PowerShell](walkthrough-prepare-the-data.md)
