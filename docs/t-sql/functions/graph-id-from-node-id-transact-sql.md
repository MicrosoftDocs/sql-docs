---
title: "GRAPH_ID_FROM_NODE_ID (Transact-SQL)"
description: "GRAPH_ID_FROM_NODE_ID (Transact-SQL)"
author: "arvindshmicrosoft"
ms.author: "arvindsh"
ms.date: 08/16/2022
ms.prod: sql
ms.technology: t-sql
ms.topic: reference
ms.custom:
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

Returns the internal graph ID value for a given graph node ID.

## Syntax  
  
```syntaxsql  
GRAPH_ID_FROM_NODE_ID ( node_id )
```
  
## Arguments

 *node_id*
 Is either the $node_id pseudo-column in a graph node table, or the $from_id pseudo-column for a graph edge table, or the $to_id column for a graph edge table.

## Return value

Returns the internal graph ID value, which is currently a `bigint`.

## Remarks  

Graph IDs are an implementation specific detail. The data type and behavior of graph IDs are subject to change. For example, you shouldn't assume that graph IDs in a given node table are sequential.
  
## Examples

### Example 1

The following example returns the internal graph ID value for all nodes in the `Person` graph node table.
  
```sql
SELECT GRAPH_ID_FROM_NODE_ID($node_id)
FROM Person;
```  

**Result**

```
...
1764
1806
19051
...

```

## See also  

 [SQL Graph Architecture](../../relational-databases/graphs/sql-graph-architecture.md)  
 [SQL Graph Database Sample](../../relational-databases/graphs/sql-graph-sample.md)
 [GRAPH_ID_FROM_EDGE_ID](./graph-id-from-edge-id-transact-sql.md)
