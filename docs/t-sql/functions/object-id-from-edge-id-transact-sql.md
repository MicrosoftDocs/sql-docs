---
title: "OBJECT_ID_FROM_EDGE_ID (Transact-SQL)"
description: "OBJECT_ID_FROM_EDGE_ID (Transact-SQL)"
author: "WilliamDAssafMSFT"
ms.author: "wiassaf"
ms.date: 08/25/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "OBJECT_ID_FROM_EDGE_ID"
helpviewer_keywords:
  - "OBJECT_ID_FROM_EDGE_ID function"
  - "Graph, system functions, graph ID, edge ID, edge"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# OBJECT_ID_FROM_EDGE_ID (Transact-SQL)
[!INCLUDE [SQL Server 2017 Azure SQL Database Azure SQL Managed Instance SQL database in Fabric](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi-fabricsqldb.md)]

In SQL Graph tables, the `OBJECT_ID_FROM_EDGE_ID` function returns the object ID for a given graph edge ID.

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

Graph tables were introduced in SQL Server 2017. The `OBJECT_ID_FROM_EDGE_ID` function isn't available in SQL Server 2016 or in Fabric Data Warehouse.

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

## Related content

- [SQL Graph Architecture](../../relational-databases/graphs/sql-graph-architecture.md)
- [Create a graph database and run some pattern matching queries using T-SQL](../../relational-databases/graphs/sql-graph-sample.md)
- [GRAPH_ID_FROM_EDGE_ID (Transact-SQL)](graph-id-from-edge-id-transact-sql.md)
- [EDGE_ID_FROM_PARTS (Transact-SQL)](edge-id-from-parts-transact-sql.md)
