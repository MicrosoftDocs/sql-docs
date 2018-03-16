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

+ A database engine instance is required. You cannot install just R or Python features, although you can add them incrementally to an existing instance.

+ Do not install on a failover cluster. The security mechanism used for isolating R and Python processes is not compatible with a Windows Server failover cluster environment.

+ Do not install on a domain controller. The Machine Learning Services portion of setup will fail.

+ Do not install standalone and in-database instances on the same computer. A standalone server will compete for the same resources, undermining the performance of both installations.

## <a name="mls2017-indb"></a> SQL Server 2017 Machine Learning Services (In-Database)

The following examples show different combinations of machine learning components. In all cases, the database engine instance is required. The FEATURES argument is required, as are licensing term agreements.

| Arguments | Description |
|-----------|-------------|
| /FEATURES = AdvancedAnalytics | Installs the in-database version: SQL Server 2017 Machine Learning Services (In-Database) or SQL Server 2016 R Services (In-Database). This option installs the prerequisites, but the installation is not usable unless you also add R or Python. |
| /FEATURES = SQL_INST_MR | Use with AdvancedAnalytics. Installs the (In-Database) R feature, including Microsoft R Open and the proprietary R packages. Applies to SQL Server 2017 only, where there is a choice between languages. The SQL Server 2016 R feature is R-only, so there is no parameter for that release.|
| /FEATURES = SQL_INST_MPY | Use with AdvancedAnalytics. Installs the (In-Database) Python feature, including Anaconda and the proprietary Python packages. Applies to SQL Server 2017 only.|
| /FEATURES = SQL_SHARED_MR | Installs the R feature for the standalone version: SQL Server 2017 Machine Learning Server (Standalone) or SQL Server 2016 R Server (Standalone). |
| /FEATURES = SQL_SHARED_MPY | Installs the Python feature for the standalone version: SQL Server 2017 Machine Learning Server (Standalone).|
| /IACCEPTROPENLICENSETERMS  | Indicates you have accepted the license terms for using the open source R components. |
| /IACCEPTPYTHONLICENSETERMS | Indicates you have accepted the license terms for using the Python components. |
| /IACCEPTSQLSERVERLICENSETERMS | Indicates you have accepted the license terms for using SQL Server.|
| /MRCACHEDIRECTORY | For offline setup, sets the folder containing the R component CAB files. |
| /MPYCACHEDIRECTORY | For offline setup, sets the folder containing the Python component CAB files. |

### Full installation (database engine, R, Python) in quiet mode

To view progress information without the interactive on-screen prompts, use the /qs argument.

```  
Setup.exe /qs /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS,SQL_INST_MR,SQL_INST_MPY /INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="<username>" /IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS /IACCEPTPYTHONLICENSETERMS
```

You can install just Python by omitting SQL_INST_MR and IACCEPTROPENLICENSETERMS.

```  
Setup.exe /qs /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS,SQL_INST_MPY /INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="<username>" /IACCEPTSQLSERVERLICENSETERMS  /IACCEPTPYTHONLICENSETERMS
```

You can add R later by rerunning the command with R options, specifying the same instance name.

```  
Setup.exe /qs /ACTION=Install /FEATURES=SQL_INST_MR /INSTANCENAME=MSSQLSERVER /IACCEPTSQLSERVERLICENSETERMS  /IACCEPTROPENLICENSETERMS
```

Silent install (/q) introduces requirements for .cab file locations.
```  
Setup.exe /q /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS,SQL_INST_MR,SQL_INST_MPY /INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="<username>" /IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS /IACCEPTPYTHONLICENSETERMS /MRCACHEDIRECTORY=%temp% /MPYCACHEDIRECTORY=%temp%
```

When setup is finished, you have a database engine instance with R and Python Services, the Microsoft R and Python packages, Microsoft R Open, Anaconda, tools, samples, and scripts that are part of the distribution. You must now enable external scripts before you can use the feature. Follow the instructions in [Install SQL Server 2017 Machine Learning Services (In-Database)](sql-machine-learning-services-windows-install.md) as your next step.

## <a name="mls2017-standalone"></a> SQL Server 2017 Machine Learning Server (Standalone)

Run the following command from an elevated command prompt to install only Machine Learning Server (Standalone) and its prerequisites.

This example shows the arguments used to install R and Python.

```
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR,SQL_SHARED_MPY  /IACCEPTROPENLICENSETERMS /IACCEPTPYTHONLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```

When setup is finished, you have servers, proprietary packages, open-source packages, tools, samples, and scripts that are part of the distribution. 

To open an R console window, go to \Program files\Microsoft SQL Server\140\R_SERVER\bin\x64 and double-click **RGui.exe**.

To open a Python command, go to \Program files\Microsoft SQL Server\140\PYTHON_SERVER\bin\x64 and double-click **python.exe**.

New to R? Try this tutorial: [Basic R commands and RevoScaleR functions: 25 common examples](https://docs.microsoft.com/en-us/machine-learning-server/r/tutorial-r-to-revoscaler).

## <a name="mrs2016-indb"></a> SQL Server 2016 R Services (In-Database)

Run the following command from an elevated command prompt to install SQL Server 2016 R Services (In-Database) with a default database engine instance. The instance name and administrator login are required. R Services includes R implicitly, so there is no provision for adding an R-specific parameter.

```
Setup.exe /q /ACTION=Install /FEATURES=SQL,ADVANCEDANALYTICS /INSTANCENAME=MSSQLSERVER /SECURITYMODE=SQL /SAPWD="%password%" /SQLSYSADMINACCOUNTS="<username>" /IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS
```
When setup is finished, you have a database engine instance with R Services, the Microsoft R packages, Microsoft R Open, and the R tools, samples, and scripts that are part of the distribution. You must now enable external scripts before you can use the feature. Follow the instructions in [Install SQL Server 2016 R Services (In-Database)](sql-r-services-windows-install.md) as your next step.

## <a name="mrs2016-standalone"></a> SQL Server 2016 R Server (Standalone)

Run the following command from an elevated command prompt to install Microsoft R Server (Standalone) and its prerequisites. The database engine is not installed. 

```
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR /IACCEPTROPENLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```
When setup is finished, you have R Server, the Microsoft R packages, Microsoft R Open, and the R tools, samples, and scripts that are part of the distribution. 

To open an R console window, go to \Program files\Microsoft SQL Server\130\R_SERVER\bin\x64 and double-click **RGui.exe**.

New to R? Try this tutorial: [Basic R commands and RevoScaleR functions: 25 common examples](https://docs.microsoft.com/en-us/machine-learning-server/r/tutorial-r-to-revoscaler).




