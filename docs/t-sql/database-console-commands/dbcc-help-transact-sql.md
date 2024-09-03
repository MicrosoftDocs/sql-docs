---
title: "DBCC HELP (Transact-SQL)"
description: DBCC HELP returns syntax information for the specified DBCC command.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/05/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "DBCC HELP"
  - "DBCC_HELP_TSQL"
helpviewer_keywords:
  - "DBCC statement syntax information"
  - "DBCC HELP statement"
dev_langs:
  - "TSQL"
---
# DBCC HELP (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Returns syntax information for the specified DBCC command.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC HELP ( 'dbcc_statement' | @dbcc_statement_var | '?' )
[ WITH NO_INFOMSGS ]
```

## Arguments

#### *dbcc_statement* | *@dbcc_statement_var*

The name of the DBCC command for which to receive syntax information. Provide only the part of the DBCC command that follows DBCC, for example, `CHECKDB` instead of `DBCC CHECKDB`.

#### `'?'`

Returns all DBCC commands for which help is available.

#### WITH NO_INFOMSGS

Suppresses all informational messages that have severity levels from 0 through 10.

## Result sets

`DBCC HELP` returns a result set displaying the syntax for the specified DBCC command.

## Permissions

Requires membership in the **sysadmin** fixed server role.

## Examples

### A. Use DBCC HELP with a variable

The following example returns syntax information for `DBCC CHECKDB`:

```sql
DECLARE @dbcc_stmt sysname;
SET @dbcc_stmt = 'CHECKDB';
DBCC HELP (@dbcc_stmt);
GO
```

### B. Use DBCC HELP with the '?' option

The following example returns all DBCC statements for which help is available.

```sql
DBCC HELP ('?');
GO
```

## See also

- [DBCC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-transact-sql.md)
