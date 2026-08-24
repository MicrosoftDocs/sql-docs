---
title: "sys.sp_dw_refresh_ext_table (Transact-SQL)"
description: "The sys.sp_dw_refresh_ext_table system stored procedure is used to refresh a specific table metadata in a Fabric SQL analytics endpoint."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: rakrish, anphil
ms.date: 05/18/2026
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_dw_refresh_ext_table_TSQL"
  - "sys.sp_dw_refresh_ext_table"
  - "sp_dw_refresh_ext_table_TSQL"
  - "sp_dw_refresh_ext_table"
helpviewer_keywords:
  - "sp_dw_refresh_ext_table"
dev_langs:
  - "TSQL"
monikerRange: "=fabric || =fabric-sqldb "
---
# sys.sp_dw_refresh_ext_table (Transact-SQL)

[!INCLUDE [fabric-se-fabricmirroredsqldb-fabricsqldb](../../includes/applies-to-version/fabric-se-fabricmirroredsqldb-fabricsqldb.md)]

Use the `sp_dw_refresh_ext_table` stored procedure to refresh the data for a specific table within a SQL analytics endpoint.

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sys.sp_dw_refresh_ext_table
    [ @tablename = ] "table_name"
[ ; ]
```

## Arguments

#### [ @tablename = ] "table_name"

The table name is required. The `@tablename` parameter is two-part name of the table, in quoted identifiers, to be refreshed.

## Returns

`0` (success) or `1` (failure).

## Permissions

You should have access to a [[!INCLUDE [fabric-se](../../includes/fabric-se.md)]](/fabric/data-warehouse/data-warehousing#sql-endpoint-of-the-lakehouse) within a Fabric workspace with Contributor or above permissions.

## Remarks

As of May 2026, if you are on [the new version of the metadata sync](/fabric/data-engineering/sql-analytics-endpoint-metadata-sync), then you can use `sp_dw_refresh_ext_table`.

## Examples

To refresh the table `dbo.publicholidays` in the current SQL analytics endpoint.

```sql
EXEC sys.sp_dw_refresh_ext_table "dbo.publicholidays"
```

## Related content

- [SQL analytics endpoint metadata sync](/fabric/data-engineering/sql-analytics-endpoint-metadata-sync)
- [sys.dm_db_external_tables_log_status (Transact-SQL)](../system-dynamic-management-objects/sys-dm-db-external-tables-log-status-transact-sql.md)
