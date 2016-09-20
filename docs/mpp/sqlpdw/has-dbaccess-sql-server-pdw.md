---
title: "HAS_DBACCESS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 8f8f5333-5f30-4f5e-992d-2959c367f1aa
caps.latest.revision: 6
author: BarbKess
---
# HAS_DBACCESS (SQL Server PDW)
Returns information about whether the user has access to the specified database. For more information, see the [HAS_DBACCESS (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187718.aspx) documentation on MSDN.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
HAS_DBACCESS ('database_name')  
```  
  
## Arguments  
'*database_name*'  
The name of the database for which the user wants access information. *database_name* is **sysname**.  
  
## Return Types  
**int**  
  
## Remarks  
HAS_DBACCESS returns 1 if the user has access to the database, 0 if the user has no access to the database, and NULL if the database name is not valid.  
  
HAS_DBACCESS returns 0 if the database is offline or suspect.  
  
HAS_DBACCESS returns 0 if the database is in single-user mode and the database is in use by another user.  
  
## Permissions  
Requires membership in the public role.  
  
## Examples  
The following example tests whether current user has access to the `AdventureWorksPDW2012` database.  
  
```  
SELECT HAS_DBACCESS('AdventureWorksPDW2012');  
GO  
```  
  
