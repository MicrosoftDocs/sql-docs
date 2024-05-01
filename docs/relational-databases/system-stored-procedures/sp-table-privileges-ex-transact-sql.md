---
title: "sp_table_privileges_ex (Transact-SQL)"
description: Returns privilege information about the specified table from the specified linked server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_table_privileges_ex"
  - "sp_table_privileges_ex_TSQL"
helpviewer_keywords:
  - "sp_table_privileges_ex"
dev_langs:
  - "TSQL"
---
# sp_table_privileges_ex (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns privilege information about the specified table from the specified linked server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_table_privileges_ex
    [ @table_server = ] N'table_server'
    [ , [ @table_name = ] N'table_name' ]
    [ , [ @table_schema = ] N'table_schema' ]
    [ , [ @table_catalog = ] N'table_catalog' ]
    [ , [ @fUsePattern = ] fUsePattern ]
[ ; ]
```

## Arguments

#### [ @table_server = ] N'*table_server*'

The name of the linked server for which to return information. *@table_server* is **sysname**, with no default.

#### [ @table_name = ] N'*table_name*'

The name of the table for which to provide table privilege information. *@table_name* is **sysname**, with a default of `NULL`.

#### [ @table_schema = ] N'*table_schema*'

The table schema. This in some DBMS environments is the table owner. *@table_schema* is **sysname**, with a default of `NULL`.

#### [ @table_catalog = ] N'*table_catalog*'

The name of the database in which the specified *@table_name* resides. *@table_catalog* is **sysname**, with a default of `NULL`.

#### [ @fUsePattern = ] *fUsePattern*

Determines whether the characters `_`, `%`, `[`, and `]` are interpreted as wildcard characters. Valid values are `0` (pattern matching is off) and `1` (pattern matching is on). *@fUsePattern* is **bit**, with a default of `1`.

## Return code values

None.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `TABLE_CAT` | **sysname** | Table qualifier name. Various DBMS products support three-part naming for tables (`<qualifier>.<owner>.<name>`). In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. In some products, it represents the server name of the table's database environment. This field can be `NULL`. |
| `TABLE_SCHEM` | **sysname** | Table owner name. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the name of the database user who created the table. This field always returns a value. |
| `TABLE_NAME` | **sysname** | Table name. This field always returns a value. |
| `GRANTOR` | **sysname** | Database username that's granted permissions on this `TABLE_NAME` to the listed `GRANTEE`. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column is always the same as the `TABLE_OWNER`. This field always returns a value. Also, the `GRANTOR` column might be either the database owner (`TABLE_OWNER`) or a user to whom the database owner granted permission by using the `WITH GRANT OPTION` clause in the `GRANT` statement. |
| `GRANTEE` | **sysname** | Database username that's been granted permissions on this `TABLE_NAME` by the listed `GRANTOR`. This field always returns a value. |
| `PRIVILEGE` | **varchar(32)** | One of the available table permissions. Table permissions can be one of the following values, or other values supported by the data source when implementation is defined.<br /><br />`SELECT` = `GRANTEE` can retrieve data for one or more of the columns.<br /><br />`INSERT` = `GRANTEE` can provide data for new rows for one or more of the columns.<br /><br />`UPDATE` = `GRANTEE` can modify existing data for one or more of the columns.<br /><br />`DELETE` = `GRANTEE` can remove rows from the table.<br /><br />`REFERENCES` = `GRANTEE` can reference a column in a foreign table in a primary key/foreign key relationship. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], primary key/foreign key relationships are defined by using table constraints.<br /><br />The scope of action given to the `GRANTEE` by a specific table privilege is data source-dependent. For example, the `UPDATE` permission could enable the `GRANTEE` to update all columns in a table on one data source and only those columns for which the `GRANTOR` has UPDATE permission on another data source. |
| `IS_GRANTABLE` | **varchar(3)** | Indicates whether the `GRANTEE` is permitted to grant permissions to other users. This is often referred to as "grant with grant" permission. Can be `YES`, `NO`, or `NULL`. An unknown, or `NULL`, value refers to a data source in which "grant with grant" isn't applicable. |

## Remarks

The results returned are ordered by `TABLE_QUALIFIER`, `TABLE_OWNER`, `TABLE_NAME`, and `PRIVILEGE`.

## Permissions

Requires `SELECT` permission on the schema.

## Examples

The following example returns privilege information about tables with names that start with `Product` in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database from the specified linked server `Seattle1`. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is assumed as the linked server.

```sql
EXEC sp_table_privileges_ex @table_server = 'Seattle1',
    @table_name = 'Product%',
    @table_schema = 'Production',
    @table_catalog ='AdventureWorks2022';
```

## Related content

- [sp_column_privileges_ex (Transact-SQL)](sp-column-privileges-ex-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Distributed Queries stored procedures (Transact-SQL)](distributed-queries-stored-procedures-transact-sql.md)
