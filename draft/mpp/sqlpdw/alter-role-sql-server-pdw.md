---
title: "ALTER ROLE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 75062248-95a1-47b4-9731-13c182276ba2
caps.latest.revision: 8
author: BarbKess
---
# ALTER ROLE (SQL Server PDW)
Changes the name of a database role.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ALTER ROLE role_name WITH NAME =new_name  
```  
  
## Arguments  
*role_name*  
Is the name of the role to be changed.  
  
WITH NAME =*new_name*  
Specifies the new name of the role. This name must not already exist in the database.  
  
## Remarks  
Changing the name of a database role does not change ID number, owner, or permissions of the role.  
  
Database roles are visible in the sys.database_role_members and sys.database_principals catalog views.  
  
## Permissions  
Requires **ALTER ANY ROLE** permission on the database, or **ALTER** permission on the role, or membership in the **db_securityadmin**.  
  
## Examples  
The following example changes the name of role `buyers` to `purchasing`.  
  
```  
USE AdventureWorks2008R2;  
ALTER ROLE buyers WITH NAME = purchasing;  
GO  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE ROLE &#40;SQL Server PDW&#41;](../sqlpdw/create-role-sql-server-pdw.md)  
[DROP ROLE &#40;SQL Server PDW&#41;](../sqlpdw/drop-role-sql-server-pdw.md)  
[sp_addrolemember &#40;SQL Server PDW&#41;](../sqlpdw/sp-addrolemember-sql-server-pdw.md)  
[sp_droprolemember &#40;SQL Server PDW&#41;](../sqlpdw/sp-droprolemember-sql-server-pdw.md)  
[sys.database_principals &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-principals-sql-server-pdw.md)  
[sys.database_permissions &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-permissions-sql-server-pdw.md)  
[sys.database_role_members &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-role-members-sql-server-pdw.md)  
  
