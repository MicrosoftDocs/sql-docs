---
title: "REVOKE Assembly Permissions (Transact-SQL)"
description: REVOKE Assembly Permissions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "07/26/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "REVOKE statement, assemblies"
  - "assemblies [CLR integration], permissions"
dev_langs:
  - "TSQL"
---
# REVOKE Assembly Permissions (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Revokes permissions on an assembly.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
REVOKE [ GRANT OPTION FOR ] permission [ ,...n ]   
    ON ASSEMBLY :: assembly_name   
    { TO | FROM } database_principal [ ,...n ]  
    [ CASCADE ]  
    [ AS revoking_principal ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 GRANT OPTION FOR  
 Indicates that the ability to grant or deny the specified permission will be revoked. The permission itself will not be revoked.  
  
> [!IMPORTANT]  
>  If the principal has the specified permission without the GRANT option, the permission itself will be revoked.  
  
 *permission*  
 Specifies a permission that can be revoked on an assembly. Listed below.  
  
 ON ASSEMBLY **::**_assembly_name_  
 Specifies the assembly on which the permission is being revoked. The scope qualifier **::** is required.  
  
 *database_principal*  
 Specifies the principal from which the permission is being revoked. One of the following:  
  
-   database user  
  
-   database role  
  
-   application role  
  
-   database user mapped to a Windows login  
  
-   database user mapped to a Windows group  
  
-   database user mapped to a certificate  
  
-   database user mapped to an asymmetric key  
  
-   database user not mapped to a server principal.  
  
 CASCADE  
 Indicates that the permission being revoked is also revoked from other principals to which it has been granted or denied by this principal.  
  
> [!CAUTION]  
>  A cascaded revocation of a permission granted WITH GRANT OPTION will revoke both GRANT and DENY of that permission.  
  
 AS *revoking_principal*  
 Specifies a principal from which the principal executing this query derives its right to revoke the permission. One of the following:  
  
-   database user  
  
-   database role  
  
-   application role  
  
-   database user mapped to a Windows login  
  
-   database user mapped to a Windows group  
  
-   database user mapped to a certificate  
  
-   database user mapped to an asymmetric key  
  
-   database user not mapped to a server principal.  
  
## Remarks  
 An assembly is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be revoked on an assembly are listed below, together with the more general permissions that include them by implication.  
  
|Assembly permission|Implied by assembly permission|Implied by database permission|  
|-------------------------|------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY ASSEMBLY|  
|REFERENCES|CONTROL|REFERENCES|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 Requires CONTROL permission on the assembly  
  
## See Also  
 [DENY &#40;Transact-SQL&#41;](../../t-sql/statements/deny-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [CREATE ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/create-assembly-transact-sql.md)   
 [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)   
 [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)   
 [CREATE APPLICATION ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-application-role-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
