---
title: "sys.dm_cryptographic_provider_properties (Transact-SQL)"
description: sys.dm_cryptographic_provider_properties (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_cryptographic_provider_properties_TSQL"
  - "sys.dm_cryptographic_provider_properties"
  - "sys.dm_cryptographic_provider_properties_TSQL"
  - "dm_cryptographic_provider_properties"
helpviewer_keywords:
  - "sys.dm_cryptographic_provider_properties dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 024b0095-6766-4189-a39a-d316c5ec2874
---
# sys.dm_cryptographic_provider_properties (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns information about registered cryptographic providers.  
  
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|provider_id|**int**|Identification number of the cryptographic provider.|  
|guid|**uniqueidentifier**|Unique provider GUID.|  
|provider_version|**nvarchar(256)**|Version of the provider in the format '*aa.bb.cccc.dd*'.|  
|sqlcrypt_version|**nvarchar(256)**|Major version of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Cryptographic API in the format '*aa.bb.cccc.dd*'.|  
|friendly_name|**nvarchar(2048)**|Name supplied by the provider.|  
|authentication_type|**nvarchar(256)**|`WINDOWS`, `BASIC`, or `OTHER`.|  
|symmetric_key_support|**tinyint**|0 (not supported)<br /><br /> 1 (supported)|  
|symmetric_key_export|**tinyint**|0 (not supported)<br /><br /> 1 (supported)|  
|symmetric_key_import|**tinyint**|0 (not supported)<br /><br /> 1 (supported)|  
|symmetric_key_persistance|**tinyint**|0 (not supported)<br /><br /> 1 (supported)|  
|asymmetric_key_support|**tinyint**|0 (not supported)<br /><br /> 1 (supported)|  
|asymmetric_key_export|**tinyint**|0 (not supported)<br /><br /> 1 (supported)|  
|symmetric_key_import|**tinyint**|0 (not supported)<br /><br /> 1 (supported)|  
|symmetric_key_persistance|**tinyint**|0 (not supported)<br /><br /> 1 (supported)|  
  
## Permissions  
 The **sys.dm_cryptographic_provider_properties** view is visible to the public.  
  
## See Also  
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md)   
 [CREATE CRYPTOGRAPHIC PROVIDER &#40;Transact-SQL&#41;](../../t-sql/statements/create-cryptographic-provider-transact-sql.md)   
 [Security-Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/security-related-dynamic-management-views-and-functions-transact-sql.md)  
  
  
