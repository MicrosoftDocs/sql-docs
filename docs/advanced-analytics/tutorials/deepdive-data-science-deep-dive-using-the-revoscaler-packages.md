---
title: RevoScaleR function deep-dive tutorial- SQL Server Machine Learning
description: In this tutorial, learn how to call RevoScaleR functions using SQL Server Machine Learning R integration.
ms.prod: sql
ms.technology: machine-learning

ms.date: 11/27/2018  
ms.topic: tutorial
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Tutorial: Use RevoScaleR R functions with SQL Server data
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

[RevoScaleR](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) is a Microsoft R package providing distributed and parallel processing for data science and machine learning workloads. For R development in SQL Server, **RevoScaleR** is one of the core built-in packages, with functions for creating data source objects, setting a compute context, managing packages, and most importantly: working with data end-to-end, from import to visualization and analysis. Machine Learning algorithms in SQL Server have a dependency on **RevoScaleR** data sources. Given the importance of **RevoScaleR**, knowing when and how to call its functions is an essential skill. 

In this multi-part tutorial, you are introduced to a range of **RevoScaleR** functions for tasks associated with data science. In the process, you will learn how to create a remote compute context, move data between local and remote compute contexts, and execute R code on a remote SQL Server. You also learn how to analyze and plot data both locally and on the remote server, and how to create and deploy models.

## Prerequisites

+ [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) with the R feature, or [SQL Server 2016 R Services (in-Database)](../install/sql-r-services-windows-install.md)
  
+ [Database permissions](../security/user-permission.md) and a SQL Server database user login

+ [SQL Server Management Studio](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms)

+ An IDE such as RStudio or the built-in RGUI tool included with R

To switch back and forth between local and remote compute contexts, you need two systems. Local is typically a development workstation with sufficent power for data science workloads. Remote in this case is SQL Server 2017 or SQL Server 2016 with the R feature enabled. 

Switching compute contexts is predicated on having the same-version **RevoScaleR** on both local and remote systems. On a local workstation, you can get the **RevoScaleR** packages and related providers by installing Microsoft R Client.

If you need to put client and server on the same computer, be sure to install a second set of Microsoft R libraries for sending R script from a "remote" client. Do not use the R libraries that are installed in the program files of the SQL Server instance. Specifically, if you are using one computer, you need the **RevoScaleR** library in both of these locations to support client and server operations.

+ C:\Program Files\Microsoft\R Client\R_SERVER\library\RevoScaleR 
+ C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library\RevoScaleR

For instructions on client configuration, see [Set up a data science client for R development](../r/set-up-a-data-science-client.md).


## R development tools

R developers typically use IDEs for writing and debugging R code. Here are some suggestions:

- **R Tools for Visual Studio** (RTVS) is a free plug-in that provides Intellisense, debugging, and support for Microsoft R. You can use it with both R Server and SQL Server Machine Learning Services. To download, see [R Tools for Visual Studio](https://www.visualstudio.com/vs/rtvs/).

- **RStudio** is one of the more popular environments for R development. For more information, see [https://www.rstudio.com/products/RStudio/](https://www.rstudio.com/products/RStudio/).

- Basic R tools (R.exe, RTerm.exe, RScripts.exe) are also installed by default when you install R in SQL Server or R Client. If you do not wish to install an IDE, you can use built-in R tools to execute the code in this tutorial.

Recall that **RevoScaleR** is required on both local and remote computers. You cannot complete this tutorial using a generic installation of RStudio or other environment that's missing the Microsoft R libraries. For more information, see [Set Up a Data Science Client](../r/set-up-a-data-science-client.md).

## Summary of tasks

+ Data is initially obtained from CSV files or XDF files. You import the data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the functions in the **RevoScaleR** package.
+ Model training and scoring is performed using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] compute context. 
+ Use **RevoScaleR** functions to create new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables to save your scoring results.
+ Create plots both on the server and in the local compute context.
+ Train a model on data in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, running R in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.
+ Extract a subset of data and save it as an XDF file for re-use in analysis on your local workstation.
+ Get new data for scoring, by opening an ODBC connection to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Scoring is done on the local workstation.
+ Create a custom R function and run it in the server compute context to perform a simulation.

## Next steps

> [!div class="nextstepaction"]
> [Lesson 1: Create database and permissions](deepdive-work-with-sql-server-data-using-r.md)