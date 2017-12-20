---
title: "R packages installed with SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "09/29/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "R"
ms.assetid: 4d426cf6-a658-4d9d-bfca-4cdfc8f1567f
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
ms.workload: "On Demand"
---
# R packages installed with SQL Server

This article describes the R packages that are installed with SQL Server, and provides information about how to manage and view existing packages.

This article also provides links to information about how to add new packages for use with SQL Server.

**Applies to:** SQL Server 2017 Machine Learning Services (In-Database), SQL Server 2016 R Services (In-Database)

## What is the instance library and where is it?

Any R solution that runs in SQL Server can use only packages that are installed in the default R library associated with the instance.

When you install R features in SQL Server, the R package library is located under the instance folder.

+ Default instance *MSSQLSERVER* 

    SQL Server 2017: `C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library` 
    
    SQL Server 2016: `C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\library`

+ Named instance *MyNamedInstance* 

    SQL Server 2017: `C:\Program Files\Microsoft SQL Server\MSSQL14.MyNamedInstance\R_SERVICES\library` 
    
    SQL Server 2016: `C:\Program Files\Microsoft SQL Server\MSSQL13.MyNamedInstance\R_SERVICES\library`

You can run the following statement to verify the default library for the current instance of R.

```SQL
EXECUTE sp_execute_external_script  @language = N'R'
, @script = N'OutputDataSet <- data.frame(.libPaths());'
WITH RESULT SETS (([DefaultLibraryName] VARCHAR(MAX) NOT NULL));
GO
```
## R packages installed with SQL Server

When you install the R language in SQL Server, by default the R **base** packages are installed. Base packages include core functionality provided by packages such as `stats` and `utils`.

Installation of R in SQL Server 2016 and SQL Server 2017 also includes the **RevoScaleR** package, and related enhanced packages and providers, which supports remote compute contexts, streaming, parallel execution of rx function, and many other features.

+ For an overview of the enhanced R features, see [About Machine Learning Server](https://docs.microsoft.com/r-server/what-is-microsoft-r-server)

+ To download the RevoScaleR libraries onto a client computer, install [Microsoft R Client](https://docs.microsoft.com/r-server/r-client/what-is-microsoft-r-client)

## Permissions required for installing R packages

In SQL Server 2016, an administrator had to install new R packages on an instance-wide basis. In SQL Server 2017, new database features were added that give the database administrator the ability to delegate package management to selected users.

This section describes the permissions required to install and manage packages per version.

+ SQL Server 2016 R Services (In-Database)

    To install a new R package on a computer that is running [!INCLUDE[ssCurrent](..\..\includes\sscurrent-md.md)], you must have administrative rights to the computer. It is the task of the database administrator or other administrator on the server to ensure that all required packages are installed on the [!INCLUDE[ssNoVersion_md](..\..\includes\ssnoversion-md.md)] instance.

    If you do not have administrative privileges on the computer that hosts the [!INCLUDE[ssNoVersion_md](..\..\includes\ssnoversion-md.md)] instance, you can provide to the administrator information about how to install R packages, and provide access to a secure package repository where packages requested by users can be obtained.

+ SQL Server 2017 Machine Learning Services

    This release includes package management functions that let database administrator delegate package installation rights to selected users. If this feature has been enabled, request that your database administrator add you to one of the new package roles. For more information, see [R package management for SQL Server](r-package-management-for-sql-server-r-services.md).

    If you are an administrator on the SQL Server instance, you can install new packages at will. Just be sure to use the default library that is associated with the instance. Packages installed to other locations cannot run when called from a stored procedure. Any R code that runs using the SQL Server as a compute context also requires that packages be available in the instance library.

+ R Server (Standalone)

    You need administrative rights to the computer to install new R packages.

+ Other client environments

    If you are installing a new R package on a computer that is being used as an R workstation and the computer does **not** have an instance of [!INCLUDE[ssNoVersion_md](..\..\includes\ssnoversion-md.md)] installed, you still need administrative rights to the computer to install the package. After you have installed the package, you can run it locally.

## Managing or viewing installed packages

There are multiple ways that you can get a complete list of the packages currently installed. For more information, see [Determine which packages are installed on SQL Server](determine-which-packages-are-installed-on-sql-server.md).
