---
title: "DBCC PDW_SHOWPARTITIONSTATS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7a5b66ac-e18d-4b03-91fd-6fd28fe4f4f7
caps.latest.revision: 18
author: BarbKess
---
# DBCC PDW_SHOWPARTITIONSTATS (SQL Server PDW)
Displays the size and number of rows for each partition of a table in a SQL Server PDW database.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
Show the partition stats for a table  
DBCC PDW_SHOWPARTITIONSTATS ( " [ database_name . [ schema_name ] . ] | [ schema_name.] table_name  ")  
[;]  
```  
  
## Arguments  
[ *database_name* . [ *schema_name* ] . | *schema_name* . ] *table_name*  
The one, two, or three-part name of the table to be displayed.  For two or three-part table names, the name must be enclosed with double quotes (""). Using quotes around a one-part table name is optional.  
  
## Permissions  
Requires **VIEW SERVER STATE** permission.  
  
## Result Sets  
This is the results for the DBCC PDW_SHOWPARTITIONSTATS command.  
  
|Column Name|Data Type|Description|  
|---------------|-------------|---------------|  
|partition_number|int|Partition number.|  
|used_page_count|bigint|Number of pages used for the data.|  
|reserved_page_count|bigint|Number of pages allocated to the partition.|  
|row_count|bigint|Number of rows in the partition.|  
|pdw_node_id|int|Compute node for the data.|  
|distribution_id|int|Distribution id for the data.|  
  
## Examples  
  
### A. DBCC PDW_SHOWPARTITIONSTATS Basic Syntax Examples  
The following examples display the space used and number of rows by partition for the FactInternetSales table in the **AdventureWorksPDW2012** database.  
  
```  
DBCC PDW_SHOWPARTITIONSTATS ("AdventureWorksPDW2012.dbo.FactInternetSales");  
DBCC PDW_SHOWPARTITIONSTATS ("dbo.FactInternetSales");  
DBCC PDW_SHOWPARTITIONSTATS (FactInternetSales);  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
