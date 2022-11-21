---
title: Handling Poison Messages
description: "This topic describes one way that an application that uses Service Broker can detect a poison message and remove the message from the queue without relying on automatic poison message detection."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Handling Poison Messages

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This topic describes one way that an application that uses Service Broker can detect a poison message and remove the message from the queue without relying on automatic poison message detection.

Service Broker provides automatic poison message detection. Automatic poison message detection sets the queue status to OFF if a transaction that receives messages from the queue rolls back five times. This feature provides a safeguard against catastrophic failures that an application cannot detect programmatically. However, an application should not rely on this feature for normal processing. Because automatic poison message detection stops the queue, this feature effectively halts all processing for the application until the poison message is removed. Instead, an application should attempt to detect and remove poison messages as part of the application logic.

The strategy outlined in this section assumes that a message should be removed if it fails a certain number of times. For many applications, this assumption is valid. However, before using this strategy in your application, consider the following questions:

- Is a failure count reliable for your application? Depending on your application, it may be normal for messages to fail from time to time. For example, in an order entry application, the service that processes an order may take less processing time than the service that adds a new customer record. In this case, it may be normal that an order for a new customer cannot be processed immediately. The application needs to account for the delay when deciding whether a message is a poison message or not. The service may need to allow several failures before removing the message.

- Can your application quickly and reliably check the content of a message to detect that it can never succeed? If so, this is a better strategy than counting the number of times that the program failed to process the message. For example, an expense report that does not contain an employee name or an employee ID number cannot be processed. In this case, the program may be more efficient if it immediately responds to a message that cannot be processed with an error, rather than attempting to process the message. Consider other validation as well. For example, if the ID number is present, but falls outside the range of assigned numbers (for example, a negative number), the application can end the conversation immediately.

- Should you remove a message after any failure? If the application handles a high volume of messages where each message has a limited useful life, it may be most efficient to immediately remove any message that causes an operation to fail. For example, if the message provides a progress report from the target service, the initiating service may choose to discard an empty progress report by committing the receive without processing the message. In this case, the conversation continues.

Consider the following questions when you decide how the application handles a poison message:

- Should your application log the failure and the content of the message? In many cases, this is not necessary. However, for some applications, preserving the content of the message may be appropriate.

- Should your application log other information about the failure? In some cases, you may want to track other information about the conversation. For example, you might use the catalog view **sys.conversation_endpoints** to identify the remote broker instance that generated the poison message.

- Should your application end the conversation with an error, or should the contract for the service allow an application to indicate an error without closing the conversation? For many services, receiving a poison message means that the task described in the contract cannot be completed. In this case, the application ends the conversation with an error. In other cases, the conversation may be able to continue even though one message fails. For example, a service that receives inventory data from a warehouse floor may occasionally receive a message with an unknown part number. Rather than ending the conversation, the service may save the message in a separate table for an operator to inspect at a later time.

## Example: Detecting a Poison Message

This Transact-SQL example shows a simple, stateless service that includes logic for handling poison messages. Before the stored procedure receives a message, the procedure saves the transaction. When the procedure cannot process a message, the procedure rolls the transaction back to the save point. The partial rollback returns the message to the queue while continuing to hold a lock on the conversation group for the message. Because the program continues to hold the conversation group lock, the program can update a table that maintains a list of failed messages without risk that another queue reader might process the message.

The following example defines the activation stored procedure for the application:

