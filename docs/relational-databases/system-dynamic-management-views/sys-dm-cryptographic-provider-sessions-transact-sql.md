---
title: "sys.dm_cryptographic_provider_sessions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_cryptographic_provider_sessions"
  - "dm_cryptographic_provider_sessions_TSQL"
  - "sys.dm_cryptographic_provider_sessions_TSQL"
  - "dm_cryptographic_provider_sessions"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_cryptographic_provider_sessions dynamic management function"
ms.assetid: 9a4de02b-1a07-4850-979a-0861fddb7f9d
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_cryptographic_provider_sessions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
## Remarks  
 The **sys.dm_cryptographic_provider_sessions** view is visible to the public for the current connection. To view all cryptographic connections, you must have the **CONTROL** server permission.  
  
## See Also  
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md)   
 [CREATE CRYPTOGRAPHIC PROVIDER &#40;Transact-SQL&#41;](../../t-sql/statements/create-cryptographic-provider-transact-sql.md)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
