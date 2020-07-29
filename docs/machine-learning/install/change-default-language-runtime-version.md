---
title: Change the default language runtime version
description: Learn how to change the default version of the R or Python runtime used by a SQL instance with SQL Server 2017 Machine Learning Services or SQL Server R Services.
ms.custom: ""
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 07/22/2020
ms.topic: conceptual
author: garyericson
ms.author: garye
ms.reviewer: davidph
monikerRange: "=sql-server-2016||=sql-server-2017||=sqlallproducts-allversions"
---

# Change the default language runtime version

[!INCLUDE[SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

This article describes how to change the default version of R or Python used by a SQL instance in [SQL Server 2017 Machine Learning Services](../sql-server-machine-learning-services.md) or [SQL Server 2016 R Services](../r/sql-server-r-services.md).

The following lists the versions of the R and Python runtime that are included in SQL Server 2017 Machine Learning Services and  SQL Server 2016 R Services.

| SQL Server Version | R runtime versions | Python runtime version |
|-|-|-|
| SQL Server 2016 RTM - SP2 CU13 | 3.2.2 | Not available |
| SQL Server 2016 SP2 CU14 and later | 3.2.2 and 3.5.2 | Not available |
| SQL Server 2017 RTM - CU21 | 3.3.3 | 3.5.2 |
| SQL Server 2017 CU22 and later | 3.3.3 and 3.5.2 | 3.5.2 and 3.7.2 |

## Prerequisites

You need to install **Cumulative Update (CU) 22 or later for SQL Server 2017** or **Cumulative Update (CU) 14 or later for SQL Server 2016 Services Pack (SP) 2** to change the default R or Python  runtime. To download the latest Cumulative Update, see the [Latest updates for Microsoft SQL Server](../../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md).

> [!NOTE]
> If you slipstream the Cumulative Update with a new installation of SQL Server, only the newest versions of the R and Python runtime will be installed.

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"

This article describes how to change the default version of R used by a SQL instance in [SQL Server 2016 R Services](../r/sql-server-r-services.md)).

If you have installed one or more CUs, you may have multiple versions of R in a SQL instance. Each version is contained in a subfolder of the instance folder with the name `R_SERVICES.`*&lt;major&gt;*.*&lt;minor&gt;* (the folder from the original installation may not have a version number appended to the folder name).

For example, if you install a CU containing R 3.5, a new `R_SERVICES` folder is created:

`C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES.3.5`

Each SQL instance uses one of these versions as the default version of R. You can change the default version by using the **RegisterRExt.exe** command-line utility. The utility is located under the R folder in each SQL instance:

*&lt;SQL instance path&gt;*\R_SERVICES.n.n\library\RevoScaleR\rxLibs\x64\RegisterRExt.exe

::: moniker-end


::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
This article describes how to change the default version of R or Python used by a SQL instance in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md).

If you have installed one or more CUs, you may have multiple versions of R or Python in a SQL instance. Each version is contained in a subfolder of the instance folder with the name `R_SERVICES.`*&lt;major&gt;*.*&lt;minor&gt;* or `PYTHON_SERVICES.`*&lt;major&gt;*.*&lt;minor&gt;* (the folder from the original installation may not have a version number appended to the folder name).

For example, if you install a CU containing Python 3.7, a new `PYTHON_SERVICES` folder is created:

`C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES.3.7`

Each SQL instance uses one of these versions as the default version of R or Python. You can change the default version by using the **RegisterRExt.exe** command-line utility. The utility is located under both the R and Python folders in each SQL instance:

*&lt;SQL instance path&gt;*`\R_SERVICES.n.n\library\RevoScaleR\rxLibs\x64\RegisterRExt.exe`  
*&lt;SQL instance path&gt;*`\PYTHON_SERVICES.n.n\Lib\site-packages\revoscalepy\rxLibs\RegisterRExt.exe`
::: moniker-end


> [!Note]
> The functionality described in this article is available only with the copy of **RegisterRExt.exe** included in SQL CUs. Don't use the copy that came with the original SQL installation.

**RegisterRExt.exe** accepts these command-line arguments:

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
- `/configure` - Required, specifies that you're configuring the default R version.
::: moniker-end

::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
- `/configure` - Required, specifies that you're configuring the default R/Python version.

- `/python` - Specifies that you're configuring the default Python version. Optional if you specify `/pythonhome`.
::: moniker-end

- `/instance:`*&lt;instance name&gt;* - Optional, the instance you want to configure. If not specified, the default instance is configured.

- `/rhome:`*&lt;path to the R_SERVICES[n.n] folder&gt;* - Optional, the version you want to set as the default R version.
  ::: moniker range="=sql-server-2017||=sqlallproducts-allversions"  
  or  
  `/pythonhome:`*&lt;path to the PYTHON_SERVICES[n.n] folder&gt;* - Optional, the version you want to set as the default Python version.
  ::: moniker-end

  ::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
  If you don't specify /rhome, the path used is the path under which **RegisterRExt.exe** is located.
  ::: moniker-end
  ::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
  If you don't specify /rhome or /pythonhome, the path used is the path under which **RegisterRExt.exe** is located.
  ::: moniker-end


For example, to configure R 3.5 as the default version of R for the instance MSSQLSERVER01:

```cmd
cd "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\R_SERVICES.3.5\library\RevoScaleR\rxLibs\x64"

.\RegisterRExt.exe /configure /rhome:"C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\R_SERVICES.3.5" /instance:MSSQLSERVER01
```

In this example, you don't need to include the `/rhome` argument since you're specifying the same folder where **RegisterRExt.exe** is located.

## Remove a language version

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
To remove a version of R, use **RegisterRExt.exe** with the `/cleanup` command-line argument, using the same `/rhome` and `/instance` arguments described previously.

For example, to remove the R 3.5 folder from the instance MSSQLSERVER01:

```cmd
.\RegisterRExt.exe /cleanup /rhome:"C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER01\R_SERVICES.3.5" /instance:MSSQLSERVER01
```
::: moniker-end

::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
To remove a version of R or Python, use **RegisterRExt.exe** with the `/cleanup` command-line argument, using the same `/rhome`, `/pythonhome`, and `/instance` arguments described previously.

For example, to remove the Python 3.7 folder from the instance MSSQLSERVER01:

```cmd
.\RegisterRExt.exe /cleanup /python /pythonhome:"C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER01\PYTHON_SERVICES.3.7" /instance:MSSQLSERVER01
```

In this example, you don't need to specify `/python` since you're including the `/pythonhome` argument.
::: moniker-end

> [!NOTE]
> You can remove a version only if it's not configured as the default.

## Next steps

::: moniker range="=sql-server-2016||=sqlallproducts-allversions"
- [Get R package information](../package-management/r-package-information.md)
- [Install packages with R tools](../package-management/install-r-packages-standard-tools.md)
::: moniker-end
::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
- [Get R package information](../package-management/r-package-information.md)
- [Get Python package information](../package-management/python-package-information.md)
- [Install packages with R tools](../package-management/install-r-packages-standard-tools.md)
- [Install packages with Python tools](../package-management/install-python-packages-standard-tools.md)
::: moniker-end
