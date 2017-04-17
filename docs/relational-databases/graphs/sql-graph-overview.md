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
# Graph Processing with SQL Server 2017  
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]   

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] offers graph database capabilities to model many-to-many relationships. The graph relationships are integrated into [!INCLUDE[tsql-md](../../includes/tsql-md.md)] and receive the benefits of using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as the foundational database management system.


## What is a graph database?  
A Graph Database is a collection of nodes (or vertices) and edges (or relationships). A node represents an entity (for example, a person or an organization) and an edge represents a relationship between the two nodes that it connects (for example, likes or friends). Both nodes and edges may have properties associated to them. Here are some features that make a graph database unique:  
-	Unlike relational databases, edges or relationships in Graph Database can have attributes or properties associated to them. 
-	A single edge can flexibly connect multiple nodes in a Graph Database.
-	You can express pattern matching and multi-hop navigation queries easily
-	They allow you to express transitive closure and polymorphic queries easily 
-	Some Graph Databases also support specialized analytical functions like Page Rank or Shortest Path. 

## When to use a Graph Database

There is nothing a graph database can achieve, which cannot be achieved using a relational database. However, a graph database can make it easier to express certain kind of queries. Also, with specific optimizations, certain queries may perform better. Your decision to choose one over the other can be based on following factors:  
-	Hierarchical or interconnected data; there are entities with multiple parents in your schema
-	Complex many-to-many relationships; as application evolves, new relationships are added
-	Analyze interconnected data and relationships

## Graph features introduced in [!INCLUDE[sssqlv14](../../includes/sssqlv14-md.md)] 

*Need help with phrasing a sentence here that highlights that we are just starting to build graph processing capabilities in SQL Server. We might not have everything that a real graph database supports in this release.*

### Create graph objects
[!INCLUDE[tsql-md](../../includes/tsql-md.md)] extensions will allow users to create node or edge tables. Both nodes and edges can have properties associated to them. Since, nodes and edges are stored as tables, all the operations that are supported on relational tables are supported on node or edge table. Here is an example:  

```   
CREATE TABLE Person (ID INTEGER PRIMARY KEY, name VARCHAR(100)) AS NODE;
CREATE TABLE friends (StartDate date) AS EDGE;
```   

![person-friends-tables](../../relational-databases/graphs/media/person-friends-tables.png "Person node and friends edge tables")  
Nodes and Edges are stored as tables  

### Query language extensions  
New `MATCH` clause is introduced to support pattern matching and multi-hop navigation through the graph. The `MATCH` function uses ASCII-art style syntax for pattern matching. For example:  

```   
SELECT Person2.Name 
FROM Person Person1, Friends, Person Person2
WHERE MATCH(Person1-(Friends)->Person2)
AND Person1.Name = 'John';
```   
 
### Fully integrated in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]   
Graph extensions are fully integrated in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] engine. We use the same storage engine, metadata, query processor, etc. to store and query graph data. This enables users to query across their graph and relational data in a single query. Users can also benefit from combining graph capabilities with other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] technologies like columnstore, HA, R services, etc. SQL graph database also supports all the security and compliance features available with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
 
### Tooling and ecosystem  
Users benefit from existing tools and ecosystem that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] offers. Tools like backup and restore, import and export, BCP just work out of the box. Other tools or services like SSIS, SSRS or PowerBI will work with graph tables, just the way they work with relational tables.
 
 ## Next steps  
Read the [SQL Graph Database - Architecture](./sql-graph-architecture.md)
   

