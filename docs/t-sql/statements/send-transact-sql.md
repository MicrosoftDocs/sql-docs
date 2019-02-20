---
title: "SEND (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SEND_ON_CONVERSATION_TSQL"
  - "ON_CONVERSATION_TSQL"
  - "SEND"
  - "SEND_TSQL"
  - "SEND ON CONVERSATION"
  - "ON CONVERSATION"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "conversations [Service Broker], message sending"
  - "SEND statement"
  - "messages [Service Broker], sending"
  - "sending messages"
ms.assetid: b6e66aeb-1714-4c2b-b7c2-d386d77b0d46
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SEND (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Sends a message, using one or more existing conversations.  
  
 ![Article link icon](../../database-engine/configure-windows/media/topic-link.gif "Article link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
SEND  
   ON CONVERSATION [(]conversation_handle [,.. @conversation_handle_n][)]  
   [ MESSAGE TYPE message_type_name ]  
   [ ( message_body_expression ) ]  
[ ; ]  
```  
  
## Arguments  
 ON CONVERSATION *conversation_handle [.. @conversation_handle_n]*  
 Specifies the conversations that the message belongs to. The *conversation_handle* must contain a valid conversation identifier. The same conversation handle can't be used more than once.  
  
 MESSAGE TYPE *message_type_name*  
 Specifies the message type of the sent message. This message type must be included in the service contracts used by these conversations. These contracts must allow the message type to be sent from this side of the conversation. For example, the target services of the conversations may only send messages specified in the contract as SENT BY TARGET or SENT BY ANY. If this clause is omitted, the message is of the message type DEFAULT.  
  
 *message_body_expression*  
 Provides an expression representing the message body. The *message_body_expression* is optional. However, if the *message_body_expression* is present the expression must be of a type that can be converted to **varbinary(max)**. The expression can't be NULL. If this clause is omitted, the message body is empty.  
  
## Remarks  
  
> [!IMPORTANT]  
>  If the SEND statement isn't the first statement in a batch or stored procedure, the preceding statement must be terminated with a semicolon (;).  
  
 The SEND statement transmits a message from the services on one end of one or more [!INCLUDE[ssSB](../../includes/sssb-md.md)] conversations to the services on the other end of these conversations. The RECEIVE statement is then used to retrieve the sent message from the queues associated with the target services.  
  
 The conversation handles supplied to the ON CONVERSATION clause comes from one of three sources:  
  
-   When sending a message that isn't in response to a message received from another service, use the conversation handle that is returned from the BEGIN DIALOG statement that created the conversation.  
  
-   When sending a message that is a response to a message previously received from another service, use the conversation handle returned by the RECEIVE statement that returned the original message.  
  
 The code that contains the SEND statement is sometimes separate from the code that contains either the BEGIN DIALOG or RECEIVE statements supplying conversation handle. In these cases, the conversation handle must be one of the data items in the state information that is passed to the code containing the SEND statement.  
  
 Messages that are sent to services in other instances of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] are stored in a transmission queue in the current database until they can be transmitted to the service queues in the remote instances. Messages sent to services in the same instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] are put directly into the queues associated with these services. If a condition prevents a local message from being put directly in the target service queue, it can be stored in the transmission queue until the condition is resolved. Examples of these occurrences include some types of errors or the target service queue being inactive. You can use the **sys.transmission_queue** system view to see the messages in the transmission queue.  
  
 SEND is an atomic statement. If a SEND statement sending a message on multiple conversations fails (for example, because a conversation is in an errored state), no messages are stored in the transmission queue or put in any target service queue.  
  
 Service Broker optimizes the storage and transmission of messages that are sent on multiple conversations in the same SEND statement.  
  
 Messages in the transmission queues for an instance are transmitted in sequence based on:  
  
-   The priority level of their associated conversation endpoint.  
  
-   Within priority level, their send sequence in the conversation.  
  
 Priority levels specified in conversation priorities are only applied to messages in the transmission queue if the HONOR_BROKER_PRIORITY database option is set to ON. If HONOR_BROKER_PRIORITY is set to OFF, all messages in the transmission queue for that database are assigned the default priority level of 5. Priority levels aren't applied to a SEND where the messages are put directly into a service queue in the same instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
 The SEND statement separately locks each conversation on which a message is sent to ensure per-conversation ordered delivery.  
  
 SEND isn't valid in a user-defined function.  
  
## Permissions  
 To send a message, the current user must have RECEIVE permission on the queue of every service that sends the message.  
  
## Examples  
 The following example starts a dialog and sends an XML message on the dialog. To send the message, the example converts the xml object to **varbinary(max)**.  
  
```  
DECLARE @dialog_handle UNIQUEIDENTIFIER,  
        @ExpenseReport XML ;  
  
SET @ExpenseReport = < construct message as appropriate for the application > ;  
  
BEGIN DIALOG @dialog_handle  
FROM SERVICE [//Adventure-Works.com/Expenses/ExpenseClient]  
TO SERVICE '//Adventure-Works.com/Expenses'  
ON CONTRACT [//Adventure-Works.com/Expenses/ExpenseProcessing] ;  
  
SEND ON CONVERSATION @dialog_handle  
    MESSAGE TYPE [//Adventure-Works.com/Expenses/SubmitExpense]  
    (@ExpenseReport) ;  
```  
  
 The following example starts three dialogs and sends an XML message on each of them.  
  
```  
DECLARE @dialog_handle1 UNIQUEIDENTIFIER,  
        @dialog_handle2 UNIQUEIDENTIFIER,  
        @dialog_handle3 UNIQUEIDENTIFIER,  
        @OrderMsg XML ;  
  
SET @OrderMsg = < construct message as appropriate for the application > ;  
  
BEGIN DIALOG @dialog_handle1  
FROM SERVICE [//InitiatorDB/InitiatorService]  
TO SERVICE '//TargetDB1/TargetService'  
ON CONTRACT [//AllDBs/OrderProcessing] ;  
  
BEGIN DIALOG @dialog_handle2  
FROM SERVICE [//InitiatorDB/InitiatorService]  
TO SERVICE '//TargetDB2/TargetService'  
ON CONTRACT [//AllDBs/OrderProcessing] ;  
  
BEGIN DIALOG @dialog_handle3  
FROM SERVICE [//InitiatorDB/InitiatorService]  
TO SERVICE '//TargetDB3/TargetService'  
ON CONTRACT [//AllDBs/OrderProcessing] ;  
  
SEND ON CONVERSATION (@dialog_handle1, @dialog_handle2, @dialog_handle3)  
    MESSAGE TYPE [//AllDBs/OrderMsg]  
    (@OrderMsg) ;  
```  
  
## See Also  
 [BEGIN DIALOG CONVERSATION &#40;Transact-SQL&#41;](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)   
 [END CONVERSATION &#40;Transact-SQL&#41;](../../t-sql/statements/end-conversation-transact-sql.md)   
 [RECEIVE &#40;Transact-SQL&#41;](../../t-sql/statements/receive-transact-sql.md)   
 [sys.transmission_queue &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-transmission-queue-transact-sql.md)  
  
  
