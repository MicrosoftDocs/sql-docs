---
title: "INSERT (SQL Graph) | Microsoft Docs"
description: INSERT syntax for SQL Graph node or edge tables. 
ms.date: "05/12/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.custom: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "INSERT statement [SQL Server], SQL graph"
  - "SQL graph, INSERT statement"
ms.assetid: 
author: "shkale-msft"
ms.author: "shkale"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# INSERT (SQL Graph)
[!INCLUDE[tsql-appliesto-ss2017-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2017-asdb-xxxx-xxx-md.md)]

Adds one or more rows to a `node` or `edge` table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. 

> [!NOTE]   
>  For standard Transact-SQL statements, see [INSERT TABLE (Transact-SQL)](../../t-sql/statements/insert-transact-sql.md).
  
![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## INSERT Into Node Table Syntax 
The syntax for inserting into a Node table is the same as for a regular table. 

```sql
[ WITH <common_table_expression> [ ,...n ] ]  
INSERT   
{  
        [ TOP ( expression ) [ PERCENT ] ]   
        [ INTO ]   
        { <object> | rowset_function_limited   
          [ WITH ( <Table_Hint_Limited> [ ...n ] ) ]  
        }  
    {  
        [ (column_list) ] | [(<edge_table_column_list>)]  
        [ <OUTPUT Clause> ]  
        { VALUES ( { DEFAULT | NULL | expression } [ ,...n ] ) [ ,...n     ]   
        | derived_table   
        | execute_statement  
        | <dml_table_source>  
        | DEFAULT VALUES   
        }  
    }  
}  
[;]  
  
<object> ::=  
{   
    [ server_name . database_name . schema_name .   
      | database_name .[ schema_name ] .   
      | schema_name .   
    ]  
    node_table_name  | edge_table_name
}  
  
<dml_table_source> ::=  
    SELECT <select_list>  
    FROM ( <dml_statement_with_output_clause> )   
      [AS] table_alias [ ( column_alias [ ,...n ] ) ]  
    [ WHERE <on_or_where_search_condition> ]  
        [ OPTION ( <query_hint> [ ,...n ] ) ]  

<on_or_where_search_condition> ::=
    {  <search_condition_with_match> | <search_condition> }

<search_condition_with_match> ::=
    { <graph_predicate> | [ NOT ] <predicate> | ( <search_condition> ) }
    [ AND { <graph_predicate> | [ NOT ] <predicate> | ( <search_condition> ) } ]
    [ ,...n ]

<search_condition> ::=
    { [ NOT ] <predicate> | ( <search_condition> ) }
    [ { AND | OR } [ NOT ] { <predicate> | ( <search_condition> ) } ]
    [ ,...n ]

<graph_predicate> ::=
    MATCH( <graph_search_pattern> [ AND <graph_search_pattern> ] [ , ...n] )

<graph_search_pattern>::=
    <node_alias> { { <-( <edge_alias> )- | -( <edge_alias> )-> } <node_alias> }

<edge_table_column_list> ::=
    ($from_id, $to_id, [column_list])

```  
  
 
## Arguments  
This document describes arguments related to SQL graph. For a full list and description of supported arguments in INSERT statement, see [INSERT TABLE (Transact-SQL)](../../t-sql/statements/insert-transact-sql.md)

INTO  
Is an optional keyword that can be used between `INSERT` and the target table.  
  
*search_condition_with_match*   
`MATCH` clause can be used in a subquery while inserting into a node or edge table. For `MATCH` statement syntax, see [GRAPH MATCH (Transact-SQL)](../../t-sql/queries/match-sql-graph.md)

*graph_search_pattern*   
Search pattern provided to `MATCH` clause as part of the graph predicate.

*edge_table_column_list*   
Users must provide values for `$from_id` and `$to_id` while inserting into an edge. An error will be returned if a value isn't provided or NULLs are inserted into these columns. 
  

## Remarks  
Inserting into a node is same as inserting into any relational table. Values for the $node_id column are automatically generated.

While inserting into an edge table, users must provide values for `$from_id` and `$to_id` columns.   

BULK insert for node table is the same as for a relational table.

Before bulk inserting into an edge table, the node tables must be imported. Values for `$from_id` and `$to_id` can then be extracted from the `$node_id` column of the node table and inserted as edges. 

  
### Permissions  
INSERT permission is required on the target table.  
  
INSERT permissions default to members of the **sysadmin** fixed server role, the **db_owner** and **db_datawriter** fixed database roles, and the table owner. Members of the **sysadmin**, **db_owner**, and the **db_securityadmin** roles, and the table owner can transfer permissions to other users.  
  
To execute INSERT with the OPENROWSET function BULK option, you must be a member of the **sysadmin** fixed server role or of the **bulkadmin** fixed server role.  
  

## Examples  
  
#### A.  Insert into node table  
The following example creates a Person node table and inserts two rows into that table.

```sql
-- Create person node table
CREATE TABLE dbo.Person (ID integer PRIMARY KEY, name varchar(50)) AS NODE;
 
-- Insert records for Alice and John
INSERT INTO dbo.Person VALUES (1, 'Alice');
INSERT INTO dbo.Person VALUES (2,'John');
```
  
#### B.  Insert into edge table  
The following example creates a friend edge table and inserts an edge into the table.

```sql
-- Create friend edge table
CREATE TABLE dbo.friend (start_date DATE) AS EDGE;

-- Create a friend edge, that connect Alice and John
INSERT INTO dbo.friend VALUES ((SELECT $node_id FROM dbo.Person WHERE name = 'Alice'),
        (SELECT $node_id FROM dbo.Person WHERE name = 'John'), '9/15/2011');
```

  
## See Also  
[INSERT TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
[Graph processing with SQL Server 2017](../../relational-databases/graphs/sql-graph-overview.md)  


