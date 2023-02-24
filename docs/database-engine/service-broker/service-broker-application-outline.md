---
title: Service Broker Application Outline
description: "Most Service Broker applications follow the same basic steps to receive and process messages"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Service Broker Application Outline

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Most Service Broker applications follow the same basic steps to receive and process messages:

1.  The application begins a transaction.

2.  If the application maintains state, the application gets a conversation group identifier. The application uses this identifier to restore state from a state table. If there is no conversation group with has messages that are ready to be received, the application rolls back the transaction and exits.

3.  The application receives one or more messages from the queue. If the application has a conversation group identifier, the application uses the conversation group identifier to receive messages for that conversation group. If no more messages are available to be received, the application commits the transaction and returns to Step 1.

4.  The application validates the content of the messages based on the message type name.

5.  The application processes the messages based on the message type name and the content of the message.

6.  The application sends any messages that result from the processing.

7.  If the application maintains state, the application updates the state table, using the conversation group identifier as the primary key for the table.

8.  The application returns to Step 3 to check whether more messages are available.

The precise structure of the application depends on the requirements of the application, the communication style of the application, whether the application is a target service or an initiating service, and whether Service Broker activates the application or not.

For example, an initiating application sends a message before it starts the processing loop outlined in the preceding steps. The initiating service may send a message from another program or stored procedure, and then use an activation stored procedure for the initiating service queue. For example, an order entry application can include an external application that initiates the conversation to enter the order. After the order is entered, the external application need not remain running. An activation stored procedure for the initiating service sends the order confirmation when a response returns from the order service. The activation stored procedure also processes any Service Broker error messages that are returned by the target service and sends notifications that the order could not be confirmed.

Alternatively, rather than sending a message from a different program, the initiating application may send a message and then start the processing loop as part of the same program. Regardless of these variations, the basic outline remains the same.

An application that processes a large number of messages in the same conversation group may keep a count of messages received and commit a transaction after a processing a certain number of messages. This count-and-commit strategy allows the application to keep transactions relatively short, and lets the application process different conversation groups.

## Example

The following Transact-SQL example processes all messages on the queue **MyServiceQueue**. The processing for the message is minimal. If the message is an **EndDialog** or **Error** message, the code ends the conversation. For any other message, the code creates an XML representation of the message and produces a result set that contains the conversation handle, the message type name, and the XML. When no messages are available for 500 milliseconds, the code exits.

For simplicity, the script produces a result set for each message. If an error occurs while reading from the queue, the script commits the changes without producing any result. Therefore, this script will silently remove any messages that cause an error.

> [!NOTE]
> Because the script simply displays messages, no poison messages are possible for this script. Therefore, the script does not contain code to handle poison messages. A production application should be written to handle poison messages. For more information on poison messages, see [Handling Poison Messages](handling-poison-messages.md).

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

```sql
    USE AdventureWorks2008R2 ;
    GO

    -- Process all conversation groups.

    WHILE (1 = 1)
    BEGIN

    DECLARE @conversation_handle UNIQUEIDENTIFIER,
            @conversation_group_id UNIQUEIDENTIFIER,
            @message_body XML,
            @message_type_name NVARCHAR(128);


    -- Begin a transaction, one per conversation group.

    BEGIN TRANSACTION ;

    -- Get next conversation group.

    WAITFOR(
       GET CONVERSATION GROUP @conversation_group_id FROM MyServiceQueue),
       TIMEOUT 500 ;

    -- Restore the state for this conversation group here

    -- If there are no more conversation groups, break.

    IF @conversation_group_id IS NULL
    BEGIN
        ROLLBACK TRANSACTION ;
        BREAK ;
    END ;

        -- Process all messages in the conversation group.

        WHILE 1 = 1
        BEGIN

            -- Get the next message.

            RECEIVE
               TOP(1)
               @conversation_handle = conversation_handle,
               @message_type_name = message_type_name,
               @message_body =
               CASE
                  WHEN validation = 'X' THEN CAST(message_body AS XML)
                  ELSE CAST(N'<none/>' AS XML)
              END
           FROM MyServiceQueue
           WHERE conversation_group_id = @conversation_group_id;

           -- If there is no message, or there is an error
           -- reading from the queue, break.

           IF @@ROWCOUNT = 0 OR @@ERROR <> 0
               BREAK;

           -- Process the message. In this case, the program ends the conversation
           -- for Error and EndDialog messages. For all other messages, the program
           -- produces a result set with information about the message.

           SELECT @conversation_handle,
                  @message_type_name,
                  @message_body ;

           -- If the message is an end dialog message or an error,
           -- end the conversation. Notice that other conversations
           -- in the same conversation group may still have messages
           -- to process. Therefore, the program does not break after
           -- ending the conversation.

           IF @message_type_name =
                  'https://schemas.microsoft.com/SQL/ServiceBroker/EndDialog'
              OR @message_type_name =
                  'https://schemas.microsoft.com/SQL/ServiceBroker/Error'
           BEGIN
              END CONVERSATION @conversation_handle ;
           END ;

        END ; -- Process all messages in conversation group.

       COMMIT TRANSACTION ;

    END ; -- Process all conversation groups.
```

## See also

- [END CONVERSATION (Transact-SQL)](../../t-sql/statements/end-conversation-transact-sql.md)
- [GET CONVERSATION GROUP (Transact-SQL)](../../t-sql/statements/get-conversation-group-transact-sql.md)
- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
