---
title: "STATS_DATE (PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 9ab41fe1-0501-4d2d-97a9-4fc38ed4e2fd
caps.latest.revision: 6
author: BarbKess
---
# STATS_DATE (PDW)
Returns the date of the most recent update for query optimization statistics in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
STATS_DATE (object_id ,stats_id )  
```  
  
## Arguments  
*object_id*  
ID of the table with the statistics.  
  
*stats_id*  
ID of the statistics object.  
  
## Return Types  
Returns **datetime** on success. Returns **NULL** on error.  
  
## Permissions  
Requires membership in the db_owner fixed database role or permission to view the metadata for the table.  
  
## General Remarks  
The STATS_DATE function, and other system functions, can be used in a SELECT list, a WHERE clause, and anywhere an expression can be used.  
  
## Limitations and Restrictions  
The results include statistics stored in the Shell database at the Control node level. They do not show the date for statistics that are auto-created by SQL Server on the Compute nodes. For more information, see [Understanding Query Optimization Statistics &#40;SQL Server PDW&#41;](../sqlpdw/understanding-query-optimization-statistics-sql-server-pdw.md).  
  
## Examples  
  
### A. Learn when a named statistics was last updated  
The following example creates statistics on the LastName column of the DimCustomer table. It then runs a query to show the date of the statistics. Then it udpates the statistics and runs the query again to show the updated date.  
  
```  
--First, create a statistics object  
USE AdventureWorksPDW2012;  
GO  
CREATE STATISTICS Customer_LastName_Stats  
ON AdventureWorksPDW2012.dbo.DimCustomer (LastName)  
WITH SAMPLE 50 PERCENT;  
GO  
  
--Return the date when Customer_LastName_Stats was last updated  
USE AdventureWorksPDW2012;  
GO  
SELECT stats_id, name AS stats_name,   
    STATS_DATE(object_id, stats_id) AS statistics_date  
FROM sys.stats s  
WHERE s.object_id = OBJECT_ID('dbo.DimCustomer')  
    AND s.name = 'Customer_LastName_Stats';  
GO  
  
--Update Customer_LastName_Stats so it will have a different timestamp in the next query  
GO  
UPDATE STATISTICS dbo.dimCustomer (Customer_LastName_Stats);  
  
--Return the date when Customer_LastName_Stats was last updated.  
SELECT stats_id, name AS stats_name,   
    STATS_DATE(object_id, stats_id) AS statistics_date  
FROM sys.stats s  
WHERE s.object_id = OBJECT_ID('dbo.DimCustomer')  
    AND s.name = 'Customer_LastName_Stats';  
GO  
```  
  
### B. View the date of the last update for all statistics on a table  
This example returns the date for when each statistics object on the DimCustomer table was last updated.  
  
```  
--Return the dates all statistics on the table were last updated.  
SELECT stats_id, name AS stats_name,   
    STATS_DATE(object_id, stats_id) AS statistics_date  
FROM sys.stats s  
WHERE s.object_id = OBJECT_ID('dbo.DimCustomer');  
GO  
```  
  
If statistics correspond to an index, the *stats_id* value in the [sys.stats](../Topic/sys.stats%20(Transact-SQL).md) catalog view is the same as the *index_id* value in the [sys.indexes](../Topic/sys.indexes%20(Transact-SQL).md) catalog view, and the following query returns the same results as the preceding query. If statistics do not correspond to an index, they are in the sys.stats results but not in the sys.indexes results.  
  
```  
USE AdventureWorksPDW2012;  
GO  
SELECT name AS index_name,   
    STATS_DATE(object_id, index_id) AS statistics_update_date  
FROM sys.indexes   
WHERE object_id = OBJECT_ID('dbo.DimCustomer');  
GO  
```  
  
## See Also  
[Understanding Query Optimization Statistics &#40;SQL Server PDW&#41;](../sqlpdw/understanding-query-optimization-statistics-sql-server-pdw.md)  
[CREATE STATISTICS &#40;SQL Server PDW&#41;](../sqlpdw/create-statistics-sql-server-pdw.md)  
  
