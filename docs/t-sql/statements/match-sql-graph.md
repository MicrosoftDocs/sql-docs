---
title: "MATCH (SQL Graph) | Microsoft Docs"
ms.custom: 
ms.date: "04/19/2017"
ms.prod: "sql-vnext"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "MATCH"
  - "MATCH_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MATCH statement [SQL Server], SQL graph"
  - "SQL graph, MATCH statement"
ms.assetid: 
caps.latest.revision: 1
author: "shkale-msft"
ms.author: "shkale"
manager: "jhubbard"
---

# MATCH (Transact-SQL)
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]  

  Specifies a search condition for a graph. MATCH can be used only with graph node and edge tables, in the SELECT statement as part of ON or WHERE clause.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
MATCH <graph_search_pattern> 

<graph_search_pattern>::=
    <node_alias> { { <-( <edge_alias> )- | -( <edge_alias> )-> } <node_alias> }
  
<node_alias> ::=
    node_table_name | node_alias 

<edge_alias> ::=
    edge_table_name | edge_alias
```

## Arguments  
*graph_search_pattern*

Specifies the pattern to search or path to traverse in the graph. This pattern uses ASCII art syntax to traverse a path in the graph. The pattern goes from one node to another via an edge, in the direction of the arrow provided. Edge names or aliases are provided inside paranthesis. Node names or aliases appear at the two ends of the arrow. The arrow can go in either direction in the pattern.

*node_alias*

Name or alias of the node table provided in the FROM clause

*edge_alias*

Name or alias of the edge table provided in the FROM clause


## Remarks  
The node names inside the match function can be repeated.  In other words, a node can be traversed an arbitrary number of times in the same subquery. 

An edge can go in either direction, but it must have an explicit direction.

A single edge cannot be traversed twice in the same subquery; however, each subquery is independent, so the main query, a subquery, and/or two subqueries can all traverse the same edge. If an edge is used twice in the from clause with different aliases, and implicit predicate edge1<>edge2 is applied. 


## Examples  
#### A.  
 The following example creates a Person node table and friends Edge table, inserts some data and then uses MATCH to find friends of Alice, a person in the graph.

 ```
 -- Create person node table
 CREATE TABLE dbo.Person (ID integer PRIMARY KEY, name varchar(50)) AS NODE;
 CREATE TABLE dbo.friend (start_date DATE) AS EDGE;

 -- Insert into node table
 INSERT INTO dbo.Person VALUES (1, 'Alice');
 INSERT INTO dbo.Person VALUES (2,'John');
 INSERT INTO dbo.Person VALUES (3, 'Jacob');

-- Insert into edge table
INSERT INTO dbo.friend VALUES ((SELECT $node_id FROM dbo.Person WHERE name = 'Alice'),
        (SELECT $node_id FROM dbo.Person WHERE name = 'John'), '9/15/2011');

INSERT INTO dbo.friend VALUES ((SELECT $node_id FROM dbo.Person WHERE name = 'Alice'),
        (SELECT $node_id FROM dbo.Person WHERE name = 'Jacob'), '10/15/2011');

INSERT INTO dbo.friend VALUES ((SELECT $node_id FROM dbo.Person WHERE name = 'John'),
        (SELECT $node_id FROM dbo.Person WHERE name = 'Jacob'), '10/15/2012');

-- use MATCH in SELECT to find friends of Alice
SELECT Person2.name AS FriendName
FROM Person Person1, friend, Person Person2
WHERE MATCH(Person1-(friend)->Person2)
AND Person1.name = 'Alice';

 ```

 #### B.  
 The following example tries to find friend of a friend of Alice. 

 ```
 -- use MATCH in SELECT to find friends of Alice
SELECT Person2.name AS FriendName
FROM Person Person1, friend, Person Person2, friend friend2, Person Person3
WHERE MATCH(Person1-(friend)->Person2-(friend2)->Person3)
AND Person1.name = 'Alice';

 ```

#### C.  
 Following are some more ways in which a pattern can be specified inside MATCH.

 ```
 -- Find a friend
    (Person-(friend)->Person2)
    OR
    (Person2<-(friend)-Person)

-- Find 2 people who are both friends with same person
    (Person1-(friend1)->Person0<-(friend2)-Person2)
    OR
    (Person1-(friend1)->Person0 AND Person2-(friend2)->Person0)

 ```
  

## See Also  
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-sql-graph.md)   
