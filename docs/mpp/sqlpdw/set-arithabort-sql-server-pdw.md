---
title: "SET ARITHABORT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 42504e57-a9bc-4b72-ac77-5277c1aff975
caps.latest.revision: 8
author: BarbKess
---
# SET ARITHABORT (SQL Server PDW)
Terminates a SQL Server PDW query when an overflow or divide-by-zero error occurs during query execution.  
  
For more information, see the [SET ARITHABORT (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms190306(v=sql11)) documentation on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET ARITHABORT ON   
[ ; ]  
```  
  
## Permissions  
Requires membership in the **public** role.  
  
## General Remarks  
If SET ARITHABORT is ON and SET ANSI WARNINGS is ON, these error conditions cause the query to terminate. Both are always ON in SQL Server PDW, regardless of whether they have been set to ON. As a result, these error conditions will always cause the query to terminate.  
  
> [!WARNING]  
> The default ARITHABORT setting for SQL Server Data Tools for Visual Studio (SSDT) and SQL Server Management Studio is ON. Client applications setting ARITHABORT to OFF can receive different query plans making it difficult to troubleshoot poorly performing queries. That is, the same query can execute fast in management studio but slow in the application. When troubleshooting queries with SSDT or Management Studio always match the client ARITHABORT setting.  
  
## Permissions  
Requires membership in the **public** role.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
