---
title: "DBCC PROCCACHE (Transact-SQL)"
description: DBCC PROCCACHE displays information in a table format about the procedure cache.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/05/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "DBCC PROCCACHE"
  - "DBCC_PROCCACHE_TSQL"
  - "PROCCACHE_TSQL"
  - "PROCCACHE"
helpviewer_keywords:
  - "procedure cache [SQL Server]"
  - "displaying procedure cache information"
  - "DBCC PROCCACHE statement"
dev_langs:
  - "TSQL"
---

# DBCC PROCCACHE (Transact-SQL)

[!INCLUDE [SQL Server SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Displays information in a table format about the procedure cache.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```sql
DBCC PROCCACHE [ WITH NO_INFOMSGS ]
```

## Arguments

#### WITH

Allows for options to be specified.

#### NO_INFOMSGS

Suppresses all informational messages that have severity levels 0 through 10.

## Remarks

The procedure cache is used to cache the compiled and executable plans to speed up the execution of batches. The entries in a procedure cache are at a batch level. The procedure cache includes the following entries:

- Compiled plans
- Execution plans
- Algebrizer tree
- Extended procedures

## Result sets

The following table describes the columns of the result set.

| Column name | Description |
| --- | --- |
| **num proc buffs** | Total number of pages used by all entries in the procedure cache. |
| **num proc buffs used** | Total number of pages used by all entries that are currently being used. |
| **num proc buffs active** | For backward compatibility only. Total number of pages used by all entries that are currently being used. |
| **proc cache size** | Total number of entries in the procedure cache. |
| **proc cache used** | Total number of entries that are currently being used. |
| **proc cache active** | For backward compatibility only. Total number of entries that are currently being used. |

## Permissions

Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.

## See also

- [DBCC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-transact-sql.md)
