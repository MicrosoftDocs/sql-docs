---
title: "EDGE_ID_FROM_PARTS (Transact-SQL)"
description: "EDGE_ID_FROM_PARTS (Transact-SQL)"
author: "arvindshmicrosoft"
ms.author: "arvindsh"
ms.date: 08/16/2022
ms.prod: sql
ms.technology: t-sql
ms.topic: reference
ms.custom:
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

Returns the character representation of the edge ID for a given an object ID and graph ID.

## Syntax  
  
```syntaxsql  
EDGE_ID_FROM_PARTS ( object_id, graph_id )
```
  
## Arguments

 *object_id*
 Is an int representing the object ID of the corresponding graph edge table.

 *graph_id*
 Is a bigint value for the graph ID for an edge.

## Return value

Returns an NVARCHAR(1000) character representation of the edge ID. The return value can be NULL if any of the supplied parameters are invalid.

## Remarks  

The character representation (JSON) of the edge ID returned by EDGE_ID_FROM_PARTS, is an implementation specific detail. EDGE_ID_FROM_PARTS is the only supported way to construct a suitable character representation (JSON) of the edge ID. EDGE_ID_FROM_PARTS is useful in cases involving bulk insert of graph nodes into a node table, and is used along with [NODE_ID_FROM_PARTS](./node-id-from-parts-transact-sql.md).

For EDGE_ID_FROM_PARTS to return valid character representation (JSON) of a node ID, the `object_id` parameter must be for an existing edge table. However, the `graph_id` parameter needn't correspond to an existing edge in that edge table. `graph_id` just needs to be a valid integer value. If any of these checks fail, EDGE_ID_FROM_PARTS returns NULL.
  
## Examples

### Example 1

The following example uses the [OPENROWSET Bulk Rowset Provider](../../relational-databases/import-export/bulk-import-large-object-data-with-openrowset-bulk-rowset-provider.md) to retrieve the `dataset_key` and `rating` columns from a CSV file stored on an Azure Storage account. It then uses EDGE_ID_FROM_PARTS to create the character representation of $edge_id, using the `dataset_key` from the CSV file. It also uses [NODE_ID_FROM_PARTS](./node-id-from-parts-transact-sql.md) twice to create the appropriate character representations of $from_id (for the Person node table) and $to_id values (for the Restaurant node table) respectively. This data is then (bulk) inserted into the `likes` edge table. This approach can be efficient to populate an edge table when the source data already has integers as natural or surrogate keys.
  
```sql
INSERT INTO likes($edge_id, $from_id, $to_id, rating)
SELECT EDGE_ID_FROM_PARTS(OBJECT_ID('likes'), dataset_key) as from_id
, NODE_ID_FROM_PARTS(OBJECT_ID('Person'), ID) as from_id
, NODE_ID_FROM_PARTS(OBJECT_ID('Restaurant'), ID) as to_id
, rating
FROM OPENROWSET (BULK 'person_likes_restaurant.csv', DATA_SOURCE = 'staging_data_source', FORMATFILE = 'format-files/likes.xml', FORMATFILE_DATA_SOURCE = 'format_files_source', FIRSTROW = 2) AS staging_data;
;
```  

## See also  

- [SQL Graph Architecture](../../relational-databases/graphs/sql-graph-architecture.md)  
- [SQL Graph Database Sample](../../relational-databases/graphs/sql-graph-sample.md)
- [OPENROWSET Bulk Rowset Provider](../../relational-databases/import-export/bulk-import-large-object-data-with-openrowset-bulk-rowset-provider.md)
- [NODE_ID_FROM_PARTS](./node-id-from-parts-transact-sql.md)
- [GRAPH_ID_FROM_EDGE_ID](./graph-id-from-edge-id-transact-sql.md)
- [GRAPH_ID_FROM_NODE_ID](./graph-id-from-node-id-transact-sql.md)
