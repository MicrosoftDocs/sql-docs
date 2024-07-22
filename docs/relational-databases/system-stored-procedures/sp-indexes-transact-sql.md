---
title: "sp_indexes (Transact-SQL)"
description: sp_indexes returns index information for the specified remote table.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_indexes_TSQL"
  - "sp_indexes"
helpviewer_keywords:
  - "sp_indexes"
dev_langs:
  - "TSQL"
---
# sp_indexes (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns index information for the specified remote table.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_indexes
    [ @table_server = ] N'table_server'
    [ , [ @table_name = ] N'table_name' ]
    [ , [ @table_schema = ] N'table_schema' ]
    [ , [ @table_catalog = ] N'table_catalog' ]
    [ , [ @index_name = ] N'index_name' ]
    [ , [ @is_unique = ] is_unique ]
[ ; ]
```

## Arguments

#### [ @table_server = ] N'*table_server*'

The name of a linked server running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] for which table information is being requested. *@table_server* is **sysname**, with no default.

#### [ @table_name = ] N'*table_name*'

The name of the remote table for which to provide index information. *@table_name* is **sysname**, with a default of `NULL`. If `NULL`, all tables in the specified database are returned.

#### [ @table_schema = ] N'*table_schema*'

Specifies the table schema. *@table_schema* is **sysname**, with a default of `NULL`. In the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] environment, this value corresponds to the table owner.

#### [ @table_catalog = ] N'*table_catalog*'

The name of the database in which *@table_name* resides. *@table_catalog* is **sysname**, with a default of `NULL`. If `NULL`, *@table_catalog* defaults to `master`.

#### [ @index_name = ] N'*index_name*'

The name of the index for which information is being requested. *@index_name* is **sysname**, with a default of `NULL`.

#### [ @is_unique = ] *is_unique*

The type of index for which to return information. *@is_unique* is **bit**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `1` | Returns information about unique indexes. |
| `0` | Returns information about indexes that aren't unique. |
| `NULL` (default) | Returns information about all indexes. |

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `TABLE_CAT` | **sysname** | Name of the database in which the specified table resides. |
| `TABLE_SCHEM` | **sysname** | Schema for the table. |
| `TABLE_NAME` | **sysname** | Name of the remote table. |
| `NON_UNIQUE` | **smallint** | Whether the index is unique or not unique:<br /><br />`0` = Unique<br />`1` = Not unique |
| `INDEX_QUALIFER` | **sysname** | Name of the index owner. Some database management system (DBMS) products allow for users other than the table owner to create indexes. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column is always the same as `TABLE_NAME`. |
| `INDEX_NAME` | **sysname** | Name of the index. |
| `TYPE` | **smallint** | Type of index:<br /><br />`0` = Statistics for a table<br />`1` = Clustered<br />`2` = Hashed<br />`3` = Other |
| `ORDINAL_POSITION` | **int** | Ordinal position of the column in the index. The first column in the index is `1`. This column always returns a value. |
| `COLUMN_NAME` | **sysname**| The corresponding name of the column for each column of the `TABLE_NAME` returned. |
| `ASC_OR_DESC` | **varchar**| The order used in collation:<br /><br />`A` = Ascending<br />`D` = Descending<br />`NULL` = Not applicable<br /><br />[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] always returns `A`. |
| `CARDINALITY` | **int**| The number of rows in the table or unique values in the index. |
| `PAGES` | **int**| The number of pages to store the index or table. |
| `FILTER_CONDITION` | **nvarchar(4000)** | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't return a value. |

## Permissions

Requires `SELECT` permission on the schema.

## Examples

The following example returns all index information from the `Employees` table of the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database on the `Seattle1` linked server.

```sql
EXEC sp_indexes @table_server = 'Seattle1',
    @table_name = 'Employee',
    @table_schema = 'HumanResources',
    @table_catalog = 'AdventureWorks2022';
```

## Related content

- [Distributed Queries stored procedures (Transact-SQL)](distributed-queries-stored-procedures-transact-sql.md)
- [sp_catalogs (Transact-SQL)](sp-catalogs-transact-sql.md)
- [sp_column_privileges (Transact-SQL)](sp-column-privileges-transact-sql.md)
- [sp_foreignkeys (Transact-SQL)](sp-foreignkeys-transact-sql.md)
- [sp_linkedservers (Transact-SQL)](sp-linkedservers-transact-sql.md)
- [sp_tables_ex (Transact-SQL)](sp-tables-ex-transact-sql.md)
- [sp_table_privileges (Transact-SQL)](sp-table-privileges-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
