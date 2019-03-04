---
title: "REVOKE Server Principal Permissions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/10/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "REVOKE statement, impersonation"
  - "permissions [SQL Server], impersonation"
  - "permissions [SQL Server], logins"
  - "impersonate [SQL Server], revoking"
  - "logins [SQL Server], revoking"
  - "REVOKE statement, logins"
ms.assetid: 75409024-f150-4326-af16-9d60e900df18
author: VanMSFT
ms.author: vanto
manager: craigg
---
# REVOKE Server Principal Permissions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Revokes permissions granted or denied on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
REVOKE [ GRANT OPTION FOR ] permission [ ,...n ] }   
    ON   
    { [ LOGIN :: SQL_Server_login ]  
      | [ SERVER ROLE :: server_role ] }   
    { FROM | TO } <server_principal> [ ,...n ]  
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
 Specifies a permission that can be revoked on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. For a list of the permissions, see the Remarks section later in this topic.  
  
 LOGIN **::** *SQL_Server_login*  
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login on which the permission is being revoked. The scope qualifier (**::**) is required.  
  
 SERVER ROLE **::** *server_role*  
 Specifies the server role on which the permission is being revoked. The scope qualifier (**::**) is required.  
  
 { FROM | TO } \<server_principal> 
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or server role from which the permission is being revoked.  
  
 *SQL_Server_login*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
 *SQL_Server_login_from_Windows_login*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login created from a Windows login.  
  
 *SQL_Server_login_from_certificate*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login mapped to a certificate.  
  
 *SQL_Server_login_from_AsymKey*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login mapped to an asymmetric key.  
  
 *server_role*  
 Specifies the name of a user-defined server role.  
  
 GRANT OPTION  
 Indicates that the right to grant the specified permission to other principals will be revoked. The permission itself will not be revoked.  
  
> [!IMPORTANT]  
>  If the principal has the specified permission without the GRANT option, the permission itself will be revoked.  
  
 CASCADE  
 Indicates that the permission being revoked is also revoked from other principals to which it has been granted or denied by this principal.  
  
> [!CAUTION]  
>  A cascaded revocation of a permission granted WITH GRANT OPTION will revoke both GRANT and DENY of that permission.  
  
 AS *SQL_Server_login*  
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login from which the principal executing this query derives its right to revoke the permission.  
  
## Remarks  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins and server roles are server-level securables. The most specific and limited permissions that can be revoked on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or server role are listed in the following table, together with the more general permissions that include them by implication.  
  
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
  
### A. Revoking IMPERSONATE permission on a login  
 The following example revokes `IMPERSONATE` permission on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `WanidaBenshoof` from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login created from the Windows user `AdvWorks\YoonM`.  
  
```  
USE master;  
REVOKE IMPERSONATE ON LOGIN::WanidaBenshoof FROM [AdvWorks\YoonM];  
GO  
```  
  
### B. Revoking VIEW DEFINITION permission with CASCADE  
 The following example revokes `VIEW DEFINITION` permission on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `EricKurjan` from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `RMeyyappan`. The `CASCADE` option indicates that `VIEW DEFINITION` permission on `EricKurjan` will also be revoked from the principals to which `RMeyyappan` granted this permission.  
  
```  
USE master;  
REVOKE VIEW DEFINITION ON LOGIN::EricKurjan FROM RMeyyappan   
    CASCADE;  
GO   
```  
  
### C. Revoking VIEW DEFINITION permission on a server role  
 The following example revokes `VIEW DEFINITION` on the `Sales` server role to the `Auditors` server role.  
  
```  
USE master;  
REVOKE VIEW DEFINITION ON SERVER ROLE::Sales TO Auditors ;  
GO   
```  
  
## See Also  
 [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)   
 [sys.server_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md)   
 [GRANT Server Principal Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-server-principal-permissions-transact-sql.md)   
 [DENY Server Principal Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-server-principal-permissions-transact-sql.md)   
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)   
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)  
  
  

