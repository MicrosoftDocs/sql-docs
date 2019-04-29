---
title: "DROP QUEUE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP QUEUE"
  - "DROP_QUEUE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "dropping queues"
  - "queues [Service Broker], removing"
  - "deleting queues"
  - "DROP QUEUE statement"
  - "removing queues"
ms.assetid: fd866520-ca00-477d-b2e9-0110e9610ed4
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# DROP QUEUE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Drops an existing queue.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DROP QUEUE <object>  
[ ; ]  
  
<object> ::=  
{  
    [ database_name . [ schema_name ] . | schema_name . ]  
        queue_name  
}  
```  
  
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
  
```  
DROP QUEUE ExpenseQueue ;  
  
```  
  
## See Also  
 [CREATE QUEUE &#40;Transact-SQL&#41;](../../t-sql/statements/create-queue-transact-sql.md)   
 [ALTER QUEUE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-queue-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
