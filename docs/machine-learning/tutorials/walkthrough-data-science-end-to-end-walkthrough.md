---
title: "R Tutorial: Develop a Predictive Model in SQL Server"
description: Learn how to build end-to-end solution for predictive modeling based on R feature support in SQL Server 2017.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/13/2026
ms.service: sql
ms.subservice: machine-learning-services
ms.topic: tutorial
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017"
---
# Tutorial: SQL Server development for R data scientists

[!INCLUDE [SQL Server 2017](../../includes/applies-to-version/sqlserver2017.md)]

In this tutorial for data scientists, learn how to build an end-to-end solution for predictive modeling based on R feature support in SQL Server 2017. This tutorial uses a [NYC Taxi demo](demo-data-nyctaxi-in-sql.md) database on SQL Server.

You use a combination of R code, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data, and custom SQL functions to build a classification model that indicates the probability that the driver might get a tip on a particular taxi trip. You also deploy your R model to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and use server data to generate scores based on the model.

You can extend this example to all kinds of real-life problems, such as predicting customer responses to sales campaigns, or predicting spending or attendance at events. Because you can invoke the model from a stored procedure, you can easily embed it in an application.

Because the walkthrough is designed to introduce R developers to [!INCLUDE [rsql_productname](../../includes/rsql-productname-md.md)], it's used wherever possible. However, this doesn't mean that R is necessarily the best tool for each task. In many cases, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] might provide better performance, particularly for tasks such as data aggregation and feature engineering. Such tasks can particularly benefit from new features in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], such as memory optimized columnstore indexes. We try to point out possible optimizations along the way.

## Prerequisites

- [SQL Server Machine Learning Services with R integration](../install/sql-machine-learning-services-windows-install.md#verify-installation).

- [Database permissions](../security/user-permission.md) granted to a database user mapped to a SQL Server login

- [SQL Server Management Studio](/ssms/install/install)

- [NYC Taxi demo database](demo-data-nyctaxi-in-sql.md)

- An R IDE such as RStudio or the built-in RGUI tool included with R

We recommend that you do this walkthrough on a client workstation. You must be able to connect, on the same network, to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] computer with SQL Server and the R language enabled. For instructions on workstation configuration, see [Set up a data science client for R development on SQL Server](../r/set-up-data-science-client.md).

Alternatively, you can run the walkthrough on a computer that has both [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and an R development environment, but we don't recommend this configuration for a production environment. If you need to put client and server on the same computer, be sure to install a second set of Microsoft R libraries for sending R script from a "remote" client. Don't use the R libraries that are installed in the program files of the SQL Server instance. Specifically, if you're using one computer, you need the RevoScaleR library in both of these locations to support client and server operations.

- C:\Program Files\Microsoft\R Client\R_SERVER\library\RevoScaleR
- C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library\RevoScaleR

<a id="add-packages"></a>

## Additional R packages

This walkthrough requires several R libraries that aren't installed by default as part of [!INCLUDE [rsql_productname](../../includes/rsql-productname-md.md)]. You must install the packages both on the client where you develop the solution, and on the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] computer where you deploy the solution.

### On a client workstation

In your R environment, copy the following lines and execute the code in a Console window (Rgui or an IDE). Some packages also install required packages. In all, about 32 packages are installed. You must have an internet connection to complete this step.

  ```r
  # Install required R libraries, if they are not already installed.
  if (!('ggmap' %in% rownames(installed.packages()))){install.packages('ggmap')}
  if (!('mapproj' %in% rownames(installed.packages()))){install.packages('mapproj')}
  if (!('ROCR' %in% rownames(installed.packages()))){install.packages('ROCR')}
  if (!('RODBC' %in% rownames(installed.packages()))){install.packages('RODBC')}
  ```

### On the server

You have several options for installing packages on SQL Server. For example, SQL Server provides the [Install R packages with sqlmlutils](../package-management/install-additional-r-packages-on-sql-server.md) feature that lets database administrators create a package repository and assign user rights to install their own packages. However, if you're an administrator on the computer, you can install new packages by using R, as long as you install to the correct library.

> [!NOTE]  
> On the server, *don't* install to a user library even if prompted. If you install to a user library, the SQL Server instance can't find or run the packages. For more information, see [Install R packages with sqlmlutils](../package-management/install-additional-r-packages-on-sql-server.md).

1. On the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] computer, open RGui.exe *as an administrator*. If you install SQL Server R Services with the default settings, you can find RGui.exe in `C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\bin\x64`.

1. At an R prompt, run the following R commands:

  ```r
  install.packages("ggmap", lib=grep("Program Files", .libPaths(), value=TRUE)[1])
  install.packages("mapproj", lib=grep("Program Files", .libPaths(), value=TRUE)[1])
  install.packages("ROCR", lib=grep("Program Files", .libPaths(), value=TRUE)[1])
  install.packages("RODBC", lib=grep("Program Files", .libPaths(), value=TRUE)[1])
  ```

  This example uses the R grep function to search the vector of available paths and find the path that includes "Program Files". For more information, see [RDocumentation for the base package](https://www.rdocumentation.org/packages/base/).

If you think the packages are already installed, check the list of installed packages by running `installed.packages()`.

## Next step

> [!div class="nextstepaction"]
> [View and summarize SQL Server data using R (walkthrough)](walkthrough-view-and-summarize-data-using-r.md)
