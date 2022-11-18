---
title: "RECEIVE (Transact-SQL)"
description: "RECEIVE retrieves one or more messages from a queue. Depending on the retention setting for the queue, either removes the message from the queue or updates the status of the message in the queue."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/25/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "RECEIVE_TSQL"
  - "RECEIVE"
helpviewer_keywords:
  - "queues [Service Broker], message retrieval"
  - "messages [Service Broker], retrieving"
  - "RECEIVE statement"
  - "receiving messages"
  - "retrieving messages"
dev_langs:
  - "TSQL"
---
# RECEIVE (Transact-SQL)
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

  Retrieves one or more messages from a queue. Depending on the retention setting for the queue, either removes the message from the queue or updates the status of the message in the queue.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
[ WAITFOR ( ]  
    RECEIVE [ TOP ( n ) ]   
        <column_specifier> [ ,...n ]  
        FROM <queue>  
        [ INTO table_variable ]  
        [ WHERE {  conversation_handle = conversation_handle  
                 | conversation_group_id = conversation_group_id } ]  
[ ) ] [ , TIMEOUT timeout ]  
[ ; ]  
  
<column_specifier> ::=  
{    *   
  |  { column_name | [ ] expression } [ [ AS ] column_alias ]  
}     [ ,...n ]   
  
<queue> ::=  
{ database_name.schema_name.queue_name | schema_name.queue_name | queue_name }
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### WAITFOR  
 Specifies that the RECEIVE statement waits for a message to arrive on the queue, if no messages are currently present.  
  
####  TOP( *n* )  
 Specifies the maximum number of messages to be returned. If this clause is not specified, all messages are returned that meet the statement criteria.  
  
#### column_specifier

 \*  
 Specifies that the result set contains all columns in the queue.  
  
 *column_name*  
 The name of a column to include in the result set.  
  
 *expression*  
 A column name, constant, function, or any combination of column names, constants, and functions connected by an operator.  
  
 *column_alias*  
 An alternative name to replace the column name in the result set.  
  
#### FROM  
 Specifies the queue that contains the messages to retrieve.  
  
 *database_name*  
 The name of the database that contains the queue to receive messages from. When no *database name* is provided, defaults to the current database.  
  
 *schema_name*  
 The name of the schema that owns the queue to receive messages from. When no *schema name* is provided, defaults to the default schema for the current user.  
  
 *queue_name*  
 The name of the queue to receive messages from.  
  
#### INTO *table_variable*  
 Specifies the table variable that RECEIVE places the messages into. The table variable must have the same number of columns as are in the messages. The data type of each column in the table variable must be implicitly convertible to the data type of the corresponding column in the messages. If INTO is not specified, the messages are returned as a result set.  
  
#### WHERE  
 Specifies the conversation or conversation group for the received messages. If omitted, returns messages from the next available conversation group.  
  
 conversation_handle = *conversation_handle*  
 Specifies the conversation for received messages. The *conversation handle* provided must be a **uniqueidentifer**, or a type that is convertible to **uniqueidentifier**.  
  
 conversation_group_id = *conversation_group_id*  
 Specifies the conversation group for received messages. The *conversation group ID that is* provided must be a **uniqueidentifier**, or a type convertible to **uniqueidentifier**.  
  
#### TIMEOUT *timeout*  
 Specifies the amount of time, in milliseconds, for the statement to wait for a message. This clause can only be used with the WAITFOR clause. If this clause is not specified, or the time-out is -**1**, the wait time is unlimited. If the time-out expires, RECEIVE returns an empty result set.  
  
## Remarks  
  
> [!IMPORTANT]  
>  If the RECEIVE statement is not the first statement in a batch or stored procedure, the preceding statement must be ended with a semi-colon (;).  
  
 The RECEIVE statement reads messages from a queue and returns a result set. The result set consists of zero or more rows, each of which contains one message. If the INTO clause is not used, and *column_specifier* does not assign the values to local variables, the statement returns a result set to the calling program.  
  
 The messages that are returned by the RECEIVE statement can be of different message types. Applications can use the `message_type_name` column to route each message to code that handles the associated message type. There are two classes of message types:  
  
