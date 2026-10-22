---
title: "Lesson 3: Begin a Conversation and Transmitting Messages"
description: "In this lesson, you learn to complete a basic request-reply message cycle in a system configured with an internal activation stored procedure."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Lesson 3: Begin a conversation and transmit messages

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you learn to complete a basic request-reply message cycle in a system configured with an internal activation stored procedure.

## Procedures

### Switch to the AdventureWorks database

[!INCLUDE [service-broker-adventureworks-2008-r2](../../includes/service-broker-adventureworks-2008-r2.md)]

Copy and paste the following code into a Query Editor window, then run it to switch context to the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
GO
```

### Begin a conversation and send a request message

Copy and paste the following code into a Query Editor window, then run it to start a conversation and send a request message to the **//AWDB/InternalAct/TargetService**. The code must be run in one block, because a variable is used to pass a dialog handle from `BEGIN DIALOG` to the `SEND` statement. The batch runs the `BEGIN DIALOG` statement to start the conversation. It builds a request message, then uses the dialog handle in a `SEND` statement to send the request message on that conversation. The last `SELECT` statement displays the text of the message that was sent.

```sql
DECLARE @InitDlgHandle AS UNIQUEIDENTIFIER;
DECLARE @RequestMsg AS NVARCHAR (100);

BEGIN TRANSACTION;

BEGIN DIALOG @InitDlgHandle
    FROM SERVICE [//AWDB/InternalAct/InitiatorService]
    TO SERVICE N'//AWDB/InternalAct/TargetService'
    ON CONTRACT [//AWDB/InternalAct/SampleContract]
    WITH ENCRYPTION = OFF;

-- Send a message on the conversation
SELECT @RequestMsg = N'<RequestMsg>Message for Target service.</RequestMsg>';

SEND ON CONVERSATION (@InitDlgHandle)
    MESSAGE TYPE [//AWDB/InternalAct/RequestMessage] (@RequestMsg);

-- Display sent request.
SELECT @RequestMsg AS SentRequestMsg;

COMMIT TRANSACTION;
GO
```

### Receive the request and send a reply

When you send the request message, Service Broker automatically activates a copy of **TargetActiveProc**. The stored procedure receives the reply message from the **TargetQueueIntAct** and sends a reply message back to the initiator.

### Receive the reply and end the conversation

Copy and paste the following code into a Query Editor window. Then, run it to receive the reply message and end the conversation. The `RECEIVE` statement retrieves the reply message from the **InitiatorQueueIntAct**. The `END CONVERSATION` statement ends the initiator side of the conversation and sends an **EndDialog** message to the target service. The last `SELECT` statement displays the text of the reply message so that you can confirm it's the same as what was sent in the previous step.

```sql
DECLARE @RecvReplyMsg AS NVARCHAR (100);
DECLARE @RecvReplyDlgHandle AS UNIQUEIDENTIFIER;

BEGIN TRANSACTION;

WAITFOR (RECEIVE TOP (1) @RecvReplyDlgHandle = conversation_handle,
    @RecvReplyMsg = message_body FROM InitiatorQueueIntAct),
TIMEOUT 5000;

END CONVERSATION @RecvReplyDlgHandle;

-- Display received request.
SELECT @RecvReplyMsg AS ReceivedReplyMsg;

COMMIT TRANSACTION;
GO
```

### End the target side of the conversation

When you run the `END CONVERSATION` statement for the initiator, Service Broker sends an **EndDialog** message to the **TargetQueueIntAct** queue. The **TargetActiveProc** procedure receives the **EndDialog** message and issues an `END CONVERSATION` that ends the target side of the conversation.

## Next step

You successfully completed a request-reply message cycle between the **//AWDB/InternalAct/InitiatorService** and the **//AWDB/InternalAct/TargetService**. You can repeat the steps in this lesson as many times as you want to transmit a request-reply pair of messages. When you finish investigating the SEND and REPLY statements, you can drop all the objects that were used by the conversation.

> [!div class="nextstepaction"]
> [Lesson 4: Drop the conversation objects](lesson-4-dropping-the-conversation-objects.md)
