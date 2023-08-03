---
title: "SHORTEST PATH (SQL Graph)"
description: "The SHORTEST_PATH function can be used inside MATCH with graph node and edge tables to find the shortest path between nodes/entities."
author: MikeRayMSFT
ms.author: mikeray
ms.date: 06/28/2023
ms.service: sql
ms.topic: "language-reference"
f1_keywords:
  - "SHORTEST PATH"
  - "SHORTEST_PATH"
helpviewer_keywords:
  - "MATCH statement [SQL Server], SQL graph"
  - "SQL graph, MATCH statement"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# SHORTEST_PATH (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sqlserver2019-and-later-asdb-asdbmi.md)]

  Specifies a search condition for a graph, which is searched recursively or repetitively. SHORTEST_PATH can be used inside MATCH with graph node and edge tables, in the SELECT statement.

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Shortest path

The SHORTEST_PATH function lets you find:

- A shortest path between two given nodes/entities
- Single source shortest path(s).
- Shortest path from multiple source nodes to multiple target nodes.

It takes an arbitrary length pattern as input and returns a shortest path that exists between two nodes. This function can only be used inside MATCH. The function returns only one shortest path between any two given nodes. If there exist, two or more shortest paths of the same length between any pair of source and destination node(s), the function returns only one path that was found first during traversal. An arbitrary length pattern can only be specified inside a SHORTEST_PATH function.

For complete syntax, refer to [MATCH (SQL Graph)](../../t-sql/queries/match-sql-graph.md).

## FOR PATH

FOR PATH must be used with any node or edge table name in the FROM clause, which participates in an arbitrary length pattern. FOR PATH tells the engine that the node or edge table returns an ordered collection representing the list of nodes or edges found along the path traversed. The attributes from these tables can't be projected directly in the SELECT clause. To project attributes from these tables, graph path aggregate functions must be used.

## Arbitrary length pattern

This pattern includes the nodes and edges that must be traversed repeatedly until either:

- The desired node is reached.
- The maximum number of iterations as specified in the pattern is met.

The following two pattern quantifiers are supported:

- **'+'**: Repeat the pattern 1 or more times. Terminate as soon as a shortest path is found.
- **{1,n}**: Repeat the pattern 1 to *n* times. Terminate as soon as a shortest  is found.

## LAST_NODE

LAST_NODE() function allows chaining of two arbitrary length traversal patterns. It can be used in scenarios where:

- More than one shortest path patterns are used in a query and one pattern begins at the LAST node of the previous pattern.
- Two shortest path patterns merge at the same LAST_NODE().

## Graph path order

Graph path order refers to the order of data in the output path. The output path order always starts at the nonrecursive part of the pattern followed by the nodes/edges that appear in the recursive part. The order in which the graph is traversed during query optimization/execution has nothing to do with the order printed in the output. Similarly, the direction of arrow in the recursive pattern also doesn't affect the graph path order.

## Graph path aggregate functions

Since the nodes and edges involved in arbitrary length pattern return a collection (of node(s) and edge(s) traversed in that path), users can't project the attributes directly using the conventional tablename.attributename syntax. For queries where it's required to project attribute values from the intermediate node or edge tables, in the path traversed, use following graph path aggregate functions: STRING_AGG, LAST_VALUE, SUM, AVG, MIN, MAX and COUNT.
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

To project attributes from the last node of path traversed, LAST_VALUE aggregate function can be used. It's an error to provide edge table alias as an input to this function, only node table names or aliases can be used.

**Last Node**: The last node refers to the node that appears last in the path traversed, irrespective of the direction of arrow in the MATCH predicate. For example: `MATCH(SHORTEST_PATH(n(-(e)->p)+) )`. Here the last node in the path is the last visited P node.

In the `MATCH(SHORTEST_PATH((n<-(e)-)+p))` pattern, the last node is the last N node visited.

### SUM

This function returns the sum of provided node/edge attribute values or expression that appeared in the traversed path.

### COUNT

This function returns the number of non-null values of the specified node/edge attribute in the path. The COUNT function doesn't support the `*` operator - attempted usage of `*`  results in a syntax error.

```syntaxsql
{  COUNT( <expression> )  <order_clause>  }
```

### AVG

Returns the average of provided node/edge attribute values or expression that appeared in the traversed path.

### MIN

Returns the minimum value from the provided node/edge attribute values or expression that appeared in the traversed path.

### MAX

Returns the maximum value from the provided node/edge attribute values or expression that appeared in the traversed path.

## Remarks

- The SHORTEST_PATH function can only be used inside MATCH.
- The LAST_NODE function is only supported inside SHORTEST_PATH.
- The SHORTEST_PATH function returns _any one_ shortest path between nodes. It currently doesn't support returning _all shortest paths_ between nodes; it also doesn't support returning _all paths_ between nodes.
- The SHORTEST_PATH implementation finds an unweighted shortest path.
- In some cases, bad plans may be generated for queries with higher number of hops, which results in higher query execution times. Evaluate if query hints like OPTION (HASH JOIN) and / or OPTION (MAXDOP 1) help.

## Examples

For the example queries shown here, we use the node and edge tables created in [SQL Graph sample](./sql-graph-sample.md)

### A. Find shortest path between two people

In the following example, we find shortest path between Jacob and Alice. We need the `Person` node and `friendOf` edge created from [SQL Graph sample](./sql-graph-sample.md).

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

### B. Find shortest path from a given node to all other nodes in the graph.

The following example finds all the people that Jacob is connected to in the graph and the shortest path starting from Jacob to all those people.

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

### C. Count the number of hops/levels traversed to go from one person to another in the graph.

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

The following example finds the shortest path between Jacob and all the people that Jacob is connected to in the graph one to three hops away from him.

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

### E. Find people exactly two hops away from a given person

The following example finds the shortest path between Jacob and people who are exactly two hops away from him in the graph.

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

The following example finds the shortest path between Jacob and all the people that he's connected to in the graph 1-3 hops away from him. The query also filters connected people by their liking a given restaurant. In the below sample, that `LAST_NODE(Person2)` returns the final node for each shortest path. The "last" `Person` node obtained from `LAST_NODE` can then be "chained" for further traversals to find the restaurant(s) that they like.

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

### G. Find the shortest path from a given node to all other nodes in the graph, excluding "loops"

The following example finds all the people that Alice is connected to in the graph and the shortest path starting from Alice to all those people. The example explicitly checks for "loops" where the start and the end node of the path happen to be the same.

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

## Next steps

- [MATCH (SQL Graph)](../../t-sql/queries/match-sql-graph.md)
- [CREATE TABLE (SQL Graph)](../../t-sql/statements/create-table-sql-graph.md)
- [INSERT (SQL Graph)](../../t-sql/statements/insert-sql-graph.md)]
- [Graph processing](../../relational-databases/graphs/sql-graph-overview.md)