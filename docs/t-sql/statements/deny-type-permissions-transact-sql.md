---
title: "DENY Type Permissions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/09/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DENY statement, types"
  - "permissions [SQL Server], types"
  - "type permissions [SQL Server]"
  - "denying permissions [SQL Server], types"
ms.assetid: 564e3500-c567-43dc-993b-9ab50e99cf3f
author: VanMSFT
ms.author: vanto
manager: craigg
---
# DENY Type Permissions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Denies permissions on a type in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
DENY permission  [ ,...n ] ON TYPE :: [ schema_name . ] type_name  
        TO <database_principal> [ ,...n ]  
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
  
## Arguments  
 *permission*  
 Specifies a permission that can be denied on a type. For a list of the permissions, see the Remarks section later in this topic.  
  
 ON TYPE **::** [ _schema_name_**.** ] *type_name*  
 Specifies the type on which the permission is being denied. The scope qualifier (**::**) is required. If *schema_name* is not specified, the default schema is used. If *schema_name* is specified, the schema scope qualifier (**.**) is required.  
  
 TO \<database_principal>  
 Specifies the principal to which the permission is being denied.  
  
 CASCADE  
 Indicates that the permission being denied is also denied to other principals to which it has been granted by this principal.  
  
 AS \<database_principal>  
 Specifies a principal from which the principal executing this query derives its right to deny the permission.  
  
 *Database_user*  
 Specifies a database user.  
  
 *Database_role*  
 Specifies a database role.  
  
 *Application_role*  
   
 Specifies an application role.  
  
 *Database_user_mapped_to_Windows_User*  
 
 Specifies a database user mapped to a Windows user.  
  
 *Database_user_mapped_to_Windows_Group*  
 
 Specifies a database user mapped to a Windows group.  
  
 *Database_user_mapped_to_certificate*  
 
 Specifies a database user mapped to a certificate.  
  
 *Database_user_mapped_to_asymmetric_key*  
  
 Specifies a database user mapped to an asymmetric key.  
  
 *Database_user_with_no_login*  
 Specifies a database user with no corresponding server-level principal.  
  
## Remarks  
 A type is a schema-level securable contained by the schema that is its parent in the permissions hierarchy.  
  
> [!IMPORTANT]  
>  **GRANT**, **DENY,** and **REVOKE** permissions do not apply to system types. User-defined types can be granted permissions. For more information about user-defined types, see [Working with User-Defined Types in SQL Server](../../relational-databases/clr-integration-database-objects-user-defined-types/working-with-user-defined-types-in-sql-server.md).  
  
 The most specific and limited permissions that can be denied on a type are listed in the following table, together with the more general permissions that include them by implication.  
  
|Type permission|Implied by type permission|Implied by schema permission|  
|---------------------|--------------------------------|----------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|EXECUTE|CONTROL|EXECUTE|  
|REFERENCES|CONTROL|REFERENCES|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 Requires CONTROL permission on the type. If you use the AS clause, the specified principal must own the type on which permissions are being denied.  
  
## Examples  
 The following example denies `VIEW DEFINITION` permission with `CASCADE` on the user-defined type `PhoneNumber` to the `KhalidR`. `PhoneNumber` is located in schema `Telemarketing`.  
  
```  
DENY VIEW DEFINITION ON TYPE::Telemarketing.PhoneNumber   
    TO KhalidR CASCADE;  
GO  
```  
  
## See Also  
 [GRANT Type Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-type-permissions-transact-sql.md)   
 [REVOKE Type Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-type-permissions-transact-sql.md)   
 [CREATE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/create-type-transact-sql.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Securables](../../relational-databases/security/securables.md)  
  
  
