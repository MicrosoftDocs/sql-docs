---
title: Install from a command prompt
description: Run SQL Server command line setup to add R language and Python integration to a SQL Server database engine instance.
ms.prod: sql
ms.technology: machine-learning-services

ms.date: 11/04/2019  
ms.topic: how-to
author: dphansen
ms.author: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Install SQL Server machine learning R and Python components from the command line
[!INCLUDE [SQL Server Windows Only - ASDBMI ](../../includes/applies-to-version/sql-windows-only-asdbmi.md)]

This article provides instructions for installing SQL Server machine learning components from a command line:

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

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
| Arguments | Description |
|-----------|-------------|
| /FEATURES = AdvancedAnalytics | Installs the in-database version: SQL Server R Services (In-Database).  |
| /FEATURES = SQL_SHARED_MR | Installs the R feature for the standalone version: SQL Server R Server (Standalone). A standalone server is a "shared feature" not bound to a database engine instance.|
| /IACCEPTROPENLICENSETERMS  | Indicates you have accepted the license terms for using the open source R components. |
| /IACCEPTPYTHONLICENSETERMS | Indicates you have accepted the license terms for using the Python components. |
| /IACCEPTSQLSERVERLICENSETERMS | Indicates you have accepted the license terms for using SQL Server.|
| /MRCACHEDIRECTORY | For offline setup, sets the folder containing the R component CAB files. |
::: moniker-end

::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
| Arguments | Description |
|-----------|-------------|
| /FEATURES = AdvancedAnalytics | Installs the in-database version: SQL Server Machine Learning Services (In-Database).  |
| /FEATURES = SQL_INST_MR | Pair this with AdvancedAnalytics. Installs the (In-Database) R feature, including Microsoft R Open and the proprietary R packages. |
| /FEATURES = SQL_INST_MPY | Pair this with AdvancedAnalytics. Installs the (In-Database) Python feature, including Anaconda and the proprietary Python packages. |
| /FEATURES = SQL_SHARED_MR | Installs the R feature for the standalone version: SQL Server Machine Learning Server (Standalone). A standalone server is a "shared feature" not bound to a database engine instance.|
| /FEATURES = SQL_SHARED_MPY | Installs the Python feature for the standalone version: SQL Server Machine Learning Server (Standalone). A standalone server is a "shared feature" not bound to a database engine instance.|
| /IACCEPTROPENLICENSETERMS  | Indicates you have accepted the license terms for using the open source R components. |
| /IACCEPTPYTHONLICENSETERMS | Indicates you have accepted the license terms for using the Python components. |
| /IACCEPTSQLSERVERLICENSETERMS | Indicates you have accepted the license terms for using SQL Server.|
| /MRCACHEDIRECTORY | For offline setup, sets the folder containing the R component CAB files. |
| /MPYCACHEDIRECTORY | Reserved for future use. Use %TEMP% to store Python component CAB files for installation on computers that do not have an internet connection. |
::: moniker-end

::: moniker range=">=sql-server-ver15||=sqlallproducts-allversions"
| Arguments | Description |
|-----------|-------------|
| /FEATURES = AdvancedAnalytics | Installs the in-database version: SQL Server Machine Learning Services (In-Database).  |
| /FEATURES = SQL_INST_MR | Pair this with AdvancedAnalytics. Installs the (In-Database) R feature, including Microsoft R Open and the proprietary R packages. |
| /FEATURES = SQL_INST_MPY | Pair this with AdvancedAnalytics. Installs the (In-Database) Python feature, including Anaconda and the proprietary Python packages. |
| /FEATURES = SQL_INST_MJAVA | Pair this with AdvancedAnalytics. Installs the (In-Database) Java feature, including Open JRE. |
| /FEATURES = SQL_SHARED_MR | Installs the R feature for the standalone version: SQL Server Machine Learning Server (Standalone). A standalone server is a "shared feature" not bound to a database engine instance.|
| /FEATURES = SQL_SHARED_MPY | Installs the Python feature for the standalone version: SQL Server Machine Learning Server (Standalone). A standalone server is a "shared feature" not bound to a database engine instance.|
| /IACCEPTROPENLICENSETERMS  | Indicates you have accepted the license terms for using the open source R components. |
| /IACCEPTPYTHONLICENSETERMS | Indicates you have accepted the license terms for using the Python components. |
| /IACCEPTSQLSERVERLICENSETERMS | Indicates you have accepted the license terms for using SQL Server.|
| /MRCACHEDIRECTORY | For offline setup, sets the folder containing the R component CAB files. |
| /MPYCACHEDIRECTORY | Reserved for future use. Use %TEMP% to store Python component CAB files for installation on computers that do not have an internet connection. |
::: moniker-end

