---
title: "sp_unprepare (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 0da09506-34b6-4c7c-a225-c0fc9ed355bf
caps.latest.revision: 5
author: BarbKess
---
# sp_unprepare (SQL Server PDW)
Discards the execution plan created by the sp_prepare stored procedure. sp_unprepare is invoked by specifying ID = 15 in a tabular data stream (TDS) packet.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
sp_unprepare handle  
```  
  
## Arguments  
*handle*  
Is the *handle* value returned by sp_prepare.  
  
## Examples  
The following example prepares, executes, and unprepares a simple statement.  
  
```  
Declare @P1 int;  
Exec sp_prepare @P1 output,   
    N'@P1 nvarchar(128), @P2 nvarchar(100)',  
    N'SELECT database_id, name FROM sys.databases WHERE name=@P1 AND state_desc = @P2';  
Exec sp_execute @P1, N'tempdb', N'ONLINE';  
EXEC sp_unprepare @P1;  
```  
  
## See Also  
[Procedures &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/procedures-sql-server-pdw.md)  
[sp_prepare &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sp-prepare-sql-server-pdw.md)  
[sp_execute &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sp-execute-sql-server-pdw.md)  
  
