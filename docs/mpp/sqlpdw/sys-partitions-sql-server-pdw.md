---
title: "sys.partitions (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c3116feb-564c-4ec4-8fe2-35f5baafde1e
caps.latest.revision: 9
author: BarbKess
---
# sys.partitions (SQL Server PDW)
Contains a row for each partition of all the tables, and most types of indexes in a SQL Server PDWdatabase. All tables and indexes contain at least one partition, whether or not they are explicitly partitioned.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|partition_id|**bigint**|id of the partition. Is unique within a database.|  
|object_id|**int**|id of the object to which this partition belongs. Every table or view is composed of at least one partition.|  
|index_id|**int**|id of the index within the object to which this partition belongs.|  
|partition_number|**int**|1-based partition number within the owning index or heap. For SQL Server PDW, the value of this column is 1.|  
|hobt_id|**bigint**|ID of the data heap or B-tree that contains the rows for this partition.|  
|rows|**bigint**|Approximate average number of rows in each table partition. To calculate this value, SQL Server PDW divides the number of rows in the table by the number of partitions in the table.<br /><br />SQL Server PDW uses statistics, which might be out-of-date, to determine the total number of rows. The statistics are from the most recent run of UPDATE STATISTICS on the table. If UPDATE STATISTICS has not been run on the table, the statistics won’t exist, and SQL Server PDW will use 1000 as the default total number of rows.<br /><br />To display the number of rows in each partition within each distribution, use [DBCC PDW_SHOWPARTITIONSTATS &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/dbcc-pdw-showpartitionstats-sql-server-pdw.md).|  
|data_compression|**int**|Indicates the state of compression for each partition:<br /><br />0 = NONE<br /><br />1 = ROW<br /><br />2 = PAGE<br /><br />3 = COLUMNSTORE|  
|data_compression_desc|**nvarchar(60)**|Indicates the state of compression for each partition. Possible values are NONE, ROW, and PAGE.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
