---
title: Install from a Command Prompt
description: Run SQL Server command line setup to add Machine Learning Services with Python and R to a SQL Server Database Engine instance.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/13/2026
ms.service: sql
ms.subservice: machine-learning-services
ms.topic: how-to
ms.custom:
  - intro-installation
  - sfi-ropc-blocked
monikerRange: ">=sql-server-2017 || >=sql-server-linux-ver15"
---
# Install SQL Server Machine Learning Services with R and Python from the command line

[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

This article provides instructions for installing [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) with Python and R from a command line.

You can specify silent, basic, or full interaction with the Setup user interface. This article supplements [Install, configure, or uninstall SQL Server on Windows from the command prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md), covering the parameters unique to R and Python machine learning components.

> [!NOTE]  
> Feature capabilities and installation options vary between versions of SQL Server. Use the version selector dropdown list to choose the appropriate version of SQL Server.

## Pre-install checklist

- Run commands from an elevated command prompt.

- A Database Engine instance is required for in-database installations. You can't install just R or Python features, although you can [add them incrementally to an existing instance](#add-existing). If you want just R and Python without the Database Engine, install the [standalone server](#shared-feature).

- Don't install on a failover cluster. The security mechanism used for isolating R and Python processes isn't compatible with a Windows Server failover cluster environment.

- Don't install on a domain controller. The Machine Learning Services portion of setup fails.

- Avoid installing standalone and in-database instances on the same computer. A standalone server competes for the same resources, undermining the performance of both installations.

## Command-line arguments

You must include the `/FEATURES` argument and agree to the licensing terms.

When you install through the command prompt, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports full quiet mode by using the `/Q` parameter, or Quiet Simple mode by using the `/QS` parameter. The `/QS` switch only shows progress, doesn't accept any input, and displays no error messages if it encounters any. The `/QS` parameter is only supported when you specify `/Action=install`.

::: moniker range="=sql-server-2017"

### Command-line arguments for SQL Server 2017

| Arguments | Description |
| --- | --- |
| `/FEATURES = AdvancedAnalytics` | Installs the in-database version: SQL Server Machine Learning Services (In-Database). |
| `/FEATURES = SQL_INST_MR` | Pair this argument with `AdvancedAnalytics`. Installs the (In-Database) R feature, including Microsoft R Open and the proprietary R packages. |
| `/FEATURES = SQL_INST_MPY` | Pair this argument with `AdvancedAnalytics`. Installs the (In-Database) Python feature, including Anaconda and the proprietary Python packages. |
| `/FEATURES = SQL_SHARED_MR` | Installs the R feature for the standalone version: SQL Server Machine Learning Server (Standalone). A standalone server is a "shared feature" not bound to a Database Engine instance. |
| `/FEATURES = SQL_SHARED_MPY` | Installs the Python feature for the standalone version: SQL Server Machine Learning Server (Standalone). A standalone server is a "shared feature" not bound to a Database Engine instance. |
| `/IACCEPTROPENLICENSETERMS` | Indicates you accept the license terms for using the open source R components. |
| `/IACCEPTPYTHONLICENSETERMS` | Indicates you accept the license terms for using the Python components. |
| `/IACCEPTSQLSERVERLICENSETERMS` | Indicates you accept the license terms for using SQL Server. |
| `/MRCACHEDIRECTORY` | For offline setup, sets the folder containing the R component CAB files. |
| `/MPYCACHEDIRECTORY` | Reserved for future use. Use `%TEMP%` to store Python component CAB files for installation on computers that don't have an internet connection. |

::: moniker-end

::: moniker range="=sql-server-ver15"

### Command-line arguments for SQL Server 2019

| Arguments | Description |
| --- | --- |
| `/FEATURES = AdvancedAnalytics` | Installs the in-database version: SQL Server Machine Learning Services (In-Database). |
| `/FEATURES = SQL_INST_MR` | Pair this argument with `AdvancedAnalytics`. Installs the (In-Database) R feature, including Microsoft R Open and the proprietary R packages. |
| `/FEATURES = SQL_INST_MPY` | Pair this argument with `AdvancedAnalytics`. Installs the (In-Database) Python feature, including Anaconda and the proprietary Python packages. |
| `/FEATURES=SQL_INST_MJAVA` | Pair this with `AdvancedAnalytics`. Installs the (In-Database) Java feature, including Open JRE. Applies to [SQL Server Java Language Extension](../../language-extensions/install/windows-java.md). |
| `/FEATURES = SQL_SHARED_MR` | Installs the R feature for the standalone version: SQL Server Machine Learning Server (Standalone). A standalone server is a "shared feature" not bound to a Database Engine instance. |
| `/FEATURES = SQL_SHARED_MPY` | Installs the Python feature for the standalone version: SQL Server Machine Learning Server (Standalone). A standalone server is a "shared feature" not bound to a Database Engine instance. |
| `/IACCEPTROPENLICENSETERMS` | Indicates you accept the license terms for using the open source R components. |
| `/IACCEPTPYTHONLICENSETERMS` | Indicates you accept the license terms for using the Python components. |
| `/IACCEPTSQLSERVERLICENSETERMS` | Indicates you accept the license terms for using SQL Server. |
| `/MRCACHEDIRECTORY` | For offline setup, sets the folder containing the R component CAB files. |
| `/MPYCACHEDIRECTORY` | Reserved for future use. Use `%TEMP%` to store Python component CAB files for installation on computers that don't have an internet connection. |

::: moniker-end

::: moniker range="=sql-server-ver16"

### Command-line arguments for SQL Server 2022

| Arguments | Description |
| --- | --- |
| `/FEATURES = AdvancedAnalytics` | Installs the in-database version: SQL Server Machine Learning Services (In-Database). |
| `/IACCEPTSQLSERVERLICENSETERMS` | Indicates you accept the license terms for using SQL Server. |

::: moniker-end

<a id="indb"></a>

## In-database instance installations

In-database analytics are available for Database Engine instances. You need a Database Engine instance to add the `AdvancedAnalytics` feature to your installation. You can install a Database Engine instance with Advanced Analytics, or [add it to an existing instance](#add-existing).

To view progress information without the interactive on-screen prompts, use the `/qs` argument.

> [!IMPORTANT]  
> After installation, two additional configuration steps remain. Integration isn't complete until you perform these tasks. See [Post-installation configuration](#post-install) for instructions.

::: moniker range="=sql-server-ver16"

### SQL Server 2022 Machine Learning Services: Database Engine, Advanced Analytics

Provide the instance name and an administrator (Windows) account for a concurrent installation of the Database Engine instance. Include features for installing core and language components, and accept all licensing terms.

```console
Setup.exe /qs /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS
/INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="<Windows-username>"
/IACCEPTSQLSERVERLICENSETERMS
```

::: moniker-end

::: moniker range="=sql-server-2017 || =sql-server-ver15"

### SQL Server Machine Learning Services: Database Engine, Advanced Analytics with Python and R

Provide the instance name and an administrator (Windows) account for a concurrent installation of the Database Engine instance. Include features for installing core and language components, and accept all licensing terms.

```console
Setup.exe /qs /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS,SQL_INST_MR,SQL_INST_MPY
/INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="<Windows-username>"
/IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS /IACCEPTPYTHONLICENSETERMS
```

This command uses a SQL Server login on a Database Engine instance with mixed authentication.

```console
Setup.exe /q /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS,SQL_INST_MR,SQL_INST_MPY
/INSTANCENAME=MSSQLSERVER /SECURITYMODE=SQL /SAPWD="%password%" /SQLSYSADMINACCOUNTS="<sql-username>"
/IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS /IACCEPTPYTHONLICENSETERMS
```

This example installs only Python, showing that you can add a single language by omitting a feature.

```console
Setup.exe /qs /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS,SQL_INST_MPY
/INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="<username>"
/IACCEPTSQLSERVERLICENSETERMS  /IACCEPTPYTHONLICENSETERMS
```
::: moniker-end

<a id="post-install"></a>

## Post-installation configuration (required)

Applies to in-database installations only.

When SQL Setup for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], and [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] finishes, you get a Database Engine instance with R and Python, the Microsoft R and Python packages, Microsoft R Open, Anaconda, tools, samples, and scripts that are part of the distribution.

Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], SQL Server Setup no longer installs runtimes for R, Python, and Java. Instead, install your desired R and Python custom runtimes and packages. For more information, see [Install SQL Server 2022 Machine Learning Services on Windows](sql-machine-learning-services-windows-install-sql-2022.md) or [Install SQL Server 2019 Machine Learning Services on Linux](../../linux/install-upgrade/setup-machine-learning.md).

