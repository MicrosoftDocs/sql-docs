---
title: "CURRENT_TIMESTAMP"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 723e071f-dc98-4fe1-b743-17ed28372cdd
caps.latest.revision: 7
author: BarbKess
---
# CURRENT_TIMESTAMP
Returns the current database system timestamp as a **datetime** value without the database time zone offset. This value is derived from the Windows operating system running on the Control node.  
  
For more information, see [CURRENT_TIMESTAMP ( Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188751(v=sql11)) on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Transact-SQL Syntax Conventions](../Topic/Transact-SQL%20Syntax%20Conventions%20(Transact-SQL).md)  
  
## Syntax  
  
```  
CURRENT_TIMESTAMP  
```  
  
## Arguments  
Takes no arguments.  
  
## Return Type  
**datetime**  
  
## Permissions  
All database users have access to CURRENT_TIMESTAMP.  
  
## Examples  
  
```  
SELECT CURRENT_TIMESTAMP;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
