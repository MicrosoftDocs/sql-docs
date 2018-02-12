---
title: "R packages installed with SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "01/04/2018"
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
# R packages installed with SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes the R packages that are installed with SQL Server if you install and enable machine learning features. This article also describes how to manage and view existing packages, or add new packages to a SQL Server instance.

**Applies to:** SQL Server 2017 Machine Learning Services (In-Database), SQL Server 2016 R Services (In-Database)

## What is the instance library and where is it?

Any R solution that runs in SQL Server can use only packages that are installed in the default R library associated with the instance. When you install R features in SQL Server, the R package library is located under the instance folder.

+ Default instance *MSSQLSERVER* 

    SQL Server 2017: `C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library` 
    
    SQL Server 2016: `C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\library`

+ Named instance *MyNamedInstance* 

    SQL Server 2017: `C:\Program Files\Microsoft SQL Server\MSSQL14.MyNamedInstance\R_SERVICES\library` 
    
    SQL Server 2016: `C:\Program Files\Microsoft SQL Server\MSSQL13.MyNamedInstance\R_SERVICES\library`

You can run the following statement to verify the default library for the current instance of R.

```sql
EXECUTE sp_execute_external_script  @language = N'R'
, @script = N'OutputDataSet <- data.frame(.libPaths());'
WITH RESULT SETS (([DefaultLibraryName] VARCHAR(MAX) NOT NULL));
GO
```

Alternatiely, you can use the new [rxSqlLibPaths](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxsqllibpaths) function, if executing sp\_execute\_external\_script directly on the target computer. The function cannot return library paths for remote connections.

```sql
EXEC sp_execute_external_script
  @language =N'R',
  @script=N'
  sql_r_path <- rxSqlLibPaths("local")
  print(sql_r_path)
```

> [!NOTE]
> If you use binding to upgrade the R components in an instance, the path to the instance library can change. Be sure to verify which library is being used by SQL Server.

## R packages installed with SQL Server

By default the R **base** packages are installed. Base packages include core functionality provided by packages such as `stats` and `utils`.

Installation of R in SQL Server 2016 or SQL Server 2017 always includes the **RevoScaleR** package, and related enhanced packages and providers, which supports remote compute contexts, streaming, parallel execution of rx function, and many other features. To upgrade the RevoScaleR package, either use binding to upgrade just the machine learning components, or patch or upgrade your instance to a newer version of SQL Server.

+ For an overview of the enhanced R features, see [About Machine Learning Server](https://docs.microsoft.com/machine-learning-server/what-is-microsoft-r-server)

+ To download the RevoScaleR libraries onto a client computer, install [Microsoft R Client](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client)

## Permissions required for installing R packages

In SQL Server 2016, an administrator had to install new R packages on an instance-wide basis. 

SQL Server 2017 introduced new features for package installation and management:

+ You can use R commands from a remote client to install packages using either private or shared scope. This feature requires either [Microsoft R Server](https://docs.microsoft.com/machine-learning-server/install/r-server-install) or  [Machine Learning Server](https://docs.microsoft.com/machine-learning-server/what-is-machine-learning-server), as well as dbo privileges on the instance.
+ New database features have been added to support package management by database administrators without using T-SQL. In future, these features provide DBAs with the ability to delegate most facets of package management to privileged users.

This section describes the permissions required to install and manage packages per version.

+ SQL Server 2016 R Services (In-Database)

    To install a new R package on a computer that is running [!INCLUDE[ssCurrent](..\..\includes\sscurrent-md.md)], you must have administrative rights to the computer. It is the task of the database administrator or other administrator on the server to ensure that all required packages are installed on the [!INCLUDE[ssNoVersion_md](..\..\includes\ssnoversion-md.md)] instance.

    If you do not have administrative privileges on the computer that hosts the [!INCLUDE[ssNoVersion_md](..\..\includes\ssnoversion-md.md)] instance, you can provide to the administrator information about how to install R packages, and provide access to a secure package repository where packages requested by users can be obtained.

+ SQL Server 2017 Machine Learning Services

    If you are an administrator on the SQL Server instance, you can install new packages at will. Just be sure to use the default library that is associated with the instance. Packages installed to other locations cannot run when called from a stored procedure. Any R code that runs using the SQL Server as a compute context also requires that packages be available in the instance library.

    This release also includes some new features intended to support easier package management by DBAs in a later release. For now, we recommend that you continue to install R packages on an instance-wide basis.

+ R Server (Standalone)

    You need administrative rights to the computer to install new R packages.

+ Other client environments

    If you are installing a new R package on a computer that is being used as an R workstation and the computer does **not** have an instance of [!INCLUDE[ssNoVersion_md](..\..\includes\ssnoversion-md.md)] installed, you still need administrative rights to the computer to install the package. After you have installed the package, you can run it locally.

## Managing or viewing installed packages

There are multiple ways that you can get a complete list of the packages currently installed. For more information, see [Determine which packages are installed on SQL Server](determine-which-packages-are-installed-on-sql-server.md).
