---
title: "MOVE CONVERSATION (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "MOVE_CONVERSATION_TSQL"
  - "MOVE CONVERSATION"
  - "MOVE_TSQL"
  - "MOVE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "moving conversations"
  - "MOVE CONVERSATION statement"
  - "relocating conversations"
  - "conversations [Service Broker], groups"
  - "conversations [Service Broker], moving"
ms.assetid: 1da4d2c9-e767-434e-b49b-615711a7f626
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# MOVE CONVERSATION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Moves a conversation to a different conversation group.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
MOVE CONVERSATION conversation_handle  
   TO conversation_group_id  
[ ; ]  
```  
  
## Arguments  
 *conversation_handle*  
 Is a variable or constant containing the conversation handle of the conversation to be moved. *conversation_handle* must be of type **uniqueidentifier**.  
  
 TO *conversation_group_id*  
 Is a variable or constant containing the identifier of the conversation group where the conversation is to be moved. *conversation_group_id* must be of type **uniqueidentifier**.  
  
## Remarks  
 The MOVE CONVERSATION statement moves the conversation specified by *conversation_handle* to the conversation group identified by *conversation_group_id*. Dialogs can be only be redirected between conversation groups that are associated with the same queue.  
  
> [!IMPORTANT]  
>  If the MOVE CONVERSATION statement is not the first statement in a batch or stored procedure, the preceding statement must be terminated with a semicolon (**;**), the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement terminator.  
  
 The MOVE CONVERSATION statement locks the conversation group associated with *conversation_handle* and the conversation group specified by *conversation_group_id* until the transaction containing the statement commits or rolls back.  
  
 MOVE CONVERSATION is not valid in a user-defined function.  
  
## Permissions  
 To move a conversation, the current user must be the owner of the conversation and the conversation group, or be a member of the sysadmin fixed server role, or be a member of the db_owner fixed database role.  
  
## Examples  
 The following example moves a conversation to a different conversation group.  
  
```  
DECLARE @conversation_handle UNIQUEIDENTIFIER,  
        @conversation_group_id UNIQUEIDENTIFIER ;  
  
SET @conversation_handle =  
    <retrieve conversation handle from database> ;  
SET @conversation_group_id =  
    <retrieve conversation group ID from database> ;  
  
MOVE CONVERSATION @conversation_handle TO @conversation_group_id ;  
```  
  
## See Also  
 [BEGIN DIALOG CONVERSATION &#40;Transact-SQL&#41;](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)   
 [GET CONVERSATION GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/get-conversation-group-transact-sql.md)   
 [END CONVERSATION &#40;Transact-SQL&#41;](../../t-sql/statements/end-conversation-transact-sql.md)   
 [sys.conversation_groups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-conversation-groups-transact-sql.md)   
 [sys.conversation_endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-conversation-endpoints-transact-sql.md)  
  
  
