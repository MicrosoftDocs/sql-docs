---
title: "DBCC PDW_SHOWEXECUTIONPLAN (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d2483a28-0719-4447-a8e1-c629af77b751
caps.latest.revision: 13
author: BarbKess
---
# DBCC PDW_SHOWEXECUTIONPLAN (SQL Server PDW)
Displays the SQL Server execution plan for a query running on a specific SQL Server PDW Compute node or Control node. SQL Server PDW. Use this to troubleshoot query performance problems while queries are running on the Compute nodes and Control node.  
  
Once query performance problems are understood for SMP SQL Server queries running on the Compute nodes, there are several ways to improve performance. Possible ways to improve query performance on the Compute nodes include creating multi-column statistics, creating non-clustered indexes, or using query hints.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DBCC PDW_SHOWEXECUTIONPLAN ( pdw_node_id, spid )  
[;]  
```  
  
## Arguments  
*pdw_node_id*  
Identifier for the node that is running the query plan. This is an integer and cannot be NULL.  
  
*spid*  
Identifier for the SQL Server session that is running the query plan. This is an integer and cannot be NULL.  
  
## Permissions  
Requires VIEW-SERVER-STATE permission.  
  
## Examples  
  
### A. DBCC PDW_SHOWEXECUTIONPLAN Basic Syntax Examples  
The query that is running too long is either running a DMS query plan operation or a SQL query plan operation. For a list of the operations and their types, see [Understanding Query Plans &#40;SQL Server PDW&#41;](../sqlpdw/understanding-query-plans-sql-server-pdw.md).  
  
If the query is running a DMS query plan operation, you can use the following query to retrieve a list of the node IDs and session IDs for steps that are not complete.  
  
```  
SELECT [sql_spid], [pdw_node_id], [request_id], [dms_step_index], [type], [start_time], [end_time], [status]   
FROM sys.dm_pdw_dms_workers   
WHERE [status] <> 'StepComplete' and [status] <> 'StepError'  
AND pdw_node_id = 201001   
order by request_id, [dms_step_index], [distribution_id];  
```  
  
Based on the results of the preceding query, use the sql_spid and pdw_node_id as parameters to DBCC PDW_SHOWEXEUCTIONPLAN. For example, the following command shows the execution plan for pdw_node_id 201001 and sql_spid 375.  
  
```  
DBCC PDW_SHOWEXECUTIONPLAN ( 201001, 375 );  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
