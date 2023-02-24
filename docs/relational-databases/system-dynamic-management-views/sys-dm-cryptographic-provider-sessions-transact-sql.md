---
title: "sys.dm_cryptographic_provider_sessions (Transact-SQL)"
description: sys.dm_cryptographic_provider_sessions (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_cryptographic_provider_sessions"
  - "dm_cryptographic_provider_sessions_TSQL"
  - "sys.dm_cryptographic_provider_sessions_TSQL"
  - "dm_cryptographic_provider_sessions"
helpviewer_keywords:
  - "sys.dm_cryptographic_provider_sessions dynamic management function"
dev_langs:
  - "TSQL"
ms.assetid: 9a4de02b-1a07-4850-979a-0861fddb7f9d
---
# sys.dm_cryptographic_provider_sessions (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns information about open sessions for a cryptographic provider.  
 
## Syntax  
  
```  
  
sys.dm_cryptographic_provider_sessions(session_identifier)  
```  
  
## Arguments  
 *session_identifier*  
 An integer indicating the sessions to be returned.  
  
 0 = Current connection only  
  
 1 = All cryptographic connections  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**provider_id**|**int**|Identification number of the cryptographic provider.|  
|**session_handle**|**varbytes(8)**|Cryptographic session handle.|  
|**identity**|**nvarchar(128)**|Identity used to authenticate with the cryptographic provider.|  
|**spid**|**short**|Session ID SPID of the connection. For more information, see [@@SPID &#40;Transact-SQL&#41;](../../t-sql/functions/spid-transact-sql.md).|  
  
## Permissions  
 Members of the public server role can use **sys.dm_cryptographic_provider_sessions** to return information about the current connection. To view all cryptographic connections, the **CONTROL** server permission is required.  
  
## See Also  
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md)   
 [CREATE CRYPTOGRAPHIC PROVIDER &#40;Transact-SQL&#41;](../../t-sql/statements/create-cryptographic-provider-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
