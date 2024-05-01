---
title: "sp_addtype (Transact-SQL)"
description: sp_addtype creates an alias data type.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_addtype"
  - "sp_addtype_TSQL"
helpviewer_keywords:
  - "sp_addtype"
dev_langs:
  - "TSQL"
---
# sp_addtype (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates an alias data type.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [CREATE TYPE](../../t-sql/statements/create-type-transact-sql.md) instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addtype
    [ @typename = ] N'typename'
    , [ @phystype = ] N'phystype'
    [ , [ @nulltype = ] 'nulltype' ]
    [ , [ @owner = ] N'owner' ]
[ ; ]
```

## Arguments

#### [ @typename = ] N'*typename*'

*@typename* is **sysname**, with no default.

The name of the alias data type. Alias data type names must follow the rules for [identifiers](../databases/database-identifiers.md) and must be unique in each database. *type* is **sysname**, with no default.

#### [ @phystype = ] N'*phystype*'

The physical, or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supplied, data type on which the alias data type is based. *@phystype* is **sysname**, with no default, and can be one of these values:

- **bigint**, **int**, **smallint**, and **tinyint**
- **binary**, **varbinary(*n*)**, and **image**
- **bit**
- **char(*n*)**, **nchar(*n*)**, **varchar(*n*)**, **nvarchar(*n*)**, **text, and **ntext**
- **datetime** and **smalldatetime**
- **decimal(*s*, *P*)** and **numeric(*s*, *P*)**
- **float** and **real**
- **money** and **smallmoney**
- **sql_variant**
- **uniqueidentifier**

Quotation marks are required around all parameters that contain embedded blank spaces or punctuation marks. For more information about available data types, see [Data Types (Transact-SQL)](../../t-sql/data-types/data-types-transact-sql.md).

- *n*

  A non-negative integer that indicates the length for the chosen data type.

- *P*

  A non-negative integer that indicates the maximum total number of decimal digits that can be stored, both to the left and to the right of the decimal point. For more information, see [decimal and numeric (Transact-SQL)](../../t-sql/data-types/decimal-and-numeric-transact-sql.md).

- *s*

  A non-negative integer that indicates the maximum number of decimal digits that can be stored to the right of the decimal point, and it must be less than or equal to the precision. For more information, see [decimal and numeric (Transact-SQL)](../../t-sql/data-types/decimal-and-numeric-transact-sql.md).

#### [ @nulltype = ] '*nulltype*'

Indicates the way the alias data type handles null values. *@nulltype* is **varchar(8)**, with a default of `NULL`, and must be enclosed in single quotation marks (`'NULL'`, `'NOT NULL'`, or `'NONULL'`).

If *@nulltype* isn't explicitly defined, it's set to the current default nullability. Use the `GETANSINULL` system function to determine the current default nullability. This can be adjusted by using the `SET` statement or `ALTER DATABASE`. Nullability should be explicitly defined. If *@phystype* is **bit**, and *@nulltype* isn't specified, the default is `NOT NULL`.

> [!NOTE]  
> The *@nulltype* parameter only defines the default nullability for this data type. If nullability is explicitly defined when the alias data type is used during table creation, it takes precedence over the defined nullability. For more information, see [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md) and [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md).

#### [ @owner = ] N'*owner*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

An alias data type name must be unique in the database, but alias data types with different names can have the same definition.

Executing `sp_addtype` creates an alias data type that appears in the `sys.types` catalog view for a specific database. If the alias data type must be available in all new user-defined databases, add it to `model`. After an alias data type is created, you can use it in `CREATE TABLE` or `ALTER TABLE`, and also bind defaults and rules to the alias data type. All scalar alias data types that are created by using `sp_addtype` are contained in the `dbo` schema.

Alias data types inherit the default collation of the database. The collations of columns and variables of alias types are defined in the [!INCLUDE [tsql](../../includes/tsql-md.md)] `CREATE TABLE`, `ALTER TABLE`, and `DECLARE @<local_variable>` statements. Changing the default collation of the database applies only to new columns and variables of the type; it doesn't change the collation of existing ones.

> [!IMPORTANT]  
> For backward compatibility purposes, the **public** database role is automatically granted `REFERENCES` permission on alias data types that are created by using `sp_addtype`. Note when alias data types are created by using the `CREATE TYPE` statement instead of `sp_addtype`, no such automatic grant occurs.

Alias data types can't be defined by using the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] **timestamp**, **table**, **xml**, **varchar(max)**, **nvarchar(max)**, or **varbinary(max)** data types.

## Permissions

Requires membership in the **db_owner** or **db_ddladmin** fixed database role.

## Examples

### A. Create an alias data type that doesn't allow for null values

The following example creates an alias data type named `ssn` (social security number) that is based on the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]-supplied **varchar** data type. The `ssn` data type is used for columns holding 11-digit social security numbers (`999-99-9999`). The column can't be `NULL`.

`varchar(11)` is enclosed in single quotation marks because it contains punctuation (parentheses).

```sql
USE master;
GO
EXEC sp_addtype ssn, 'varchar(11)', 'NOT NULL';
GO
```

### B. Create an alias data type that allows for null values

The following example creates an alias data type (based on `datetime`) named `birthday` that allows for null values.

```sql
USE master;
GO
EXEC sp_addtype birthday, datetime, 'NULL';
```

### C. Create additional alias data types

The following example creates two more alias data types, `telephone` and `fax`, for both domestic and international telephone and fax numbers.

```sql
USE master;
GO
EXEC sp_addtype telephone, 'varchar(24)', 'NOT NULL';
GO
EXEC sp_addtype fax, 'varchar(24)', 'NULL';
GO
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [CREATE TYPE (Transact-SQL)](../../t-sql/statements/create-type-transact-sql.md)
- [CREATE DEFAULT (Transact-SQL)](../../t-sql/statements/create-default-transact-sql.md)
- [CREATE RULE (Transact-SQL)](../../t-sql/statements/create-rule-transact-sql.md)
- [sp_bindefault (Transact-SQL)](sp-bindefault-transact-sql.md)
- [sp_bindrule (Transact-SQL)](sp-bindrule-transact-sql.md)
- [sp_droptype (Transact-SQL)](sp-droptype-transact-sql.md)
- [sp_rename (Transact-SQL)](sp-rename-transact-sql.md)
- [sp_unbindefault (Transact-SQL)](sp-unbindefault-transact-sql.md)
- [sp_unbindrule (Transact-SQL)](sp-unbindrule-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
