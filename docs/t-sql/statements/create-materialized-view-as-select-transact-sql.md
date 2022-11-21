---
title: "CREATE MATERIALIZED VIEW AS SELECT (Transact-SQL) creates a materialized view to persist the data returned from the view definition query and automatically gets updated as data changes in the underlying tables."
description: CREATE MATERIALIZED VIEW AS SELECT (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.reviewer: xiaoyul
ms.date: 07/20/2022
ms.service: sql
ms.subservice: data-warehouse
ms.topic: reference
f1_keywords:
  - "CREATE VIEW"
  - "VIEW"
  - "SCHEMABINDING_TSQL"
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
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest"
---
# CREATE MATERIALIZED VIEW AS SELECT (Transact-SQL)  

[!INCLUDE [Azure Synapse Analytics](../../includes/applies-to-version/asa.md)]

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
      | DISTRIBUTION = HASH ( [distribution_column_name [, ...n]] ) 
      | DISTRIBUTION = ROUND_ROBIN  
    }

<select_statement> ::=
    SELECT select_criteria

```

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]
  
## Arguments

#### *schema_name*     
 Is the name of the schema to which the view belongs.  
  
#### *materialized_view_name*   
Is the name of the view. View names must follow the rules for identifiers. Specifying the view owner name is optional.  

#### *distribution option*     
Only HASH and ROUND_ROBIN distributions are supported. For more information on distribution options, see [CREATE TABLE Table distribution options](create-table-azure-sql-data-warehouse.md#TableDistributionOptions). For recommendations on which distribution to choose for a table based on actual usage or sample queries, see [Distribution Advisor in Azure Synapse SQL](/azure/synapse-analytics/sql/distribution-advisor). 

`DISTRIBUTION` = `HASH` ( *distribution_column_name* )     
Distributes the rows based on the values of a single column.

`DISTRIBUTION = HASH ( [distribution_column_name [, ...n]] )` (*preview*) 
Distributes the rows based on the hash values of up to eight columns, allowing for more even distribution of materialized view data, reducing the data skew over time and improving query performance. 

> [!NOTE]
> - To enable this preview feature, join the preview by changing the database's compatibility level to 9000 with this command. For more information on setting the database compatibility level, see [ALTER DATABSE SCOPED CONFIGURATION](./alter-database-scoped-configuration-transact-sql.md). For example: `DATABASE SCOPED CONFIGURATION SET DW_COMPATIBILITY_LEVEL = 9000;`
> - To opt-out the preview, run this command to change the database's compatibility level to AUTO. For example: `ALTER DATABASE SCOPED CONFIGURATION SET DW_COMPATIBILITY_LEVEL = AUTO;` This will disable the multi-column distribution (MCD) feature (preview). Existing MCD materialized views will stay but become unreadable. 
>     - To regain access to MCD materialized views, opt-in the preview again. 

#### *select_statement*   
The SELECT list in the materialized view definition needs to meet at least one of these two criteria:

- The SELECT list contains an aggregate function.
- GROUP BY is used in the Materialized view definition and all columns in GROUP BY are included in the SELECT list.  Up to 32 columns can be used in the GROUP BY clause.

Aggregate functions are required in the SELECT list of the materialized view definition.  Supported aggregations include MAX, MIN, AVG, COUNT, COUNT_BIG, SUM, VAR, STDEV.

When MIN/MAX aggregates are used in the SELECT list of materialized view definition, following requirements apply:

- `FOR_APPEND` is required.  For example:
  ```sql 
  CREATE MATERIALIZED VIEW mv_test2  
  WITH (distribution = hash(i_category_id), FOR_APPEND)  
  AS
  SELECT MAX(i.i_rec_start_date) as max_i_rec_start_date, MIN(i.i_rec_end_date) as min_i_rec_end_date, i.i_item_sk, i.i_item_id, i.i_category_id
  FROM syntheticworkload.item i  
  GROUP BY i.i_item_sk, i.i_item_id, i.i_category_id
  ```

- The materialized view will be disabled when an UPDATE or DELETE occurs in the referenced base tables.  This restriction doesn't apply to INSERTs.  To re-enable the materialized view, run ALTER MATERIALIZED VIEW with REBUILD.
  
## Remarks

A materialized view in Azure data warehouse is similar to an indexed view in SQL Server.  It shares almost the same restrictions as indexed view (see [Create Indexed Views](../../relational-databases/views/create-indexed-views.md) for details) except that a materialized view supports aggregate functions.   

>[!Note]
>Although CREATE MATERIALIZED VIEW does not support COUNT, DISTINCT, COUNT(DISTINCT expression), or COUNT_BIG (DISTINCT expression), SELECT queries with these functions can still benefit from materialized views for faster performance as the Synapse SQL optimizer can automatically re-write those aggregations in the user query to match existing materialized views.  For details, check this article's example section. 

APPROX_COUNT_DISTINCT is not supported in CREATE MATERIALIZED VIEW AS SELECT.

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

Once created, materialized views are visible within [SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md) under the views folder of the [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)] instance.

Users can run [SP_SPACEUSED](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md?view=azure-sqldw-latest&preserve-view=true) and [DBCC PDW_SHOWSPACEUSED](../database-console-commands/dbcc-pdw-showspaceused-transact-sql.md?view=azure-sqldw-latest&preserve-view=true) to determine the space being consumed by a materialized view. There are also DMVs to provide more customizable queries to identify space and rows consumed. For more information, see [Table size queries](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-overview#table-size-queries).

A materialized view can be dropped via DROP VIEW.  You can use ALTER MATERIALIZED VIEW to disable or rebuild a materialized view.   

Materialized view is an automatic query optimization mechanism.  Users don't need to query a materialized view directly.  When a user query is submitted, the engine checks the user's permissions to the query objects and fails the query without execution if the user doesn't have access to the tables or regular views in the query.  If the user's permission has been verified, the optimizer automatically uses a matching materialized view to execute the query for faster performance.  Users get the same data back regardless if the query is served by querying the base tables or the materialized view.  

EXPLAIN plan and the graphical Estimated Execution Plan in SQL Server Management Studio can show whether a materialized view is considered by the query optimizer for query execution. and the graphical Estimated Execution Plan in SQL Server Management Studio can show whether a materialized view is considered by the query optimizer for query execution.

To find out if a SQL statement can benefit from a new materialized view, run the `EXPLAIN` command with `WITH_RECOMMENDATIONS`.  For details, see [EXPLAIN (Transact-SQL)](../queries/explain-transact-sql.md?view=azure-sqldw-latest&preserve-view=true).

## Ownership
- A materialized view cannot be created if the owners of the base tables and the materialized view to-be-created are not the same.
- A materialized view and its base tables can reside in different schemas. When the materialized view is created, the  view's schema owner automatically becomes the owner of the materialized view and this view ownership cannot be changed.     

## Permissions
A user needs following permissions to create a materialized view in addition to meeting the object ownership requirements: 
1) CREATE VIEW permission in the database
2) SELECT permission on the base tables of the materialized view
3) REFERENCES permission on the schema containing the base tables
4) ALTER permission on schema containing the materialized view 


## Example
A. This example shows how Synapse SQL optimizer automatically uses materialized views to execute a query for better performance even when the query uses functions unsupported in CREATE MATERIALIZED VIEW, such as `COUNT(DISTINCT expression)`. A query used to take multiple seconds to complete now finishes in sub-second without any change in the user query.   

```sql 

