---
title: "GRANT Server Principal Permissions (Transact-SQL) | Microsoft Docs"
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
  - "impersonate [SQL Server], granting"
  - "granting permissions [SQL Server], logins"
  - "permissions [SQL Server], impersonate"
  - "permissions [SQL Server], logins"
  - "GRANT statement, impersonation"
  - "GRANT statement, logins"
  - "logins [SQL Server], granting access"
  - "granting permissions [SQL Server], impersonation"
ms.assetid: 4cbed281-5e1e-4d8b-b410-4c18a6cd0205
author: VanMSFT
ms.author: vanto
manager: craigg
---
# GRANT Server Principal Permissions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Grants permissions on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
GRANT permission [ ,...n ] }   
    ON   
    { [ LOGIN :: SQL_Server_login ]  
      | [ SERVER ROLE :: server_role ] }   
    TO <server_principal> [ ,...n ]  
    [ WITH GRANT OPTION ]  
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
 Specifies a permission that can be granted on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login. For a list of the permissions, see the Remarks section later in this topic.  
  
 LOGIN **::** *SQL_Server_login*  
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login on which the permission is being granted. The scope qualifier (**::**) is required.  
  
 SERVER ROLE **::** *server_role*  
 Specifies the user-defined server role on which the permission is being granted. The scope qualifier (**::**) is required.  
  
 TO \<server_principal> 
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or server role to which the permission is being granted.  
  
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
  
 WITH GRANT OPTION  
 Indicates that the principal will also be given the ability to grant the specified permission to other principals.  
  
 AS *SQL_Server_login*  
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login from which the principal executing this query derives its right to grant the permission.  
  
## Remarks  
 Permissions at the server scope can be granted only when the current database is master.  
  
 Information about server permissions is visible in the [sys.server_permissions](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md) catalog view. Information about server principals is visible in the [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md) catalog view.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins and server roles are server-level securables. The most specific and limited permissions that can be granted on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or server role are listed in the following table, together with the more general permissions that include them by implication.  
  
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
  
### A. Granting IMPERSONATE permission on a login  
 The following example grants `IMPERSONATE` permission on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `WanidaBenshoof` to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login created from the Windows user `AdvWorks\YoonM`.  
  
```  
USE master;  
GRANT IMPERSONATE ON LOGIN::WanidaBenshoof to [AdvWorks\YoonM];  
GO  
```  
  
### B. Granting VIEW DEFINITION permission with GRANT OPTION  
 The following example grants `VIEW DEFINITION` on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `EricKurjan` to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `RMeyyappan` with `GRANT OPTION`.  
  
```  
USE master;  
GRANT VIEW DEFINITION ON LOGIN::EricKurjan TO RMeyyappan   
    WITH GRANT OPTION;  
GO   
```  
  
### C. Granting VIEW DEFINITION permission on a server role  
 The following example grants `VIEW DEFINITION` on the `Sales` server role to the `Auditors` server role.  
  
```  
USE master;  
GRANT VIEW DEFINITION ON SERVER ROLE::Sales TO Auditors ;  
GO   
```  
  
## See Also  
 [sys.server_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)   
 [sys.server_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md)   
 [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)   
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)  
  
  

