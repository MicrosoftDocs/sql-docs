---
title: "sys.dm_pdw_hadoop_operations (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 4326c9ce-020d-4d07-8c51-e44c0c1b14d2
caps.latest.revision: 4
author: BarbKess
---
# sys.dm_pdw_hadoop_operations (SQL Server PDW)
Contains a row for each map-reduce job that is pushed down to Hadoop as part of running a SQL Server PDW query on an external Hadoop table. Each map-reduce job represents one of the predicates in the query. This is only used when predicate pushdown is enabled for queries on Hadoop external tables.  
  
For more information about predicate pushdown, see [Queries on External Tables With Predicate Pushdown &#40;SQL Server PDW&#41;](../sqlpdw/queries-on-external-tables-with-predicate-pushdown-sql-server-pdw.md).  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|request_id|**nvarchar(32)**|ID for this external Hadoop operation.|Same as ID in [sys.dm_pdw_exec_requests &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-requests-sql-server-pdw.md).|  
|step_index|**int**|Index of the query step that refers to this Hadoop operation.|Same as step_index in [sys.dm_pdw_request_steps &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-request-steps-sql-server-pdw.md).|  
|operation_type|**nvarchar(255)**|Describes the type of external operation.|'External Hadoop Operation'|  
|operation_name|**nvarchar(4000)**|The job ID for a map-reduce job. This is returned by Hadoop after SQL Server PDW submits the job.||  
|map_progress|**float**|The percentage of input data that has been consumed so far by the map job.|A floating point number between, and including, 0 and 100.|  
|reduce_progress|**int**|The percentage of the reduce job that has completed..|A floating point number between, and including, 0 and 100.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
