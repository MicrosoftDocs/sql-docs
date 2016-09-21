---
title: "SESSION_ID (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 86661bdb-3f87-4d9c-a554-04ce757c0e50
caps.latest.revision: 14
author: BarbKess
---
# SESSION_ID (SQL Server PDW)
Returns the ID of the current SQL Server PDW session.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SESSION_ID ()  
```  
  
## Return Value  
Returns an **nvarchar(32)** value.  
  
## General Remarks  
The session ID is assigned to each user connection when the connection is made. It persists for the duration of the connection. When the connection ends, the session ID is released.  
  
The session ID begins with the alphabetical characters 'SID'. These are case-sensitive and must be capitalized when session ID is used in SQL commands.  
  
You can query the view [sys.dm_pdw_exec_sessions](../sqlpdw/sys-dm-pdw-exec-sessions-sql-server-pdw.md) to retrieve the same information as this function.  
  
## Examples  
The following example returns the current session ID.  
  
```  
SELECT SESSION_ID();  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[DB_NAME &#40;SQL Server PDW&#41;](../sqlpdw/db-name-sql-server-pdw.md)  
[VERSION &#40;SQLServer PDW&#41;](../sqlpdw/version-sqlserver-pdw.md)  
  
