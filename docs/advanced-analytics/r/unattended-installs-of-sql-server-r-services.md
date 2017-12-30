---
title: "Unattended installation of Machine Learning Services | Microsoft Docs"
ms.custom: ""
ms.date: "10/31/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 77e92b2d-5777-4c31-bf02-f931ed54a247
caps.latest.revision: 18
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---
# Unattended installation of Machine Learning Services (In-Database)

This article describes how to use command-line arguments with SQL Server setup to install the machine learning components.

By unattended installation, we mean that you do not use the interactive features of the setup wizard, and instead provide all arguments required to complete installation, including licensing agreements for SQL Server and for machine learning components on a command line or as part of a script, typically in quiet mode.

+ [SQL Server 2016 R Services](#bkmk_OldInstall)
+ [SQL Server 2017 Machine Learning Services](#bkmk_NewInstall) with R or Python
+ [Microsoft R Server or Machine Learning Server](../r/install-microsoft-r-server-from-the-command-line.md)

**Applies to: SQL Server 2017 Machine Learning Services (In-Database), SQL Server 2016 R Services**

## Prerequisites

+ You must install the database engine on each instance where you will use machine learning.

+ If the computer does not have Internet access, be sure to download the installers for the machine learning components beforehand. See [Installing machine learning components without Internet access](../r/installing-ml-components-without-internet-access.md).

+ There are separate licensing parameters for each of the open source components for R and Python. SQL Server 2016 supports only R; SQL Server 2017 supports both R and Python. You must accept the license terms for each language that you install. Setup fails if you do not include this parameter in your command line.

+ To complete setup without having to respond to prompts, make sure that you have identified all required arguments, including those for licensing, and for any other features that you might want to install.

+ The **Mixed** security mode that supports SQL logins was required in early releases. Although it is no longer required, you might consider enabling Mixed Mode authentication to support solution development by data scientists who use a SQL login.

> [!IMPORTANT]
> 
> Additional steps are required after setup is finished to enable the feature. These include a reconfiguration and restart of the instance. Be sure to review all items in the section on [post-installation steps] (#bkmk_PostInstall) to determine actions needed after setup completes.

## <a name="bkmk_NewInstall"></a>  Command-line installation for SQL Server 2017

The following examples include the **minimum** required features.

> [!IMPORTANT]
> Be sure to run all commands from an elevated command prompt.
> 
> After installation is complete, some additional steps are required. See [this section](#bkmk_PostInstall). 
> Another restart of the SQL Server services will be required after configuration is complete.

### Install R only

The following example shows the arguments required to perform a silent, unattended install of SQL Server 2017 Machine Services (In-Database) with the R language added.

```
Setup.exe /q /ACTION=Install /FEATURES=SQLENGINE,ADVANCEDANALYTICS, SQL_INST_MR /INSTANCENAME=MSSQLSERVER /SECURITYMODE=SQL /SAPWD="%password%" /SQLSYSADMINACCOUNTS="<username>" /IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS
```

Note the flags required for R in SQL Server 2017:

+ `ADVANCEDANALYTICS`
+ `SQL_INST_MR`
+ `IACCEPTROPENLICENSETERMS`.

### Install Python only

The following example shows the arguments required to perform a silent, unattended install of SQL Server 2017 Machine Services (In-Database) with the Python language added.

```
Setup.exe /q /ACTION=Install /FEATURES=SQLENGINE,ADVANCEDANALYTICS, SQL_INST_MPY /INSTANCENAME=MSSQLSERVER /SECURITYMODE=SQL /SAPWD="%password%" /SQLSYSADMINACCOUNTS="<username>" /IACCEPTSQLSERVERLICENSETERMS /IACCEPTPYTHONOPENLICENSETERMS
```

Note the flags required for Python in SQL Server 2017:

+ `ADVANCEDANALYTICS`
+ `SQL_INST_MPY`
+ `IACCEPTPYTHONOPENLICENSETERMS`

### Install both R and Python on a named instance

The following example shows the arguments required to perform a silent, unattended install of SQL Server 2017 Machine Services (In-Database) on a named instance. Both R and Python languages are added.

```
Setup.exe /q /ACTION=Install /FEATURES=SQLENGINE,ADVANCEDANALYTICS, SQL_INST_MR, SQL_INST_MPY /INSTANCENAME=MSSQLSERVER.ServerName /SECURITYMODE=SQL /SAPWD="%password%" /SQLSYSADMINACCOUNTS="<username>" /IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS /IACCEPTPYTHONOPENLICENSETERMS
```

## <a name="OldInstall"></a> Command-line installation for SQL Server 2016
 
The following example shows the arguments required to perform a silent, unattended install of SQL Server 2016 with the R language added.

```
Setup.exe /q /ACTION=Install /FEATURES=SQL,ADVANCEDANALYTICS /INSTANCENAME=MSSQLSERVER /SECURITYMODE=SQL /SAPWD="%password%" /SQLSYSADMINACCOUNTS="<username>" /IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS
```

Note the flags required for 2016 R Services:

+ `ADVANCEDANALYTICS`
+ `IACCEPTROPENLICENSETERMS`

## <a name = "bkmk_PostInstall"></a>Additional steps after setup

You must perform the following steps after SQL Server setup is complete, regardless of which version you installed. machine learning features are not enabled by default and must be explicitly enabled.

1.  After installation is complete, open a new **Query** window in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and run the following command to enable the feature.
  
    ```SQL
    EXEC sp_configure  'external scripts enabled', 1
    RECONFIGURE WITH OVERRIDE
    ```
  
    > [!NOTE]
    >  You must explicitly enable the feature and reconfigure; otherwise, it will not be possible to invoke external scripts even if the feature has been installed.
  
2.  Restart the SQL Server service for the reconfigured instance. Doing so will automatically restart the related [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service as well.

> [!IMPORTANT]
> 
> Some additional steps might be required if you have a custom security configuration, or if you will be using the SQL Server as a compute context when executing R or Python code from a remote client. 
> 
> For more information, see [Upgrade and installation FAQ](../../advanced-analytics/r/upgrade-and-installation-faq-sql-server-r-services.md).
