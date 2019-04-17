---
title: "REVOKE Endpoint Permissions (Transact-SQL) | Microsoft Docs"
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
  - "endpoints [SQL Server], permissions"
  - "REVOKE statement, endpoints"
  - "permissions [SQL Server], endpoints"
ms.assetid: 826f513e-9ad0-46b9-87ad-7525713638c8
author: VanMSFT
ms.author: vanto
manager: craigg
---
# REVOKE Endpoint Permissions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Revokes permissions granted or denied on an endpoint.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
REVOKE [ GRANT OPTION FOR ] permission [ ,...n ]   
    ON ENDPOINT :: endpoint_name  
    { FROM | TO } <server_principal> [ ,...n ]  
    [ CASCADE ]  
    [ AS SQL_Server_login ]   
  
<server_principal> ::=   
        SQL_Server_login  
    | SQL_Server_login_from_Windows_login   
    | SQL_Server_login_from_certificate   
    | SQL_Server_login_from_AsymKey  
```  
  
## Arguments  
 *permission*  
 Specifies a permission that can be granted on an endpoint. For a list of the permissions, see the Remarks section later in this topic.  
  
 ON ENDPOINT **::**_endpoint_name_  
 Specifies the endpoint on which the permission is being granted. The scope qualifier (**::**) is required.  
  
 { FROM | TO } \<server_principal> 
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login from which the permission is being revoked.  
  
 *SQL_Server_login*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
 *SQL_Server_login_from_Windows_login*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login created from a Windows login.  
  
 *SQL_Server_login_from_certificate*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login mapped to a certificate.  
  
 *SQL_Server_login_from_AsymKey*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login mapped to an asymmetric key.  
  
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
 Permissions at the server scope can be revoked only when the current database is **master**.  
  
 Information about endpoints is visible in the [sys.endpoints](../../relational-databases/system-catalog-views/sys-endpoints-transact-sql.md) catalog view. Information about server permissions is visible in the [sys.server_permissions](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md) catalog view, and information about server principals is visible in the [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md) catalog view.  
  
 An endpoint is a server-level securable. The most specific and limited permissions that can be revoked on an endpoint are listed in the following table, together with the more general permissions that include them by implication.  
  
|Endpoint permission|Implied by endpoint permission|Implied by server permission|  
|-------------------------|------------------------------------|----------------------------------|  
|ALTER|CONTROL|ALTER ANY ENDPOINT|  
|CONNECT|CONTROL|CONTROL SERVER|  
|CONTROL|CONTROL|CONTROL SERVER|  
|TAKE OWNERSHIP|CONTROL|CONTROL SERVER|  
|VIEW DEFINITION|CONTROL|VIEW ANY DEFINITION|  
  
## Permissions  
 Requires CONTROL permission on the endpoint or ALTER ANY ENDPOINT permission on the server.  
  
## Examples  
  
### A. Revoking VIEW DEFINITION permission on an endpoint  
 The following example revokes `VIEW DEFINITION` permission on the endpoint `Mirror7` from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `ZArifin`.  
  
```  
USE master;  
REVOKE VIEW DEFINITION ON ENDPOINT::Mirror7 FROM ZArifin;  
GO  
```  
  
### B. Revoking TAKE OWNERSHIP permission with the CASCADE option  
 The following example revokes `TAKE OWNERSHIP` permission on the endpoint `Shipping83` from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user `PKomosinski` and from all principals to which `PKomosinski` granted `TAKE OWNERSHIP` on `Shipping83`.  
  
```  
USE master;  
REVOKE TAKE OWNERSHIP ON ENDPOINT::Shipping83 FROM PKomosinski   
    CASCADE;  
GO  
```  
  
## See Also  
 [GRANT Endpoint Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-endpoint-permissions-transact-sql.md)   
 [DENY Endpoint Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-endpoint-permissions-transact-sql.md)   
 [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/create-endpoint-transact-sql.md)   
 [Endpoints Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/endpoints-catalog-views-transact-sql.md)   
 [sys.endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-endpoints-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  

