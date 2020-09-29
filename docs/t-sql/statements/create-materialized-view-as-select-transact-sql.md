---
description: "CREATE MATERIALIZED VIEW AS SELECT (Transact-SQL)"
title: "CREATE MATERIALIZED VIEW AS SELECT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2020"
ms.prod: sql
ms.prod_service: "sql-data-warehouse"
ms.reviewer: "jrasnick"
ms.technology: data-warehouse
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE VIEW"
  - "VIEW_TSQL"
  - "VIEW"
  - "CREATE_VIEW_TSQL"
  - "SCHEMABINDING_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "table creation [SQL Server], CREATE VIEW"
  - "views [SQL Server], creating"
  - "CREATE VIEW statement"
  - "updatable partitioned views"
  - "tables [SQL Server], virtual"
  - "updatable views"
  - "modifying partitioned views"
  - "virtual tables [SQL Server]"
  - "number of columns per view"
  - "partitioned views [SQL Server], creating"
  - "WITH ENCRYPTION clause"
  - "WITH CHECK OPTION clause"
  - "partitioned views [SQL Server], modifying"
  - "partitioned views [SQL Server], replication"
  - "distributed partitioned views [SQL Server]"
  - "views [SQL Server], indexed views"
  - "maximum number of columns per view"
ms.assetid: aecc2f73-2ab5-4db9-b1e6-2f9e3c601fb9
author: XiaoyuMSFT  
ms.author: xiaoyul
monikerRange: "=azure-sqldw-latest||=sqlallproducts-allversions"
---
# CREATE MATERIALIZED VIEW AS SELECT (Transact-SQL)  

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

This article explains the CREATE MATERIALIZED VIEW AS SELECT T-SQL statement in [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)] for developing solutions. The article also provides code examples.

A Materialized View persists the data returned from the view definition query and automatically gets updated as data changes in the underlying tables.   It improves the performance of complex queries (typically queries with joins and aggregations) while offering simple maintenance operations.   With its execution plan automatching capability, a materialized view does not have to be referenced in the query for the optimizer to consider the view for substitution.  This capability allows data engineers to implement materialized views as a mechanism for improving query response time, without having to change queries.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
CREATE MATERIALIZED VIEW [ schema_name. ] materialized_view_name
    WITH (  
      <distribution_option>
    )
    AS <select_statement>
[;]

<distribution_option> ::=
    {  
        DISTRIBUTION = HASH ( distribution_column_name )  
      | DISTRIBUTION = ROUND_ROBIN  
    }

<select_statement> ::=
    SELECT select_criteria
