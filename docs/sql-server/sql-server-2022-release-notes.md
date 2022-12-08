---
title: "SQL Server 2022 release notes"
description: Find information about SQL Server 2022 (16.x) limitations, known issues, help resources, and other release notes.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 12/01/2022
ms.service: sql
ms.subservice: release-landing
ms.topic: "article"
ms.custom: event-tier1-build-2022
monikerRange: "= sql-server-ver16"
---

# SQL Server 2022 release notes

[!INCLUDE[sqlserver2022](../includes/applies-to-version/sqlserver2022.md)]

This article describes requirements, limitations and known issues for [!INCLUDE[sssql22-md](../includes/sssql22-md.md)].

## Hardware and software requirements

For hardware and software requirements, see [SQL Server 2022: Hardware and software requirements](install/hardware-and-software-requirements-for-installing-sql-server-2022.md).

## Feature notes

This section identifies known issues you may experience with this product.

### SQL Setup

#### Help

When you run `setup /HELP`, the information returned doesn't include the new `/AZUREEXTENSION` feature. Complete information for Setup is available at [Install SQL Server on Windows from the command prompt](../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).

#### Localized language interface

In certain localized languages, the Azure Extension configuration screen controls may be partially overwritten or missing. To resolve this issue, expand or maximize the Setup window from the default window sizing.

#### Software Assurance installation parameter

A new Setup command line installation parameter `/PRODUCTCOVEREDBYSA` indicates if the provided product key (`/PID=`) license is covered under a Software Assurance or SQL Server Subscription contract, or just a SQL Server license.

[Install SQL Server on Windows from the command prompt](../database-engine/install-windows/install-sql-server-from-the-command-prompt.md) describes this parameter.

#### Deprecated feature parameters

The following features aren't available in Setup in [!INCLUDE[sssql22-md](../includes/sssql22-md.md)]. If specified in command line installations or scripts, these previously supported parameters may fail.

:::row:::
    :::column:::
     - `/PolyBaseJava`
     - `/SQL_INST_MR`
     - `/SQL_INST_JAVA`
     - `/SQL_INST_MPY`
    :::column-end:::
    :::column:::
     - `/SQLJAVADIR`
     - `/SQL_SHARED_MPY`
     - `/SNAC_SDK`
     - `/SQL_SHARED_MR`
    :::column-end:::
    :::column:::
     - `/SDK`
     - `/DREPLAY_CTLR`
     - `/TOOLS`
     - `/DREPLAY_CLT`
    :::column-end:::
:::row-end:::

#### Reboot requirement

When you install an initial [!INCLUDE[sssql22-md](../includes/sssql22-md.md)] instance on a Windows Server 2022 machine, if the computer doesn't have `VCRuntime140` version 14.29.30139 or a later version installed, Setup will require a reboot.

Windows Server 2022 was released with VCRuntime version 14.28.29914.

### Query Store for secondary replicas

[Query Store for secondary replicas](../relational-databases/performance/query-store-for-secondary-replicas.md) is available for preview. It isn't available for use in production environments.

### RPC calls fail with `Encrypt=Strict`

An issue in the TDS 8.0 protocol implementation may cause RPC calls to fail if the `Encrypt` option is set to `Strict` in your connection string, for example when running the `sp_who` system stored procedure.

**Applies to**: [!INCLUDE[sssql22-md](../includes/sssql22-md.md)] RTM

### Availability group replica manager

**Error 35221** states that the Always On availability groups replica manager is disabled. This error may be encountered when attempting to add a file to a FILESTREAM filegroup or a memory-optimized filegroup, or when attempting to add additional transaction log files to a database.

**Applies to**: [!INCLUDE[sssql22-md](../includes/sssql22-md.md)] RTM

The fix for this issue will be released in Cumulative Update 1 for [!INCLUDE [sssql22-md](../includes/sssql22-md.md)].

To work around this issue, you can use Trace Flag 12342 as either as startup trace flag, or at the session level (using `DBCC TRACEON`).

## Build number

| Preview build | Version number | Date |
| :-- | :-- | :-- |
| General release to market (RTM) | 16.0.1000.6 | November 16, 2022 |
| RC 1 | 16.0.950.9 | September 22, 2022 |
| RC 0 | 16.0.900.6 | August 23, 2022 |
| CTP 2.1 | 16.0.700.4 | July 27, 2022 |
| CTP 2.0 | 16.0.600.9 | May 20, 2022 |

## Next steps

- [What's new in [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)]](what-s-new-in-sql-server-2022.md).