Complete the installation by performing the following tasks:

::: moniker range=">=sql-server-2017"

1. Restart the Database Engine service.

1. SQL Server Machine Learning Services: Enable external scripts before you can use the feature. Follow the instructions in [Install SQL Server Machine Learning Services (Python and R) on Windows](sql-machine-learning-services-windows-install.md) as your next step.
   ::: moniker-end

::: moniker range="=sql-server-2017 || =sql-server-ver15"
<a id="add-existing"></a>

## Add Advanced Analytics to an existing Database Engine instance

When adding in-database Advanced Analytics to an existing Database Engine instance, provide the instance name. For example, if you previously installed a SQL Server 2017 or later Database Engine and Python, you could use this command to add R.

```console
Setup.exe /qs /ACTION=Install /FEATURES=SQL_INST_MR /INSTANCENAME=MSSQLSERVER
/IACCEPTSQLSERVERLICENSETERMS  /IACCEPTROPENLICENSETERMS
```
::: moniker-end

::: moniker range="=sql-server-ver16"
<a id="add-existing-2022"></a>

## Add Advanced Analytics to an existing Database Engine instance

When you add in-database Advanced Analytics to an existing Database Engine instance, provide the instance name. For example, if you previously installed a [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later Database Engine, you can add the Machine Learning Services feature by using the following command:

```console
Setup.exe /qs /ACTION=Install /FEATURES=ADVANCEDANALYTICS /INSTANCENAME=MSSQLSERVER
/IACCEPTSQLSERVERLICENSETERMS  /IACCEPTROPENLICENSETERMS
```

::: moniker-end

::: moniker range="=sql-server-ver16"
<a id="silent-sql2022"></a>

## Silent install for SQL Server 2022

For a silent installation of [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], use the following sample:

```console
Setup.exe /q /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS
/INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="<username>"
/IACCEPTSQLSERVERLICENSETERMS
```
::: moniker-end

::: moniker range="=sql-server-2017 || =sql-server-ver15"

<a id="silent"></a>

## Silent install

A silent installation suppresses the check for .cab file locations. For this reason, you must specify the location where .cab files are to be unpacked. For Python, place CAB files in `%TEMP%`. For R, set the folder path by using the temp directory.

```console
Setup.exe /q /ACTION=Install /FEATURES=SQLEngine,ADVANCEDANALYTICS,SQL_INST_MR,SQL_INST_MPY
/INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="<username>"
/IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS /IACCEPTPYTHONLICENSETERMS
/MRCACHEDIRECTORY=%temp%
```

::: moniker-end

<a id="shared-feature"></a>

## Standalone server installations

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], and [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] only.

