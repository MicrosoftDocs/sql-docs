---
title: "DENY Search Property List Permissions"
titleSuffix: SQL Server (Transact-SQL)
description: Deny permissions on a search property list.
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "full-text search [SQL Server], permissions"
  - "DENY statement, search property list permissions"
  - "denying permissions [SQL Server], search property lists"
  - "search property lists [SQL Server], permissions"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# DENY Search Property List Permissions (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

  Denies permissions on a search property list.  
 
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DENY permission [ ,...n ] ON  
        SEARCH PROPERTY LIST :: search_property_list_name  
    TO database_principal [ ,...n ] [ CASCADE ]  
    [ AS denying_principal ]  
```  
  
## Arguments
 *permission*  
 Is the name of a permission. The valid mappings of permissions to securables are described in the "Remarks" section, later in this topic.  
  
ON SEARCH PROPERTY LIST **::**_search_property_list_name_  
 Specifies the search property list on which the permission is being denied. The scope qualifier :: is required.  
  
*database_principal*  
 Specifies the principal to which the permission is being denied. The principal can be one of the following:  
  
-   database user  
-   database role  
-   application role  
-   database user mapped to a Windows login  
-   database user mapped to a Windows group  
-   database user mapped to a certificate  
-   database user mapped to an asymmetric key  
-   database user not mapped to a server principal.  
  
CASCADE  
 Indicates that the permission being denied is also denied to other principals to which it has been granted by this principal.  
  
*denying_principal*  
 Specifies a principal from which the principal executing this query derives its right to deny the permission. The principal can be one of the following:  
  
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
 A search property list is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be denied on a search property list are listed in the following table, together with the more general permissions that include them by implication.  
  
|Search property list permission|Implied by search property list permission|Implied by database permission|  
|-------------------------------------|------------------------------------------------|------------------------------------|  
|ALTER|CONTROL|ALTER ANY FULLTEXT CATALOG|  
|CONTROL|CONTROL|CONTROL|  
|REFERENCES|CONTROL|REFERENCES|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 Requires CONTROL permission on the full-text catalog. If using the AS option, the specified principal must own the full-text catalog.  
  
## Related content

- [CREATE APPLICATION ROLE (Transact-SQL)](create-application-role-transact-sql.md)
- [CREATE ASYMMETRIC KEY (Transact-SQL)](create-asymmetric-key-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](create-certificate-transact-sql.md)
- [CREATE SEARCH PROPERTY LIST (Transact-SQL)](create-search-property-list-transact-sql.md)
- [DENY (Transact-SQL)](deny-transact-sql.md)
- [Encryption hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
- [sys.fn_my_permissions (Transact-SQL)](../../relational-databases/system-functions/sys-fn-my-permissions-transact-sql.md)
- [GRANT Search Property List Permissions (Transact-SQL)](grant-search-property-list-permissions-transact-sql.md)
- [HAS_PERMS_BY_NAME (Transact-SQL)](../functions/has-perms-by-name-transact-sql.md)
- [Principals (Database Engine)](../../relational-databases/security/authentication-access/principals-database-engine.md)
- [REVOKE Search Property List Permissions (Transact-SQL)](revoke-search-property-list-permissions-transact-sql.md)
- [sys.fn_builtin_permissions (Transact-SQL)](../../relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql.md)
- [sys.registered_search_property_lists (Transact-SQL)](../../relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql.md)
- [Search document properties with search property lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md)
