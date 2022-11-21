---
title: Change the default R or Python language runtime version
description: Learn how to change the default version of the R or Python runtime used by a SQL instance with SQL Server 2017 Machine Learning Services or SQL Server R Services.
ms.custom: ""
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 08/14/2020
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf

monikerRange: "=sql-server-2016||=sql-server-2017"
---
# Change the default R or Python language runtime version

[!INCLUDE[SQL Server 2016 and 2017 only](../../includes/applies-to-version/sqlserver2016-2017-only.md)]

This article describes how to change the default version of R or Python used in [SQL Server 2016 R Services](../r/sql-server-r-services.md) or [SQL Server 2017 Machine Learning Services](../sql-server-machine-learning-services.md).

The following lists the versions of the R and Python runtime that are included in the different SQL Server versions.

| SQL Server version | Service | Cumulative Update | R runtime versions | Python runtime version |
|-|-|-|-|-|
| SQL Server 2016 | R Services | RTM - SP2 CU13 | 3.2.2 | Not available |
| SQL Server 2016 | R Services | SP2 CU14 and later | 3.2.2 and 3.5.2 | Not available |
| SQL Server 2017 | Machine Learning Services | RTM - CU21 | 3.3.3 | 3.5.2 |
| SQL Server 2017 | Machine Learning Services | CU22 and later | 3.3.3 and 3.5.2 | 3.5.2 and 3.7.2 |

## Prerequisites

You need to install a Cumulative Update (CU) to change the default R or Python language runtime version:

- **SQL Server 2016:** Services Pack (SP) 2 Cumulative Update (CU) 14 or later
- **SQL Server 2017:** Cumulative Update (CU) 22 or later

To download the latest Cumulative Update, see the [Latest updates for Microsoft SQL Server](../../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md).

> [!NOTE]
> If you slipstream the Cumulative Update with a new installation of SQL Server, only the newest versions of the R and Python runtime will be installed.

## Change R runtime version

If you have installed one of the above Cumulative Updates for SQL Server 2016 or 2017, you may have multiple versions of R in a SQL instance. Each version is contained in a subfolder of the instance folder with the name `R_SERVICES.`*&lt;major&gt;*.*&lt;minor&gt;* (the folder from the original installation may not have a version number appended to the folder name).

If you install a CU containing R 3.5, the new `R_SERVICES` folder is:

- SQL Server 2016: `C:\Program Files\Microsoft SQL Server\MSSQL13.<INSTANCE_NAME>\R_SERVICES.3.5`
- SQL Server 2017: `C:\Program Files\Microsoft SQL Server\MSSQL14.<INSTANCE_NAME>\R_SERVICES.3.5`

Each SQL instance uses one of these versions as the default version of R. You can change the default version by using the **RegisterRext.exe** command-line utility. The utility is located under the R folder in each SQL instance:

*&lt;SQL instance path&gt;*\R_SERVICES.n.n\library\RevoScaleR\rxLibs\x64\RegisterRext.exe

> [!Note]
> The functionality described in this article is available only with the copy of **RegisterRext.exe** included in SQL CUs. Don't use the copy that came with the original SQL installation.

To change the R runtime version, pass the following command line arguments to **RegisterRext.exe**:

- `/configure` - Required, specifies that you're configuring the default R version.

- `/instance:`*&lt;instance name&gt;* - Optional, the instance you want to configure. If not specified, the default instance is configured.

- `/rhome:`*&lt;path to the R_SERVICES[n.n] folder&gt;* - Optional, path to the runtime version folder you want to set as the default R version.

  If you don't specify /rhome, the path configured is the path under which **RegisterRext.exe** is located.

### Examples

Below are examples on how to change the R runtime version in SQL Server 2016 and 2017.

#### Change R runtime version in SQL Server 2016

For example, to configure **R 3.5** as the default version of R for the instance MSSQLSERVER01 on SQL Server 2016:

```cmd
cd "C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER01\R_SERVICES.3.5\library\RevoScaleR\rxLibs\x64"

.\RegisterRext.exe /configure /rhome:"C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER01\R_SERVICES.3.5" /instance:MSSQLSERVER01
```

#### Change R runtime version in SQL Server 2017