```sql
    CREATE PROCEDURE ProcessExpenseReport
    AS
    BEGIN
      WHILE (1 = 1)
        BEGIN
          BEGIN TRANSACTION ;
          DECLARE @conversationHandle UNIQUEIDENTIFIER ;
          DECLARE @messageBody VARBINARY(MAX) ;
          DECLARE @messageTypeName NVARCHAR(256) ;

          SAVE TRANSACTION UndoReceive ;

            WAITFOR (
                      RECEIVE TOP(1)
                        @messageTypeName = message_type_name,
                        @messageBody = message_body,
                        @conversationHandle = conversation_handle
                        FROM ExpenseQueue
                     ), TIMEOUT 500 ;

            IF @@ROWCOUNT = 0
            BEGIN
              ROLLBACK TRANSACTION ;
              BREAK ;
            END ;

            -- Typical message processing loop: dispatch to a stored
            -- procedure based on the message type name.  End conversation
            -- with an error for unknown message types.

            -- Process expense report messages. If processing fails,
            -- roll back to the save point and track the failed message.

            IF (@messageTypeName =
                  '//Adventure-Works.com/AccountsPayable/ExpenseReport')
              BEGIN
                DECLARE @expenseReport NVARCHAR(MAX) ;
                SET @expenseReport = CAST(@messageBody AS NVARCHAR(MAX)) ;
                EXEC AdventureWorks2008R2.dbo.AddExpenseReport
                  @report = @expenseReport ;
                IF @@ERROR <> 0
                 BEGIN
                   ROLLBACK TRANSACTION UndoReceive ;
                   EXEC TrackMessage @conversationHandle ;
                 END ;
                ELSE
                 BEGIN
                   EXEC AdventureWorks2008R2.dbo.ClearMessageTracking
                     @conversationHandle ;
                 END ;
               END ;
            ELSE

            -- For error messages and end dialog messages, end the
            -- conversation.

            IF (@messageTypeName =
                  'https://schemas.microsoft.com/SQL/ServiceBroker/Error' OR
                 @messageTypeName =
                  'https://schemas.microsoft.com/SQL/ServiceBroker/EndDialog')
              BEGIN
                END CONVERSATION @conversationHandle ;
                EXEC dbo.ClearMessageTracking @conversationHandle ;
              END ;


             COMMIT TRANSACTION ;
        END ;
    END ;
```

The stored procedure TrackMessage tracks the number of times that a message has failed. When the message has not failed before, the procedure inserts a new counter for the message into the table ExpenseServiceFailedMessages. Otherwise, the procedure checks the counter for the number of times that the message failed. The procedure increments the counter when the counter is less than a predefined number. When the counter is greater than the predefined number, the procedure ends the conversation with an error and removes the counter for the conversation from the table.

```sql
    CREATE PROCEDURE TrackMessage
    @conversationHandle uniqueidentifier
    AS
    BEGIN
      IF @conversationHandle IS NULL
        RETURN ;

      DECLARE @count INT ;
      SET @count = NULL ;
      SET @count = (SELECT count FROM dbo.ExpenseServiceFailedMessages
                      WHERE conversation_handle = @conversationHandle) ;

      IF @count IS NULL
        BEGIN
          INSERT INTO dbo.ExpenseServiceFailedMessages
            (count, conversation_handle)
            VALUES (1, @conversationHandle) ;
        END ;
      IF @count > 3
        BEGIN
          EXEC dbo.ClearMessageTracking @conversationHandle ;
          END CONVERSATION @conversationHandle
            WITH ERROR = 500
            DESCRIPTION = 'Unable to process message.' ;
        END ;
      ELSE
        BEGIN
          UPDATE dbo.ExpenseServiceFailedMessages
            SET count=count+1
            WHERE conversation_handle = @conversationHandle ;
        END ;
    END ;
    GO
```

The definition of table ExpenseServiceFailedMessages simply contains a conversation_handle column and a count column, as shown in the following sample:

```sql
    CREATE TABLE ExpenseServiceFailedMessages (
      conversation_handle uniqueidentifier PRIMARY KEY,
      count smallint
    ) ;
```

The procedure ClearMessageTracking deletes the counter for a conversation from the table ExpenseServiceFailedMessages, as shown in the following sample:

```sql
    CREATE PROCEDURE ClearMessageTracking
      @conversationHandle uniqueidentifier
    AS
    BEGIN
       DELETE FROM dbo.ExpenseServiceFailedMessages
         WHERE conversation_handle = @conversationHandle ;
    END ;
    GO
```

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

The strategy shown here is deliberately simple. You should use the ideas in this topic as a basis for building an application that matches your needs. For example, if your application maintains state, it may be more efficient to include the tracking information for failed messages in the state tables for the application.

The stored procedures above do not handle errors that would cause a transaction to fail. If this service receives a message that causes the entire transaction to fail, the transaction will roll back. If this happens five times, automatic poison message detection will set the queue status to OFF. In this case, the poison message must be removed by a different application or by an administrator.

If you believe that the processing you perform on the message might cause a transaction failure, you can use TRY and CATCH statements to handle the error. For more information on handling errors, see [Understanding Database Engine Errors](../../relational-databases/errors-events/understanding-database-engine-errors.md).

## See also

- [TRY...CATCH (Transact-SQL)](../../t-sql/language-elements/try-catch-transact-sql.md)
- [sys.conversation_endpoints (Transact-SQL)](../../relational-databases/system-catalog-views/sys-conversation-endpoints-transact-sql.md)
- [Removing Poison Messages](removing-poison-messages.md)
- [Understanding Database Engine Errors](../../relational-databases/errors-events/understanding-database-engine-errors.md)
