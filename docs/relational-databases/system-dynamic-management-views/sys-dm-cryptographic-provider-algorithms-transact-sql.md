---
title: "sys.dm_cryptographic_provider_algorithms (Transact-SQL)"
description: sys.dm_cryptographic_provider_algorithms (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_cryptographic_provider_algorithms_TSQL"
  - "sys.dm_cryptographic_provider_algorithms"
  - "sys.dm_cryptographic_provider_algorithms_TSQL"
  - "dm_cryptographic_provider_algorithms"
helpviewer_keywords:
  - "sys.dm_cryptographic_provider_algorithms dynamic management function"
dev_langs:
  - "TSQL"
ms.assetid: 8bcccb37-5cfb-4e1e-a0bb-7ff4c279fe8e
---
# sys.dm_cryptographic_provider_algorithms (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the algorithms supported by an Extensible Key Management (EKM) provider.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.dm_cryptographic_provider_algorithms ( provider_id )  
```  
  
## Arguments  
 *provider_id*  
 Identification number of the EKM provider, with no default.  
  
## Tables Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|algorithm_id|**int**|Is the identification number of the algorithm.|  
|algorithm_tag|**nvarchar(60)**|Is the identification tag of the algorithm.|  
|key_type|**nvarchar(128)**|Shows the key type. Returns either ASYMMETRIC KEY or SYMMETRIC KEY.|  
|key_length|**int**|Indicates the key length in bits.|  
  
## Permissions  
 The user must be a member of the public database role.  
  
## Examples  
 The following example shows the provider options for a provider with the identification number of `1234567`.  
  
```  
SELECT * FROM sys.dm_cryptographic_provider_algorithms(1234567);  
GO  
```  
  
## See Also  
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md)   
 [Security-Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/security-related-dynamic-management-views-and-functions-transact-sql.md)  
  
  
