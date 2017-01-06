---
title: "DROP ROLE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f700fc73-9b8a-4194-b47b-823fd641f5e3
caps.latest.revision: 8
author: BarbKess
---
# DROP ROLE (SQL Server PDW)
Removes a role from the database in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DROP ROLE role_name  
```  
  
## Arguments  
*role_name*  
Specifies the role to be dropped from the database.  
  
## Remarks  
Roles that own securables cannot be dropped from the database. To drop a database role that owns securables, you must first transfer ownership of those securables or drop them from the database. Roles that have members cannot be dropped from the database. To drop a role that has members, you must first remove members of the role.  
  
You cannot use DROP ROLE to drop a fixed database role.  
  
Information about role membership can be viewed in the sys.database_role_members catalog view.  
  
## Permissions  
Requires **ALTER ANY ROLE** permission on the database, or **CONTOL** permission on the role, or membership in the **db_securityadmin**.  
  
## Examples  
The following example drops the database role `purchasing` from `AdventureWorks2008R2`.  
  
```  
USE AdventureWorks2008R2;  
DROP ROLE purchasing;  
GO  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE ROLE &#40;SQL Server PDW&#41;](../sqlpdw/create-role-sql-server-pdw.md)  
[ALTER ROLE &#40;SQL Server PDW&#41;](../sqlpdw/alter-role-sql-server-pdw.md)  
[sp_addrolemember &#40;SQL Server PDW&#41;](../sqlpdw/sp-addrolemember-sql-server-pdw.md)  
[sp_droprolemember &#40;SQL Server PDW&#41;](../sqlpdw/sp-droprolemember-sql-server-pdw.md)  
[sys.database_principals &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-principals-sql-server-pdw.md)  
[sys.database_permissions &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-permissions-sql-server-pdw.md)  
[sys.database_role_members &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-role-members-sql-server-pdw.md)  
  
