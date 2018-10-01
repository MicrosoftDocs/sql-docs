---
title: "CURRENT_TRANSACTION_ID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CURRENT_TRANSACTION_ID"
  - "CURRENT_TRANSACTION_ID_TSQL"
  - "sys.CURRENT_TRANSACTION_ID"
  - "sys.CURRENT_TRANSACTION_ID_TSQL"
helpviewer_keywords: 
  - "CURRENT_TRANSACTION_ID function"
ms.assetid: 82cd9f92-d935-45a0-a433-620d6e15b467
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# CURRENT_TRANSACTION_ID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

This function returns the transaction ID of the current transaction in the current session.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
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
  
## See also
[sp_set_session_context &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-set-session-context-transact-sql.md)  
[SESSION_CONTEXT &#40;Transact-SQL&#41;](../../t-sql/functions/session-context-transact-sql.md)  
[Row-Level Security](../../relational-databases/security/row-level-security.md)  
[CONTEXT_INFO  &#40;Transact-SQL&#41;](../../t-sql/functions/context-info-transact-sql.md)  
[SET CONTEXT_INFO &#40;Transact-SQL&#41;](../../t-sql/statements/set-context-info-transact-sql.md)
  
  
