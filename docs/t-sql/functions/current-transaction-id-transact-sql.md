---
title: CURRENT_TRANSACTION_ID (Transact-SQL)
description: "CURRENT_TRANSACTION_ID (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "07/24/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "CURRENT_TRANSACTION_ID"
  - "CURRENT_TRANSACTION_ID_TSQL"
  - "sys.CURRENT_TRANSACTION_ID"
  - "sys.CURRENT_TRANSACTION_ID_TSQL"
helpviewer_keywords:
  - "CURRENT_TRANSACTION_ID function"
dev_langs:
  - "TSQL"
---

# CURRENT_TRANSACTION_ID (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

This function returns the transaction ID of the current transaction in the current session.
  
:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
CURRENT_TRANSACTION_ID( )  
  
```  

## Return types

**bigint**
  
## Return Value  
The transaction ID of the current transaction in the current session, taken from [sys.dm_tran_current_transaction &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-current-transaction-transact-sql.md).
  
## Permissions  
Any user can return the transaction ID of the current session.
  
## Examples  
This example returns the transaction ID of the current session:
  
```sql
SELECT CURRENT_TRANSACTION_ID();  
```  
  
## Related content

- [sp_set_session_context (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-set-session-context-transact-sql.md)
- [SESSION_CONTEXT (Transact-SQL)](session-context-transact-sql.md)
- [Row-level security](../../relational-databases/security/row-level-security.md)
- [CONTEXT_INFO (Transact-SQL)](context-info-transact-sql.md)
- [SET CONTEXT_INFO (Transact-SQL)](../statements/set-context-info-transact-sql.md)
