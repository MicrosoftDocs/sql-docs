---
title: "Security Functions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "functions [SQL Server], security"
  - "security functions"
  - "roles [SQL Server], functions"
  - "users [SQL Server], security functions"
  - "security [SQL Server], functions"
ms.assetid: 7773a87d-2f1b-4951-a225-baf159a7291b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Security Functions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  The following functions return information that is useful in managing security. Additional functions are listed under [Cryptographic Functions &#40;Transact-SQL&#41;](../../t-sql/functions/cryptographic-functions-transact-sql.md).  
  
|||  
|-|-|  
|[CERTENCODED &#40;Transact-SQL&#41;](../../t-sql/functions/certencoded-transact-sql.md)|[PWDCOMPARE &#40;Transact-SQL&#41;](../../t-sql/functions/pwdcompare-transact-sql.md)|  
|[CERTPRIVATEKEY &#40;Transact-SQL&#41;](../../t-sql/functions/certprivatekey-transact-sql.md)|[PWDENCRYPT &#40;Transact-SQL&#41;](../../t-sql/functions/pwdencrypt-transact-sql.md)|  
|[CURRENT_USER &#40;Transact-SQL&#41;](../../t-sql/functions/current-user-transact-sql.md)|[SCHEMA_ID &#40;Transact-SQL&#41;](../../t-sql/functions/schema-id-transact-sql.md)|  
|[DATABASE_PRINCIPAL_ID &#40;Transact-SQL&#41;](../../t-sql/functions/database-principal-id-transact-sql.md)|[SCHEMA_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/schema-name-transact-sql.md)|  
|[sys.fn_builtin_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql.md)|[SESSION_USER &#40;Transact-SQL&#41;](../../t-sql/functions/session-user-transact-sql.md)|  
|[sys.fn_get_audit_file &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md)|[SUSER_ID &#40;Transact-SQL&#41;](../../t-sql/functions/suser-id-transact-sql.md)|  
|[sys.fn_my_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-my-permissions-transact-sql.md)|[SUSER_SID &#40;Transact-SQL&#41;](../../t-sql/functions/suser-sid-transact-sql.md)|  
|[HAS_PERMS_BY_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/has-perms-by-name-transact-sql.md)|[SUSER_SNAME &#40;Transact-SQL&#41;](../../t-sql/functions/suser-sname-transact-sql.md)|  
|[IS_MEMBER &#40;Transact-SQL&#41;](../../t-sql/functions/is-member-transact-sql.md)|[SYSTEM_USER &#40;Transact-SQL&#41;](../../t-sql/functions/system-user-transact-sql.md)|  
|[IS_ROLEMEMBER &#40;Transact-SQL&#41;](../../t-sql/functions/is-rolemember-transact-sql.md)|[SUSER_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/suser-name-transact-sql.md)|  
|[IS_SRVROLEMEMBER &#40;Transact-SQL&#41;](../../t-sql/functions/is-srvrolemember-transact-sql.md)|[USER_ID &#40;Transact-SQL&#41;](../../t-sql/functions/user-id-transact-sql.md)|  
|[ORIGINAL_LOGIN &#40;Transact-SQL&#41;](../../t-sql/functions/original-login-transact-sql.md)|[USER_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/user-name-transact-sql.md)|  
|[PERMISSIONS &#40;Transact-SQL&#41;](../../t-sql/functions/permissions-transact-sql.md)||  
  
 For information about membership in Windows groups, see [xp_logininfo &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/xp-logininfo-transact-sql.md) and [xp_enumgroups &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/xp-enumgroups-transact-sql.md).  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [Cryptographic Functions &#40;Transact-SQL&#41;](../../t-sql/functions/cryptographic-functions-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [Security Statements](https://msdn.microsoft.com/library/aebe2ec7-31bc-4697-a493-dcfcd0903a7b)  
  
  
