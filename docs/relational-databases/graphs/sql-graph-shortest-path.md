---
description: "SHORTEST_PATH (Transact-SQL)"
title: "SHORTEST PATH (SQL Graph) | Microsoft Docs"
ms.custom: ""
ms.date: 07/01/2020
ms.service: sql
ms.reviewer: ""
ms.subservice:
ms.topic: "language-reference"
f1_keywords:
  - "SHORTEST PATH"
  - "SHORTEST_PATH"
dev_langs:
  - "TSQL"
helpviewer_keywords:
  - "MATCH statement [SQL Server], SQL graph"
  - "SQL graph, MATCH statement"
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-ver15||=azuresqldb-mi-current"
---
# SHORTEST_PATH (Transact-SQL)

[!INCLUDE [sqlserver2019-asdb-asdbmi](../../includes/applies-to-version/sqlserver2019-asdb-asdbmi.md)]

  Specifies a search condition for a graph, which is searched recursively or repetitively. SHORTEST_PATH can be used inside MATCH with graph node and edge tables, in the SELECT statement.

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Shortest Path

The SHORTEST_PATH function lets you find:
* A shortest path between two given nodes/entities
* Single source shortest path(s).
* Shortest path from multiple source nodes to multiple target nodes.

It takes an arbitrary length pattern as input and returns a shortest path that exists between two nodes. This function can only be used inside MATCH. The function returns only one shortest path between any two given nodes. If there exists, two or more shortest paths of the same length between any pair of source and destination node(s), the function returns only one path that was found first during traversal. Note that, an arbitrary length pattern can only be specified inside a SHORTEST_PATH function.

Refer to the [MATCH (SQL Graph)](../../t-sql/queries/match-sql-graph.md) for syntax.

## FOR PATH
FOR PATH must be used with any node or edge table name in the FROM clause, which will participate in an arbitrary length pattern. FOR PATH tells the engine that the node or edge table will return an ordered collection representing the list of nodes or edges found along the path traversed. The attributes from these tables cannot be projected directly in the SELECT clause. To project attributes from these tables, graph path aggregate functions must be used.

## Arbitrary Length Pattern
This pattern includes the nodes and edges that must be traversed repeatedly until the desired node is reached or until the maximum number of iterations as specified in the pattern is met. Each time the query is executed, the result of executing this pattern will be an ordered collection of the nodes and edges traversed along the path from the start node to the end node. This is a regular expression style syntax pattern and the following two pattern quantifiers are supported:

* **'+'**: Repeat the pattern 1 or more times. Terminate as soon as a shortest path is found.
* **{1,n}**: Repeat the pattern 1 to *n* times. Terminate as soon as a shortest  is found.

## LAST_NODE
LAST_NODE() function allows chaining of two arbitrary length traversal patterns. It can be used in scenarios where:
* More than one shortest path patterns are used in a query and one pattern begins at the LAST node of the previous pattern.
* Two shortest path patterns merge at the same LAST_NODE().

## Graph Path Order
Graph path order refers to the order of data in the output path. The output path order always starts at the non-recursive part of the pattern followed by the nodes/edges that appear in the recursive part. The order in which the graph is traversed during query optimization/execution has nothing to do with the order printed in the output. Similarly, the direction of arrow in the recursive pattern also does not affect the graph path order.

## Graph Path Aggregate Functions
Since the nodes and edges involved in arbitrary length pattern return a collection (of node(s) and edge(s) traversed in that path), users cannot project the attributes directly using the conventional tablename.attributename syntax. For queries where it is required to project attribute values from the intermediate node or edge tables, in the path traversed, use following graph path aggregate functions: STRING_AGG, LAST_VALUE, SUM, AVG, MIN, MAX and COUNT.
The general syntax to use these aggregate functions in the SELECT clause is:

```syntaxsql
<GRAPH_PATH_AGGREGATE_FUNCTION>(<expression> , <separator>)  <order_clause>

	<order_clause> ::=
		{ WITHIN GROUP (GRAPH PATH) }

	<GRAPH_PATH_AGGREGATE_FUNCTION> ::=
		  STRING_AGG
		| LAST_VALUE
		| SUM
		| COUNT
		| AVG
	 	| MIN
		| MAX

```

