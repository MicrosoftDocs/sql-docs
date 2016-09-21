---
title: "sp_droprolemember (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 16dfeaf1-1d53-4c55-abfa-0d884998a78c
caps.latest.revision: 8
author: BarbKess
---
# sp_droprolemember (SQL Server PDW)
Removes a security account from a SQL Server role in the current database.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
sp_droprolemember'role' , 'security_account'  
```  
  
## Arguments  
**'***role***'**  
Is the name of the role from which the member is being removed. *role* is **sysname**, with no default. *role* must exist in the current database.  
  
**'***security_account***'**  
Is the name of the security account being removed from the role. *security_account* is **sysname**, with no default. *security_account* can be a database user or another database role. *security_account* must exist in the current database.  
  
## Return Code Values  
0 (success) or 1 (failure)  
  
## Remarks  
**sp_droprolemember** removes a member from a database role by deleting a row from the **sysmembers** table. When a member is removed from a role the member loses any permissions it has by membership in that role.  
  
Users cannot be removed from the **public** role, and **dbo** cannot be removed from any role.  
  
**sp_droprolemember** cannot be executed within a user-defined transaction.  
  
Query [sys.database_role_members &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-role-members-sql-server-pdw.md) to see the members of a database role. Use **sp_addrolemember** to add a member to a role.  
  
## Permissions  
Requires ALTER permission on the role.  
  
## Locking Behavior  
Requires a Shared lock on the database.  
  
## Examples  
The following example removes the user `JonB` from the role `Sales`.  
  
```  
EXEC sp_droprolemember 'Sales', 'JonB'  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[sp_addrolemember &#40;SQL Server PDW&#41;](../sqlpdw/sp-addrolemember-sql-server-pdw.md)  
[DROP ROLE &#40;SQL Server PDW&#41;](../sqlpdw/drop-role-sql-server-pdw.md)  
  
