---
title: "OBJECT_ID_FROM_NODE_ID (Transact-SQL)"
description: "OBJECT_ID_FROM_NODE_ID (Transact-SQL)"
author: "arvindshmicrosoft"
ms.author: "arvindsh"
ms.date: 08/16/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "OBJECT_ID_FROM_NODE_ID"
helpviewer_keywords:
  - "OBJECT_ID_FROM_NODE_ID function"
  - "Graph, system functions, graph ID, node ID, node"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || >= sql-server-2017 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# OBJECT_ID_FROM_NODE_ID (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi.md)]

Returns the object ID for a given graph node ID.

## Syntax  
  
```syntaxsql  
OBJECT_ID_FROM_NODE_ID ( node_id )
```
  
## Arguments

#### *node_id*

The character representation (JSON) for one of the following items:

- The `$node_id` pseudo-column for a node table.
- The `$from_id` pseudo-column for an edge table.
- The `$to_id` column for an edge table.

## Return value

Returns the `object_id` for the graph table corresponding to the `node_id` supplied. `object_id` is an **int**. If an invalid `node_id` is supplied, NULL is returned.

## Remarks

- Owing to the performance overhead of parsing and validating the supplied character representation (JSON) of nodes, you should only use `OBJECT_ID_FROM_NODE_ID` where needed. In most cases, [MATCH](../queries/match-sql-graph.md) should be sufficient for queries over graph tables.
- For `OBJECT_ID_FROM_NODE_ID` to return a value, the supplied character representation (JSON) of the node ID must be valid, and the named `schema.table` within the JSON, must be a valid node table. The graph ID within the character representation (JSON), need not exist in the node table. It can be any valid integer.
- `OBJECT_ID_FROM_NODE_ID` is the only supported way to parse the character representation (JSON) of a node ID.

## Examples

The following example returns the `object_id` for all the `$from_id` nodes in the `likes` graph edge table. In the [SQL Graph Database Sample](../../relational-databases/graphs/sql-graph-sample.md), we only use the `$node_id` values from the `Person` table as the corresponding `$from_id` values in `likes`. Therefore, the values returned are constant and equal to the `object_id` of the `Person` table (1525580473 in this example).
  
```sql
SELECT OBJECT_ID_FROM_NODE_ID($from_id)
FROM likes;
```  

Here are the results:

```output
...
1525580473
1525580473
1525580473
...
```

## See also  

- [SQL Graph Architecture](../../relational-databases/graphs/sql-graph-architecture.md)  
- [SQL Graph Database Sample](../../relational-databases/graphs/sql-graph-sample.md)
- [GRAPH_ID_FROM_NODE_ID](./graph-id-from-node-id-transact-sql.md)
- [NODE_ID_FROM_PARTS](./node-id-from-parts-transact-sql.md)