### STRING_AGG
The STRING_AGG function takes an expression and separator as input and returns a string. Users can use this function in the SELECT clause to project attributes from the intermediate nodes or edges in the path traversed.

### LAST_VALUE
To project attributes from the last node of path traversed, LAST_VALUE aggregate function can be used. It is an error to provide edge table alias as an input to this function, only node table names or aliases can be used.

**Last Node**: The last node refers to the node which appears last in the path traversed, irrespective of the direction of arrow in the MATCH predicate. For example: `MATCH(SHORTEST_PATH(n(-(e)->p)+) )`. Here the last node in the path will be the last visited P node.

Whereas, last node is the last Nth node in the output graph path for this pattern: `MATCH(SHORTEST_PATH((n<-(e)-)+p))`

### SUM
This function returns the sum of provided node/edge attribute values or expression that appeared in the traversed path.

### COUNT
This function returns the number of non-null values of the desired node/edge attribute in the path. The COUNT function supports the '\*' operator with a node or edge table alias. Without the node or edge table alias, the usage of \* is ambiguous and will result in an error.

```syntaxsql
{  COUNT( <expression> | <node_or_edge_alias>.* )  <order_clause>  }
```

### AVG
Returns the average of provided node/edge attribute values or expression that appeared in the traversed path.

### MIN
Returns the minimum value from the provided node/edge attribute values or expression that appeared in the traversed path.

### MAX
Returns the maximum value from the provided node/edge attribute values or expression that appeared in the traversed path.

## Remarks
* The SHORTEST_PATH function can only be used inside MATCH.
* The LAST_NODE function is only supported inside SHORTEST_PATH.
* The SHORTEST_PATH function will return _any one_ shortest path between nodes. It currently does not support returning _all shortest paths_ between nodes; it also does not support returning _all paths_ between nodes.
* The SHORTEST_PATH implementation finds an unweighted shortest path.
* In some cases, bad plans may be generated for queries with higher number of hops, which results in higher query execution times. Evaluate if query hints like OPTION (HASH JOIN) and / or OPTION (MAXDOP 1) help.

## Examples
For the example queries shown here, we leverage the node and edge tables created in [SQL Graph sample](./sql-graph-sample.md)

### A.  Find shortest path between 2 people
 In the following example, we find shortest path between Jacob and Alice. We will need the Person node and FriendOf edge created from graph sample script.

```sql
SELECT PersonName, Friends
FROM (
	SELECT
		Person1.name AS PersonName,
		STRING_AGG(Person2.name, '->') WITHIN GROUP (GRAPH PATH) AS Friends,
		LAST_VALUE(Person2.name) WITHIN GROUP (GRAPH PATH) AS LastNode
	FROM
		Person AS Person1,
		friendOf FOR PATH AS fo,
		Person FOR PATH  AS Person2
	WHERE MATCH(SHORTEST_PATH(Person1(-(fo)->Person2)+))
	AND Person1.name = 'Jacob'
) AS Q
WHERE Q.LastNode = 'Alice'
```

 ### B.  Find shortest path from a given node to all other nodes in the graph.
 The following  example finds all the people that Jacob is connected to in the graph and the shortest path starting from Jacob to all those people.

```sql
SELECT
	Person1.name AS PersonName,
	STRING_AGG(Person2.name, '->') WITHIN GROUP (GRAPH PATH) AS Friends
FROM
	Person AS Person1,
	friendOf FOR PATH AS fo,
	Person FOR PATH  AS Person2
WHERE MATCH(SHORTEST_PATH(Person1(-(fo)->Person2)+))
AND Person1.name = 'Jacob'
```

### C.  Count the number of hops/levels traversed to go from one person to another in the graph.
 The following example finds the shortest path between Jacob and Alice and prints the number of hops it takes to go from Jacob to Alice.

