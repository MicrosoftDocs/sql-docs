---
title: "sp_column_privileges_ex (Transact-SQL)"
description: sp_column_privileges_ex returns column privileges for the specified table on the specified linked server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_column_privileges_ex"
  - "sp_column_privileges_ex_TSQL"
helpviewer_keywords:
  - "sp_column_privileges_ex"
dev_langs:
  - "TSQL"
---
# sp_column_privileges_ex (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns column privileges for the specified table on the specified linked server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_column_privileges_ex
    [ @table_server = ] N'table_server'
    [ , [ @table_name = ] N'table_name' ]
    [ , [ @table_schema = ] N'table_schema' ]
    [ , [ @table_catalog = ] N'table_catalog' ]
    [ , [ @column_name = ] N'column_name' ]
[ ; ]
```

## Arguments

#### [ @table_server = ] N'*table_server*'

The name of the linked server for which to return information. *@table_server* is **sysname**, with no default.

#### [ @table_name = ] N'*table_name*'

The name of the table that contains the specified column. *@table_name* is **sysname**, with a default of `NULL`.

#### [ @table_schema = ] N'*table_schema*'

The table schema. *@table_schema* is **sysname**, with a default of `NULL`.

#### [ @table_catalog = ] N'*table_catalog*'

The name of the database in which the specified *@table_name* resides. *@table_catalog* is **sysname**, with a default of `NULL`.

#### [ @column_name = ] N'*column_name*'

The name of the column for which to provide privilege information. *@column_name* is **sysname**, with a default of `NULL` (all common).

## Result set

The following table shows the result set columns. The results returned are ordered by `TABLE_QUALIFIER`, `TABLE_OWNER`, `TABLE_NAME`, `COLUMN_NAME`, and `PRIVILEGE`.

| Column name | Data type | Description |
| --- | --- | --- |
| `TABLE_CAT` | **sysname** | Table qualifier name. Various DBMS products support three-part naming for tables (`<qualifier>.<owner>.<name>`). In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. In some products, it represents the server name of the table's database environment. This field can be `NULL`. |
| `TABLE_SCHEM` | **sysname** | Table owner name. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the name of the database user who created the table. This field always returns a value. |
| `TABLE_NAME` | **sysname** | Table name. This field always returns a value. |
| `COLUMN_NAME` | **sysname** | Column name, for each column of the `TABLE_NAME` returned. This field always returns a value. |
| `GRANTOR` | **sysname** | Database user name that was granted permissions on this `COLUMN_NAME` to the listed `GRANTEE`. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column is always the same as the `TABLE_OWNER`. This field always returns a value.<br /><br />The `GRANTOR` column can be either the database owner (`TABLE_OWNER`) or someone to whom the database owner granted permissions by using the `WITH GRANT OPTION` clause in the `GRANT` statement. |
| `GRANTEE` | **sysname** | Database user name that was granted permissions on this `COLUMN_NAME` by the listed `GRANTOR`. This field always returns a value. |
| `PRIVILEGE` | **varchar(32)** | One of the available column permissions. Column permissions can be one of the following values (or other values supported by the data source when implementation is defined):<br /><br />`SELECT` = `GRANTEE` can retrieve data for the columns.<br />`INSERT` = `GRANTEE` can provide data for this column when new rows are inserted (by the `GRANTEE`) into the table.<br />`UPDATE` = `GRANTEE` can modify existing data in the column.<br />`REFERENCES` = `GRANTEE` can reference a column in a foreign table in a primary key/foreign key relationship. Primary key/foreign key relationships are defined with table constraints. |
| `IS_GRANTABLE` | **varchar(3)** | Indicates whether the `GRANTEE` is permitted to grant permissions to other users (often referred to as "grant with grant" permission). Can be YES, NO, or `NULL`. An unknown, or `NULL`, value refers to a data source where "grant with grant" isn't applicable. |

## Permissions

Requires `SELECT` permission on the schema.

## Examples

The following example returns column privilege information for the `HumanResources.Department` table in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database on the `Seattle1` linked server.

```sql
EXEC sp_column_privileges_ex
    @table_server = 'Seattle1',
    @table_name = 'Department',
    @table_schema = 'HumanResources',
    @table_catalog = 'AdventureWorks2022';
```

## Related content

- [sp_table_privileges_ex (Transact-SQL)](sp-table-privileges-ex-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
