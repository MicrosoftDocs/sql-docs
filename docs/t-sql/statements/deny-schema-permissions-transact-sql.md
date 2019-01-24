---
title: "DENY Schema Permissions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "denying permissions [SQL Server], schemas"
  - "schemas [SQL Server], permissions"
  - "permissions [SQL Server], schemas"
  - "DENY statement, schemas"
ms.assetid: 300a67c4-d226-4653-9e9f-7ae4d53fcf33
author: VanMSFT
ms.author: vanto
manager: craigg
---
# DENY Schema Permissions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Denies permissions on a schema.  
  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
DENY permission  [ ,...n ] } ON SCHEMA :: schema_name  
    TO database_principal [ ,...n ]   
    [ CASCADE ]  
        [ AS denying_principal ]  
```  
  
## Arguments  
 *permission*  
 Specifies a permission that can be denied on a schema. For a list of these permissions, see the Remarks section later in this topic.  
  
 ON SCHEMA **::** schema*_name*  
 Specifies the schema on which the permission is being denied. The scope qualifier **::** is required.  
  
 *database_principal*  
 Specifies the principal to which the permission is being denied. *database_principal* can be one of the following:  
  
-   Database user  
-   Database role  
-   Application role  
-   Database user mapped to a Windows login  
-   Database user mapped to a Windows group  
-   Database user mapped to a certificate  
-   Database user mapped to an asymmetric key  
-   Database user not mapped to a server principal  
  
CASCADE  
 Indicates that the permission being denied is also denied to other principals to which it has been granted by this principal.  
  
*denying_principal*  
 Specifies a principal from which the principal executing this query derives its right to deny the permission. *denying_principal* can be one of the following:  
  
-   Database user  
-   Database role  
-   Application role  
-   Database user mapped to a Windows login  
-   Database user mapped to a Windows group  
-   Database user mapped to a certificate  
-   Database user mapped to an asymmetric key  
-   Database user not mapped to a server principal  
  
## Remarks  
 A schema is a database-level securable that is contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be denied on a schema are listed in the following table, together with the more general permissions that include them by implication.  
  
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
  
## Permissions  
 Requires CONTROL permission on the schema. If you are using the AS option, the specified principal must own the schema.  
  
## See Also  
 [CREATE SCHEMA &#40;Transact-SQL&#41;](../../t-sql/statements/create-schema-transact-sql.md)   
 [DENY &#40;Transact-SQL&#41;](../../t-sql/statements/deny-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [sys.fn_builtin_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql.md)   
 [sys.fn_my_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-my-permissions-transact-sql.md)   
 [HAS_PERMS_BY_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/has-perms-by-name-transact-sql.md)  
  
  