-- Create a table with ~536 million rows
create table t(a int not null, b int not null, c int not null) with (distribution=hash(a), clustered columnstore index);

insert into t values(1,1,1);

declare @p int =1;
while (@P < 30)
    begin
    insert into t select a+1,b+2,c+3 from t;  
    select @p +=1;
end

-- A SELECT query with COUNT_BIG (DISTINCT expression) took multiple seconds to complete and it reads data directly from the base table a. 
select a, count_big(distinct b) from t group by a;

-- Create two materialized views, not using COUNT_BIG(DISTINCT expression).
create materialized view V1 with(distribution=hash(a)) as select a, b from dbo.t group by a, b;

-- Clear all cache.

DBCC DROPCLEANBUFFERS;
DBCC freeproccache;

-- Check the estimated execution plan in SQL Server Management Studio.  It shows the SELECT query is first step (GET operator) is to read data from the materialized view V1, not from base table a.
select a, count_big(distinct b) from t group by a;

-- Now execute this SELECT query.  This time it took sub-second to complete because Synapse SQL engine automatically matches the query with materialized view V1 and uses it for faster query execution.  There was no change in the user query.

DECLARE @timerstart datetime2, @timerend datetime2;
SET @timerstart = sysdatetime();

select a, count_big(distinct b) from t group by a;

