---
title: "sys.column_encryption_keys  (Transact-SQL)"
description: sys.column_encryption_keys  (Transact-SQL)
author: jaszymas
ms.author: jaszymas
ms.date: "10/15/2019"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.column_encryption_keys"
  - "column_encryption_keys_TSQL"
  - "sys.column_encryption_keys_TSQL"
  - "column_encryption_keys"
helpviewer_keywords:
  - "sys.column_encryption_keys catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sys.column_encryption_keys  (Transact-SQL)

[!INCLUDE [sqlserver2016-asa](../../includes/applies-to-version/sqlserver2016-asa.md)]

  Returns information about column encryption keys (CEKs) created with the [CREATE COLUMN ENCRYPTION KEY](../../t-sql/statements/create-column-encryption-key-transact-sql.md) statement. Each row represents a CEK.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|The name of the CEK.|  
|**column_encryption_key_id**|**int**|ID of the CEK.|  
|**create_date**|**datetime**|Date the CEK was created.|  
|**modify_date**|**datetime**|Date the CEK was last modified.|  
  
## Permissions  
 Requires the **VIEW ANY COLUMN ENCRYPTION KEY** permission.  
  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Related content

- [CREATE COLUMN ENCRYPTION KEY (Transact-SQL)](../../t-sql/statements/create-column-encryption-key-transact-sql.md)
- [ALTER COLUMN ENCRYPTION KEY (Transact-SQL)](../../t-sql/statements/alter-column-encryption-key-transact-sql.md)
- [DROP COLUMN ENCRYPTION KEY (Transact-SQL)](../../t-sql/statements/drop-column-encryption-key-transact-sql.md)
- [CREATE COLUMN MASTER KEY (Transact-SQL)](../../t-sql/statements/create-column-master-key-transact-sql.md)
- [Security Catalog Views (Transact-SQL)](security-catalog-views-transact-sql.md)
- [sys.column_encryption_key_values (Transact-SQL)](sys-column-encryption-key-values-transact-sql.md)
- [Always Encrypted](../security/encryption/always-encrypted-database-engine.md)
- [Always Encrypted with secure enclaves](../security/encryption/always-encrypted-enclaves.md)
- [Overview of key management for Always Encrypted](../security/encryption/overview-of-key-management-for-always-encrypted.md)
- [Manage keys for Always Encrypted with secure enclaves](../security/encryption/always-encrypted-enclaves-manage-keys.md)
