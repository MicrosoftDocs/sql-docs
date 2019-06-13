---
title: "CREATE MATERIALIZED VIEW AS SELECT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/10/2018"
ms.prod: sql
ms.prod_service: "sql-data-warehouse"
ms.reviewer: ""
ms.technology: dat-warehouse
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
author: 
ms.author: 
manager: craigg
monikerRange: ||=azure-sqldw-latest||=sqlallproducts-allversions"
---
# CREATE MATERIALIZED VIEW AS SELECT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Creates a virtual table whose contents (columns and rows) are defined by a query. Use this statement to create a view of the data in one or more tables in the database. For example, a view can be used for the following purposes:  
  
-   To focus, simplify, and customize the perception each user has of the database.  
  
-   As a security mechanism by allowing users to access data through the view, without granting the users permissions to directly access the underlying base tables.  
  
-   To provide a backward compatible interface to emulate a table whose schema has changed.  
  
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
  
 *column*  
 Is the name to be used for a column in a view. A column name is required only when a column is derived from an arithmetic expression, a function, or a constant; when two or more columns may otherwise have the same name, typically because of a join; or when a column in a view is specified a name different from that of the column from which it is derived. Column names can also be assigned in the SELECT statement.  
  
 If *column* is not specified, the view columns acquire the same names as the columns in the SELECT statement.  
  
> [!NOTE]  
>  In the columns for the view, the permissions for a column name apply across a CREATE VIEW or ALTER VIEW statement, regardless of the source of the underlying data. For example, if permissions are granted on the **SalesOrderID** column in a CREATE VIEW statement, an ALTER VIEW statement can name the **SalesOrderID** column with a different column name, such as **OrderRef**, and still have the permissions associated with the view using **SalesOrderID**.  
  
 AS  
 Specifies the actions the view is to perform.  
  
 *select_statement*  
 Is the SELECT statement that defines the view. The statement can use more than one table and other views. Appropriate permissions are required to select from the objects referenced in the SELECT clause of the view that is created.  
  
 A view does not have to be a simple subset of the rows and columns of one particular table. A view can be created that uses more than one table or other views with a SELECT clause of any complexity.  
  
 In an indexed view definition, the SELECT statement must be a single table statement or a multitable JOIN with optional aggregation.  
  
 The SELECT clauses in a view definition cannot include the following:  
  
-   An ORDER BY clause, unless there is also a TOP clause in the select list of the SELECT statement  
  
    > [!IMPORTANT]  
    >  The ORDER BY clause is used only to determine the rows that are returned by the TOP or OFFSET clause in the view definition. The ORDER BY clause does not guarantee ordered results when the view is queried, unless ORDER BY is also specified in the query itself.  
  
-   The INTO keyword  
  
-   The OPTION clause  
  
-   A reference to a temporary table or a table variable.  
  
 Because *select_statement* uses the SELECT statement, it is valid to use \<join_hint> and \<table_hint> hints as specified in the FROM clause. For more information, see [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md) and [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md). 
  
 Functions and multiple SELECT statements separated by UNION or UNION ALL can be used in *select_statement*.  
  
 CHECK OPTION  
 Forces all data modification statements executed against the view to follow the criteria set within *select_statement*. When a row is modified through a view, the WITH CHECK OPTION makes sure the data remains visible through the view after the modification is committed.  
  
> [!NOTE]  
>  Any updates performed directly to a view's underlying tables are not verified against the view, even if CHECK OPTION is specified.  
  
 ENCRYPTION  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
  
 Encrypts the entries in [sys.syscomments](../../relational-databases/system-compatibility-views/sys-syscomments-transact-sql.md) that contain the text of the CREATE VIEW statement. Using WITH ENCRYPTION prevents the view from being published as part of SQL Server replication.  
  
 SCHEMABINDING  
 Binds the view to the schema of the underlying table or tables. When SCHEMABINDING is specified, the base table or tables cannot be modified in a way that would affect the view definition. The view definition itself must first be modified or dropped to remove dependencies on the table that is to be modified. When you use SCHEMABINDING, the *select_statement* must include the two-part names (_schema_**.**_object_) of tables, views, or user-defined functions that are referenced. All referenced objects must be in the same database.  
  
 Views or tables that participate in a view created with the SCHEMABINDING clause cannot be dropped unless that view is dropped or changed so that it no longer has schema binding. Otherwise, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] raises an error. Also, executing ALTER TABLE statements on tables that participate in views that have schema binding fail when these statements affect the view definition.  
  
 VIEW_METADATA  
 Specifies that the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will return to the DB-Library, ODBC, and OLE DB APIs the metadata information about the view, instead of the base table or tables, when browse-mode metadata is being requested for a query that references the view. Browse-mode metadata is additional metadata that the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns to these client-side APIs. This metadata enables the client-side APIs to implement updatable client-side cursors. Browse-mode metadata includes information about the base table that the columns in the result set belong to.  
  
 For views created with VIEW_METADATA, the browse-mode metadata returns the view name and not the base table names when it describes columns from the view in the result set.  
  
 When a view is created by using WITH VIEW_METADATA, all its columns, except a **timestamp** column, are updatable if the view has INSTEAD OF INSERT or INSTEAD OF UPDATE triggers. For more information about updatable views, see Remarks.  
  
## Remarks

The SELECT list in the materialized view definition needs to meet at least one of these two criteria:

- The SELECT list contains an aggregate function
- GROUP BY is used in the Materialized view definition and all columns in GROUP BY are included in the SELECT list.  

Aggregate functions are required in the SELECT list of the materialized view definition.  Supported aggregations include MAX, MIN, AVG, COUNT, COUNT_BIG, SUM, VAR, STDEV.

MIN/MAX aggregates used in the SELECT list of the materialized view definition cause the materialized view to be disabled when an UPDATE or DELETE occurs in the referenced base tables. To re-enable the materialized view, run `ALTER MATERIALIZED INDEX with REBUILD`.  Inserts into the base table don’t impact the materialized view.
  
The following considerations apply:

- Only INNER JOINs  are supported in the FROM clause.  SELF JOINs, OUTER JOINs, or CROSS APPLY are not supported.
- Only HASH and ROUND_ROBIN distributions are supported.
- Only CLUSTERED COLUMNSTORE INDEX is supported.  
- The definition of a MATERIALIZED VIEW must be deterministic.  A view is deterministic if all expressions in the select list, as well as the WHERE and GROUP BY clauses, are deterministic. For details, check - Deterministic Views.
- ALTER VIEW is not supported.  
- Materialized Views can be created on partitioned tables.  SPLIT/MERGE operations are supported on tables referenced in materialized views.  SWITCH is not supported on tables referenced in materialized views. If attempted, the user will see the following error.
  
## Permissions

Requires CREATE VIEW permission in the database and ALTER permission on the schema in which the view is being created.  
  
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
  
## See Also  
 [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md)   
 [ALTER VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/alter-view-transact-sql.md)   
 [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)   
 [DROP VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/drop-view-transact-sql.md)   
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
 [Create a Stored Procedure](../../relational-databases/stored-procedures/create-a-stored-procedure.md)   
 [sys.dm_sql_referenced_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referenced-entities-transact-sql.md)   
 [sys.dm_sql_referencing_entities &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-sql-referencing-entities-transact-sql.md)   
 [sp_help &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-transact-sql.md)   
 [sp_helptext &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptext-transact-sql.md)   
 [sp_refreshview &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-refreshview-transact-sql.md)   
 [sp_rename &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-rename-transact-sql.md)   
 [sys.views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-views-transact-sql.md)   
 [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  