---
title: "GRANT Server Permissions (Transact-SQL)"
description: GRANT Server Permissions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "08/10/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "GRANT statement, servers"
  - "permissions [SQL Server], servers"
  - "servers [SQL Server], permissions"
  - "granting permissions [SQL Server], servers"
dev_langs:
  - "TSQL"
---
# GRANT Server Permissions (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Grants permissions on a server. 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
GRANT permission [ ,...n ]   
    TO <grantee_principal> [ ,...n ] [ WITH GRANT OPTION ]  
    [ AS <grantor_principal> ]  
  
<grantee_principal> ::= SQL_Server_login   
    | SQL_Server_login_mapped_to_Windows_login  
    | SQL_Server_login_mapped_to_Windows_group  
    | SQL_Server_login_mapped_to_certificate  
    | SQL_Server_login_mapped_to_asymmetric_key  
    | server_role  
  
<grantor_principal> ::= SQL_Server_login   
    | SQL_Server_login_mapped_to_Windows_login  
    | SQL_Server_login_mapped_to_Windows_group  
    | SQL_Server_login_mapped_to_certificate  
    | SQL_Server_login_mapped_to_asymmetric_key  
    | server_role  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *permission*  
 Specifies a permission that can be granted on a server. For a list of the permissions, see the Remarks section later in this topic.  
  
 TO \<grantee_principal> 
 Specifies the principal to which the permission is being granted.  
  
 AS \<grantor_principal> 
 Specifies the principal from which the principal executing this query derives its right to grant the permission.  
  
 WITH GRANT OPTION  
 Indicates that the principal will also be given the ability to grant the specified permission to other principals.  
  
 *SQL_Server_login*  
 Specifies a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
 *SQL_Server_login_mapped_to_Windows_login*  
 Specifies a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login mapped to a Windows login.  
  
 *SQL_Server_login_mapped_to_Windows_group*  
 Specifies a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login mapped to a Windows group.  
  
 *SQL_Server_login_mapped_to_certificate*  
 Specifies a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login mapped to a certificate.  
  
 *SQL_Server_login_mapped_to_asymmetric_key*  
 Specifies a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login mapped to an asymmetric key.  
  
 *server_role*  
 Specifies a user-defined server role.  
  
## Remarks  
 Permissions at the server scope can be granted only when the current database is master.  
  
 Information about server permissions can be viewed in the [sys.server_permissions](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md) catalog view, and information about server principals can be viewed in the [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md) catalog view. Information about membership of server roles can be viewed in the [sys.server_role_members](../../relational-databases/system-catalog-views/sys-server-role-members-transact-sql.md) catalog view.  
  
 A server is the highest level of the permissions hierarchy. The most specific and limited permissions that can be granted on a server are listed in the following table.  
  
