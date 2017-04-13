---
title: "CREATE TABLE (SQL Graph) | Microsoft Docs"
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
caps.latest.revision: 1
author: "shkale-msft"
ms.author: "shkale"
manager: "jhubbard"
---

# CREATE TABLE (SQL Graph)
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]   

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
 *database_name*  
 Is the name of the database in which the table is created. *database_name* must specify the name of an existing database. If not specified, *database_name* defaults to the current database. The login for the current connection must be associated with an existing user ID in the database specified by *database_name*, and that user ID must have CREATE TABLE permissions.  
  
 *schema_name*  
 Is the name of the schema to which the new table belongs.  
  
 *table_name*  
 Is the name of the new table. Table names must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). *table_name* can be a maximum of 128 characters, except for local temporary table names (names prefixed with a single number sign (#)) that cannot exceed 116 characters.  
  
 NODE   
 Creates a node table.

 EDGE  
 Create an edge table.  
  
## Remarks  
 
  
## Permissions  

  
## Examples  
  
### A. Create a ...  
 The following example shows ...  
  

## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   


