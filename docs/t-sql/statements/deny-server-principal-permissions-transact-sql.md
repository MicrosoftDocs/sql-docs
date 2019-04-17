---
title: "DENY Server Principal Permissions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/09/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DENY statement, impersonate"
  - "permissions [SQL Server], impersonate"
  - "impersonate [SQL Server], denying"
  - "DENY statement, logins"
  - "permissions [SQL Server], logins"
  - "denying permissions [SQL Server], logins"
  - "servers [SQL Server], permissions"
  - "logins [SQL Server], denying access"
ms.assetid: 859affa7-0567-47d1-9490-57c1abbd619b
author: VanMSFT
ms.author: vanto
manager: craigg
---
# DENY Server Principal Permissions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Denies permissions granted on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
DENY permission [ ,...n ] }   
    ON   
    { [ LOGIN :: SQL_Server_login ]  
      | [ SERVER ROLE :: server_role ] }   
    TO <server_principal> [ ,...n ]  
    [ CASCADE ]  
    [ AS SQL_Server_login ]   
  
<server_principal> ::=   
    SQL_Server_login  
    | SQL_Server_login_from_Windows_login   
    | SQL_Server_login_from_certificate   
    | SQL_Server_login_from_AsymKey   
    | server_role  
```  
  
## Arguments  
 *permission*  
 Specifies a permission that can be denies on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. For a list of the permissions, see the Remarks section later in this topic.  
  
 LOGIN **::** *SQL_Server_login*  
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login on which the permission is being denied. The scope qualifier (**::**) is required.  
  
 SERVER ROLE **::** *server_role*  
 Specifies the server role on which the permission is being denied. The scope qualifier (**::**) is required.  
  
 TO \<server_principal>  
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or server role to which the permission is being granted.  
  
 TO *SQL_Server_login*  
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to which the permission is being denied.  
  
 *SQL_Server_login*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
 *SQL_Server_login_from_Windows_login*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login created from a Windows login.  
  
 *SQL_Server_login_from_certificate*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login mapped to a certificate.  
  
 *SQL_Server_login_from_AsymKey*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login mapped to an asymmetric key.  
  
 *server_role*  
 Specifies the name of a server role.  
  
 CASCADE  
 Indicates that the permission being denied is also denied to other principals to which it has been granted by this principal.  
  
 AS *SQL_Server_login*  
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login from which the principal executing this query derives its right to deny the permission.  
  
## Remarks  
 Permissions at the server scope can be denied only when the current database is master.  
  
 Information about server permissions is available in the [sys.server_permissions](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md) catalog view. Information about server principals is available in the [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md) catalog view.  
  
 The DENY statement fails if CASCADE is not specified when you are denying a permission to a principal that was granted that permission with GRANT OPTION.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins and server roles are server-level securables. The most specific and limited permissions that can be denied on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or server role are listed in the following table, together with the more general permissions that include them by implication.  
  
|SQL Server login or server role permission|Implied by SQL Server login or server role permission|Implied by server permission|  
|------------------------------------------------|-----------------------------------------------------------|----------------------------------|  
|CONTROL|CONTROL|CONTROL SERVER|  
|IMPERSONATE|CONTROL|CONTROL SERVER|  
|VIEW DEFINITION|CONTROL|VIEW ANY DEFINITION|  
|ALTER|CONTROL|ALTER ANY LOGIN<br /><br /> ALTER ANY SERVER ROLE|  
  
## Permissions  
 For logins, requires CONTROL permission on the login or ALTER ANY LOGIN permission on the server.  
  
 For server roles, requires CONTROL permission on the server role or ALTER ANY SERVER ROLE permission on the server.  
  
## Examples  
  
### A. Denying IMPERSONATE permission on a login  
 The following example denies `IMPERSONATE` permission on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `WanidaBenshoof` to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login created from the Windows user `AdvWorks\YoonM`.  
  
```  
USE master;  
DENY IMPERSONATE ON LOGIN::WanidaBenshoof TO [AdvWorks\YoonM];  
GO  
```  
  
### B. Denying VIEW DEFINITION permission with CASCADE  
 The following example denies `VIEW DEFINITION` permission on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `EricKurjan` to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `RMeyyappan`. The `CASCADE` option indicates that `VIEW DEFINITION` permission on `EricKurjan` will also be denied to principals to which `RMeyyappan` granted this permission.  
  
```  
USE master;  
DENY VIEW DEFINITION ON LOGIN::EricKurjan TO RMeyyappan   
    CASCADE;  
GO   
```  
  
### C. Denying VIEW DEFINITION permission on a server role  
 The following example denies `VIEW DEFINITION` on the `Sales` server role to the `Auditors` server role.  
  
```  
USE master;  
DENY VIEW DEFINITION ON SERVER ROLE::Sales TO Auditors ;  
GO   
```  
  
## See Also  
 [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)   
 [sys.server_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md)   
 [GRANT Server Principal Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-server-principal-permissions-transact-sql.md)   
 [REVOKE Server Principal Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-server-principal-permissions-transact-sql.md)   
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)   
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)  
  
  
