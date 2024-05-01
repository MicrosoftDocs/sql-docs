---
title: "sp_delete_log_shipping_primary_database (Transact-SQL)"
description: Removes log shipping of primary database including backup job, and local and remote history.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_delete_log_shipping_primary_database"
  - "sp_delete_log_shipping_primary_database_TSQL"
helpviewer_keywords:
  - "sp_delete_log_shipping_primary_database"
dev_langs:
  - "TSQL"
---
# sp_delete_log_shipping_primary_database (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This stored procedure removes log shipping of primary database including backup job, local and remote history. Only use this stored procedure after you remove the secondary databases using `sp_delete_log_shipping_primary_secondary`.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_delete_log_shipping_primary_database
    [ @database = ] N'database'
    [ , [ @ignoreremotemonitor = ] ignoreremotemonitor ]
[ ; ]
```

## Arguments

#### [ @database = ] N'*database*'

The name of the log shipping primary database. *@database* is **sysname**, with no default, and can't be `NULL`.

#### [ @ignoreremotemonitor = ] *ignoreremotemonitor*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_delete_log_shipping_primary_database` must be run from the `master` database on the primary server. This stored procedure performs the following steps:

1. Deletes the backup job for the specified primary database.

1. Removes the local monitor record in `log_shipping_monitor_primary` on the primary server.

1. Removes corresponding entries in `log_shipping_monitor_history_detail` and `log_shipping_monitor_error_detail`.

1. If the monitor server is different from the primary server, it removes the monitor record in `log_shipping_monitor_primary` on the monitor server.

1. Removes corresponding entries in `log_shipping_monitor_history_detail` and `log_shipping_monitor_error_detail` on the monitor server.

1. Removes the entry in `log_shipping_primary_databases` for this primary database.

1. Calls `sp_delete_log_shipping_alert_job` on the monitor server.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Examples

This example illustrates using `sp_delete_log_shipping_primary_database` to delete the primary database [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)].

```sql
EXEC master.dbo.sp_delete_log_shipping_primary_database
    @database = N'AdventureWorks2022';
GO
```

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
