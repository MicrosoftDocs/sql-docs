---
title: "@@TRANCOUNT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: e6fd74b6-b64e-418c-8ea9-cb3734955dd0
caps.latest.revision: 4
author: BarbKess
---
# @@TRANCOUNT (SQL Server PDW)
@@TRANCOUNT returns the number of BEGIN TRANSACTION statements that have occurred on the current connection in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
@@TRANCOUNT  
```  
  
## Return Types  
**integer**  
  
## Remarks  
The BEGIN TRANSACTION statement increments @@TRANCOUNT by 1. ROLLBACK TRANSACTION decrements @@TRANCOUNT to 0. COMMIT TRANSACTION and COMMIT WORK decrement @@TRANCOUNT by 1.  
  
## General Remarks  
Since SQL Server PDW does not support nested transactions, @@TRANCOUNT is always 1 or 0.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
