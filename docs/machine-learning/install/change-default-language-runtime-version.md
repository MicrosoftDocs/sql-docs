---
title: Change the default language runtime version
description: Learn how to change the default version of the R runtime used by a SQL instance with SQL Server 2016 R Services.
ms.custom: ""
ms.prod: sql
ms.technology: machine-learning
ms.date: 08/06/2020
ms.topic: how-to
author: garyericson
ms.author: garye
ms.reviewer: davidph
monikerRange: "=sql-server-2016||=sqlallproducts-allversions"
---
# Change the default language runtime version

[!INCLUDE[SQL Server 2016 only](../../includes/applies-to-version/sqlserver2016-only.md)]

This article describes how to change the default version of R used by a SQL instance in [SQL Server 2016 R Services](../r/sql-server-r-services.md).

The following lists the versions of the R runtime that are included in SQL Server 2016 R Services.

| SQL Server Version | R runtime versions |
|-|-|
| SQL Server 2016 RTM - SP2 CU13 | 3.2.2 |
| SQL Server 2016 SP2 CU14 and later | 3.2.2 and 3.5.2 |

## Prerequisites

You need to install **Cumulative Update (CU) 14 or later for SQL Server 2016 Services Pack (SP) 2** to change the default R runtime. To download the latest Cumulative Update, see the [Latest updates for Microsoft SQL Server](../../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md).

> [!NOTE]
> If you slipstream CU14 or later with a new installation of SQL Server 2016 SP2, only the newest version of the R runtime will be installed.

## Change runtime version

If you have installed CU14 or later for SQL Server 2016 SP2, you may have multiple versions of R in a SQL instance. Each version is contained in a subfolder of the instance folder with the name `R_SERVICES.`*&lt;major&gt;*.*&lt;minor&gt;* (the folder from the original installation may not have a version number appended to the folder name).

For example, if you install a CU containing R 3.5, a new `R_SERVICES` folder is created:

`C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES.3.5`

Each SQL instance uses one of these versions as the default version of R. You can change the default version by using the **RegisterRext.exe** command-line utility. The utility is located under the R folder in each SQL instance:

*&lt;SQL instance path&gt;*\R_SERVICES.n.n\library\RevoScaleR\rxLibs\x64\RegisterRext.exe

> [!Note]
> The functionality described in this article is available only with the copy of **RegisterRext.exe** included in SQL CUs. Don't use the copy that came with the original SQL installation.

**RegisterRext.exe** accepts these command-line arguments:

- `/configure` - Required, specifies that you're configuring the default R version.

- `/instance:`*&lt;instance name&gt;* - Optional, the instance you want to configure. If not specified, the default instance is configured.

- `/rhome:`*&lt;path to the R_SERVICES[n.n] folder&gt;* - Optional, the version you want to set as the default R version.

  If you don't specify /rhome, the path used is the path under which **RegisterRext.exe** is located.

For example, to configure **R 3.5** as the default version of R for the instance MSSQLSERVER01:

```cmd
cd "C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER01\R_SERVICES.3.5\library\RevoScaleR\rxLibs\x64"

.\RegisterRext.exe /configure /rhome:"C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER01\R_SERVICES.3.5" /instance:MSSQLSERVER01
```

In this example, you don't need to include the `/rhome` argument since you're specifying the same folder where **RegisterRext.exe** is located.

## Remove a runtime version

To remove a version of R, use **RegisterRext.exe** with the `/cleanup` command-line argument, using the same `/rhome` and `/instance` arguments described previously.

For example, to remove the **R 3.2** folder from the instance MSSQLSERVER01:

```cmd
.\RegisterRext.exe /cleanup /rhome:"C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER01\R_SERVICES" /instance:MSSQLSERVER01
```

**RegisterRext.exe** will ask you to confirm the clean up of the specified R runtime:

> *Are you sure you want to permanently delete the given runtime along with all the packages installed on it? \[Yes(Y)/No(N)/Default(Yes)\]:*

To confirm, answer `Y` or press enter. Alternatively, you can skip this prompt by passing in `/y` or `/Yes` along the `/cleanup` option. For example:

```cmd
.\RegisterRext.exe /cleanup /rhome:"C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER01\R_SERVICES" /instance:MSSQLSERVER01 /y
```

> [!NOTE]
> You can remove a version only if it's not configured as the default and it’s not currently being used to run **RegisterRext.exe**.

## Next steps

- [Get R package information](../package-management/r-package-information.md)
- [Install packages with R tools](../package-management/install-r-packages-standard-tools.md)
