---
title: "Security stored procedures (Transact-SQL)"
description: "Security stored procedures (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "system stored procedures [SQL Server], security"
  - "stored procedures [SQL Server], security"
  - "security [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Security stored procedures (Transact-SQL)

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports the following system stored procedures that are used to manage security. Some of these stored procedures are deprecated, but continue to be available to support backward compatibility. The topics for deprecated procedures will list their replacement.

:::row:::
    :::column:::
        [sys.sp_add_trusted_assembly]( sys-sp-add-trusted-assembly-transact-sql.md)  
    :::column-end:::
    :::column:::
        [sp_addapprole](sp-addapprole-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_addlinkedserver](sp-addlinkedserver-transact-sql.md)
    :::column-end:::
    :::column:::
        [sp_addlinkedsrvlogin](sp-addlinkedsrvlogin-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_addlogin](sp-addlogin-transact-sql.md) (Deprecated)  
    :::column-end:::
    :::column:::
        [sp_addremotelogin](sp-addremotelogin-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_addrole](sp-addrole-transact-sql.md) (Deprecated)  
    :::column-end:::
    :::column:::
        [sp_addrolemember](sp-addrolemember-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_addserver](sp-addserver-transact-sql.md) (Deprecated)  
    :::column-end:::
    :::column:::
        [sp_addsrvrolemember](sp-addsrvrolemember-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_adduser](sp-adduser-transact-sql.md) (Deprecated)  
    :::column-end:::
    :::column:::
        [sp_approlepassword](sp-approlepassword-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_audit_write](sp-audit-write-transact-sql.md)  
    :::column-end:::
    :::column:::
        [sp_change_users_login](sp-change-users-login-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_changedbowner](sp-changedbowner-transact-sql.md) (Deprecated)  
    :::column-end:::
    :::column:::
        [sp_changeobjectowner](sp-changeobjectowner-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_control_dbmasterkey_password](sp-control-dbmasterkey-password-transact-sql.md)  
    :::column-end:::
    :::column:::
        [sp_copy_data_in_batches](sys-sp-copy-data-in-batches-transact-sql.md)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_dbfixedrolepermission](sp-dbfixedrolepermission-transact-sql.md) (Deprecated)
    :::column-end:::
    :::column:::
        [sp_defaultdb](sp-defaultdb-transact-sql.md) (Deprecated)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_defaultlanguage](sp-defaultlanguage-transact-sql.md) (Deprecated)
    :::column-end:::
    :::column:::
        [sp_denylogin](sp-denylogin-transact-sql.md) (Deprecated)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_describe_parameter_encryption](sp-describe-parameter-encryption-transact-sql.md)
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
        [sp_dropapprole](sp-dropapprole-transact-sql.md) (Deprecated)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_droplinkedsrvlogin](sp-droplinkedsrvlogin-transact-sql.md)  
    :::column-end:::
    :::column:::
        [sp_droplogin](sp-droplogin-transact-sql.md) (Deprecated)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_dropremotelogin](sp-dropremotelogin-transact-sql.md) (Deprecated)  
    :::column-end:::
    :::column:::
        [sp_droprole](sp-droprole-transact-sql.md) (Deprecated)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_droprolemember](sp-droprolemember-transact-sql.md) (Deprecated)  
    :::column-end:::
    :::column:::
        [sp_dropserver](sp-dropserver-transact-sql.md)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_dropsrvrolemember](sp-dropsrvrolemember-transact-sql.md) (Deprecated)  
    :::column-end:::
    :::column:::
        [sp_dropuser](sp-dropuser-transact-sql.md) (Deprecated)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_generate_database_ledger_digest](sys-sp-generate-database-ledger-digest-transact-sql.md)  
    :::column-end:::
    :::column:::
        [sp_external_policy_refresh](sp-external-policy-refresh-transact-sql.md)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_grantdbaccess](sp-grantdbaccess-transact-sql.md) (Deprecated)  
    :::column-end:::
    :::column:::
        [sp_grantlogin](sp-grantlogin-transact-sql.md) (Deprecated)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_helpdbfixedrole](sp-helpdbfixedrole-transact-sql.md)  
    :::column-end:::
    :::column:::
        [sp_helplinkedsrvlogin](sp-helplinkedsrvlogin-transact-sql.md)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_helplogins](sp-helplogins-transact-sql.md)  
    :::column-end:::
    :::column:::
        [sp_helpntgroup](sp-helpntgroup-transact-sql.md)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_helpremotelogin](sp-helpremotelogin-transact-sql.md) (Deprecated)  
    :::column-end:::
    :::column:::
        [sp_helprole](sp-helprole-transact-sql.md)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_helprolemember](sp-helprolemember-transact-sql.md)  
    :::column-end:::
    :::column:::
        [sp_helprotect](sp-helprotect-transact-sql.md) (Deprecated)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_helpsrvrole](sp-helpsrvrole-transact-sql.md)  
    :::column-end:::
    :::column:::
        [sp_helpsrvrolemember](sp-helpsrvrolemember-transact-sql.md)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_helpuser](sp-helpuser-transact-sql.md) (Deprecated)  
    :::column-end:::
    :::column:::
        [sp_migrate_user_to_contained](sp-migrate-user-to-contained-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_MShasdbaccess](sp-mshasdbaccess-transact-sql.md)  
    :::column-end:::
    :::column:::
        [sp_password](sp-password-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_refresh_parameter_encryption](sp-refresh-parameter-encryption-transact-sql.md)  
    :::column-end:::
    :::column:::
        [sp_remoteoption](sp-remoteoption-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_revokedbaccess](sp-revokedbaccess-transact-sql.md) (Deprecated)  
    :::column-end:::
    :::column:::
        [sp_revokelogin](sp-revokelogin-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_setapprole](sp-setapprole-transact-sql.md)  
    :::column-end:::
    :::column:::
        [sp_srvrolepermission](sp-srvrolepermission-transact-sql.md) (Deprecated)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_testlinkedserver](sp-testlinkedserver-transact-sql.md)  
    :::column-end:::
    :::column:::
        [sp_unsetapprole](sp-unsetapprole-transact-sql.md)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_validatelogins](sp-validatelogins-transact-sql.md)  
    :::column-end:::
    :::column:::
        [sp_verify_database_ledger](sys-sp-verify-database-ledger-transact-sql.md)  
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sp_verify_database_ledger_from_digest_storage](sys-sp-verify-database-ledger-from-digest-storage-transact-sql.md)  
    :::column-end:::
    :::column:::
        [sp_xp_cmdshell_proxy_account](sp-xp-cmdshell-proxy-account-transact-sql.md)  
    :::column-end:::
:::row-end:::

&nbsp;

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Security Functions (Transact-SQL)](../../t-sql/functions/security-functions-transact-sql.md)
