---
title: "EDGE_ID_FROM_PARTS (Transact-SQL)"
description: "EDGE_ID_FROM_PARTS (Transact-SQL)"
author: "arvindshmicrosoft"
ms.author: "arvindsh"
ms.date: 08/16/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "EDGE_ID_FROM_PARTS"
helpviewer_keywords:
  - "EDGE_ID_FROM_PARTS function"
  - "Graph, system functions, graph ID, edge ID, edge"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || >= sql-server-2017 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# EDGE_ID_FROM_PARTS (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi.md)]

Returns the character representation (JSON) of the edge ID for a given object ID and graph ID.

## Syntax  
  
```syntaxsql  
EDGE_ID_FROM_PARTS ( object_id, graph_id )
```
  
## Arguments

#### *object_id* 

An **int** representing the object ID for the edge table.

#### *graph_id*

A **bigint** value for the graph ID for an edge.

## Return value

Returns an **nvarchar(1000)** character representation (JSON) of the edge ID. The return value can be NULL if any of the supplied arguments are invalid.

## Remarks  

- The character representation (JSON) of the edge ID returned by `EDGE_ID_FROM_PARTS` is an implementation specific detail, and is subject to change.
- `EDGE_ID_FROM_PARTS` is the only supported way to construct a suitable character representation (JSON) of the edge ID.
- `EDGE_ID_FROM_PARTS` is useful in cases involving bulk insert of data into an edge table, when the source data has a suitable natural or surrogate key with an integer data type.
- The value returned from `EDGE_ID_FROM_PARTS` can be used to populate the `$edge_id` column in an edge table.
- For `EDGE_ID_FROM_PARTS` to return valid character representation (JSON) of an edge ID, the `object_id` parameter must correspond to an existing edge table. The `graph_id` parameter can be any valid integer, but it need not exist in that edge table. If any of these checks fail, `EDGE_ID_FROM_PARTS` returns NULL.
  
## Examples

The following example uses the [OPENROWSET Bulk Rowset Provider](../../relational-databases/import-export/bulk-import-large-object-data-with-openrowset-bulk-rowset-provider.md) to retrieve the `dataset_key` and `rating` columns from a CSV file stored on an Azure Storage account. It then uses `EDGE_ID_FROM_PARTS` to create the character representation of $edge_id, using the `dataset_key` from the CSV file. It also uses [NODE_ID_FROM_PARTS](./node-id-from-parts-transact-sql.md) twice to create the appropriate character representations of $from_id (for the Person node table) and $to_id values (for the Restaurant node table) respectively. This transformed data is then (bulk) inserted into the `likes` edge table.
  
```sql
INSERT INTO likes($edge_id, $from_id, $to_id, rating)
SELECT EDGE_ID_FROM_PARTS(OBJECT_ID('likes'), dataset_key) as from_id
, NODE_ID_FROM_PARTS(OBJECT_ID('Person'), ID) as from_id
, NODE_ID_FROM_PARTS(OBJECT_ID('Restaurant'), ID) as to_id
, rating
FROM OPENROWSET (BULK 'person_likes_restaurant.csv',
    DATA_SOURCE = 'staging_data_source',
    FORMATFILE = 'format-files/likes.xml',
    FORMATFILE_DATA_SOURCE = 'format_files_source',
    FIRSTROW = 2) AS staging_data;
;
```  

## See also  

- [SQL Graph Architecture](../../relational-databases/graphs/sql-graph-architecture.md)  
- [SQL Graph Database Sample](../../relational-databases/graphs/sql-graph-sample.md)
- [OPENROWSET Bulk Rowset Provider](../../relational-databases/import-export/bulk-import-large-object-data-with-openrowset-bulk-rowset-provider.md)
- [NODE_ID_FROM_PARTS](./node-id-from-parts-transact-sql.md)
- [GRAPH_ID_FROM_EDGE_ID](./graph-id-from-edge-id-transact-sql.md)
- [GRAPH_ID_FROM_NODE_ID](./graph-id-from-node-id-transact-sql.md)
