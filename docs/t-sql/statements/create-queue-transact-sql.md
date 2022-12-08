---
title: "CREATE QUEUE (Transact-SQL)"
description: CREATE QUEUE (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "10/21/2021"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "QUEUE_TSQL"
  - "CREATE QUEUE"
  - "QUEUE"
  - "CREATE_QUEUE_TSQL"
helpviewer_keywords:
  - "CREATE QUEUE statement"
  - "internal activation [Service Broker]"
  - "stored procedure activation [Service Broker]"
  - "message retention [Service Broker]"
  - "unavailable queues [Service Broker]"
  - "activation stored procedures [Service Broker]"
  - "queues [Service Broker], creating"
dev_langs:
  - "TSQL"
---
# CREATE QUEUE (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates a new queue in a database. Queues store messages. When a message arrives for a service, [!INCLUDE[ssSB](../../includes/sssb-md.md)] puts the message on the queue associated with the service.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CREATE QUEUE <object>
   [ WITH
     [ STATUS = { ON | OFF } [ , ] ]
     [ RETENTION = { ON | OFF } [ , ] ]
     [ ACTIVATION (
         [ STATUS = { ON | OFF } , ]
           PROCEDURE_NAME = <procedure> ,
           MAX_QUEUE_READERS = max_readers ,
           EXECUTE AS { SELF | 'user_name' | OWNER }
            ) [ , ] ]
     [ POISON_MESSAGE_HANDLING (
         [ STATUS = { ON | OFF } ] ) ]
    ]
     [ ON { filegroup | [ DEFAULT ] } ]
[ ; ]

<object> ::=
{ database_name.schema_name.queue_name | schema_name.queue_name | queue_name }

<procedure> ::=
{ database_name.schema_name.stored_procedure_name | schema_name.stored_procedure_name | stored_procedure_name }

```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *database_name* (object)
Is the name of the database within which to create the new queue. *database_name* must specify the name of an existing database. When *database_name* is not provided, the queue is created in the current database.

#### *schema_name* (object)
Is the name of the schema to which the new queue belongs. The schema defaults to the default schema for the user that executes the statement. If the CREATE QUEUE statement is executed by a member of the sysadmin fixed server role, or a member of the db_dbowner or db_ddladmin fixed database roles in the database specified by *database_name*, *schema_name* can specify a schema other than the one associated with the login of the current connection. Otherwise, *schema_name* must be the default schema for the user who executes the statement.

#### *queue_name*
Is the name of the queue to create. This name must meet the guidelines for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifiers.

#### STATUS (Queue)
Specifies whether the queue is available (ON) or unavailable (OFF). When the queue is unavailable, no messages can be added to the queue or removed from the queue. You can create the queue in an unavailable state to keep messages from arriving on the queue until the queue is made available with an ALTER QUEUE statement. If this clause is omitted, the default is ON, and the queue is available.

#### RETENTION
Specifies the retention setting for the queue. If RETENTION = ON, all messages sent or received on conversations that use this queue are retained in the queue until the conversations have ended. This lets you retain messages for auditing purposes, or to perform compensating transactions if an error occurs. If this clause is not specified, the retention setting defaults to OFF.

> [!NOTE]
> Setting RETENTION = ON can decrease performance. This setting should only be used if it is required for the application.

#### ACTIVATION
Specifies information about which stored procedure you have to start to process messages in this queue.

#### STATUS (Activation)
Specifies whether [!INCLUDE[ssSB](../../includes/sssb-md.md)] starts the stored procedure. When STATUS = ON, the queue starts the stored procedure specified with PROCEDURE_NAME when the number of procedures currently running is less than MAX_QUEUE_READERS and when messages arrive on the queue faster than the stored procedures receive messages. When STATUS = OFF, the queue does not start the stored procedure. If this clause is not specified, the default is ON.

#### PROCEDURE_NAME = \<procedure>
Specifies the name of the stored procedure to start to process messages in this queue. This value must be a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifier.

*database_name*(procedure)
Is the name of the database that contains the stored procedure.

*schema_name*(procedure)
Is the name of the schema that contains the stored procedure.

*procedure_name*
Is the name of the stored procedure.

#### MAX_QUEUE_READERS =*max_readers*
Specifies the maximum number of instances of the activation stored procedure that the queue starts at the same time. The value of *max_readers* must be a number between **0** and **32767**.

#### EXECUTE AS
Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database user account under which the activation stored procedure runs. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be able to check the permissions for this user at the time that the queue starts the stored procedure. For a domain user, the server must be connected to the domain when the procedure is started or activation fails. For a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user, the server can always check permissions.

SELF
Specifies that the stored procedure executes as the current user. (The database principal executing this CREATE QUEUE statement.)

'*user_name*'
Is the name of the user who the stored procedure executes as. The *user_name* parameter must be a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user specified as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifier. The current user must have IMPERSONATE permission for the *user_name* specified.

OWNER
Specifies that the stored procedure executes as the owner of the queue.

#### POISON_MESSAGE_HANDLING
Specifies whether poison message handling is enabled for the queue. The default is ON.

A queue that has poison message handling set to OFF will not be disabled after five consecutive transaction rollbacks. This allows for a custom poison message handing system to be defined by the application.

#### ON *filegroup |* [**DEFAULT**]
Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] filegroup on which to create this queue. You can use the *filegroup* parameter to identify a filegroup, or use the DEFAULT identifier to use the default filegroup for the service broker database. In the context of this clause, DEFAULT is not a keyword, and must be delimited as an identifier. When no filegroup is specified, the queue uses the default filegroup for the database.

## Remarks

A queue can be the target of a SELECT statement. However, the contents of a queue can only be modified using statements that operate on [!INCLUDE[ssSB](../../includes/sssb-md.md)] conversations, such as SEND, RECEIVE, and END CONVERSATION. A queue cannot be the target of an INSERT, UPDATE, DELETE, or TRUNCATE statement.

A queue might not be a temporary object. Therefore, queue names starting with **#** are not valid.

Creating a queue in an inactive state lets you get the infrastructure in place for a service before allowing messages to be received on the queue.

[!INCLUDE[ssSB](../../includes/sssb-md.md)] does not stop activation stored procedures when there are no messages on the queue. An activation stored procedure should exit when no messages are available on the queue for a short time.

Permissions for the activation stored procedure are checked when [!INCLUDE[ssSB](../../includes/sssb-md.md)] starts the stored procedure, not when the queue is created. The CREATE QUEUE statement does not verify that the user specified in the EXECUTE AS clause has permission to execute the stored procedure specified in the PROCEDURE NAME clause.

When a queue is unavailable, [!INCLUDE[ssSB](../../includes/sssb-md.md)] holds messages for services that use the queue in the transmission queue for the database. The `sys.transmission_queue` catalog view provides a view of the transmission queue.

A queue is a schema-owned object. Queues appear in the `sys.objects` catalog view.

The following table lists the columns in a queue.

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|status|**tinyint**|Status of the message. The RECEIVE statement returns all messages that have a status of **1**. If message retention is on, the status is then set to 0. If message retention is off, the message is deleted from the queue. Messages in the queue can contain one of the following values:<br /><br /> **0**=Retained received message<br /><br /> **1**=Ready to receive<br /><br /> **2**=Not yet complete<br /><br /> **3**=Retained sent message|
|priority|**tinyint**|The priority level that is assigned to this message.|
|queuing_order|**bigint**|Message order number in the queue.|
|conversation_group_id|**uniqueidentifier**|Identifier for the conversation group that this message belongs to.|
|conversation_handle|**uniqueidentifier**|Handle for the conversation that this message is part of.|
|message_sequence_number|**bigint**|Sequence number of the message in the conversation.|
|service_name|**nvarchar(512)**|Name of the service that the conversation is to.|
|service_id|**int**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] object identifier of the service that the conversation is to.|
|service_contract_name|**nvarchar(256)**|Name of the contract that the conversation follows.|
|service_contract_id|**int**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] object identifier of the contract that the conversation follows.|
|message_type_name|**nvarchar(256)**|Name of the message type that describes the message.|
|message_type_id|**int**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] object identifier of the message type that describes the message.|
|validation|**nchar(2)**|Validation used for the message.<br /><br /> E=Empty<br /><br /> N=None<br /><br /> X=XML|
|message_body|**varbinary(max)**|Content of the message.|
|message_enqueue_time|**datetime**|Time when the message was enqueued.|

## Permissions

Permission for creating a queue uses members of the `db_ddladmin` or `db_owner` fixed database roles, or the `sysadmin` fixed server role.

`REFERENCES` permission for a queue defaults to the owner of the queue, members of the `db_ddladmin` or `db_owner` fixed database roles, or members of the `sysadmin` fixed server role.

`RECEIVE` permission for a queue defaults to the owner of the queue, members of the `db_owner` fixed database role, or members of the `sysadmin` fixed server role.

## Examples

### A. Create a queue with no parameters

The following example creates a queue that is available to receive messages. No activation stored procedure is specified for the queue.

```sql
CREATE QUEUE ExpenseQueue;
```

### B. Create an unavailable queue

The following example creates a queue that is unavailable to receive messages. No activation stored procedure is specified for the queue.

```sql
CREATE QUEUE ExpenseQueue WITH STATUS=OFF;
```

### C. Create a queue and specify internal activation information

The following example creates a queue that is available to receive messages. The queue starts the stored procedure `expense_procedure` when a message enters the queue. The stored procedure executes as the user `ExpenseUser`. The queue starts a maximum of `5` instances of the stored procedure.

```sql
CREATE QUEUE ExpenseQueue
    WITH STATUS=ON,
    ACTIVATION (
        PROCEDURE_NAME = expense_procedure
        , MAX_QUEUE_READERS = 5
        , EXECUTE AS 'ExpenseUser' );
```

### D. Create a queue on a specific filegroup

The following example creates a queue on the filegroup `ExpenseWorkFileGroup`.

```sql
CREATE QUEUE ExpenseQueue
    ON ExpenseWorkFileGroup;
```

### E. Create a queue with multiple parameters

The following example creates a queue on the `DEFAULT` filegroup. The queue is unavailable. Messages are retained in the queue until the conversation that they belong to ends. When the queue is made available through `ALTER QUEUE`, the queue starts the stored procedure `AdventureWorks2012.dbo.expense_procedure` to process messages. The stored procedure executes as the user who ran the `CREATE QUEUE` statement. The queue starts a maximum of `10` instances of the stored procedure.

```sql
CREATE QUEUE ExpenseQueue
    WITH STATUS = OFF
      , RETENTION = ON
      , ACTIVATION (
          PROCEDURE_NAME = AdventureWorks2012.dbo.expense_procedure
          , MAX_QUEUE_READERS = 10
          , EXECUTE AS SELF )
    ON [DEFAULT];
```

## Next steps

- [ALTER QUEUE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-queue-transact-sql.md)
- [CREATE SERVICE &#40;Transact-SQL&#41;](../../t-sql/statements/create-service-transact-sql.md)
- [DROP QUEUE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-queue-transact-sql.md)
- [RECEIVE &#40;Transact-SQL&#41;](../../t-sql/statements/receive-transact-sql.md)
- [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)
