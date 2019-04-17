---
title: "BEGIN CONVERSATION TIMER (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CONVERSATION"
  - "BEGIN_CONVERSATION_TSQL"
  - "TIMER_TSQL"
  - "TIMER"
  - "CONVERSATION TIMER"
  - "CONVERSATION_TIMER_TSQL"
  - "BEGIN CONVERSATION TIMER"
  - "BEGIN_CONVERSATION_TIMER_TSQL"
  - "CONVERSATION_TSQL"
  - "BEGIN CONVERSATION"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "BEGIN CONVERSATION TIMER statement"
  - "DialogTimer message"
  - "dialogs [Service Broker], beginning"
  - "timeouts [Service Broker]"
  - "timer messages [Service Broker]"
  - "conversations [Service Broker], timers"
  - "starting timers [Service Broker]"
  - "https://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer message"
ms.assetid: 98e49b3f-a38f-4180-8171-fa9cb30db4cb
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# BEGIN CONVERSATION TIMER (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Starts a timer. When the time-out expires, [!INCLUDE[ssSB](../../includes/sssb-md.md)] puts a message of type `https://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer` on the local queue for the conversation.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
BEGIN CONVERSATION TIMER ( conversation_handle )  
   TIMEOUT = timeout   
[ ; ]  
```  
  
## Arguments  
 BEGIN CONVERSATION TIMER **(**_conversation\_handle_**)**  
 Specifies the conversation to time. The *conversation_handle* must be of type **uniqueidentifier**.  
  
 TIMEOUT  
 Specifies, in seconds, the amount of time to wait before putting the message on the queue.  
  
## Remarks  
 A conversation timer provides a way for an application to receive a message on a conversation after a specific amount of time. Calling BEGIN CONVERSATION TIMER on a conversation before the timer has expired sets the timeout to the new value. Unlike the conversation lifetime, each side of the conversation has an independent conversation timer. The **DialogTimer** message arrives on the local queue without affecting the remote side of the conversation. Therefore, an application can use a timer message for any purpose.  
  
 For example, you can use the conversation timer to keep an application from waiting too long for an overdue response. If you expect the application to complete a dialog in 30 seconds, you might set the conversation timer for that dialog to 60 seconds (30 seconds plus a 30-second grace period). If the dialog is still open after 60 seconds, the application receives a time-out message on the queue for that dialog.  
  
 Alternatively, an application can use a conversation timer to request activation at a particular time. For example, you might create a service that reports the number of active connections every few minutes, or a service that reports the number of open purchase orders every evening. The service sets a conversation timer to expire at the desired time; when the timer expires, [!INCLUDE[ssSB](../../includes/sssb-md.md)] sends a **DialogTimer** message. The **DialogTimer** message causes [!INCLUDE[ssSB](../../includes/sssb-md.md)] to start the activation stored procedure for the queue. The stored procedure sends a message to the remote service and restarts the conversation timer.  
  
 BEGIN CONVERSATION TIMER is not valid in a user-defined function.  
  
## Permissions  
 Permission for setting a conversation timer defaults to users that have SEND permissions on the service for the conversation, members of the **sysadmin** fixed server role, and members of the **db_owner** fixed database role.  
  
## Examples  
 The following example sets a two-minute time-out on the dialog identified by `@dialog_handle`.  
  
```  
-- @dialog_handle is of type uniqueidentifier and  
-- contains a valid conversation handle.  
  
BEGIN CONVERSATION TIMER (@dialog_handle)  
TIMEOUT = 120 ;  
```  
  
## See Also  
 [BEGIN DIALOG CONVERSATION &#40;Transact-SQL&#41;](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)   
 [END CONVERSATION &#40;Transact-SQL&#41;](../../t-sql/statements/end-conversation-transact-sql.md)   
 [RECEIVE &#40;Transact-SQL&#41;](../../t-sql/statements/receive-transact-sql.md)  
  
  
