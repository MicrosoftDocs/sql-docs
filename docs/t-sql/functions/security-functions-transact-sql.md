---
title: "Security Functions (Transact-SQL)"
description: "Security Functions (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: ""
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
helpviewer_keywords:
  - "functions [SQL Server], security"
  - "security functions"
  - "roles [SQL Server], functions"
  - "users [SQL Server], security functions"
  - "security [SQL Server], functions"
dev_langs:
  - "TSQL"
---
# Security Functions (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  The following functions return information that is useful in managing security. Additional functions are listed under [Cryptographic Functions &#40;Transact-SQL&#41;](../../t-sql/functions/cryptographic-functions-transact-sql.md).  
  
:::row:::
    :::column:::
        [CERTENCODED &#40;Transact-SQL&#41;](../../t-sql/functions/certencoded-transact-sql.md)
    :::column-end:::
    :::column:::
        [PWDCOMPARE &#40;Transact-SQL&#41;](../../t-sql/functions/pwdcompare-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [CERTPRIVATEKEY &#40;Transact-SQL&#41;](../../t-sql/functions/certprivatekey-transact-sql.md)
    :::column-end:::
    :::column:::
        [PWDENCRYPT &#40;Transact-SQL&#41;](../../t-sql/functions/pwdencrypt-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [CURRENT_USER &#40;Transact-SQL&#41;](../../t-sql/functions/current-user-transact-sql.md)
    :::column-end:::
    :::column:::
        [SCHEMA_ID &#40;Transact-SQL&#41;](../../t-sql/functions/schema-id-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [DATABASE_PRINCIPAL_ID &#40;Transact-SQL&#41;](../../t-sql/functions/database-principal-id-transact-sql.md)
    :::column-end:::
    :::column:::
        [SCHEMA_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/schema-name-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.fn_builtin_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql.md)
    :::column-end:::
    :::column:::
        [SESSION_USER &#40;Transact-SQL&#41;](../../t-sql/functions/session-user-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.fn_get_audit_file &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-get-audit-file-transact-sql.md)
    :::column-end:::
    :::column:::
        [SUSER_ID &#40;Transact-SQL&#41;](../../t-sql/functions/suser-id-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.fn_my_permissions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-my-permissions-transact-sql.md)
    :::column-end:::
    :::column:::
        [SUSER_SID &#40;Transact-SQL&#41;](../../t-sql/functions/suser-sid-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [HAS_PERMS_BY_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/has-perms-by-name-transact-sql.md)
    :::column-end:::
    :::column:::
        [SUSER_SNAME &#40;Transact-SQL&#41;](../../t-sql/functions/suser-sname-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [IS_MEMBER &#40;Transact-SQL&#41;](../../t-sql/functions/is-member-transact-sql.md)
    :::column-end:::
    :::column:::
        [SYSTEM_USER &#40;Transact-SQL&#41;](../../t-sql/functions/system-user-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [IS_ROLEMEMBER &#40;Transact-SQL&#41;](../../t-sql/functions/is-rolemember-transact-sql.md)
    :::column-end:::
    :::column:::
        [SUSER_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/suser-name-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [IS_SRVROLEMEMBER &#40;Transact-SQL&#41;](../../t-sql/functions/is-srvrolemember-transact-sql.md)
    :::column-end:::
    :::column:::
        [USER_ID &#40;Transact-SQL&#41;](../../t-sql/functions/user-id-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [LOGINPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/loginproperty-transact-sql.md)
    :::column-end:::
    :::column:::
        [USER_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/user-name-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [ORIGINAL_LOGIN &#40;Transact-SQL&#41;](../../t-sql/functions/original-login-transact-sql.md)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [PERMISSIONS &#40;Transact-SQL&#41;](../../t-sql/functions/permissions-transact-sql.md)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::

&nbsp;
  
 For information about membership in Windows groups, see [xp_logininfo &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/xp-logininfo-transact-sql.md) and [xp_enumgroups &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/xp-enumgroups-transact-sql.md).  
  
## See Also  
 [Security Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/security-stored-procedures-transact-sql.md)   
 [Cryptographic Functions &#40;Transact-SQL&#41;](../../t-sql/functions/cryptographic-functions-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [Security Statements](../statements/permissions-grant-deny-revoke-azure-sql-data-warehouse-parallel-data-warehouse.md)  
  
