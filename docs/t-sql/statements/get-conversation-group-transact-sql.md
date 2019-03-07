---
title: "GET CONVERSATION GROUP (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "GET"
  - "CONVERSATION_GROUP_TSQL"
  - "GET_TSQL"
  - "GET_CONVERSATION_GROUP_TSQL"
  - "GET CONVERSATION GROUP"
  - "CONVERSATION GROUP"
  - "GET CONVERSATION"
  - "GET_CONVERSATION_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "GET CONVERSATION GROUP statement"
  - "conversations [Service Broker], groups"
ms.assetid: 4da8a855-33c0-43b2-a49d-527487cb3b5c
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# GET CONVERSATION GROUP (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the conversation group identifier for the next message to be received, and locks the conversation group for the conversation that contains the message. The conversation group identifier can be used to retrieve conversation state information before retrieving the message itself.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
[ WAITFOR ( ]  
   GET CONVERSATION GROUP @conversation_group_id  
      FROM <queue>  
[ ) ] [ , TIMEOUT timeout ]  
[ ; ]  
  
<queue> ::=  
{  
    [ database_name . [ schema_name ] . | schema_name . ] queue_name  
}  
```  
  
## Arguments  
 WAITFOR  
 Specifies that the GET CONVERSATION GROUP statement waits for a message to arrive on the queue if no messages are currently present.  
  
 *@conversation_group_id*  
 Is a variable used to store the conversation group ID returned by the GET CONVERSATION GROUP statement. The variable must be of type **uniqueidentifier**. If there are no conversation groups available, the variable is set to NULL.  
  
 FROM  
 Specifies the queue to get the conversation group from.  
  
 *database_name*  
 Is the name of the database that contains the queue to get the conversation group from. When no *database_name* is provided, defaults to the current database.  
  
 *schema_name*  
 Is the name of the schema that owns the queue to get the conversation group from. When no *schema_name* is provided, defaults to the default schema for the current user.  
  
 *queue_name*  
 Is the name of the queue to get the conversation group from.  
  
 TIMEOUT *timeout*  
 Specifies the length of time, in milliseconds, that Service Broker waits for a message to arrive on the queue. This clause may only be used with the WAITFOR clause. If a statement that uses WAITFOR does not include this clause or the *timeout* is -1, the wait time is unlimited. If the timeout expires, GET CONVERSATION GROUP sets the *@conversation_group_id* variable to NULL.  
  
## Remarks  
  
> [!IMPORTANT]  
>  If the GET CONVERSATION GROUP statement is not the first statement in a batch or stored procedure, the preceding statement must be terminated with a semicolon (**;**), the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement terminator.  
  
 If the queue specified in the GET CONVERSATION GROUP statement is unavailable, the statement fails with a [!INCLUDE[tsql](../../includes/tsql-md.md)] error.  
  
 This statement returns the next conversation group where all of the following is true:  
  
-   The conversation group can be successfully locked.  
  
-   The conversation group has messages available in the queue.  
  
-   The conversation group has the highest priority level of all the conversation groups that meet the previously-listed criteria. The priority level of a conversation group is the highest priority level assigned to any conversation that is a member of the group and has messages in the queue.  
  
 Successive calls to GET CONVERSATION GROUP within the same transaction may lock more than one conversation group. If no conversation group is available, the statement returns NULL as the conversation group identifier.  
  
 When the WAITFOR clause is specified, the statement waits for the timeout specified, or until a conversation group is available. If the queue is dropped while the statement is waiting, the statement immediately returns an error.  
  
 GET CONVERSATION GROUP is not valid in a user-defined function.  
  
## Permissions  
 To get a conversation group identifier from a queue, the current user must have RECEIVE permission on the queue.  
  
## Examples  
  
### A. Getting a conversation group, waiting indefinitely  
 The following example sets `@conversation_group_id` to the conversation group identifier for the next available message on `ExpenseQueue`. The command waits until a message becomes available.  
  
```  
DECLARE @conversation_group_id UNIQUEIDENTIFIER ;  
  
WAITFOR (  
 GET CONVERSATION GROUP @conversation_group_id  
     FROM ExpenseQueue  
) ;  
```  
  
### B. Getting a conversation group, waiting one minute  
 The following example sets `@conversation_group_id` to the conversation group identifier for the next available message on `ExpenseQueue`. If no message becomes available within one minute, GET CONVERSATION GROUP returns without changing the value of `@conversation_group_id`.  
  
```  
DECLARE @conversation_group_id UNIQUEIDENTIFIER  
  
WAITFOR (  
    GET CONVERSATION GROUP @conversation_group_id   
    FROM ExpenseQueue ),  
TIMEOUT 60000 ;  
```  
  
### C. Getting a conversation group, returning immediately  
 The following example sets `@conversation_group_id` to the conversation group identifier for the next available message on `ExpenseQueue`. If no message is available, `GET CONVERSATION GROUP` returns immediately without changing `@conversation_group_id`.  
  
```  
DECLARE @conversation_group_id UNIQUEIDENTIFIER ;  
  
GET CONVERSATION GROUP @conversation_group_id  
FROM AdventureWorks.dbo.ExpenseQueue ;  
```  
  
## See Also  
 [BEGIN DIALOG CONVERSATION &#40;Transact-SQL&#41;](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)   
 [MOVE CONVERSATION &#40;Transact-SQL&#41;](../../t-sql/statements/move-conversation-transact-sql.md)  
  
  
