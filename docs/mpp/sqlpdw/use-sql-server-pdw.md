---
title: "USE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 158ec56b-b822-410f-a7c4-1a196d4f0e15
caps.latest.revision: 27
author: BarbKess
---
# USE (SQL Server PDW)
Specifies the database context in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
USE database_name   
[;]  
```  
  
## Arguments  
*database_name*  
The name of the database context to use.  
  
## Permissions  
Requires CONNECT permission in the target database.  
  
## General Remarks  
When a login connects to SQL Server PDW, it acquires the database context of its default database.  
  
## Locking  
Takes a shared lock on the database object.  
  
## Examples  
The following example changes the database context to the `AccountingDB` database.  
  
```  
USE AccountingDB;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
