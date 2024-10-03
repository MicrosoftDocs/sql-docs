---
title: DBCC DROPCLEANBUFFERS (Transact-SQL)
description: DBCC DROPCLEANBUFFERS removes all clean buffers from the buffer pool, and columnstore objects from the columnstore object pool.
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/23/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "DROPCLEANBUFFERS"
  - "DBCC_DROPCLEANBUFFERS_TSQL"
  - "DROPCLEANBUFFERS_TSQL"
  - "DBCC DROPCLEANBUFFERS"
helpviewer_keywords:
  - "clean buffers"
  - "cold buffer cache"
  - "buffer pools [SQL Server]"
  - "dropping buffers"
  - "removing buffers"
  - "DBCC DROPCLEANBUFFERS statement"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# DBCC DROPCLEANBUFFERS (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Removes all clean buffers from the buffer pool, and columnstore objects from the columnstore object pool.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)],  [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE[ssSOD](../../includes/sssodfull-md.md)]:

```syntaxsql
DBCC DROPCLEANBUFFERS [ WITH NO_INFOMSGS ]
```

Syntax for [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]:

```syntaxsql
DBCC DROPCLEANBUFFERS ( COMPUTE | ALL ) [ WITH NO_INFOMSGS ]
```

## Arguments

#### WITH NO_INFOMSGS

Suppresses all informational messages. Informational messages are always suppressed on [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].

#### COMPUTE

Purge the data cache in memory from each Compute node.

#### ALL

Purge the data cache in memory from each Compute node and from the Control node. This setting is the default if you don't specify a value.

## Remarks

Use `DBCC DROPCLEANBUFFERS` to test queries with a cold buffer cache without shutting down and restarting the server.
To drop clean buffers from the buffer pool and columnstore objects from the columnstore object pool, first use CHECKPOINT to produce a cold buffer cache. CHECKPOINT forces all dirty pages for the current database to be written to disk and cleans the buffers. After you checkpoint the database, you can issue `DBCC DROPCLEANBUFFERS` command to remove all buffers from the buffer pool.

In Azure SQL Database, `DBCC DROPCLEANBUFFERS` acts on the database engine instance hosting the current database or elastic pool. Executing `DBCC DROPCLEANBUFFERS` in a user database drops clean buffers for that database. If the database is in an elastic pool, it also drops clean buffers in all other databases in that elastic pool. Executing the command in the `master` database has no effect on other databases on the same logical server. Executing this command in a database using Basic, S0, or S1 service objective may drop clean buffers in other databases using these service objectives on the same logical server.

## Result sets

`DBCC DROPCLEANBUFFERS` on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns:

```output
DBCC execution completed. If DBCC printed error messages, contact your system administrator.
```

## Permissions

Applies to: SQL Server, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

- Requires **sysadmin** permission on the server

Applies to: SQL Server 2022 and later

- Requires **ALTER SERVER STATE** permission on the server

Applies to: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]

- Requires membership in server role **##MS_ServerStateManager##**

Applies to: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]

- Requires membership in the **db_owner** fixed server role

## See also

- [DBCC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-transact-sql.md)
- [CHECKPOINT (Transact-SQL)](../../t-sql/language-elements/checkpoint-transact-sql.md)
