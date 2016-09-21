---
title: "Fixed Server Roles (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 8831a666-2718-485d-a1b0-0c545755dfb7
caps.latest.revision: 6
author: BarbKess
---
# Fixed Server Roles (SQL Server PDW)
Fixed server roles are created automatically by SQL Server. SQL Server PDW has a limited implementation of SQL Server fixed server roles. Only the **sysadmin** and **public** have user logins as members. The **setupadmin** and **dbcreator** roles are used internally by SQL Server PDW. Additional members cannot be added or removed from any role.  
  
## sysadmin Fixed Server Role  
Members of the **sysadmin** fixed server role can perform any activity in the server. The **sa** login is the only member of the **sysadmin** fixed server role. Additional logins cannot be added to the **sysadmin** fixed server role. Granting the **CONTROL SERVER** permission approximates membership in the **sysadmin** fixed server role. The following example grants the **CONTROL SERVER** permission to a login named Fay.  
  
```  
USE master;  
GO  
GRANT CONTROL SERVER TO Fay;  
```  
  
> [!IMPORTANT]  
> The **CONTROL SERVER** permission provides nearly complete control of SQL Server PDW. Whenever possible, provide more granular permissions to logins instead. For example, consider granting the **VIEW SERVER STATE**, **ALTER ANY LOGIN**, **VIEW ANY DATABASE**, or **CREATE ANY DATABASE** permissions.  
  
## public Server Role  
Every login that can connect to SQL Server PDW is a member of the **public** server role. All logins inherit the permissions granted to **public** on any object. Only assign **public** permissions on an object when you want the object to be available to all users. You cannot change membership in the **public** role.  
  
> [!NOTE]  
> **public** is implemented differently than other roles. Because all server principals are members of public, the membership of the **public** role is not listed in the **sys.server_role_members** DMV.  
  
## Fixed Server Roles vs. Granting Permissions  
The system of fixed server roles and fixed database roles is a legacy system originated in the 1980's. Fixed roles are still supported and are useful in environments where there are few users and the security needs are simple. Beginning with SQL Server 2005, a more detailed system of granting permission was created. This new system is more granular, providing many more options for both granting and denying permissions. The extra complexity of the more granular system makes it harder to learn, but most enterprise systems should grant permissions instead of using the fixed roles. The permissions are discussed and listed in the topic [Permissions: GRANT, DENY, REVOKE &#40;SQL Server PDW&#41;](../sqlpdw/permissions-grant-deny-revoke-sql-server-pdw.md).  
  
## See Also  
[Permissions: GRANT, DENY, REVOKE &#40;SQL Server PDW&#41;](../sqlpdw/permissions-grant-deny-revoke-sql-server-pdw.md)  
[PDW Permissions &#40;SQL Server PDW&#41;](../sqlpdw/pdw-permissions-sql-server-pdw.md)  
  
