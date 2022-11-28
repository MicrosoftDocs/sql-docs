---
title: "DROP QUEUE (Transact-SQL)"
description: DROP QUEUE (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP QUEUE"
  - "DROP_QUEUE_TSQL"
helpviewer_keywords:
  - "dropping queues"
  - "queues [Service Broker], removing"
  - "deleting queues"
  - "DROP QUEUE statement"
  - "removing queues"
dev_langs:
  - "TSQL"
---
# DROP QUEUE (Transact-SQL)
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

  Drops an existing queue.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP QUEUE <object>  
[ ; ]  
  
<object> ::=  
{ database_name.schema_name.queue_name | schema_name.queue_name | queue_name }
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *database_name*  
 The name of the database that contains the queue to drop. When no *database_name* is provided, defaults to the current database.  
  
 *schema_name (object)*  
 The name of the schema that owns the queue to drop. When no *schema_name* is provided, defaults to the default schema for the current user.  
  
 *queue_name*  
 The name of the queue to drop.  
  
## Remarks  
 You cannot drop a queue if any services refer to the queue.  
  
## Permissions  
 Permission for dropping a queue defaults to the owner of the queue, members of the **db_ddladmin** or **db_owner** fixed database roles, and members of the **sysadmin** fixed server role.  
  
## Examples  
 The following example drops the **ExpenseQueue** queue from the current database.  
  
```sql  
DROP QUEUE ExpenseQueue ;  
```  
  
## See Also  
 [CREATE QUEUE &#40;Transact-SQL&#41;](../../t-sql/statements/create-queue-transact-sql.md)   
 [ALTER QUEUE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-queue-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