SET @timerend = sysdatetime()
select DATEDIFF(ms,@timerstart,@timerend);

```

B. In this example, User2 creates a materialized view on tables owned by User1.  The materialized view is owned by User1.

```sql
/****************************************************************
Setup:
SchemaX owner = DBO
SchemaX.T1 owner = User1
SchemaX.T2 owner = User1
SchemaY owner = User1
*****************************************************************/
CREATE USER User1 WITHOUT LOGIN ;
CREATE USER User2 WITHOUT LOGIN ;
GO
CREATE SCHEMA SchemaX;
GO
CREATE SCHEMA SchemaY AUTHORIZATION User1;
GO
CREATE TABLE [SchemaX].[T1] (    [vendorID] [varchar](255) Not NULL, [totalAmount] [float] Not NULL,    [puYear] [int] NULL );
CREATE TABLE [SchemaX].[T2] (    [vendorID] [varchar](255) Not NULL,    [totalAmount] [float] Not NULL,    [puYear] [int] NULL);
GO
ALTER AUTHORIZATION ON OBJECT::SchemaX.[T1] TO User1;
ALTER AUTHORIZATION ON OBJECT::SchemaX.[T2] TO User1;

/*****************************************************************************
For user2 to create a MV in SchemaY on SchemaX.T1 and SchemaX.T2, user2 needs:
1. CREATE VIEW permission in the database
2. REFERENCES permission on the schema1
3. SELECT permission on base table T1, T2  
4. ALTER permission on SchemaY
******************************************************************************/
GRANT CREATE VIEW to User2;
GRANT REFERENCES ON SCHEMA::SchemaX to User2;  
GRANT SELECT ON OBJECT::SchemaX.T1 to User2; 
GRANT SELECT ON OBJECT::SchemaX.T2 to User2;
GRANT ALTER ON SCHEMA::SchemaY to User2; 
GO
EXECUTE AS USER = 'User2';  
GO
CREATE materialized VIEW [SchemaY].MV_by_User2 with(distribution=round_robin) 
as 
        select A.vendorID, sum(A.totalamount) as S, Count_Big(*) as T 
        from [SchemaX].[T1] A
        inner join [SchemaX].[T2] B on A.vendorID = B.vendorID group by A.vendorID ;
GO
revert;
GO
```

## See also

- [ALTER MATERIALIZED VIEW &#40;Transact-SQL&#41;](./alter-materialized-view-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)      
- [DROP VIEW](./drop-view-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)  
- [EXPLAIN &#40;Transact-SQL&#41;](../queries/explain-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
- [sys.pdw_materialized_view_column_distribution_properties &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-materialized-view-column-distribution-properties-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
- [sys.pdw_materialized_view_distribution_properties &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-materialized-view-distribution-properties-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
- [sys.pdw_materialized_view_mappings &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-materialized-view-mappings-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
- [DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD &#40;Transact-SQL&#41;](../database-console-commands/dbcc-pdw-showmaterializedviewoverhead-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)   
- [[!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)   
- [System views supported in Azure [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)]](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-system-views)   
- [T-SQL statements supported in Azure [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)]](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-statements)

## Next steps

- [Performance tuning with Materialized View](/azure/sql-data-warehouse/performance-tuning-materialized-views)