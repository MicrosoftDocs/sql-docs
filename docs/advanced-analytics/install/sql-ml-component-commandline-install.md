---
title: "Command prompt installation of SQL Server machine learning components | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2018"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid:
caps.latest.revision: 4
author: "HeidiSteen"
ms.author: "heidist"
manager: "cgronlun"
ms.workload: "Inactive"
---
# Install machine learning components from the command line
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes how to use SQL Server command-line arguments to install the following SQL Server features by using the command line:

+ [SQL Server 2017 Machine Learning Services (In-Database)](#mls2017-indb) 
+ [SQL Server 2017 Machine Learning Server (Standalone)](#mls2017-standalone) 
+ [SQL Server 2016 R Services (In-Database)](#mrs2016-indb) 
+ [SQL Server 2016 R Server (Standalone)](#mrs2016-standalone)

You can specify silent, basic, or full interaction with the Setup user interface. This article supplements [Install SQL Server from the Command Prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md), covering the parameters specific to machine learning components.

When installing through the command prompt, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports full quiet mode by using the /Q parameter, or Quiet Simple mode by using the /QS parameter. The /QS switch only shows progress, does not accept any input, and displays no error messages if encountered. The /QS parameter is only supported when /Action=install is specified.

## Pre-install checklist

+ A database engine instance is required. You cannot install just R or Python features, athough you can add them incrementally to an existing instance.

+ Do not install on a failover cluster. The security mechanism used for isolating R and Python processes is not compatible with a Windows Server failover cluster environment.

+ Do not install on a domain controller. The Machine Learning Services portion of setup will fail.

+ Do not install standalone and in-database instances on the same computer. A standalone server will compete for the same resources, undermining the performance of both installations.

## <a name="mls2017-indb"></a> SQL Server 2017 Machine Learning Services (In-Database)

The following examples show different combinations of machine learning components. In all cases, the database engine instance is required. The FEATURES argument is required, as are licensing term agreements.

| Arguments | Description |
|-----------|-------------|
| /FEATURES = AdvancedAnalytics | Installs the in-database version: SQL Server 2017 Machine Learning Services (In-Database) or SQL Server 2016 R Services (In-Database). This option installs the prerequisites, but the installation is not usable unless you also add R or Python. |
| /FEATURES = SQL_SHARED_MR | Installs the standalone version: SQL Server 2017 Machine Learning Server (Standalone) or SQL Server 2016 R Server (Standalone). This option installs the prerequisites, but the installation is not usable unless you also add R or Python. |
| /FEATURES = SQL_INST_MR | Installs Microsoft R Open and the proprietary R packages.|
| /FEATURES = SQL_INST_MPY | Installs Anaconda and the proprietary Python packages. |
| /IACCEPTROPENLICENSETERMS="True"  | Indicates you have accepted the license terms for using the open source R components. |
| /IACCEPTPYTHONLICENSETERMS="True" | Indicates you have accepted the license terms for using the Python components. |
| /IACCEPTSQLSERVERLICENSETERMS="True" | Indicates you have accepted the license terms for using SQL Server.|
| /MRCACHEDIRECTORY | For offline setup, sets the folder containing the R component CAB files. |
| /MPYCACHEDIRECTORY | For offline setup, sets the folder containing the Python component CAB files. |

### Full installation (database engine, R, Python, pre-trained models) in quiet mode

To view progress and prompts, remove the _/q_ argument.

    ```  
    Setup.exe /q /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS,SQL_INST_MR,SQL_INST_MPY /INSTANCENAME=MSSQLSERVER /SECURITYMODE=SQL /SAPWD="%password%" /SQLSYSADMINACCOUNTS="<username>" /IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS /IACCEPTPYTHONLICENSETERMS
    ```

Adds R only, to an existing default database engine instance.

```
Setup.exe /q /ACTION=Install /FEATURES=ADVANCEDANALYTICS,SQL_INST_MR  /INSTANCENAME=MSSQLSERVER /IACCEPTROPENLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```

## <a name="mls2017-standalone"></a> SQL Server 2017 Machine Learning Server (Standalone)

Run the following command from an elevated command prompt to install only Machine Learning Server (Standalone) and its prerequisites.

This example shows the arguments used to install R.

```
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR, SQL_INST_MR  /IACCEPTROPENLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```

This example shows the arguments used to install Python.

```
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR, SQL_INST_MPY  /IACCEPTPYTHONOPENLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```

## <a name="mrs2016-indb"></a> SQL Server 2016 R Services (In-Database)

Run the following command from an elevated command prompt to install SQL Server 2016 R Services (In-Database) with a default database engine instance.

```
Setup.exe /q /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS,SQL_INST_MR  /INSTANCENAME=MSSQLSERVER /IACCEPTROPENLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```

## <a name="mrs2016-standalone"></a> SQL Server 2016 R Server (Standalone)

Run the following command from an elevated command prompt to install Microsoft R Server (Standalone) and its prerequisites. 

```
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR /IACCEPTROPENLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```





