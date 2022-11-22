---
description: "sp_set_session_context (Transact-SQL)"
title: "sp_set_session_context (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/14/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_set_session_context"
  - "sp_set_session_context_TSQL"
  - "sys.sp_set_session_context"
  - "sys.sp_set_session_context_TSQL"
helpviewer_keywords: 
  - "sp_set_session_context"
ms.assetid: 7a3a3b2a-1408-4767-a376-c690e3c1fc5b
author: VanMSFT
ms.author: vanto
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_set_session_context (Transact-SQL)
[!INCLUDE [sqlserver2016-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

Sets a key-value pair in the session context.  
  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_set_session_context [ @key= ] N'key', [ @value= ] 'value'  
    [ , [ @read_only = ] { 0 | 1 } ]  
[ ; ]  
```  
  
## Arguments  
 [ @key= ] N'key'  
 The key being set, of type **sysname**. The maximum key size is 128 bytes.  
  
 [ @value= ] 'value'  
 The value for the specified key, of type **sql_variant**. Setting a value of NULL frees the memory. The maximum size is 8,000 bytes.  
  
 [ @read_only= ] { 0 | 1 }  
 A flag of type **bit**. If 1, then the value for the specified key cannot be changed again on this logical connection. If 0 (default), then the value can be changed.  
  
## Permissions  
 Any user can set a session context for their session.  
  
## Remarks  
 Like other stored procedures, only literals and variables (not expressions or function calls) can be passed as parameters.  
  
 The total size of the session context is limited to 1 MB. If you set a value that causes this limit to be exceeded, the statement fails. You can monitor overall memory usage in [sys.dm_os_memory_objects &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-objects-transact-sql.md).  
  
 You can monitor overall memory usage by querying [sys.dm_os_memory_cache_counters &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-cache-counters-transact-sql.md) as follows: `SELECT * FROM sys.dm_os_memory_cache_counters WHERE type = 'CACHESTORE_SESSION_CONTEXT';`  
  
## Examples  
A. The following example shows how to set  and then return a sessions context key named language with a value of English.  
  
```  
EXEC sys.sp_set_session_context @key = N'language', @value = 'English';  
SELECT SESSION_CONTEXT(N'language');  
```  
  
 The following example demonstrates the use of the optional read-only flag.  
  
```  
EXEC sys.sp_set_session_context @key = N'user_id', @value = 4, @read_only = 1;  
```  

B. The following example shows how to set and retrieve a session context key named the client_correlation_id with a value of 12323ad.
```
-- set value
EXEC sp_set_session_context 'client_correlation_id', '12323ad'; 

--check value
SELECT SESSION_CONTEXT(N'client_correlation_id');

```

## See Also  
 [CURRENT_TRANSACTION_ID &#40;Transact-SQL&#41;](../../t-sql/functions/current-transaction-id-transact-sql.md)   
 [SESSION_CONTEXT &#40;Transact-SQL&#41;](../../t-sql/functions/session-context-transact-sql.md)   
 [Row-Level Security](../../relational-databases/security/row-level-security.md)   
 [CONTEXT_INFO  &#40;Transact-SQL&#41;](../../t-sql/functions/context-info-transact-sql.md)   
 [SET CONTEXT_INFO &#40;Transact-SQL&#41;](../../t-sql/statements/set-context-info-transact-sql.md)  
  
  
