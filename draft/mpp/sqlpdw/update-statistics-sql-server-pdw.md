---
title: "UPDATE STATISTICS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7c477b5f-fe51-4a38-a9ae-4c5a1e8a7053
caps.latest.revision: 35
author: BarbKess
---
# UPDATE STATISTICS (SQL Server PDW)
Updates query optimization statistics on a SQL Server PDW table. Use the UPDATE STATISTICS statement to improve query performance. For example, we recommend updating statistics after loads.  
  
For more information and best practices for updating statistics, see [Understanding Query Optimization Statistics &#40;SQL Server PDW&#41;](../sqlpdw/understanding-query-optimization-statistics-sql-server-pdw.md).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
UPDATE STATISTICS [ database_name . [ schema_name ] . |  schema_name . ] table_name   
    [ ( { statistics_name | index_name } ) ]  
    [ WITH   
      {  
            FULLSCAN   
          | SAMPLE number PERCENT   
       }  
    ]  
[;]  
```  
  
## Arguments  
[ *database_name* . [ *schema_name* ] . | *schema_name* . ] *table_name*  
The one-, two-, or three-part name of the table that contains the statistics to update.  
  
*statistics_name | index_name*  
The statistics to update. This can be specified as the name of a statistics object, or as the name of an index. Since all indexes have statistics, providing the index name easily indicates which statistics to update.  
  
If omitted, SQL Server PDW will update all statistics on the table.  
  
WITH FULLSCAN  
Update statistics by scanning all rows in the table. When omitted, SQL Server PDW uses sampling to create the statistics, and determines the sample size that is required to create a high quality query plan.  
  
For table data stored in SQL Server PDW (e.g., replicated tables and distributed tables), SQL Server determines the sample size.  
  
WITH SAMPLE *number* PERCENT  
Specifies the approximate percentage of rows in the table for the query optimizer to use when it updates statistics. For PERCENT, *number* can be from 0 through 100. The actual percentage of rows the query optimizer samples might not match the percentage specified. For example, the query optimizer scans all rows on a data page.  
  
SAMPLE cannot be used with the FULLSCAN option.  
  
When neither SAMPLE nor FULLSCAN is specified, the query optimizer uses sampled data and computes the sample size by default. For regular tables, SQL Server determines the sample size. For external tables, SQL Server PDW computes the sample size by using a similar method.  
  
SAMPLE is useful for special cases in which the query plan, based on default sampling, is not optimal. In most situations, it is not necessary to specify SAMPLE because the query optimizer already determines the sample size by default..  
  
We recommend against specifying 0 PERCENT. When 0 PERCENT is specified, the statistics object is created but does not contain statistics data.  
  
## Permissions  
Requires **ALTER** permission on the table, or the user must be the owner of the table, or the user must be a member of the **db_ddladmin** fixed database role.  
  
## Error Handling  
If UPDATE STATISTICS returns an error, re-run the command to ensure that statistics are updated on all Compute nodes.  
  
## General Remarks  
For a distributed table, updating statistics encompasses updating SQL Server statistics on the Compute nodes, and then updating the corresponding merged MPP statistics on the Control node. For more information, see [Understanding Query Optimization Statistics &#40;SQL Server PDW&#41;](../sqlpdw/understanding-query-optimization-statistics-sql-server-pdw.md).  
  
## Limitations and Restrictions  
Updating statistics on external tables is not supported. To update external table statistics, drop the statistics with [DROP STATISTICS &#40;SQL Server PDW&#41;](../sqlpdw/drop-statistics-sql-server-pdw.md)and then re-create the statistics with [CREATE STATISTICS &#40;SQL Server PDW&#41;](../sqlpdw/create-statistics-sql-server-pdw.md).  
  
## Examples  
  
### A. Update statistics on a table  
The following example updates the `CustomerStats1` statistics on the `Customer` table.  
  
```  
UPDATE STATISTICS Customer ( CustomerStats1 );  
```  
  
### B. Update statistics by using a full scan  
The following example updates the `CustomerStats1` statistics, based on scanning all of the rows in the `Customer` table.  
  
```  
UPDATE STATISTICS Customer (CustomerStats1) WITH FULLSCAN;  
```  
  
### C. Update statistics by sampling the rows  
The following example updates the `CustomerStats1` statistics, based on sampling 50 percent of the rows in the `Customer` table.  
  
```  
UPDATE STATISTICS Customer (CustomerStats1) WITH SAMPLE 50 PERCENT;  
```  
  
### D. Update all statistics on a table  
The following example updates all statistics on the `Customer` table.  
  
```  
UPDATE STATISTICS Customer;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Query &#40;SQL Server PDW&#41;](../sqlpdw/query-sql-server-pdw.md)  
[CREATE STATISTICS &#40;SQL Server PDW&#41;](../sqlpdw/create-statistics-sql-server-pdw.md)  
[DROP STATISTICS &#40;SQL Server PDW&#41;](../sqlpdw/drop-statistics-sql-server-pdw.md)  
  
