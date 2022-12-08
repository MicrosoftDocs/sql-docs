---
title: "Security Catalog Views (Transact-SQL)"
description: Security Catalog Views (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "05/24/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom: event-tier1-build-2022
helpviewer_keywords:
  - "cryptography [SQL Server], catalog views"
  - "encryption [SQL Server], catalog views"
  - "catalog views [SQL Server], security"
  - "security catalog views [SQL Server]"
dev_langs:
  - "TSQL"
ms.assetid: 4d5cf1bf-09a7-4ee0-9dbb-5c584750fc67
---
# Security Catalog Views (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Security information is exposed in catalog views that are optimized for performance and utility. When possible, use the following catalog views to access catalog metadata.  
  
## Database-Level Views   
  
:::row:::
    :::column:::
        [sys.database_permissions](../../relational-databases/system-catalog-views/sys-database-permissions-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.database_scoped_credentials](../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.database_principals](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.master_key_passwords](../../relational-databases/system-catalog-views/sys-master-key-passwords-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.database_role_members](../../relational-databases/system-catalog-views/sys-database-role-members-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.user_token](../../relational-databases/system-catalog-views/sys-user-token-transact-sql.md)
    :::column-end:::
:::row-end:::

&nbsp;

## Server-Level Views  

:::row:::
    :::column:::
        [sys.credentials](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.login_token](../../relational-databases/system-catalog-views/sys-login-token-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.server_role_members](../../relational-databases/system-catalog-views/sys-server-role-members-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.securable_classes](../../relational-databases/system-catalog-views/sys-securable-classes-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.sql_logins](../../relational-databases/system-catalog-views/sys-sql-logins-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.server_permissions](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.system_components_surface_area_configuration](../../relational-databases/system-catalog-views/sys-system-components-surface-area-configuration-transact-sql.md)
    :::column-end:::
:::row-end:::

&nbsp;
  
## Encryption Views  
  
:::row:::
    :::column:::
        [sys.asymmetric_keys](../../relational-databases/system-catalog-views/sys-asymmetric-keys-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.cryptographic_providers](../../relational-databases/system-catalog-views/sys-cryptographic-providers-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.certificates](../../relational-databases/system-catalog-views/sys-certificates-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.key_encryptions](../../relational-databases/system-catalog-views/sys-key-encryptions-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.column_encryption_key_values](../../relational-databases/system-catalog-views/sys-column-encryption-key-values-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.openkeys](../../relational-databases/system-catalog-views/sys-openkeys-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.column_encryption_keys](../../relational-databases/system-catalog-views/sys-column-encryption-keys-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.security_policies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-security-policies-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.column_master_keys](../../relational-databases/system-catalog-views/sys-column-master-keys-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.security_predicates &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-security-predicates-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.crypt_properties](../../relational-databases/system-catalog-views/sys-crypt-properties-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.symmetric_keys](../../relational-databases/system-catalog-views/sys-symmetric-keys-transact-sql.md)
    :::column-end:::
:::row-end:::

&nbsp;

## SQL Server Audit Views  
  
:::row:::
    :::column:::
        [sys.server_audits](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.server_file_audits](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.server_audit_specifications](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.server_audit_specifications_details](../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.database_audit_specifications](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.database_audit_specification_details](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)
    :::column-end:::
:::row-end:::

## Ledger Views

:::row:::
    :::column:::
        [sys.database_ledger_transactions](../../relational-databases/system-catalog-views/sys-database-ledger-transactions-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.database_ledger_blocks](../../relational-databases/system-catalog-views/sys-database-ledger-blocks-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.ledger_table_history](../../relational-databases/system-catalog-views/sys-ledger-table-history-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.ledger_column_history](../../relational-databases/system-catalog-views/sys-ledger-column-history-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.database_ledger_digest_locations](../../relational-databases/system-catalog-views/sys-database-ledger-digest-locations-transact-sql.md)
    :::column-end:::
:::row-end:::

&nbsp;

## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Security Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)   
 [Security-Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/security-related-dynamic-management-views-and-functions-transact-sql.md)  
  
  