```  
  
## Arguments

*schema_name*     
 Is the name of the schema to which the view belongs.  
  
*materialized_view_name*   
Is the name of the view. View names must follow the rules for identifiers. Specifying the view owner name is optional.  

*distribution option*     
Only HASH and ROUND_ROBIN distributions are supported.

*select_statement*   
The SELECT list in the materialized view definition needs to meet at least one of these two criteria:
- The SELECT list contains an aggregate function.
- GROUP BY is used in the Materialized view definition and all columns in GROUP BY are included in the SELECT list.  Up to 32 columns can be used in the GROUP BY clause.

Aggregate functions are required in the SELECT list of the materialized view definition.  Supported aggregations include MAX, MIN, AVG, COUNT, COUNT_BIG, SUM, VAR, STDEV.

When MIN/MAX aggregates are used in the SELECT list of materialized view definition, following requirements apply:
 
- FOR_APPEND is required.  For example:
  ```sql 
  CREATE MATERIALIZED VIEW mv_test2  
  WITH (distribution = hash(i_category_id), FOR_APPEND)  
  AS
  SELECT MAX(i.i_rec_start_date) as max_i_rec_start_date, MIN(i.i_rec_end_date) as min_i_rec_end_date, i.i_item_sk, i.i_item_id, i.i_category_id
  FROM syntheticworkload.item i  
  GROUP BY i.i_item_sk, i.i_item_id, i.i_category_id
  ```

- The materialized view will be disabled when an UPDATE or DELETE occurs in the referenced base tables.  This restriction doesn't  apply to INSERTs.  To re-enable the materialized view, run ALTER MATERIALIZED VIEW with REBUILD.
  
## Remarks

A materialized view in Azure data warehouse is similar to an indexed view in SQL Server.  It shares almost the same restrictions as indexed view (see [Create Indexed Views](/sql/relational-databases/views/create-indexed-views) for details) except that a materialized view supports aggregate functions.   

Only CLUSTERED COLUMNSTORE INDEX is supported by materialized view. 

A materialized view cannot reference other views.  

A materialized view can't be created on a table with dynamic data masking (DDM), even if the DDM column is not part of the materialized view.  If a table column is part of an active materialized view or a disabled materialized view, DDM can't be added to this column.  

A materialized view can't be created on a table with row level security enabled.

Materialized Views can be created on partitioned tables.  Partition SPLIT/MERGE are supported on materialized views base tables, partition SWITCH isn't supported.  
 
ALTER TABLE SWITCH is not supported on tables that are referenced in materialized views. Disable or drop the materialized views before using ALTER TABLE SWITCH. In the following scenarios, the materialized view creation requires new columns to be added to the materialized view:

|Scenario|New columns to add to materialized view|Comment|  
|-----------------|---------------|-----------------|
|COUNT_BIG() is missing in the SELECT list of a materialized view definition| COUNT_BIG (*) |Automatically added by materialized view creation.  No user action is required.|
|SUM(a) is specified by users in the SELECT list of a materialized view definition AND 'a' is a nullable expression |COUNT_BIG (a) |Users need to add the expression 'a' manually in the materialized view definition.|
|AVG(a) is specified by users in the SELECT list of a materialized view definition where 'a' is an expression.|SUM(a), COUNT_BIG(a)|Automatically added by materialized view creation.  No user action is required.|
|STDEV(a) is specified by users in the SELECT list of a materialized view definition where 'a' is an expression.|SUM(a), COUNT_BIG(a), SUM(square(a))|Automatically added by materialized view creation.  No user action is required. |
| | | |

Once created, materialized views are visible within SQL Server Management Studio under the views folder of the [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)] instance.

Users can run [SP_SPACEUSED](/sql/relational-databases/system-stored-procedures/sp-spaceused-transact-sql?view=azure-sqldw-latest) and [DBCC PDW_SHOWSPACEUSED](/sql/t-sql/database-console-commands/dbcc-pdw-showspaceused-transact-sql?view=azure-sqldw-latest) to determine the space being consumed by a materialized view.  

A materialized view can be dropped via DROP VIEW.  You can use ALTER MATERIALIZED VIEW to disable or rebuild a materialized view.   

EXPLAIN plan and the graphical Estimated Execution Plan in SQL Server Management Studio can show whether a materialized view is considered by the query optimizer for query execution. and the graphical Estimated Execution Plan in SQL Server Management Studio can show whether a materialized view is considered by the query optimizer for query execution.

To find out if a SQL statement can benefit from a new materialized view, run the `EXPLAIN` command with `WITH_RECOMMENDATIONS`.  For details, see [EXPLAIN (Transact-SQL)](/sql/t-sql/queries/explain-transact-sql?view=azure-sqldw-latest).

## Permissions

Requires 1) REFERENCES and CREATE VIEW permission OR 2) CONTROL permission on the schema in which the view is being created. 

  
## See also

[Performance tuning with Materialized View](/azure/sql-data-warehouse/performance-tuning-materialized-views)   
[ALTER MATERIALIZED VIEW &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-materialized-view-transact-sql?view=azure-sqldw-latest)      
[DROP VIEW](/sql/t-sql/statements/drop-view-transact-sql?view=azure-sqldw-latest)  
[EXPLAIN &#40;Transact-SQL&#41;](/sql/t-sql/queries/explain-transact-sql?view=azure-sqldw-latest)   
[sys.pdw_materialized_view_column_distribution_properties &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-pdw-materialized-view-column-distribution-properties-transact-sql?view=azure-sqldw-latest)   
[sys.pdw_materialized_view_distribution_properties &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-pdw-materialized-view-distribution-properties-transact-sql?view=azure-sqldw-latest)   
[sys.pdw_materialized_view_mappings &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-pdw-materialized-view-mappings-transact-sql?view=azure-sqldw-latest)   
[DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-pdw-showmaterializedviewoverhead-transact-sql?view=azure-sqldw-latest)   
[[!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)   
[System views supported in Azure [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)]](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-system-views)   
[T-SQL statements supported in Azure [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)]](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-statements)
