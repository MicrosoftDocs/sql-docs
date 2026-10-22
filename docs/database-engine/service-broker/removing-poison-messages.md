---
title: Remove Poison Messages
description: "A poison message is a message containing information that an application cannot successfully process."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Remove poison messages

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

A poison message is a message containing information that an application can't successfully process. For example, a manufacturing workstation might submit a request to withdraw a part from inventory just before a change order makes the part obsolete. The change order becomes effective while the request for inventory is in transit. The inventory management application receives the request from the workstation, but can't successfully process the request, and the database operation to update the number of parts in stock fails. The transaction containing the receive operation then rolls back, returning the message to the queue. In this situation, the application continues to receive the same message, the update continues to fail, and the message returns to the queue.

A poison message isn't a corrupt message, and might not be an invalid request. Service Broker contains message integrity checks that detect corrupt messages. An application also typically validates the content of a message, and discards a message that contains an illegal request. In contrast, many poison messages were valid when the message was created, but later became impossible to process.

## Automatic poison message detection

Service Broker provides automatic poison message detection. When a transaction that contains a `RECEIVE` statement rolls back five times, Service Broker disables all queues from which the transaction received messages by automatically setting the queue status to `OFF`. In addition, Service Broker generates an event of type **Broker:Queue Disabled**.

An administrator might use SQL Server Agent alerts to be notified when a queue is disabled. A developer can also create an application that detects when a queue is disabled by Service Broker. That application often inspects the messages in the queue to find the poison message. Once the application determines which message can't be processed, the application sets the queue status to `ON` and ends the conversation for the message with an error. An application that detects poison messages must be careful to clean up any state associated with the conversation when ending the conversation. For more information on creating an application to recover from poison messages, see [Handle poison messages](handling-poison-messages.md).

## Remove poison messages administratively

Most applications should track and remove poison messages programmatically. However, it might sometimes be necessary to remove a poison message manually. For example, the part of the application that performs recovery might not be able to detect the poison message, or might not be able to safely clean up the saved state for the conversation.

Removing a message manually runs the risk of interrupting an important conversation. Therefore, always inspect a poison message before removing the message from the queue. To see the content of the message, begin a transaction, receive the message body, display the message body, and then roll back the transaction. Until you're positive that the message in question is a poison message, it's important to roll back the transaction.

### Example

The following example shows how to inspect a message safely for the conversation handle e29059bb-9922-40f4-a575-66b2e4c70cf9 in the queue ExpenseQueue.[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

```sql
USE AdventureWorks2008R2;
GO

-- Sample to show the content of a message, then return
-- the message to the queue. This may be useful to determine
-- whether a specific message cannot be processed due to the
-- content of the message.

-- Every exit path from the transaction rolls back the transaction.
-- This code is intended to inspect the message, not remove the
-- message from the queue permanently. The transaction must roll
-- back to return the message to the queue.
BEGIN TRANSACTION;

-- To print the body, the code needs the message_body and
-- the encoding_format.
DECLARE @messageBody AS VARBINARY (MAX),
    @validation AS NCHAR;

-- Receive the message. The WAITFOR handles the case where
-- an application is attempting to process the message when
-- this batch is submitted. Replace the name of the queue and
-- the conversation_handle value.
WAITFOR (RECEIVE TOP (1)
        @messageBody = message_body,
        @validation = validation
    FROM dbo.ExpenseQueue
    WHERE CONVERSATION_HANDLE = 'e29059bb-9922-40f4-a575-66b2e4c70cf9'),
TIMEOUT 2000;

-- Roll back and exit if the message is not available
-- in two seconds.
IF @@ROWCOUNT = 0
    BEGIN
        ROLLBACK TRANSACTION;
        PRINT 'No message available.';
        RETURN;
    END

      -- Print the message based on the encoding format of
      -- the message body.
IF (@validation = 'E')
    BEGIN
        PRINT 'Empty message.';
    END
ELSE IF (@validation = 'X')
        BEGIN
            PRINT CONVERT (NVARCHAR (MAX), @messageBody);
        END
ELSE IF (@validation = 'N')
        BEGIN
            PRINT 'No validation -- binary message:';
            PRINT @messageBody;
        END

ROLLBACK TRANSACTION;
GO
```

When you find a poison message, you end the conversation. The following example ends the conversation e29059bb-9922-40f4-a575-66b2e4c70cf9:

```sql
-- End the conversation. Do this only if the message cannot be
-- processed by the normal procedure.
END CONVERSATION 'e29059bb-9922-40f4-a575-66b2e4c70cf9'
    WITH ERROR = 127 DESCRIPTION = N'Unable to process message.';
GO
```

When a conversation ends, Service Broker discards the messages for that conversation. The application that normally processes the message doesn't receive an **EndDialog** or **Error** message for this conversation. Therefore, if the application maintains state, you must be careful to remove the state associated with the conversation after ending the conversation with an error.

If a service can't process a message, this means that the service can't complete the task for the conversation. Ending the conversation with an error notifies the other participant in the conversation that the task failed.

## Related content

- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
- [END CONVERSATION (Transact-SQL)](../../t-sql/statements/end-conversation-transact-sql.md)
- [Query queues](querying-queues.md)
