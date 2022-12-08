---
title: "sys.column_encryption_key_values (Transact-SQL)"
description: sys.column_encryption_key_values (Transact-SQL)
author: jaszymas
ms.author: jaszymas
ms.date: "10/15/2019"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "column_encryption_key_values"
  - "column_encryption_key_values_TSQL"
  - "sys.column_encryption_key_values"
  - "sys.column_encryption_key_values_TSQL"
helpviewer_keywords:
  - "sys.column_encryption_key_values catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 440875ab-b0e9-4966-8c16-01503558fedd
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.column_encryption_key_values (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns information about encrypted values of column encryption keys (CEKs) created with either the [CREATE COLUMN ENCRYPTION KEY](../../t-sql/statements/create-column-encryption-key-transact-sql.md) or the [ALTER COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-column-encryption-key-transact-sql.md) statement. Each row represents a value of a CEK, encrypted with a column master key (CMK).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**column_encryption_key_id**|**int**|ID of the CEK in the database.|  
|**column_master_key_id**|**int**|ID of the column master key that was used to encrypt the CEK value.|  
|**encrypted_value**|**varbinary(8000)**|CEK value encrypted with the CMK specified in column_master_key_id.|  
|**encryption_algorithm_name**|**sysname**|Name of an algorithm used to encrypt the CEK value.<br /><br /> Name of the encryption algorithm used to encrypt the value. The algorithm for the system providers must be  **RSA_OAEP**.|  
  
## Permissions  
 Requires the **VIEW ANY COLUMN ENCRYPTION KEY** permission.  
  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [CREATE COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-column-encryption-key-transact-sql.md)   
 [ALTER COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-column-encryption-key-transact-sql.md)   
 [DROP COLUMN ENCRYPTION KEY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-column-encryption-key-transact-sql.md)   
 [CREATE COLUMN MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-column-master-key-transact-sql.md)   
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [sys.column_encryption_keys  &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-encryption-keys-transact-sql.md)   
 [sys.column_master_keys &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-master-keys-transact-sql.md)   
 [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)   
 [Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md)   
 [Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves.md)   
 [Overview of Key Management for Always Encrypted](../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md)   
 [Manage keys for Always Encrypted with secure enclaves](../../relational-databases/security/encryption/always-encrypted-enclaves-manage-keys.md)   

  
  
