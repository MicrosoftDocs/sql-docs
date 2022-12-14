---
title: "DROP EVENT NOTIFICATION (Transact-SQL)"
description: DROP EVENT NOTIFICATION (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP EVENT NOTIFICATION"
  - "DROP_EVENT_NOTIFICATION_TSQL"
helpviewer_keywords:
  - "event notifications [SQL Server], removing"
  - "deleting event notifications"
  - "dropping event notifications"
  - "DROP EVENT NOTIFICATION statement"
  - "removing event notifications"
dev_langs:
  - "TSQL"
---
# DROP EVENT NOTIFICATION (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes an event notification trigger from the current database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
DROP EVENT NOTIFICATION notification_name [ ,...n ]  
ON { SERVER | DATABASE | QUEUE queue_name }  
[ ; ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *notification_name*  
 Is the name of the event notification to remove. Multiple event notifications can be specified. To see a list of currently created event notifications, use [sys.event_notifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-event-notifications-transact-sql.md).  
  
 SERVER  
 Indicates the scope of the event notification applies to the current server. SERVER must be specified if it was specified when the event notification was created.  
  
 DATABASE  
 Indicates the scope of the event notification applies to the current database. DATABASE must be specified if it was specified when the event notification was created.  
  
 QUEUE *queue_name*  
 Indicates the scope of the event notification applies to the queue specified by *queue_name*. QUEUE must be specified if it was specified when the event notification was created. *queue_name* is the name of the queue and must also be specified.  
  
## Remarks  
 If an event notification fires within a transaction and is dropped within the same transaction, the event notification instance is sent, and then the event notification is dropped.  
  
## Permissions  
 To drop an event notification that is scoped at the database level, at a minimum, a user must be the owner of the event notification or have ALTER ANY DATABASE EVENT NOTIFICATION permission in the current database.  
  
 To drop an event notification that is scoped at the server level, at a minimum, a user must be the owner of the event notification or have ALTER ANY EVENT NOTIFICATION permission in the server.  
  
 To drop an event notification on a specific queue, at a minimum, a user must be the owner of the event notification or have ALTER permission on the parent queue.  
  
## Examples  
 The following example creates a database-scoped event notification, then drops it:  
  
```sql  
USE AdventureWorks2012;  
GO  
CREATE EVENT NOTIFICATION NotifyALTER_T1  
ON DATABASE  
FOR ALTER_TABLE  
TO SERVICE 'NotifyService',  
    '8140a771-3c4b-4479-8ac0-81008ab17984';  
GO  
DROP EVENT NOTIFICATION NotifyALTER_T1  
ON DATABASE;  
```  
  
## See Also  
 [CREATE EVENT NOTIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/create-event-notification-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sys.event_notifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-event-notifications-transact-sql.md)   
 [sys.events &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-events-transact-sql.md)  
  
  
