---
title: Change the default language runtime version
description: Learn how to change the default version of the R runtime used by a SQL instance with SQL Server 2016 R Services.
ms.custom: ""
ms.prod: sql
ms.technology: machine-learning
ms.date: 07/07/2020
ms.topic: how-to
author: garyericson
ms.author: garye
ms.reviewer: davidph
monikerRange: "=sql-server-2016||=sqlallproducts-allversions"
---

# Change the default language runtime version

[!INCLUDE[sqlserver2016-md](../../includes/applies-to-version/sqlserver2016.md)]

This article describes how to change the default version of R used by a SQL instance in [SQL Server 2016 R Services](../r/sql-server-r-services.md).

The following lists the versions of the R runtime that are included in SQL Server 2016 R Services.

SQL Server Version | Default R runtime versions |
|-|-|
| SQL Server 2016 RTM - SP2 CU15 | 3.2.2 |
| SQL Server 2016 SP2 CU16 and later | 3.2.2 and 3.5.2 |

## Prerequisites

You need to install Cumulative Update (CU) 16 or later for SQL Server 2016 SP2 to change the default R runtime. To download the latest Cumulative Update, see the [Latest updates for Microsoft SQL Server](../../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md).

## Change runtime version

If you have installed Cumulative Update (CU) 16 or later for SQL Server 2016 SP2, you may have multiple versions of R in a SQL instance. Each version is contained in a subfolder of the instance folder with the name `R_SERVICES.`*&lt;major&gt;*.*&lt;minor&gt;* (the folder from the original installation may not have a version number appended to the folder name).

For example, if you install a CU containing R 3.5, a new `R_SERVICES` folder is created:

`C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES.3.5`

Each SQL instance uses one of these versions as the default version of R. You can change the default version by using the **RegisterRExt.exe** command-line utility. The utility is located under the R folder in each SQL instance:

*&lt;SQL instance path&gt;*\R_SERVICES.n.n\library\RevoScaleR\rxLibs\x64\RegisterRExt.exe

> [!Note]
> The functionality described in this article is available only with the copy of **RegisterRExt.exe** included in SQL CUs. Don't use the copy that came with the original SQL installation.

**RegisterRExt.exe** accepts these command-line arguments:

- `/configure` - Required, specifies that you're configuring the default R version.

- `/instance:`*&lt;instance name&gt;* - Optional, the instance you want to configure. If not specified, the default instance is configured.

- `/rhome:`*&lt;path to the R_SERVICES[n.n] folder&gt;* - Optional, the version you want to set as the default R version.

  If you don't specify /rhome, the path used is the path under which **RegisterRExt.exe** is located.

For example, to configure R 3.5 as the default version of R for the instance MSSQLSERVER01:

```cmd
cd "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\R_SERVICES.3.5\library\RevoScaleR\rxLibs\x64"

.\RegisterRExt.exe /configure /rhome:"C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\R_SERVICES.3.5" /instance:MSSQLSERVER01
```

In this example, you don't need to include the `/rhome` argument since you're specifying the same folder where **RegisterRExt.exe** is located.

## Remove a runtime version

To remove a version of R, use **RegisterRExt.exe** with the `/cleanup` command-line argument, using the same `/rhome` and `/instance` arguments described previously.

For example, to remove the R 3.5 folder from the instance MSSQLSERVER01:

```cmd
.\RegisterRExt.exe /cleanup /rhome:"C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER01\R_SERVICES.3.5" /instance:MSSQLSERVER01
```

> [!NOTE]
> You can remove a version only if it's not configured as the default.

## Next steps

- [Get R package information](../package-management/r-package-information.md)
- [Install packages with R tools](../package-management/install-r-packages-standard-tools.md)
