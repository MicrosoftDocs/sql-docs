---
title: "SET CONCAT_NULL_YIELDS_NULL  (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6960850a-dbcc-4885-8d6a-6495b4940b97
caps.latest.revision: 7
author: BarbKess
---
# SET CONCAT_NULL_YIELDS_NULL  (SQL Server PDW)
Specifies that concatenating a string with a NULL value yields a NULL result in SQL Server PDW. CONCAT_NULL_YIELDS_NULL is always ON, and can only be set to ON. For example, `SELECT 'abc' + NULL` yields `NULL`. This is useful for backwards compatibility with SQL Server application.  
  
For more information, see [SET CONCAT_NULL_YIELDS_NULL (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms176056(v=sql11).aspx) on MSDN.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Transact-SQL Syntax Conventions](../Topic/Transact-SQL%20Syntax%20Conventions%20(Transact-SQL).md)  
  
## Syntax  
  
```  
SET CONCAT_NULL_YIELDS_NULL ON  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
