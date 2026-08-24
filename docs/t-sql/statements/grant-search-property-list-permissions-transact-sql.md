---
title: "GRANT Search Property List Permissions"
titleSuffix: SQL Server (Transact-SQL)
description: Grant permissions on a search property list.
author: VanMSFT
ms.author: vanto
ms.date: "07/26/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "full-text search [SQL Server], permissions"
  - "search property lists [SQL Server], permissions"
  - "granting permissions [SQL Server], search property lists"
  - "GRANT statement, search property list permissions"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# GRANT Search Property List Permissions (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

  Grants permissions on a search property list.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
GRANT permission [ ,...n ] ON   
    SEARCH PROPERTY LIST :: search_property_list_name  
    TO database_principal [ ,...n ]  
    [ WITH GRANT OPTION ]  
    [ AS granting_principal ]  
```  
  
## Arguments
 *permission*  
 Is the name of a permission. The valid mappings of permissions to securables are described in the "Remarks" section, later in this topic.  
  
 ON SEARCH PROPERTY LIST **::**_search_property_list_name_  
 Specifies the search property list on which the permission is being granted. The scope qualifier **::** is required.  
  
 **To view the existing search property lists**  
  
-   [sys.registered_search_property_lists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql.md)  
  
 *database_principal*  
 Specifies the principal to which the permission is being granted. The principal can be one of the following:  
  
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
 Specifies a principal from which the principal executing this query derives its right to grant the permission. The principal can be one of the following:  
  
-   database user  
  
-   database role  
  
-   application role  
  
-   database user mapped to a Windows login  
  
-   database user mapped to a Windows group  
  
-   database user mapped to a certificate  
  
-   database user mapped to an asymmetric key  
  
-   database user not mapped to a server principal.  
  
## Remarks  
  
## SEARCH PROPERTY LIST Permissions  
 A search property list is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be granted on a search property list are listed in the following table, together with the more general permissions that include them by implication.  
  
|Search property list permission|Implied by search property list permission|Implied by database permission|  
|-------------------------------------|------------------------------------------------|------------------------------------|  
|ALTER|CONTROL|ALTER ANY FULLTEXT CATALOG|  
|CONTROL|CONTROL|CONTROL|  
|REFERENCES|CONTROL|REFERENCES|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 The grantor (or the principal specified with the AS option) must have either the permission itself with GRANT OPTION, or a higher permission that implies the permission being granted.  
  
 If using the AS option, the following additional requirements apply.  
  
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
  
### Granting permissions to a search property list  
 The following example grants `Mary` the `VIEW DEFINITION` permission on the search property list `DocumentTablePropertyList`.  
  
```sql  
GRANT VIEW DEFINITION  
    ON SEARCH PROPERTY LIST :: DocumentTablePropertyList  
    TO Mary ;  
```  
  
## Related content

- [CREATE APPLICATION ROLE (Transact-SQL)](create-application-role-transact-sql.md)
- [CREATE ASYMMETRIC KEY (Transact-SQL)](create-asymmetric-key-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](create-certificate-transact-sql.md)
- [CREATE SEARCH PROPERTY LIST (Transact-SQL)](create-search-property-list-transact-sql.md)
- [DENY Search Property List Permissions (Transact-SQL)](deny-search-property-list-permissions-transact-sql.md)
- [Encryption hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
- [sys.fn_my_permissions (Transact-SQL)](../../relational-databases/system-functions/sys-fn-my-permissions-transact-sql.md)
- [GRANT (Transact-SQL)](grant-transact-sql.md)
- [HAS_PERMS_BY_NAME (Transact-SQL)](../functions/has-perms-by-name-transact-sql.md)
- [Principals (Database Engine)](../../relational-databases/security/authentication-access/principals-database-engine.md)
- [REVOKE Search Property List Permissions (Transact-SQL)](revoke-search-property-list-permissions-transact-sql.md)
- [sys.fn_builtin_permissions (Transact-SQL)](../../relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql.md)
- [sys.registered_search_property_lists (Transact-SQL)](../../relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql.md)
- [Search document properties with search property lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md)
