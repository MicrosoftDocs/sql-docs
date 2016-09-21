---
title: "SESSION_USER (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f158ba63-7ea2-4d0d-924a-3158669b94a1
caps.latest.revision: 6
author: BarbKess
---
# SESSION_USER (SQL Server PDW)
SESSION_USER returns the user name of the current context in the current database.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SESSION_USER  
```  
  
## Return Types  
**nvarchar(128)**  
  
## Remarks  
This function takes no arguments. SESSION_USER can be used in queries.  
  
## Examples  
The following example returns the session user for the current session.  
  
```  
SELECT SESSION_USER;  
```  
  
