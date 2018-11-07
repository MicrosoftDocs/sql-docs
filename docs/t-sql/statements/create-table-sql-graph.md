---
title: "CREATE TABLE (SQL Graph) | Microsoft Docs"
ms.custom: ""
ms.date: "05/04/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
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
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "graph"
  - "SQL graph"
  - "CREATE TABLE SQL graph"
  - "NODE"
  - "EDGE"
  - "SQL graph, CREATE TABLE statement"
ms.assetid: 
author: "shkale-msft"
ms.author: "shkale"
manager: craigg
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE TABLE (SQL Graph)
[!INCLUDE[tsql-appliesto-ss2017-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2017-xxxx-xxxx-xxx-md.md)]

Creates a new SQL graph table as either a `NODE` or an `EDGE` table. 
  
> [!NOTE]   
>  For standard Transact-SQL statements, see [CREATE TABLE (Transact-SQL)](../../t-sql/statements/create-table-transact-sql.md).
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
CREATE TABLE   
    [ database_name . [ schema_name ] . | schema_name . ] table_name   
    ( { <column_definition> } [ ,...n ] )   
    AS [ NODE | EDGE ]
[ ; ]  
```  
  
  
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
  
## Remarks  
Creating a temporary table as node or edge table is not supported.  

Creating a node or edge table as a temporal table is not supported.

Stretch database is not supported for node or edge table.

Node or edge tables cannot be external tables (no PolyBase support for graph tables). 
  
 
## Examples  
  
### A. Create a `NODE` table
 The following example shows how to create a `NODE` table

```
 CREATE TABLE Person (
        ID INTEGER PRIMARY KEY, 
        name VARCHAR(100), 
        email VARCHAR(100)
 ) AS NODE;
```

### B. Create an `EDGE` table
The following examples show how to create `EDGE` tables

```
 CREATE TABLE friends (
    id integer PRIMARY KEY,
    start_date date
 ) AS EDGE;

```

```
 -- Create a likes edge table, this table does not have any user defined attributes   
 CREATE TABLE likes AS EDGE;

```


## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [INSERT (SQL Graph)](../../t-sql/statements/insert-sql-graph.md)]  
 [Graph processing with SQL Server 2017](../../relational-databases/graphs/sql-graph-overview.md)

