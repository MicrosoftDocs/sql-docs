---
title: "sys.cryptographic_providers (Transact-SQL)"
description: sys.cryptographic_providers (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "cryptographic_providers"
  - "sys.cryptographic_providers"
  - "sys.cryptographic_providers_TSQL"
  - "cryptographic_providers_TSQL"
helpviewer_keywords:
  - "sys.cryptographic_providers catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 9da0da95-792e-48b4-9f60-47f0729c279c
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.cryptographic_providers (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns one row for each registered cryptographic provider.  
    
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**provider_id**|**int**|Identification number of the cryptographic provider.|  
|**name**|**sysname**|Name of the cryptographic provider.|  
|**guid**|**uniqueidentifier**|Unique provider GUID.|  
|**version**|**nvarchar(50)**|Version of the provider in the format '*aa.bb.cccc.dd*'.|  
|**dll_path**|**nvarchar(512)**|Path to DLL that implements the Extensible Key Management (EKM) Application Program Interface (API).|  
|**is_enabled**|**bit**|Whether the provider is enabled on the server or not.<br /><br /> 0 = not enabled  (default)<br /><br /> 1 = enabled|  
  
## Permissions  
 The **sys.cryptographic_providers** view is visible to the public.  
  
## See Also  
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md)   
 [CREATE CRYPTOGRAPHIC PROVIDER &#40;Transact-SQL&#41;](../../t-sql/statements/create-cryptographic-provider-transact-sql.md)  
  
  
