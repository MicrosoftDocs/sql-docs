---
title: "DROP LOGIN (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 4a8441f3-ee61-4684-a04f-7d609b5ceb22
caps.latest.revision: 28
author: BarbKess
---
# DROP LOGIN (SQL Server PDW)
Drops or deletes a login in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DROP LOGIN login_name  
[;]  
```  
  
## Arguments  
*login_name*  
The name of the login to drop or delete.  
  
## Permissions  
Requires ALTER ANY LOGIN permission. A login cannot be dropped while it is logged in.  
  
## Limitations and Restrictions  
To drop a login that owns one or more securables, you must first delete the securables or use the [ALTER AUTHORIZATION &#40;SQL Server PDW&#41;](../sqlpdw/alter-authorization-sql-server-pdw.md) statement to change the securable owner to a different login.  
  
## Examples  
  
### A. Dropping a login  
The following example removes database login `login7` from the appliance.  
  
```  
DROP LOGIN login7;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE LOGIN &#40;SQL Server PDW&#41;](../sqlpdw/create-login-sql-server-pdw.md)  
[ALTER LOGIN &#40;SQL Server PDW&#41;](../sqlpdw/alter-login-sql-server-pdw.md)  
  
