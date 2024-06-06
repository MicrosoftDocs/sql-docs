---
title: "sp_helpsort (Transact-SQL)"
description: sp_helpsort Displays the sort order and character set for the instance of SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpsort_TSQL"
  - "sp_helpsort"
helpviewer_keywords:
  - "sp_helpsort"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_helpsort (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Displays the sort order and character set for the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpsort
[ ; ]
```

## Arguments

None.

## Return code values

`0` (success) or `1` (failure).

## Result set

Returns server default collation.

## Remarks

If an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is installed with a collation specified to be compatible with an earlier installation of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], `sp_helpsort` returns blank results. When this behavior occurs, you can determine the collation by querying the `SERVERPROPERTY` object, such as: `SELECT SERVERPROPERTY ('Collation');`.

## Permissions

Requires membership in the **public** role.

## Examples

The following example displays the name of the default sort order of the server, its character set, and a table of its primary sort values.

```sql
EXEC sp_helpsort;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Server default collation
------------------------
Latin1-General , case-sensitive , accent-sensitive , kanatype-insensitive , width-insensitive for Unicode Data , SQL Server Sort Order 51 on Code Page 1252 for non-Unicode Data.
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [COLLATE (Transact-SQL)](../../t-sql/statements/collations.md)
- [sys.fn_helpcollations (Transact-SQL)](../system-functions/sys-fn-helpcollations-transact-sql.md)
- [SERVERPROPERTY (Transact-SQL)](../../t-sql/functions/serverproperty-transact-sql.md)
