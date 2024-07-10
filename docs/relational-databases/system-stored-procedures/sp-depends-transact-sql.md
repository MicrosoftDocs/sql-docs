---
title: "sp_depends (Transact-SQL)"
description: sp_depends displays information about database object dependencies.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_depends"
  - "sp_depends_TSQL"
helpviewer_keywords:
  - "sp_depends"
dev_langs:
  - "TSQL"
---
# sp_depends (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Displays information about database object dependencies, such as the views and procedures that depend on a table or view, and the tables and views that are depended on by the view or procedure. References to objects outside the current database aren't reported.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [sys.dm_sql_referencing_entities](../system-dynamic-management-views/sys-dm-sql-referencing-entities-transact-sql.md) and [sys.dm_sql_referenced_entities](../system-dynamic-management-views/sys-dm-sql-referenced-entities-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_depends [ @objname = ] N'objname'

<objname> ::=
{
    [ database_name. [ schema_name ] . | schema_name. ]
    object_name 
}

[ ; ]
```

## Arguments

#### [ @objname = ] N'*objname*'

*@objname* is **nvarchar(776)**, with no default.

- *database_name*

  The name of the database.

- *schema_name*

  The name of the schema to which the object belongs.

- *object_name*

  The database object to examine for dependencies. The object can be a table, view, stored procedure, user-defined function, or trigger. *object_name* is **nvarchar(776)**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

`sp_depends` displays two result sets.

The following result set shows the objects on which *@objname* depends.

| Column name | Data type | Description |
| --- | --- | --- |
| `name` | **nvarchar(257)** | Name of the item for which a dependency exists. |
| `type` | **nvarchar(16)** | Type of the item. |
| `updated` | **nvarchar(7)** | Specifies whether the item is updated. |
| `selected` | **nvarchar(8)** | Specifies whether the item is used in a `SELECT` statement. |
| `column` | **sysname** | Column or parameter on which the dependency exists. |

The following result set shows the objects that depend on *@objname*.

| Column name | Data type | Description |
| --- | --- | --- |
| `name` | **nvarchar(257)** | Name of the item for which a dependency exists. |
| `type` | **nvarchar(16)** | Type of the item. |

## Permissions

Requires membership in the **public** role.

## Examples

### A. List dependencies on a table

The following example lists the database objects that depend on the `Sales.Customer` table in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. Both the schema name and table name are specified.

```sql
USE AdventureWorks2022;
GO
EXEC sp_depends @objname = N'Sales.Customer';
```

### B. List dependencies on a trigger

The following example lists the database objects on which the trigger `iWorkOrder` depends.

```sql
EXEC sp_depends @objname = N'AdventureWorks2022.Production.iWorkOrder';
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [EXECUTE (Transact-SQL)](../../t-sql/language-elements/execute-transact-sql.md)
- [sp_help (Transact-SQL)](sp-help-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [sys.sql_dependencies (Transact-SQL)](../system-catalog-views/sys-sql-dependencies-transact-sql.md)
