---
title: "Graph processing with SQL Server and Azure SQL Database | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: 
ms.topic: "language-reference"
helpviewer_keywords: 
  - "SQL graph"
  - "SQL graph, overview"
ms.assetid: 
author: shkale-msft
ms.author: shkale
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Graph processing with SQL Server and Azure SQL Database
[!INCLUDE[tsql-appliesto-ss2017-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2017-asdb-xxxx-xxx-md.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] offers graph database capabilities to model many-to-many relationships. The graph relationships are integrated into [!INCLUDE[tsql-md](../../includes/tsql-md.md)] and receive the benefits of using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as the foundational database management system.


## What is a graph database?  
A graph database is a collection of nodes (or vertices) and edges (or relationships). A node represents an entity (for example, a person or an organization) and an edge represents a relationship between the two nodes that it connects (for example, likes or friends). Both nodes and edges may have properties associated with them. Here are some features that make a graph database unique:  
-	Edges or relationships are first class entities in a Graph Database and can have attributes or properties associated with them. 
-	A single edge can flexibly connect multiple nodes in a Graph Database.
-	You can express pattern matching and multi-hop navigation queries easily.
-	You can express transitive closure and polymorphic queries easily.

## When to use a graph database

There is nothing a graph database can achieve, which cannot be achieved using a relational database. However, a graph database can make it easier to express certain kind of queries. Also, with specific optimizations, certain queries may perform better. Your decision to choose one over the other can be based on following factors:  
-	Your application has hierarchical data. The HierarchyID datatype can be used to implement hierarchies, but it has some limitations. For example, it does not allow you to store multiple parents for a node.
-	Your application has complex many-to-many relationships; as application evolves, new relationships are added.
-	You need to analyze interconnected data and relationships.

## Graph features introduced in [!INCLUDE[sssqlv14](../../includes/sssqlv14-md.md)] 
We are starting to add graph extensions to SQL Server, to make storing and querying graph data easier. Following features are introduced in the first release. 


### Create graph objects
[!INCLUDE[tsql-md](../../includes/tsql-md.md)] extensions will allow users to create node or edge tables. Both nodes and edges can have properties associated to them. Since, nodes and edges are stored as tables, all the operations that are supported on relational tables are supported on node or edge table. Here is an example:  

```   
CREATE TABLE Person (ID INTEGER PRIMARY KEY, Name VARCHAR(100), Age INT) AS NODE;
CREATE TABLE friends (StartDate date) AS EDGE;
```   

![person-friends-tables](../../relational-databases/graphs/media/person-friends-tables.png "Person node and friends edge tables")  
Nodes and Edges are stored as tables  

### Query language extensions  
New `MATCH` clause is introduced to support pattern matching and multi-hop navigation through the graph. The `MATCH` function uses ASCII-art style syntax for pattern matching. For example:  

```   
-- Find friends of John
SELECT Person2.Name 
FROM Person Person1, Friends, Person Person2
WHERE MATCH(Person1-(Friends)->Person2)
AND Person1.Name = 'John';
```   
 
### Fully integrated in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Engine 
Graph extensions are fully integrated in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] engine. Use the same storage engine, metadata, query processor, etc. to store and query graph data. Query across graph and relational data in a single query. Combining graph capabilities with other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] technologies like columnstore, HA, R services, etc. SQL graph database also supports all the security and compliance features available with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
 
### Tooling and ecosystem

Benefit from existing tools and ecosystem that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] offers. Tools like backup and restore, import and export, BCP just work out of the box. Other tools or services like SSIS, SSRS, or Power BI will work with graph tables, just the way they work with relational tables.

## Edge Constraints
An edge constraint is defined on a graph edge table and is a pair of node table(s) that a given edge type can connect. This gives users a better control over their graph schema. With the help of edge constraints users can restrict the type of nodes a given edge is allowed to connect. 

To learn more about how to create and use edge constraints refer to [Edge Constraints](../../relational-databases/tables/graph-edge-constraints.md)

## Merge DML 
The [MERGE](../../t-sql/statements/merge-transact-sql.md) statement performs insert, update, or delete operations on a target table based on the results of a join with a source table. For example, you can synchronize two tables by inserting, updating, or deleting rows in a target table based on differences between the target table and the source table. Using MATCH predicates in a MERGE statement is now supported on Azure SQL Database and SQL Server vNext. That is, it is now possible to merge your current graph data (node or edge tables) with new data using the MATCH predicates to specify graph relationships in a single statement, instead of separate INSERT/UPDATE/DELETE statements.

To learn more about how match can be used in merge DML refer to [MERGE Statement](../../t-sql/statements/merge-transact-sql.md)

 ## Next steps  
Read the [SQL Graph Database - Architecture](./sql-graph-architecture.md)
   