-   Application-defined message types that were created by using the CREATE MESSAGE TYPE statement. The set of application-defined message types that are allowed in a conversation are defined by the [!INCLUDE[ssSB](../../includes/sssb-md.md)] contract that is specified for the conversation.  
  
-   [!INCLUDE[ssSB](../../includes/sssb-md.md)] system messages that return status or error information.  
  
 The RECEIVE statement removes received messages from the queue unless the queue specifies message retention. When the RETENTION setting for the queue is ON, the RECEIVE statement updates the `status` column to `0` and leaves the messages in the queue. When a transaction that contains a RECEIVE statement rolls back, all changes to the queue in the transaction are also rolled back, returning messages to the queue.  
  
 All messages that are returned by a RECEIVE statement belong the same conversation group. The RECEIVE statement locks the conversation group for the messages that are returned until the transaction that contains the statement finishes. A RECEIVE statement returns messages that have a `status` of `1`. The result set returned by a RECEIVE statement is implicitly ordered:  
  
-   If messages from multiple conversations meet the WHERE clause conditions, the RECEIVE statement returns all messages from one conversation before it returns messages for any other conversation. The conversations are processed in descending priority level order.  
  
-   For a given conversation, a RECEIVE statement returns messages in ascending `message_sequence_number` order.  
  
 The WHERE clause of the RECEIVE statement can only contain one search condition that uses either `conversation_handle` or `conversation_group_id`. The search condition cannot contain one or more of the other columns in the queue. The `conversation_handle` or `conversation_group_id` cannot be an expression. The set of messages that is returned depends on the conditions that are specified in the WHERE clause:  
  
-   If **conversation_handle** is specified, RECEIVE returns all messages from the specified conversation that are available in the queue.  
  
-   If **conversation_group_id** is specified, RECEIVE returns all messages that are available in the queue from any conversation that is a member of the specified conversation group.  
  
-   If there is no WHERE clause, RECEIVE determines which conversation group:  
  
    -   Has one or more messages in the queue.  
  
    -   Has not been locked by another RECEIVE statement.  
  
    -   Has the highest priority level of all the conversation groups that meet these criteria.  
  
     RECEIVE then returns all messages available in the queue from any conversation that is a member of the selected conversation group.  
  
 If the conversation handle or conversation group identifier specified in the WHERE clause does not exist, or is not associated with the specified queue, the RECEIVE statement returns an error.  
  
 If the queue specified in the RECEIVE statement has the queue status set to OFF, the statement fails with a [!INCLUDE[tsql](../../includes/tsql-md.md)] error.  
  
 When the WAITFOR clause is specified, the statement waits for the specified time out, or until a result set is available. If the queue is dropped or the status of the queue is set to OFF while the statement is waiting, the statement immediately returns an error. If the RECEIVE statement specifies a conversation group or conversation handle and the service for that conversation is dropped or moved to another queue, the RECEIVE statement reports a [!INCLUDE[tsql](../../includes/tsql-md.md)] error.  
  
 RECEIVE is not valid in a user-defined function.  
  
 The RECEIVE statement has no priority starvation prevention. If a single RECEIVE statement locks a conversation group and retrieves a lot of messages from low priority conversations, no messages can be received from high priority conversations in the group. To prevent this, when you are retrieving messages from low priority conversations, use the TOP clause to limit the number of messages retrieved by each RECEIVE statement.  
  
## Queue Columns  
 The following table lists the columns in a queue:  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**status**|**tinyint**|Status of the message. For messages returned by the RECEIVE command, the status is always **0**. Messages in the queue might contain one of the following values:<br /><br /> **0**=Ready**1**=Received message**2**=Not yet complete**3**=Retained sent message|  
