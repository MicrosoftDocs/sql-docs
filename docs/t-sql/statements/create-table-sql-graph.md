---
title: "CREATE TABLE (SQL Graph)"
description: CREATE TABLE (SQL Graph)
author: "MikeRayMSFT"
ms.author: "mikeray"
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SQL_GRAPH_TSQL"
  - "TABLE"
  - "CREATE_TABLE_TSQL"
  - "NODE"
  - "NODE_TSQL"
  - "AS_NODE"
  - "AS_NODE_TSQL"
  - "EDGE"
  - "EDGE_TSQL"
  - "AS_EDGE"
  - "AS_EDGE_TSQL"
helpviewer_keywords:
  - "graph"
  - "SQL graph"
  - "CREATE TABLE SQL graph"
  - "NODE"
  - "EDGE"
  - "SQL graph, CREATE TABLE statement"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE TABLE (SQL Graph)
[!INCLUDE[SQL Server 2017](../../includes/applies-to-version/sqlserver2017.md)]

Creates a new SQL graph table as either a `NODE` or an `EDGE` table. 
  
> [!NOTE]   
>  For standard Transact-SQL statements, see [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md).
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
CREATE TABLE   
    { database_name.schema_name.table_name | schema_name.table_name | table_name }
    ( { <column_definition> } 
       | <computed_column_definition>
       | <column_set_definition>
       | [ <table_constraint> ] [ ,... n ]
       | [ <table_index> ] }
          [ ,...n ]
    )   
    AS [ NODE | EDGE ]
    [ ON { partition_scheme_name ( partition_column_name )
           | filegroup
           | "default" } ]
[ ; ] 

< table_constraint > ::=
[ CONSTRAINT constraint_name ]
{
    { PRIMARY KEY | UNIQUE }
        [ CLUSTERED | NONCLUSTERED ]
        (column [ ASC | DESC ] [ ,...n ] )
        [
            WITH FILLFACTOR = fillfactor
           |WITH ( <index_option> [ , ...n ] )
        ]
        [ ON { partition_scheme_name (partition_column_name)
            | filegroup | "default" } ]
    | FOREIGN KEY
        ( column [ ,...n ] )
        REFERENCES referenced_table_name [ ( ref_column [ ,...n ] ) ]
        [ ON DELETE { NO ACTION | CASCADE | SET NULL | SET DEFAULT } ]
        [ ON UPDATE { NO ACTION | CASCADE | SET NULL | SET DEFAULT } ]
        [ NOT FOR REPLICATION ]
    | CONNECTION
        ( { node_table TO node_table } 
          [ , {node_table TO node_table }]
          [ , ...n ]
        )
        [ ON DELETE { NO ACTION | CASCADE } ]
    | CHECK [ NOT FOR REPLICATION ] ( logical_expression )
```  
  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
This document lists only arguments pertaining to SQL graph. For a full list and description of supported arguments, see [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md)

 *database_name*    
 Is the name of the database in which the table is created. *database_name* must specify the name of an existing database. If not specified, *database_name* defaults to the current database. The login for the current connection must be associated with an existing user ID in the database specified by *database_name*, and that user ID must have CREATE TABLE permissions.  
  
 *schema_name*    
 Is the name of the schema to which the new table belongs.  
  
 *table_name*      
 Is the name of the node or edge table. Table names must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). *table_name* can be a maximum of 128 characters, except for local temporary table names (names prefixed with a single number sign (#)) that cannot exceed 116 characters.  
  
 NODE   
 Creates a node table.

 EDGE  
 Creates an edge table.  
 
 *table_constraint*   
 Specifies the properties of a PRIMARY KEY, UNIQUE, FOREIGN KEY, CONNECTION constraint, a CHECK constraint, or a DEFAULT definition added to a table.
 
 > [!NOTE]   
 > CONNECTION constraint applies only to an edge table type.
 
 ON { partition_scheme | filegroup | "default" }    
 Specifies the partition scheme or filegroup on which the table is stored. If partition_scheme is specified, the table is to be a   partitioned table whose partitions are stored on a set of one or more filegroups specified in partition_scheme. If filegroup is specified, the table is stored in the named filegroup. The filegroup must exist within the database. If "default" is specified, or if ON is not specified at all, the table is stored on the default filegroup. The storage mechanism of a table as specified in CREATE TABLE cannot be subsequently altered.

 ON {partition_scheme | filegroup | "default"}    
 Can also be specified in a PRIMARY KEY or UNIQUE constraint. These constraints create  indexes. If filegroup is specified, the index is stored in the named filegroup. If "default" is specified, or if ON is not specified at all, the index is stored in the same filegroup as the table. If the PRIMARY KEY or UNIQUE constraint creates a clustered index, the data pages for the table are stored in the same filegroup as the index. If CLUSTERED is specified or the constraint otherwise creates a clustered index, and a partition_scheme is specified that differs from the partition_scheme or filegroup of the table definition, or vice-versa, only the constraint definition will be honored, and the other will be ignored.
  
## Remarks

Creating a temporary table as node or edge table is not supported.  

Creating a node or edge table as a temporal table is not supported.

Stretch database is not supported for node or edge table.

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

Node or edge tables cannot be external tables (no PolyBase support for graph tables). 

A non-partitioned graph node/edge table cannot be altered into a partitioned graph node/edge table. 
  
 
## Examples  
  
### A. Create a `NODE` table
 The following example shows how to create a `NODE` table

```sql
 CREATE TABLE Person (
        ID INTEGER PRIMARY KEY, 
        name VARCHAR(100), 
        email VARCHAR(100)
 ) AS NODE;
```

### B. Create an `EDGE` table
The following examples show how to create `EDGE` tables

```sql
 CREATE TABLE friends (
    id INTEGER PRIMARY KEY,
    start_date DATe
 ) AS EDGE;
```

```sql
 -- Create a likes edge table, this table does not have any user defined attributes   
 CREATE TABLE likes AS EDGE;
```

The next example models a rule that **only** people can be friends with other people, which means this edge does not allow reference to any node other than Person.

```
/* Create friend edge table with CONSTRAINT, restricts for nodes and it direction */
CREATE TABLE dbo.FriendOf(
  CONSTRAINT cnt_Person_FriendOf_Person
    CONNECTION (dbo.Person TO dbo.Person) 
)AS EDGE;
```


## See Also 
 [ALTER TABLE table_constraint](../../t-sql/statements/alter-table-table-constraint-transact-sql.md)   
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [INSERT (SQL Graph)](../../t-sql/statements/insert-sql-graph.md)]  
 [Graph processing with SQL Server 2017](../../relational-databases/graphs/sql-graph-overview.md)

