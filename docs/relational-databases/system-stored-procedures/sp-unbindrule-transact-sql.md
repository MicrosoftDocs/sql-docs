---
title: "sp_unbindrule (Transact-SQL)"
description: Unbinds a rule from a column or an alias data type in the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_unbindrule_TSQL"
  - "sp_unbindrule"
helpviewer_keywords:
  - "sp_unbindrule"
dev_langs:
  - "TSQL"
---
# sp_unbindrule (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Unbinds a rule from a column or an alias data type in the current database.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you create default definitions by using the DEFAULT keyword in the [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) statements instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_unbindrule
    [ @objname = ] N'objname'
    [ , [ @futureonly = ] 'futureonly' ]
[ ; ]
```

## Arguments

#### [ @objname = ] N'*objname*'

The name of the table and column or the alias data type from which the rule is unbound. *@objname* is **nvarchar(776)**, with no default. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] attempts to resolve two-part identifiers to column names first, then to alias data types. When unbinding a rule from an alias data type, any columns of the data type that have the same rule are also unbound. Columns of that data type with rules bound directly to them are unaffected.

> [!NOTE]  
> *@objname* can contain brackets `[]` as delimited identifier characters. For more information, see [Database identifiers](../databases/database-identifiers.md).

#### [ @futureonly = ] 'futureonly'

Used only when unbinding a rule from an alias data type. *@futureonly* is **varchar(15)**, with a default of `NULL`. When *@futureonly* is `futureonly`, existing columns of that data type don't lose the specified rule.

## Return code values

`0` (success) or `1` (failure).

## Remarks

To display the text of a rule, execute `sp_helptext` with the rule name as the parameter.

When a rule is unbound, the information about the binding is removed from the `sys.columns` table if the rule was bound to a column, and from the `sys.types` table if the rule was bound to an alias data type.

When a rule is unbound from an alias data type, it's also unbound from any columns having that alias data type. The rule might also still be bound to columns whose data types were later changed by the ALTER COLUMN clause of an ALTER TABLE statement, you must specifically unbind the rule from these columns by using `sp_unbindrule` and specifying the column name.

## Permissions

To unbind a rule from a table column requires ALTER permission on the table. To unbind a rule from an alias data type requires CONTROL permission on the type or ALTER permission on the schema to which the type belongs.

## Examples

### A. Unbind a rule from a column

The following example unbinds the rule from the `startdate` column of an `employees` table.

```sql
EXEC sp_unbindrule 'employees.startdate';
```

### B. Unbind a rule from an alias data type

The following example unbinds the rule from the alias data type `ssn`. It unbinds the rule from existing and future columns of that type.

```sql
EXEC sp_unbindrule ssn;
```

### C. Use futureonly_flag

The following example unbinds the rule from the alias data type `ssn` without affecting existing `ssn` columns.

```sql
EXEC sp_unbindrule 'ssn', 'futureonly';
```

### D. Use delimited identifiers

The following example shows using delimited identifiers in the *@objname* parameter. Notice the period as part of the table name. In the `sp_bindrule` portion, the object contains two periods; the first is part of the table name, and the second distinguishes the table name from the column name.

```sql
CREATE TABLE [t.4] (c1 int);
GO
CREATE RULE rule2 AS @value > 100;
GO
EXEC sp_bindrule rule2, '[t.4].c1'
GO
EXEC sp_unbindrule '[t.4].c1';
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [CREATE RULE (Transact-SQL)](../../t-sql/statements/create-rule-transact-sql.md)
- [DROP RULE (Transact-SQL)](../../t-sql/statements/drop-rule-transact-sql.md)
- [sp_bindrule (Transact-SQL)](sp-bindrule-transact-sql.md)
- [sp_helptext (Transact-SQL)](sp-helptext-transact-sql.md)
