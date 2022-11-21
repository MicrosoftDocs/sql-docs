---
title: "Lesson 3: Beginning a Conversation and Transmitting Messages"
description: "In this lesson, you will learn to complete a simple request-reply message cycle in a system configured with an internal activation stored procedure."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Lesson 3: Beginning a Conversation and Transmitting Messages

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you will learn to complete a simple request-reply message cycle in a system configured with an internal activation stored procedure.

## Procedures

### Switch to the AdventureWorks2008R2 database

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

- Copy and paste the following code into a Query Editor window. Then, run it to switch context to the **AdventureWorks2008R2** database.

    ```sql
        USE AdventureWorks2008R2;
        GO
    ```

### Begin a conversation and send a request message

- Copy and paste the following code into a Query Editor window. Then, run it to start a conversation and send a request message to the **//AWDB/InternalAct/TargetService**. The code must be run in one block because a variable is used to pass a dialog handle from BEGIN DIALOG to the SEND statement. The batch runs the BEGIN DIALOG statement to start the conversation. It builds a request message, and then uses the dialog handle in a SEND statement to send the request message on that conversation. The last SELECT statement displays the text of the message that was sent.

    ```sql
        DECLARE @InitDlgHandle UNIQUEIDENTIFIER;
        DECLARE @RequestMsg NVARCHAR(100);

        BEGIN TRANSACTION;

        BEGIN DIALOG @InitDlgHandle
             FROM SERVICE
              [//AWDB/InternalAct/InitiatorService]
             TO SERVICE
              N'//AWDB/InternalAct/TargetService'
             ON CONTRACT
              [//AWDB/InternalAct/SampleContract]
             WITH
                 ENCRYPTION = OFF;

        -- Send a message on the conversation
        SELECT @RequestMsg =
               N'<RequestMsg>Message for Target service.</RequestMsg>';

        SEND ON CONVERSATION @InitDlgHandle
             MESSAGE TYPE
             [//AWDB/InternalAct/RequestMessage]
             (@RequestMsg);

        -- Diplay sent request.
        SELECT @RequestMsg AS SentRequestMsg;

        COMMIT TRANSACTION;
        GO
    ```

### Receive the request and send a reply

- When you send the request message, Service Broker automatically activates a copy of **TargetActiveProc**. The stored procedure receives the reply message from the **TargetQueueIntAct** and sends a reply message back to the initiator.

### Receive the reply and end the conversation

- Copy and paste the following code into a Query Editor window. Then, run it to receive the reply message and end the conversation. The RECEIVE statement retrieves the reply message from the **InitiatorQueueIntAct**. The END CONVERSATION statement ends the initiator side of the conversation and sends an **EndDialog** message to the target service. The last SELECT statement displays the text of the reply message so that you can confirm it is the same as what was sent in the previous step.

   ```sql
        DECLARE @RecvReplyMsg NVARCHAR(100);
        DECLARE @RecvReplyDlgHandle UNIQUEIDENTIFIER;

        BEGIN TRANSACTION;

        WAITFOR
        ( RECEIVE TOP(1)
            @RecvReplyDlgHandle = conversation_handle,
            @RecvReplyMsg = message_body
            FROM InitiatorQueueIntAct
        ), TIMEOUT 5000;

        END CONVERSATION @RecvReplyDlgHandle;

        -- Display recieved request.
        SELECT @RecvReplyMsg AS ReceivedReplyMsg;

        COMMIT TRANSACTION;
        GO
   ```

### End the target side of the conversation

- When you run the END CONVERSATION statement for the initiator, Service Broker sends an **EndDialog** message to the **TargetQueueIntAct** queue. The **TargetActiveProc** procedure receives the **EndDialog** message and issues an END CONVERSATION that ends the target side of the conversation.

## Next Steps

You have successfully completed a request-reply message cycle between the **//AWDB/InternalAct/InitiatorService** and the **//AWDB/InternalAct/TargetService**. You can repeat the steps in this lesson as many times as you want to transmit a request-reply pair of messages. When you have finished investigating the SEND and REPLY statements, you can drop all the objects that were used by the conversation. For more information, see [Lesson 4: Dropping the Conversation Objects](lesson-4-dropping-the-conversation-objects.md).

## See also

- [BEGIN DIALOG CONVERSATION (Transact-SQL)](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)
- [SEND (Transact-SQL)](../../t-sql/statements/send-transact-sql.md)
- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
- [END CONVERSATION (Transact-SQL)](../../t-sql/statements/end-conversation-transact-sql.md)
- [Service Broker Applications](service-broker-applications.md)
