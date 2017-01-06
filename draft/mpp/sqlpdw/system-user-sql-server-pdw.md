---
title: "SYSTEM_USER (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 27bcaf5d-d709-43c8-88ee-85145e3f08bb
caps.latest.revision: 6
author: BarbKess
---
# SYSTEM_USER (SQL Server PDW)
Allows a system-supplied value for the current login to be inserted into a table.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SYSTEM_USER  
```  
  
## Return Types  
**nchar**  
  
## Remarks  
You can use the SYSTEM_USER function as any standard function.  
  
If the user name and login name are different, SYSTEM_USER returns the login name.  
  
SYSTEM_USER returns the name of the currently executing context.  
  
## Examples  
The following example returns the current value of `SYSTEM_USER`.  
  
```  
SELECT SYSTEM_USER;  
```  
  
