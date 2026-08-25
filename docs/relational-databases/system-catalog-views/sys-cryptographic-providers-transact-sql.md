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
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
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
|**dll_path**|**nvarchar(512)**|Path to DLL that implements the Extensible Key Management (EKM) application programming interface (API).|  
|**is_enabled**|**bit**|Whether the provider is enabled on the server or not.<br /><br /> 0 = not enabled  (default)<br /><br /> 1 = enabled|  
  
## Permissions  
 The **sys.cryptographic_providers** view is visible to the public.  
  
## Related content

- [Security Catalog Views (Transact-SQL)](security-catalog-views-transact-sql.md)
- [Encryption hierarchy](../security/encryption/encryption-hierarchy.md)
- [Extensible Key Management (EKM)](../security/encryption/extensible-key-management-ekm.md)
- [CREATE CRYPTOGRAPHIC PROVIDER (Transact-SQL)](../../t-sql/statements/create-cryptographic-provider-transact-sql.md)
