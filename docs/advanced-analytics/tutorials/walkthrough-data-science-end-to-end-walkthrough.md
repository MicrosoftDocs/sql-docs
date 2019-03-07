---
title: Tutorial for data scientists using R language - SQL Server Machine Learning
description: Tutorial showing how to create an end-to-end R solution for in-database analytics.
ms.prod: sql
ms.technology: machine-learning

ms.date: 11/26/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Tutorial: SQL development for R data scientists
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In this tutorial for data scientists, learn how to build end-to-end solution for predictive modeling based on R feature support in either SQL Server 2016 or SQL Server 2017. This tutorial uses a [NYCTaxi_sample](demo-data-nyctaxi-in-sql.md) database on SQL Server. 

You use a combination of R code, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data, and custom SQL functions to build a classification model that indicates the probability that the driver might get a tip on a particular taxi trip. You also deploy your R model to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and use server data to generate scores based on the model.

This example can be extended to all kinds of real-life problems, such as predicting customer responses to sales campaigns, or predicting spending or attendance at events. Because the model can be invoked from a stored procedure, you can easily embed it in an application.

Because the walkthrough is designed to introduce R developers to [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], R is used wherever possible. However, this does not mean that R is necessarily the best tool for each task. In many cases, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might provide better performance, particularly for tasks such as data aggregation and feature engineering.  Such tasks can particularly benefit from new features in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], such as memory optimized columnstore indexes. We try to point out possible optimizations along the way.

## Prerequisites

+ [SQL Server 2017 Machine Learning Services with R integration](../install/sql-machine-learning-services-windows-install.md#verify-installation) or [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md)

+ [Database permissions](../security/user-permission.md) and a SQL Server database user login

+ [SQL Server Management Studio](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms)

+ [NYC Taxi demo database](demo-data-nyctaxi-in-sql.md)

+ An R IDE such as RStudio or the built-in RGUI tool included with R

We recommend that you do this walkthrough on a client workstation. You must be able to connect, on the same network, to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer with SQL Server and the R language enabled. For instructions on workstation configuration, see [Set up a data science client for R development](../r/set-up-a-data-science-client.md).

Alternatively, you can run the walkthrough on a computer that has both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and an R development environment, but we don't recommend this configuration for a production environment. If you need to put client and server on the same computer, be sure to install a second set of Microsoft R libraries for sending R script from a "remote" client. Do not use the R libraries that are installed in the program files of the SQL Server instance. Specifically, if you are using one computer, you need the RevoScaleR library in both of these locations to support client and server operations.

+ C:\Program Files\Microsoft\R Client\R_SERVER\library\RevoScaleR 
+ C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library\RevoScaleR

<a name="add-packages"></a>

## Additional R packages

This walkthrough requires several R libraries that are not installed by default as part of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]. You must install the packages both on the client where you develop the solution, and on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer where you deploy the solution.

### On a client workstation

In your R environment, copy the following lines and execute the code in a Console window (Rgui or an IDE). Some packages also install required packages. In all, about 32 packages are installed. You must have an internet connection to complete this step.
    
  ```R
  # Install required R libraries, if they are not already installed.
  if (!('ggmap' %in% rownames(installed.packages()))){install.packages('ggmap')}
  if (!('mapproj' %in% rownames(installed.packages()))){install.packages('mapproj')}
  if (!('ROCR' %in% rownames(installed.packages()))){install.packages('ROCR')}
  if (!('RODBC' %in% rownames(installed.packages()))){install.packages('RODBC')}
  ```

### On the server

You have several options for installing packages on SQL Server. For example, SQL Server provides [R package management](../r/install-additional-r-packages-on-sql-server.md) feature that lets database administrators create a package repository and assign user the rights to install their own packages. However, if you are an administrator on the computer, you can install new packages using R, as long as you install to the correct library.

> [!NOTE]
> On the server, **do not** install to a user library even if prompted. If you install to a user library, the SQL Server instance cannot find or run the packages. For more information, see [Installing new R Packages on SQL Server](../r/install-additional-r-packages-on-sql-server.md).

1. On the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer, open RGui.exe **as an administrator**.  If you have installed SQL Server R Services using the defaults, Rgui.exe can be found in C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\bin\x64).

2. At an R prompt, run the following R commands:
  
  ```R
  install.packages("ggmap", lib=grep("Program Files", .libPaths(), value=TRUE)[1])
  install.packages("mapproj", lib=grep("Program Files", .libPaths(), value=TRUE)[1])
  install.packages("ROCR", lib=grep("Program Files", .libPaths(), value=TRUE)[1])
  install.packages("RODBC", lib=grep("Program Files", .libPaths(), value=TRUE)[1])
  ```
  This example uses the R grep function to search the vector of available paths and find the path that includes "Program Files". For more information, see [https://www.rdocumentation.org/packages/base/functions/grep](https://www.rdocumentation.org/packages/base/functions/grep).

  If you think the packages are already installed, check the list of installed packages by running `installed.packages()`.

## Next steps

> [!div class="nextstepaction"]
> [Explore and summarize the data](walkthrough-view-and-summarize-data-using-r.md)