|Server permission|Implied by server permission|  
|-----------------------|----------------------------------|  
|ADMINISTER BULK OPERATIONS|CONTROL SERVER|  
|ALTER ANY AVAILABILITY GROUP<br /><br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).|CONTROL SERVER|  
|ALTER ANY CONNECTION|CONTROL SERVER|  
|ALTER ANY CREDENTIAL|CONTROL SERVER|  
|ALTER ANY DATABASE|CONTROL SERVER|  
|ALTER ANY ENDPOINT|CONTROL SERVER|  
|ALTER ANY EVENT NOTIFICATION|CONTROL SERVER|  
|ALTER ANY EVENT SESSION|CONTROL SERVER|  
|ALTER ANY LINKED SERVER|CONTROL SERVER|  
|ALTER ANY LOGIN|CONTROL SERVER|  
|ALTER ANY SERVER AUDIT|CONTROL SERVER|  
|ALTER ANY SERVER ROLE<br /><br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).|CONTROL SERVER|  
|ALTER RESOURCES|CONTROL SERVER|  
|ALTER SERVER STATE|CONTROL SERVER|  
|ALTER SETTINGS|CONTROL SERVER|  
|ALTER TRACE|CONTROL SERVER|  
|AUTHENTICATE SERVER|CONTROL SERVER|  
|CONNECT ANY DATABASE<br /><br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).|CONTROL SERVER|  
|CONNECT SQL|CONTROL SERVER|  
|CONTROL SERVER|CONTROL SERVER|  
|CREATE ANY DATABASE|ALTER ANY DATABASE|  
|CREATE AVAILABILITY GROUP<br /><br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).|ALTER ANY AVAILABILITY GROUP|  
|CREATE DDL EVENT NOTIFICATION|ALTER ANY EVENT NOTIFICATION|  
|CREATE ENDPOINT|ALTER ANY ENDPOINT|  
|CREATE SERVER ROLE<br /><br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).|ALTER ANY SERVER ROLE|  
|CREATE TRACE EVENT NOTIFICATION|ALTER ANY EVENT NOTIFICATION|  
|EXTERNAL ACCESS ASSEMBLY|CONTROL SERVER|  
|IMPERSONATE ANY LOGIN<br /><br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).|CONTROL SERVER|  
|SELECT ALL USER SECURABLES<br /><br /> **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level)).|CONTROL SERVER|  
|SHUTDOWN|CONTROL SERVER|  
|UNSAFE ASSEMBLY|CONTROL SERVER|  
|VIEW ANY DATABASE|VIEW ANY DEFINITION|  
|VIEW ANY DEFINITION|CONTROL SERVER|  
|VIEW SERVER STATE|ALTER SERVER STATE|  
  
 The following three server permissions were added in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].  
  
 **CONNECT ANY DATABASE** Permission  
 Grant **CONNECT ANY DATABASE** to a login that must connect to all databases that currently exist and to any new databases that might be created in future. Does not grant any permission in any database beyond connect. Combine with **SELECT ALL USER SECURABLES** or **VIEW SERVER STATE** to allow an auditing process to view all data or all database states on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 **IMPERSONATE ANY LOGIN** Permission  
 When granted, allows a middle-tier process to impersonate the account of clients connecting to it, as it connects to databases. When denied, a high privileged login can be blocked from impersonating other logins. For example, a login with **CONTROL SERVER** permission can be blocked from impersonating other logins.  
  
 **SELECT ALL USER SECURABLES** Permission  
 When granted, a login can view data from all schema-level objects, such as tables, views and table valued functions that reside user-writable schemas (any schema except sys and INFORMATION_SCHEMA) can be used to create user-objects. This permission has effect in all databases that the user can connect to. When denied, it prevents access to all objects unless they are in the sys- or INFORMATION_SCHEMA-schema. This also has effect on metadata visibility of the covered objects also see: [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Permissions  
 The grantor (or the principal specified with the AS option) must have either the permission itself with GRANT OPTION or a higher permission that implies the permission being granted. Members of the sysadmin fixed server role can grant any permission.  
  
## Examples  
  
### A. Granting a permission to a login  
 The following example grants `CONTROL SERVER` permission to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `TerryEminhizer`.  
  
```sql  
USE master;  
GRANT CONTROL SERVER TO TerryEminhizer;  
GO  
```  
  
### B. Granting a permission that has GRANT permission  
 The following example grants `ALTER ANY EVENT NOTIFICATION` to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login `JanethEsteves` with the right to grant that permission to another login.  
  
```sql  
USE master;  
GRANT ALTER ANY EVENT NOTIFICATION TO JanethEsteves WITH GRANT OPTION;  
GO  
```  
  
### C. Granting a permission to a server role  
 The following example creates a server role named `ITDevelopers`. It grants the `ALTER ANY DATABASE` permission to the `ITDevelopers` server role..  
  
```sql  
USE master;  
CREATE SERVER ROLE ITDevelopers ;  
GRANT ALTER ANY DATABASE TO ITDevelopers ;  
GO  
```  
  
## See Also  
 [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 [DENY &#40;Transact-SQL&#41;](../../t-sql/statements/deny-transact-sql.md)   
 [DENY Server Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-server-permissions-transact-sql.md)   
 [REVOKE Server Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-server-permissions-transact-sql.md)   
 [Permissions Hierarchy &#40;Database Engine&#41;](../../relational-databases/security/permissions-hierarchy-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [sys.fn_builtin_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql.md)   
 [sys.fn_my_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-my-permissions-transact-sql.md)   
 [HAS_PERMS_BY_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/has-perms-by-name-transact-sql.md)  
  
