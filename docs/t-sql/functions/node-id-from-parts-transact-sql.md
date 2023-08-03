---
title: "NODE_ID_FROM_PARTS (Transact-SQL)"
description: "NODE_ID_FROM_PARTS (Transact-SQL)"
author: "arvindshmicrosoft"
ms.author: "arvindsh"
ms.date: 08/16/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "NODE_ID_FROM_PARTS"
helpviewer_keywords:
  - "NODE_ID_FROM_PARTS function"
  - "Graph, system functions, graph ID, node ID, node"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || >= sql-server-2017 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# NODE_ID_FROM_PARTS (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi.md)]

Returns the character representation (JSON) of the node ID for a given object ID and graph ID.

## Syntax  
  
```syntaxsql  
NODE_ID_FROM_PARTS ( object_id, graph_id )
```
  
## Arguments

#### *object_id*

An **int** representing the object ID for the node table.

#### *graph_id*

A **bigint** value for the graph ID for a node.

## Return value

Returns an **nvarchar(1000)** character representation (JSON) of the node ID. The return value can be NULL if any of the supplied arguments are invalid.

## Remarks  

- The character representation (JSON) of the node ID returned by `NODE_ID_FROM_PARTS` is an implementation specific detail, and is subject to change.
- `NODE_ID_FROM_PARTS` is the only supported way to construct a suitable character representation of the node ID.
- `NODE_ID_FROM_PARTS` is useful for bulk inserting of data into a graph table, when the source data has a suitable natural or surrogate key with an integer data type.
- The value returned from `NODE_ID_FROM_PARTS` can be used to populate the `$node_id` column in a node table. It can also be used to populate the `$from_id` / `$to_id` columns in an edge table.
- For `NODE_ID_FROM_PARTS` to return valid character representation (JSON) of a node ID, the `object_id` parameter must correspond to an existing node table. The `graph_id` parameter can be any valid integer, but it need not exist in that node table. If any of these checks fail, `NODE_ID_FROM_PARTS` returns NULL.
  
## Examples

The following example uses the [OPENROWSET Bulk Rowset Provider](../../relational-databases/import-export/bulk-import-large-object-data-with-openrowset-bulk-rowset-provider.md) to retrieve the `ID` and `name` columns from a CSV file stored on an Azure Storage account. It then uses `NODE_ID_FROM_PARTS` to create the appropriate character representation of `$node_id` for eventual (bulk) insert into the `Person` node table. This transformed data is then (bulk) inserted into the `Person` node table.
  
```sql
INSERT INTO Person($node_id, ID, [name])
SELECT NODE_ID_FROM_PARTS(OBJECT_ID('Person'), ID) as node_id, ID, [name]
FROM OPENROWSET (BULK 'person_0_0.csv',
    DATA_SOURCE = 'staging_data_source',
    FORMATFILE = 'format-files/person.xml',
    FORMATFILE_DATA_SOURCE = 'format_files_source',
    FIRSTROW = 2) AS staging_data;
;
```  

## See also  

- [SQL Graph Architecture](../../relational-databases/graphs/sql-graph-architecture.md)  
- [SQL Graph Database Sample](../../relational-databases/graphs/sql-graph-sample.md)
- [OPENROWSET Bulk Rowset Provider](../../relational-databases/import-export/bulk-import-large-object-data-with-openrowset-bulk-rowset-provider.md)
- [EDGE_ID_FROM_PARTS](./edge-id-from-parts-transact-sql.md)
- [GRAPH_ID_FROM_EDGE_ID](./graph-id-from-edge-id-transact-sql.md)
- [GRAPH_ID_FROM_NODE_ID](./graph-id-from-node-id-transact-sql.md)
