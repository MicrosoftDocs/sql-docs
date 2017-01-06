---
title: "DBCC DROPCLEANBUFFERS"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 473c64c8-d142-4323-bca5-c95a420b0a46
caps.latest.revision: 4
---
# DBCC DROPCLEANBUFFERS
Removes all clean buffers from the buffer pool.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions (SQL Server PDW)](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
```  
-- Syntax for SQL Server  
  
DBCC DROPCLEANBUFFERS [ WITH NO_INFOMSGS ]  
```  
  
```  
-- Syntax for SQL Data Warehouse and Parallel Data Warehouse  
  
DBCC DROPCLEANBUFFERS ( COMPUTE | ALL ) [ WITH NO_INFOMSGS ]  
```  
  
## Arguments  
**WITH NO_INFOMSGS**  
Suppresses all informational messages. Informational messages are always suppressed on SQL Data Warehouse and SQL Server PDW.  
  
**COMPUTE**  
Purge the query plan cache from each Compute node.  
  
**ALL**  
Purge the query plan cache from each Compute node and from the Control node. This is the default if you do not specify a value.  
  
## Remarks  
Use DBCC DROPCLEANBUFFERS to test queries with a cold buffer cache without shutting down and restarting the server.  
  
To drop clean buffers from the buffer pool, first use CHECKPOINT to produce a cold buffer cache. This forces all dirty pages for the current database to be written to disk and cleans the buffers. After you do this, you can issue DBCC DROPCLEANBUFFERS command to remove all buffers from the buffer pool.  
  
## Result Sets  
DBCC DROPCLEANBUFFERS on SQL Server returns:  
  
```  
DBCC execution completed. If DBCC printed error messages, contact your system administrator.  
```  
  
## Permissions  
Requires membership in the **sysadmin** fixed server role on SQL Server.  
  
Requires **CONTROL** permission on SQL Data Warehouse.  
  
Requires **ALTER SERVER STATE** permission on SQL Server PDW.  
  
## See Also  
[Database Console Commands(SQL Server PDW)](../sqlpdw/database-console-commands-sql-server-pdw.md)  
