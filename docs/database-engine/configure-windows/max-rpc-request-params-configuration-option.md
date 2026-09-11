---
title: "Server Configuration: max RPC request params (KB)"
description: "Explains the max RPC request params instance configuration setting."
author: dimitri-furman
ms.author: dfurman
ms.reviewer: randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "Max RPC request params"
---

# Server configuration: max RPC request params (KB)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

The `max RPC request params (KB)` server configuration option limits the amount of memory used by the remote procedure call (RPC) parameters for a single batched RPC call. A batched RPC call contains one or more statements that are submitted to the server as a single batch with its associated set of parameters.

By default, the server memory that is used for the parameters of batched RPC calls is unlimited. When the total memory consumed by RPC parameters is excessively large, the current server process might be terminated because of insufficient memory.

When the `max RPC request params (KB)` configuration is set to a nonzero value, the memory used by a *single* batched RPC call is limited to the specified value. If the RPC call exceeds the memory limit, it's terminated with error 701, severity 17, state 21, message: `There is insufficient system memory in resource pool 'resource-pool-name' to run this query.` Terminating the RPC call releases the memory consumed by the RPC parameters and avoids the risk of server process termination. For more information, see [MSSQLSERVER_701](../../relational-databases/errors-events/mssqlserver-701-database-engine-error.md).

## Availability

This configuration option is available in the following SQL platforms and versions:

- [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 26 and later versions
- [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 13 and later versions
- [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]
- [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]

## Remarks

You can monitor the total server memory consumption by RPC parameters using [sys.dm_os_memory_clerks](../../relational-databases/system-dynamic-management-objects/sys-dm-os-memory-clerks-transact-sql.md), with `USERSTORE_SXC` as the memory clerk type, and using [sys.dm_os_memory_objects](../../relational-databases/system-dynamic-management-objects/sys-dm-os-memory-objects-transact-sql.md), with `MEMOBJ_PROCESSRPC` as the memory object type.

If you observe that the `USERSTORE_SXC` memory clerk or the `MEMOBJ_PROCESSRPC` memory objects consume disproportionately large amounts of memory, consider the following mitigations:

- Modify the `max RPC request params (KB)` server configuration to limit the amount of memory consumed by a single batch RPC call. The optimal value depends on the size of the parameter data used in RPC calls and the number of RPC calls executing concurrently. Set a larger value initially, then start reducing the value while monitoring memory consumption and the occurrence of out-of-memory errors (error 701, state 21) while RPC calls execute. The goal is to keep memory consumption under control while minimizing the occurrence of out-of-memory errors.
- Split large RPC calls into smaller batches. For example, instead of using 100,000 `INSERT` statements in a single RPC call, issue 10 RPC calls with 10,000 statements in each call.
- Avoid using RPC calls to insert data in bulk. Instead, use the bulk copy methods of the client driver, such as [SqlBulkCopy](/dotnet/api/microsoft.data.sqlclient.sqlbulkcopy) for SqlClient in .NET or [SQLServerBulkCopy](/java/api/com.microsoft.sqlserver.jdbc.sqlserverbulkcopy) for JDBC in Java. For more information, see [Bulk Import and Export of Data (SQL Server)](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md).

## Examples

### A. Set the maximum RPC call parameter memory

The following example sets the maximum RPC parameter memory a single RPC call can consume to 1 MB.

```sql
EXECUTE sp_configure 'show advanced options', 1;

RECONFIGURE;
GO

EXECUTE sp_configure 'max RPC request params (KB)', 1024;

RECONFIGURE;
GO
```

### B. Monitor current total and maximum RPC parameter memory

The following examples monitor the current total and the maximum memory consumed by RPC parameters:

```sql
SELECT SUM(pages_kb) / 1024. AS userstore_sxc_memory_mb
FROM sys.dm_os_memory_clerks
WHERE type = 'USERSTORE_SXC';

SELECT SUM(pages_in_bytes) / 1024. / 1024 AS total_processrpc_memory_mb,
       MAX(max_pages_in_bytes) / 1024. / 1024 AS max_processrpc_memory_mb
FROM sys.dm_os_memory_objects
WHERE type = 'MEMOBJ_PROCESSRPC';
```

## Related content

- [Server configuration options](server-configuration-options-sql-server.md)
- [Blog: Packed/Batched Transact-SQL (TSQL) RPC Invocation](https://techcommunity.microsoft.com/blog/sqlserver/packedbatched-transact-sql-tsql-rpc-invocation/3964314)
