---
title: "sys.sp_catalogs (Transact-SQL)"
description: sp_catalogs returns the list of catalogs in the specified linked server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/19/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_catalogs_TSQL"
  - "sp_catalogs"
helpviewer_keywords:
  - "sp_catalogs"
dev_langs:
  - "TSQL"
---
# sys.sp_catalogs (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the list of catalogs in the specified linked server. This is equivalent to databases in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_catalogs [ @server_name = ] N'server_name'
[ ; ]
```

## Arguments

#### [ @server_name = ] N'*server_name*'

The name of a linked server. *@server_name* is **sysname**, with no default.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `CATALOG_NAME` | **nvarchar(128)** | Name of the catalog |
| `DESCRIPTION` | **nvarchar(4000)** | Description of the catalog |

## Permissions

Requires `SELECT` permission on the schema.

## Examples

The following example returns catalog information for the linked server named `OLE DB ODBC Linked Server #3`.

> [!NOTE]  
> For `sp_catalogs` to provide useful information, the `OLE DB ODBC Linked Server #3` must already exist.

```sql
USE master;
GO

EXECUTE sp_catalogs 'OLE DB ODBC Linked Server #3';
```

## Related content

- [sys.sp_addlinkedserver (Transact-SQL)](sp-addlinkedserver-transact-sql.md)
- [sys.sp_columns_ex (Transact-SQL)](sp-columns-ex-transact-sql.md)
- [sys.sp_column_privileges (Transact-SQL)](sp-column-privileges-transact-sql.md)
- [sys.sp_foreignkeys (Transact-SQL)](sp-foreignkeys-transact-sql.md)
- [sys.sp_indexes (Transact-SQL)](sp-indexes-transact-sql.md)
- [sp_linkedservers (Transact-SQL)](sp-linkedservers-transact-sql.md)
- [sys.sp_primarykeys (Transact-SQL)](sp-primarykeys-transact-sql.md)
- [sys.sp_tables_ex (Transact-SQL)](sp-tables-ex-transact-sql.md)
- [sys.sp_table_privileges (Transact-SQL)](sp-table-privileges-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
