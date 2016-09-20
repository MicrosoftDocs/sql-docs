---
title: "sp_addrolemember (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: afed2e83-d085-4414-a4ca-85146fad64fb
caps.latest.revision: 7
author: BarbKess
---
# sp_addrolemember (SQL Server PDW)
Adds a database user or a database role to a database role in the current database.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
sp_addrolemember'role','security_account'  
```  
  
## Arguments  
'*role*'  
Is the name of the database role in the current database. *role* is a **sysname**, with no default.  
  
'*security_account*'  
Is the security account being added to the role. *security_account* is a **sysname**, with no default. *security_account* can be a database user or a database role.  
  
## Return Code Values  
0 (success) or 1 (failure)  
  
## Remarks  
A member added to a role by using sp_addrolemember inherits the permissions of the role. Always check that the login exists and has access to the database.  
  
A role cannot include itself as a member. Such "circular" definitions are not valid, even when membership is only indirectly implied by one or more intermediate memberships.  
  
sp_addrolemember cannot add a fixed database role, fixed server role, or dbo to a role. sp_addrolemember cannot be executed within a user-defined transaction.  
  
## Permissions  
Adding members to database user-defined roles requires one of the following:  
  
-   Membership in the db_securityadmin or db_owner fixed database role.  
  
-   Membership in the role that owns the role.  
  
-   **ALTER ANY ROLE** permission or **ALTER** permission on the role.  
  
Adding members to fixed database roles requires membership in the db_owner fixed database role.  
  
## Locking Behavior  
Requires a Shared lock on the database.  
  
## Examples  
  
### A. Adding a Windows login  
The following example adds the login `LoginMary` to the `AdventureWorks2008R2` database as user `UserMary`. The user `UserMary` is then added to the `Production` role.  
  
> [!NOTE]  
> Because the login `LoginMary` is known as the database user `UserMary` in the AdventureWorks2012 database, the user name `UserMary` must be specified. The statement will fail unless a `Mary5` login exists. Logins and users usually have the same name. This example uses different names to differentiate the actions affecting the login vs. the user.  
  
```  
USE AdventureWorks2008R2;  
GO  
CREATE USER UserMary FOR LOGIN LoginMary ;  
GO  
EXEC sp_addrolemember 'Production', 'UserMary'  
```  
  
### B. Adding a database user  
The following example adds the database user `UserMary` to the `Production` database role in the current database.  
  
```  
EXEC sp_addrolemember 'Production', 'UserMary'  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[sp_droprolemember &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sp-droprolemember-sql-server-pdw.md)  
[CREATE ROLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-role-sql-server-pdw.md)  
[ALTER ROLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/alter-role-sql-server-pdw.md)  
[DROP ROLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/drop-role-sql-server-pdw.md)  
  
