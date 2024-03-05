---
title: "sp_bindrule (Transact-SQL)"
description: sp_bindrule binds a rule to a column or to an alias data type.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_bindrule_TSQL"
  - "sp_bindrule"
helpviewer_keywords:
  - "sp_bindrule"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_bindrule (Transact-SQL)

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

Binds a rule to a column or to an alias data type.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [Unique constraints and check constraints](../tables/unique-constraints-and-check-constraints.md) instead. CHECK constraints are created by using the CHECK keyword of the [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) or [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) statements.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_bindrule
    [ @rulename = ] N'rulename'
    , [ @objname = ] N'objname'
    [ , [ @futureonly = ] 'futureonly' ]
[ ; ]
```

## Arguments

#### [ @rulename = ] N'*rulename*'

The name of a rule created by the `CREATE RULE` statement. *@rulename* is **nvarchar(776)**, with no default.

#### [ @objname = ] N'*objname*'

The table and column, or the alias data type to which the rule is to be bound. *@objname* is **nvarchar(776)**, with no default.

A rule can't be bound to a **text**, **ntext**, **image**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, CLR user-defined type, or **timestamp** column. A rule can't be bound to a computed column.

*@objname* is **nvarchar(776)** with no default. If *@objname* is a one-part name, it resolves as an alias data type. If it's a two- or three-part name, it first resolves as a table and column; if this resolution fails, it resolves as an alias data type. By default, existing columns of the alias data type inherit *@rulename* unless a rule is bound directly to the column.

*@objname* can contain the bracket (`[` and `]`) characters as delimited identifier characters. For more information, see [Database identifiers](../databases/database-identifiers.md).

Rules created on expressions that use alias data types can be bound to columns or alias data types, but fail to compile when they're referenced. Avoid using rules created on alias data types.

#### [ @futureonly = ] '*futureonly*'

Used only when binding a rule to an alias data type. *@futureonly* is **varchar(15)**, with a default of `NULL`. This parameter, when set to `futureonly`, prevents existing columns of an alias data type from inheriting the new rule. If *@futureonly* is `NULL`, the new rule is bound to any columns of the alias data type that currently have no rule or that are using the existing rule of the alias data type.

## Return code values

`0` (success) or `1` (failure).

## Remarks

You can bind a new rule to a column (although using a `CHECK` constraint is preferred) or to an alias data type with `sp_bindrule` without unbinding an existing rule. The old rule is overridden. If a rule is bound to a column with an existing `CHECK` constraint, all restrictions are evaluated. You can't bind a rule to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type.

The rule is enforced when an `INSERT` statement is tried, not at binding. You can bind a character rule to a column of **numeric** data type, although such an `INSERT` operation isn't valid.

Existing columns of the alias data type inherit the new rule unless *@futureonly* is specified as `futureonly`. New columns defined with the alias data type always inherit the rule. However, if the `ALTER COLUMN` clause of an `ALTER TABLE` statement changes the data type of a column to an alias data type bound to a rule, the rule bound to the data type isn't inherited by the column. The rule must be specifically bound to the column by using `sp_bindrule`.

When you bind a rule to a column, related information is added to the `sys.columns` table. When you bind a rule to an alias data type, related information is added to the `sys.types` table.

## Permissions

To bind a rule to a table column, you must have `ALTER` permission on the table. `CONTROL` permission on the alias data type, or `ALTER` permission on the schema to which the type belongs, is required to bind a rule to an alias data type.

## Examples

### A. Bind a rule to a column

Assuming that a rule named `today` is created in the current database by using the `CREATE RULE` statement, the following example binds the rule to the `HireDate` column of the `Employee` table. When a row is added to `Employee`, the data for the `HireDate` column is checked against the `today` rule.

```sql
USE master;
GO

EXEC sp_bindrule 'today', 'HumanResources.Employee.HireDate';
```

### B. Bind a rule to an alias data type

Assuming the existence of a rule named `rule_ssn` and an alias data type named `ssn`, the following example binds `rule_ssn` to `ssn`. In a `CREATE TABLE` statement, columns of type `ssn` inherit the `rule_ssn` rule. Existing columns of type `ssn` also inherit the `rule_ssn` rule, unless **futureonly** is specified for *@futureonly*, or `ssn` has a rule bound directly to it. Rules bound to columns always take precedence over defaults bound to data types.

```sql
USE master;
GO

EXEC sp_bindrule 'rule_ssn', 'ssn';
```

### C. Use the `futureonly` option

The following example binds the `rule_ssn` rule to the alias data type `ssn`. Because `futureonly` is specified, no existing columns of type `ssn` are affected.

```sql
USE master;
GO

EXEC sp_bindrule rule_ssn, 'ssn', 'futureonly';
```

### D. Use delimited identifiers

The following example shows the use of delimited identifiers in *@objname* parameter.

```sql
USE master;
GO

CREATE TABLE [t.2] (c1 int) ;
-- Notice the period as part of the table name.
EXEC sp_bindrule rule1, '[t.2].c1' ;
-- The object contains two periods;
-- the first is part of the table name
-- and the second distinguishes the table name from the column name.
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [CREATE RULE (Transact-SQL)](../../t-sql/statements/create-rule-transact-sql.md)
- [DROP RULE (Transact-SQL)](../../t-sql/statements/drop-rule-transact-sql.md)
- [sp_unbindrule (Transact-SQL)](sp-unbindrule-transact-sql.md)
