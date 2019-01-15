---
title: "GRANT Type Permissions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/10/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "permissions [SQL Server], types"
  - "granting permissions [SQL Server], types"
  - "GRANT statement, types"
  - "type permissions [SQL Server]"
ms.assetid: 14bd2fb3-1446-49c0-be87-c6a670317ed0
author: VanMSFT
ms.author: vanto
manager: craigg
---
# GRANT Type Permissions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Grants permissions on a type.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
GRANT permission  [ ,...n ] ON TYPE :: [ schema_name . ] type_name  
    TO <database_principal> [ ,...n ]  
    [ WITH GRANT OPTION ]  
    [ AS <database_principal> ]  
  
<database_principal> ::=   
        Database_user   
    | Database_role   
        | Application_role   
    | Database_user_mapped_to_Windows_User   
    | Database_user_mapped_to_Windows_Group   
    | Database_user_mapped_to_certificate   
    | Database_user_mapped_to_asymmetric_key   
    | Database_user_with_no_login  
```  
  
## Arguments  
 *permission*  
 Specifies a permission that can be granted on a type. For a list of the permissions, see the Remarks section later in this topic.  
  
 ON TYPE **::** [ _schema_name_**.** ] *type_name*  
 Specifies the type on which the permission is being granted. The scope qualifier (**::**) is required. If *schema_name* is not specified, the default schema will be used. If *schema_name* is specified, the schema scope qualifier (**.**) is required.  
  
 TO \<database_principal> 
 Specifies the principal to which the permission is being granted.  
  
 WITH GRANT OPTION  
 Indicates that the principal will also be given the ability to grant the specified permission to other principals.  
  
 AS \<database_principal> 
 Specifies a principal from which the principal executing this query derives its right to grant the permission.  
  
 *Database_user*  
 Specifies a database user.  
  
 *Database_role*  
 Specifies a database role.  
  
 *Application_role*  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)]
  
 Specifies an application role.  
  
 *Database_user_mapped_to_Windows_User*  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]
  
 Specifies a database user mapped to a Windows user.  
  
 *Database_user_mapped_to_Windows_Group*  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]
  
 Specifies a database user mapped to a Windows group.  
  
 *Database_user_mapped_to_certificate*  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]
  
 Specifies a database user mapped to a certificate.  
  
 *Database_user_mapped_to_asymmetric_key*  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]
  
 Specifies a database user mapped to an asymmetric key.  
  
 *Database_user_with_no_login*  
 Specifies a database user with no corresponding server-level principal.  
  
## Remarks  
 A type is a schema-level securable contained by the schema that is its parent in the permissions hierarchy.  
  
> [!IMPORTANT]  
>  **GRANT**, **DENY,** and **REVOKE** permissions do not apply to system types. User-defined types can be granted permissions. For more information about user-defined types, see [Working with User-Defined Types in SQL Server](../../relational-databases/clr-integration-database-objects-user-defined-types/working-with-user-defined-types-in-sql-server.md).  
  
 The most specific and limited permissions that can be granted on a type are listed in the following table, together with the more general permissions that include them by implication.  
  
|Type permission|Implied by type permission|Implied by schema permission|  
|---------------------|--------------------------------|----------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|EXECUTE|CONTROL|EXECUTE|  
|REFERENCES|CONTROL|REFERENCES|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 The grantor (or the principal specified with the AS option) must have either the permission itself with GRANT OPTION, or a higher permission that implies the permission being granted.  
  
 If you are using the AS option, the following additional requirements apply.  
  
|AS|Additional permission required|  
|--------|------------------------------------|  
|Database user|IMPERSONATE permission on the user, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database user mapped to a Windows login|IMPERSONATE permission on the user, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database user mapped to a Windows group|Membership in the Windows group, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database user mapped to a certificate|Membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database user mapped to an asymmetric key|Membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database user not mapped to any server principal|IMPERSONATE permission on the user, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Database role|ALTER permission on the role, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
|Application role|ALTER permission on the role, membership in the **db_securityadmin** fixed database role, membership in the **db_owner** fixed database role, or membership in the **sysadmin** fixed server role.|  
  
## Examples  
 The following example grants `VIEW DEFINITION` permission with `GRANT OPTION` on the user-defined type `PhoneNumber` to user `KhalidR`. `PhoneNumber` is located in the schema `Telemarketing`.  
  
```  
GRANT VIEW DEFINITION ON TYPE::Telemarketing.PhoneNumber   
    TO KhalidR WITH GRANT OPTION;  
GO  
```  
  
## See Also  
 [DENY Type Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-type-permissions-transact-sql.md)   
 [REVOKE Type Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-type-permissions-transact-sql.md)   
 [CREATE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/create-type-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Securables](../../relational-databases/security/securables.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  

