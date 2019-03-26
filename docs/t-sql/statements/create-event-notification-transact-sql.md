---
title: "CREATE EVENT NOTIFICATION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE_EVENT_NOTIFICATION_TSQL"
  - "NOTIFICATION_TSQL"
  - "EVENT"
  - "NOTIFICATION"
  - "CREATE EVENT NOTIFICATION"
  - "EVENT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CREATE EVENT NOTIFICATION statement"
  - "events [SQL Server], notifications"
  - "event notifications [SQL Server], creating"
ms.assetid: dbbff0e8-9e25-4f12-a1ba-e12221d16ac2
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# CREATE EVENT NOTIFICATION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates an object that sends information about a database or server event to a service broker service. Event notifications are created only by using [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
CREATE EVENT NOTIFICATION event_notification_name   
ON { SERVER | DATABASE | QUEUE queue_name }   
[ WITH FAN_IN ]  
FOR { event_type | event_group } [ ,...n ]  
TO SERVICE 'broker_service' , { 'broker_instance_specifier' | 'current database' }  
[ ; ]  
```  
  
## Arguments  
 *event_notification_name*  
 Is the name of the event notification. An event notification name must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md) and must be unique within the scope in which they are created: SERVER, DATABASE, or *object_name*.  
  
 SERVER  
 Applies the scope of the event notification to the current instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If specified, the notification fires whenever the specified event in the FOR clause occurs anywhere in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  This option is not available in a contained database.  
  
 DATABASE  
 Applies the scope of the event notification to the current database. If specified, the notification fires whenever the specified event in the FOR clause occurs in the current database.  
  
 QUEUE  
 Applies the scope of the notification to a specific queue in the current database. QUEUE can be specified only if FOR QUEUE_ACTIVATION or FOR BROKER_QUEUE_DISABLED is also specified.  
  
 *queue_name*  
 Is the name of the queue to which the event notification applies. *queue_name* can be specified only if QUEUE is specified.  
  
 WITH FAN_IN  
 Instructs [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to send only one message per event to any specified service for all event notifications that:  
  
-   Are created on the same event.  
  
-   Are created by the same principal (as identified by the same SID).  
  
-   Specify the same service and *broker_instance_specifier*.  
  
-   Specify WITH FAN_IN.  
  
 For example, three event notifications are created. All event notifications specify FOR ALTER_TABLE, WITH FAN_IN, the same TO SERVICE clause, and are created by the same SID. When an ALTER TABLE statement is run, the messages that are created by these three event notifications are merged into one. Therefore, the target service receives only one message of the event.  
  
 *event_type*  
 Is the name of an event type that causes the event notification to execute. *event_type* can be a [!INCLUDE[tsql](../../includes/tsql-md.md)] DDL event type, a SQL Trace event type, or a [!INCLUDE[ssSB](../../includes/sssb-md.md)] event type. For a list of qualifying [!INCLUDE[tsql](../../includes/tsql-md.md)] DDL event types, see [DDL Events](../../relational-databases/triggers/ddl-events.md). [!INCLUDE[ssSB](../../includes/sssb-md.md)] event types are QUEUE_ACTIVATION and BROKER_QUEUE_DISABLED. For more information, see [Event Notifications](../../relational-databases/service-broker/event-notifications.md).  
  
 *event_group*  
 Is the name of a predefined group of [!INCLUDE[tsql](../../includes/tsql-md.md)] or SQL Trace event types. An event notification can fire after execution of any event that belongs to an event group. For a list of DDL event groups, the [!INCLUDE[tsql](../../includes/tsql-md.md)] events they cover, and the scope at which they can be defined, see [DDL Event Groups](../../relational-databases/triggers/ddl-event-groups.md).  
  
 *event_group* also acts as a macro, when the CREATE EVENT NOTIFICATION statement finishes, by adding the event types it covers to the **sys.events** catalog view.  
  
 **'** *broker_service* **'**  
 Specifies the target service that receives the event instance data. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] opens one or more conversations to the target service for the event notification. This service must honor the same [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Events message type and contract that is used to send the message.  
  
 The conversations remain open until the event notification is dropped. Certain errors could cause the conversations to close earlier. Ending some or all conversations explicitly might prevent the target service from receiving more messages.  
  
 { **'**_broker\_instance\_specifier_**'** | **'current database'** }  
 Specifies a service broker instance against which *broker_service* is resolved. The value for a specific service broker can be acquired by querying the **service_broker_guid** column of the **sys.databases** catalog view. Use **'current database'** to specify the service broker instance in the current database. **'current database'** is a case-insensitive string literal.  
  
> [!NOTE]  
>  This option is not available in a contained database.  
  
## Remarks  
 [!INCLUDE[ssSB](../../includes/sssb-md.md)] includes a message type and contract specifically for event notifications. Therefore, a Service Broker initiating service does not have to be created because one already exists that specifies the following contract name: `https://schemas.microsoft.com/SQL/Notifications/PostEventNotification`  
  
 The target service that receives event notifications must honor this preexisting contract.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssSB](../../includes/sssb-md.md)] dialog security should be configured for event notifications that send messages to a service broker on a remote server. Dialog security must be configured manually according to the full security model. For more information, see [Configure Dialog Security for Event Notifications](../../relational-databases/service-broker/configure-dialog-security-for-event-notifications.md).  
  
 If an event transaction that activates a notification is rolled back, the sending of the event notification is also rolled back. Event notifications do not fire by an action defined in a trigger when the transaction is committed or rolled back inside the trigger. Because trace events are not bound by transactions, event notifications based on trace events are sent regardless of whether the transaction that activates them is rolled back.  
  
 If the conversation between the server and the target service is broken after an event notification fires, an error is reported and the event notification is dropped.  
  
 The event transaction that originally started the notification is not affected by the success or failure of the sending of the event notification.  
  
 Any failure to send an event notification is logged.  
  
