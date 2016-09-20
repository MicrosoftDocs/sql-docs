---
title: "Grant Permissions to Manage Logins, Users, and Database Roles (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c327876a-726b-4406-85f1-4a40e38d43df
caps.latest.revision: 14
author: BarbKess
---
# Grant Permissions to Manage Logins, Users, and Database Roles (SQL Server PDW)
This topic describes how to grant permissions to manage logins, database users, and database roles.  
  
## <a name="PermsAdminConsole"></a>Grant Permissions to Manage Logins  
**Add or Manage Logins**  
  
The following SQL statements create a Login named KimAbercrombie  that can create new logins by using the [CREATE LOGIN &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-login-sql-server-pdw.md) statement and alter existing logins by using the [ALTER LOGIN &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/alter-login-sql-server-pdw.md) statement.  
  
The **ALTER ANY LOGIN** permission grants the ability to create new logins and drop exisiting. Once a login exists, the login can be managed by logins with the **ALTER ANY LOGIN** permission or the **ALTER** permission on that login. A login can change the password and default database for its own login.  
  
```Transact-SQL  
CREATE LOGIN KimAbercrombie   
WITH PASSWORD = 'A2c3456$#' MUST_CHANGE,  
CHECK_EXPIRATION = ON,  
CHECK_POLICY = ON;  
GO  
  
GRANT ALTER ANY LOGIN TO KimAbercrombie;  
```  
  
## Grant Permissions to Manage Login Sessions  
To have the ability to view all sessions on the server requires the **VIEW SERVER STATE** permission. The ability to terminate the sessions of other logins requires the **ALTER ANY CONNECTION** permission. The following example uses the `KimAbercrombie` login created earlier.  
  
```  
-- Grant permissions to view sessions and queries  
GRANT VIEW SERVER STATE TO KimAbercrombie;  
  
-- Grant permission to end sessions  
GRANT ALTER ANY CONNECTION TO KimAbercrombie;  
```  
  
## Grant Permission to Manage Database Users  
Creating and dropping database users requires the **ALTER ANY USER** permission. Managing existing users requires the **ALTER ANY USER** permission or the **ALTER** permission on that user. The following example uses the `KimAbercrombie` login created earlier.  
  
```  
-- Create a user  
USE AdventureWorksPDW2012;  
GO  
CREATE USER KimAbercrombie;  
  
-- Grant permissions to create and drop users   
GRANT ALTER ANY USER TO KimAbercrombie;  
```  
  
## Grant Permisson to Manage Database Roles  
Create and dropping user-defined database roles requires the **ALTER ANY ROLE** permission. The following example uses the `KimAbercrombie` login and use created earlier.  
  
```  
USE AdventureWorksPDW2012;  
GO  
-- Grant permissions to create and drop roles  
GRANT ALTER ANY ROLE TO KimAbercrombie;  
```  
  
## Login, User, and Role Permission Charts  
The following charts can be confusing, but they show how higher lever permissions (such as CONTROL) include more granular permissions that can be granted separately (such as ALTER). It is a best practice to always grant the least amount of permissions for someone to complete their necessary tasks. To do that, grant more specific permissions, instead of the top level permissions.  
  
**Login permissions:**  
  
![APS security login permissions](../../mpp/sqlpdw/media/APS_security_login_perms.png "APS_security_login_perms")  
  
**User permissions:**  
  
![APS security user permissions](../../mpp/sqlpdw/media/APS_security_user_perms.png "APS_security_user_perms")  
  
**Role permissions:**  
  
![APS security role permissions](../../mpp/sqlpdw/media/APS_security_role_perms.png "APS_security_role_perms")  
  
For a list of all permissions, see [Permissions: GRANT, DENY, REVOKE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/permissions-grant-deny-revoke-sql-server-pdw.md).  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[CREATE LOGIN &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-login-sql-server-pdw.md)  
[CREATE USER &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-user-sql-server-pdw.md)  
[CREATE ROLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-role-sql-server-pdw.md)  
[Permissions: GRANT, DENY, REVOKE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/permissions-grant-deny-revoke-sql-server-pdw.md)  
  
