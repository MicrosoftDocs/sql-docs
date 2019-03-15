---
title: Command-prompt installation of R and Python components - SQL Server Machine Learning
description: Run SQL Server command line setup to add R language and Python integration to a SQL Server database engine instance.
ms.prod: sql
ms.technology: machine-learning

ms.date: 03/13/2019  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Install SQL Server machine learning R and Python components from the command line
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article provides instructions for intalling SQL Server machine learning components from a command line:

+ [New In-Database instance](#indb)
+ [Add to an existing database engine instance](#add-existing)
+ [Silent install](#silent)
+ [New standalone server](#shared-feature)

You can specify silent, basic, or full interaction with the Setup user interface. This article supplements [Install SQL Server from the Command Prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md), covering the parameters unique to R and Python machine learning components.

## Pre-install checklist

+ Run commands from an elevated command prompt. 

+ A database engine instance is required for in-database installations. You cannot install just R or Python features, although you can [add them incrementally to an existing instance](#add-existing). If you want just R and Python without the database engine, install the [standalone server](#shared-feature).

+ Do not install on a failover cluster. The security mechanism used for isolating R and Python processes is not compatible with a Windows Server failover cluster environment.

+ Do not install on a domain controller. The Machine Learning Services portion of setup will fail.

+ Avoid installing standalone and in-database instances on the same computer. A standalone server will compete for the same resources, undermining the performance of both installations.


## Command line arguments

The FEATURES argument is required, as are licensing term agreements. 

When installing through the command prompt, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports full quiet mode by using the /Q parameter, or Quiet Simple mode by using the /QS parameter. The /QS switch only shows progress, does not accept any input, and displays no error messages if encountered. The /QS parameter is only supported when /Action=install is specified.

| Arguments | Description |
|-----------|-------------|
| /FEATURES = AdvancedAnalytics | Installs the in-database version: SQL Server 2017 Machine Learning Services (In-Database) or SQL Server 2016 R Services (In-Database).  |
| /FEATURES = SQL_INST_MR | Applies to SQL Server 2017 only. Pair this with AdvancedAnalytics. Installs the (In-Database) R feature, including Microsoft R Open and the proprietary R packages. The SQL Server 2016 R Services feature is R-only, so there is no parameter for that release.|
| /FEATURES = SQL_INST_MPY | Applies to SQL Server 2017 only. Pair this with AdvancedAnalytics. Installs the (In-Database) Python feature, including Anaconda and the proprietary Python packages. |
| /FEATURES = SQL_SHARED_MR | Installs the R feature for the standalone version: SQL Server 2017 Machine Learning Server (Standalone) or SQL Server 2016 R Server (Standalone). A standalone server is a "shared feature" not bound to a database engine instance.|
| /FEATURES = SQL_SHARED_MPY | Applies to SQL Server 2017 only. Installs the Python feature for the standalone version: SQL Server 2017 Machine Learning Server (Standalone). A standalone server is a "shared feature" not bound to a database engine instance.|
| /IACCEPTROPENLICENSETERMS  | Indicates you have accepted the license terms for using the open source R components. |
| /IACCEPTPYTHONLICENSETERMS | Indicates you have accepted the license terms for using the Python components. |
| /IACCEPTSQLSERVERLICENSETERMS | Indicates you have accepted the license terms for using SQL Server.|
| /MRCACHEDIRECTORY | For offline setup, sets the folder containing the R component CAB files. |
| /MPYCACHEDIRECTORY | Reserved for future use. Use %TEMP% to store Python component CAB files for installation on computers that do not have an internet connection. |


## <a name="indb"></a> In-database instance installations

In-database analytics are available for database engine instances, required for adding the **AdvancedAnalytics** feature to your installation. You can install a database engine instance with advanced analytics, or [add it to an existing instance](#add-existing). 

To view progress information without the interactive on-screen prompts, use the /qs argument.

> [!IMPORTANT]
> After installation, two additional configuration steps remain. Integration is not complete until these tasks are performed. See [Post-installation tasks](#post-install) for instructions.

### SQL Server 2017: database engine, advanced analytics with Python and R

For a concurrent installation of the database engine instance, provide the instance name and an administrator (Windows) login. Include features for installing core and language components, as well as acceptance of all licensing terms.

```cmd
Setup.exe /qs /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS,SQL_INST_MR,SQL_INST_MPY
/INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="<Windows-username>" 
/IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS /IACCEPTPYTHONLICENSETERMS
```

This the same command, but with a SQL Server login on a database engine using mixed authentication.

```cmd
Setup.exe /q /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS,SQL_INST_MR,SQL_INST_MPY
/INSTANCENAME=MSSQLSERVER /SECURITYMODE=SQL /SAPWD="%password%" /SQLSYSADMINACCOUNTS="<sql-username>" 
/IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS /IACCEPTPYTHONLICENSETERMS
```

This example is Python only, showing that you can add one language by omitting a feature.

```cmd  
Setup.exe /qs /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS,SQL_INST_MPY 
/INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="<username>" 
/IACCEPTSQLSERVERLICENSETERMS  /IACCEPTPYTHONLICENSETERMS
```

### SQL Server 2016: database engine and advanced analytics with R

This command is identical to SQL Server 2017, but without the Python elements, which are not available in SQL Server 2016 setup.

```cmd  
Setup.exe /qs /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS,SQL_INST_MR
/INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="<Windows-username>" 
/IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS 
```

## <a name="post-install"></a> Post-install configuration (required)

Applies to in-database installations only.

When setup is finished, you have a database engine instance with R and Python, the Microsoft R and Python packages, Microsoft R Open, Anaconda, tools, samples, and scripts that are part of the distribution. 

Two more tasks are required to complete the installation:

1. Restart the database engine service.

1. Enable external scripts before you can use the feature. Follow the instructions in [Install SQL Server 2017 Machine Learning Services (In-Database)](sql-machine-learning-services-windows-install.md) as your next step. 

For SQL Server 2016, use this article instead [Install SQL Server 2016 R Services (In-Database)](sql-r-services-windows-install.md).

## <a name="add-existing"></a> Add advanced analytics to an existing database engine instance

When adding in-database advanced analytics to an existing database engine instance, provide the instance name. For example, if you previously installed a SQL Server 2017 database engine and Python, you could use this command to add R.

```cmd  
Setup.exe /qs /ACTION=Install /FEATURES=SQL_INST_MR /INSTANCENAME=MSSQLSERVER 
/IACCEPTSQLSERVERLICENSETERMS  /IACCEPTROPENLICENSETERMS
```



## <a name="silent"></a> Silent install

A silent installation suppresses the check for .cab file locations. For this reason, you must specify the location where .cab files are to be unpacked. For Python, CAB files must be located in %TEMP*. For R, you can set the folder path using You can the temp directory for this.
 
```cmd  
Setup.exe /q /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS,SQL_INST_MR,SQL_INST_MPY 
/INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="<username>" 
/IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS /IACCEPTPYTHONLICENSETERMS 
/MRCACHEDIRECTORY=%temp% 
```

## <a name="shared-feature"></a> Standalone server installations

A standalone server is a "shared feature" not bound to a database engine instance. The following examples show valid syntax for both releases.

SQL Server 2017 supports Python and R on a standalone server:

```cmd
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR,SQL_SHARED_MPY  
/IACCEPTROPENLICENSETERMS /IACCEPTPYTHONLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```

SQL Server 2016 is R-only:

```cmd
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR 
/IACCEPTROPENLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```

When setup is finished, you have a server, Microsoft packages, open-source distributions of R and Python, tools, samples, and scripts that are part of the distribution. 

To open an R console window, go to \Program files\Microsoft SQL Server\140 (or 130)\R_SERVER\bin\x64 and double-click **RGui.exe**. New to R? Try this tutorial: [Basic R commands and RevoScaleR functions: 25 common examples](https://docs.microsoft.com/machine-learning-server/r/tutorial-r-to-revoscaler).

To open a Python command, go to \Program files\Microsoft SQL Server\140\PYTHON_SERVER\bin\x64 and double-click **python.exe**.

## Get help

Need help with installation or upgrade? For answers to common questions and known issues, see the following article:

* [Upgrade and installation FAQ - Machine Learning Services](../r/upgrade-and-installation-faq-sql-server-r-services.md)

To check the installation status of the instance and fix common issues, try these custom reports.

* [Custom reports for SQL Server R Services](../r/monitor-r-services-using-custom-reports-in-management-studio.md)

## Next steps

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

+ [Tutorial: Run R in T-SQL](../tutorials/rtsql-using-r-code-in-transact-sql-quickstart.md)
+ [Tutorial: In-database analytics for R developers](../tutorials/sqldev-in-database-r-for-sql-developers.md)

Python developers can learn how to use Python with SQL Server by following these tutorials:

+ [Tutorial: Run Python in T-SQL](../tutorials/run-python-using-t-sql.md)
+ [Tutorial: In-database analytics for Python developers](../tutorials/sqldev-in-database-python-for-sql-developers.md)

To view examples of machine learning that are based on real-world scenarios, see [Machine learning tutorials](../tutorials/machine-learning-services-tutorials.md).