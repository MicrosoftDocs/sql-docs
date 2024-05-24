---
title: "sp_helpindex (Transact-SQL)"
description: sp_helpindex reports information about the indexes on a table or view.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpindex_TSQL"
  - "sp_helpindex"
helpviewer_keywords:
  - "sp_helpindex"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_helpindex (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Reports information about the indexes on a table or view.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpindex [ @objname = ] N'objname'
[ ; ]
```

## Arguments

#### [ @objname = ] N'*objname*'

The qualified or nonqualified name of a user-defined table or view. *@objname* is **nvarchar(776)**, with no default. Quotation marks are required only if a qualified table or view name is specified. If a fully qualified name, including a database name, is provided, the database name must be the name of the current database.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `index_name` | **sysname** | Index name. |
| `index_description` | **varchar(210)** | Index description, including the filegroup in which it's located. |
| `index_keys` | **nvarchar(2078)** | Table or view columns upon which the index is built. |

A descending indexed column is listed in the result set with a minus sign (`-`) following its name; an ascending indexed column, the default, is listed by its name alone.

## Remarks

If indexes are set by using the `NORECOMPUTE` option of `UPDATE STATISTICS`, that information is included in the `index_description` column.

`sp_helpindex` exposes only orderable index columns; therefore, it doesn't expose information about XML indexes or spatial indexes.

## Permissions

Requires membership in the **public** role.

## Examples

The following example reports on the types of indexes on the `Customer` table in [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)].

```sql
USE AdventureWorks2022;
GO
EXEC sp_helpindex N'Sales.Customer';
GO
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [sys.indexes (Transact-SQL)](../system-catalog-views/sys-indexes-transact-sql.md)
- [sys.index_columns (Transact-SQL)](../system-catalog-views/sys-index-columns-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [UPDATE STATISTICS (Transact-SQL)](../../t-sql/statements/update-statistics-transact-sql.md)
