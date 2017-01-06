---
title: "CLOSE MASTER KEY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a19b8ae7-1cf3-4f14-8603-d0f6e120657f
caps.latest.revision: 6
author: BarbKess
---
# CLOSE MASTER KEY (SQL Server PDW)
Closes the master key of the current database. Must be executed in the master database.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CLOSE MASTER KEY  
```  
  
## Arguments  
Takes no arguments.  
  
## Remarks  
This statement reverses the operation performed by `OPEN MASTER KEY`. `CLOSE MASTER KEY` only succeeds when the database master key was opened in the current session by using the `OPEN MASTER KEY` statement.  
  
## Permissions  
Requires `CONTROL SERVER` permission to open the master key. The master key can be closed in the same session as it was opened without additional permissions.  
  
## Examples  
  
```  
USE master;  
OPEN MASTER KEY DECRYPTION BY PASSWORD = '43987hkhj4325tsku7';  
GO   
CLOSE MASTER KEY;  
GO  
```  
  
## See Also  
[OPEN MASTER KEY &#40;SQL Server PDW&#41;](../sqlpdw/open-master-key-sql-server-pdw.md)  
[CREATE MASTER KEY &#40;SQL Server PDW&#41;](../sqlpdw/create-master-key-sql-server-pdw.md)  
[ALTER MASTER KEY &#40;SQL Server PDW&#41;](../sqlpdw/alter-master-key-sql-server-pdw.md)  
[DROP MASTER KEY &#40;SQL Server PDW&#41;](../sqlpdw/drop-master-key-sql-server-pdw.md)  
[sys.databases &#40;SQL Server PDW&#41;](../sqlpdw/sys-databases-sql-server-pdw.md)  
  
