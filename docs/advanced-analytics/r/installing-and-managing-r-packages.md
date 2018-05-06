---
title: Default R and Python package libraries in SQL Server R and SQL Server Machine Learning | Microsoft Docs
description: R and Python packages installed by SQL Server for R Services, R Server, Machine Learning Services (In-Database), and Machine Learning Server (Standalone)
ms.prod: sql
ms.technology: machine-learning

ms.date: 05/08/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Default R and Python packages in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article covers package libraries, location, and versions of R and Python packages installed with SQL Server. 

## Using the default instance library

When you install machine learning with SQL Server, a single package library is created at the instance level for each language that you install. 

All script or code that runs in-database on SQL Server must load functions from the instance library. SQL Server cannot access packages installed to other libraries. This applies to remote clients as well. When connecting to the server from a remote client, any R or Python code that you want to run in the server compute context can only use packages installed in the instance library.

To protect server assets, the default instance library is installed to a secured folder that is registered with SQL Server and can be modified only by a computer administrator. If you are not the owner of the computer, you might need to get permission from an administrator to install packages to this library. 

Even if you own the computer, you should consider the usefulness of any particular R or Python package in a server environment before adding the package to the instance library. Consider factors such as the size of package files and the need for multiple versions, as well as whether the package requires network or internet access.

### In-Database engine instance file paths

The following table shows the file location of R and Python for version and database engine instance combinations. 

|Version | Instance name|Default path|
|--------|--------------|------------|
| SQL Server 2016 |default instance| C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\library|
| SQL Server 2016 |named instance | C:\Program Files\Microsoft SQL Server\MSSQL13.<instance_name>\R_SERVICES\library|
| SQL Server 2017 with R|default instance | C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library |
| SQL Server 2017 with R|named instance| C:\Program Files\Microsoft SQL Server\MSSQL14.MyNamedInstance\R_SERVICES\library |
| SQL Server 2017 with Python |default instance | C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\library |
| SQL Server 2017 with Python|named instance| C:\Program Files\Microsoft SQL Server\MSSQL14.<instance_name>\PYTHON_SERVICES\library |

### Standalone server file paths 

The following table lists the default paths of the binaries when SQL Server 2016 R Server (Standalone) or SQL Server 2017 Machine Learning Server (Standalone) server is installed. 

|Version| Installation|Default path|
|------|------|------|
| SQL Server 2016|R Server (Standalone)| C:\Program Files\Microsoft SQL Server\130\R_SERVER|
|SQL Server 2017|Machine Learning Server, with R |C:\Program Files\Microsoft SQL Server\140\R_SERVER|
|SQL Server 2017|Machine Learning Server, with Python |C:\Program Files\Microsoft SQL Server\140\PYTHON_SERVER|

