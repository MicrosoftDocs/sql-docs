---
title: "SET ANSI_DEFAULTS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 9d2048a8-a648-40f5-8886-bcab08364285
caps.latest.revision: 7
author: BarbKess
---
# SET ANSI_DEFAULTS (SQL Server PDW)
Controls a group of SQL Server PDWSQL settings that collectively specify some ISO standard behavior.  
  
In this release, ANSI_DEFAULTS is always ON.  
  
For more information, see the [SET ANSI_DEFAULTS (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188340(v=sql11)) documentation on MSDN.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET ANSI_DEFAULTS ON;  
```  
  
## Permissions  
Requires membership in the **public** role.  
  
## General Remarks  
ANSI_DEFAULTS is a server-side setting that the client does not modify.  
  
The SQL Server Native Client ODBC driver and SQL Server Native Client OLE DB Provider for SQL Server automatically set ANSI_DEFAULTS to ON when connecting.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
