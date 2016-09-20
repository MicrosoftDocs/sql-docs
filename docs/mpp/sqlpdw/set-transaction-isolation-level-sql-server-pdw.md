---
title: "SET TRANSACTION ISOLATION LEVEL (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7b52a2b2-be64-4a1e-b658-87c614498b4a
caps.latest.revision: 7
author: BarbKess
---
# SET TRANSACTION ISOLATION LEVEL (SQL Server PDW)
Controls the locking and row versioning behavior of SQL statements issued by a connection to SQL Server PDW.  
  
In this release, the isolation level is always READ UNCOMMITTED.  
  
For more information, see the [SET TRANSACTION ISOLATION LEVEL (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms173763(v=sql11)) documentation on MSDN.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED  
[ ; ]  
```  
  
## Arguments  
READ UNCOMMITTED  
Specifies that statements can read rows that have been modified by other transactions but not yet committed.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
