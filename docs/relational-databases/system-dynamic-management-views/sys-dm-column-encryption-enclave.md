---
description: "sys.dm_column_encryption_enclave (Transact-SQL)"
title: "sys.dm_column_encryption_enclave (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/11/2019"
ms.prod: sql
ms.reviewer: "vanto"
ms.technology: system-objects
ms.topic: "language-reference"
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"

---
# sys.dm_column_encryption_enclave (Transact-SQL)
[!INCLUDE [sqlserver2019-windows-only](../../includes/applies-to-version/sqlserver2019-windows-only.md)]

Returns performance counters for the secure enclave for Always Encrypted. For more information, see [Always Encrypted with secure enclaves](../security/encryption/always-encrypted-enclaves.md).

If the enclave is configured and has been correctly initialized after the last restart of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the view contains exactly one row. If the enclave is not configured or it has not been correctly initialized, the view returns no rows. 

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|current_enclave_session_count|**int**|The current number of client sessions using the enclave.|  
|current_column_encryption_key_count|**int**|The count of column encryption keys the enclave currently holds.|  
|current_memory_size_kb|**bigint**|Enclave memory size in KB|  
|total_evicted_session_count|**bigint**|The total count of enclave sessions evicted since the last server restart.|   
  
## Permissions  
Requires `VIEW SERVER STATE` permission.   
  
## Examples  
 
```sql  
SELECT * FROM sys.dm_column_encryption_enclave;  
GO  
```  
  
## See Also  
 [Configure the enclave type for Always Encrypted Server Configuration Option](../../database-engine/configure-windows/configure-column-encryption-enclave-type.md)
  
  
