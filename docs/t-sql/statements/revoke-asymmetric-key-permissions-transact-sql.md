---
title: "REVOKE Asymmetric Key Permissions (Transact-SQL)"
description: REVOKE Asymmetric Key Permissions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "permissions [SQL Server], asymmetric keys"
  - "asymmetric keys [SQL Server], permissions"
  - "REVOKE statement, asymmetric keys"
dev_langs:
  - "TSQL"
---
# REVOKE Asymmetric Key Permissions (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Revokes permissions on an asymmetric key.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
REVOKE [ GRANT OPTION FOR ] { permission  [ ,...n ] }   
    ON ASYMMETRIC KEY :: asymmetric_key_name   
    { TO | FROM } database_principal [ ,...n ]  
    [ CASCADE ]  
    [ AS revoking_principal ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 GRANT OPTION FOR  
 Indicates that the ability to grant the specified permission will be revoked.  
  
> [!IMPORTANT]  
>  If the principal has the specified permission without the GRANT option, the permission itself will be revoked.  
  
 *permission*  
 Specifies a permission that can be revoked on an assembly. Listed below.  
  
 ON ASYMMETRIC KEY **::**_asymmetric_key_name_  
 Specifies the asymmetric key on which the permission is being revoked. The scope qualifier **::** is required.  
  
 *database_principal*  
 Specifies the principal from which the permission is being revoked. One of the following:  
  
-   Database user  
  
-   Database role  
  
-   Application role  
  
-   Database user mapped to a Windows login  
  
-   Database user mapped to a Windows group  
  
-   Database user mapped to a certificate  
  
-   Database user mapped to an asymmetric key  
  
-   Database user not mapped to a server principal.  
  
 CASCADE  
 Indicates that the permission being revoked is also revoked from other principals to which it has been granted or denied by this principal. The permission itself will not be revoked.  
  
> [!CAUTION]  
>  A cascaded revocation of a permission granted WITH GRANT OPTION will revoke both GRANT and DENY of that permission.  
  
 AS *revoking_principal*  
 Specifies a principal from which the principal executing this query derives its right to revoke the permission. One of the following:  
  
-   Database user  
  
-   Database role  
  
-   Application role  
  
-   Database user mapped to a Windows login  
  
-   Database user mapped to a Windows group  
  
-   Database user mapped to a certificate  
  
-   Database user mapped to an asymmetric key  
  
-   Database user not mapped to a server principal.  
  
## Remarks  
 An asymmetric key is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be revoked on an asymmetric key are listed below, together with the more general permissions that include them by implication.  
  
|Asymmetric Key permission|Implied by asymmetric key permission|Implied by database permission|  
|-------------------------------|------------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY ASYMMETRIC KEY|  
|REFERENCES|CONTROL|REFERENCES|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 Requires CONTROL permission on the asymmetric key.  
  
## See Also  
 [REVOKE &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)   
 [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)   
 [CREATE APPLICATION ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-application-role-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
