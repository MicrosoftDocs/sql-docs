---
title: "USER (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 4fe59f5e-f1a2-4dae-a605-635974944b73
caps.latest.revision: 7
author: BarbKess
---
# USER (SQL Server PDW)
Allows a system-supplied value for the database user name of the current user to be inserted into a table when no default value is specified.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
USER  
```  
  
## Return Types  
**char**  
  
## Remarks  
USER provides the same functionality as the USER_NAME system function.  
  
Use USER with DEFAULT constraints in either the CREATE TABLE or ALTER TABLE statements, or use as any standard function.  
  
USER always returns the name of the current context. When called after an EXECUTE AS statement, USER returns the name of the impersonated context.  
  
If a Windows principal accesses the database by way of membership in a group, USER returns the name of the Windows principal instead of the name of the group.  
  
## Examples  
  
### A. Using USER to return the database user name  
The following example declares a variable as `char`, assigns the current value of USER to it, and then prints the variable with a text description.  
  
```  
DECLARE @usr char(30)  
SET @usr = user  
SELECT 'The current user''s database username is: '+ @usr  
GO  
```  
  
Here is the result set.  
  
<pre>-----------------------------------------------------------------------  
The current user's database username is: dbo  
(1 row(s) affected)</pre>  
  
## See Also  
[USER_NAME &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/user-name-sql-server-pdw.md)  
[SUSER_SNAME &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/suser-sname-sql-server-pdw.md)  
[HAS_DBACCESS &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/has-dbaccess-sql-server-pdw.md)  
[sys.database_principals &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-database-principals-sql-server-pdw.md)  
[sys.database_permissions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-database-permissions-sql-server-pdw.md)  
  
