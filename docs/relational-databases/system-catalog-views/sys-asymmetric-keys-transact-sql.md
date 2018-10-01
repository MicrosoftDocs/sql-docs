---
title: "sys.asymmetric_keys (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "asymmetric_keys"
  - "sys.asymmetric_keys_TSQL"
  - "asymmetric_keys_TSQL"
  - "sys.asymmetric_keys"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.asymmetric_keys catalog view"
ms.assetid: bbca796a-9bb5-4a62-9ca8-1d255984553d
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.asymmetric_keys (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns a row for each asymmetric key.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the key. Is unique within the database.|  
|**principal_id**|**int**|ID of the database principal that owns the key.|  
|**asymmetric_key_id**|**int**|ID of the key. Is unique within the database.|  
|**pvt_key_encryption_type**|**char(2)**|How the key is encrypted.<br /><br /> NA = Not encrypted<br /><br /> MK = Key is encrypted by the master key<br /><br /> PW = Key is encrypted by a user-defined password<br /><br /> SK = Key is encrypted by service master key.|  
|**pvt_key_encryption_type_desc**|**nvarchar(60)**|Description of how the private key is encrypted.<br /><br /> NO_PRIVATE_KEY<br /><br /> ENCRYPTED_BY_MASTER_KEY<br /><br /> ENCRYPTED_BY_PASSWORD<br /><br /> ENCRYPTED_BY_SERVICE_MASTER_KEY|  
|**thumbprint**|**varbinary(32)**|SHA-1 hash of the key. The hash is globally unique.|  
|**algorithm**|**char(2)**|Algorithm used with the key.<br /><br /> 1R = 512-bit RSA<br /><br /> 2R = 1024-bit RSA<br /><br /> 3R = 2048-bit RSA|  
|**algorithm_desc**|**nvarchar(60)**|Description of the algorithm used with the key.<br /><br /> RSA_512<br /><br /> RSA_1024<br /><br /> RSA_2048|  
|**key_length**|**int**|Bit length of the key.|  
|**sid**|**varbinary(85)**|Login SID for this key. For Extensible Key Management keys this value will be NULL.|  
|**string_sid**|**nvarchar(128)**|String representation of the login SID of the key. For Extensible Key Management keys this value will be NULL.|  
|**public_key**|**varbinary(max)**|Public key.|  
|**attested_by**|**nvarchar(260)**|System use only.|  
|**provider_type**|**nvarchar(120)**|Type of cryptographic provider:<br /><br /> CRYPTOGRAPHIC PROVIDER = Extensible Key Management keys<br /><br /> NULL = Non-Extensible Key Management keys|  
|**cryptographic_provider_guid**|**uniqueidentifier**|GUID for the cryptographic provider. For non-Extensible Key Management keys this value will be NULL.|  
|**cryptographic_provider_algid**|**sql_variant**|Algorithm ID for the cryptographic provider. For non-Extensible Key Management keys this value will be NULL.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [CREATE ASYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-asymmetric-key-transact-sql.md)  
  
  
