---
title: "GRAPH_ID_FROM_EDGE_ID (Transact-SQL)"
description: "GRAPH_ID_FROM_EDGE_ID (Transact-SQL)"
author: "arvindshmicrosoft"
ms.author: "arvindsh"
ms.date: 08/16/2022
ms.prod: sql
ms.technology: t-sql
ms.topic: reference
ms.custom:
f1_keywords:
  - "GRAPH_ID_FROM_EDGE_ID"
helpviewer_keywords:
  - "GRAPH_ID_FROM_EDGE_ID function"
  - "Graph, system functions, graph ID, edge ID, edge"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || >= sql-server-2017 || >= sql-server-linux-ver17"
---
# GRAPH_ID_FROM_EDGE_ID (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi.md)]

Returns the internal graph ID value for a given graph edge ID.

## Syntax  
  
```syntaxsql  
GRAPH_ID_FROM_EDGE_ID ( edge_id )
```
  
## Arguments

 *edge_id*
 Is the $edge_id pseudo-column in a graph edge table.

## Return value

Returns the internal graph ID value, which is currently a `bigint`.

## Remarks  

Graph IDs are an implementation specific detail. The data type and behavior of graph IDs are subject to change. For example, you shouldn't assume that graph IDs in a given edge table are sequential.
  
## Examples

### Example 1

The following example returns the internal graph ID value for all edges in the `friendOf` graph edge table.
  
```sql
SELECT GRAPH_ID_FROM_EDGE_ID($edge_id)
FROM friendOf;
```  

**Result**

```
...
25073
98943
69725
68781
30354
...

```

## See also  

 [SQL Graph Architecture](../../relational-databases/graphs/sql-graph-architecture.md)  
 [SQL Graph Database Sample](../../relational-databases/graphs/sql-graph-sample.md)
 [GRAPH_ID_FROM_NODE_ID](./graph-id-from-node-id-transact-sql.md)
 [NODE_ID_FROM_PARTS](./node-id-from-parts-transact-sql.md)
 