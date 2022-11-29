---
title: "REVOKE Symmetric Key Permissions (Transact-SQL)"
description: REVOKE Symmetric Key Permissions (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "08/10/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "symmetric keys [SQL Server], permissions"
  - "permissions [SQL Server], symmetric keys"
  - "REVOKE statement, symmetric keys"
dev_langs:
  - "TSQL"
---
# REVOKE Symmetric Key Permissions (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Revokes permissions granted and denied on a symmetric key.  
   
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
REVOKE [ GRANT OPTION FOR ] permission [ ,...n ]    
    ON SYMMETRIC KEY :: symmetric_key_name   
        { TO | FROM } <database_principal> [ ,...n ]   
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
 Specifies a permission that can be revoked on a symmetric key. For a list of the permissions, see the Remarks section later in this topic.  
  
 ON SYMMETRIC KEY :: *asymmetric_key_name*  
 Specifies the symmetric key on which the permission is being revoked. The scope qualifier (::) is required.  
  
 GRANT OPTION  
 Indicates that the right to grant the specified permission to other principals will be revoked. The permission itself will not be revoked.  
  
> [!IMPORTANT]  
>  If the principal has the specified permission without the GRANT option, the permission itself will be revoked.  
  
 CASCADE  
 Indicates that the permission being revoked is also revoked from other principals to which it has been granted or denied by this principal.  
  
> [!CAUTION]  
>  A cascaded revocation of a permission granted WITH GRANT OPTION will revoke both GRANT and DENY of that permission.  
  
 { TO | FROM } \<*database_principal*>  
 Specifies the principal from which the permission is being revoked.  
  
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
 Information about symmetric keys is visible in the [sys.symmetric_keys](../../relational-databases/system-catalog-views/sys-symmetric-keys-transact-sql.md) catalog view.  
  
 The statement will fail if CASCADE is not specified when revoking a permission from a principal that was granted that permission with GRANT OPTION specified.  
  
 A symmetric key is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be granted on a symmetric key are listed in the following table, together with the more general permissions that include them by implication.  
  
|Symmetric Key permission|Implied by symmetric key permission|Implied by database permission|  
|------------------------------|-----------------------------------------|------------------------------------|  
|ALTER|CONTROL|ALTER ANY SYMMETRIC KEY|  
|CONTROL|CONTROL|CONTROL|  
|REFERENCES|CONTROL|REFERENCES|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 Requires CONTROL permission on the symmetric key or ALTER ANY SYMMETRIC KEY permission on the database. If you use the AS option, the specified principal must own the symmetric key.  
  
## Examples  
 The following example revokes `ALTER` permission on the symmetric key `SamInventory42` from the user `HamidS` and from other principals to which `HamidS` has granted `ALTER` permission.  
  
```sql  
USE AdventureWorks2012;  
REVOKE ALTER ON SYMMETRIC KEY::SamInventory42 TO HamidS CASCADE;  
GO  
```  
  
## See Also  
 [sys.symmetric_keys &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-symmetric-keys-transact-sql.md)   
 [GRANT Symmetric Key Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-symmetric-key-permissions-transact-sql.md)   
 [DENY Symmetric Key Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/deny-symmetric-key-permissions-transact-sql.md)   
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  

