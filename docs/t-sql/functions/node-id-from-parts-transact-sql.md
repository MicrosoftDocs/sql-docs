---
title: "NODE_ID_FROM_PARTS (Transact-SQL)"
description: "NODE_ID_FROM_PARTS (Transact-SQL)"
author: "arvindshmicrosoft"
ms.author: "arvindsh"
ms.date: 08/16/2022
ms.prod: sql
ms.technology: t-sql
ms.topic: reference
ms.custom:
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

Returns the character representation of the node ID for a given an object ID and graph ID.

## Syntax  
  
```syntaxsql  
NODE_ID_FROM_PARTS ( object_id, graph_id )
```
  
## Arguments

 *object_id*
 Is the object ID of the corresponding graph node table. object_id is int.

 *graph_id*
 Is a bigint value for the graph ID for a node.

## Return value

Returns an NVARCHAR(1000) character representation of the node ID. The return value can be NULL if any of the supplied parameters are invalid.

## Remarks  

The character representation (JSON) of the node ID returned by NODE_ID_FROM_PARTS, is an implementation specific detail. NODE_ID_FROM_PARTS is the only supported way to construct a suitable character representation of the node ID. NODE_ID_FROM_PARTS is useful in cases involving bulk insert of graph nodes into a node table.

For NODE_ID_FROM_PARTS to return valid character representation (JSON) of a node ID, the `object_id` parameter must be for an existing node table. However, the supplied `graph_id` need not exist in that node table. `graph_id` just needs to be a valid integer value. If any of these checks fail, NODE_ID_FROM_PARTS returns NULL.
  
## Examples

### Example 1

The following example uses the [OPENROWSET Bulk Rowset Provider](../../relational-databases/import-export/bulk-import-large-object-data-with-openrowset-bulk-rowset-provider.md) to retrieve the `ID` and `name` columns from a CSV file stored on an Azure Storage account. It then uses NODE_ID_FROM_PARTS to create the appropriate character representation of $node_id for eventual (bulk) insert into the `Person` node table.
  
```sql
INSERT INTO Person($node_id, ID, [name])
SELECT NODE_ID_FROM_PARTS(OBJECT_ID('Person'), ID) as node_id, ID, [name]
FROM OPENROWSET (BULK 'person_0_0.csv', DATA_SOURCE = 'staging_data_source', FORMATFILE = 'format-files/person.xml', FORMATFILE_DATA_SOURCE = 'format_files_source', FIRSTROW = 2) AS staging_data;
;
```  

## See also  

- [SQL Graph Architecture](../../relational-databases/graphs/sql-graph-architecture.md)  
- [SQL Graph Database Sample](../../relational-databases/graphs/sql-graph-sample.md)
- [OPENROWSET Bulk Rowset Provider](../../relational-databases/import-export/bulk-import-large-object-data-with-openrowset-bulk-rowset-provider.md)
- [GRAPH_ID_FROM_EDGE_ID](./graph-id-from-edge-id-transact-sql.md)
- [GRAPH_ID_FROM_NODE_ID](./graph-id-from-node-id-transact-sql.md)