[!INCLUDE [machine-learning-server-retirement](../../includes/machine-learning-server-retirement.md)]

A standalone server is a *shared feature* that isn't bound to a Database Engine instance. The following examples show valid syntax for installation of the standalone server.

::: moniker range=">=sql-server-2017"
SQL Server Machine Learning Server supports Python and R on a standalone server:

```console
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR,SQL_SHARED_MPY
/IACCEPTROPENLICENSETERMS /IACCEPTPYTHONLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```

::: moniker-end

When SQL Server Setup for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], and [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] finishes, you get a server, Microsoft packages, open-source distributions of R and Python, tools, samples, and scripts that are part of the distribution.

Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], SQL Server Setup no longer installs runtimes for R, Python, and Java. Instead, install your desired R and Python custom runtimes and packages. For more information, see [Install SQL Server 2022 Machine Learning Services on Windows](sql-machine-learning-services-windows-install-sql-2022.md) or [Install SQL Server 2019 Machine Learning Services on Linux](../../linux/install-upgrade/setup-machine-learning.md).

To open an R console window, go to `\Program files\Microsoft SQL Server\<nnn>\R_SERVER\bin\x64` (where `<nnn>` is `150`, `140`, or `130`), and select **RGui.exe**. For more information, see [Basic R commands and RevoScaleR functions: 25 common examples](/machine-learning-server/r/tutorial-r-to-revoscaler).

To open a Python command, go to `\Program files\Microsoft SQL Server\<nnn>\PYTHON_SERVER\bin\x64` (where `<nnn>` is `150` or `140`), and select **python.exe**.

## Related content

- [Python language extension in SQL Server Machine Learning Services](../concepts/extension-python.md)
- [Quickstart: Run simple R scripts with SQL machine learning](../tutorials/quickstart-r-create-script.md)
- [R tutorial: Predict NYC taxi fares with binary classification](../tutorials/r-taxi-classification-introduction.md)