## Permissions  
 To create an event notification that is scoped to the database (ON DATABASE), requires CREATE DATABASE DDL EVENT NOTIFICATION permission in the current database.  
  
 To create an event notification on a DDL statement that is scoped to the server (ON SERVER), requires CREATE DDL EVENT NOTIFICATION permission in the server.  
  
 To create an event notification on a trace event, requires CREATE TRACE EVENT NOTIFICATION permission in the server.  
  
 To create an event notification that is scoped to a queue, requires ALTER permission on the queue.  
  
## Examples  
  
> [!NOTE]  
>  In Examples A and B below, the GUID in the `TO SERVICE 'NotifyService'` clause ('8140a771-3c4b-4479-8ac0-81008ab17984') is specific to the computer on which the example was set up. For that instance, that was the GUID for the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
>   
>  To copy and run these examples, you need to replace this GUID with one from your computer and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. As explained in the Arguments section above, you can acquire the **'**_broker\_instance\_specifier_**'** by querying the service_broker_guid column of the sys.databases catalog view.  
  
### A. Creating an event notification that is server scoped  
 The following example creates the required objects to set up a target service using [!INCLUDE[ssSB](../../includes/sssb-md.md)]. The target service references the message type and contract of the initiating service specifically for event notifications. Then an event notification is created on that target service that sends a notification whenever an `Object_Created` trace event happens on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```sql  
--Create a queue to receive messages.  
CREATE QUEUE NotifyQueue ;  
GO  
--Create a service on the queue that references  
--the event notifications contract.  
CREATE SERVICE NotifyService  
ON QUEUE NotifyQueue  
([https://schemas.microsoft.com/SQL/Notifications/PostEventNotification]);  
GO  
--Create a route on the service to define the address   
--to which Service Broker sends messages for the service.  
CREATE ROUTE NotifyRoute  
WITH SERVICE_NAME = 'NotifyService',  
ADDRESS = 'LOCAL';  
GO  
--Create the event notification.  
CREATE EVENT NOTIFICATION log_ddl1   
ON SERVER   
FOR Object_Created   
TO SERVICE 'NotifyService',  
    '8140a771-3c4b-4479-8ac0-81008ab17984' ;  
```  
  
### B. Creating an event notification that is database scoped  
 The following example creates an event notification on the same target service as the previous example. The event notification fires after an `ALTER_TABLE` event occurs on the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database.  
  
```sql  
CREATE EVENT NOTIFICATION Notify_ALTER_T1  
ON DATABASE  
FOR ALTER_TABLE  
TO SERVICE 'NotifyService',  
    '8140a771-3c4b-4479-8ac0-81008ab17984';  
```  
  
### C. Getting information about an event notification that is server scoped  
 The following example queries the `sys.server_event_notifications` catalog view for metadata about event notification `log_ddl1` that was created with server scope.  
  
```  
SELECT * FROM sys.server_event_notifications  
WHERE name = 'log_ddl1';  
```  
  
### D. Getting information about an event notification that is database scoped  
 The following example queries the `sys.event_notifications` catalog view for metadata about event notification `Notify_ALTER_T1` that was created with database scope.  
  
```sql  
SELECT * FROM sys.event_notifications  
WHERE name = 'Notify_ALTER_T1';  
```  
  
## See Also  
 [Event Notifications](../../relational-databases/service-broker/event-notifications.md)   
 [DROP EVENT NOTIFICATION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-event-notification-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sys.event_notifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-event-notifications-transact-sql.md)   
 [sys.server_event_notifications &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-event-notifications-transact-sql.md)   
 [sys.events &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-events-transact-sql.md)   
 [sys.server_events &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-events-transact-sql.md)  
  
  
