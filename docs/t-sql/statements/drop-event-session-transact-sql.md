---
title: "DROP EVENT SESSION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_EVENT_SESSION_TSQL"
  - "DROP EVENT SESSION"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "event sessions [SQL Server]"
  - "DROP EVENT SESSION statement"
ms.assetid: 92eabe4b-24e2-43b1-978c-31a199964b90
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# DROP EVENT SESSION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Drops an event session.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```    
DROP EVENT SESSION event_session_name  
ON SERVER  
```  
  
## Arguments  
 *event_session_name*  
 Is the name of an existing event session.  
  
## Remarks  
 When you drop an event session, all configuration information, such as targets and session parameters, is completely removed.  
  
## Permissions  
 Requires the `ALTER ANY EVENT SESSION` permission.  
  
## Examples  
The following example shows how to drop an event session.  
  
```sql  
DROP EVENT SESSION evt_spin_lock_diagnosis ON SERVER;
GO
```  
  
## See Also  
 [CREATE EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/create-event-session-transact-sql.md)   
 [ALTER EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-event-session-transact-sql.md)   
 [sys.server_event_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-event-sessions-transact-sql.md)  
  
  
