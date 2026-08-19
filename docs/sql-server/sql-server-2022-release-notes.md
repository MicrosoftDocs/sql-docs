---
title: SQL Server 2022 Release Notes
description: Find information about SQL Server 2022 (16.x) limitations, known issues, help resources, and other release notes.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/23/2026
ms.service: sql
ms.subservice: release-landing
ms.topic: release-notes
monikerRange: ">=sql-server-2017"
---

# SQL Server 2022 release notes

[!INCLUDE [sqlserver2022](../includes/applies-to-version/sqlserver2022.md)]

This article describes requirements, limitations and known issues for [!INCLUDE [sssql22-md](../includes/sssql22-md.md)].

## Hardware and software requirements

For hardware and software requirements, see [Hardware and software requirements for SQL Server 2022](install/hardware-and-software-requirements-for-installing-sql-server-2022.md).

## Known issues

This section identifies known issues you might experience with this product. You can also review the Known Issues section of the [Cumulative Update](/troubleshoot/sql/releases/sqlserver-2022/build-versions) articles.

### SQL Setup

#### Help

When you run `setup /HELP`, the information returned doesn't include the new `/AZUREEXTENSION` feature. Complete information for Setup is available at [Install, configure, or uninstall SQL Server on Windows from the command prompt](../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).

#### Localized language interface

In certain localized languages, the Azure Extension configuration screen controls might be partially overwritten or missing. To resolve this issue, expand or maximize the Setup window from the default window sizing.

#### Software Assurance installation parameter

A new Setup command line installation parameter `/PRODUCTCOVEREDBYSA` indicates if the provided product key (`/PID=`) license is covered under a Software Assurance or SQL Server Subscription contract, or just a SQL Server license.

[Install, configure, or uninstall SQL Server on Windows from the command prompt](../database-engine/install-windows/install-sql-server-from-the-command-prompt.md) describes this parameter.

#### Deprecated feature parameters

The following features aren't available in Setup in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]. If specified in command line installations or scripts, these previously supported parameters can fail.

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

#### Restart requirement

When you install an initial [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] instance on a Windows Server 2022 machine, if the computer doesn't have `VCRuntime140` version 14.29.30139 or a later version installed, Setup requires a restart.

Windows Server 2022 was released with VCRuntime version 14.28.29914.

### Query Store for secondary replicas

[Query Store for readable secondary replicas](../relational-databases/performance/query-store-for-secondary-replicas.md) is a preview feature in SQL Server 2022. Query Store for readable secondary replicas isn't supported for use in production environments.

### Known issues with Parameter Sensitive Plan optimization

If you use the [Parameter Sensitive Plan optimization](../relational-databases/performance/parameter-sensitive-plan-optimization.md) feature, review the guidance and mitigation for known issues that can result in exceptions during Query Store cleanup process. For more information, see [Access violation exception occurs in Query Store in SQL Server 2022 under certain conditions](../relational-databases/performance/parameter-sensitive-plan-optimization.md#access-violation-exception-occurs-in-query-store-in-sql-server-2022-under-certain-conditions).

### RPC calls fail with `Encrypt=Strict`

**Applies to**: [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] RTM

An issue in the TDS 8.0 protocol implementation can cause RPC calls to fail if the `Encrypt` option is set to `Strict` in your connection string, for example when running the `sp_who` system stored procedure.

### Availability group replica manager

**Applies to**: [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] RTM

**Error 35221** states that the Always On availability groups replica manager is disabled. This error might be encountered when attempting to add a file to a FILESTREAM filegroup or a memory-optimized filegroup, or when attempting to add additional transaction log files to a database.

The fix for this issue is released in [Cumulative Update 1](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate1#1993393) for [!INCLUDE [sssql22-md](../includes/sssql22-md.md)].

To work around this issue, you can use trace flag 12324 as either as startup trace flag, or at the session level (using `DBCC TRACEON`).

### SQL Server Agent errors when using contained Availability Group

You might encounter error messages in SQL Server Agent or Database Mail when using contained Availability Group feature of [!INCLUDE [sssql22-md](../includes/sssql22-md.md)].

Review the article [Errors occur after you apply a cumulative update to an instance of SQL Server that has a contained availability group](/troubleshoot/sql/releases/sqlserver-2022/errors-apply-cu-contained-availability-group) for details on addressing the issue.

### SQL Server services are set to Automatic (Delayed Start) start mode

In [!INCLUDE [sssql22-md](../includes/sssql22-md.md)], setting the **Start Mode** for a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service to *Automatic* in Configuration Manager, configures that service to start in *Automatic (Delayed Start)* mode instead, even though the **Start Mode** shows as *Automatic*.

### MSOLEDBSQL19 and linked servers

Currently, MSOLEDBSQL19 prevents the creation of linked servers without encryption and a trusted certificate (a self-signed certificate is insufficient). If linked servers are required, use the existing supported version of MSOLEDBSQL.

### Linked server queries that use MSDASQL fail with error 7416

**Applies to**: [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] starting with CU 24.

[!INCLUDE [msdasql-linked-server-7416](../includes/msdasql-linked-server-7416.md)]

### Transaction log growth for databases with In-Memory OLTP

You might notice excessive growth in the transaction log size for databases with the [In-Memory OLTP](../relational-databases/in-memory-oltp/overview-and-usage-scenarios.md) feature enabled. This might be coupled with `XTP_CHECKPOINT` as `log_reuse_wait_desc` in [sys.databases](../relational-databases/system-catalog-views/sys-databases-transact-sql.md).

For more information, review [Transaction log file grows for databases with In-Memory OLTP in SQL Server 2022](/troubleshoot/sql/database-engine/general/transaction-log-file-grows-databases-in-memory-oltp).

### DBCC CHECKDB command reports inconsistency after dropping index

**Applies to**: [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] databases that originated from [!INCLUDE [ssazuremi-md](../includes/ssazuremi-md.md)]

You might see the following error when you run the `DBCC CHECKDB` command on a [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] database after you delete an index, or a table with an index, and the database originated from [!INCLUDE [ssazuremi-md](../includes/ssazuremi-md.md)], such as after restoring a backup file, or from the [SQL Managed Instance link feature](/azure/azure-sql/managed-instance/managed-instance-link-feature-overview):

```output
Msg 8992, Level 16, State 1, Line <Line_Number>
Check Catalog Msg 3853, State 1: Attribute (%ls) of row (%ls) in sys.sysrowsetrefs does not have a matching row (%ls) in sys.indexes.
```

To work around the issue, first drop the index, or the table with the index, from the source database in [!INCLUDE [ssazuremi-md](../includes/ssazuremi-md.md)], and then restore, or link, the database to [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] again. If recreating the database from the source [!INCLUDE [ssazuremi-md](../includes/ssazuremi-md.md)] isn't possible, contact Microsoft support to help resolve this issue.

> [!CAUTION]  
> If you create a partitioned index on a table after dropping an index as described in this scenario, the table becomes inaccessible.

### Access violations in Windows Server 2025 with LPIM enabled

[!INCLUDE [windows-server-2025-disable-lpim](../includes/windows-server-2025-disable-lpim.md)]

## Build number

For information about [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] build numbers, see [SQL Server 2022 build versions](/troubleshoot/sql/releases/sqlserver-2022/build-versions).

## Related content

- [What's new in SQL Server 2022](what-s-new-in-sql-server-2022.md)
