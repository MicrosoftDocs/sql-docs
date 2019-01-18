---
title: "CREATE STATISTICS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/04/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STATISTICS"
  - "STATISTICS_TSQL"
  - "CREATE STATISTICS"
  - "CREATE_STATISTICS_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "query optimization statistics [SQL Server], creating"
  - "indexed views [SQL Server], statistics"
  - "FULLSCAN option"
  - "CREATE STATISTICS statement"
  - "filtered statistics [SQL Server]"
  - "creating statistics [SQL Server]"
  - "NORECOMPUTE clause"
ms.assetid: b23e2f6b-076c-4e6d-9281-764bdb616ad2
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE STATISTICS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Creates query optimization statistics on one or more columns of a table, an indexed view, or an external table. For most queries, the query optimizer already generates the necessary statistics for a high-quality query plan; in a few cases, you need to create additional statistics with CREATE STATISTICS or modify the query design to improve query performance.  
  
 To learn more, see [Statistics](../../relational-databases/statistics/statistics.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server and Azure SQL Database  
  
-- Create statistics on an external table  
CREATE STATISTICS statistics_name   
ON { table_or_indexed_view_name } ( column [ ,...n ] )   
    [ WITH FULLSCAN ] ;  
  
-- Create statistics on a regular table or indexed view  
CREATE STATISTICS statistics_name   
ON { table_or_indexed_view_name } ( column [ ,...n ] )   
    [ WHERE <filter_predicate> ]  
    [ WITH   
        [ [ FULLSCAN   
            [ [ , ] PERSIST_SAMPLE_PERCENT = { ON | OFF } ]    
          | SAMPLE number { PERCENT | ROWS }   
            [ [ , ] PERSIST_SAMPLE_PERCENT = { ON | OFF } ]    
          | <update_stats_stream_option> [ ,...n ]    
        [ [ , ] NORECOMPUTE ]   
        [ [ , ] INCREMENTAL = { ON | OFF } ] 
        [ [ , ] MAXDOP = max_degree_of_parallelism ]
    ] ;  
  
<filter_predicate> ::=   
    <conjunct> [AND <conjunct>]  
  
<conjunct> ::=  
    <disjunct> | <comparison>  
  
<disjunct> ::=  
        column_name IN (constant ,...)  
  
<comparison> ::=  
        column_name <comparison_op> constant  
  
<comparison_op> ::=  
    IS | IS NOT | = | <> | != | > | >= | !> | < | <= | !<  
    
<update_stats_stream_option> ::=  
    [ STATS_STREAM = stats_stream ]  
    [ ROWCOUNT = numeric_constant ]  
    [ PAGECOUNT = numeric_contant ] 
```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
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
        column_name IN (constant ,...)  
  
<comparison> ::=  
        column_name <comparison_op> constant  
  
<comparison_op> ::=  
    IS | IS NOT | = | <> | != | > | >= | !> | < | <= | !<  
```  
  
## Arguments  
 *statistics_name*  
 Is the name of the statistics to create.  
  
 *table_or_indexed_view_name*  
 Is the name of the table, indexed view, or external table on which to create the statistics. To create statistics on another database, specify a qualified table name.  
  
 *column [ ,...n]*  
 One or more columns to be included in the statistics. The columns should be in priority order from left to right. Only the first column is used for creating the histogram. All columns are used for cross-column correlation statistics called densities.  
  
 You can specify any column that can be specified as an index key column with the following exceptions:  
  
-   **Xml**, full-text, and FILESTREAM columns cannot be specified.  
  
-   Computed columns can be specified only if the ARITHABORT and QUOTED_IDENTIFIER database settings are ON.  
  
-   CLR user-defined type columns can be specified if the type supports binary ordering. Computed columns defined as method invocations of a user-defined type column can be specified if the methods are marked deterministic.  
  
 WHERE \<filter_predicate> 
 Specifies an expression for selecting a subset of rows to include when creating the statistics object. Statistics that are created with a filter predicate are called filtered statistics. The filter predicate uses simple comparison logic and cannot reference a computed column, a UDT column, a spatial data type column, or a **hierarchyID** data type column. Comparisons using NULL literals are not allowed with the comparison operators. Use the IS NULL and IS NOT NULL operators instead.  
  
 Here are some examples of filter predicates for the Production.BillOfMaterials table:  
  
 * `WHERE StartDate > '20000101' AND EndDate <= '20000630'`  
  
 * `WHERE ComponentID IN (533, 324, 753)`  
  
 * `WHERE StartDate IN ('20000404', '20000905') AND EndDate IS NOT NULL`  
  
 For more information about filter predicates, see [Create Filtered Indexes](../../relational-databases/indexes/create-filtered-indexes.md).  
  
 FULLSCAN  
 Compute statistics by scanning all rows. FULLSCAN and SAMPLE 100 PERCENT have the same results. FULLSCAN cannot be used with the SAMPLE option.  
  
 When omitted, SQL Server uses sampling to create the statistics, and determines the sample size that is required to create a high quality query plan  
  
 SAMPLE *number* { PERCENT | ROWS }  
 Specifies the approximate percentage or number of rows in the table or indexed view for the query optimizer to use when it creates statistics. For PERCENT, *number* can be from 0 through 100 and for ROWS, *number* can be from 0 to the total number of rows. The actual percentage or number of rows the query optimizer samples might not match the percentage or number specified. For example, the query optimizer scans all rows on a data page.  
  
 SAMPLE is useful for special cases in which the query plan, based on default sampling, is not optimal. In most situations, it is not necessary to specify SAMPLE because the query optimizer already uses sampling and determines the statistically significant sample size by default, as required to create high-quality query plans.  
  
 SAMPLE cannot be used with the FULLSCAN option. When neither SAMPLE nor FULLSCAN is specified, the query optimizer uses sampled data and computes the sample size by default.  
  
 We recommend against specifying 0 PERCENT or 0 ROWS. When 0 PERCENT or ROWS is specified, the statistics object is created but does not contain statistics data.  
 
 PERSIST_SAMPLE_PERCENT = { ON | OFF }  
 When **ON**, the statistics will retain the creation sampling percentage for subsequent updates that do not explicitly specify a sampling percentage. When **OFF**, statistics sampling percentage will get reset to default sampling in subsequent updates that do not explicitly specify a sampling percentage. The default is **OFF**. 
 
 **Applies to**: [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] (starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1 CU4) through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] (starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU1).    
  
 STATS_STREAM **=**_stats_stream_  
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  
  
 NORECOMPUTE  
 Disable the automatic statistics update option, AUTO_STATISTICS_UPDATE, for *statistics_name*. If this option is specified, the query optimizer will complete any in-progress statistics updates for *statistics_name* and disable future updates.  
  
 To re-enable statistics updates, remove the statistics with [DROP STATISTICS](../../t-sql/statements/drop-statistics-transact-sql.md) and then run CREATE STATISTICS without the NORECOMPUTE option.  
  
> [!WARNING]  
> Using this option can produce suboptimal query plans. We recommend using this option sparingly, and then only by a qualified system administrator.  
  
 For more information about the AUTO_STATISTICS_UPDATE option, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md). For more information about disabling and re-enabling statistics updates, see [Statistics](../../relational-databases/statistics/statistics.md).  
  
 INCREMENTAL = { ON | OFF }  
 When **ON**, the statistics created are per partition statistics. When **OFF**, stats are combined for all partitions. The default is **OFF**.  
  
 If per partition statistics are not supported an error is generated. Incremental stats are not supported for following statistics types:  
  
-   Statistics created with indexes that are not partition-aligned with the base table.  
-   Statistics created on Always On readable secondary databases.  
-   Statistics created on read-only databases.  
-   Statistics created on filtered indexes.  
-   Statistics created on views.  
-   Statistics created on internal tables.  
-   Statistics created with spatial indexes or XML indexes.  
  
**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
MAXDOP = *max_degree_of_parallelism*  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3).  
  
 Overrides the **max degree of parallelism** configuration option for the duration of the statistic operation. For more information, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md). Use MAXDOP to limit the number of processors used in a parallel plan execution. The maximum is 64 processors.  
  
 *max_degree_of_parallelism* can be:  
  
 1  
 Suppresses parallel plan generation.  
  
 \>1  
 Restricts the maximum number of processors used in a parallel statistic operation to the specified number or fewer based on the current system workload.  
  
 0 (default)  
 Uses the actual number of processors or fewer based on the current system workload.  
  
 \<update_stats_stream_option> 
 [!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]  

