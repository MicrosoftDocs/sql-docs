---
title: "END CONVERSATION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "END DIALOG"
  - "END CONVERSATION"
  - "END_DIALOG_TSQL"
  - "END_CONVERSATION_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "errors [Service Broker], conversations"
  - "dialogs [Service Broker], ending"
  - "closing conversations"
  - "END CONVERSATION statement"
  - "conversations [Service Broker], ending"
  - "ending conversations [SQL Server]"
ms.assetid: 4415a126-cd22-4a5e-b84a-d8c68515c83b
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# END CONVERSATION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Ends one side of an existing conversation.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
END CONVERSATION conversation_handle  
   [   [ WITH ERROR = failure_code DESCRIPTION = 'failure_text' ]  
     | [ WITH CLEANUP ]  
    ]  
[ ; ]  
```  
  
## Arguments  
 *conversation_handle*  
 Is the conversation handle for the conversation to end.  
  
 WITH ERROR =*failure_code*  
 Is the error code. The *failure_code* is of type **int**. The failure code is a user-defined code that is included in the error message sent to the other side of the conversation. The failure code must be greater than 0.  
  
 DESCRIPTION =*failure_text*  
 Is the error message. The *failure_text* is of type **nvarchar(3000)**. The failure text is user-defined text that is included in the error message sent to the other side of the conversation.  
  
 WITH CLEANUP  
 Removes all messages and catalog view entries for one side of a conversation that cannot complete normally. The other side of the conversation is not notified of the cleanup. [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] drops the conversation endpoint, all messages for the conversation in the transmission queue, and all messages for the conversation in the service queue. Administrators can use this option to remove conversations which cannot complete normally. For example, if the remote service has been permanently removed, an administrator can use WITH CLEANUP to remove conversations to that service. Do not use WITH CLEANUP in the code of a [!INCLUDE[ssSB](../../includes/sssb-md.md)] application. If END CONVERSATION WITH CLEANUP is run before the receiving endpoint acknowledges receiving a message, the sending endpoint will send the message again. This could potentially re-run the dialog.  
  
## Remarks  
 Ending a conversation locks the conversation group that the provided *conversation_handle* belongs to. When a conversation ends, [!INCLUDE[ssSB](../../includes/sssb-md.md)] removes all messages for the conversation from the service queue.  
  
 After a conversation ends, an application can no longer send or receive messages for that conversation. Both participants in a conversation must call END CONVERSATION for the conversation to complete. If [!INCLUDE[ssSB](../../includes/sssb-md.md)] has not received an end dialog message or an Error message from the other participant in the conversation, [!INCLUDE[ssSB](../../includes/sssb-md.md)] notifies the other participant in the conversation that the conversation has ended. In this case, although the conversation handle for the conversation is no longer valid, the endpoint for the conversation remains active until the instance that hosts the remote service acknowledges the message.  
  
 If [!INCLUDE[ssSB](../../includes/sssb-md.md)] has not already processed an end dialog or error message for the conversation, [!INCLUDE[ssSB](../../includes/sssb-md.md)] notifies the remote side of the conversation that the conversation has ended. The messages that [!INCLUDE[ssSB](../../includes/sssb-md.md)] sends to the remote service depend on the options specified:  
  
-   If the conversation ends without errors, and the conversation to the remote service is still active, [!INCLUDE[ssSB](../../includes/sssb-md.md)] sends a message of type `https://schemas.microsoft.com/SQL/ServiceBroker/EndDialog` to the remote service. [!INCLUDE[ssSB](../../includes/sssb-md.md)] adds this message to the transmission queue in conversation order. [!INCLUDE[ssSB](../../includes/sssb-md.md)] sends all messages for this conversation that are currently in the transmission queue before sending this message.  
  
-   If the conversation ends with an error and the conversation to the remote service is still active, [!INCLUDE[ssSB](../../includes/sssb-md.md)] sends a message of type `https://schemas.microsoft.com/SQL/ServiceBroker/Error` to the remote service. [!INCLUDE[ssSB](../../includes/sssb-md.md)] drops any other messages for this conversation currently in the transmission queue.  
  
-   The WITH CLEANUP clause allows a database administrator to remove conversations that cannot complete normally. This option removes all messages and catalog view entries for the conversation. Notice that, in this case, the remote side of the conversation receives no indication that the conversation has ended, and may not receive messages that have been sent by an application but not yet transmitted over the network. Avoid this option unless the conversation cannot complete normally.  
  
 After a conversation ends, a [!INCLUDE[tsql](../../includes/tsql-md.md)] SEND statement that specifies the conversation handle causes a [!INCLUDE[tsql](../../includes/tsql-md.md)] error. If messages for this conversation arrive from the other side of the conversation, [!INCLUDE[ssSB](../../includes/sssb-md.md)] discards those messages.  
  
 If a conversation ends while the remote service still has unsent messages for the conversation, the remote service drops the unsent messages. This is not considered an error, and the remote service receives no notification that messages have been dropped.  
  
 Failure codes specified in the WITH ERROR clause must be positive numbers. Negative numbers are reserved for [!INCLUDE[ssSB](../../includes/sssb-md.md)] error messages.  
  
 END CONVERSATION is not valid in a user-defined function.  
  
## Permissions  
 To end an active conversation, the current user must be the owner of the conversation, a member of the sysadmin fixed server role or a member of the db_owner fixed database role.  
  
 A member of the sysadmin fixed server role or a member of the db_owner fixed database role may use the WITH CLEANUP to remove the metadata for a conversation that has already completed.  
  
## Examples  
  
### A. Ending a conversation  
 The following example ends the dialog specified by `@dialog_handle`.  
  
```  
END CONVERSATION @dialog_handle ;  
```  
  
### B. Ending a conversation with an error  
 The following example ends the dialog specified by `@dialog_handle` with an error if the processing statement reports an error. Notice that this is a simplistic approach to error handling, and may not be appropriate for some applications.  
  
```  
DECLARE @dialog_handle UNIQUEIDENTIFIER,  
        @ErrorSave INT,  
        @ErrorDesc NVARCHAR(100) ;  
BEGIN TRANSACTION ;  
  
<receive and process message>  
  
SET @ErrorSave = @@ERROR ;  
  
IF (@ErrorSave <> 0)  
  BEGIN  
      ROLLBACK TRANSACTION ;  
      SET @ErrorDesc = N'An error has occurred.' ;  
      END CONVERSATION @dialog_handle   
      WITH ERROR = @ErrorSave DESCRIPTION = @ErrorDesc ;  
  END  
ELSE  
  
COMMIT TRANSACTION ;  
```  
  
### C. Cleaning up a conversation that cannot complete normally  
 The following example ends the dialog specified by `@dialog_handle`. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] immediately removes all messages from the service queue and the transmission queue, without notifying the remote service. Since ending a dialog with cleanup does not notify the remote service, you should only use this in cases where the remote service is not available to receive an **EndDialog** or **Error** message.  
  
```  
END CONVERSATION @dialog_handle WITH CLEANUP ;  
```  
  
## See Also  
 [BEGIN CONVERSATION TIMER &#40;Transact-SQL&#41;](../../t-sql/statements/begin-conversation-timer-transact-sql.md)   
 [BEGIN DIALOG CONVERSATION &#40;Transact-SQL&#41;](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)   
 [sys.conversation_endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-conversation-endpoints-transact-sql.md)  
  
  
