---
title: "DENY Database Scoped Credential (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: conceptual
f1_keywords: 
  - "DENY DATABASE SCOPED CREDENTIAL"
  - "DENY_DATABASE_SCOPED_CREDENTIAL_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DENY statement, database scoped credentials"
  - "denying permissions [SQL Server], database scoped credential"
ms.assetid: c508b1c9-169e-4e7a-9a49-7ddf2ca8f848
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2017||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# DENY Database Scoped Credential (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2017-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2017-asdb-xxxx-xxx-md.md)]

  Denies permissions on a database scoped credential.  

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DENY permission  [ ,...n ]   
    ON DATABASE SCOPED CREDENTIAL :: credential_name   
    TO principal [ ,...n ]  
    [ CASCADE ]  
    [ AS denying_principal ]  
```  
  
## Arguments  
 *permission*  
 Specifies a permission that can be denied on a database scoped credential. Listed below.  
  
 ON DATABASE SCOPED CREDENTIAL **::**_credential_name_  
 Specifies the database scoped credential on which the permission is being denied. The scope qualifier "::" is required.  
  
 *database_principal*  
 Specifies the principal to which the permission is being denied. One of the following:  
  
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
 Specifies a principal from which the principal executing this query derives its right to deny the permission. One of the following:  
  
-   database user  
  
-   database role  
  
-   application role  
  
-   database user mapped to a Windows login  
  
-   database user mapped to a Windows group  
  
-   database user mapped to a certificate  
  
-   database user mapped to an asymmetric key  
  
-   database user not mapped to a server principal.  
  
## Remarks  
 A database scoped credential is a database-level securable contained by the database that is its parent in the permissions hierarchy. The most specific and limited permissions that can be denied on a database scoped credential are listed below, together with the more general permissions that include them by implication.  
  
|Database scoped credential permission|Implied by database scoped credential permission|Implied by database permission|  
|----------------------------|---------------------------------------|------------------------------------|  
|CONTROL|CONTROL|CONTROL|  
|TAKE OWNERSHIP|CONTROL|CONTROL|  
|ALTER|CONTROL|CONTROL|  
|REFERENCES|CONTROL|REFERENCES|  
|VIEW DEFINITION|CONTROL|VIEW DEFINITION|  
  
## Permissions  
 Requires CONTROL permission on the database scoped credential. If the AS clause is used, the specified principal must own the database scoped credential.  
  
## See Also  
 [DENY &#40;Transact-SQL&#41;](../../t-sql/statements/deny-transact-sql.md)   
 [GRANT database scoped credential (Transact-SQL)](../../t-sql/statements/grant-database-scoped-credential-transact-sql.md)   
 [REVOKE database scoped credential (Transact-SQL)](../../t-sql/statements/revoke-database-scoped-credential-transact-sql.md)   
 [Permissions &#40;Database Engine&#41;](../../relational-databases/security/permissions-database-engine.md)   
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
