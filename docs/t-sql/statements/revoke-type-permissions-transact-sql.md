---
title: "REVOKE Type Permissions (Transact-SQL)"
description: REVOKE Type Permissions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "08/10/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "REVOKE statement, types"
  - "permissions [SQL Server], types"
  - "type permissions [SQL Server]"
dev_langs:
  - "TSQL"
---
# REVOKE Type Permissions (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Revokes permissions on a type.  
  
  ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
REVOKE [ GRANT OPTION FOR ] permission [ ,...n ]   
    ON TYPE :: [ schema_name ]. type_name   
    { FROM | TO } <database_principal> [ ,...n ]   
    [ CASCADE ]  
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
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *permission*  
 Specifies a permission that can be revoked on a type. For a list of the permissions, see the Remarks section later in this topic.  
  
 ON TYPE **::** [ *schema_name* ] **.** *type_name*  
 Specifies the type on which the permission is being revoked. The scope qualifier (**::**) is required. If *schema_name* is not specified, the default schema is used. If *schema_name* is specified, the schema scope qualifier (**.**) is required.  
  
 { FROM | TO } \<database_principal> 
 Specifies the principal from which the permission is being revoked.  
  
 GRANT OPTION  
 Indicates that the right to grant the specified permission to other principals will be revoked. The permission itself will not be revoked.  
  
> [!IMPORTANT]  
>  If the principal has the specified permission without the GRANT option, the permission itself will be revoked.  
  
 CASCADE  
 Indicates that the permission being revoked is also revoked from other principals to which it has been granted or denied by this principal.  
  
> [!CAUTION]  
>  A cascaded revocation of a permission granted WITH GRANT OPTION will revoke both GRANT and DENY of that permission.  
  
 AS \<database_principal> 
 Specifies a principal from which the principal executing this query derives its right to revoke the permission.  
  
 *Database_user*  
 Specifies a database user.  
  
 *Database_role*  
 Specifies a database role.  
  
 *Application_role*  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later, [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)]
  
 Specifies an application role.  
  
 *Database_user_mapped_to_Windows_User*  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later
  
 Specifies a database user mapped to a Windows user.  
  
 *Database_user_mapped_to_Windows_Group*  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later
  
 Specifies a database user mapped to a Windows group.  
  
 *Database_user_mapped_to_certificate*  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later
  
 Specifies a database user mapped to a certificate.  
  
 *Database_user_mapped_to_asymmetric_key*  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later
  
 Specifies a database user mapped to an asymmetric key.  
  
 *Database_user_with_no_login*  
 Specifies a database user with no corresponding server-level principal.  
  
## Remarks  
 A type is a schema-level securable contained by the schema that is its parent in the permissions hierarchy.  
  
> [!IMPORTANT]  
>  **GRANT**, **DENY,** and **REVOKE** permissions do not apply to system types. User-defined types can be granted permissions. For more information about user-defined types, see [Working with User-Defined Types in SQL Server](../../relational-databases/clr-integration-database-objects-user-defined-types/working-with-user-defined-types-in-sql-server.md).  
  
 The most specific and limited permissions that can be revoked on a type are listed in the following table, together with the more general permissions that include them by implication.  
  
|Type permission|Implied by type permission|Implied by schema permission|  
|---------------------|--------------------------------|----------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|EXECUTE|CONTROL|EXECUTE|  
|REFERENCES|CONTROL|REFERENCES|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 Requires CONTROL permission on the type. If you use the AS clause, the specified principal must own the type.  
  
## Examples  
 The following example revokes `VIEW DEFINITION` permission on the user-defined type `PhoneNumber` from the user `KhalidR`. The `CASCADE` option indicates that `VIEW DEFINITION` permission will also be revoked from principals to which `KhalidR` granted it. `PhoneNumber` is located in schema `Telemarketing`.  
  
```sql  
REVOKE VIEW DEFINITION ON TYPE::Telemarketing.PhoneNumber   
    FROM KhalidR CASCADE;  
GO  
```  
  
## See Also  
 [GRANT Type Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-type-permissions-transact-sql.md)   
 [DENY Type Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-type-permissions-transact-sql.md)   
 [CREATE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/create-type-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [Securables](../../relational-databases/security/securables.md)  
  
  

