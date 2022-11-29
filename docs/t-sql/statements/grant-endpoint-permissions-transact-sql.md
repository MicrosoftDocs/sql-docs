---
title: "GRANT Endpoint Permissions (Transact-SQL)"
description: GRANT Endpoint Permissions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/17/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "endpoints [SQL Server], permissions"
  - "granting permissions [SQL Server], endpoints"
  - "GRANT statement, endpoints"
  - "permissions [SQL Server], endpoints"
dev_langs:
  - "TSQL"
---
# GRANT Endpoint Permissions (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Grants permissions on an endpoint.  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
GRANT permission  [ ,...n ] ON ENDPOINT :: endpoint_name  
        TO < server_principal >  [ ,...n ]  
    [ WITH GRANT OPTION ]  
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
 Specifies a permission that can be granted on an endpoint. For a list of the permissions, see the Remarks section later in this topic.  
  
 ON ENDPOINT **::**_endpoint_name_  
 Specifies the endpoint on which the permission is being granted. The scope qualifier (**::**) is required.  
  
 TO \<server_principal>  
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to which the permission is being granted.  
  
 *SQL_Server_login*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
 *SQL_Server_login_from_Windows_login*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login created from a Windows login.  
  
 *SQL_Server_login_from_certificate*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login mapped to a certificate.  
  
 *SQL_Server_login_from_AsymKey*  
 Specifies the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login mapped to an asymmetric key.  
  
 WITH GRANT OPTION  
 Indicates that the principal will also be given the ability to grant the specified permission to other principals.  
  
 AS *SQL_Server_login*  
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login from which the principal executing this query derives its right to grant the permission.  
  
## Remarks  
 Permissions at the server scope can be granted only when the current database is **master**.  
  
 Information about endpoints is visible in the [sys.endpoints](../../relational-databases/system-catalog-views/sys-endpoints-transact-sql.md) catalog view. Information about server permissions is visible in the [sys.server_permissions](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md) catalog view, and information about server principals is visible in the [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md) catalog view.  
  
 An endpoint is a server-level securable. The most specific and limited permissions that can be granted on an endpoint are listed in the following table, together with the more general permissions that include them by implication.  
  
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
  
### A. Granting VIEW DEFINITION permission on an endpoint  
 The following example grants `VIEW DEFINITION` permission on endpoint `Mirror7` to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `ZArifin`.  
  
```sql  
USE master;  
GRANT VIEW DEFINITION ON ENDPOINT::Mirror7 TO ZArifin;  
GO  
```  
  
### B. Granting TAKE OWNERSHIP permission with the GRANT OPTION  
 The following example grants `TAKE OWNERSHIP` permission on endpoint `Shipping83` to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user `PKomosinski` with the `GRANT OPTION`.  
  
```sql  
USE master;  
GRANT TAKE OWNERSHIP ON ENDPOINT::Shipping83 TO PKomosinski   
    WITH GRANT OPTION;  
GO  
```  
  
## See Also  
 [DENY Endpoint Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-endpoint-permissions-transact-sql.md)   
 [REVOKE Endpoint Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-endpoint-permissions-transact-sql.md)   
 [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../t-sql/statements/create-endpoint-transact-sql.md)   
 [Endpoints Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/endpoints-catalog-views-transact-sql.md)   
 [sys.endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-endpoints-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  
