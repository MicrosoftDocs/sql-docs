---
title: "sp_wait_for_database_copy_sync"
titleSuffix: Azure SQL Database
description: "sp_wait_for_database_copy_sync is scoped to an active geo-replication relationship between a primary and secondary."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/27/2023
ms.service: azure-sql-database
ms.topic: "reference"
f1_keywords:
  - "sp_wait_for_database_copy_sync_TSQL"
  - "sp_wait_for_database_copy_sync"
helpviewer_keywords:
  - "sp_wait_for_database_copy_sync"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_wait_for_database_copy_sync (Active geo-replication)

[!INCLUDE [Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

This procedure is scoped to an [!INCLUDE [ssGeoDR](../../includes/ssgeodr-md.md)] relationship between a primary and secondary. Calling the `sys.sp_wait_for_database_copy_sync` causes the application to wait until all committed transactions are replicated and acknowledged by the active secondary database. Run `sys.sp_wait_for_database_copy_sync` on only the primary database.

## Syntax

```syntaxsql
sp_wait_for_database_copy_sync [ @target_server = ] 'server_name'
     , [ @target_database = ] 'database_name'
```

## Arguments

#### [ @target_server = ] '*server_name*'

The name of the Azure SQL Database server that hosts the active secondary database. *server_name* is **sysname**, with no default.

#### [ @target_database = ] '*database_name*'

The name of the active secondary database. *database_name* is **sysname**, with no default.

## Return code values

Returns 0 for success or an error number for failure.

The most likely error conditions are as follows:

- The server name or database name is missing.

- The link can't be found to the specified server name or database.

- Interlink connectivity has been lost, and `sys.sp_wait_for_database_copy_sync` will return after the connection timeout.

## Permissions

Any user in the primary database can call this system stored procedure. The login must be a user in both the primary and active secondary databases.

## Remarks

All transactions committed before a `sp_wait_for_database_copy_sync` call are sent to the active secondary database.

## Examples

The following example invokes `sp_wait_for_database_copy_sync` to ensure that all transactions are committed to the primary database, `AdventureWorks`, get sent to its active secondary database on the target server `serverSecondary`.

```sql
USE AdventureWorks;
GO
EXEC sys.sp_wait_for_database_copy_sync @target_server = N'serverSecondary', @target_database = N'AdventureWorks';
GO
```

## Related content

- [sys.dm_continuous_copy_status (Azure SQL Database)](../system-dynamic-management-views/sys-dm-continuous-copy-status-azure-sql-database.md)
- [Geo-Replication Dynamic Management Views (DMVs) and Functions (Azure SQL Database)](../system-dynamic-management-views/geo-replication-dynamic-management-views-and-functions-azure-sql-database.md)
- [sys.dm_geo_replication_link_status](../system-dynamic-management-views/sys-dm-geo-replication-link-status-azure-sql-database.md)
