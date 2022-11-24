---
description: "Security Stored Procedures (Transact-SQL)"
title: "Security Stored Procedures (Transact-SQL) | Microsoft Docs"
ms.custom:
- event-tier1-build-2022
ms.date: 05/24/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "system stored procedures [SQL Server], security"
  - "stored procedures [SQL Server], security"
  - "security [SQL Server], stored procedures"
ms.assetid: 62b72907-7e95-4c97-9891-0c45d5b678ce
author: VanMSFT
ms.author: vanto
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Security Stored Procedures (Transact-SQL)

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports the following system stored procedures that are used to manage security. Some of these stored procedures are deprecated but continue to be available to support backward compatibility. The topics for deprecated procedures will list their replacement.  

:::row:::
    :::column:::
        [sys.sp_add_trusted_assembly]( sys-sp-add-trusted-assembly-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_addapprole](../../relational-databases/system-stored-procedures/sp-addapprole-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_addlinkedserver](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_addlinkedsrvlogin](../../relational-databases/system-stored-procedures/sp-addlinkedsrvlogin-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_addlogin](../../relational-databases/system-stored-procedures/sp-addlogin-transact-sql.md) (Deprecated) 
    :::column-end:::
    :::column:::
        [sp_addremotelogin](../../relational-databases/system-stored-procedures/sp-addremotelogin-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_addrole](../../relational-databases/system-stored-procedures/sp-addrole-transact-sql.md) (Deprecated) 
    :::column-end:::
    :::column:::
        [sp_addrolemember](../../relational-databases/system-stored-procedures/sp-addrolemember-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_addserver](../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md) (Deprecated) 
    :::column-end:::
    :::column:::
        [sp_addsrvrolemember](../../relational-databases/system-stored-procedures/sp-addsrvrolemember-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_adduser](../../relational-databases/system-stored-procedures/sp-adduser-transact-sql.md) (Deprecated) 
    :::column-end:::
    :::column:::
        [sp_approlepassword](../../relational-databases/system-stored-procedures/sp-approlepassword-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_audit_write](../../relational-databases/system-stored-procedures/sp-audit-write-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_change_users_login](../../relational-databases/system-stored-procedures/sp-change-users-login-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_changedbowner](../../relational-databases/system-stored-procedures/sp-changedbowner-transact-sql.md) (Deprecated) 
    :::column-end:::
    :::column:::
        [sp_changeobjectowner](../../relational-databases/system-stored-procedures/sp-changeobjectowner-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_control_dbmasterkey_password](../../relational-databases/system-stored-procedures/sp-control-dbmasterkey-password-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_copy_data_in_batches](../../relational-databases/system-stored-procedures/sys-sp-copy-data-in-batches-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_dbfixedrolepermission](../../relational-databases/system-stored-procedures/sp-dbfixedrolepermission-transact-sql.md) (Deprecated)
    :::column-end:::
    :::column:::
        [sp_defaultdb](../../relational-databases/system-stored-procedures/sp-defaultdb-transact-sql.md) (Deprecated) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_defaultlanguage](../../relational-databases/system-stored-procedures/sp-defaultlanguage-transact-sql.md) (Deprecated)
    :::column-end:::
    :::column:::
        [sp_denylogin](../../relational-databases/system-stored-procedures/sp-denylogin-transact-sql.md) (Deprecated) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_describe_parameter_encryption](../../relational-databases/system-stored-procedures/sp-describe-parameter-encryption-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_dropalias](./system-stored-procedures-transact-sql.md) (Deprecated) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.sp_drop_trusted_assembly]( sys-sp-drop-trusted-assembly-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_dropapprole](../../relational-databases/system-stored-procedures/sp-dropapprole-transact-sql.md) (Deprecated) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_droplinkedsrvlogin](../../relational-databases/system-stored-procedures/sp-droplinkedsrvlogin-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_droplogin](../../relational-databases/system-stored-procedures/sp-droplogin-transact-sql.md) (Deprecated) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_dropremotelogin](../../relational-databases/system-stored-procedures/sp-dropremotelogin-transact-sql.md) (Deprecated) 
    :::column-end:::
    :::column:::
        [sp_droprole](../../relational-databases/system-stored-procedures/sp-droprole-transact-sql.md) (Deprecated) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_droprolemember](../../relational-databases/system-stored-procedures/sp-droprolemember-transact-sql.md) (Deprecated) 
    :::column-end:::
    :::column:::
        [sp_dropserver](../../relational-databases/system-stored-procedures/sp-dropserver-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_dropsrvrolemember](../../relational-databases/system-stored-procedures/sp-dropsrvrolemember-transact-sql.md) (Deprecated) 
    :::column-end:::
    :::column:::
        [sp_dropuser](../../relational-databases/system-stored-procedures/sp-dropuser-transact-sql.md) (Deprecated) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_generate_database_ledger_digest](../../relational-databases/system-stored-procedures/sys-sp-generate-database-ledger-digest-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_external_policy_refresh](../../relational-databases/system-stored-procedures/sp-external-policy-refresh-transact-sql.md)      
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_grantdbaccess](../../relational-databases/system-stored-procedures/sp-grantdbaccess-transact-sql.md) (Deprecated) 
    :::column-end:::
    :::column:::
        [sp_grantlogin](../../relational-databases/system-stored-procedures/sp-grantlogin-transact-sql.md) (Deprecated) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_helpdbfixedrole](../../relational-databases/system-stored-procedures/sp-helpdbfixedrole-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_helplinkedsrvlogin](../../relational-databases/system-stored-procedures/sp-helplinkedsrvlogin-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_helplogins](../../relational-databases/system-stored-procedures/sp-helplogins-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_helpntgroup](../../relational-databases/system-stored-procedures/sp-helpntgroup-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_helpremotelogin](../../relational-databases/system-stored-procedures/sp-helpremotelogin-transact-sql.md) (Deprecated) 
    :::column-end:::
    :::column:::
        [sp_helprole](../../relational-databases/system-stored-procedures/sp-helprole-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_helprolemember](../../relational-databases/system-stored-procedures/sp-helprolemember-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_helprotect](../../relational-databases/system-stored-procedures/sp-helprotect-transact-sql.md) (Deprecated) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_helpsrvrole](../../relational-databases/system-stored-procedures/sp-helpsrvrole-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_helpsrvrolemember](../../relational-databases/system-stored-procedures/sp-helpsrvrolemember-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_helpuser](../../relational-databases/system-stored-procedures/sp-helpuser-transact-sql.md) (Deprecated) 
    :::column-end:::
    :::column:::
        [sp_migrate_user_to_contained](../../relational-databases/system-stored-procedures/sp-migrate-user-to-contained-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_MShasdbaccess](../../relational-databases/system-stored-procedures/sp-mshasdbaccess-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_password](../../relational-databases/system-stored-procedures/sp-password-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_refresh_parameter_encryption](../../relational-databases/system-stored-procedures/sp-refresh-parameter-encryption-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_remoteoption](../../relational-databases/system-stored-procedures/sp-remoteoption-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_revokedbaccess](../../relational-databases/system-stored-procedures/sp-revokedbaccess-transact-sql.md) (Deprecated) 
    :::column-end:::
    :::column:::
        [sp_revokelogin](../../relational-databases/system-stored-procedures/sp-revokelogin-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_setapprole](../../relational-databases/system-stored-procedures/sp-setapprole-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_srvrolepermission](../../relational-databases/system-stored-procedures/sp-srvrolepermission-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_testlinkedserver](../../relational-databases/system-stored-procedures/sp-testlinkedserver-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_unsetapprole](../../relational-databases/system-stored-procedures/sp-unsetapprole-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_validatelogins](../../relational-databases/system-stored-procedures/sp-validatelogins-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_verify_database_ledger](../../relational-databases/system-stored-procedures/sys-sp-verify-database-ledger-transact-sql.md) 
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_verify_database_ledger_from_digest_storage](../../relational-databases/system-stored-procedures/sys-sp-verify-database-ledger-from-digest-storage-transact-sql.md) 
    :::column-end:::
    :::column:::
        [sp_xp_cmdshell_proxy_account](../../relational-databases/system-stored-procedures/sp-xp-cmdshell-proxy-account-transact-sql.md) 
    :::column-end:::
:::row-end:::

&nbsp;

## See Also

- [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
- [Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)  
