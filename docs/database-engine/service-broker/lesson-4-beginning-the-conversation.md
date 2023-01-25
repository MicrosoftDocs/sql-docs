---
title: "Lesson 4: Beginning the Conversation"
description: "In this lesson, you will learn to start a conversation that spans two instances of the Database Engine and send a request message from the initiator instance to the target instance."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Lesson 4: Beginning the Conversation

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you will learn to start a conversation that spans two instances of the Database Engine and send a request message from the initiator instance to the target instance. Run these steps from a copy of Management Studio running on the same computer as the initiator instance.

## Procedures


### Switch to the InitiatorDB database

- Copy and paste the following code into a Query Editor window. Then, run it to switch context to the **InstInitiatorDB** database where you will initiate the conversation.

    ```sql
        USE InstInitiatorDB;
        GO
    ```

### Start a conversation and send a request message

- Copy and paste the following code into a Query Editor window. Then, run it to begin a conversation and send a request message to the **//TgtDB/2InstSample/TargetService** in the **InstTargetDB**. The code must be run in one block because a variable is used to pass a dialog handle from BEGIN DIALOG to the SEND statement. The batch runs the BEGIN DIALOG statement to begin the conversation, and then builds a request message. Then, it uses the dialog handle in a SEND statement to send the request message on that conversation. The last SELECT statement just displays the text of the message that was sent.

    ```sql
        DECLARE @InitDlgHandle UNIQUEIDENTIFIER;
        DECLARE @RequestMsg NVARCHAR(100);

        BEGIN TRANSACTION;

        BEGIN DIALOG @InitDlgHandle
             FROM SERVICE [//InstDB/2InstSample/InitiatorService]
             TO SERVICE N'//TgtDB/2InstSample/TargetService'
             ON CONTRACT [//BothDB/2InstSample/SimpleContract]
             WITH
                 ENCRYPTION = ON;

        SELECT @RequestMsg = N'<RequestMsg>Message for Target service.</RequestMsg>';

        SEND ON CONVERSATION @InitDlgHandle
             MESSAGE TYPE [//BothDB/2InstSample/RequestMessage]
             (@RequestMsg);

        SELECT @RequestMsg AS SentRequestMsg;

        COMMIT TRANSACTION;
        GO
    ```

## Next Steps

You have successfully started a conversation and sent the request message to the target service. Next, you will receive the request message from the target queue and send a reply message to the initiator service. For more information, see [Lesson 5: Receiving a Request and Sending a Reply](lesson-5-receiving-a-request-and-sending-a-reply.md).

## See also

- [BEGIN DIALOG CONVERSATION (Transact-SQL)](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)
- [SEND (Transact-SQL)](../../t-sql/statements/send-transact-sql.md)
- [Service Broker Applications](service-broker-applications.md)
