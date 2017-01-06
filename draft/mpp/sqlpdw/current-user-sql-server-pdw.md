---
title: "CURRENT_USER (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 550848d1-47c1-4443-b639-83602f624238
caps.latest.revision: 5
author: BarbKess
---
# CURRENT_USER (SQL Server PDW)
Returns the name of the current user. This function is equivalent to USER_NAME().  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CURRENT_USER  
```  
  
## Return Types  
**sysname**  
  
## Remarks  
CURRENT_USER returns the name of the current security context.  
  
To return the login of the current user, see SYSTEM_USER.  
  
## Examples  
The following example returns the name of the current user.  
  
```  
SELECT CURRENT_USER;  
```  
  