## Permissions  
 Requires one of these permissions:  
  
-   ALTER TABLE  
-   User is the table owner  
-   Membership in the **db_ddladmin** fixed database role  
  
## General Remarks  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can use tempdb to sort the sampled rows before building statistics.  
  
### Statistics for external tables  
 When creating external table statistics, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] imports the external table into a temporary [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table, and then creates the statistics. For samples statistics, only the sampled rows are imported. If you have a large external table, it will be much faster to use the default sampling instead of the full scan option.  
  
### Statistics with a filtered condition  
 Filtered statistics can improve query performance for queries that select from well-defined subsets of data. Filtered statistics use a filter predicate in the WHERE clause to select the subset of data that is included in the statistics.  
  
### When to Use CREATE STATISTICS  
 For more information about when to use CREATE STATISTICS, see [Statistics](../../relational-databases/statistics/statistics.md).  
  
### Referencing Dependencies for Filtered Statistics  
 The [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) catalog view tracks each column in the filtered statistics predicate as a referencing dependency. Consider the operations that you perform on table columns before creating filtered statistics because you cannot drop, rename, or alter the definition of a table column that is defined in a filtered statistics predicate.  
  
## Limitations and Restrictions  
* Updating statistics is not supported on external tables. To update statistics on an external table, drop and re-create the statistics.  
* You can list up to 64 columns per statistics object.
* The MAXDOP option is not compatible with STATS_STREAM, ROWCOUNT and PAGECOUNT options.
* The MAXDOP option is limited by the Resource Governor workload group MAX_DOP setting, if used.
  
## Examples  

### Examples use the AdventureWorks database.  

### A. Using CREATE STATISTICS with SAMPLE number PERCENT  
 The following example creates the `ContactMail1` statistics, using a random sample of 5 percent of the `BusinessEntityID` and `EmailPromotion` columns of the `Contact` table of the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)]database.  
  
