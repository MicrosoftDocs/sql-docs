---
title: "DENY Endpoint Permissions (Transact-SQL)"
description: DENY Endpoint Permissions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "05/15/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "endpoints [SQL Server], permissions"
  - "DENY statement, endpoints"
  - "denying permissions [SQL Server], endpoints"
  - "permissions [SQL Server], endpoints"
dev_langs:
  - "TSQL"
---
# DENY Endpoint Permissions (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Denies permissions on an endpoint.  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DENY permission  [ ,...n ] ON ENDPOINT :: endpoint_name  
    TO < server_principal >  [ ,...n ]  
    [ CASCADE ]  
    [ AS SQL_Server_login ]   
  
<server_principal> ::=   
        SQL_Server_login  
    | SQL_Server_login_from_Windows_login   
    | SQL_Server_login_from_certificate   
    | SQL_Server_login_from_AsymKey  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *permission*  
 Specifies a permission that can be denied on an endpoint. For a list of the permissions, see the Remarks section later in this topic.  
  
 ON ENDPOINT **::**_endpoint_name_  
 Specifies the endpoint on which the permission is being denied. The scope qualifier (**::**) is required.  
  
 TO \<server_principal>  
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to which the permission is being denied.  
  
 *SQL_Server_login*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
 *SQL_Server_login_from_Windows_login*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login created from a Windows login.  
  
 *SQL_Server_login_from_certificate*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login mapped to a certificate.  
  
 *SQL_Server_login_from_AsymKey*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login mapped to an asymmetric key.  
  
 CASCADE  
 Indicates that the permission being denied is also denied to other principals to which it has been granted by this principal.  
  
 AS *SQL_Server_login*  
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login from which the principal executing this query derives its right to deny the permission.  
  
## Remarks  
 Permissions at the server scope can be denied only when the current database is **master**.  
  
 Information about endpoints is visible in the [sys.endpoints](../../relational-databases/system-catalog-views/sys-endpoints-transact-sql.md) catalog view. Information about server permissions is visible in the [sys.server_permissions](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md) catalog view, and information about server principals is visible in the [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md) catalog view.  
  
 An endpoint is a server-level securable. The most specific and limited permissions that can be denied on an endpoint are listed in the following table, together with the more general permissions that include them by implication.  
  
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
  
### A. Denying VIEW DEFINITION permission on an endpoint  
 The following example denies `VIEW DEFINITION` permission on the endpoint `Mirror7` to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `ZArifin`.  
  
```sql  
USE master;  
DENY VIEW DEFINITION ON ENDPOINT::Mirror7 TO ZArifin;  
GO  
```  
  
### B. Denying TAKE OWNERSHIP permission with CASCADE option  
 The following example denies `TAKE OWNERSHIP` permission on the endpoint `Shipping83` to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user `PKomosinski` and to principals to which `PKomosinski` granted `TAKE OWNERSHIP`.  
  
```sql  
USE master;  
DENY TAKE OWNERSHIP ON ENDPOINT::Shipping83 TO PKomosinski   
    CASCADE;  
GO  
```  
  
## See Also  
 [GRANT Endpoint Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-endpoint-permissions-transact-sql.md)   
 [REVOKE Endpoint Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-endpoint-permissions-transact-sql.md)   
 [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/create-endpoint-transact-sql.md)   
 [Endpoints Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/endpoints-catalog-views-transact-sql.md)   
 [sys.endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-endpoints-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  
