---
title: "BEGIN DIALOG CONVERSATION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DIALOG CONVERSATION"
  - "DIALOG"
  - "BEGIN_DIALOG_TSQL"
  - "BEGIN_DIALOG_CONVERSATION_TSQL"
  - "DIALOG_CONVERSATION_TSQL"
  - "DIALOG_TSQL"
  - "BEGIN DIALOG"
  - "BEGIN DIALOG CONVERSATION"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "conversations [Service Broker]"
  - "beginning dialogs"
  - "dialogs [Service Broker], beginning"
  - "BEGIN DIALOG CONVERSATION statement"
  - "cryptography [SQL Server], conversations"
  - "opening conversations"
  - "encryption [SQL Server], conversations"
  - "starting conversations"
ms.assetid: 8e814f9d-77c1-4906-b8e4-668a86fc94ba
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# BEGIN DIALOG CONVERSATION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Begins a dialog from one service to another service. A dialog is a conversation that provides exactly-once-in-order messaging between two services.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
BEGIN DIALOG [ CONVERSATION ] @dialog_handle  
   FROM SERVICE initiator_service_name  
   TO SERVICE 'target_service_name'  
       [ , { 'service_broker_guid' | 'CURRENT DATABASE' }]   
   [ ON CONTRACT contract_name ]  
   [ WITH  
   [  { RELATED_CONVERSATION = related_conversation_handle   
      | RELATED_CONVERSATION_GROUP = related_conversation_group_id } ]   
   [ [ , ] LIFETIME = dialog_lifetime ]   
   [ [ , ] ENCRYPTION = { ON | OFF }  ] ]  
