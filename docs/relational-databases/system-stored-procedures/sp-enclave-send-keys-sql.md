---
title: "sp_enclave_send_keys (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/20/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: vanto
ms.suite: "sql"
ms.technology: system-objects
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sp_enclave_send_keys"
  - "sp_enclave_send_keys_TSQL"
  - "sys.sp_enclave_send_keys"
  - "sys.sp_enclave_send_keys_TSQL"
helpviewer_keywords: 
  - "sp_enclave_send_keys"
author: jaszymas
ms.author: jaszymas
manager: craigg
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# sp_enclave_send_keys    (Transact-SQL)
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../../../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

Sends all enclave-enabled column encryption keys in the database to the enclave used by [Always Encrypted with secure enclaves &#40;Database Engine&#41;](../../relational-databases/security/encryption/always-encrypted-enclaves.md).
 
## Syntax  
  
```  
sp_enclave_send_keys   
[ ;]  
```  
Arguments
This strored procedure has no arguments.

## Return Value  
This stroed procedure has no return value.
  
## Result Sets  
This stored procedure has no result sets.
  
## Remarks  
 **sp_enclave_send_keys** sends enclave-enabled column encryption keys to the enclave if all of the following conditions are met:
- The enclave is enabled in the SQL Server instance.
- **sp_enclave_send_keys** has been invoked from an application using a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client driver, supporting Always Encrypted with secure enclaves using a database connection that has both Always Encrypted and enclave computations enabled.

## Permissions  
 Require the **VIEW ANY COLUMN ENCRYPTION KEY DEFINITION** and **VIEW ANY COLUMN MASTER KEY DEFINITION** permissions in the database.  
  
## Examples  
  
```  
EXEC sp_enclave_send_keys;  
```    
## See Also  
 [Always Encrypted with secure enclaves &#40;Database Engine&#41;](../../relational-databases/security/encryption/always-encrypted-enclaves.md).
  
  
