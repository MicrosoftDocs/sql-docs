---
title: "sp_tables_ex (Transact-SQL)"
description: sp_tables_ex returns table information about the tables from the specified linked server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_tables_ex"
  - "sp_tables_ex_TSQL"
helpviewer_keywords:
  - "sp_tables_ex"
dev_langs:
  - "TSQL"
---
# sp_tables_ex (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns table information about the tables from the specified linked server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_tables_ex
    [ @table_server = ] N'table_server'
    [ , [ @table_name = ] N'table_name' ]
    [ , [ @table_schema = ] N'table_schema' ]
    [ , [ @table_catalog = ] N'table_catalog' ]
    [ , [ @table_type = ] N'table_type' ]
    [ , [ @fUsePattern = ] fUsePattern ]
[ ; ]
```

## Arguments

#### [ @table_server = ] N'*table_server*'

The name of the linked server for which to return table information. *@table_server* is **sysname**, with no default.

#### [ @table_name = ] N'*table_name*'

The name of the table for which to return data type information. *@table_name* is **sysname**, with a default of `NULL`.

#### [ @table_schema = ] N'*table_schema*'

The table schema. *@table_schema* is **sysname**, with a default of `NULL`.

#### [ @table_catalog = ] N'*table_catalog*'

The name of the database in which the specified *table_name* resides. *@table_catalog* is **sysname**, with a default of `NULL`.

#### [ @table_type = ] N'*table_type*'

The type of the table to return. *@table_type* is **sysname**, and can have one of the following values.

| Value | Description |
| --- | --- |
| `ALIAS` | Name of an alias. |
| `GLOBAL TEMPORARY` | Name of a temporary table available system wide. |
| `LOCAL TEMPORARY` | Name of a temporary table available only to the current job. |
| `SYNONYM` | Name of a synonym. |
| `SYSTEM TABLE` | Name of a system table. |
| `SYSTEM VIEW` | Name of a system view. |
| `TABLE` | Name of a user table. |
| `VIEW` | Name of a view. |

#### [ @fUsePattern = ] *fUsePattern*

Determines whether the characters `_`, `%`, `[`, and `]` are interpreted as wildcard characters. Valid values are 0 (pattern matching is off) and 1 (pattern matching is on). *@fUsePattern* is **bit**, with a default of `1`.

## Return code values

None.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `TABLE_CAT` | **sysname** | Table qualifier name. Various DBMS products support three-part naming for tables (`<qualifier>.<owner>.<name>`). In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the database name. In some other products, it represents the server name of the database environment of the table. This field can be `NULL`. |
| `TABLE_SCHEM` | **sysname** | Table owner name. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this column represents the name of the database user who created the table. This field always returns a value. |
| `TABLE_NAME` | **sysname** | Table name. This field always returns a value. |
| `TABLE_TYPE` | **varchar(32)** | Table, system table, or view. |
| `REMARKS` | **varchar(254)** | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't return a value for this column. |

## Remarks

`sp_tables_ex` is executed by querying the TABLES rowset of the `IDBSchemaRowset` interface of the OLE DB provider corresponding to *table_server*. The *table_name*, *table_schema*, *table_catalog*, and *column* parameters are passed to this interface to restrict the rows returned.

`sp_tables_ex` returns an empty result set if the OLE DB provider of the specified linked server doesn't support the `TABLES` rowset of the `IDBSchemaRowset` interface.

## Permissions

Requires `SELECT` permission on the schema.

## Examples

The following example returns information about the tables that are contained in the `HumanResources` schema in the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database on the `LONDON2` linked server.

```sql
EXEC sp_tables_ex @table_server = 'LONDON2',
@table_catalog = 'AdventureWorks2022',
@table_schema = 'HumanResources',
@table_type = 'TABLE';
```

## Related content

- [Distributed Queries stored procedures (Transact-SQL)](distributed-queries-stored-procedures-transact-sql.md)
- [sp_catalogs (Transact-SQL)](sp-catalogs-transact-sql.md)
- [sp_columns_ex (Transact-SQL)](sp-columns-ex-transact-sql.md)
- [sp_column_privileges (Transact-SQL)](sp-column-privileges-transact-sql.md)
- [sp_foreignkeys (Transact-SQL)](sp-foreignkeys-transact-sql.md)
- [sp_indexes (Transact-SQL)](sp-indexes-transact-sql.md)
- [sp_linkedservers (Transact-SQL)](sp-linkedservers-transact-sql.md)
- [sp_table_privileges (Transact-SQL)](sp-table-privileges-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
