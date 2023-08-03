---
title: "GRAPH_ID_FROM_EDGE_ID (Transact-SQL)"
description: "GRAPH_ID_FROM_EDGE_ID (Transact-SQL)"
author: "arvindshmicrosoft"
ms.author: "arvindsh"
ms.date: 08/16/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "GRAPH_ID_FROM_EDGE_ID"
helpviewer_keywords:
  - "GRAPH_ID_FROM_EDGE_ID function"
  - "Graph, system functions, graph ID, edge ID, edge"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || >= sql-server-2017 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# GRAPH_ID_FROM_EDGE_ID (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi.md)]

Returns the internal graph ID for a given edge ID.

## Syntax  
  
```syntaxsql  
GRAPH_ID_FROM_EDGE_ID ( edge_id )
```
  
## Arguments

#### *edge_id*

The character representation (JSON) of the `$edge_id` pseudo-column in an edge table.

## Return value

Returns the internal graph ID, which is a **bigint**.

## Remarks  

- Owing to the performance overhead of parsing and validating the supplied character representation (JSON) of edges, you should only use `GRAPH_ID_FROM_EDGE_ID` where needed. In most cases, [MATCH](../queries/match-sql-graph.md) should be sufficient for queries over graph tables.
- For `GRAPH_ID_FROM_EDGE_ID` to return a value, the supplied character JSON must be valid and the named `schema.table` within the JSON, must be a valid edge table.
- If a graph ID is returned by the function, it's only guaranteed that it will be a valid integer. No checks are made whether the graph ID is present in the edge table.
- The data type and behavior of graph IDs are implementation specific details, and are subject to change. For example, you shouldn't assume that graph IDs in a given edge table are sequential.

## Examples

The following example returns the internal graph ID for the edges in the `friendOf` edge table.
  
```sql
SELECT GRAPH_ID_FROM_EDGE_ID($edge_id)
FROM friendOf;
```  

Here are the results:

```output
...
25073
98943
69725
68781
30354
...
```

## See also  

- [SQL Graph Architecture](../../relational-databases/graphs/sql-graph-architecture.md)  
- [SQL Graph Database Sample](../../relational-databases/graphs/sql-graph-sample.md)
- [GRAPH_ID_FROM_NODE_ID](./graph-id-from-node-id-transact-sql.md)
- [EDGE_ID_FROM_PARTS](./edge-id-from-parts-transact-sql.md)
- [MATCH](../queries/match-sql-graph.md)