## <a name="indb"></a> In-database instance installations

In-database analytics are available for database engine instances, required for adding the **AdvancedAnalytics** feature to your installation. You can install a database engine instance with advanced analytics, or [add it to an existing instance](#add-existing). 

To view progress information without the interactive on-screen prompts, use the /qs argument.

> [!IMPORTANT]
> After installation, two additional configuration steps remain. Integration is not complete until these tasks are performed. See [Post-installation tasks](#post-install) for instructions.

::: moniker range=">=sql-server-2017||=sqlallproducts-allversions"
### SQL Server Machine Learning Services: database engine, advanced analytics with Python and R

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
::: moniker-end

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
### SQL Server R Services: database engine and advanced analytics with R

For a concurrent installation of the database engine instance, provide the instance name and an administrator (Windows) login. Include features for installing core and language components, as well as acceptance of all licensing terms.

```cmd  
Setup.exe /qs /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS,SQL_INST_MR
/INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="<Windows-username>" 
/IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS 
```
::: moniker-end

## <a name="post-install"></a> Post-install configuration (required)

Applies to in-database installations only.

When setup is finished, you have a database engine instance with R and Python, the Microsoft R and Python packages, Microsoft R Open, Anaconda, tools, samples, and scripts that are part of the distribution. 

Two more tasks are required to complete the installation:


::: moniker range=">=sql-server-2017||=sqlallproducts-allversions"
1. Restart the database engine service.

1. SQL Server Machine Learning Services: Enable external scripts before you can use the feature. Follow the instructions in [Install SQL Server Machine Learning Services (In-Database)](sql-machine-learning-services-windows-install.md) as your next step. 
::: moniker-end

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
1. Restart the database engine service.

1. SQL Server R Services: Enable external scripts before you can use the feature. Follow the instructions in  [Install SQL Server R Services (In-Database)](sql-r-services-windows-install.md) as your next step. 
::: moniker-end

## <a name="add-existing"></a> Add advanced analytics to an existing database engine instance

When adding in-database advanced analytics to an existing database engine instance, provide the instance name. For example, if you previously installed a SQL Server 2017 or later database engine and Python, you could use this command to add R.

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

A standalone server is a "shared feature" not bound to a database engine instance. The following examples show valid syntax for installation of the standalone server.

::: moniker range=">=sql-server-2017||=sqlallproducts-allversions"
SQL Server Machine Learning Server supports Python and R on a standalone server:

```cmd
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR,SQL_SHARED_MPY  
/IACCEPTROPENLICENSETERMS /IACCEPTPYTHONLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```
::: moniker-end
::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
SQL Server R Server is R-only:

```cmd
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR 
/IACCEPTROPENLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```
::: moniker-end

When setup is finished, you have a server, Microsoft packages, open-source distributions of R and Python, tools, samples, and scripts that are part of the distribution. 

To open an R console window, go to `\Program files\Microsoft SQL Server\150 (or 140/130)\R_SERVER\bin\x64` and double-click **RGui.exe**. New to R? Try this tutorial: [Basic R commands and RevoScaleR functions: 25 common examples](https://docs.microsoft.com/machine-learning-server/r/tutorial-r-to-revoscaler).

To open a Python command, go to `\Program files\Microsoft SQL Server\150 (or 140)\PYTHON_SERVER\bin\x64` and double-click **python.exe**.

## Next steps

Python developers can learn how to use Python with SQL Server by following these tutorials:

+ [Python tutorial: Predict ski rental with linear regression in SQL Server Machine Learning Services](../tutorials/python-ski-rental-linear-regression-deploy-model.md)
+ [Python tutorial: Categorizing customers using k-means clustering with SQL Server Machine Learning Services](../tutorials/python-clustering-model.md)

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

+ [Quickstart: Run R in T-SQL](../tutorials/quickstart-r-create-script.md)
+ [Tutorial: In-database analytics for R developers](../tutorials/r-taxi-classification-introduction.md)