[ ; ]  
```  
  
## Arguments  
 **@** _dialog_handle_  
 Is a variable used to store the system-generated dialog handle for the new dialog that is returned by the BEGIN DIALOG CONVERSATION statement. The variable must be of type **uniqueidentifier**.  
  
 FROM SERVICE *initiator_service_name*  
 Specifies the service that initiates the dialog. The name specified must be the name of a service in the current database. The queue specified for the initiator service receives messages returned by the target service and messages created by Service Broker for this conversation.  
  
 TO SERVICE **'**_target_service_name_**'**  
 Specifies the target service with which to initiate the dialog. The *target_service_name* is of type **nvarchar(256)**. [!INCLUDE[ssSB](../../includes/sssb-md.md)] uses a byte-by-byte comparison to match the *target_service_name* string. In other words, the comparison is case-sensitive and does not take into account the current collation.  
  
 *service_broker_guid*  
 Specifies the database that hosts the target service. When more than one database hosts an instance of the target service, you can communicate with a specific database by providing a *service_broker_guid*.  
  
 The *service_broker_guid* is of type **nvarchar(128)**. To find the *service_broker_guid* for a database, run the following query in the database:  
  
```  
SELECT service_broker_guid  
FROM sys.databases  
WHERE database_id = DB_ID() ;  
```  
  
> [!NOTE]  
>  This option is not available in a contained database.  
  
 **'**CURRENT DATABASE**'**  
 Specifies that the conversation use the *service_broker_guid* for the current database.  
  
 ON CONTRACT *contract_name*  
 Specifies the contract that this conversation follows. The contract must exist in the current database. If the target service does not accept new conversations on the contract specified, [!INCLUDE[ssSB](../../includes/sssb-md.md)] returns an error message on the conversation. When this clause is omitted, the conversation follows the contract named **DEFAULT**.  
  
 RELATED_CONVERSATION **=**_related_conversation_handle_  
 Specifies the existing conversation group that the new dialog is added to. When this clause is present, the new dialog belongs to the same conversation group as the dialog specified by *related_conversation_handle*. The *related_conversation_handle*must be of a type implicitly convertible to type **uniqueidentifier**. The statement fails if the *related_conversation_handle* does not reference an existing dialog.  
  
 RELATED_CONVERSATION_GROUP **=**_related_conversation_group_id_  
 Specifies the existing conversation group that the new dialog is added to. When this clause is present, the new dialog will be added to the conversation group specified by *related_conversation_group_id*. The *related_conversation_group_id*must be of a type implicitly convertible to type **uniqueidentifier**. If *related_conversation_group_id*does not reference an existing conversation group, the service broker creates a new conversation group with the specified *related_conversation_group_id* and relates the new dialog to that conversation group.  
  
 LIFETIME **=**_dialog_lifetime_  
 Specifies the maximum amount of time the dialog will remain open. For the dialog to complete successfully, both endpoints must explicitly end the dialog before the lifetime expires. The *dialog_lifetime* value must be expressed in seconds. Lifetime is of type **int**. When no LIFETIME clause is specified, the dialog lifetime is the maximum value of the **int** data type.  
  
 ENCRYPTION  
 Specifies whether or not messages sent and received on this dialog must be encrypted when they are sent outside of an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A dialog that must be encrypted is a *secured dialog*. When ENCRYPTION = ON and the certificates required to support encryption are not configured, [!INCLUDE[ssSB](../../includes/sssb-md.md)] returns an error message on the conversation. If ENCRYPTION = OFF, encryption is used if a remote service binding is configured for the *target_service_name*; otherwise messages are sent unencrypted. If this clause is not present, the default value is ON.  
  
> [!NOTE]  
>  Messages exchanged with services in the same instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are never encrypted. However, a database master key and the certificates for encryption are still required for conversations that use encryption if the services for the conversation are in different databases. This allows conversations to continue in the event that one of the databases is moved to a different instance while the conversation is in progress.  
  
## Remarks  
 All messages are part of a conversation. Therefore, an initiating service must begin a conversation with the target service before sending a message to the target service. The information specified in the BEGIN DIALOG CONVERSATION statement is similar to the address on a letter; [!INCLUDE[ssSB](../../includes/sssb-md.md)] uses the information to deliver messages to the correct service. The service specified in the TO SERVICE clause is the address that messages are sent to. The service specified in the FROM SERVICE clause is the return address used for reply messages.  
  
 The target of a conversation does not need to call BEGIN DIALOG CONVERSATION. [!INCLUDE[ssSB](../../includes/sssb-md.md)] creates a conversation in the target database when the first message in the conversation arrives from the initiator.  
  
 Beginning a dialog creates a conversation endpoint in the database for the initiating service, but does not create a network connection to the instance that hosts the target service. [!INCLUDE[ssSB](../../includes/sssb-md.md)] does not establish communication with the target of the dialog until the first message is sent.  
  
 When the BEGIN DIALOG CONVERSATION statement does not specify a related conversation or a related conversation group, [!INCLUDE[ssSB](../../includes/sssb-md.md)] creates a new conversation group for the new conversation.  
  
 [!INCLUDE[ssSB](../../includes/sssb-md.md)] does not allow arbitrary groupings of conversations. All conversations in a conversation group must have the service specified in the FROM clause as either the initiator or the target of the conversation.  
  
 The BEGIN DIALOG CONVERSATION command locks the conversation group that contains the *dialog_handle* returned. When the command includes a RELATED_CONVERSATION_GROUP clause, the conversation group for *dialog_handle* is the conversation group specified in the *related_conversation_group_id* parameter. When the command includes a RELATED_CONVERSATION clause, the conversation group for *dialog_handle* is the conversation group associated with the *related_conversation_handle* specified.  
  
 BEGIN DIALOG CONVERSATION is not valid in a user-defined function.  
  
## Permissions  
 To begin a dialog, the current user must have RECEIVE permission on the queue for the service specified in the FROM clause of the command and REFERENCES permission for the contract specified.  
  
## Examples  
  
### A. Beginning a dialog  
 The following example begins a dialog conversation and stores an identifier for the dialog in `@dialog_handle.` The `//Adventure-Works.com/ExpenseClient` service is the initiator for the dialog, and the `//Adventure-Works.com/Expenses` service is the target of the dialog. The dialog follows the contract `//Adventure-Works.com/Expenses/ExpenseSubmission`.  
  
```  
DECLARE @dialog_handle UNIQUEIDENTIFIER ;  
  
BEGIN DIALOG CONVERSATION @dialog_handle  
   FROM SERVICE [//Adventure-Works.com/ExpenseClient]  
   TO SERVICE '//Adventure-Works.com/Expenses'  
   ON CONTRACT [//Adventure-Works.com/Expenses/ExpenseSubmission] ;  
```  
  
### B. Beginning a dialog with an explicit lifetime  
 The following example begins a dialog conversation and stores an identifier for the dialog in `@dialog_handle`. The `//Adventure-Works.com/ExpenseClient` service is the initiator for the dialog, and the `//Adventure-Works.com/Expenses` service is the target of the dialog. The dialog follows the contract `//Adventure-Works.com/Expenses/ExpenseSubmission`. If the dialog has not been closed by the END CONVERSATION command within `60` seconds, the broker ends the dialog with an error.  
  
```  
DECLARE @dialog_handle UNIQUEIDENTIFIER ;  
  
BEGIN DIALOG CONVERSATION @dialog_handle  
   FROM SERVICE [//Adventure-Works.com/ExpenseClient]  
   TO SERVICE '//Adventure-Works.com/Expenses'  
   ON CONTRACT [//Adventure-Works.com/Expenses/ExpenseSubmission]  
   WITH LIFETIME = 60 ;  
```  
  
