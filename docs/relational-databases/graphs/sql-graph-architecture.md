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
The `sys.columns` view will contain additional bit columns`is_from_id`, `is_to_id`, `is_system_generated` that indicate the type of the column in node and edge tables.
 
|Column Name |Data Type |Description |
|--- |---|--- |
|is_from_id |Bit/int (with set of values) |1 = this is the ‘from’ node |
|is_to_id |Bit/int (with set of values) |1 = this is the ‘to’ node |
|is_system_generated |bit |1 = id is system generated |
 
`sys.columns` will also store information about implicit columns created in node or edge tables. Following information can be retrieved from sys.columns. Users will be able to select these columns in a query, but they will not be able to modify them.

|Column Name	|Data Type	|is_computed	|is_hidden	|Comment  |
|---  |---|---|---|---  |
|edge_id_\<hex_string>	|BIGINT	|0	|1	|internal identity column  |
|$edge_id_\<hex_string>	|NVARCHAR	|1	|0	|external edge id column  |
|from_object_id_\<hex_string>	|INT	|0	|1	|internal from node object id  |
|from_node_id_\<hex_string>	|BIGINT	|0	|1	|Internal from node id  |
|$from_id_\<hex_string>	|NVARCHAR	|1	|0	|external from node id  |
|to_object_id_\<hex_string>	|INT	|0	|1	|internal to node object id  |
|to_node_id_\<hex_string>	|BIGINT	|0	|1	|Internal to node id  |
|$to_id_\<hex_string>	|NVARCHAR	|1	|0	|external to node id  |
|node_id_\<hex_string>	|BIGINT	|0	|1	|Internal identity column  |
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
 
## Next Steps
To learn more about the [!INCLUDE[tsql-md](../../includes/tsql-md.md)] extensions introduced for SQL graph please see <SQL Graph database - [!INCLUDE[tsql-md](../../includes/tsql-md.md)] Extensions>
 