|**priority**|**tinyint**|The conversation priority level that is applied to the message.|  
|**queuing_order**|**bigint**|Message order number in the queue.|  
|**conversation_group_id**|**uniqueidentifier**|Identifier for the conversation group that this message belongs to.|  
|**conversation_handle**|**uniqueidentifier**|Handle for the conversation that this message is part of.|  
|**message_sequence_number**|**bigint**|Sequence number of the message in the conversation.|  
|**service_name**|**nvarchar(512)**|Name of the service that the conversation is to.|  
|**service_id**|**int**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] object identifier of the service that the conversation is to.|  
|**service_contract_name**|**nvarchar(256)**|Name of the contract that the conversation follows.|  
|**service_contract_id**|**int**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] object identifier of the contract that the conversation follows.|  
|**message_type_name**|**nvarchar(256)**|Name of the message type that describes the format of the message. Messages can be either application message types or Broker system messages.|  
|**message_type_id**|**int**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] object identifier of the message type that describes the message.|  
|**validation**|**nchar(2)**|Validation used for the message.<br /><br /> **E**=Empty<BR>**N**=None<BR>**X**=XML|  
|**message_body**|**varbinary(MAX)**|Content of the message.|  
  
## Permissions  
 To receive a message, the current user must have RECEIVE permission on the queue.  
  
## Examples  
  
### A. Receiving all columns for all messages in a conversation group  
 The following example receives all available messages for the next available conversation group from the `ExpenseQueue` queue. The statement returns the messages as a result set.  
  
```sql  
RECEIVE * FROM ExpenseQueue ;  
```  
  
### B. Receiving specified columns for all messages in a conversation group  
 The following example receives all available messages for the next available conversation group from the `ExpenseQueue` queue. The statement returns the messages as a result set that contains the columns `conversation_handle`, `message_type_name`, and `message_body`.  
  
```sql  
RECEIVE conversation_handle, message_type_name, message_body  
FROM ExpenseQueue ;  
```  
  
### C. Receiving the first available message in the queue  
 The following example receives the first available message from the `ExpenseQueue` queue as a result set.  
  
```sql  
RECEIVE TOP (1) * FROM ExpenseQueue ;  
```  
  
### D. Receiving all messages for a specified conversation  
 The following example receives all available messages for the specified conversation from the `ExpenseQueue` queue as a result set.  
  
```sql  
DECLARE @conversation_handle UNIQUEIDENTIFIER ;  
  
SET @conversation_handle = <retrieve conversation from database> ;  
  
RECEIVE *  
FROM ExpenseQueue  
WHERE conversation_handle = @conversation_handle ;  
```  
  
### E. Receiving messages for a specified conversation group  
 The following example receives all available messages for the specified conversation group from the `ExpenseQueue` queue as a result set.  
  
```sql  
DECLARE @conversation_group_id UNIQUEIDENTIFIER ;  
  
SET @conversation_group_id =   
    <retrieve conversation group ID from database> ;  
  
RECEIVE *  
FROM ExpenseQueue  
WHERE conversation_group_id = @conversation_group_id ;  
```  
  
### F. Receiving into a table variable  
 The following example receives all available messages for a specified conversation group from the `ExpenseQueue` queue into a table variable.  
  
```sql  
DECLARE @conversation_group_id UNIQUEIDENTIFIER ;  
  
DECLARE @procTable TABLE(  
     service_instance_id UNIQUEIDENTIFIER,  
     handle UNIQUEIDENTIFIER,  
     message_sequence_number BIGINT,  
     service_name NVARCHAR(512),  
     service_contract_name NVARCHAR(256),  
     message_type_name NVARCHAR(256),  
     validation NCHAR,  
     message_body VARBINARY(MAX)) ;  
  
SET @conversation_group_id = <retrieve conversation group ID from database> ;  
  
RECEIVE TOP (1)  
    conversation_group_id,  
    conversation_handle,  
    message_sequence_number,  
    service_name,  
    service_contract_name,  
    message_type_name,  
    validation,  
    message_body  
FROM ExpenseQueue  
INTO @procTable  
WHERE conversation_group_id = @conversation_group_id ;  
```  
  
