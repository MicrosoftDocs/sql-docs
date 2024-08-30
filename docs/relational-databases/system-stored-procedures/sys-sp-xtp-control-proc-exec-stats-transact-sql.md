---
title: "sys.sp_xtp_control_proc_exec_stats (Transact-SQL)"
description: "Enables statistics collection for natively compiled stored procedures for the instance."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_xtp_control_proc_exec_stats"
  - "sys.sp_xtp_control_proc_exec_stats_TSQL"
helpviewer_keywords:
  - "sys.sp_xtp_control_proc_exec_stats"
dev_langs:
  - "TSQL"
---
# sys.sp_xtp_control_proc_exec_stats (Transact-SQL)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Enables statistics collection for natively compiled stored procedures for the instance.

To enable statistics collection at the query level for natively compiled stored procedures, see [sys.sp_xtp_control_query_exec_stats](sys-sp-xtp-control-query-exec-stats-transact-sql.md).

## Syntax

```syntaxsql
sys.sp_xtp_control_proc_exec_stats
    [ [ @new_collection_value = ] collection_value ]
    , [ @old_collection_value = ] old_collection_value OUTPUT
[ ; ]
```

## Arguments

#### [ @new_collection_value = ] *new_collection_value*

Determines whether procedure-level statistics collection is on (`1`) or off (`0`). *@new_collection_value* is **bit**.

@new_collection_value is set to zero when [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or the database starts.

#### [ @old_collection_value = ] *old_collection_value*

Returns the current status. *@old_collection_value* is **bit**.

## Return code values

`0` for success. Nonzero for failure.

## Permissions

Requires membership in the fixed **sysadmin** role.

## Examples

To set *@new_collection_value* and query for the value of *@new_collection_value*:

```sql
EXEC sys.sp_xtp_control_proc_exec_stats @new_collection_value = 1;

DECLARE @c BIT;

EXEC sys.sp_xtp_control_proc_exec_stats @old_collection_value = @c OUTPUT;

SELECT @c AS 'collection status';
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [In-Memory OLTP overview and usage scenarios](../in-memory-oltp/overview-and-usage-scenarios.md)
