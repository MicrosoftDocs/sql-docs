---
title: "SET XACT_ABORT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7280b6f1-7acf-4d92-a9d6-3482c0b6dae1
caps.latest.revision: 8
author: BarbKess
---
# SET XACT_ABORT (SQL Server PDW)
Specifies whether SQL Server PDW automatically rolls back the current transaction when a SQL statement raises a run-time error.  
  
In this release, SET XACT_ABORT is always ON.  
  
For more information, see the [SET XACT_ABORT (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188792(v=sql11).aspx) documentation on MSDN.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET XACT_ABORT ON  
```  
  
## General Remarks  
When SET XACT_ABORT is ON, if a SQL statement raises a run-time error, the entire transaction is terminated and rolled back.  
  
SET XACT_ABORT is set at execute or run time and not at parse time.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
