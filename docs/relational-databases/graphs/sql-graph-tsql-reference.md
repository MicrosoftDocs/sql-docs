---
title: SQL Graph Database Transact-SQL reference | Microsoft Docs
description: Links to reference content for the Transact-SQL topics used by SQL Graph Database.
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
  - "SQL graph, tsql reference"
ms.assetid: 
caps.latest.revision: 1
author: "shkale-msft"
ms.author: "shkale"
manager: "jhubbard"
---
# SQL Graph Database Transact-SQL reference 
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]   


Learn the [!INCLUDE[tsql-md](../../includes/tsql-md.md)] extensions introduced in SQL Server, that enable creating and querying graph objects. The query language extensions help query and traverse the graph using ASCII art syntax.
 
## Data Definition Language (DDL) statements
|Task	|Related Topic  |Notes
|---  |---  |---  |
|CREATE TABLE |[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-sql-graph.md)|`CREATE TABLE ` is now extended to support creating a table AS NODE or AS EDGE. Note that an edge table may or may not have any user defined attributes  |
|ALTER TABLE	|[ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)|Node and edge tables can be altered the same way a relational table is, using the `ALTER TABLE`. Users can add or modify user defined columns, indexes or constraints. However, altering internal graph columns, like `$node_id` or `$edge_id`, will result in an error.  |
|CREATE INDEX	|[CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)  |Users can create indexes on node and edge tables, including clustered and non-clustered columnstore indexes  |
|DROP TABLE |[DROP TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-table-transact-sql.md)  |Node and edge tables can be dropped the same way a relational table is, using the `DROP TABLE`. However, in this release, cascade deletion of edges, upon deletion of a node or node table is not supported. It is recommended that if a node table is dropped, users drop any edges connected to the nodes in that node table manually to maintain the integrity of the graph.  |



## Data Manipulation Language (DML) statements
|Task	|Related Topic  |Notes
|---  |---  |---  |
|INSERT |[INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-sql-graph.md)|Inserting into a node table is no different than inserting into a relational table. The values for `$node_id` and `$edge_id` columns are automatically generated. Trying to insert a value in `$node_id` or `$edge_id` column will result in an error. Users must provide values for `$from_id` and `$to_id` columns while inserting into an edge table. `$from_id` and `$to_id` are the `$node_ids` of the nodes a given edge connects.  |
|DELETE	| [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)|Data from node or edge tables can be deleted in same way as it is deleted from relational tables. However, in the first release, cascade deletion of connecting edges, upon deletion of a node is not supported. It is recommended that whenever a node is deleted, all the connecting edges to that node are also deleted, to maintain the integrity of the graph.  |
|UPDATE	|[UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)  |Values in user defined columns can be updated using the UPDATE statement. Updating internal graph columns, `$node_id`, `$edge_id`, `$from_id` and `$to_id` is not allowed.  |
|MERGE |[MERGE &#40;Transact-SQL&#41;](../../t-sql/statements/merge-transact-sql.md)  |`MERGE` statement is not supported on node or edge table.  |



## Query Statements
|Task	|Related Topic  |Notes
|---  |---  |---  |
|SELECT |[SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)|Nodes and edges are stored as tables internally, hence all operations supported on a table in SQL Server or Azure SQL Database are supported on node and edge tables  |
|MATCH	| [MATCH &#40;Transact-SQL&#41;](../../t-sql/statements/match-sql-graph.md)|MATCH clause is introduced to support pattern matching and traversal through the graph. In this release, MATCH clause can be used only on node or edge tables.  |



## Next Steps
To get started with SQL graph, please refer to SQL graph sample [SQL Graph Database - sample] (github link to a sample)