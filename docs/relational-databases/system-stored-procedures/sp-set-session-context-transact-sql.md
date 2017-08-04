---
title: "sp_set_session_context (Transact-SQL) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "08/04/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
applies_to: 
  - "Azure SQL Database"
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "sp_set_session_context"
  - "sp_set_session_context_TSQL"
  - "sys.sp_set_session_context"
  - "sys.sp_set_session_context_TSQL"
helpviewer_keywords: 
  - "sp_set_session_context"
ms.assetid: 7a3a3b2a-1408-4767-a376-c690e3c1fc5b
caps.latest.revision: 7
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sp_set_session_context (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

Sets a key-value pair in the session context.  
  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_set_session_context [ @key= ] 'key', [ @value= ] 'value'  
    [ , [ @read_only = ] { 0 | 1 } ]  
[ ; ]  
```  
  
## Arguments  
 [ @key= ] 'key'  
 The key being set, of type **sysname**. The maximum key size is 128 bytes.  
  
 [ @value= ] 'value'  
 The value for the specified key, of type **sql_variant**. Setting a value of NULL will free the memory. The maximum size is 8,000 bytes.  
  
 [ @read_only= ] { 0 | 1 }  
 A flag of type **bit**. If 1, then the value for the specified key cannot be changed again on this logical connection. If 0 (default), then the value can be changed.  
  
## Permissions  
 Any user can set a session context for their session.  
  
## Remarks  
 Like other stored procedures, only literals and variables (not expressions or function calls) can be passed as parameters.  
  
 The total size of the session context is limited to 256 kb. If you try to set a value that causes this limit to be exceeded, the statement will fail. You can monitor overall memory usage in [sys.dm_os_memory_objects &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-objects-transact-sql.md).  
  
 You can monitor overall memory usage by querying [sys.dm_os_memory_cache_counters &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-cache-counters-transact-sql.md) as follows: `SELECT * FROM sys.dm_os_memory_cache_counters WHERE type = 'CACHESTORE_SESSION_CONTEXT';`  
  
## Examples  
 The following example shows how to set  and then return a sessions context key named language with a value of English.  
  
```  
EXEC sp_set_session_context 'language', 'English';  
SELECT SESSION_CONTEXT(N'language');  
```  
  
 The following example demonstrates the use of the optional read-only flag.  
  
```  
EXEC sp_set_session_context 'user_id', 4, @read_only = 1;  
```  
  
## See Also  
 [CURRENT_TRANSACTION_ID &#40;Transact-SQL&#41;](../../t-sql/functions/current-transaction-id-transact-sql.md)   
 [SESSION_CONTEXT &#40;Transact-SQL&#41;](../../t-sql/functions/session-context-transact-sql.md)   
 [Row-Level Security](../../relational-databases/security/row-level-security.md)   
 [CONTEXT_INFO  &#40;Transact-SQL&#41;](../../t-sql/functions/context-info-transact-sql.md)   
 [SET CONTEXT_INFO &#40;Transact-SQL&#41;](../../t-sql/statements/set-context-info-transact-sql.md)  
  
  
