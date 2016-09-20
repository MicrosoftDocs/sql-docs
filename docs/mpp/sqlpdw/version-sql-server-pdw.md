---
title: "@@Version (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 34dbd857-aad3-4050-b6a8-529757f18934
caps.latest.revision: 6
author: BarbKess
---
# @@Version (SQL Server PDW)
Returns version, processor architecture, build date, and operating system for the current installation of SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
@@VERSION  
```  
  
## Return Types  
**nvarchar**  
  
## General Remarks  
The @@VERSION results are returned as one nvarchar string.  
  
## Examples  
  
### A. Return the current version of SQL Server PDW  
  
```  
SELECT @@VERSION AS 'SQL Server PDW Version';  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
