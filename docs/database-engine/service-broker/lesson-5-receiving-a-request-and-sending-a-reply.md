---
title: "Lesson 5: Receiving a Request and Sending a Reply"
description: "In this lesson, you will learn how to receive a request message from the target queue and send a reply message to the initiator service."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Lesson 5: Receiving a Request and Sending a Reply

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you will learn how to receive a request message from the target queue and send a reply message to the initiator service. Run these steps from a copy of Management Studio that is running on the same computer as the target instance of the Database Engine.

## Procedures


### Switch to the TargetDB database

- Copy and paste the following code into a Query Editor window. Then, run it to switch context to the **InstTargetDB** database where you will receive the request message and send a reply message back to the **InstInitiatorDB**.

    ```sql
        USE InstTargetDB;
        GO
    ```

### Receive the request and send a reply

- Copy and paste the following code into a Query Editor window. Then, run it to receive the reply message from the **InstTargetQueue** and send a reply message back to the initiator. The RECEIVE statement retrieves the request message. Then, the following SELECT statement displays the text so that you can verify that it is the same message that was sent in the previous step. The IF statement tests whether the received message is a request message type, and if a SEND statement is used to send a reply message back to the initiator. The END CONVERSATION statement is used to end the target side of the conversation. The final SELECT statement displays the text of the reply message.

    ```sql
        DECLARE @RecvReqDlgHandle UNIQUEIDENTIFIER;
        DECLARE @RecvReqMsg NVARCHAR(100);
        DECLARE @RecvReqMsgName sysname;

        BEGIN TRANSACTION;

        WAITFOR
        ( RECEIVE TOP(1)
            @RecvReqDlgHandle = conversation_handle,
            @RecvReqMsg = message_body,
            @RecvReqMsgName = message_type_name
          FROM InstTargetQueue
        ), TIMEOUT 1000;

        SELECT @RecvReqMsg AS ReceivedRequestMsg;

        IF @RecvReqMsgName = N'//BothDB/2InstSample/RequestMessage'
        BEGIN
             DECLARE @ReplyMsg NVARCHAR(100);
             SELECT @ReplyMsg =
                N'<ReplyMsg>Message for Initiator service.</ReplyMsg>';

             SEND ON CONVERSATION @RecvReqDlgHandle
                  MESSAGE TYPE [//BothDB/2InstSample/ReplyMessage]
                  (@ReplyMsg);

             END CONVERSATION @RecvReqDlgHandle;
        END

        SELECT @ReplyMsg AS SentReplyMsg;

        COMMIT TRANSACTION;
        GO
    ```

## Next Steps

You have successfully received the request message and sent a reply message to the initiator service. Next, you will receive the reply message from the initiator queue and end the conversation. For more information, see [Lesson 6: Receiving the Reply and Ending the Conversation](lesson-6-receiving-the-reply-and-ending-the-conversation.md).

## See also

- [END CONVERSATION (Transact-SQL)](../../t-sql/statements/end-conversation-transact-sql.md)
- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
- [SEND (Transact-SQL)](../../t-sql/statements/send-transact-sql.md)
- [WAITFOR (Transact-SQL)](../../t-sql/language-elements/waitfor-transact-sql.md)
- [Service Broker Applications](service-broker-applications.md)
