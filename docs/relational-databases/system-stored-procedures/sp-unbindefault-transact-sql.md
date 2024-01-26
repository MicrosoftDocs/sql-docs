---
title: "sp_unbindefault (Transact-SQL)"
description: Unbinds, or removes, a default from a column or from an alias data type in the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_unbindefault_TSQL"
  - "sp_unbindefault"
helpviewer_keywords:
  - "sp_unbindefault"
dev_langs:
  - "TSQL"
---
# sp_unbindefault (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Unbinds, or removes, a default from a column or from an alias data type in the current database.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you create default definitions by using the DEFAULT keyword in the [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) statements instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_unbindefault
    [ @objname = ] N'objname'
    [ , [ @futureonly = ] 'futureonly' ]
[ ; ]
```

## Arguments

#### [ @objname = ] N'*objname*'

The name of the table and column or the alias data type from which the default is to be unbound. *@objname* is **nvarchar(776)**, with no default. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] attempts to resolve two-part identifiers to column names first, then to alias data types.

When unbinding a default from an alias data type, any columns of that data type that have the same default are also unbound. Columns of that data type with defaults bound directly to them are unaffected.

> [!NOTE]  
> *@objname* can contain brackets `[]` as delimited identifier characters. For more information, see [Database Identifiers](../databases/database-identifiers.md).

#### [ @futureonly = ] 'futureonly'

Used only when unbinding a default from an alias data type. *@futureonly* is **varchar(15)**, with a default of `NULL`. When *@futureonly* is `futureonly`, existing columns of the data type don't lose the specified default.

## Return code values

`0` (success) or `1` (failure).

## Remarks

To display the text of a default, execute `sp_helptext` with the name of the default as the parameter.

## Permissions

To unbind a default from a table column requires ALTER permission on the table. To unbind a default from an alias data type requires CONTROL permission on the type or ALTER permission on the schema to which the type belongs.

## Examples

### A. Unbind a default from a column

The following example unbinds the default from the `hiredate` column of an `employees` table.

```sql
EXEC sp_unbindefault 'employees.hiredate';
```

### B. Unbind a default from an alias data type

The following example unbinds the default from the alias data type `ssn`. It unbinds existing and future columns of that type.

```sql
EXEC sp_unbindefault 'ssn';
```

### C. Use the futureonly_flag

The following example unbinds future uses of the alias data type `ssn` without affecting existing `ssn` columns.

```sql
EXEC sp_unbindefault 'ssn', 'futureonly';
```

### D. Use delimited identifiers

The following example shows using delimited identifiers in *@objname* parameter. Notice the period as part of the table name. In the `sp_unbindefault` portion, the object contains two periods; the first is part of the table name, and the second distinguishes the table name from the column name.

```sql
-- 
CREATE TABLE [t.3] (c1 INT);

CREATE DEFAULT default2 AS 0;
GO

EXEC sp_bindefault 'default2', '[t.3].c1';

EXEC sp_unbindefault '[t.3].c1';
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [CREATE DEFAULT (Transact-SQL)](../../t-sql/statements/create-default-transact-sql.md)
- [DROP DEFAULT (Transact-SQL)](../../t-sql/statements/drop-default-transact-sql.md)
- [sp_bindefault (Transact-SQL)](sp-bindefault-transact-sql.md)
- [sp_helptext (Transact-SQL)](sp-helptext-transact-sql.md)
