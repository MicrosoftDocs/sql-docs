---
title: "REVOKE Availability Group Permissions"
titleSuffix: SQL Server (Transact-SQL)
description: Revoke permissions on an Always On availability group.
author: VanMSFT
ms.author: vanto
ms.date: "08/10/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "Availability Groups [SQL Server], permissions"
  - "REVOKE statement, availability groups"
  - "revoking permissions, [SQL Server], availability groups"
  - "permissions [SQL Server], availability group"
dev_langs:
  - "TSQL"
---
# REVOKE Availability Group Permissions (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Revokes permissions on an Always On availability group. 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
REVOKE [ GRANT OPTION FOR ] permission  [ ,...n ]   
    ON AVAILABILITY GROUP :: availability_group_name  
    { FROM | TO } < server_principal >  [ ,...n ]  
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
 Specifies a permission that can be revoked on an availability group. For a list of the permissions, see the Remarks section later in this topic.  
  
 ON AVAILABILITY GROUP **::**_availability_group_name_  
 Specifies the availability group on which the permission is being revoked. The scope qualifier (**::**) is required.  
  
 { FROM | TO } \<server_principal> 
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to which the permission is being revoked.  
  
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
  
> [!IMPORTANT]  
>  A cascaded revocation of a permission granted WITH GRANT OPTION will revoke both GRANT and DENY of that permission.  
  
 AS *SQL_Server_login*  
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login from which the principal executing this query derives its right to revoke the permission.  
  
## Remarks  
 Permissions at the server scope can be revoked only when the current database is **master**.  
  
 Information about availability groups is visible in the [sys.availability_groups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md) catalog view. Information about server permissions is visible in the [sys.server_permissions](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md) catalog view, and information about server principals is visible in the [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md) catalog view.  
  
 An availability group is a server-level securable. The most specific and limited permissions that can be revoked on an availability group are listed in the following table, together with the more general permissions that include them by implication.  
  
|Availability group permission|Implied by availability group permission|Implied by server permission|  
|-----------------------------------|----------------------------------------------|----------------------------------|  
|ALTER|CONTROL|ALTER ANY AVAILABILITY GROUP|  
|CONNECT|CONTROL|CONTROL SERVER|  
|CONTROL|CONTROL|CONTROL SERVER|  
|TAKE OWNERSHIP|CONTROL|CONTROL SERVER|  
|VIEW DEFINITION|CONTROL|VIEW ANY DEFINITION|  
  
## Permissions  
 Requires CONTROL permission on the availability group or ALTER ANY AVAILABILITY GROUP permission on the server.  
  
## Examples  
  
### A. Revoking VIEW DEFINITION permission on an availability group  
 The following example revokes `VIEW DEFINITION` permission on availability group `MyAg` to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `ZArifin`.  
  
```sql  
USE master;  
REVOKE VIEW DEFINITION ON AVAILABILITY GROUP::MyAg TO ZArifin;  
GO  
```  
  
### B. Revoking TAKE OWNERSHIP permission with the CASCADE  
 The following example revokes `TAKE OWNERSHIP` permission on availability group `MyAg` to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user `PKomosinski` and from all principals to which `PKomosinski` granted TAKE OWNERSHIP on MyAg.  
  
```sql  
USE master;  
REVOKE TAKE OWNERSHIP ON AVAILABILITY GROUP::MyAg TO PKomosinski   
    CASCADE;  
GO  
```  
  
### C. Revoking a previously granted WITH GRANT OPTION clause  
 If a permission was granted using the WITH GRANT OPTION, use REVOKE GRANT OPTION FOR ... to remove the WITH GRANT OPTION. The following example grants the permission and then removes the WITH GRANT portion of the permission.  
  
```sql  
USE master;  
GRANT CONTROL ON AVAILABILITY GROUP::MyAg TO PKomosinski   
    WITH GRANT OPTION;  
GO  
REVOKE GRANT OPTION FOR CONTROL ON AVAILABILITY GROUP::MyAg TO PKomosinski  
CASCADE  
GO  
```  
  
## See Also  
 [GRANT Availability Group Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-availability-group-permissions-transact-sql.md)   
 [DENY Availability Group Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-availability-group-permissions-transact-sql.md)   
 [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-availability-group-transact-sql.md)   
 [sys.availability_groups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md)   
 [Always On Availability Groups Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  

