---
title: "DBCC FREEPROCCACHE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a489f347-8998-4a17-97eb-57b0096a46b9
caps.latest.revision: 14
author: BarbKess
---
# DBCC FREEPROCCACHE (SQL Server PDW)
Removes all query plan cache entries for all databases from the Compute nodes. Use this statement to force SQL Server PDW to recompile all queries the next time they are run.  
  
For example, when an existing query plan was generated with out-of-date statistics, you can improve query performance by updating statistics and then recompiling the query. To force SQL Server PDW to recompile the query instead of using the existing query plan, you can use DBCC FREEPROCCACHE to remove the existing query plans. Upon the next execution of the query, SQL Server PDW will recompile the query with the latest statistics.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DBCC FREEPROCCACHE ( COMPUTE )  
     [ WITH NO_INFOMSGS ]   
[;]  
```  
  
## Arguments  
COMPUTE  
Purge the query plan cache from each Compute node. This is the only option available in this release.  
  
WITH NO_INFOMSGS  
Results do not contain informational messages.  
  
## Permissions  
Requires **ALTER SERVER STATE** permission.  
  
## General Remarks  
Multiple DBCC FREEPROCCACHE commands can be run concurrently.  
  
In SQL Server PDW, clearing the query plan cache can cause a temporary decrease in query performance when queries are recompiled. DBCC FREEPROCCACHE only causes SQL Server to recompile queries when they are run on the Compute nodes. It does not cause SQL Server PDW to recompile the parallel query plan that is generated on the Control node.  
  
DBCC FREEPROCCACHE can be cancelled during execution.  
  
## Error Handling  
Errors are reported in the [sys.dm_pdw_errors &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-errors-sql-server-pdw.md) system view.  
  
## Limitations and Restrictions  
DBCC FREEPROCCACHE can not run within a transaction.  
  
DBCC FREEPROCCAHCE is not supported in an EXPLAIN statement.  
  
## Metadata  
A new row is added to the sys.pdw_exec_requests system view when DBCC FREEPROCCACHE is run.  
  
## Examples  
  
### A. DBCC FREEPROCCACHE Basic Syntax Examples  
The following example removes all existing query plan caches from the Compute nodes. Although the context is set to UserDbSales, the Compute node query plan caches for all databases will be removed. The WITH NO_INFOMSGS clause prevents informational messages from appearing in the results.  
  
```  
USE UserDbSales;  
DBCC FREEPROCCACHE (COMPUTE) WITH NO_INFOMSGS;  
```  
  
The following example has the same results as the previous example, except that informational messages will show in the results.  
  
```  
USE UserDbSales;  
DBCC FREEPROCCACHE (COMPUTE);  
```  
  
When informational messages are requested and the execution is successful, the query results will have one line per Compute node.  
  
### B. Granting Permission to run DBCC FREEPROCCACHE  
The following example gives the login David permission to run DBCC FREEPROCCACHE.  
  
```  
GRANT ALTER SERVER STATE TO David;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
