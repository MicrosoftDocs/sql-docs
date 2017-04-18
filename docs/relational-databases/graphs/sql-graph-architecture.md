---
title: "SQL Graph Architecture | Microsoft Docs"
ms.custom: Learn SQL graph architecture. A graph is a collection of node or edge tables. Node tables represent entities and edge tables represent relationships between nodes. 
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
  - "SQL graph, architecture"
ms.assetid: 
caps.latest.revision: 1
author: "shkale-msft"
ms.author: "shkale"
manager: "jhubbard"
---
# SQL Graph Architecture  
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]   


Learn how SQL Graph is architected. Knowing the basics will make it easier to understand other SQL Graph articles.
 
## SQL Graph Database
Users can create one graph per database. A graph is a collection of node and edge tables. Node or edge tables can be created under any schema in the database, but they all belong to one logical graph. A node table is collection of similar type of nodes. For example, a Person node table holds all the Person nodes belonging to a graph. Similarly, an edge table is a collection of similar type of edges. Since nodes and edges are stored in tables, most of the operations supported on regular tables are supported on node or edge tables. 
 
 
![sql-graph-architecture](../../relational-databases/graphs/media/sql-graph-architecture.png "Sql graph database architecture")   

Figure 1: SQL Graph database architecture
 
## Node Table
A node table represents an entity in a graph schema. Every time a node table is created, along with the user defined columns, an implicit `$node_id` column is created, which uniquely identifies a given node in the database. The values in `$node_id` are automatically generated and is a combination of `object_id` of that node table and an internally generated bigint value. However, when the `$node_id` column is selected, a computed value in the form of a JSON string is displayed. 

It is recommended that users create a unique constraint or index on the `$node_id` column at the time of creation of node table, but if one is not created, a default unique, non-clustered index is created. 
 

## Edge Table
An edge table represents a relationship in a graph. Edges are always directed and they go from one node to another. An edge table enables users to model many-to-many relationships in the graph. An edge table may or may not have any user defined attributes in it. Every time an edge table is created, along with the user defined attributes, three implicit columns are created in the edge table:

|Column name    |Description  |
|---   |---  |
|`$edge_id`   |Uniquely identifies a given edge in the database. It is a generated column and the value is a combination of object_id of the edge table and a internally generated bigint value. However, when the `$edge_id` column is selected, a computed value in the form of a JSON string is displayed. |
|`$from_id`   |Stores `$node_id` of the node, the edge starts from  |
|`$to_id`   |Stores `$node_id` of the node, the edge points to  |

The nodes that a given edge can connect is governed by the data inserted in the `$from_id` and `$to_id` columns. In the first release, there are no constraints defined on the edge table, that will restrict it from connecting any two type of nodes. That is, an edge can connect any node to any node in a graph.

Similar to the `$node_id` column, it is recommended that users create a unique index or constraint on the `$edge_id` column at the time of creation of the edge table, but if one is not created, a default unique, non-clustered index is created on this column. It is also recommended, that users create an index on (`$from_id`, `$to_id`) columns, for faster lookups in the direction of the edge. 

Figure 2 shows how node and edge tables are stored in the database. 

![person-friends-tables](../../relational-databases/graphs/media/person-friends-tables.png "Person node and friends edge tables")   

Figure 2: Node and edge table representation



## Metadata
Use these metadata view to see attributes of a node or edge table.
 
### SYS.TABLES
Following new, bit type, columns will be added to SYS.TABLES. If is_node is set to 1, that will indicate that the table is a node table and if is_edge is set to 1, that will indicate that the table is an edge table.
 
|Column Name |Data Type |Description |
|--- |---|--- |
|is_node |bit |1 = this is a node table |
|is_edge |bit |1 = this is an edge table |
 
### SYS.COLUMNS
The `sys.columns` view will contain additional integer columns`graph_type`, `graph_type_desc`, that indicate the type of the column in node and edge tables.
 
|Column Name |Data Type |Description |
|--- |---|--- |
|graph_type |int |1 = graph_id column |
|graph_type_desc |int  |internal column with a set of values |
 
`sys.columns` will also store information about implicit columns created in node or edge tables. Following information can be retrieved from sys.columns. Users will be able to select these columns in a query, but they will not be able to modify them.

|Column Name	|Data Type	|is_computed	|is_hidden	|Comment  |
|---  |---|---|---|---  |
|graph_id_\<hex_string>	|BIGINT	|0	|1	|internal identity column  |
|$edge_id_\<hex_string>	|NVARCHAR	|1	|0	|external edge id column  |
|from_obj_id_\<hex_string>	|INT	|0	|1	|internal from node object id  |
|from_id_\<hex_string>	|BIGINT	|0	|1	|Internal from node id  |
|$from_id_\<hex_string>	|NVARCHAR	|1	|0	|external from node id  |
|to_obj_id_\<hex_string>	|INT	|0	|1	|internal to node object id  |
|to_id_\<hex_string>	|BIGINT	|0	|1	|Internal to node id  |
|$to_id_\<hex_string>	|NVARCHAR	|1	|0	|external to node id  |
|$node_id_\<hex_string>	|NVARCHAR	|1	|0	|External node id column  |
 
