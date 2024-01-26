---
title: "sys.sp_flush_commit_table_on_demand (Transact-SQL)"
description: Deletes rows from syscommittab in batches.
author: JetterMcTedder
ms.author: bspendolini
ms.reviewer: randolphwest
ms.date: 07/06/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_flush_commit_table_on_demand "
  - "sp_flush_commit_table_on_demand_TSQL"
  - "sys.sp_flush_commit_table_on_demand"
  - "sys.sp_flush_commit_table_on_demand_TSQL"
helpviewer_keywords:
  - "sys.sp_flush_commit_table_on_demand"
  - "sp_flush_commit_table_on_demand"
dev_langs:
  - "TSQL"
---
# sys.sp_flush_commit_table_on_demand (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deletes rows from `syscommittab` in batches.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_flush_commit_table_on_demand
    [ @numrows = ] numrows
    , [ @deleted_rows = ] deleted_rows OUTPUT
    , [ @date_cleanedup = ] date_cleanedup OUTPUT
    , [ @cleanup_ts = ] cleanup_ts OUTPUT
[ ; ]
```

## Arguments

#### [ @numrows = ] *numrows*

Specifies the number of rows you want to delete from syscommittab. *@numrows* is **bigint**, and can't be NULL.

#### [ @deleted_rows = ] *deleted_rows* OUTPUT

*@deleted_rows* is an OUTPUT parameter of type **bigint**.

#### [ @date_cleanedup = ] *date_cleanedup* OUTPUT

*@date_cleanedup* is an OUTPUT parameter of type **datetime**.

#### [ @cleanup_ts = ] *cleanup_ts* OUTPUT

*@cleanup_ts* is an OUTPUT parameter of type **bigint**.

## Return code values

`0` (success) or `1` (failure).

## Examples

```sql
DECLARE @deleted_rows BIGINT;
DECLARE @date_cleanedup DATETIME;
DECLARE @cleanup_ts BIGINT;

EXEC sys.sp_flush_commit_table_on_demand 3000,
    @deleted_rows = @deleted_rows OUTPUT,
    @date_cleanedup = @date_cleanedup OUTPUT,
    @cleanup_ts = @cleanup_ts OUTPUT;

PRINT CONCAT ('Number of rows deleted: ', @deleted_rows);
PRINT CONCAT ('Cleanup date: ', @date_cleanedup);
PRINT CONCAT ('Change tracking version: ', @cleanup_ts);
GO
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
Started executing query at Line 1
The value returned by change_tracking_hardened_cleanup_version() is 17.
The value returned by safe_cleanup_version() is 17.
(0 rows affected)
Number of rows deleted: 100
Cleanup date: Aug 29 2022  8:59PM
Change tracking Version: 17
Total execution time: 00:00:02.008
```

## Remarks

This procedure must be run in a database that has change tracking enabled.

## Permissions

Only a member of the **sysadmin** server role or **db_owner** database role can execute this procedure.

## Related content

- [About change tracking (Transact-SQL)](../track-changes/about-change-tracking-sql-server.md)
- [Change tracking cleanup and Troubleshooting (Transact-SQL)](../track-changes/cleanup-and-troubleshoot-change-tracking-sql-server.md)
- [Change tracking functions (Transact-SQL)](../system-functions/change-tracking-functions-transact-sql.md)
- [Change tracking system tables (Transact-SQL)](../system-tables/change-tracking-tables-transact-sql.md)
- [Change tracking stored procedures (Transact-SQL)](change-tracking-stored-procedures-transact-sql.md)
