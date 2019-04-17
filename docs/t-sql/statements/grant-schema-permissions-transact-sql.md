---
title: "GRANT Schema Permissions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/19/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "schemas [SQL Server], permissions"
  - "permissions [SQL Server], schemas"
  - "GRANT statement, schemas"
  - "granting permissions [SQL Server], schemas"
ms.assetid: b2aa1fc8-e7af-45d2-9f80-737543c8aa95
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# GRANT Schema Permissions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Grants permissions on a schema.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
GRANT permission  [ ,...n ] ON SCHEMA :: schema_name  
    TO database_principal [ ,...n ]  
    [ WITH GRANT OPTION ]  
    [ AS granting_principal ]  
```  
  
## Arguments  
 *permission*  
 Specifies a permission that can be granted on a schema. For a list of the permissions, see the Remarks section later in this topic..  
  
 ON SCHEMA **::** schema*_name*  
 Specifies the schema on which the permission is being granted. The scope qualifier **::** is required.  
  
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
  
> [!IMPORTANT]  
>  A combination of ALTER and REFERENCE permissions in some cases could allow the grantee to view data or execute unauthorized functions. For example: A user with ALTER permission on a table and REFERENCE permission on a function can create a computed column over a function and have it be executed. In this case, the user must also have SELECT permission on the computed column.  
  
 A schema is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be granted on a schema are listed below, together with the more general permissions that include them by implication.  
  
|Schema permission|Implied by schema permission|Implied by database permission|  
|-----------------------|----------------------------------|------------------------------------|  
|ALTER|CONTROL|ALTER ANY SCHEMA|  
|CONTROL|CONTROL|CONTROL|  
|CREATE SEQUENCE|ALTER|ALTER ANY SCHEMA|  
|DELETE|CONTROL|DELETE|  
|EXECUTE|CONTROL|EXECUTE|  
|INSERT|CONTROL|INSERT|  
|REFERENCES|CONTROL|REFERENCES|  
|SELECT|CONTROL|SELECT|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|UPDATE|CONTROL|UPDATE|  
|VIEW CHANGE TRACKING|CONTROL|CONTROL|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
> [!CAUTION]  
>  A user with ALTER permission on a schema can use ownership chaining to access securables in other schemas, including securables to which that user is explicitly denied access. This is because ownership chaining bypasses permissions checks on referenced objects when they are owned by the principal that owns the objects that refer to them. A user with ALTER permission on a schema can create procedures, synonyms, and views that are owned by the schema's owner. Those objects will have access (via ownership chaining) to information in other schemas owned by the schema's owner. When possible, you should avoid granting ALTER permission on a schema if the schema's owner also owns other schemas.  
  
 For example, this issue may occur in the following scenarios. These scenarios assume that a user, referred as U1, has the ALTER permission on the S1 schema. The U1 user is denied to access a table object, referred as T1, in the schema S2. The S1 schema and the S2 schema are owned by the same owner.  
  
 The U1 user has the CREATE PROCEDURE permission on the database and the EXECUTE permission on the S1 schema. Therefore, the U1 user can create a stored procedure, and then access the denied object T1 in the stored procedure.  
  
 The U1 user has the CREATE SYNONYM permission on the database and the SELECT permission on the S1 schema. Therefore, the U1 user can create a synonym in the S1 schema for the denied object T1, and then access the denied object T1 by using the synonym.  
  
 The U1 user has the CREATE VIEW permission on the database and the SELECT permission on the S1 schema. Therefore, the U1 user can create a view in the S1 schema to query data from the denied object T1, and then access the denied object T1 by using the view.  
  
 For more information, see the Microsoft KB Article number 914847.  
  
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
  
### A. Granting INSERT permission on schema HumanResources to guest  
  
```  
GRANT INSERT ON SCHEMA :: HumanResources TO guest;  
```  
  
### B. Granting SELECT permission on schema Person to database user WilJo  
  
```  
GRANT SELECT ON SCHEMA :: Person TO WilJo WITH GRANT OPTION;  
```  
  
## See Also  
 [DENY Schema Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-schema-permissions-transact-sql.md)   
 [REVOKE Schema Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-schema-permissions-transact-sql.md)   
 [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [CREATE CERTIFICATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-certificate-transact-sql.md)   
 [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)   
 [CREATE APPLICATION ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-application-role-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [sys.fn_builtin_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql.md)   
 [sys.fn_my_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-my-permissions-transact-sql.md)   
 [HAS_PERMS_BY_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/has-perms-by-name-transact-sql.md)  
  
  
