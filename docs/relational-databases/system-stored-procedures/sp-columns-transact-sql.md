---
title: "sp_columns (Transact-SQL)"
description: sp_columns returns column information for the specified objects that can be queried in the current environment.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_columns_TSQL"
  - "sp_columns"
helpviewer_keywords:
  - "sp_columns"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# sp_columns (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns column information for the specified objects that can be queried in the current environment.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_columns
    [ @table_name = ] N'table_name'
    [ , [ @table_owner = ] N'table_owner' ]
    [ , [ @table_qualifier = ] N'table_qualifier' ]
    [ , [ @column_name = ] N'column_name' ]
    [ , [ @ODBCVer = ] ODBCVer ]
[ ; ]
```

## Arguments

#### [ @table_name = ] N'*table_name*'

*@table_name* is **nvarchar(384)**, with no default.

The name of the object that is used to return catalog information. *@table_name* can be a table, view, or other object that's columns such as table-valued functions. *@table_name* is **nvarchar(384)**, with no default. Wildcard pattern matching is supported.

#### [ @table_owner = ] N'*table_owner*'

The object owner of the object that is used to return catalog information. *@table_owner* is **nvarchar(384)**, with a default of `NULL`. Wildcard pattern matching is supported. If *@table_owner* isn't specified, the default object visibility rules of the underlying DBMS apply.

If the current user owns an object with the specified name, the columns of that object are returned. If *@table_owner* isn't specified and the current user doesn't own an object with the specified *@table_name*, `sp_columns` looks for an object with the specified *@table_name* owned by the database owner. If one exists, that object's columns are returned.

#### [ @table_qualifier = ] N'*table_qualifier*'

*@table_qualifier* is **sysname**, with a default of `NULL`.

The name of the object qualifier. *@table_qualifier* is **sysname**, with a default of `NULL`. Various DBMS products support three-part naming for objects (`<qualifier>.<owner>.<name>`). In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. In some products, it represents the server name of the object's database environment.

#### [ @column_name = ] N'*column_name*'

A single column and is used when only one column of catalog information is wanted. *@column_name* is **nvarchar(384)**, with a default of `NULL`. If *@column_name* isn't specified, all columns are returned. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], *@column_name* represents the column name as listed in the `syscolumns` table. Wildcard pattern matching is supported. For maximum interoperability, the gateway client should assume only SQL-92 standard pattern matching (the `%` and `_` wildcard characters).

#### [ @ODBCVer = ] *ODBCVer*

The version of ODBC that is being used. *@ODBCVer* is **int**, with a default of `2`. This indicates ODBC Version 2. Valid values are `2` or `3`. For the behavior differences between versions 2 and 3, see the ODBC `SQLColumns` specification.

## Return code values

None.

## Result set

The `sp_columns` catalog stored procedure is equivalent to `SQLColumns` in ODBC. The results returned are ordered by `TABLE_QUALIFIER`, `TABLE_OWNER`, and `TABLE_NAME`.

| Column name | Data type | Description |
| --- | --- | --- |
| `TABLE_QUALIFIER` | **sysname** | Object qualifier name. This field can be `NULL`. |
| `TABLE_OWNER` | **sysname** | Object owner name. This field always returns a value. |
| `TABLE_NAME` | **sysname** | Object name. This field always returns a value. |
| `COLUMN_NAME` | **sysname** | Column name, for each column of the `TABLE_NAME` returned. This field always returns a value. |
| `DATA_TYPE` | **smallint** | Integer code for ODBC data type. If this data type can't be mapped to an ODBC type, it's `NULL`. The native data type name is returned in the `TYPE_NAME` column. |
| `TYPE_NAME` | **sysname** | String representing a data type. The underlying DBMS presents this data type name. |
| `PRECISION` | **int** | Number of significant digits. The return value for the `PRECISION` column is in base 10. |
| `LENGTH` | **int** | Transfer size of the data. <sup>1</sup> |
| `SCALE` | **smallint** | Number of digits to the right of the decimal point. |
| `RADIX` | **smallint** | Base for numeric data types. |
| `NULLABLE` | **smallint** | Specifies nullability.<br /><br />`1` = `NULL` is possible.<br />`0` = NOT `NULL`. |
| `REMARKS` | **varchar(254)** | This field always returns `NULL`. |
| `COLUMN_DEF` | **nvarchar(4000)** | Default value of the column. |
| `SQL_DATA_TYPE` | **smallint** | Value of the SQL data type as it appears in the TYPE field of the descriptor. This column is the same as the `DATA_TYPE` column, except for the **datetime** and SQL-92 **interval** data types. This column always returns a value. |
| `SQL_DATETIME_SUB` | **smallint** | Subtype code for **datetime** and SQL-92 **interval** data types. For other data types, this column returns `NULL`. |
| `CHAR_OCTET_LENGTH` | **int** | Maximum length in bytes of a character or integer data type column. For all other data types, this column returns `NULL`. |
| `ORDINAL_POSITION` | **int** | Ordinal position of the column in the object. The first column in the object is 1. This column always returns a value. |
| `IS_NULLABLE` | **varchar(254)** | Nullability of the column in the object. ISO rules are followed to determine nullability. An ISO SQL-compliant DBMS can't return an empty string.<br /><br />`YES` = Column can include `NULL`.<br />`NO` = Column can't include `NULL`.<br /><br />This column returns a zero-length string if nullability is unknown.<br /><br />The value returned for this column is different from the value returned for the `NULLABLE` column. |
| `SS_DATA_TYPE` | **tinyint** | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type used by extended stored procedures. For more information, see [Data types](../../t-sql/data-types/data-types-transact-sql.md). |

<sup>1</sup> For more information, see [ODBC Overview](../../odbc/reference/odbc-overview.md).

## Permissions

Requires `SELECT` and `VIEW DEFINITION` permissions on the schema.

## Remarks

`sp_columns` follows the requirements for delimited identifiers. For more information, see [Database identifiers](../databases/database-identifiers.md).

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

The following example returns column information for a specified table.

```sql
USE AdventureWorks2022;
GO

EXEC sp_columns
    @table_name = N'Department',
    @table_owner = N'HumanResources';
```

## Examples: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]

The following example returns column information for a specified table.

```sql
USE AdventureWorksDW2022;
GO

EXEC sp_columns
    @table_name = N'DimEmployee',
    @table_owner = N'dbo';
```

## Related content

- [sp_tables (Transact-SQL)](sp-tables-transact-sql.md)
- [Catalog stored procedures (Transact-SQL)](catalog-stored-procedures-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
