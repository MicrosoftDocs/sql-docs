---
title: "Event Notifications | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "event notifications, about"
  - "events [SQL Server], notifications"
ms.assetid: 4da73ca1-6c06-4e96-8ab8-2ecba30b6c86
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Event Notifications
  Event notifications send information about events to a [!INCLUDE[ssSB](../../includes/sssb-md.md)] service. Event notifications execute in response to a variety of [!INCLUDE[tsql](../../includes/tsql-md.md)] data definition language (DDL) statements and SQL Trace events by sending information about these events to a [!INCLUDE[ssSB](../../includes/sssb-md.md)] service.  
  
 Event notifications can be used to do the following:  
  
-   Log and review changes or activity occurring on the database.  
  
-   Perform an action in response to an event in an asynchronous instead of synchronous manner.  
  
 Event notifications can offer a programming alternative to DDL triggers and SQL Trace.  
  
## Event Notifications Benefits  
 Event notifications run asynchronously, outside the scope of a transaction. Therefore, unlike DDL triggers, event notifications can be used inside a database application to respond to events without using any resources defined by the immediate transaction.  
  
 Unlike SQL Trace, event notifications can be used to perform an action inside an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in response to a SQL Trace event.  
  
 Event data can be used by applications that are running together with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to track progress and make decisions. For example, the following event notification sends a notice to a certain service every time an `ALTER TABLE` statement is issued in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database.  
  
```  
USE AdventureWorks2012;  
GO  
CREATE EVENT NOTIFICATION NotifyALTER_T1  
ON DATABASE  
FOR ALTER_TABLE  
TO SERVICE '//Adventure-Works.com/ArchiveService' ,  
    '8140a771-3c4b-4479-8ac0-81008ab17984';  
```  
  
## Event Notifications Concepts  
 When an event notification is created, one or more [!INCLUDE[ssSB](../../includes/sssb-md.md)] conversations between an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the target service you specify are opened. The conversations typically remain open as long as the event notification exists as an object on the server instance. In some error cases the conversations can close before the event notification is dropped. These conversations are never shared between event notifications. Every event notification has its own exclusive conversations. Ending a conversation explicitly prevents the target service from receiving more messages, and the conversation will not reopen the next time the event notification fires.  
  
 Event information is delivered to the [!INCLUDE[ssSB](../../includes/sssb-md.md)] service as a variable of type `xml` that provides information about when an event occurs, about the database object affected, the [!INCLUDE[tsql](../../includes/tsql-md.md)] batch statement involved, and other information. For more information about the XML schema produced by event notifications, see [EVENTDATA &#40;Transact-SQL&#41;](/sql/t-sql/functions/eventdata-transact-sql).  
  
### Event Notifications vs. Triggers  
 The following table compares and contrasts triggers and event notifications.  
  
|Triggers|Event Notifications|  
|--------------|-------------------------|  
|DML triggers respond to data manipulation language (DML) events. DDL triggers respond to data definition language (DDL) events.|Event notifications respond to DDL events and a subset of SQL trace events.|  
|Triggers can run Transact-SQL or common language runtime (CLR) managed code.|Event notifications do not run code. Instead, they send `xml` messages to a Service Broker service.|  
|Triggers are processed synchronously, within the scope of the transactions that cause them to fire.|Event notifications may be processed asynchronously and do not run in the scope of the transactions that cause them to fire.|  
|The consumer of a trigger is tightly coupled with the event that causes it to fire.|The consumer of an event notification is decoupled from the event that causes it to fire.|  
|Triggers must be processed on the local server.|Event notifications can be processed on a remote server.|  
|Triggers can be rolled back.|Event notifications cannot be rolled back.|  
|DML trigger names are schema-scoped. DDL trigger names are database-scoped or server-scoped.|Event notification names are scoped by the server or database. Event notifications on a QUEUE_ACTIVATION event are scoped to a specific queue.|  
|DML triggers are owned by the same owner as the tables on which they are applied.|The owner of an event notification on a queue may have a different owner than the object on which it is applied.|  
|Triggers support the EXECUTE AS clause.|Event notifications do not support the EXECUTE AS clause.|  
|DDL trigger event information can be captured using the EVENTDATA function, which returns an `xml` data type.|Event notifications send `xml` event information to a Service Broker service. The information is formatted to the same schema as that of the EVENTDATA function.|  
|Metadata about triggers is found in the **sys.triggers** and **sys.server_triggers** catalog views.|Metadata about event notifications is found in the **sys.event_notifications** and **sys.server_event_notifications** catalog views.|  
  
### Event Notifications vs. SQL Trace  
 The following table compares and contrasts using event notifications and SQL Trace for monitoring server events.  
  
|SQL Trace|Event Notifications|  
|---------------|-------------------------|  
|SQL Trace generates no performance overhead associated with transactions. Packaging of data is efficient.|There is performance overhead associated with creating the XML-formatted event data and sending the event notification.|  
|SQL Trace can monitor any trace event class.|Event notifications can monitor a subset of trace event classes and also all data definition language (DDL) events.|  
|You can customize which data columns to generate in a trace event.|The schema of the XML-formatted event data returned by event notifications is fixed.|  
|Trace events generated by DDL are always generated, regardless of whether the DDL statement is rolled back.|Event notifications do not fire if the event in the corresponding DDL statement is rolled back.|  
|Managing the intermediate flow of trace event data involves populating and managing trace files or trace tables.|Intermediate management of event notification data is accomplished automatically through Service Broker queues.|  
|Traces must be restarted every time the server restarts.|After being registered, event notifications persist across server cycles and are transacted.|  
|After being initiated, the firing of traces cannot be controlled. Stop times and filter times can be used to specify when they initiate. Traces are accessed by polling the corresponding trace file.|Event notifications can be controlled by using the WAITFOR statement against the queue that receives the message generated by the event notification. They can be accessed by polling the queue.|  
|ALTER TRACE is the least permission that is required to create a trace. Permission is also required to create a trace file on the corresponding computer.|Least permission depends on the type of event notification being created. RECEIVE permission is also needed on the corresponding queue.|  
|Traces can be received remotely.|Event notifications can be received remotely.|  
|Trace events are implemented by using system stored procedures.|Event notifications are implemented by using a combination of [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssSB](../../includes/sssb-md.md)][!INCLUDE[tsql](../../includes/tsql-md.md)] statements.|  
|Trace event data can be accessed programmatically by querying the corresponding trace table, parsing the trace file, or using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects (SMO) TraceReader Class.|Event data is accessed programmatically by issuing XQuery against the XML-formatted event data, or by using the SMO Event classes.|  
  
## Event Notification Tasks  
  
|Task|Topic|  
|----------|-----------|  
|Describes how to create and implement event notifications.|[Implement Event Notifications](implement-event-notifications.md)|  
|Describes how to configure [!INCLUDE[ssSB](../../includes/sssb-md.md)] dialog security for event notifications that send messages to a service broker on a remote server.|[Configure Dialog Security for Event Notifications](configure-dialog-security-for-event-notifications.md)|  
|Describes how to return information about event notifications.|[Get Information About Event Notifications](get-information-about-event-notifications.md)|  
  
## See Also  
 [DDL Triggers](../triggers/ddl-triggers.md)   
 [DML Triggers](../triggers/dml-triggers.md)   
 [SQL Trace](../sql-trace/sql-trace.md)  
  
  
