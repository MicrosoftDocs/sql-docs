---
title: "DBCC SHOW_STATISTICS (PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e25aa814-46c2-4cd6-b89f-854be05b7159
caps.latest.revision: 7
author: BarbKess
---
# DBCC SHOW_STATISTICS (PDW)
Displays the contents of query optimization statistics tables stored in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DBCC SHOW_STATISTICS (table_name ,target )   
    [ WITH {STAT_HEADER | DENSITY_VECTOR | HISTOGRAM } [ ,...n ] ]  
[;]  
```  
  
## Arguments  
*table_name*  
Name of the table that contains the statistics to display. The table cannot be an external table.  
  
*target*  
Name of the index or statistics object for which to display statistics. *Target* is enclosed in brackets, single quotes, double quotes, or no quotes.  
  
Different from SQL Server, *target* cannot be a column name.  
  
STAT_HEADER | DENSITY_VECTOR | HISTOGRAM  
Limits the result sets returned by the statement to the specified option or options. If no options are specified, all statistics information is returned.  
  
## Results  
Each of the three options has a result set.  
  
### STAT_HEADER  
The following table describes the columns returned in the result set when STAT_HEADER is specified.  
  
|Column name|Description|  
|---------------|---------------|  
|Name|Name of the statistics object.|  
|Updated|Date and time the statistics were last updated. The STATS_DATE function is an alternate way to retrieve this information.|  
|Rows|Total number of rows in the table or indexed view when the statistics were last updated.|  
|Rows Sampled|Total number of rows sampled for statistics calculations. If Rows Sampled < Rows, the displayed histogram and density results are estimates based on the sampled rows.|  
|Steps|Number of steps in the histogram. Each step spans a range of column values followed by an upper bound column value. The histogram steps are defined on the first key column in the statistics. The maximum number of steps is 200.|  
|Density|Calculated as 1 / *distinct values* for all values in the first key column of the statistics object, excluding the histogram boundary values. This Density value is not used by the query optimizer and is displayed for backward compatibility with versions before SQL Server 2008.|  
|Average Key Length|Average number of bytes per value for all of the key columns in the statistics object.|  
|String Index|Yes indicates the statistics object contains string summary statistics to improve the cardinality estimates for query predicates that use the LIKE operator; for example, `WHERE ProductName LIKE '%Bike'`. String summary statistics are stored separately from the histogram and are created on the first key column of the statistics object when it is of type **char**, **varchar**, **nchar**, **nvarchar**, **varchar(max)**, **nvarchar(max)**, **text**, or **ntext.**.|  
|Filter Expression|Predicate for the subset of table rows included in the statistics object. NULL = non-filtered statistics.|  
|Unfiltered Rows|Total number of rows in the table before applying the filter expression. If **Filter Expression** is NULL, **Unfiltered Rows** is equal to **Rows**.|  
  
### DENSITY_VECTOR  
The following table describes the columns returned in the result set when DENSITY_VECTOR is specified.  
  
|Column name|Description|  
|---------------|---------------|  
|All Density|Density is 1 / *distinct values*. Results display density for each prefix of columns in the statistics object, one row per density. A distinct value is a distinct list of the column values per row and per columns prefix. For example, if the statistics object contains key columns (A, B, C), the results report the density of the distinct lists of values in each of these column prefixes: (A), (A,B), and (A, B, C). Using the prefix (A, B, C), each of these lists is a distinct value list: (3, 5, 6), (4, 4, 6), (4, 5, 6), (4, 5, 7). Using the prefix (A, B) the same column values have these distinct value lists: (3, 5), (4, 4), and (4, 5)|  
|Average Length|Average length, in bytes, to store a list of the column values for the column prefix. For example, if the values in the list (3, 5, 6) each require 4 bytes the length is 12 bytes.|  
|Columns|Names of columns in the prefix for which All density and Average length are displayed.|  
  
### HISTOGRAM  
The following table describes the columns returned in the result set when the HISTOGRAM option is specified.  
  
|Column name|Description|  
|---------------|---------------|  
|RANGE_HI_KEY|Upper bound column value for a histogram step. The column value is also called a key value.|  
|RANGE_ROWS|Estimated number of rows whose column value falls within a histogram step, excluding the upper bound.|  
|EQ_ROWS|Estimated number of rows whose column value equals the upper bound of the histogram step.|  
|DISTINCT_RANGE_ROWS|Estimated number of rows with a distinct column value within a histogram step, excluding the upper bound.|  
|AVG_RANGE_ROWS|Average number of rows with duplicate column values within a histogram step, excluding the upper bound (RANGE_ROWS / DISTINCT_RANGE_ROWS for DISTINCT_RANGE_ROWS > 0).|  
  
## General Remarks  
  
### Histogram  
A histogram measures the frequency of occurrence for each distinct value in a data set. The query optimizer computes a histogram on the column values in the first key column of the statistics object, selecting the column values by statistically sampling the rows or by performing a full scan of all rows in the table or view. If the histogram is created from a sampled set of rows, the stored totals for number of rows and number of distinct values are estimates and do not need to be whole integers.  
  
To create the histogram, the query optimizer sorts the column values, computes the number of values that match each distinct column value and then aggregates the column values into a maximum of 200 contiguous histogram steps. Each step includes a range of column values followed by an upper bound column value. The range includes all possible column values between boundary values, excluding the boundary values themselves. The lowest of the sorted column values is the upper boundary value for the first histogram step.  
  
The following diagram shows a histogram with six steps. The area to the left of the first upper boundary value is the first step.  
  
![](../../mpp/sqlpdw/media/SQL_Histogram.gif "SQL_Histogram")  
  
For each histogram step:  
  
-   Bold line represents the upper boundary value (RANGE_HI_KEY) and the number of times it occurs (EQ_ROWS)  
  
-   Solid area left of RANGE_HI_KEY represents the range of column values and the average number of times each column value occurs (AVG_RANGE_ROWS). The AVG_RANGE_ROWS for the first histogram step is always 0.  
  
-   Dotted lines represent the sampled values used to estimate total number of distinct values in the range (DISTINCT_RANGE_ROWS) and total number of values in the range (RANGE_ROWS). The query optimizer uses RANGE_ROWS and DISTINCT_RANGE_ROWS to compute AVG_RANGE_ROWS and does not store the sampled values.  
  
The query optimizer defines the histogram steps according to their statistical significance. It uses a maximum difference algorithm to minimize the number of steps in the histogram while maximizing the difference between the boundary values. The maximum number of steps is 200. The number of histogram steps can be fewer than the number of distinct values, even for columns with fewer than 200 boundary points. For example, a column with 100 distinct values can have a histogram with fewer than 100 boundary points.  
  
### Density Vector  
The query optimizer uses densities to enhance cardinality estimates for queries that return multiple columns from the same table or indexed view. The density vector contains one density for each prefix of columns in the statistics object. For example, if a statistics object has the key columns CustomerId, ItemId, Price, density is calculated on each of the following column prefixes.  
  
|Column prefix|Density calculated on|  
|-----------------|-------------------------|  
|(CustomerId)|Rows with matching values for CustomerId|  
|(CustomerId, ItemId)|Rows with matching values for CustomerId and ItemId|  
|(CustomerId, ItemId, Price)|Rows with matching values for CustomerId, ItemId, and Price|  
  
## Permissions  
DBCC SHOW_STATISTICS requires SELECT permission on the table or membership in one of the following:  
  
-   sysadmin fixed server role  
  
-   db_owner fixed database role  
  
-   db_ddladmin fixed database role  
  
## Limitations and Restrictions  
DBCC SHOW_STATISTICS shows statistics stored in the Shell database at the Control node level. It does not show statistics that are auto-created by SQL Server on the Compute nodes. For more information, see [Understanding Query Optimization Statistics &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/understanding-query-optimization-statistics-sql-server-pdw.md).  
  
DBCC SHOW_STATISTICS is not supported on external tables.  
  
## Examples  
  
### A. Display the contents of one statistics object  
The following example displays the contents of the Customer_LastName statistics on the DimCustomer table.  
  
```  
--First, create a statistics object  
USE AdventureWorksPDW2012;  
GO  
CREATE STATISTICS Customer_LastName   
ON AdventureWorksPDW2012.dbo.DimCustomer (LastName);  
GO  
DBCC SHOW_STATISTICS ("dbo.DimCustomer",Customer_LastName);  
GO  
```  
  
The results show the header, the density vector, and part of the histogram.  
  
![DBCC SHOW_STATISTICS results](../../mpp/sqlpdw/media/APS_SQL_DBCCSHOW_STATISTICS.png "APS_SQL_DBCCSHOW_STATISTICS")  
  
### B. Specifying the HISTOGRAM option  
This limits the statistics information displayed for Customer_LastName to the HISTOGRAM data.  
  
```  
DBCC SHOW_STATISTICS ("dbo.DimCustomer",Customer_LastName) WITH HISTOGRAM;  
GO  
```  
  
## See Also  
[Understanding Query Optimization Statistics &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/understanding-query-optimization-statistics-sql-server-pdw.md)  
[CREATE STATISTICS &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-statistics-sql-server-pdw.md)  
  