For example, to configure **R 3.5** as the default version of R for the instance MSSQLSERVER01 on SQL Server 2017:

```cmd
cd "C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER01\R_SERVICES.3.5\library\RevoScaleR\rxLibs\x64"

.\RegisterRext.exe /configure /rhome:"C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER01\R_SERVICES.3.5" /instance:MSSQLSERVER01
```

In these examples, you don't need to include the `/rhome` argument since you're specifying the same folder where **RegisterRext.exe** is located.

## Change Python runtime version

If you have installed CU22 or later for SQL Server 2017, you may have multiple versions of Python in a SQL instance. Each version is contained in a subfolder of the instance folder with the name `PYTHON_SERVICES.`*&lt;major&gt;*.*&lt;minor&gt;* (the folder from the original installation may not have a version number appended to the folder name).

For example, if you install a CU containing Python 3.7, a new `PYTHON_SERVICES` folder is created:

`C:\Program Files\Microsoft SQL Server\MSSQL14.<INSTANCE_NAME>\PYTHON_SERVICES.3.7`  

Each SQL instance uses one of these versions as the default version of Python. You can change the default version by using the **RegisterRExt.exe** command-line utility. The utility is located under the Python folders in each SQL instance:

*&lt;SQL instance path&gt;*`\PYTHON_SERVICES.n.n\Lib\site-packages\revoscalepy\rxLibs\RegisterRExt.exe`

> [!Note]
> The functionality described in this article is available only with the copy of **RegisterRExt.exe** included in SQL CUs. Don't use the copy that came with the original SQL installation.

To change the Python runtime version, pass the following command line arguments to **RegisterRext.exe**:

- `/configure` - Required, specifies that you're configuring the default Python version.

- `/python` - Specifies that you're configuring the default Python version. Optional if you specify `/pythonhome`.

- `/instance:`*&lt;instance name&gt;* - Optional, the instance you want to configure. If not specified, the default instance is configured.

- `/pythonhome:`*&lt;path to the PYTHON_SERVICES[n.n] folder&gt;* - Optional, path to the runtime version folder you want to set as the default Python version.

  If you don't specify /pythonhome, the path configured is the path under which **RegisterRExt.exe** is located.

### Example

For example, to configure **Python 3.7** as the default version of Python for the instance MSSQLSERVER01 on SQL Server 2017:

```cmd
cd "C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER01\PYTHON_SERVICES.3.7\Lib\site-packages\revoscalepy\rxLibs"

.\RegisterRext.exe /configure /pythonhome:"C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES.3.7" /instance:MSSQLSERVER01
```

In this example, you don't need to include the `/pythonhome` argument since you're specifying the same folder where **RegisterRext.exe** is located.

## Remove a runtime version

To remove a version of R or Python, use **RegisterRExt.exe** with the `/cleanup` command-line argument, using the same `/rhome`, `/pythonhome`, and `/instance` arguments described previously.

For example, to remove the **R 3.2** folder from the instance MSSQLSERVER01:

```cmd
.\RegisterRext.exe /cleanup /rhome:"C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER01\R_SERVICES" /instance:MSSQLSERVER01
```

For example, to remove the **Python 3.7** folder from the instance MSSQLSERVER01:

```cmd
.\RegisterRExt.exe /cleanup /python /pythonhome:"C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER01\PYTHON_SERVICES.3.7" /instance:MSSQLSERVER01
```

**RegisterRext.exe** will ask you to confirm the clean up of the specified R runtime:

> *Are you sure you want to permanently delete the given runtime along with all the packages installed on it? \[Yes(Y)/No(N)/Default(Yes)\]:*

To confirm, answer `Y` or press enter. Alternatively, you can skip this prompt by passing in `/y` or `/Yes` along the `/cleanup` option.

> [!NOTE]
> You can remove a version only if it's not configured as the default and it's not currently being used to run **RegisterRext.exe**.

## Next steps

- [Get R package information](../package-management/r-package-information.md)
- [Get Python package information](../package-management/python-package-information.md)
- [Install packages with R tools](../package-management/install-r-packages-standard-tools.md)
- [Install packages with Python tools](../package-management/install-python-packages-standard-tools.md)
