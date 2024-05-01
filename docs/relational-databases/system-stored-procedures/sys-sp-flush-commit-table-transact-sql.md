---
title: "sys.sp_flush_commit_table (Transact-SQL)"
description: Flushes the in memory syscommittab to disk to help with Change Tracking cleanup.
author: JetterMcTedder
ms.author: bspendolini
ms.reviewer: randolphwest
ms.date: 07/06/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_flush_commit_table"
  - "sp_flush_commit_table_TSQL"
  - "sys.sp_flush_commit_table"
  - "sys.sp_flush_commit_table_TSQL"
helpviewer_keywords:
  - "sys.sp_flush_commit_tables"
  - "sp_flush_commit_tables"
dev_langs:
  - "TSQL"
---
# sys.sp_flush_commit_table (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Flushes the in memory `syscommittab` to disk to help with change tracking cleanup.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_flush_commit_table
    [ @flush_ts = ] flush_ts
    [ , [ @cleanup_version = ] cleanup_version ]
[ ; ]
```

## Arguments

#### [ @flush_ts = ] *flush_ts*

Specifies the current change tracking version. *@flush_ts* is **bigint**, and can't be NULL.

#### [ @cleanup_version = ] *cleanup_version*

The watermark change tracking version for `syscommittab` cleanup. *@cleanup_version* is **bigint**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Examples

```sql
EXEC sys.sp_flush_commit_table 11;
GO
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
Started executing query at Line 1
(10 rows affected)
Total execution time: 00:00:00.076
```

## Remarks

This procedure must be run in a database that has change tracking enabled.

## Permissions

Only a member of the **sysadmin** server role or **db_owner** database role can execute this procedure.

## Related content

- [About change tracking (Transact-SQL)](../track-changes/about-change-tracking-sql-server.md)
- [Change tracking cleanup and troubleshooting (Transact-SQL)](../track-changes/cleanup-and-troubleshoot-change-tracking-sql-server.md)
- [Change tracking functions (Transact-SQL)](../system-functions/change-tracking-functions-transact-sql.md)
- [Change tracking system tables (Transact-SQL)](../system-tables/change-tracking-tables-transact-sql.md)
- [Change tracking stored procedures (Transact-SQL)](change-tracking-stored-procedures-transact-sql.md)
