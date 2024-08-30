---
title: "sys.sp_xtp_control_query_exec_stats (Transact-SQL)"
description: "Enables per query statistics collection for all or specific natively compiled stored procedures on an instance."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_xtp_control_query_exec_stats_TSQL"
  - "sys.sp_xtp_control_query_exec_stats"
helpviewer_keywords:
  - "sys.sp_xtp_control_query_exec_stats"
dev_langs:
  - "TSQL"
---
# sys.sp_xtp_control_query_exec_stats (Transact-SQL)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Enables per query statistics collection for all natively compiled stored procedures for the instance, or specific natively compiled stored procedures.

Performance decreases when you enable statistics collection. If you only need to troubleshoot one, or a few natively compiled stored procedures, you can enable statistics collection for just those few natively compiled stored procedures.

To enable statistics collection at the procedure level for all natively compiled stored procedures, see [sys.sp_xtp_control_proc_exec_stats](sys-sp-xtp-control-proc-exec-stats-transact-sql.md).

## Syntax

```syntaxsql
sys.sp_xtp_control_query_exec_stats
    [ [ @new_collection_value = ] collection_value ]
    [ , [ @database_id = ] database_id ]
    [ , [ @xtp_object_id = ] procedure_id ]
    , [ @old_collection_value = ] old_collection_value OUTPUT
[ ; ]
```

## Arguments

#### [ @new_collection_value = ] *collection_value*

Determines whether procedure-level statistics collection is on (`1`) or off (`0`). *@new_collection_value* is **bit**.

*@new_collection_value* is set to `0` when [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] starts.

#### [ @database_id = ] *database_id*, [ @xtp_object_id = ] *procedure_id*

The database ID and object ID (data type **int**) for the natively compiled stored procedure. If statistics collection is enabled for the instance ([sys.sp_xtp_control_proc_exec_stats](sys-sp-xtp-control-proc-exec-stats-transact-sql.md)), statistics on a natively compiled stored procedure are collected. Turning off statistics collection on the instance doesn't turn off statistics collection for individual natively compiled stored procedures.

Use [sys.databases](../system-catalog-views/sys-databases-transact-sql.md), [sys.procedures](../system-catalog-views/sys-procedures-transact-sql.md), [DB_ID](../../t-sql/functions/db-id-transact-sql.md), or [OBJECT_ID](../../t-sql/functions/object-id-transact-sql.md) to get IDs for a database and stored procedure.

#### [ @old_collection_value = ] *old_collection_value* OUTPUT

Returns the current status. *@old_collection_value* is **bit**.

## Return code values

`0` for success. Nonzero for failure.

## Permissions

Requires membership in the fixed **sysadmin** role.

## Examples

The following code sample shows how to enable statistics collection for all natively compiled stored procedures for the instance, and then for a specific natively compiled stored procedure.

```sql
DECLARE @c BIT;

EXEC sys.sp_xtp_control_query_exec_stats @new_collection_value = 1;

EXEC sys.sp_xtp_control_query_exec_stats @old_collection_value = @c OUTPUT;

SELECT @c AS 'collection status';

EXEC sys.sp_xtp_control_query_exec_stats @new_collection_value = 1,
    @database_id = 5,
    @xtp_object_id = 41576255;

EXEC sys.sp_xtp_control_query_exec_stats @database_id = 5,
    @xtp_object_id = 41576255,
    @old_collection_value = @c OUTPUT;

SELECT @c AS 'collection status';
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [In-Memory OLTP overview and usage scenarios](../in-memory-oltp/overview-and-usage-scenarios.md)
