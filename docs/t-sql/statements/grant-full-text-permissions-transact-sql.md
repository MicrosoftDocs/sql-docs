---
title: "GRANT Full-Text Permissions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "granting permissions [SQL Server], full-text"
  - "full-text search [SQL Server], permissions"
  - "full-text catalogs [SQL Server], permissions"
  - "full-text stoplist [SQL Server], permissions"
  - "GRANT statement, full-text permissions"
ms.assetid: fdb64e09-222a-47fe-b08b-999264ca261d
author: MightyPen
ms.author: genemi
manager: "craigg"
---
# GRANT Full-Text Permissions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Grants permissions on a full-text catalog or full-text stoplist.  
  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
GRANT permission [ ,...n ] ON  
    FULLTEXT   
        {  
           CATALOG :: full-text_catalog_name  
           |  
           STOPLIST :: full-text_stoplist_name  
        }  
    TO database_principal [ ,...n ]  
    [ WITH GRANT OPTION ]  
    [ AS granting_principal ]  
```  
  
## Arguments  
 *permission*  
 Is the name of a permission. The valid mappings of permissions to securables are described in the "Remarks" section, later in this topic.  
  
 ON FULLTEXT CATALOG **::**_full-text_catalog_name_  
 Specifies the full-text catalog on which the permission is being granted. The scope qualifier **::** is required.  
  
 ON FULLTEXT STOPLIST **::**_full-text_stoplist_name_  
 Specifies the full-text stoplist on which the permission is being granted. The scope qualifier **::** is required.  
  
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
  
## FULLTEXT CATALOG Permissions  
 A full-text catalog is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be granted on a full-text catalog are listed in the following table, together with the more general permissions that include them by implication.  
  
|Full-text catalog permission|Implied by full-text catalog permission|Implied by database permission|  
|-----------------------------------|----------------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY FULLTEXT CATALOG|  
|REFERENCES|CONTROL|REFERENCES|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## FULLTEXT STOPLIST Permissions  
 A full-text stoplist is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be granted on a full-text stoplist are listed in the following table, together with the more general permissions that include them by implication.  
  
|Full-text stoplist permission|Implied by full-text stoplist permission|Implied by database permission|  
|------------------------------------|-----------------------------------------------|------------------------------------|  
|ALTER|CONTROL|ALTER ANY FULLTEXT CATALOG|  
|CONTROL|CONTROL|CONTROL|  
|REFERENCES|CONTROL|REFERENCES|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 The grantor (or the principal specified with the AS option) must have either the permission itself with GRANT OPTION, or a higher permission that implies the permission being granted.  
  
 If using the AS option, these additional requirements apply.  
  
|AS *granting_principal*|Additional permission required|  
|------------------------------|------------------------------------|  
|Database user|IMPERSONATE permission on the user, membership in the db_securityadmin fixed database role, membership in the db_owner fixed database role, or membership in the sysadmin fixed server role.|  
|Database user mapped to a Windows login|IMPERSONATE permission on the user, membership in the db_securityadmin fixed database role, membership in the db_owner fixed database role, or membership in the sysadmin fixed server role.|  
|Database user mapped to a Windows group|Membership in the Windows group, membership in the db_securityadmin fixed database role, membership in the db_owner fixed database role, or membership in the sysadmin fixed server role.|  
|Database user mapped to a certificate|Membership in the db_securityadmin fixed database role, membership in the db_owner fixed database role, or membership in the sysadmin fixed server role.|  
|Database user mapped to an asymmetric key|Membership in the db_securityadmin fixed database role, membership in the db_owner fixed database role, or membership in the sysadmin fixed server role.|  
|Database user not mapped to any server principal|IMPERSONATE permission on the user, membership in the db_securityadmin fixed database role, membership in the db_owner fixed database role, or membership in the sysadmin fixed server role.|  
|Database role|ALTER permission on the role, membership in the db_securityadmin fixed database role, membership in the db_owner fixed database role, or membership in the sysadmin fixed server role.|  
|Application role|ALTER permission on the role, membership in the db_securityadmin fixed database role, membership in the db_owner fixed database role, or membership in the sysadmin fixed server role.|  
  
 Object owners can grant permissions on the objects they own. Principals with CONTROL permission on a securable can grant permission on that securable.  
  
 Grantees of CONTROL SERVER permission, such as members of the sysadmin fixed server role, can grant any permission on any securable in the server. Grantees of CONTROL permission on a database, such as members of the db_owner fixed database role, can grant any permission on any securable in the database. Grantees of CONTROL permission on a schema can grant any permission on any object within the schema.  
  
## Examples  
  
### A. Granting permissions to a full-text catalog  
 The following example grants `Ted` the `CONTROL` permission on the full-text catalog `ProductCatalog`.  
  
```  
GRANT CONTROL  
    ON FULLTEXT CATALOG :: ProductCatalog  
    TO Ted ;  
```  
  
### B. Granting permissions to a stoplist  
 The following example grants `Mary` the `VIEW DEFINITION` permission on the full-text stoplist `ProductStoplist`.  
  
```  
GRANT VIEW DEFINITION  
    ON FULLTEXT STOPLIST :: ProductStoplist  
    TO Mary ;  
```  
  
## See Also  
 [CREATE APPLICATION ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-application-role-transact-sql.md)   
 [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)   
 [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)   
 [CREATE FULLTEXT CATALOG &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-catalog-transact-sql.md)   
 [CREATE FULLTEXT STOPLIST &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-stoplist-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [sys.fn_my_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-my-permissions-transact-sql.md)   
 [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 [HAS_PERMS_BY_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/has-perms-by-name-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [sys.fn_builtin_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql.md)   
 [sys.fulltext_catalogs &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-catalogs-transact-sql.md)   
 [sys.fulltext_stoplists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-stoplists-transact-sql.md)  
  
  
