---
title: "sp_linkedservers (Transact-SQL)"
description: sp_linkedservers returns the list of linked servers defined in the local server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_linkedservers"
  - "sp_linkedservers_TSQL"
helpviewer_keywords:
  - "sp_linkedservers"
dev_langs:
  - "TSQL"
---
# sp_linkedservers (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the list of linked servers defined in the local server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_linkedservers
[ ; ]
```

## Arguments

None.

## Return code values

`0` (success) or a nonzero number (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `SRV_NAME` | **sysname** | Name of the linked server. |
| `SRV_PROVIDERNAME` | **nvarchar(128)** | Friendly name of the OLE DB provider managing access to the specified linked server. |
| `SRV_PRODUCT` | **nvarchar(128)** | Product name of the linked server. |
| `SRV_DATASOURCE` | **nvarchar(4000)** | OLE DB data source property corresponding to the specified linked server. |
| `SRV_PROVIDERSTRING` | **nvarchar(4000)** | OLE DB provider string property corresponding to the linked server. |
| `SRV_LOCATION` | **nvarchar(4000)** | OLE DB location property corresponding to the specified linked server. |
| `SRV_CAT` | **sysname** | OLE DB catalog property corresponding to the specified linked server. |

## Permissions

Requires `SELECT` permission on the schema.

## Related content

- [sp_catalogs (Transact-SQL)](sp-catalogs-transact-sql.md)
- [sp_column_privileges (Transact-SQL)](sp-column-privileges-transact-sql.md)
- [sp_columns_ex (Transact-SQL)](sp-columns-ex-transact-sql.md)
- [sp_foreignkeys (Transact-SQL)](sp-foreignkeys-transact-sql.md)
- [sp_indexes (Transact-SQL)](sp-indexes-transact-sql.md)
- [sp_primarykeys (Transact-SQL)](sp-primarykeys-transact-sql.md)
- [sp_table_privileges (Transact-SQL)](sp-table-privileges-transact-sql.md)
- [sp_tables_ex (Transact-SQL)](sp-tables-ex-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Distributed Queries stored procedures (Transact-SQL)](distributed-queries-stored-procedures-transact-sql.md)
