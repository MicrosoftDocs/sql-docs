---
title: "SESSION_CONTEXT (Transact-SQL)"
description: "SESSION_CONTEXT (Transact-SQL)"
author: VanMSFT
ms.author: vanto
ms.reviewer: ""
ms.date: "05/14/2019"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "SESSION_CONTEXT"
  - "sys.SESSION_CONTEXT"
  - "SESSION_CONTEXT_TSQL"
  - "sys.SESSION_CONTEXT_TSQL"
helpviewer_keywords:
  - "SESSION_CONTEXT function"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqledge-current || = azure-sqldw-latest"
---
# SESSION_CONTEXT (Transact-SQL)
[!INCLUDE [sqlserver2016-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

  Returns the value of the specified key in the current session context. The value is set by using the [sp_set_session_context &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-set-session-context-transact-sql.md) procedure.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
SESSION_CONTEXT(N'key')  
```  
  
## Arguments
 'key'  
 The key (type sysname) of the value being retrieved.  
  
## Return Type  
 **sql_variant**  
  
## Return Value  
 The value associated with the specified key in the session context, or NULL if no value has been set for that key.  
  
## Permissions  
 Any user can read the session context for their session.  
  
## Remarks  
 SESSION_CONTEXT's MARS behavior is similar to that of CONTEXT_INFO. If a MARS batch sets a key-value pair, the new value will not be returned in other MARS batches on the same connection unless they started after the batch that set the new value completed. If multiple MARS batches are active on a connection, values cannot be set as "read_only." This prevents race conditions and non-determinism about which value "wins."  
  
## Examples  
 The following simple example sets the session context value for key `user_id` to 4, and then uses the **SESSION_CONTEXT** function to retrieve the value.  
  
```sql  
EXEC sp_set_session_context 'user_id', 4;  
SELECT SESSION_CONTEXT(N'user_id');  
```  
  
## See Also  
 [sp_set_session_context &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-set-session-context-transact-sql.md)   
 [CURRENT_TRANSACTION_ID &#40;Transact-SQL&#41;](../../t-sql/functions/current-transaction-id-transact-sql.md)   
 [Row-Level Security](../../relational-databases/security/row-level-security.md)   
 [CONTEXT_INFO  &#40;Transact-SQL&#41;](../../t-sql/functions/context-info-transact-sql.md)   
 [SET CONTEXT_INFO &#40;Transact-SQL&#41;](../../t-sql/statements/set-context-info-transact-sql.md)  
  
  
