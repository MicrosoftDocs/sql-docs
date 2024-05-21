---
title: "sp_helpconstraint (Transact-SQL)"
description: sp_helpconstraint returns a list of all constraint types and their information.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpconstraint"
  - "sp_helpconstraint_TSQL"
helpviewer_keywords:
  - "sp_helpconstraint"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_helpconstraint (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns a list of all constraint types, their user-defined or system-supplied name, the columns on which they're defined, and the expression that defines the constraint (for `DEFAULT` and `CHECK` constraints only).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpconstraint
    [ @objname = ] N'objname'
    [ , [ @nomsg = ] 'nomsg' ]
[ ; ]
```

## Arguments

#### [ @objname = ] N'*objname*'

Specifies the table for which the constraint information is returned. *@objname* is **nvarchar(776)**, with no default. The table specified must be local to the current database.

#### [ @nomsg = ] '*nomsg*'

An optional parameter that prints the table name. *@nomsg* is **varchar(5)**, with a default of `msg`. `nomsg` suppresses the printing.

## Return code values

`0` (success) or `1` (failure).

## Result set

`sp_helpconstraint` displays a descending indexed column if it participated in primary keys. The descending indexed column is listed in the result set with a minus sign (`-`) following its name. The default, an ascending indexed column, is listed by its name alone.

## Remarks

Executing `sp_help <table>` reports all information about the specified table. To see only the constraint information, use `sp_helpconstraint`.

## Permissions

Requires membership in the **public** role.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

The following example shows all constraints for the `Product.Product` table.

```sql
USE AdventureWorks2022;
GO
EXEC sp_helpconstraint 'Production.Product';
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md)
- [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md)
- [sp_help (Transact-SQL)](sp-help-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [sys.key_constraints (Transact-SQL)](../system-catalog-views/sys-key-constraints-transact-sql.md)
- [sys.check_constraints (Transact-SQL)](../system-catalog-views/sys-check-constraints-transact-sql.md)
- [sys.default_constraints (Transact-SQL)](../system-catalog-views/sys-default-constraints-transact-sql.md)