```sql
 SELECT PersonName, Friends, levels
FROM (
	SELECT
		Person1.name AS PersonName,
		STRING_AGG(Person2.name, '->') WITHIN GROUP (GRAPH PATH) AS Friends,
		LAST_VALUE(Person2.name) WITHIN GROUP (GRAPH PATH) AS LastNode,
		COUNT(Person2.name) WITHIN GROUP (GRAPH PATH) AS levels
	FROM
		Person AS Person1,
		friendOf FOR PATH AS fo,
		Person FOR PATH  AS Person2
	WHERE MATCH(SHORTEST_PATH(Person1(-(fo)->Person2)+))
	AND Person1.name = 'Jacob'
) AS Q
WHERE Q.LastNode = 'Alice'
```

### D. Find people 1-3 hops away from a given person
The following example finds the shortest path between Jacob and all the people that he is connected to in the graph 1-3 hops away from him.

```sql
SELECT
	Person1.name AS PersonName,
	STRING_AGG(Person2.name, '->') WITHIN GROUP (GRAPH PATH) AS Friends
FROM
	Person AS Person1,
	friendOf FOR PATH AS fo,
	Person FOR PATH  AS Person2
WHERE MATCH(SHORTEST_PATH(Person1(-(fo)->Person2){1,3}))
AND Person1.name = 'Jacob'
```

### E. Find people exactly 2 hops away from a given person
The following example finds the shortest path between Jacob and people who are exactly 2 hops away from him in the graph.

```sql
SELECT PersonName, Friends
FROM (
	SELECT
		Person1.name AS PersonName,
		STRING_AGG(Person2.name, '->') WITHIN GROUP (GRAPH PATH) AS Friends,
		COUNT(Person2.name) WITHIN GROUP (GRAPH PATH) AS levels
	FROM
		Person AS Person1,
		friendOf FOR PATH AS fo,
		Person FOR PATH  AS Person2
	WHERE MATCH(SHORTEST_PATH(Person1(-(fo)->Person2){1,3}))
	AND Person1.name = 'Jacob'
) Q
WHERE Q.levels = 2
```

### F. Find people 1-3 hops away from a given person, who also like a specific restaurant
The following example finds the shortest path between Jacob and all the people that he is connected to in the graph 1-3 hops away from him; and also filters only those connected people by their liking for a given restaurant. Note in the below sample, that LAST_NODE(Person2) returns the final node for each shortest-path traversal, thereby allowing that Person node to be "chained" for further traversals to find the Restaurant(s) that they like.

```sql
SELECT
	Person1.name AS PersonName,
	STRING_AGG(Person2.name, '->') WITHIN GROUP (GRAPH PATH) AS Friends,
	Restaurant.name
FROM
	Person AS Person1,
	friendOf FOR PATH AS fo,
	Person FOR PATH  AS Person2,
	likes,
	Restaurant
WHERE MATCH(SHORTEST_PATH(Person1(-(fo)->Person2){1,3}) AND LAST_NODE(Person2)-(likes)->Restaurant )
AND Person1.name = 'Jacob'
AND Restaurant.name = 'Ginger and Spice'
```

### G.  Find the shortest path from a given node to all other nodes in the graph, excluding "loops".
The following  example finds all the people that Jacob is connected to in the graph and the shortest path starting from Alice to all those people. The example explicitly checks for "loops" where the start and the end node of the path happen to be the same.

```sql
SELECT PersonName, Friends
FROM (
	SELECT
		Person1.name AS PersonName,
		STRING_AGG(Person2.name, '->') WITHIN GROUP (GRAPH PATH) AS Friends,
		LAST_VALUE(Person2.name) WITHIN GROUP (GRAPH PATH) AS LastNode
	FROM
		Person AS Person1,
		friendOf FOR PATH AS fo,
		Person FOR PATH  AS Person2
	WHERE MATCH(SHORTEST_PATH(Person1(-(fo)->Person2)+))
	AND Person1.name = 'Alice'
) AS Q
WHERE Q.LastNode != 'Alice'
```

## See Also
 [MATCH (SQL Graph)](../../t-sql/queries/match-sql-graph.md)
 [CREATE TABLE &#40;SQL Graph&#41;](../../t-sql/statements/create-table-sql-graph.md)
 [INSERT (SQL Graph)](../../t-sql/statements/insert-sql-graph.md)]
 [Graph processing with SQL Server 2017](../../relational-databases/graphs/sql-graph-overview.md)

