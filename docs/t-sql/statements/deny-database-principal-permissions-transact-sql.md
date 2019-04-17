---
title: "DENY Database Principal Permissions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database roles [SQL Server], permissions"
  - "denying permissions [SQL Server], database roles"
  - "denying permissions [SQL Server], database users"
  - "permissions [SQL Server], database roles"
  - "DENY statement, database roles"
  - "database user permissions [SQL Server]"
  - "permissions [SQL Server], application roles"
  - "permissions [SQL Server], database users"
  - "database permissions [SQL Server], denying"
  - "DENY statement, application roles"
  - "DENY statement, database users"
  - "denying permissions [SQL Server], application roles"
  - "application roles [SQL Server], permissions"
ms.assetid: e2429a5d-e9be-4c05-be20-414d1038a63a
author: VanMSFT
ms.author: vanto
manager: craigg
---
# DENY Database Principal Permissions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Denies permissions granted on a database user, database role, or application role in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
DENY permission [ ,...n ]    
    ON   
    {  [ USER :: database_user ]  
     | [ ROLE :: database_role ]  
     | [ APPLICATION ROLE :: application_role ]  
    }  
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
 Specifies a permission that can be denied on the database principal. For a list of the permissions, see the Remarks section later in this topic.  
  
 USER ::*database_user*  
 Specifies the class and name of the user on which the permission is being denied. The scope qualifier (**::**) is required.  
  
 ROLE ::*database_role*  
 Specifies the class and name of the role on which the permission is being denied. The scope qualifier (**::**) is required.  
  
 APPLICATION ROLE ::*application_role*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
 Specifies the class and name of the application role on which the permission is being denied. The scope qualifier (**::**) is required.  
  
 CASCADE  
 Indicates that the permission being denied is also denied to other principals to which it has been granted by this principal.  
  
 AS \<database_principal>  
 Specifies a principal from which the principal executing this query derives its right to revoke the permission.  
  
 *Database_user*  
 Specifies a database user.  
  
 *Database_role*  
 Specifies a database role.  
  
 *Application_role*  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
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
  
## Database User Permissions  
 A database user is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be denied on a database user are listed in the following table, together with the more general permissions that include them by implication.  
  
|Database user permission|Implied by database user permission|Implied by database permission|  
|------------------------------|-----------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|IMPERSONATE|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY USER|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Database Role Permissions  
 A database role is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be denied on a database role are listed in the following table, together with the more general permissions that include them by implication.  
  
|Database role permission|Implied by database role permission|Implied by database permission|  
|------------------------------|-----------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY ROLE|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Application Role Permissions  
 An application role is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be denied on an application role are listed in the following table, together with the more general permissions that include them by implication.  
  
|Application role permission|Implied by application role permission|Implied by database permission|  
|---------------------------------|--------------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|ALTER|CONTROL|ALTER ANY APPLICATION ROLE|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 Requires CONTROL permission on the specified principal, or a higher permission that implies CONTROL permission.  
  
 Grantees of CONTROL permission on a database, such as members of the db_owner fixed database role, can deny any permission on any securable in the database.  
  
## Examples  
  
### A. Denying CONTROL permission on a user to another user  
 The following example denies `CONTROL` permission on the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] user `Wanida` to user `RolandX`.  
  
```  
USE AdventureWorks2012;  
DENY CONTROL ON USER::Wanida TO RolandX;  
GO  
```  
  
### B. Denying VIEW DEFINITION permission on a role to a user to which it was granted with GRANT OPTION  
 The following example denies `VIEW DEFINITION` permission on the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] role `SammamishParking` to database user `JinghaoLiu`. The `CASCADE` option is specified because user `JinghaoLiu` was granted VIEW DEFINITION permission WITH GRANT OPTION.  
  
```  
USE AdventureWorks2012;  
DENY VIEW DEFINITION ON ROLE::SammamishParking   
    TO JinghaoLiu CASCADE;  
GO  
```  
  
### C. Denying IMPERSONATE permission on a user to an application role  
 The following example denies `IMPERSONATE` permission on user `HamithaL` to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] application role `AccountsPayable17`.  
  
**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)].  
  
```  
USE AdventureWorks2012;  
DENY IMPERSONATE ON USER::HamithaL TO AccountsPayable17;  
GO    
```  
  
## See Also  
 [GRANT Database Principal Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-database-principal-permissions-transact-sql.md)   
 [REVOKE Database Principal Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/revoke-database-principal-permissions-transact-sql.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)   
 [sys.database_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-permissions-transact-sql.md)   
 [CREATE USER &#40;Transact-SQL&#41;](../../t-sql/statements/create-user-transact-sql.md)   
 [CREATE APPLICATION ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-application-role-transact-sql.md)   
 [CREATE ROLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-role-transact-sql.md)   
 [GRANT &#40;Transact-SQL&#41;](../../t-sql/statements/grant-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
  
  
