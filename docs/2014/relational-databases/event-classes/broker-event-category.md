---
title: "Broker Event Category | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SQL Server event classes, Broker event category"
  - "Broker event category [SQL Server]"
  - "event classes [SQL Server], Broker event category"
ms.assetid: 470dc93c-0dda-4d89-829b-937738d59b31
author: stevestein
ms.author: sstein
manager: craigg
---
# Broker Event Category
  The **Broker** event category contains general Service Broker events.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Broker:Activation Event Class](broker-activation-event-class.md)|An event generated when a queue monitor starts an activation stored procedure.|  
|[Broker:Connection Event Class](broker-connection-event-class.md)|An event generated to report the status of a transport connection managed by Service Broker.|  
|[Broker:Conversation Event Class](broker-conversation-event-class.md)|An event generated to report the progress of a conversation.|  
|[Broker:Conversation Group Event Class](broker-conversation-group-event-class.md)|An event generated when the database creates or drops a conversation group.|  
|[Broker:Corrupted Message Event Class](broker-corrupted-message-event-class.md)|An event generated to report that the database has received a corrupt message.|  
|[Broker:Forwarded Message Dropped Event Class](broker-forwarded-message-dropped-event-class.md)|An event generated when SQL Server drops a Service Broker message that was to have been forwarded.|  
|[Broker:Forwarded Message Sent Event Class](broker-forwarded-message-sent-event-class.md)|An event generated when SQL Server forwards a Service Broker message.|  
|[Broker:Message Classify Event Class](broker-message-classify-event-class.md)|An event generated when Service Broker determines the routing for a message.|  
|[Broker:Message Drop Event Class](broker-message-drop-event-class.md)|An event generated when Service Broker is unable to retain a received message that should have been delivered to a service in this instance|  
|[Broker:Remote Message Ack Event Class](broker-remote-message-ack-event-class.md)|An event generated when Service Broker sends or receives a message acknowledgement.|  
  
 Two security audit events are also provided for Service Broker. For more information on those events, see [Audit Broker Login Event Class](audit-broker-login-event-class.md) and [Audit Broker Conversation Event Class](audit-broker-conversation-event-class.md).  
  
## See Also  
 [Security Audit Event Category](https://docs.microsoft.com/bi-reference/trace-events/security-audit-event-category)  
  
  
