---
title: "REVOKE Object Permissions (Transact-SQL)"
description: REVOKE Object Permissions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "08/10/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "table permissions [SQL Server], revoking"
  - "REVOKE statement, objects"
  - "revoking permissions to access tables"
  - "object permissions [SQL Server], revoking"
dev_langs:
  - "TSQL"
---
# REVOKE Object Permissions (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Revokes permissions on a table, view, table-valued function, stored procedure, extended stored procedure, scalar function, aggregate function, service queue, or synonym. 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
REVOKE [ GRANT OPTION FOR ] <permission> [ ,...n ] ON   
    [ OBJECT :: ][ schema_name ]. object_name [ ( column [ ,...n ] ) ]  
        { FROM | TO } <database_principal> [ ,...n ]   
    [ CASCADE ]  
    [ AS <database_principal> ]  
  
<permission> ::=  
    ALL [ PRIVILEGES ] | permission [ ( column [ ,...n ] ) ]  
  
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
 Specifies a permission that can be revoked on a schema-contained object. For a list of the permissions, see the Remarks section later in this topic.  
  
 ALL  
 Revoking ALL does not revoke all possible permissions. Revoking ALL is equivalent to revoking all [!INCLUDE[vcpransi](../../includes/vcpransi-md.md)]-92 permissions applicable to the specified object. The meaning of ALL varies as follows:  
  
 Scalar function permissions: EXECUTE, REFERENCES.  
  
 Table-valued function permissions: DELETE, INSERT, REFERENCES, SELECT, UPDATE.  
  
 Stored Procedure permissions: EXECUTE.  
  
 Table permissions: DELETE, INSERT, REFERENCES, SELECT, UPDATE.  
  
 View permissions: DELETE, INSERT, REFERENCES, SELECT, UPDATE.  
  
 PRIVILEGES  
 Included for [!INCLUDE[vcpransi](../../includes/vcpransi-md.md)]-92 compliance. Does not change the behavior of ALL.  
  
 *column*  
 Specifies the name of a column in a table, view, or table-valued function on which the permission is being revoked. The parentheses ( ) are required. Only SELECT, REFERENCES, and UPDATE permissions can be denied on a column. *column* can be specified in the permissions clause or after the securable name.  
  
 ON [ OBJECT :: ] [ *schema_name* ] . *object_name*  
 Specifies the object on which the permission is being revoked. The OBJECT phrase is optional if *schema_name* is specified. If the OBJECT phrase is used, the scope qualifier (::) is required. If *schema_name* is not specified, the default schema is used. If *schema_name* is specified, the schema scope qualifier (.) is required.  
  
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
 Information about objects is visible in various catalog views. For more information, see [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md).  
  
 An object is a schema-level securable contained by the schema that is its parent in the permissions hierarchy. The most specific and limited permissions that can be revoked on an object are listed in the following table, together with the more general permissions that include them by implication.  
  
|Object permission|Implied by object permission|Implied by schema permission|  
|-----------------------|----------------------------------|----------------------------------|  
|ALTER|CONTROL|ALTER|  
|CONTROL|CONTROL|CONTROL|  
|DELETE|CONTROL|DELETE|  
|EXECUTE|CONTROL|EXECUTE|  
|INSERT|CONTROL|INSERT|  
|RECEIVE|CONTROL|CONTROL|  
|REFERENCES|CONTROL|REFERENCES|  
|SELECT|RECEIVE|SELECT|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|UPDATE|CONTROL|UPDATE|  
|VIEW CHANGE TRACKING|CONTROL|VIEW CHANGE TRACKING|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 Requires CONTROL permission on the object.  
  
 If you use the AS clause, the specified principal must own the object on which permissions are being revoked.  
  
## Examples  
  
### A. Revoking SELECT permission on a table  
 The following example revokes `SELECT` permission from the user `RosaQdM` on the table `Person.Address` in the `AdventureWorks2012` database.  
  
```sql  
USE AdventureWorks2012;  
REVOKE SELECT ON OBJECT::Person.Address FROM RosaQdM;  
GO  
```  
  
### B. Revoking EXECUTE permission on a stored procedure  
 The following example revokes `EXECUTE` permission on the stored procedure `HumanResources.uspUpdateEmployeeHireInfo` from an application role called `Recruiting11`.  
  
```sql  
USE AdventureWorks2012;  
REVOKE EXECUTE ON OBJECT::HumanResources.uspUpdateEmployeeHireInfo  
    FROM Recruiting11;  
GO   
```  
  
### C. Revoking REFERENCES permission on a view with CASCADE  
 The following example revokes `REFERENCES` permission on the column `BusinessEntityID` in the view `HumanResources.vEmployee` from the user `Wanida` with `CASCADE`.  
  
```sql  
USE AdventureWorks2012;  
REVOKE REFERENCES (BusinessEntityID) ON OBJECT::HumanResources.vEmployee   
    FROM Wanida CASCADE;  
GO  
```  
  
## See Also  
 [GRANT Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-object-permissions-transact-sql.md)   
 [DENY Object Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-object-permissions-transact-sql.md)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [Securables](../../relational-databases/security/securables.md)   
 [sys.fn_builtin_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql.md)   
 [HAS_PERMS_BY_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/has-perms-by-name-transact-sql.md)   
 [sys.fn_my_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-my-permissions-transact-sql.md)  
  
  

