---
title: "GRANT Certificate Permissions (Transact-SQL)"
description: GRANT Certificate Permissions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/12/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "granting permissions [SQL Server], certificates"
  - "certificates [SQL Server], permissions"
  - "permissions [SQL Server], certificates"
  - "GRANT statement, certificates"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017||=azure-sqldw-latest"
---
# GRANT Certificate Permissions (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  Grants permissions on a certificate in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]
  
## Syntax  
  
```syntaxsql
GRANT permission  [ ,...n ]    
    ON CERTIFICATE :: certificate_name   
    TO principal [ ,...n ] [ WITH GRANT OPTION ]   
    [ AS granting_principal ]   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *permission*  
 Specifies a permission that can be granted on a certificate. Listed below.  
  
 ON CERTIFICATE **::**_certificate_name_  
 Specifies the certificate on which the permission is being granted. The scope qualifier "::" is required.  
  
 *database_principal*  
 Specifies the principal to which the permission is being granted. One of the following:  
  
-   database user  
-   database role  
-   application role  
-   database user mapped to a Windows login  
-   database user mapped to a Windows group  
-   database user mapped to a certificate  
-   database user mapped to an asymmetric key  
-   database user not mapped to a server principal.  
  
GRANT OPTION  
 Indicates that the principal will also be given the ability to grant the specified permission to other principals.  
  
AS *granting_principal*  
 Specifies a principal from which the principal executing this query derives its right to grant the permission. One of the following:  
  
-   database user  
-   database role  
-   application role  
-   database user mapped to a Windows login  
-   database user mapped to a Windows group  
-   database user mapped to a certificate  
-   database user mapped to an asymmetric key  
-   database user not mapped to a server principal.  
  
## Remarks  
 A certificate is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be granted on a certificate are listed below, together with the more general permissions that include them by implication.  
  
|Certificate permission|Implied by certificate permission|Implied by database permission|  
|----------------------------|---------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY CERTIFICATE|  
|REFERENCES|CONTROL|REFERENCES|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 The grantor (or the principal specified with the AS option) must have either the permission itself with GRANT OPTION, or a higher permission that implies the permission being granted.  
  
 If using the AS option, these additional requirements apply.  
  
|AS *granting_principal*|Additional permission required|  
|------------------------------|------------------------------------|  
|Database user|IMPERSONATE permission on the user, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database user mapped to a Windows login|IMPERSONATE permission on the user, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database user mapped to a Windows group|Membership in the Windows group, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database user mapped to a certificate|Membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database user mapped to an asymmetric key|Membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database user not mapped to any server principal|IMPERSONATE permission on the user, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database role|ALTER permission on the role, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Application role|ALTER permission on the role, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
  
 Object owners can grant permissions on the objects they own. Principals with CONTROL permission on a securable can grant permission on that securable.  
  
 Grantees of CONTROL SERVER permission, such as members of the **sysadmin** fixed server role, can grant any permission on any securable in the server. Grantees of CONTROL permission on a database, such as members of the **db_owner** fixed database role, can grant any permission on any securable in the database. Grantees of CONTROL permission on a schema can grant any permission on any object within the schema.  
  
## See Also  
 [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)   
 [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)   
 [CREATE APPLICATION ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-application-role-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
