---
title: "sp_databases (Transact-SQL)"
description: sp_databases lists databases that either reside in an instance of SQL Server, or are accessible through a database gateway.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_databases_TSQL"
  - "sp_databases"
helpviewer_keywords:
  - "sp_databases"
dev_langs:
  - "TSQL"
---
# sp_databases (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Lists databases that either reside in an instance of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or are accessible through a database gateway.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_databases
[ ; ]
```

## Return code values

None.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `DATABASE_NAME` | **sysname** | Name of the database. In the [!INCLUDE [ssDE](../../includes/ssde-md.md)], this column represents the database name as stored in the `sys.databases` catalog view. |
| `DATABASE_SIZE` | **int** | Size of database, in kilobytes. |
| `REMARKS` | **varchar(254)** | For the [!INCLUDE [ssDE](../../includes/ssde-md.md)], this field always returns `NULL`. |

## Remarks

Database names that are returned can be used as parameters in the `USE` statement to change the current database context.

`DATABASE_SIZE` returns a `NULL` value for databases larger than 2.15 TB.

`sp_databases` has no equivalent in Open Database Connectivity (ODBC).

## Permissions

Requires `CREATE DATABASE`, or `ALTER ANY DATABASE`, or `VIEW ANY DEFINITION` permission, and must have access permission to the database. Can't be denied `VIEW ANY DEFINITION` permission.

## Examples

The following example shows executing `sp_databases`.

```sql
USE master;
GO
EXEC sp_databases;
```

## Related content

- [sys.databases (Transact-SQL)](../system-catalog-views/sys-databases-transact-sql.md)
- [HAS_DBACCESS (Transact-SQL)](../../t-sql/functions/has-dbaccess-transact-sql.md)
