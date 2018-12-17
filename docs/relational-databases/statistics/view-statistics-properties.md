---
title: "View Statistics Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.statistics.details.f1"
helpviewer_keywords: 
  - "viewing statistics properties"
  - "statistics [SQL Server], viewing properties"
ms.assetid: 0eaa2101-006e-4015-9979-3468b50e0aaa
author: julieMSFT
ms.author: jrasnick
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# View Statistics Properties
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  You can display current query optimization statistics for a table or indexed view in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Statistics objects include a header with metadata about the statistics, a histogram with the distribution of values in the first key column of the statistics object, and a density vector to measure cross-column correlation. For more information about histograms and density vectors, see [DBCC SHOW_STATISTICS &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To view statistics properties, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 In order to view the statistics object, the user must own the table or the user must be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the **db_ddladmin** fixed database role.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To view statistics properties  
  
1.  In **Object Explorer**, click the plus sign to expand the database in which you want to create a new statistic.  
  
2.  Click the plus sign to expand the **Tables** folder.  
  
3.  Click the plus sign to expand the table in which you want to view the statistic's properties.  
  
4.  Click the plus sign to expand the **Statistics** folder.  
  
5.  Right-click the Statistics object of which you want to view the properties and select **Properties**.  
  
6.  In the **Statistics Properties -** _statistics_name_ dialog box, in the **Select a page** pane, select **Details**.  
  
     The following properties show on the **Details** page in the **Statistics Properties -** _statistics_name_ dialog box.  
  
     **Table Name**  
     Displays the name of the table described by the statistics.  
  
     **Statistics Name**  
     Displays the name of the database object where the statistics are stored.  
  
     **Statistics for INDEXstatistics_name**  
     This text box shows the properties returned from the statistics object. This properties are divided into three sections: Stats Header, Density Vector, and Histogram.  
  
     The following information describes the columns returned in the result set for the Stats Header.  
  
     **Name**  
     Name of the statistics object.  
  
     **Updated**  
     Date and time the statistics were last updated.  
  
     **Rows**  
     Total number of rows in the table or indexed view when the statistics were last updated. If the statistics are filtered or correspond to a filtered index, the number of rows might be less than the number of rows in the table.  
  
     **Rows Sampled**  
     Total number of rows sampled for statistics calculations. If Rows Sampled < Rows, the displayed histogram and density results are estimates based on the sampled rows.  
  
     **Steps**  
     Number of steps in the histogram. Each step spans a range of column values followed by an upper bound column value. The histogram steps are defined on the first key column in the statistics. The maximum number of steps is 200.  
  
     **Density**  
     Calculated as 1 / *distinct values* for all values in the first key column of the statistics object, excluding the histogram boundary values. This Density value is not used by the query optimizer and is displayed for backward compatibility with versions before SQL Server 2008.  
  
     **Average Key Length**  
     Average number of bytes per value for all of the key columns in the statistics object.  
  
     **String Index**  
     Yes indicates the statistics object contains string summary statistics to improve the cardinality estimates for query predicates that use the LIKE operator; for example, `WHERE ProductName LIKE '%Bike'`. String summary statistics are stored separately from the histogram and are created on the first key column of the statistics object when it is of type **char**, **varchar**, **nchar**, **nvarchar**, **varchar(max)**, **nvarchar(max)**, **text**, or **ntext**.  
  
     **Filter Expression**  
     Predicate for the subset of table rows included in the statistics object. NULL = non-filtered statistics.  
  
     **Unfiltered Rows**  
     Total number of rows in the table before applying the filter expression. If Filter Expression is NULL, Unfiltered Rows is equal to Rows.  
  
     The following information describes the columns returned in the result set for the Density Vector.  
  
     **All Density**  
     Density is 1 / *distinct values*. Results display density for each prefix of columns in the statistics object, one row per density. A distinct value is a distinct list of the column values per row and per columns prefix. For example, if the statistics object contains key columns (A, B, C), the results report the density of the distinct lists of values in each of these column prefixes: (A), (A,B), and (A, B, C). Using the prefix (A, B, C), each of these lists is a distinct value list: (3, 5, 6), (4, 4, 6), (4, 5, 6), (4, 5, 7). Using the prefix (A, B) the same column values have these distinct value lists: (3, 5), (4, 4), and (4, 5).  
  
     **Average Length**  
     Average length, in bytes, to store a list of the column values for the column prefix. For example, if the values in the list (3, 5, 6) each require 4 bytes the length is 12 bytes.  
  
     **Columns**  
     Names of columns in the prefix for which All density and Average length are displayed.  
  
     The following information describes the columns returned in the result set for the Histogram.  
  
     **RANGE_HI_KEY**  
     Upper bound column value for a histogram step. The column value is also called a key value.  
  
     **RANGE_ROWS**  
     Estimated number of rows whose column value falls within a histogram step, excluding the upper bound.  
  
     **EQ_ROWS**  
     Estimated number of rows whose column value equals the upper bound of the histogram step.  
  
     **DISTINCT_RANGE_ROWS**  
     Estimated number of rows with a distinct column value within a histogram step, excluding the upper bound.  
  
     **AVG_RANGE_ROWS**  
     Average number of rows with duplicate column values within a histogram step, excluding the upper bound (RANGE_ROWS / DISTINCT_RANGE_ROWS for DISTINCT_RANGE_ROWS > 0).  
  
7.  Click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To view statistics properties  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    -- The following example displays all statistics information for the AK_Address_rowguid index of the Person.Address table.   
    DBCC SHOW_STATISTICS ("Person.Address", AK_Address_rowguid);   
    GO  
    ```  
  
 For more information, see [DBCC SHOW_STATISTICS &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md).  
  
#### To find all of the statistics on a table or view  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;   
    GO  
    /*Gets the following information: name and ID of the statistics, whether the statistics were created automatically or by the user, whether the statistics were created with the NORECOMPUTE option, and whether the statistics have a filter and, if so, what that filter is.  
    */  
    SELECT name AS statistics_name  
        ,stats_id  
        ,auto_created  
        ,user_created  
        ,no_recompute  
        ,has_filter  
        ,filter_definition  
    -- using the sys.stats catalog view  
    FROM sys.stats  
    -- for the Sales.SpecialOffer table  
    WHERE object_id = OBJECT_ID('Sales.SpecialOffer');  
    GO  
    ```  
  
 For more information, see [sys.stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-stats-transact-sql.md).  
  
  
