---
title: "OBJECT_ID_FROM_EDGE_ID (Transact-SQL)"
description: "OBJECT_ID_FROM_EDGE_ID (Transact-SQL)"
author: "arvindshmicrosoft"
ms.author: "arvindsh"
ms.date: 08/16/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "OBJECT_ID_FROM_EDGE_ID"
helpviewer_keywords:
  - "OBJECT_ID_FROM_EDGE_ID function"
  - "Graph, system functions, graph ID, edge ID, edge"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || >= sql-server-2017 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# OBJECT_ID_FROM_EDGE_ID (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi.md)]

Returns the object ID for a given graph edge ID.

## Syntax  
  
```syntaxsql  
OBJECT_ID_FROM_EDGE_ID ( edge_id )
```
  
## Arguments

#### *edge_id*

The `$edge_id` pseudo-column in a graph edge table.

## Return value

Returns the `object_id` for the graph table corresponding to the `edge_id` supplied. `object_id` is an **int**. If an invalid `edge_id` is supplied, NULL is returned.

## Remarks

- Owing to the performance overhead of parsing and validating the supplied character representation (JSON) of edges, you should only use `OBJECT_ID_FROM_EDGE_ID` where needed. In most cases, [MATCH](../queries/match-sql-graph.md) should be sufficient for queries over graph tables.
- For `OBJECT_ID_FROM_EDGE_ID` to return a value, the supplied character representation (JSON) of the edge ID must be valid, and the named `schema.table` within the JSON, must be a valid edge table. The graph ID within the character representation (JSON), need not exist in the edge table. It can be any valid integer.
- `OBJECT_ID_FROM_EDGE_ID` is the only supported way to parse the character representation (JSON) of an edge ID.

## Examples

The following example returns the `object_id` for all the `$edge_id` nodes in the `likes` graph edge table. In the [SQL Graph Database Sample](../../relational-databases/graphs/sql-graph-sample.md), the values returned are constant and equal to the `object_id` of the `likes` table (978102525 in this example).
  
```sql
SELECT OBJECT_ID_FROM_EDGE_ID($from_id)
FROM likes;
```

Here are the results:

```output
...
978102525
978102525
978102525
...
```

## See also  

- [SQL Graph Architecture](../../relational-databases/graphs/sql-graph-architecture.md)  
- [SQL Graph Database Sample](../../relational-databases/graphs/sql-graph-sample.md)
- [GRAPH_ID_FROM_EDGE__ID](./graph-id-from-edge-id-transact-sql.md)
- [EDGE_ID_FROM_PARTS](./edge-id-from-parts-transact-sql.md)
