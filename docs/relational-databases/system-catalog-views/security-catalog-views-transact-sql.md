---
title: "Security Catalog Views (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/27/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "cryptography [SQL Server], catalog views"
  - "encryption [SQL Server], catalog views"
  - "catalog views [SQL Server], security"
  - "security catalog views [SQL Server]"
ms.assetid: 4d5cf1bf-09a7-4ee0-9dbb-5c584750fc67
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Security Catalog Views (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Security information is exposed in catalog views that are optimized for performance and utility. When possible, use the following catalog views to access catalog metadata.  
  
## Database-Level Views  
  
|||  
|-|-|  
|[sys.database_permissions](../../relational-databases/system-catalog-views/sys-database-permissions-transact-sql.md)|[sys.database_scoped_credentials](../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md) |  
|[sys.database_principals](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)|[sys.master_key_passwords](../../relational-databases/system-catalog-views/sys-master-key-passwords-transact-sql.md) |  
|[sys.database_role_members](../../relational-databases/system-catalog-views/sys-database-role-members-transact-sql.md)|[sys.user_token](../../relational-databases/system-catalog-views/sys-user-token-transact-sql.md) |  
  
## Server-Level Views  
  
|||  
|-|-|  
|[sys.credentials](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)|[sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)|  
|[sys.login_token](../../relational-databases/system-catalog-views/sys-login-token-transact-sql.md)|[sys.server_role_members](../../relational-databases/system-catalog-views/sys-server-role-members-transact-sql.md)|  
|[sys.securable_classes](../../relational-databases/system-catalog-views/sys-securable-classes-transact-sql.md)|[sys.sql_logins](../../relational-databases/system-catalog-views/sys-sql-logins-transact-sql.md)|  
|[sys.server_permissions](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md)|[sys.system_components_surface_area_configuration](../../relational-databases/system-catalog-views/sys-system-components-surface-area-configuration-transact-sql.md)|  
  
## Encryption Views  
  
|||  
|-|-|  
|[sys.asymmetric_keys](../../relational-databases/system-catalog-views/sys-asymmetric-keys-transact-sql.md)|[sys.cryptographic_providers](../../relational-databases/system-catalog-views/sys-cryptographic-providers-transact-sql.md)|  
|[sys.certificates](../../relational-databases/system-catalog-views/sys-certificates-transact-sql.md)|[sys.key_encryptions](../../relational-databases/system-catalog-views/sys-key-encryptions-transact-sql.md)|  
|[sys.column_encryption_key_values](../../relational-databases/system-catalog-views/sys-column-encryption-key-values-transact-sql.md)|[sys.openkeys](../../relational-databases/system-catalog-views/sys-openkeys-transact-sql.md)|  
|[sys.column_encryption_keys](../../relational-databases/system-catalog-views/sys-column-encryption-keys-transact-sql.md)|[sys.security_policies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-security-policies-transact-sql.md)|  
|[sys.column_master_key_definitions](../../relational-databases/system-catalog-views/sys-column-master-keys-transact-sql.md)|[sys.security_predicates &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-security-predicates-transact-sql.md)|  
|[sys.crypt_properties](../../relational-databases/system-catalog-views/sys-crypt-properties-transact-sql.md)|[sys.symmetric_keys](../../relational-databases/system-catalog-views/sys-symmetric-keys-transact-sql.md)|  
  
## SQL Server Audit Views  
  
|||  
|-|-|  
|[sys.server_audits](../../relational-databases/system-catalog-views/sys-server-audits-transact-sql.md)|[sys.server_file_audits](../../relational-databases/system-catalog-views/sys-server-file-audits-transact-sql.md)|  
|[sys.server_audit_specifications](../../relational-databases/system-catalog-views/sys-server-audit-specifications-transact-sql.md)|[sys.server_audit_specifications_details](../../relational-databases/system-catalog-views/sys-server-audit-specification-details-transact-sql.md)|  
|[sys.database_ audit_specifications](../../relational-databases/system-catalog-views/sys-database-audit-specifications-transact-sql.md)|[sys.audit_database_specification_details](../../relational-databases/system-catalog-views/sys-database-audit-specification-details-transact-sql.md)|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Security Center for SQL Server Database Engine and Azure SQL Database](../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)   
 [Security-Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/security-related-dynamic-management-views-and-functions-transact-sql.md)  
  
  
