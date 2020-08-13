---
title: "ALTER MATERIALIZED VIEW (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/03/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: jrasnick
ms.technology: data-warehouse
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_VIEW_TSQL"
  - "ALTER VIEW"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "indexed views [SQL Server], modifying"
  - "views [SQL Server], modifying"
  - "modifying views"
  - "ALTER VIEW statement"
author: XiaoyuMSFT 
ms.author: xiaoyul
monikerRange: "= azure-sqldw-latest || = sqlallproducts-allversions"
---
# ALTER MATERIALIZED VIEW (Transact-SQL)  

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Modifies a previously created materialized view. ALTER VIEW does not affect dependent stored procedures or triggers and does not change permissions.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
ALTER MATERIALIZED VIEW [ schema_name . ] view_name
{
      REBUILD | DISABLE
}
[;]
```  
  
## Arguments

 *schema_name*     
 Is the name of the schema to which the view belongs.  
  
 *view_name*     
 Is the materialized view to change.  
  
*REBUILD*   
Resumes the materialized view.

*DISABLE*   
Suspends maintenance on the materialized view while maintaining metadata and permissions.  All queries against the materialized view while in a disabled state resolve against the underlying tables.
  
## Permissions

ALTER permission on the table or view is required.
  
## Examples

This example disables a materialized view and puts it in suspended mode.
  
```sql
ALTER MATERIALIZED VIEW My_Indexed_View DISABLE;  
```  
  
This example resumes materialized view by rebuilding it.  
  
```sql
ALTER MATERIALIZED VIEW My_Indexed_View REBUILD;  
```  
  
## See also

[Performance tuning with Materialized View](/azure/sql-data-warehouse/performance-tuning-materialized-views)   
[CREATE MATERIALIZED VIEW AS SELECT &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-materialized-view-as-select-transact-sql?view=azure-sqldw-latest)   
[EXPLAIN &#40;Transact-SQL&#41;](/sql/t-sql/queries/explain-transact-sql?view=azure-sqldw-latest)   
[sys.pdw_materialized_view_column_distribution_properties &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-pdw-materialized-view-column-distribution-properties-transact-sql?view=azure-sqldw-latest)   
[sys.pdw_materialized_view_distribution_properties &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-pdw-materialized-view-distribution-properties-transact-sql?view=azure-sqldw-latest)   
[sys.pdw_materialized_view_mappings &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-pdw-materialized-view-mappings-transact-sql?view=azure-sqldw-latest)   
[DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-pdw-showmaterializedviewoverhead-transact-sql?view=azure-sqldw-latest)   
[SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)   
[System views supported in Azure SQL Data Warehouse](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-system-views)   
[T-SQL statements supported in Azure SQL Data Warehouse](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-statements)