```sql  
CREATE STATISTICS ContactMail1  
    ON Person.Person (BusinessEntityID, EmailPromotion)  
    WITH SAMPLE 5 PERCENT;  
```  
  
### B. Using CREATE STATISTICS with FULLSCAN and NORECOMPUTE  
 The following example creates the `ContactMail2` statistics for all rows in the `BusinessEntityID` and `EmailPromotion` columns of the `Contact` table and disables automatic recomputing of statistics.  
  
```sql  
CREATE STATISTICS NamePurchase  
    ON AdventureWorks2012.Person.Person (BusinessEntityID, EmailPromotion)  
    WITH FULLSCAN, NORECOMPUTE;  
```  
  
### C. Using CREATE STATISTICS to create filtered statistics  
 The following example creates the filtered statistics `ContactPromotion1`. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] samples 50 percent of the data and then selects the rows with `EmailPromotion` equal to 2.  
  
```sql  
CREATE STATISTICS ContactPromotion1  
    ON Person.Person (BusinessEntityID, LastName, EmailPromotion)  
WHERE EmailPromotion = 2  
WITH SAMPLE 50 PERCENT;  
GO  
```  
  
### D. Create statistics on an external table  
 The only decision you need to make when you create statistics on an external table, besides providing the list of columns, is whether to create the statistics by sampling the rows or by scanning all of the rows.  
  
 Since [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] imports data from the external table into a temporary table to create statistics, the full scan option will take much longer. For a large table, the default sampling method is usually sufficient.  
  
```sql  
--Create statistics on an external table and use default sampling.  
CREATE STATISTICS CustomerStats1 ON DimCustomer (CustomerKey, EmailAddress);  
  
--Create statistics on an external table and scan all the rows  
CREATE STATISTICS CustomerStats1 ON DimCustomer (CustomerKey, EmailAddress) WITH FULLSCAN;  
```  

### E. Using CREATE STATISTICS with FULLSCAN and PERSIST_SAMPLE_PERCENT  
 The following example creates the `ContactMail2` statistics for all rows in the `BusinessEntityID` and `EmailPromotion` columns of the `Contact` table and sets a 100 percent sampling percentage for all subsequent updates that do not explicitly specify a sampling percentage.  
  
```sql  
CREATE STATISTICS NamePurchase  
    ON AdventureWorks2012.Person.Person (BusinessEntityID, EmailPromotion)  
    WITH FULLSCAN, PERSIST_SAMPLE_PERCENT = ON;  
```  
  
### Examples using AdventureWorksDW database. 
  
### F. Create statistics on two columns  
 The following example creates the `CustomerStats1` statistics, based on the `CustomerKey` and `EmailAddress` columns of the `DimCustomer` table. The statistics are created based on a statistically significant sampling of the rows in the `Customer` table.  
  
```sql  
CREATE STATISTICS CustomerStats1 ON DimCustomer (CustomerKey, EmailAddress);  
```  
  
### G. Create statistics by using a full scan  
 The following example creates the `CustomerStatsFullScan` statistics, based on scanning all of the rows in the `DimCustomer` table.  
  
```sql  
CREATE STATISTICS CustomerStatsFullScan 
ON DimCustomer (CustomerKey, EmailAddress) WITH FULLSCAN;  
```  
  
### H. Create statistics by specifying the sample percentage  
 The following example creates the `CustomerStatsSampleScan` statistics, based on scanning 50 percent of the rows in the `DimCustomer` table.  
  
```sql  
CREATE STATISTICS CustomerStatsSampleScan 
ON DimCustomer (CustomerKey, EmailAddress) WITH SAMPLE 50 PERCENT;  
```  
  
## See Also  
 [Statistics](../../relational-databases/statistics/statistics.md)   
 [UPDATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/update-statistics-transact-sql.md)   
 [sp_updatestats &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-updatestats-transact-sql.md)   
 [DBCC SHOW_STATISTICS &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)   
 [DROP STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/drop-statistics-transact-sql.md)   
 [sys.stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-stats-transact-sql.md)   
 [sys.stats_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-stats-columns-transact-sql.md)  
  
  

