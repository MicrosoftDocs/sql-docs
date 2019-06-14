---
title: "CREATE MATERIALIZED VIEW AS SELECT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/10/2018"
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
author: XiaoyuL-Preview  
ms.author: xiaoyul
manager: craigg
monikerRange: ||=azure-sqldw-latest||=sqlallproducts-allversions"
---
# CREATE MATERIALIZED VIEW AS SELECT (Transact-SQL)

[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

This article explains the CREATE MATERIALIZED VIEW AS SELECT T-SQL statement in Azure SQL Data Warehouse for developing solutions. The article also provides code examples.

A Materialized View persists the data returned from the view definition query and automatically gets updated as data changes in the underlying tables.   It improves the performance of complex queries (typically queries with joins and aggregations) while offering simple maintenance operations.   With its execution plan automatching capability, a materialized view does not have to be referenced in the query for the optimizer to consider the view for substitution.  This allows data engineers to implement indexed views as a mechanism for improving query response time, without having to change queries.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
CREATE MATERIALIZED VIEW [ schema_name. ] table_name
    [ ( column_name [ ,...n ] ) ]
    WITH (  
      <distribution_option> -- required
      [ , <table_option> [ ,...n ] ]
    )
    AS <select_statement>
[;]

<distribution_option> ::=
    {  
        DISTRIBUTION = HASH ( distribution_column_name )  
      | DISTRIBUTION = ROUND_ROBIN  
    }

<table_option> ::=  
    {
        CLUSTERED COLUMNSTORE INDEX  
    }

<select_statement> ::=
    [ WITH <common_table_expression> [ ,...n ] ]
    SELECT select_criteria
```  
  
## Arguments

*schema_name*  
 Is the name of the schema to which the view belongs.  
  
*view_name*
Is the name of the view. View names must follow the rules for identifiers. Specifying the view owner name is optional.  

*distribution option*
Only HASH and ROUND_ROBIN distributions are supported.

*table option*
Only CLUSTERED COLUMNSTORE INDEX is supported.

*select_statement*
The SELECT list in the materialized view definition needs to meet at least one of these two criteria:
- The SELECT list contains an aggregate function.
- GROUP BY is used in the Materialized view definition and all columns in GROUP BY are included in the SELECT list.  

Aggregate functions are required in the SELECT list of the materialized view definition.  Supported aggregations include MAX, MIN, AVG, COUNT, COUNT_BIG, SUM, VAR, STDEV.

MIN/MAX aggregates used in the SELECT list of the materialized view definition cause the materialized view to be disabled when an UPDATE or DELETE occurs in the referenced base tables.   To re-enable the materialized view, run ALTER MATERIALIZED INDEX with REBUILD.  Inserts into the base table don’t impact the materialized view.

The SELECT list in the materialized view definition cannot include other views or the CREATE MATERIALIZE VIEW will fail with this error:

Cannot schema bind %S_MSG '%.*ls'. '%.*ls' is not schema bound.

The definition of a MATERIALIZED VIEW must be deterministic.  A view is deterministic if all expressions in the select list, as well as the WHERE and GROUP BY clauses, are deterministic. For details, see **Deterministic Views NEED LINK**.

Only inner joins  are supported in the FROM clause.  Self joins, outer joins, or cross apply are not supported.  
  
## Permissions

Requires CREATE VIEW permission in the database and ALTER permission on the schema in which the view is being created.  

## Remarks

A materialized view can be dropped via DROP VIEW.  ALTER VIEW is not supported.

Materialized Views can be created on partitioned tables.  SPLIT/MERGE operations are supported on tables referenced in materialized views.  SWITCH is not supported on tables referenced in materialized views. If attempted, the user will see the following error.

Msg 106104, Level 16, State 1, Line 9 

ALTER TABLE SWITCH is not supported on tables that are referenced in materialized views. Disable or drop the materialized views before using ALTER TABLE SWITCH. 
In the following scenarios, the materialized view creation requires new columns to be added to the materialized view:

|Scenario|New columns to add to materialized view|Comment|  
|-----------------|---------------|-----------------|
|COUNT_BIG() | is missing in the SELECT list of an materialized view definition |COUNT_BIG (*) |Automatically added by materialized view creation.  No user action is required.|
|SUM(a) is specified by users in the SELECT list of an materialized view definition AND ‘a’ is a nullable expression |COUNT_BIG (a) |Users need to add the expression ‘a’ manually in the materialized view definition.|
|AVG(a) is specified by users in the SELECT list of an materialized view definition where ‘a’ is an expression.|SUM(a), COUNT_BIG(a)|Automatically added by materialized view creation.  No user action is required.|
|STDEV(a) is specified by users in the SELECT list of an materialized view definition where ‘a’ is an expression.|SUM(a),  
COUNT_BIG(a) SUM(square(a))|Automatically added by materialized view creation.  No user action is required. |
| | | |

Once created, materialized views are visible within SQL Server Management Studio under the views folder of the Azure SQL Data Warehouse instance.

Users can run [SP_SPACEUSED](/sql/relational-databases/system-stored-procedures/sp-spaceused-transact-sql?view=azure-sqldw-latest) and [DBCC PDW_SHOWSPACEUSED](/sql/t-sql/database-console-commands/dbcc-pdw-showspaceused-transact-sql?view=azure-sqldw-latest) to determine the space being consumed by an materialized view.  

EXPLAIN plan and the graphical Estimated Execution Plan in SQL Server Management Studio can show whether a materialized view is considered by the query optimizer for query execution. and the graphical Estimated Execution Plan in SQL Server Management Studio can show whether a materialized view is considered by the query optimizer for query execution.  
To find out if a SQL statement can benefit from a new materialized view, run EXPLAIN command with WITH_RECOMMENDATIONS.  For details, check EXPLAIN (Transact-SQL).
  
## Examples  

```SQL
CREATE MATRIALIZED VIEW StoreSales_SalesAmt
WITH (DISTRIBUTION=ROUND_ROBIN)
AS
SELECT s.sales_year, cd.education_status, ib.upper_bound, SalesAmt=SUM(s.ext_sales_price)
FROM dbo.StoreSales s 
JOIN CustomerDemographics cd on cd.demo_sk = s.cdemo_sk
JOIN HouseholdDemographics hd on hd.demo_sk = s.hdemo_sk
JOIN IncomeBand ib on ib.income_band_sk = hd.income_band_sk
GROUP BY s.sales_year, cd.education_status, ib.upper_bound
```  
  
## See also

