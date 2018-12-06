---
title: "sys.dm_cryptographic_provider_keys (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_cryptographic_provider_keys_TSQL"
  - "dm_cryptographic_provider_keys_TSQL"
  - "dm_cryptographic_provider_keys"
  - "sys.dm_cryptographic_provider_keys"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_cryptographic_provider_keys dynamic management function"
ms.assetid: 5a8c1421-c56b-44b5-96e5-4f01782a0c7c
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_cryptographic_provider_keys (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about the keys provided by a Extensible Key Management (EKM) provider.  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
dm_cryptographic_provider_keys ( provider_id )  
```  
  
## Arguments  
 *provider_id*  
 Identification number of the EKM provider, with no default.  
  
## Tables Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**key_id**|**int**|Identification number of the key on the provider.|  
|**key_name**|**nvarchar(512)**|Name of the key on the provider.|  
|**key_thumbprint**|**varbinary(32)**|Thumbprint from the provider of the key.|  
|**algorithm_id**|**int**|Identification number of the algorithm on the provider.|  
|**algorithm_tag**|**int**|Tag of the algorithm on the provider.|  
|**key_type**|**nchar(256)**|Type of key on the provider.|  
|**key_length**|**int**|Length of the key on the provider.|  
  
## Permissions  
 When this view is queried it will authenticate the user context with the provider and enumerate all keys visible to the user.  
  
 If the user cannot authenticate with the EKM provider, no key information will be returned.  
  
## Examples  
 The following example shows the key properties for a provider with the identification number of `1234567`.  
  
```  
SELECT * FROM sys.dm_cryptographic_provider_keys(1234567);  
GO  
```  
  
## See Also  
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md)  
  
  