> [!Note]
> If you find other folders having similar subfolder names and files, you probably have a standalone installation of  Microsoft R Server or [Machine Learning server](https://docs.microsoft.com/machine-learning-server/). These server products have different installers and paths (namely, C:\Program Files\Microsoft\R Server\R_SERVER or C:\Program Files\Microsoft\ML SERVER\R_SERVER). For more information, see [Install Machine Learning Server for Windows](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install) or [Install R Server 9.1 for Windows](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows).

## What is included in a default installation

This section summarizes R and Python features included in a default installation.

### R components

Components include Microsoft's distribution of open-source R as [Microsoft R Open](https://mran.microsoft.com/open). Base R packages include core functionality such as **stats** and **utils**. You can run `installed.packages(priority = "base")` to return a package list. A base installation of R also includes numerous sample datasets, and standard R tools such as RGui (a lightweight interactive editor) and RTerm (an R command prompt).

Microsoft packages include [RevoScaleR](https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler) for remote compute contexts, streaming, parallel execution of rx functions for data import and transformation, modeling, visualization, and analysis. [MicrosoftML](https://docs.microsoft.com/r-server/r-reference/microsoftml/microsoftml-package) adds machine learning modeling in R. Other packages include [olapR](https://docs.microsoft.com/machine-learning-server/r-reference/olapr/olapr) for writing MDX statements in R, and [sqlrutils](https://docs.microsoft.com/machine-learning-server/r-reference/sqlrutils/sqlrutils) for including R script in stored procedures.


|Release             | R version       | Microsoft packages    |
|--------------------|-----------------|-----------------------|
| SQL Server 2016 R Services | 3.2.2   | RevoScaleR, sqlrutil  |
| SQL Server 2017 Machine Learning Services| 3.4.3 | RevoScaleR, MicrosoftML, olapR, sqlrutil|

You can add packages and pre-installed models to SQL Server 2016 R Services by binding to the Modern Lifecycle Support policy. Binding changes the servicing model. By default, after an initial installation, R packages are refreshed through service packs and cumulative updates. Additional packages and full version upgrades of core R components are only possible through product upgrades (from SQL Server 2016 to SQL Server 2017) or by binding R support to Microsoft Machine Learning Server. For more information, see [Upgrade R and Python components in SQL Server](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

### Python components

SQL Server 2017 adds Python components. When you select the Python language option, an Anaconda distribution is installed. In addition to Python code libraries, the standard installation includes sample data, unit tests, and sample scripts. 

Microsoft packages include [revoscalepy](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) as the Python equivalent of RevoScaleR, with streaming, parallel execution of rx functions for data import and transformation, modeling, visualization, and analysis. Another Python package is [microsoftml](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) for training and transformations, scoring, text and image analysis, and feature extraction for deriving values from existing data.

SQL Server 2017 Machine Learning is the first release to have both R and Python support.

|Release             | Anaconda version| Microsoft packages    |
|--------------------|-----------------|-----------------------|
| SQL Server 2017 Machine Learning Services  | 4.2 over Python 3.5 | revoscalepy, microsoftml |

After an initial installation, Python packages are refreshed through service packs and cumulative updates, but full version upgrades are only possible by binding Python support to Microsoft Machine Learning Server. For more information, see [Upgrade R and Python components in SQL Server](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

## Administrative permissions for package installation

The permissions required for package installation have changed between SQL Server 2016 and SQL Server 2017.

+ In SQL Server 2016, administrative access is required for installation of new R packages.
+ In SQL Server 2017, you can continue to install packages as an administrator for both R and Python, and this is probably the easiest method. 

    The DDL statement, CREATE EXTERNAL LIBRARY, allows the database administrator  to install packages without using R tools. 

    If you use the package management feature for Machine Learning Server, you can use RevoScaleR to install R packages at the database level. The database administrator must enable the feature and then grant users the ability to install their own packages on a per database basis. For more information, see [Enable package management using DDLs](r-package-how-to-enable-or-disable.md).

### User libraries are not supported

Users who cannot install a package to a secured location often resort to installing a package to a user library. However, this is not possible in the SQL Server environment. File system access is typically restricted on the server, and even if you have admin rights and access to a user document folder on the server, the external script runtime that executes in SQL Server cannot access any packages installed outside the default instance library. However, workarounds are possible. For tips on how to resolve problems related to user libraries, see [Workarounds for R user libraries](packages-installed-in-user-libraries.md).

## Next steps

+ [Get package information](determine-which-packages-are-installed-on-sql-server.md)
+ [Install new R packages](install-additional-r-packages-on-sql-server.md)
+ [Install new Python packages](../python/install-additional-python-packages-on-sql-server.md)
+ [Enable remote R package management](r-package-how-to-enable-or-disable.md)
+ [RevoScaleR functions for R package management](use-revoscaler-to-manage-r-packages.md)
+ [R package synchronization](package-install-uninstall-and-sync.md)
+ [miniCRAN for local R package repository](create-a-local-package-repository-using-minicran.md)
