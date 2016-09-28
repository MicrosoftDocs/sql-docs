---
title: "CREATE ROLE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7834740c-76a8-4e07-8f1f-b82fb3a9ebb1
caps.latest.revision: 8
author: BarbKess
---
# CREATE ROLE (SQL Server PDW)
Creates a new database role in the SQL Server PDW current database.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CREATE ROLE role_name [ AUTHORIZATION owner_name ]  
```  
  
## Arguments  
*role_name*  
Is the name of the role to be created.  
  
AUTHORIZATION *owner_name*  
Is the database user or role that is to own the new role. If no user is specified, the role will be owned by the user that executes CREATE ROLE.  
  
## Remarks  
Roles are database-level securables. After you create a role, configure the database-level permissions of the role by using GRANT, DENY, and REVOKE. To add members to a database role, use the sp_addrolemember stored procedure. For more information, see [sys.database_principals &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-principals-sql-server-pdw.md).  
  
Database roles are visible in the sys.database_role_members and sys.database_principals catalog views.  
  
## Permissions  
Requires **CREATE ROLE** permission on the database or membership in the **db_securityadmin** fixed database role. When you use the **AUTHORIZATION** option, the following permissions are also required:  
  
-   To assign ownership of a role to another user, requires IMPERSONATE permission on that user.  
  
-   To assign ownership of a role to another role, requires membership in the recipient role or ALTER permission on that role.  
  
-   To assign ownership of a role to an application role, requires ALTER permission on the application role.  
  
## Examples  
  
### A. Creating a database role that is owned by a database user  
The following example creates the database role `buyers` that is owned by user `BenMiller`.  
  
```  
USE AdventureWorks2008R2;  
CREATE ROLE buyers AUTHORIZATION BenMiller;  
GO  
```  
  
### B. Creating a database role that is owned by a fixed database role  
The following example creates the database role `auditors` that is owned the `db_securityadmin` fixed database role.  
  
```  
USE AdventureWorks2008R2;  
CREATE ROLE auditors AUTHORIZATION db_securityadmin;  
GO  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[ALTER ROLE &#40;SQL Server PDW&#41;](../sqlpdw/alter-role-sql-server-pdw.md)  
[DROP ROLE &#40;SQL Server PDW&#41;](../sqlpdw/drop-role-sql-server-pdw.md)  
[sp_addrolemember &#40;SQL Server PDW&#41;](../sqlpdw/sp-addrolemember-sql-server-pdw.md)  
[sp_droprolemember &#40;SQL Server PDW&#41;](../sqlpdw/sp-droprolemember-sql-server-pdw.md)  
[sys.database_role_members &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-role-members-sql-server-pdw.md)  
[sys.database_principals &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-principals-sql-server-pdw.md)  
  
