---
title: "GRAPH_ID_FROM_NODE_ID (Transact-SQL)"
description: "GRAPH_ID_FROM_NODE_ID (Transact-SQL)"
author: "arvindshmicrosoft"
ms.author: "arvindsh"
ms.date: 08/16/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "GRAPH_ID_FROM_NODE_ID"
helpviewer_keywords:
  - "GRAPH_ID_FROM_NODE_ID function"
  - "Graph, system functions, graph ID, node ID, node"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || >= sql-server-2017 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# GRAPH_ID_FROM_NODE_ID (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi.md)]

Returns the internal graph ID for a given node ID.

## Syntax  
  
```syntaxsql  
GRAPH_ID_FROM_NODE_ID ( node_id )
```
  
## Arguments

#### *node_id*

The character representation (JSON) for one of the below:

- The `$node_id` pseudo-column for a node table.
- The `$from_id` pseudo-column for an edge table.
- The `$to_id` column for an edge table.

## Return value

Returns the internal graph ID, which is a **bigint**.

## Remarks

- Owing to the performance overhead of parsing and validating the supplied character representation (JSON) of nodes, you should only use `GRAPH_ID_FROM_NODE_ID` where needed. In most cases, [MATCH](../queries/match-sql-graph.md) should be sufficient for queries over graph tables.
- For `GRAPH_ID_FROM_NODE_ID` to return a value, the supplied character representation (JSON) must be valid and the named `schema.table` within the JSON, must be a valid node table.
- If a graph ID is returned by the function, it's only guaranteed that it will be a valid integer. No checks are made whether the graph ID is present in the node table.
- The data type and behavior of graph IDs are implementation specific details, and are subject to change. For example, you shouldn't assume that graph IDs in a given node table are sequential.
  
## Examples

The following example returns the internal graph ID for the nodes in the `Person` node table.
  
```sql
SELECT GRAPH_ID_FROM_NODE_ID($node_id)
FROM Person;
```  

Here are the results:

```output
...
1764
1806
19051
...
```

## See also  

- [SQL Graph Architecture](../../relational-databases/graphs/sql-graph-architecture.md)  
- [SQL Graph Database Sample](../../relational-databases/graphs/sql-graph-sample.md)
- [GRAPH_ID_FROM_EDGE_ID](./graph-id-from-edge-id-transact-sql.md)
- [NODE_ID_FROM_PARTS](./node-id-from-parts-transact-sql.md)
- [MATCH](../queries/match-sql-graph.md)
