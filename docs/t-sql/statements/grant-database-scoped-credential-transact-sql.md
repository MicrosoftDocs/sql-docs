---
title: "GRANT Database Scoped Credential (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/19/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: conceptual
f1_keywords: 
  - "GRANT DATABASE SCOPED CREDENTIAL"
  - "GRANT_DATABASE_SCOPED_CREDENTIAL_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "granting permissions [SQL Server], database scoped credential"
  - "permissions [SQL Server], database scoped credential"
  - "database scoped credential [SQL Server], permissions"
  - "GRANT statement, database scoped credentials"
ms.assetid: 501f2c8a-6aeb-41af-bf0b-974d17af33c0
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# GRANT Database Scoped Credential Permissions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2017-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2017-asdb-xxxx-xxx-md.md)]

  Grants permissions on a database scoped credential. 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
GRANT permission  [ ,...n ]    
    ON DATABASE SCOPED CREDENTIAL :: credential_name   
    TO principal [ ,...n ] [ WITH GRANT OPTION ]   
    [ AS granting_principal ]   
```  
  
## Arguments  
 *permission*  
 Specifies a permission that can be granted on a database scoped credential. Listed below.  
  
 ON DATABASE SCOPED CREDENTIAL **::**_credential_name_  
 Specifies the database scoped credential on which the permission is being granted. The scope qualifier "::" is required.  
  
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
 A database scoped credential is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be granted on a database scoped credential are listed below, together with the more general permissions that include them by implication.  
  
|Database scoped credential permission|Implied by database scoped credential permission|Implied by database permission|  
|----------------------------|---------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|CONTROL|  
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
 [REVOKE Database Scoped Credential (Transact-SQL)](../../t-sql/statements/revoke-database-scoped-credential-transact-sql.md)   
 [DENY Database Scoped Credential (Transact-SQL)](../../t-sql/statements/deny-database-scoped-credential-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
