---
title: "sp_columns_ex (Transact-SQL)"
description: sp_columns_ex returns the column information, one row per column, for the specified linked server tables.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_columns_ex"
  - "sp_columns_ex_TSQL"
helpviewer_keywords:
  - "sp_columns_ex"
dev_langs:
  - "TSQL"
---
# sp_columns_ex (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the column information, one row per column, for the specified linked server tables. `sp_columns_ex` returns column information for only the specific column if *@column_name* is specified.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_columns_ex
    [ @table_server = ] N'table_server'
    [ , [ @table_name = ] N'table_name' ]
    [ , [ @table_schema = ] N'table_schema' ]
    [ , [ @table_catalog = ] N'table_catalog' ]
    [ , [ @column_name = ] N'column_name' ]
    [ , [ @ODBCVer = ] ODBCVer ]
[ ; ]
```

## Arguments

#### [ @table_server = ] N'*table_server*'

The name of the linked server for which to return column information. *@table_server* is **sysname**, with no default.

#### [ @table_name = ] N'*table_name*'

The name of the table for which to return column information. *@table_name* is **sysname**, with a default of `NULL`.

#### [ @table_schema = ] N'*table_schema*'

The schema name of the table for which to return column information. *@table_schema* is **sysname**, with a default of `NULL`.

#### [ @table_catalog = ] N'*table_catalog*'

The catalog name of the table for which to return column information. *@table_catalog* is **sysname**, with a default of `NULL`.

#### [ @column_name = ] N'*column_name*'

The name of the database column for which to provide information. *@column_name* is **sysname**, with a default of `NULL`.

#### [ @ODBCVer = ] *ODBCVer*

The version of ODBC that is being used. *@ODBCVer* is **int**, with a default of `2`. This indicates ODBC Version 2. Valid values are `2` or `3`. For information about the behavior differences between versions 2 and 3, see the ODBC `SQLColumns` specification.

## Return code values

None.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `TABLE_CAT` | **sysname** | Table or view qualifier name. Various DBMS products support three-part naming for tables (`<qualifier>.<owner>.<name>`). In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] this column represents the database name. In some products, it represents the server name of the table's database environment. This field can be `NULL`. |
| `TABLE_SCHEM` | **sysname** | Table or view owner name. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the name of the database user that created the table. This field always returns a value. |
| `TABLE_NAME` | **sysname** | Table or view name. This field always returns a value. |
| `COLUMN_NAME` | **sysname** | Column name, for each column of the `TABLE_NAME` returned. This field always returns a value. |
| `DATA_TYPE` | **smallint** | An **int** value that corresponds to ODBC type indicators. If this is a data type that can't be mapped to an ODBC type, this value is `NULL`. The native data type name is returned in the `TYPE_NAME` column. |
| `TYPE_NAME` | **varchar(13)** | String representing a data type. The underlying DBMS presents this data type name. |
| `COLUMN_SIZE` | **int** | Number of significant digits. The return value for the `PRECISION` column is in base 10. |
| `BUFFER_LENGTH` | **int** | Transfer size of the data.1 |
| `DECIMAL_DIGITS` | **smallint** | Number of digits to the right of the decimal point. |
| `NUM_PREC_RADIX` | **smallint** | The base for numeric data types. |
| `NULLABLE` | **smallint** | Specifies nullability.<br /><br />`1` = `NULL` is possible.<br />`0` = `NOT NULL`. |
| `REMARKS` | **varchar(254)** | This field always returns `NULL`. |
| `COLUMN_DEF` | **varchar(254)** | Default value of the column. |
| `SQL_DATA_TYPE` | **smallint** | Value of the SQL data type as it appears in the `TYPE` field of the descriptor. This column is the same as the `DATA_TYPE` column, except for the **datetime** and SQL-92 **interval** data types. This column always returns a value. |
| `SQL_DATETIME_SUB` | **smallint** | Subtype code for **datetime** and SQL-92 **interval** data types. For other data types, this column returns `NULL`. |
| `CHAR_OCTET_LENGTH` | **int** | Maximum length in bytes of a character or integer data type column. For all other data types, this column returns `NULL`. |
| `ORDINAL_POSITION` | **int** | Ordinal position of the column in the table. The first column in the table is 1. This column always returns a value. |
| `IS_NULLABLE` | **varchar(254)** | Nullability of the column in the table. ISO rules are followed to determine nullability. An ISO SQL-compliant DBMS can't return an empty string.<br /><br />`YES` = Column can include `NULL`.<br />`NO` = Column can't include `NULL`.<br /><br />This column returns a zero-length string if nullability is unknown.<br /><br />The value returned for this column is different from the value returned for the `NULLABLE` column. |
| `SS_DATA_TYPE` | **tinyint** | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type, used by extended stored procedures. |

For more information, see [ODBC Overview](../../odbc/reference/odbc-overview.md).

## Remarks

- `sp_columns_ex` is executed by querying the `COLUMNS` rowset of the `IDBSchemaRowset` interface of the OLE DB provider corresponding to *@table_server*. The *@table_name*, *@table_schema*, *@table_catalog*, and *@column_name* parameters are passed to this interface to restrict the rows returned.

- `sp_columns_ex` returns an empty result set if the OLE DB provider of the specified linked server doesn't support the `COLUMNS` rowset of the `IDBSchemaRowset` interface.

- `sp_columns_ex` follows the requirements for delimited identifiers. For more information, see [Database identifiers](../databases/database-identifiers.md).

## Permissions

Requires `SELECT` permission on the schema.

## Examples

The following example returns the data type of the `JobTitle` column of the `HumanResources.Employee` table in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database on the linked server `Seattle1`.

```sql
EXEC sp_columns_ex 'Seattle1',
    'Employee',
    'HumanResources',
    'AdventureWorks2022',
    'JobTitle';
```

## Related content

- [sp_catalogs (Transact-SQL)](sp-catalogs-transact-sql.md)
- [sp_foreignkeys (Transact-SQL)](sp-foreignkeys-transact-sql.md)
- [sp_indexes (Transact-SQL)](sp-indexes-transact-sql.md)
- [sp_linkedservers (Transact-SQL)](sp-linkedservers-transact-sql.md)
- [sp_primarykeys (Transact-SQL)](sp-primarykeys-transact-sql.md)
- [sp_tables_ex (Transact-SQL)](sp-tables-ex-transact-sql.md)
- [sp_table_privileges (Transact-SQL)](sp-table-privileges-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
