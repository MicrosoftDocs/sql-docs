---
title: "sys.sp_cleanup_temporal_history"
description: "Removes all rows from temporal history table that match configured HISTORY_RETENTION PERIOD within a single transaction."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.topic: conceptual
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current"
---
# sys.sp_cleanup_temporal_history (Transact-SQL)

[!INCLUDE [sqlserver2017-asdb-asdbmi](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi.md)]

Removes all rows from temporal history table that match configured `HISTORY_RETENTION_PERIOD` within a single transaction.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_cleanup_temporal_history
    [ @schema_name = ] N'schema_name'
    , [ @table_name = ] N'table_name'
    [ , [ @row_count = ] @row_count_var [ OUTPUT ] ]
```

## Arguments

#### [ @schema_name = ] N'*schema_name*'

The name of the temporal table for which retention cleanup is invoked.

#### [ @table_name = ] N'*table_name*'

The name of the schema that current temporal table belongs to.

#### [ @row_count = ] *@row_count_var* [ OUTPUT ]

The output parameter that returns number of deleted rows. If the history table has a clustered columnstore index, this parameter returns `0`.

## Remarks

This stored procedure can be used only with temporal tables that have finite retention period specified. Use this stored procedure only if you need to immediately clean all aged rows from the history table.

`sp_cleanup_temporal_history` can have a negative effect on the database log and I/O subsystem, as it deletes all eligible rows within the same transaction.

You should always rely on an internal background task for cleanup that removes aged rows, with the minimal impact on regular workloads and database in general.

## Permissions

Requires **db_owner** permissions.

## Examples

```sql
DECLARE @rowcnt INT;

EXEC sys.sp_cleanup_temporal_history 'dbo', 'Department', @rowcnt OUTPUT;

SELECT @rowcnt;
```

## Related content

- [Temporal tables retention policy](/azure/sql-database/sql-database-temporal-tables-retention-policy)
