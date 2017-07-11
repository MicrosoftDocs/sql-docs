---
title: "sys.column_master_keys (Transact-SQL) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/10/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
applies_to: 
  - "Azure SQL Database"
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "column_master_key_definitions_TSQL"
  - "column_master_key_definitions"
  - "sys.column_master_key_definitions_TSQL"
  - "sys.column_master_key_definitions"
  - "column_master_keys_TSQL"
  - "column_master_keys"
  - "sys.column_master_keys_TSQL"
  - "sys.column_master_keys"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.column_master_key_definitions catalog view"
  - "sys.column_master_keys catalog view"
ms.assetid: fbec2efa-5fe9-4121-9b34-60497b0b2aca
caps.latest.revision: 14
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.column_master_keys (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Returns a row for each database master key added by using the [CREATE MASTER KEY](../../t-sql/statements/create-column-master-key-transact-sql.md) statement. Each row represents a single column master key (CMK).  
    
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|The name of the CMK.|  
|**column_master_key_id**|**int**|ID of the column master key.|  
|**create_date**|**datetime**|Date the column master key was created.|  
|**modify_date**|**datetime**|Date the column master key was last modified.|  
|**key_store_provider_name**|**sysname**|Name of the provider for the column master key store that contains the CMK. Allowed values are:<br /><br /> MSSQL_CERTIFICATE_STORE – If the column master key store is a Certificate Store.<br /><br /> A user-defined value, if the column master key store is of a custom type.|  
|**key_path**|**nvarchar(4000)**|A column master key store-specific path of the key. The format of the path depends on the column master key store type. Example:<br /><br /> `'CurrentUser/Personal/'<thumbprint>`<br /><br /> For a custom column master key store, the developer is responsible for defining what a key path is for the custom column master key store.|  
  
## Permissions  
 Requires the **VIEW ANY COLUMN MASTER KEY** permission.  
  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [CREATE COLUMN MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-column-master-key-transact-sql.md)   
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Always Encrypted &#40;Database Engine&#41;](../../relational-databases/security/encryption/always-encrypted-database-engine.md)   
 [sys.column_encryption_key_values &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-encryption-key-values-transact-sql.md)  
  
  
