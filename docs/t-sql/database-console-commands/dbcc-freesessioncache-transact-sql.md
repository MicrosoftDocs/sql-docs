---
title: "DBCC FREESESSIONCACHE (Transact-SQL)"
description: DBCC FREESESSIONCACHE flushes the distributed query connection cache used by distributed queries against an instance of SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/05/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "FREESESSIONCACHE"
  - "FREESESSIONCACHE_TSQL"
  - "DBCC_FREESESSIONCACHE_TSQL"
  - "DBCC FREESESSIONCACHE"
helpviewer_keywords:
  - "DBCC FREESESSIONCACHE statement"
  - "distributed queries [SQL Server], cache"
  - "clearing distributed query cache"
  - "flushing distributed query cache"
dev_langs:
  - "TSQL"
---
# DBCC FREESESSIONCACHE (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Flushes the distributed query connection cache used by distributed queries against an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC FREESESSIONCACHE [ WITH NO_INFOMSGS ]
```

## Arguments

#### WITH NO_INFOMSGS

Suppresses all informational messages.

## Permissions

Requires membership in the **sysadmin** fixed server role.

## Examples

The following example flushes the distributed query cache.

```sql
USE AdventureWorks2022;
GO
DBCC FREESESSIONCACHE WITH NO_INFOMSGS;
GO
```

## See also

- [DBCC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-transact-sql.md)
