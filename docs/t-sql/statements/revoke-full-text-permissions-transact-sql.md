---
title: "REVOKE Full-Text Permissions (Transact-SQL)"
description: REVOKE Full-Text Permissions (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "07/26/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "REVOKE statement, full-text permissions"
  - "full-text catalogs [SQL Server], permissions"
  - "full-text stoplist [SQL Server], permissions"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# REVOKE Full-Text Permissions (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

  Revokes permissions on a full-text catalog or full-text stoplist.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
REVOKE [ GRANT OPTION FOR ] permission [ ,...n ] ON  
    FULLTEXT   
        {  
           CATALOG :: full-text_catalog_name  
           |  
           STOPLIST :: full-text_stoplist_name  
        }  
    { TO | FROM } database_principal [ ,...n ]  
    [ CASCADE ]  
    [ AS revoking_principal ]  
```  
  
## Arguments
 GRANT OPTION FOR  
 Indicates that the right to grant the specified permission to other principals will be revoked. The permission itself will not be revoked.  
  
> [!IMPORTANT]  
>  If the principal has the specified permission without the GRANT option, the permission itself will be revoked.  
  
 *permission*  
 Is the name of a permission. The valid mappings of permissions to securables are described in the "Remarks" section, later in this topic.  
  
 ON FULLTEXT CATALOG **::**_full-text_catalog_name_  
 Specifies the full-text catalog on which the permission is being revoked. The scope qualifier **::** is required.  
  
 ON FULLTEXT STOPLIST **::**_full-text_stoplist_name_  
 Specifies the full-text stoplist on which the permission is being revoked. The scope qualifier **::** is required.  
  
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
 Indicates that the permission being revoked is also revoked from other principals to which it has been granted by this principal.  
  
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
  
## FULLTEXT CATALOG Permissions  
 A full-text catalog is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be revoked on a full-text catalog are listed in the following table, together with the more general permissions that include them by implication.  
  
|Full-text catalog permission|Implied by full-text catalog permission|Implied by database permission|  
|-----------------------------------|----------------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY FULLTEXT CATALOG|  
|REFERENCES|CONTROL|REFERENCES|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## FULLTEXT STOPLIST Permissions  
 A full-text stoplist is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be revoked on a full-text stoplist are listed in the following table, together with the more general permissions that include them by implication.  
  
|Full-text stoplist permission|Implied by full-text stoplist permission|Implied by database permission|  
|------------------------------------|-----------------------------------------------|------------------------------------|  
|ALTER|CONTROL|ALTER ANY FULLTEXT CATALOG|  
|CONTROL|CONTROL|CONTROL|  
|REFERENCES|CONTROL|REFERENCES|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 Requires CONTROL permission on the full-text catalog.  
  
## Related content

- [CREATE APPLICATION ROLE (Transact-SQL)](create-application-role-transact-sql.md)
- [CREATE ASYMMETRIC KEY (Transact-SQL)](create-asymmetric-key-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](create-certificate-transact-sql.md)
- [CREATE FULLTEXT CATALOG (Transact-SQL)](create-fulltext-catalog-transact-sql.md)
- [CREATE FULLTEXT STOPLIST (Transact-SQL)](create-fulltext-stoplist-transact-sql.md)
- [Encryption hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
- [sys.fn_my_permissions (Transact-SQL)](../../relational-databases/system-functions/sys-fn-my-permissions-transact-sql.md)
- [GRANT Full-Text Permissions (Transact-SQL)](grant-full-text-permissions-transact-sql.md)
- [HAS_PERMS_BY_NAME (Transact-SQL)](../functions/has-perms-by-name-transact-sql.md)
- [Permissions (Database Engine)](../../relational-databases/security/permissions-database-engine.md)
- [Principals (Database Engine)](../../relational-databases/security/authentication-access/principals-database-engine.md)
- [REVOKE (Transact-SQL)](revoke-transact-sql.md)
- [sys.fn_builtin_permissions (Transact-SQL)](../../relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql.md)
- [sys.fulltext_catalogs (Transact-SQL)](../../relational-databases/system-catalog-views/sys-fulltext-catalogs-transact-sql.md)
- [sys.fulltext_stoplists (Transact-SQL)](../../relational-databases/system-catalog-views/sys-fulltext-stoplists-transact-sql.md)