### C. Beginning a dialog with a specific broker instance  
 The following example begins a dialog conversation and stores an identifier for the dialog in `@dialog_handle`. The `//Adventure-Works.com/ExpenseClient` service is the initiator for the dialog, and the `//Adventure-Works.com/Expenses` service is the target of the dialog. The dialog follows the contract `//Adventure-Works.com/Expenses/ExpenseSubmission`. The broker routes messages on this dialog to the broker identified by the GUID `a326e034-d4cf-4e8b-8d98-4d7e1926c904.`  
  
```  
DECLARE @dialog_handle UNIQUEIDENTIFIER ;  
  
BEGIN DIALOG CONVERSATION @dialog_handle  
   FROM SERVICE [//Adventure-Works.com/ExpenseClient]  
   TO SERVICE '//Adventure-Works.com/Expenses',   
              'a326e034-d4cf-4e8b-8d98-4d7e1926c904'  
   ON CONTRACT [//Adventure-Works.com/Expenses/ExpenseSubmission] ;  
```  
  
### D. Beginning a dialog, and relating it to an existing conversation group  
 The following example begins a dialog conversation and stores an identifier for the dialog in `@dialog_handle`. The `//Adventure-Works.com/ExpenseClient` service is the initiator for the dialog, and the `//Adventure-Works.com/Expenses` service is the target of the dialog. The dialog follows the contract `//Adventure-Works.com/Expenses/ExpenseSubmission`. The broker associates the dialog with the conversation group identified by `@conversation_group_id` instead of creating a new conversation group.  
  
```  
DECLARE @dialog_handle UNIQUEIDENTIFIER ;  
DECLARE @conversation_group_id UNIQUEIDENTIFIER ;  
  
SET @conversation_group_id = <retrieve conversation group ID from database>  
  
BEGIN DIALOG CONVERSATION @dialog_handle  
   FROM SERVICE [//Adventure-Works.com/ExpenseClient]  
   TO SERVICE '//Adventure-Works.com/Expenses'  
   ON CONTRACT [//Adventure-Works.com/Expenses/ExpenseSubmission]  
   WITH RELATED_CONVERSATION_GROUP = @conversation_group_id ;  
```  
  
### E. Beginning a dialog with an explicit lifetime, and relating the dialog to an existing conversation  
 The following example begins a dialog conversation and stores an identifier for the dialog in `@dialog_handle`. The `//Adventure-Works.com/ExpenseClient` service is the initiator for the dialog, and the `//Adventure-Works.com/Expenses` service is the target of the dialog. The dialog follows the contract `//Adventure-Works.com/Expenses/ExpenseSubmission`. The new dialog belongs to the same conversation group that `@existing_conversation_handle` belongs to. If the dialog has not been closed by the END CONVERSATION command within `600` seconds, [!INCLUDE[ssSB](../../includes/sssb-md.md)] ends the dialog with an error.  
  
```  
DECLARE @dialog_handle UNIQUEIDENTIFIER  
DECLARE @existing_conversation_handle UNIQUEIDENTIFIER  
  
SET @existing_conversation_handle = <retrieve conversation handle from database>  
  
BEGIN DIALOG CONVERSATION @dialog_handle  
   FROM SERVICE [//Adventure-Works.com/ExpenseClient]  
   TO SERVICE '//Adventure-Works.com/Expenses'  
   ON CONTRACT [//Adventure-Works.com/Expenses/ExpenseSubmission]  
   WITH RELATED_CONVERSATION = @existing_conversation_handle  
   LIFETIME = 600 ;  
```  
  
### F. Beginning a dialog with optional encryption  
 The following example begins a dialog and stores an identifier for the dialog in `@dialog_handle`. The `//Adventure-Works.com/ExpenseClient` service is the initiator for the dialog, and the `//Adventure-Works.com/Expenses` service is the target of the dialog. The dialog follows the contract `//Adventure-Works.com/Expenses/ExpenseSubmission`. The conversation in this example allows the message to travel over the network without encryption if encryption is not available.  
  
```  
DECLARE @dialog_handle UNIQUEIDENTIFIER  
  
BEGIN DIALOG CONVERSATION @dialog_handle  
   FROM SERVICE [//Adventure-Works.com/ExpenseClient]  
   TO SERVICE '//Adventure-Works.com/Expenses'  
   ON CONTRACT [//Adventure-Works.com/Expenses/ExpenseSubmission]  
   WITH ENCRYPTION = OFF ;  
```  
  
## See Also  
 [BEGIN CONVERSATION TIMER &#40;Transact-SQL&#41;](../../t-sql/statements/begin-conversation-timer-transact-sql.md)   
 [END CONVERSATION &#40;Transact-SQL&#41;](../../t-sql/statements/end-conversation-transact-sql.md)   
 [MOVE CONVERSATION &#40;Transact-SQL&#41;](../../t-sql/statements/move-conversation-transact-sql.md)   
 [sys.conversation_endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-conversation-endpoints-transact-sql.md)  
  
  
