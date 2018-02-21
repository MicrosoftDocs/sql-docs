---
title: "Default package libraries for machine learning on SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "02/19/2018"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "R"
ms.assetid: 4d426cf6-a658-4d9d-bfca-4cdfc8f1567f
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "On Demand"
---
# Default package libraries for machine learning on SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes the default libraries for R and Python that are installed with SQL Server.  

## What is the default instance library

When you install machine learning with SQL Server, a package library is created at the instance level for each language that you install. This article describes these instance libraries, provides their locations, and explains how you can determine which packages and which version of R or Python are installed in each instance library.

> [!IMPORTANT]
> These features are installed only if you have installed and enable machine learning features. For more information, see SETUP LINK>

## Where is the default instance library

The default instance library is installed to a secured folder that is registered with SQL Server and can be modified only by a computer administrator. The security on this library means that only approved packages can be installed and run on the server.

Even when you connect to the server from a remote client, R or Python code that you want to run in the server compute context must use packages installed in the instance library.

### SQL Server

|Version | Instance name|Default path|
|------|------|------|
| SQL Server 2016 |default instance|`C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\library`|
| SQL Server 2016 |named instance |`C:\Program Files\Microsoft SQL Server\MSSQL13.<instance_name>\R_SERVICES\library`|
| SQL Server 2017 with R|default instance |`C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library` |
| SQL Server 2017 with R|named instance|`C:\Program Files\Microsoft SQL Server\MSSQL14.MyNamedInstance\R_SERVICES\library` |
| SQL Server 2017 with Python |default instance |`C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\library` |
| SQL Server 2017 with Python|named instance|`C:\Program Files\Microsoft SQL Server\MSSQL14.<instance_name>\PYTHON_SERVICES\library` |

### R Server (standalone) or Machine Learning Server (Standalone)

This table lists the default paths of the binaries when the standalone server is installed using SQL Server setup. 

|Version| Installation|Default path|
|------|------|------|
| SQL Server 2016|R Server (Standalone)| |`C:\Program Files\Microsoft SQL Server\130\R_SERVER`|
|SQL Server 2017|Machine Learning Server, with R |`C:\Program Files\Microsoft SQL Server\130\R_SERVER`|
|SQL Server 2017|Machine Learning Server, with Python |`C:\Program Files\Microsoft SQL Server\130\PYTHON_SERVER`|

If you install Microsoft R Server or Machine Learning server using the separate Windows installer, the default paths are different: typically, something like `C:\Program Files\Microsoft\R Server\R_SERVER`. For more information, see:
 
+ [Install Machine Learning Server for Windows](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install)
+ [Install R Server 9.1 for Windows](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows)

## What is included in a default installation

This section provides a summary of the R or Python features that are installed by default.

### Default R installation for SQL Server

By default the R **base** packages are installed. Base packages include core functionality provided by packages such as `stats` and `utils`.

A base installation of R also includes numerous sample datasets, and standard R tools such as RGui (a lightweight interactive editor) and RTerm (a command line tool).

Installation of R in SQL Server 2016 or SQL Server 2017 also includes the **RevoScaleR** package, and related enhanced packages and providers, which supports remote compute contexts, streaming, parallel execution of rx function, and many other features.

To upgrade the RevoScaleR package, either use binding to upgrade just the machine learning components, or patch or upgrade your instance to a newer version of SQL Server.

+ For an overview of the enhanced R features, see [About Machine Learning Server](https://docs.microsoft.com/machine-learning-server/what-is-microsoft-r-server)

+ To download the RevoScaleR libraries onto a client computer, install [Microsoft R Client](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client)

### Default Python installation for SQL Server

[!INCLUDE[tsql-appliesto-ss2017-xxxx-xxxx-xxx-md(../../includes/tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md)]

If you select the machine learning features and the Python language option, an Anaconda distribution is installed. The exact version depends on the version of SQL Server you have installed and whether you have upgraded the instance using Machine Learning Server installer.

|Release| Anaconda version| Other changes|
|------|------|------|
| SQL Server 2017 RTM| 3.5.2| New: revoscalepy|
| update via Machine Learning Server 9.2.1 Sept 2017| Anaconda 4.2| updates to revoscalepy 
| SQL Server 2017 CU3| Anaconda 4.2| updates to revoscalepy |

In addition to Python code libraries, the standard installation includes sample data, unit tests, and sample scripts.

## Restrictions and known issues

Any solution that runs in SQL Server can use **only** packages that are installed in the default library associated with the instance.

If you use binding to upgrade the R components in an instance, the path to the instance library can change. Be sure to verify the path of the library currently used by SQL Server.

## Administrative permissions required for package installation

The permissions required for package installation have changed between SQL Server 2016 and SQL Server 2017.

+ In SQL Server 2016, administrative access is required for installation of new R packages.

+ In SQL Server 2017, you can continue to install packages as an administrator for both R and Python, and this is probably the easiest method.

    However, if you enable package management on the instance, you can also use database roles and DDL statements to install R packages at the database level. This feature will be extended to support Python in later releases.

    After this feature has been enabled, a database administrator can grant users the ability to install their own packages on a per database basis. You can also use R commands from a remote client to install packages into the instance library. This feature requires some additional configuration on the instance. for more information, see [Enable package management using DDLs](r-package-how-to-enable-or-disable.md).

### User libraries are not supported

Often users who cannot install a package to a secured location will install a package to a user library. However, this is not possible in the SQL Server environment.

Often file system access is restricted on the server, but even if you have admin rights and access to a user document folder on the server, the external script runtime that executes in SQL Server cannot access any packages installed outside the default instance library.

For tips on how to resolve problems related to user libraries, see [Package installed in user libraries](packages-installed-in-user-libraries.md).