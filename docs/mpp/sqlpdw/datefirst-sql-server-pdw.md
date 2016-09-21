---
title: "@@DATEFIRST (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 2a081c87-1e84-431b-a227-d150f0ccc28a
caps.latest.revision: 6
author: BarbKess
---
# @@DATEFIRST (SQL Server PDW)
Returns the current value of SET DATEFIRST, which specifies the first day of the week. In this release, the return value is always 7, which is Sunday, the U.S. English default.  
  
For more information, see the [@@DATEFIRST (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187766) documentation on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
@@DATEFIRST  
```  
  
## Return Type  
**tinyint**  
  
## Permissions  
All logins, database users and database roles can access @@DATEFIRST.  
  
## Example  
  
```  
SELECT @@DATEFIRST;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
