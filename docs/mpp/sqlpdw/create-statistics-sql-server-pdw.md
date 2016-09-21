---
title: "CREATE STATISTICS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 9f053d08-5e3b-48bd-b70b-d6b120f3fdf6
caps.latest.revision: 43
author: BarbKess
---
# CREATE STATISTICS (SQL Server PDW)
Creates query optimization statistics on one or more columns of a SQL Server PDW table. The Control node uses statistics to improve the MPP distributed query plan for data movement operations, and the Compute nodes use statistics to improve the SMP query plan. Use this to improve query performance by pre-populating the Control node and the Compute nodes with user-defined single-column or multi-column statistics.  
  
For more information, see [Understanding Query Optimization Statistics &#40;SQL Server PDW&#41;](../sqlpdw/understanding-query-optimization-statistics-sql-server-pdw.md).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CREATE STATISTICS statistics_name   
    ON [ database_name . [schema_name ] . | schema_name. ] table_name   
    ( column_name  [ ,...n ] )   
    [ WHERE <filter_predicate> ]  
    [ WITH {  
           FULLSCAN   
           | SAMPLE number PERCENT   
      }  
    ]  
[;]  
  
<filter_predicate> ::=   
    <conjunct> [AND <conjunct>]  
  
<conjunct> ::=  
    <disjunct> | <comparison>  
  
<disjunct> ::=  
        column_name IN (constant ,…)  
  
<comparison> ::=  
        column_name <comparison_op> constant  
  
<comparison_op> ::=  
    IS | IS NOT | = | <> | != | > | >= | !> | < | <= | !<  
```  
  
## Arguments  
*statistics_name*  
The name of the statistics to create. See [Object Naming Rules &#40;SQL Server PDW&#41;](../sqlpdw/object-naming-rules-sql-server-pdw.md) for information on supported statistics names.  
  
[ *database_name* . [ *schema_name* ] . | *schema_name* . ] *table_name*  
The one- to three-part name of the table that will contain the statistics.  
  
*column_name* [ ,...*n* ]  
One or more columns to be included in the statistics. The columns should be in priority order from left to right. Only the first column is used for creating the histogram. All columns are used for cross-column correlation statistics called densities.  
  
WHERE <filter_predicate>  
Specifies an expression for selecting a subset of rows to include when creating the statistics object. Statistics that are created with a filter predicate are called filtered statistics. The filter predicate uses simple comparison logic. Comparisons using NULL literals are not allowed with the comparison operators. Use the IS NULL and IS NOT NULL operators instead.  
  
Here are some examples of filter predicates for the Production.BillOfMaterials table:  
  
`WHERE StartDate > '20000101' AND EndDate <= '20000630'`  
  
`WHERE ComponentID IN (533, 324, 753)`  
  
`WHERE StartDate IN ('20000404', '20000905') AND EndDate IS NOT NULL`  
  
FULLSCAN  
Compute statistics by scanning all rows in the table. FULLSCAN and SAMPLE 100 PERCENT have the same results. FULLSCAN cannot be used with the SAMPLE option.  
  
When neither FULLSCAN nor SAMPLE is specified, the query optimizer uses sampled data and computes the sample size by default.  
  
SAMPLE *number* PERCENT  
Specifies the approximate percentage of rows in the table for the query optimizer to use when it creates statistics. For PERCENT, *number* can be from 0 through 100. The actual percentage of rows the query optimizer samples might not match the percentage specified. For example, the query optimizer scans all rows on a data page.  
  
SAMPLE cannot be used with the FULLSCAN option.  
  
When neither SAMPLE nor FULLSCAN is specified, the query optimizer uses sampled data and computes the sample size by default. For regular tables, SQL Server determines the sample size. For external tables, SQL Server PDW computes the sample size by using a similar method.  
  
SAMPLE is useful for special cases in which the query plan, based on default sampling, is not optimal. In most situations, it is not necessary to specify SAMPLE because the query optimizer already determines the sample size by default..  
  
We recommend against specifying 0 PERCENT. When 0 PERCENT is specified, the statistics object is created but does not contain statistics data.  
  
## Permissions  
Requires ALTER permission on the table, the table owner, or a member of the db_ddladmin fixed database role.  
  
## General Remarks  
The query optimizer use statistics to estimate the cardinality of the query plan operations. This in turn enables the query optimizer to choose operations that will improve query performance.  
  
This statement creates SMP SQL Server statistics on the Compute nodes and then merges the statistics. It stores the merged statistics on the Control node where the query optimizer uses them to make plan choices that make the data movement operations perform faster.  
  
External table statistics are stored on the Control node. When creating external table statistics, SQL Server PDW imports the external table into a temporary SQL Server PDW table, and then creates the statistics. For sampled statistics, only the sampled rows are imported. If you have a large external table, it will be much faster to created sampled statistics.  
  
## Limitations and Restrictions  
For information on minimum and maximum constraints on statistics, see [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../sqlpdw/minimum-and-maximum-values-sql-server-pdw.md).  
  
Updating statistics is not supported on external tables. To update statistics on an external table, drop the statistics and then re-create the statistics.  
  
SQL Server PDW does not automatically create and update statistics on the Control node. Running CREATE STATISTICS is the only way to populate statistics on the Control node.  
  
The Compute nodes automatically create single-column statistics as needed for the SMP query plan. These are system-level statistics and are not exposed to user operations such as deleting statistics.  
  
## Locking  
Takes an exclusive lock on the TABLE object. Takes a shared lock on the DATABASE object.  
  
## Performance  
We recommend using CREATE STATISTICS to create single column statistics on columns that are used in join predicates, ORDER BY, WHERE, and GROUP BY clauses. This ensures the statistics are available to the Control node and to the Compute nodes when they choose the query plan.  
  
To be in full control of creating, updating, and deleting statistics, consider creating single-column statistics right after creating the table. This avoids the possibility that a system generated statistic and also a user-defined statistic are generated on the same column; this would require double compute time to create the statistics and also to update statistics. We recommend creating single-column statistics on columns in join predicates, and also columns in WHERE, ORDER BY, and GROUP BY clauses.  
  
1.  
  
Multi-column stats are not used for composite filters, which are filters on the same table. The cardinality estimator estimates the cardinality of composite filters with an approach called exponential backoff.  
  
Multi-column statistics improve the query plan for:  
  
-   Multiple joins on equality join predicates. Creating multi-column statistics for this situation can prevent SMP SQL Server from using nested loops in the query plan.  
  
-   Multiple group by clauses. This improves the cardinality estimates for aggregations.  
  
-   Distinct counts on multiple columns. This improves accuracy by reducing the cardinality estimate when the columns are correlated.  
  
## Examples  
  
### A. Create statistics on two columns  
The following example creates the `CustomerStats1` statistics, based on the `CustomerKey` and `EmailAddress` columns of the `DimCustomer` table. The statistics are created based on a statistically significant sampling of the rows in the `Customer` table.  
  
```  
CREATE STATISTICS CustomerStats1 ON DimCustomer (CustomerKey, EmailAddress);  
```  
  
### B. Create statistics by using a full scan  
The following example creates the `CustomerStatsFullScan` statistics, based on scanning all of the rows in the `DimCustomer` table.  
  
```  
CREATE STATISTICS CustomerStatsFullScan ON DimCustomer (CustomerKey, EmailAddress) WITH FULLSCAN;  
```  
  
### C. Create statistics by specifying the sample percentage  
The following example creates the `CustomerStatsSampleScan` statistics, based on scanning 50 percent of the rows in the `DimCustomer` table.  
  
```  
CREATE STATISTICS CustomerStatsSampleScan ON DimCustomer (CustomerKey, EmailAddress) WITH SAMPLE 50 PERCENT;  
```  
  
### D. Create filtered statistics  
The following example creates the filtered statistics `ContactPromotion1`. SQL Server PDW samples 50 percent of the data and then selects the rows with `EmailPromotion` equal to 2.  
  
```  
USE AdventureWorksPDW2012;  
GO  
IF EXISTS (SELECT name FROM sys.stats  
    WHERE name = N'ContactPromotion1'  
    AND object_id = OBJECT_ID(N'Person.Person'))  
DROP STATISTICS Person.Person.ContactPromotion1;  
GO  
CREATE STATISTICS ContactPromotion1  
    ON Person.Person (BusinessEntityID, LastName, EmailPromotion)  
WHERE EmailPromotion = 2  
WITH SAMPLE 50 PERCENT;  
GO  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Understanding Query Optimization Statistics &#40;SQL Server PDW&#41;](../sqlpdw/understanding-query-optimization-statistics-sql-server-pdw.md)  
[UPDATE STATISTICS &#40;SQL Server PDW&#41;](../sqlpdw/update-statistics-sql-server-pdw.md)  
[DROP STATISTICS &#40;SQL Server PDW&#41;](../sqlpdw/drop-statistics-sql-server-pdw.md)  
[STATS_DATE &#40;PDW&#41;](../sqlpdw/stats-date-pdw.md)  
  
