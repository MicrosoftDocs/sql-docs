---
title: "@@SPID (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 785474d2-a36c-49cd-b39e-34027a39a549
caps.latest.revision: 6
author: BarbKess
---
# @@SPID (SQL Server PDW)
Returns the server process ID (SPID) for SMP SQL Server running on the SQL Server PDW Control node.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
@@SPID  
```  
  
## Return Types  
**smallint**  
  
## General Remarks  
The @@SPID function does not return a SQL Server PDW[SESSION_ID &#40;SQL Server PDW&#41;](../sqlpdw/session-id-sql-server-pdw.md); it returns the server process ID (SPID) for SMP SQL Server running on the Control node. This behavior is slightly different from SMP SQL Server for which SESSION_ID(), @@SPID, and the SQL Server DMVs all refer to the same type of SQL Server session.  
  
The SPID in SQL Server is not guaranteed to be unique and can be re-used from closed sessions. For more information, see the [@@SPID (Transact-SQL)](http://technet.microsoft.com/en-us/library/ms189535(v=sql.120).aspx) documentation on Technet.  
  
## Metadata  
These are examples of queries that use @@SPID in SQL Server PDW.  
  
```  
SELECT * FROM sys.dm_pdw_exec_connections WHERE sql_spid = @@SPID;  
SELECT * FROM sys.dm_pdw_exec_sessions WHERE sql_spid = @@SPID;  
SELECT * FROM sys.dm_pdw_exec_requests WHERE session_id = SESSION_ID();  
```  
  
## Examples  
This example returns the SQL Server PDW session ID, the SQL Server Control node session ID, login name, and user name for the current user process.  
  
```  
SELECT SESSION_ID() AS ID, @@SPID AS 'Control ID', SYSTEM_USER AS 'Login Name', USER AS 'User Name';  
```  
  
## See Also  
[SESSION_ID &#40;SQL Server PDW&#41;](../sqlpdw/session-id-sql-server-pdw.md)  
  