### System Functions
Following built-in functions are added. These will help users extract part of information from the generated columns. Note that, these methods will not validate the input from the user, that is, if the user specifies an invalid sys.node_id the method will extract the appropriate part and return to the user. For example, OBJECT_ID_FROM_NODE_ID will take `$node_id` as input and will return the object_id of the table, this node belongs to. 
 
|Built-in	|Description  |
|---  |---  |
|OBJECT_ID_FROM_NODE_ID	|Extract object_id from node_id  |
|IDENTITY_FROM_NODE_ID	|Extract identity from node_id  |
|NODE_ID_FROM_PARTS	|Construct node_id from object_id and identity  |
|OBJECT_ID_FROM_EDGE_ID	|Extract object_id from edge_id  |
|IDENTITY_FROM_EDGE_ID	|Extract identity from edge_id  |
|EDGE_ID_FROM_PARTS	|Construct edge_id from object_id and identity  |



## Transact-SQL reference 
Learn the [!INCLUDE[tsql-md](../../includes/tsql-md.md)] extensions introduced in SQL Server, that enable creating and querying graph objects. The query language extensions help query and traverse the graph using ASCII art syntax.
 
### Data Definition Language (DDL) statements
|Task	|Related Topic  |Notes
|---  |---  |---  |
|CREATE TABLE |[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-sql-graph.md)|`CREATE TABLE ` is now extended to support creating a table AS NODE or AS EDGE. Note that an edge table may or may not have any user defined attributes  |
|ALTER TABLE	|[ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)|Node and edge tables can be altered the same way a relational table is, using the `ALTER TABLE`. Users can add or modify user defined columns, indexes or constraints. However, altering internal graph columns, like `$node_id` or `$edge_id`, will result in an error.  |
|CREATE INDEX	|[CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)  |Users can create indexes on node and edge tables, including clustered and non-clustered columnstore indexes  |
|DROP TABLE |[DROP TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-table-transact-sql.md)  |Node and edge tables can be dropped the same way a relational table is, using the `DROP TABLE`. However, in this release, cascade deletion of edges, upon deletion of a node or node table is not supported. It is recommended that if a node table is dropped, users drop any edges connected to the nodes in that node table manually to maintain the integrity of the graph.  |


### Data Manipulation Language (DML) statements
|Task	|Related Topic  |Notes
|---  |---  |---  |
|INSERT |[INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-sql-graph.md)|Inserting into a node table is no different than inserting into a relational table. The values for `$node_id` and `$edge_id` columns are automatically generated. Trying to insert a value in `$node_id` or `$edge_id` column will result in an error. Users must provide values for `$from_id` and `$to_id` columns while inserting into an edge table. `$from_id` and `$to_id` are the `$node_ids` of the nodes a given edge connects.  |
|DELETE	| [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)|Data from node or edge tables can be deleted in same way as it is deleted from relational tables. However, in the first release, cascade deletion of connecting edges, upon deletion of a node is not supported. It is recommended that whenever a node is deleted, all the connecting edges to that node are also deleted, to maintain the integrity of the graph.  |
|UPDATE	|[UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)  |Values in user defined columns can be updated using the UPDATE statement. Updating internal graph columns, `$node_id`, `$edge_id`, `$from_id` and `$to_id` is not allowed.  |
|MERGE |[MERGE &#40;Transact-SQL&#41;](../../t-sql/statements/merge-transact-sql.md)  |`MERGE` statement is not supported on node or edge table.  |


### Query Statements
|Task	|Related Topic  |Notes
|---  |---  |---  |
|SELECT |[SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)|Nodes and edges are stored as tables internally, hence all operations supported on a table in SQL Server or Azure SQL Database are supported on node and edge tables  |
|MATCH	| [MATCH &#40;Transact-SQL&#41;](../../t-sql/statements/match-sql-graph.md)|MATCH clause is introduced to support pattern matching and traversal through the graph. In this release, MATCH clause can be used only on node or edge tables.  |



## Limitations and known issues  
There are certain limitations on node and edge tables in this release:
* Local or global temporary tables cannot be node or edge tables.
* Node and edge tables cannot be created as system-versioned temporal tables.   
* Node and edge tables cannot be memory optimized tables.  
* Users cannot update the $from_id and $to_id columns of an edge using UPDATE statement. To update the nodes that an edge connects, users will have to insert the new edge pointing to new nodes and delete the previous one.


## Next Steps
To get started with the new syntax, see [SQL Graph Database - Sample](./sql-graph-sample.md)
 

