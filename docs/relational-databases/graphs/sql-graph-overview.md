---
title: "SQL Graph Overview | Microsoft Docs"
ms.custom: 
ms.date: "04/19/2017"
ms.prod: "sql-vnext"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "SQL graph"
  - "SQL graph, overview"
ms.assetid: 
caps.latest.revision: 1
author: "shkale-msft"
ms.author: "shkale"
manager: "jhubbard"
---
# SQL graph database for many-to-many relationships  
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]   

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] offers graph database capabilities to model many-to-many relationships. The graph relationships are integrated into Transact-SQL (T-SQL) and receive the benefits of using SQL Server as the foundational database management system. 

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is trusted by many customers for enterprise grade, mission critical workloads, which have to store and process large volumes of data. Technologies like in-memory OLTP and columnstore, have helped our customers to improve application performance by multiple folds. But, when it comes to hierarchical data with complex relationships or data that share multiple relationships, one might find themselves struggling with a good schema design to represent all the entities and relationships, and writing optimal queries to analyze complex data spread across these tables. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses foreign keys or joins to handle relationships between entities or tables. But, foreign keys can only represent one-to-many relationships. To model many-to-many relationships, the general approach is to introduce a table that holds such relationship. For example, Student and Course in a school share many to many relationship; a Student takes multiple Courses and a Course is taken by multiple Students. To represent this kind of relationship one can create an 'Attends' table to hold information about all the Courses a Student is taking. The 'Attends' table can then store some extra information like the dates when a given Student took this Course, etc. 
But, with time applications tend to evolve and get more complex. New relationships could evolve, for example, now the Student is also 'Volunteering' in a Course or is 'Mentoring' others. Same entities can share more than one relationships with each other. Writing queries to analyze complex and huge volume of data connected by means of foreign keys or multiple tables, involving multiple joins across these tables, is not the most desired task. The queries can get very complex, very quickly; therefore, resulting in complex execution plans and degraded query performance over time. 
We live in an era of connected information; where people, machines, devices, businesses across the globe are connected to each other more than ever before. To facilitate analysis over data connected by means of complex relationships, we are introducing graph extensions to [!INCLUDE[sssqlv14](../../includes/sssqlv14-md.md)]. 
 
## What is a graph database?
A Graph Database is a collection of nodes (or vertices) and edges (or relationships). A node represents an entity (for example, a person or an organization) and an edge represents a relationship between the two nodes that it connects. Starting with [!INCLUDE[sssqlv14](../../includes/sssqlv14-md.md)], [!INCLUDE[tsql-md](../../includes/tsql-md.md)] allows a graph schema that contains objects for nodes and edges.  After defining a graph, you can use T-SQL to find a pattern or multi-hop through a graph. 
These graph capabilities are fully integrated into the relational database engine. The database management capabilities such as backup and restore, import and export just work out of the box with SQL Graph. Since the graph database is part of SQL, you can combine the graph database with other SQL technologies.. 
 
### T-SQL Extensions  
**DDL Extensions:** `CREATE TABLE` DDL is extended to support creation of Node or Edge tables. Since, Nodes and Edges are stored as tables, any operation that is supported on relational tables is supported on graph Node or Edge tables. All the other DDL statements like `ALTER TABLE`, `DROP TABLE`, `CREATE INDEX`, etc. work on the Node and Edge tables, just the way they would work on relational tables.  
**DML Extensions:** `INSERT`, `UPDATE`, `DELETE` DML statements now support inserting, updating or deleting to/from a Node or Edge table. `MERGE` DML is not supported in the first release.  
**SELECT Extensions:** A new `MATCH` clause is introduced to support pattern matching and multi-hop navigation through the graph. Along with this, all the existing constructs of the query language work as it is on Node or Edge tables. The new MATCH clause, however, works only on the Node or Edge tables in this release. 
 
### Node Table  
A Node table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], represents an entity in the system. For example, a Person node in a social network graph represents all the people in that graph and Organization node would represent all the known Organizations. For every Node table that is created, one implicit $node_id column is created, which uniquely identifies that node within the database. We recommend users create a unique constraint or index on this column at the time of creation of Node table, but if an index or constraint is not created, we will create a default non-clustered unique index on the $node_id column. 
 
