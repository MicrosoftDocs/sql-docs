---
title: "sp_helptrigger (Transact-SQL)"
description: sp_helptrigger returns the type or types of DML triggers defined on the specified table for the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helptrigger"
  - "sp_helptrigger_TSQL"
helpviewer_keywords:
  - "sp_helptrigger"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_helptrigger (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the type or types of data manipulation language (DML) triggers defined on the specified table for the current database. `sp_helptrigger` can't be used with data definition language (DDL) triggers. Query the [sys.triggers](../system-catalog-views/sys-triggers-transact-sql.md) catalog view instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helptrigger
    [ @tabname = ] N'tabname'
    [ , [ @triggertype = ] 'triggertype' ]
[ ; ]
```

## Arguments

#### [ @tabname = ] N'*tabname*'

The name of the table in the current database for which to return trigger information. *@tabname* is **nvarchar(776)**, with no default.

#### [ @triggertype = ] '*triggertype*'

The type of DML trigger to return information about. *@triggertype* is **char(6)**, and can be one of these values.

| Value | Description |
| --- | --- |
| `DELETE` | Returns `DELETE` trigger information. |
| `INSERT` | Returns `INSERT` trigger information. |
| `UPDATE` | Returns `UPDATE` trigger information. |

## Return code values

`0` (success) or `1` (failure).

## Result set

The following table shows the information that is contained in the result set.

| Column name | Data type | Description |
| --- | --- | --- |
| `trigger_name` | **sysname** | Name of the trigger. |
| `trigger_owner` | **sysname** | Name of the owner of the table on which the trigger is defined. |
| `isupdate` | **int** | `1` = `UPDATE` trigger<br /><br />`0` = Not an `UPDATE` trigger |
| `isdelete` | **int** | `1` = `DELETE` trigger<br /><br />`0` = Not a `DELETE` trigger |
| `isinsert` | **int** | `1` = `INSERT` trigger<br /><br />`0` = Not an `INSERT` trigger |
| `isafter` | **int** | `1` = `AFTER` trigger<br /><br />`0` = Not an `AFTER` trigger |
| `isinsteadof` | **int** | `1` = `INSTEAD OF` trigger<br /><br />`0` = Not an `INSTEAD OF` trigger |
| `trigger_schema` | **sysname** | Name of the schema to which the trigger belongs. |

## Permissions

Requires [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md) permission on the table.

## Examples

The following example executes `sp_helptrigger` to produce information about the triggers on the `Person.Person` table in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
GO
EXEC sp_helptrigger 'Person.Person';
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [ALTER TRIGGER (Transact-SQL)](../../t-sql/statements/alter-trigger-transact-sql.md)
- [CREATE TRIGGER (Transact-SQL)](../../t-sql/statements/create-trigger-transact-sql.md)
- [DROP TRIGGER (Transact-SQL)](../../t-sql/statements/drop-trigger-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
