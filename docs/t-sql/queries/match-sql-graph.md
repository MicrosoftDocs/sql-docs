---
description: "MATCH (Transact-SQL)"
title: "MATCH (SQL Graph) | Microsoft Docs"
ms.custom: ""
ms.date: "06/26/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "MATCH"
  - "MATCH_TSQL"
  - "SHORTEST_PATH"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MATCH statement [SQL Server], SQL graph"
  - "SQL graph, MATCH statement"
  - "Shortest Path, shortest_path"
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "=azuresqldb-current||>=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# MATCH (Transact-SQL)
[!INCLUDE [sqlserver2017-asdb-asdbmi](../../includes/applies-to-version/sqlserver2017-asdb-asdbmi.md)]

  Specifies a search condition for a graph. MATCH can be used only with graph node and edge tables, in the SELECT statement as part of  WHERE clause. 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
MATCH (<graph_search_pattern>)

<graph_search_pattern>::=
  {  
      <simple_match_pattern> 
    | <arbitrary_length_match_pattern>  
    | <arbitrary_length_match_last_node_predicate> 
  }

<simple_match_pattern>::=
  {
      LAST_NODE(<node_alias>) | <node_alias>   { 
          { <-( <edge_alias> )- } 
        | { -( <edge_alias> )-> }
        <node_alias> | LAST_NODE(<node_alias>)
        } 
  }
  [ { AND } { ( <simple_match_pattern> ) } ]
  [ ,...n ]

<node_alias> ::=
  node_table_name | node_table_alias 

<edge_alias> ::=
  edge_table_name | edge_table_alias


<arbitrary_length_match_pattern>  ::=
  { 
    SHORTEST_PATH( 
      <arbitrary_length_pattern> 
      [ { AND } { <arbitrary_length_pattern> } ] 
      [ ,…n] 
    )
  } 

<arbitrary_length_match_last_node_predicate> ::=
  {  LAST_NODE( <node_alias> ) = LAST_NODE( <node_alias> ) }


<arbitrary_length_pattern> ::=
	{  LAST_NODE( <node_alias> )   | <node_alias>
     ( <edge_first_al_pattern> [<edge_first_al_pattern>…,n] )
     <al_pattern_quantifier> 
  }
 	|  ( {<node_first_al_pattern> [<node_first_al_pattern> …,n] )
  	  	<al_pattern_quantifier> 
        LAST_NODE( <node_alias> ) | <node_alias> 
 }
	
<edge_first_al_pattern> ::=
  { (  
        { -( <edge_alias> )->   } 
      | { <-( <edge_alias> )- } 
      <node_alias>
	  ) 
  } 

<node_first_al_pattern> ::=
  { ( 
      <node_alias> 
        { <-( <edge_alias> )- } 
      | { -( <edge_alias> )-> }
 	  ) 
  } 


<al_pattern_quantifier> ::=
  {
	    +
	  | { 1 , n }
  }

n -  positive integer only.
 
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*graph_search_pattern*  
Specifies the pattern to search or path to traverse in the graph. This pattern uses ASCII art syntax to traverse a path in the graph. The pattern goes from one node to another via an edge, in the direction of the arrow provided. Edge names or aliases are provided inside parentheses. Node names or aliases appear at the two ends of the arrow. The arrow can go in either direction in the pattern.

*node_alias*  
Name or alias of a node table provided in the FROM clause.

*edge_alias*  
Name or alias of an edge table provided in the FROM clause.

*SHORTEST_PATH*
Shortest path function is used to find shortest path between two given nodes in a graph or between a given node and all the other nodes in a graph. It takes an arbitrary length pattern as input, that is searched repeatedly in a graph. Introduced in SQL Server 2019. Requires SQL Server 2019 or later.

*arbitrary_length_match_pattern*  
Specifies the nodes and edges that must be traversed repeatedly until the desired node is reached or until the maximum number of iterations as specified in the pattern is met. 

*al_pattern_quantifier*   
The arbitrary length pattern takes regular expression style pattern quantifiers in order to specify the number of times a given search pattern is repeated. The supported search pattern quantifiers are:   
* **+**: Repeat the pattern 1 or more times. Terminate as soon as a shortest path is found.    
* **{1,n}**: Repeat the pattern 1 to *n* times. Terminate as soon as a shortest path is found.     

## Remarks  
The node names inside MATCH can be repeated.  In other words, a node can be traversed an arbitrary number of times in the same query.  
An edge name cannot be repeated inside MATCH.  
An edge can point in either direction, but it must have an explicit direction.  
OR and NOT operators are not supported in the MATCH pattern. 
MATCH can be combined with other expressions using AND in the WHERE clause. However, combining it with other expressions using OR or NOT is not supported. 

## Examples  
### A.  Find a friend 
 The following example creates a Person node table and friends Edge table, inserts some data and then uses MATCH to find friends of Alice, a person in the graph.

 ```sql
 -- Create person node table
 CREATE TABLE dbo.Person (ID INTEGER PRIMARY KEY, name VARCHAR(50)) AS NODE;
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

 ### B.  Find friend of a friend
 The following example tries to find friend of a friend of Alice. 

 ```sql
SELECT Person3.name AS FriendName 
FROM Person Person1, friend, Person Person2, friend friend2, Person Person3
WHERE MATCH(Person1-(friend)->Person2-(friend2)->Person3)
AND Person1.name = 'Alice';
 ```

### C.  More `MATCH` patterns
 Following are some more ways in which a pattern can be specified inside MATCH.

 ```sql
 -- Find a friend
    SELECT Person2.name AS FriendName
    FROM Person Person1, friend, Person Person2
    WHERE MATCH(Person1-(friend)->Person2);
    
-- The pattern can also be expressed as below

    SELECT Person2.name AS FriendName
    FROM Person Person1, friend, Person Person2 
    WHERE MATCH(Person2<-(friend)-Person1);


-- Find 2 people who are both friends with same person
    SELECT Person1.name AS Friend1, Person2.name AS Friend2
    FROM Person Person1, friend friend1, Person Person2, 
         friend friend2, Person Person0
    WHERE MATCH(Person1-(friend1)->Person0<-(friend2)-Person2);
    
-- this pattern can also be expressed as below

    SELECT Person1.name AS Friend1, Person2.name AS Friend2
    FROM Person Person1, friend friend1, Person Person2, 
         friend friend2, Person Person0
    WHERE MATCH(Person1-(friend1)->Person0 AND Person2-(friend2)->Person0);
 ```
 

## See Also  
 [CREATE TABLE &#40;SQL Graph&#41;](../../t-sql/statements/create-table-sql-graph.md)   
 [INSERT (SQL Graph)](../../t-sql/statements/insert-sql-graph.md)]  
 [Graph processing with SQL Server 2017](../../relational-databases/graphs/sql-graph-overview.md)  