### Edge Table  
An Edge table represents a relationship between two entities or Nodes. An Edge may or may not have any user defined attributes associated to it. Users can model many-to-many relationships with the help of Edge tables. For every edge table that is created, we create three implicit columns; `$edge_id`, `$from_id`, `$to_id`. `$edge_id` uniquely identifies an edge in the database. Similar to `$node_id` column, we recommend that users create a unique constraint or index on this column, but if one is not created, we will create a default non-clustered unique index on this column. An Edge in SQL Graph, is always directed. The `$from_id` column holds the $node_id of the Node from where a given edge starts and `$to_id` holds the `$node_id` of the node where that edge ends.  
A single type of edge table, can connect multiple type of Nodes in the schema. For example a 'likes' edge can connect one Person to another Person or it could also connect a Person to an Organization. 
 
Figure 1 shows how Node and Edge tables are stored in the database. In this example we have a Person Node table and a Friends Edge table. A Person Node is connected to another Person Node via the Friends Edge.
 
![person-friends-tables](../../relational-databases/graphs/media/person-friends-tables.png "Person node and friends edge tables")  
Figure 1: Person Node table and Friends Edge table. 
  
### MATCH Clause  
A new MATCH clause is introduced, which supports ASCII-art style syntax, to enable pattern matching and multi-hop navigation through the graph. The MATCH clause takes a pattern as an input, starts from a Node and goes to another Node via an Edge. For example, consider the Node and Edge tables shown in Figure 1, following query will find all the Friends of John.
 
```   
SELECT Person2.Name 
FROM Person Person1, Friends, Person Person2
WHERE MATCH(Person1-(Friends)->Person2)
AND Person1.Name = 'John';
```
 
 Anything that appears inside parenthesis in the pattern is an Edge and the entities at the 2 ends of the arrow are Nodes. The pattern provided inside the MATCH clause can be as long as required for the query. For example, to find Friends of Friends of John, following query can be used. Here we start with John, find all the people that he is connected to via Friends edge, then for each of these people, we find people who are connected to them via Friends edge. 
 
```   
SELECT Person3.Name 
FROM Person Person1, Friends Friends1, Person Person2, 
Friends Friends2, Person Person3
WHERE MATCH(Person1-(Friends1)->Person2-(Friends2)->Person3)
AND Person1.Name = 'John';
```   
 
## Remarks   
 
### How can I ingest unstructured data?  
Since, we are storing data in tables, users must know the schema at the time of creation. Users can always add new types of nodes or edges to their schema. But, if they want to modify an existing node or edge table, they will have to use ALTER TABLE to add/delete attributes. If you expect any unknown attributes in your schema, you could either use sparse columns or create a column to hold JSON strings and use that as a placeholder for unknown attributes.

### Do you maintain an adjacency list for faster lookups?  
No. We are not maintaining an adjacency list on every node; instead we are storing edge data in tables. Being a relational database, storing data in the form of tables was a more natural choice for us. In native directed Graph Databases, with an adjacency list, you can only traverse in one direction. Should you need to traverse in the reverse direction, you would need to maintain an adjacency list at the remote node too. This also increases your storage requirements. Also, with adjacency lists, in a big graph for a large query which spawns across your graph, you are essentially doing a nested loop lookup – for every node, find me all the edges, from there find me all the connected nodes and edges and so on.   
Storing edge data in a separate table allows us to benefit from the query optimizer, which can pick optimal join strategy to use for large queries. Depending on the complexity of query and data statistics, the optimizer can pick a nested loop join, hash join or other join strategies. Each edge table has two implicit columns $from_id and $to_id, which store information about the nodes that it connects. We recommend users to create indexes on these columns ($from_id, $to_id) for faster lookups in the direction of the edge. If your application needs to perform traversals in reverse direction of an edge, you can create an index on ($to_id, $from_id).  
 
### Do I need to be on a special compatibility level to use the SQL language extensions?  
No. The new syntax works with all compatibility levels. You can use this on any compatibility level that you are using for your existing application in SQL Server 2017. So, start connecting! 
 
### How do I find a Node connected to me, arbitrary number of hops away, in my graph?  
The ability to traverse arbitrary number of Nodes and Edges in the graph is called transitive closure. For example, find me all the people connected to me through 3 levels of indirections or find the employee chain for a given employee in an organization. Transitive Closure is not supported in the first release. Customers will need to write a recursive CTE or a stored procedure to work around these type of queries. 
 
### How do I find ANY Node connected to me in my graph?  
The ability to find any type of node connected to a given node in a graph is called polymorphism; SQL graph does not support Polymorphism in the first release. Users will have to write a recursive CTE or a stored procedure to work around these type of queries. 
 
### Are there special graph analytics functions introduced?  
Some graph databases provided dedicated graph analytical functions like 'shortest path' or 'page rank'. SQL Graph does not provide any such functions in this release. 
 
 ## Next steps  

   