### G. Receiving messages and waiting indefinitely  
 The following example receives all available messages for the next available conversation group in the `ExpenseQueue` queue. The statement waits until at least one message becomes available then returns a result set that contains all message columns.  
  
```sql  
WAITFOR (  
    RECEIVE *  
    FROM ExpenseQueue) ;  
```  
  
### H. Receiving messages and waiting for a specified interval  
 The following example receives all available messages for the next available conversation group in the `ExpenseQueue` queue. The statement waits for 60 seconds or until at least one message becomes available, whichever occurs first. The statement returns a result set that contains all message columns if at least one message is available. Otherwise, the statement returns an empty result set.  
  
```sql  
WAITFOR (  
    RECEIVE *  
    FROM ExpenseQueue ),  
TIMEOUT 60000 ;  
```  
  
### I. Receiving messages, modifying the type of a column  
 The following example receives all available messages for the next available conversation group in the `ExpenseQueue` queue. When the message type states that the message contains an XML document, the statement converts the message body to XML.  
  
```sql  
WAITFOR (  
    RECEIVE message_type_name,  
        CASE  
            WHEN validation = 'X' THEN CAST(message_body as XML)  
            ELSE NULL  
         END AS message_body   
         FROM ExpenseQueue ),  
TIMEOUT 60000 ;  
```  
  
### J. Receiving a message, extracting data from the message body, retrieving conversation state  
 The following example receives the next available message for the next available conversation group in the `ExpenseQueue` queue. When the message is of type `//Adventure-Works.com/Expenses/SubmitExpense`, the statement extracts the employee ID and a list of items from the message body. The statement also retrieves state for the conversation from the `ConversationState` table.  
  
```sql  
WAITFOR(  
    RECEIVE   
    TOP(1)  
      message_type_name,  
      COALESCE(  
           (SELECT TOP(1) ConversationState  
            FROM CurrentConversations AS cc  
            WHERE cc.ConversationHandle = conversation_handle),  
           'NEW')  
      AS ConversationState,  
      COALESCE(  
          (SELECT TOP(1) ErrorCount  
           FROM CurrentConversations AS cc  
           WHERE cc.ConversationHandle = conversation_handle),   
           0)  
      AS ConversationErrors,  
      CASE WHEN message_type_name = N'//Adventure-Works.com/Expenses/SubmitExpense'  
          THEN CAST(message_body AS XML).value(  
                'declare namespace rpt = "https://Adventure-Works.com/schemas/expenseReport"  
                   (/rpt:ExpenseReport/rpt:EmployeeID)[1]', 'nvarchar(20)')  
         ELSE NULL  
      END AS EmployeeID,  
      CASE WHEN message_type_name = N'//Adventure-Works.com/Expenses/SubmitExpense'  
          THEN CAST(message_body AS XML).query(  
                'declare namespace rpt = "https://Adventure-Works.com/schemas/expenseReport"   
                     /rpt:ExpenseReport/rpt:ItemDetail')  
          ELSE NULL  
      END AS ItemList  
    FROM ExpenseQueue   
), TIMEOUT 60000 ;  
```  
  
## Next steps

 [BEGIN DIALOG CONVERSATION &#40;Transact-SQL&#41;](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)   
 [BEGIN CONVERSATION TIMER &#40;Transact-SQL&#41;](../../t-sql/statements/begin-conversation-timer-transact-sql.md)   
 [END CONVERSATION &#40;Transact-SQL&#41;](../../t-sql/statements/end-conversation-transact-sql.md)   
 [CREATE CONTRACT &#40;Transact-SQL&#41;](../../t-sql/statements/create-contract-transact-sql.md)   
 [CREATE MESSAGE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/create-message-type-transact-sql.md)   
 [SEND &#40;Transact-SQL&#41;](../../t-sql/statements/send-transact-sql.md)   
 [CREATE QUEUE &#40;Transact-SQL&#41;](../../t-sql/statements/create-queue-transact-sql.md)   
 [ALTER QUEUE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-queue-transact-sql.md)   
 [DROP QUEUE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-queue-transact-sql.md)  
