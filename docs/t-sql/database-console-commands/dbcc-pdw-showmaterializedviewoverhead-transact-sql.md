---
description: "DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD (Transact-SQL)"
title: "DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD  (Transact-SQL)"
ms.custom: seo-dt-2019
ms.date: "07/03/2019"
ms.prod: sql
ms.technology: data-warehouse
ms.prod_service: "sql-data-warehouse, pdw"
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
author: XiaoyuMSFT 
ms.author: xiaoyul
monikerRange: "= azure-sqldw-latest || = sqlallproducts-allversions"
---

# DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD (Transact-SQL)  

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Displays the number of incremental changes in the base tables that are held for materialized views in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)]. The overhead ratio is calculated as TOTAL_ROWS / MAX (1, BASE_VIEW_ROWS).

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions &#40;Transact-SQL&#41;](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax

```syntaxsql
DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD ( " [ schema_name .] materialized_view_name  " )
[;]
```

## Arguments

 *schema_name*     
 Is the name of the schema to which the view belongs.

*materialized_view_name*   
Is the name of the materialized view.

## Remarks

To keep materialized views refreshed with data changes in base tables, data warehouse engine adds tracking rows to each affected view to reflect the changes. Selecting from a materialized view includes scanning the view's clustered columnstore index and applying any incremental changes.  The tracking rows (TOTAL_ROWS - BASE_VIEW_ROWS) do not get eliminated until users REBUILD the materialized view.  

The overhead_ratio is calculated as TOTAL_ROWS/MAX(1, BASE_VIEW_ROWS).  If it's high, SELECT performance will degrade.  Users can rebuild the materialized view to reduce its overhead ratio.

## Permissions  
  
Requires VIEW DATABASE STATE permission.  

## Examples  

### A. This example returns the overhead ratio of a materialized view.

```sql
DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD ( "dbo.MyIndexedView" )
```

Output:

|OBJECT_ID|BASE_VIEW_ROWS|TOTAL_ROWS|OVERHEAD_RATIO|
|--------|--------|--------|--------|  
|1234|1|3 |3.0 |

</br>

### B. This example shows how the materialized view overhead increases as data changes in base tables

Create a table
```sql
CREATE TABLE t1 (c1 int NOT NULL, c2 int not null, c3 int not null)
```
Insert five rows to t1
```sql
INSERT INTO t1 VALUES (1, 1, 1)
INSERT INTO t1 VALUES (2, 2, 2) 
INSERT INTO t1 VALUES (3, 3, 3) 
INSERT INTO t1 VALUES (4, 4, 4) 
INSERT INTO t1 VALUES (5, 5, 5) 
```
Create materialized views MV1
```sql
CREATE materialized view MV1 
WITH (DISTRIBUTION = HASH(c1))  
AS
SELECT c1, count(*) total_number 
FROM dbo.t1 where c1 < 3
GROUP BY c1  
```
Selecting from the materialized view returns two rows.

|c1|total_number|
|--------|--------| 
|1|1| 
|2|1|

Check the materialized view overhead before any data changes in the base table.
```sql
DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD ("dbo.mv1")
```
Output:

|OBJECT_ID|BASE_VIEW_ROWS|TOTAL_ROWS|OVERHEAD_RATIO|
|--------|--------|--------|--------|  
|587149137|2|2 |1.00000000000000000 |

Update the base table.  This query updates the same column in the same row 100 times to the same value.  The materialized view content does not change.
```sql
DECLARE @p int
SELECT @p = 1
WHILE (@p < 101)
BEGIN
UPDATE t1 SET c1 = 1 WHERE c1 = 1
SELECT @p = @p+1
END  
```

Selecting from the materialized view returns the same result as before.  

|c1|total_number|
|--------|--------| 
|1|1| 
|2|1|

Below is the output from DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD ("dbo.mv1").  100 rows are added to the materialized view (total_row - base_view_rows) and its overhead_ratio is increased. 

|OBJECT_ID|BASE_VIEW_ROWS|TOTAL_ROWS|OVERHEAD_RATIO|
|--------|--------|--------|--------|  
|587149137|2|102 |51.00000000000000000 |

After rebuilding the materialized view, all tracking rows for incremental data changes are eliminated and the view overhead ratio is reduced.  

```sql
ALTER MATERIALIZED VIEW dbo.MV1 REBUILD
go
DBCC PDW_SHOWMATERIALIZEDVIEWOVERHEAD ("dbo.mv1")
```
Output

|OBJECT_ID|BASE_VIEW_ROWS|TOTAL_ROWS|OVERHEAD_RATIO|
|--------|--------|--------|--------|  
|587149137|2|2 |1.00000000000000000 |

## See also

[Performance tuning with Materialized View](/azure/sql-data-warehouse/performance-tuning-materialized-views)   
[CREATE MATERIALIZED VIEW AS SELECT &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-materialized-view-as-select-transact-sql?view=azure-sqldw-latest)   
[ALTER MATERIALIZED VIEW &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-materialized-view-transact-sql?view=azure-sqldw-latest)   
[EXPLAIN &#40;Transact-SQL&#41;](/sql/t-sql/queries/explain-transact-sql?view=azure-sqldw-latest)   
[sys.pdw_materialized_view_column_distribution_properties &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-pdw-materialized-view-column-distribution-properties-transact-sql?view=azure-sqldw-latest)   
[sys.pdw_materialized_view_distribution_properties &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-pdw-materialized-view-distribution-properties-transact-sql?view=azure-sqldw-latest)   
[sys.pdw_materialized_view_mappings &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-pdw-materialized-view-mappings-transact-sql?view=azure-sqldw-latest)   
[SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)   
[System views supported in Azure SQL Data Warehouse](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-system-views)   
[T-SQL statements supported in Azure SQL Data Warehouse](/azure/sql-data-warehouse/sql-data-warehouse-reference-tsql-statements)
