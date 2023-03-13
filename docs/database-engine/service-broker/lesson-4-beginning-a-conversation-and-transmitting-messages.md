---
title: "Lesson 4: Beginning a Conversation and Transmitting Messages"
description: "In this lesson, you will learn to start a conversation that spans two databases in the same instance of the Database Engine."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Lesson 4: Beginning a Conversation and Transmitting Messages

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you will learn to start a conversation that spans two databases in the same instance of the Database Engine. You will also learn how to complete a simple request-reply message cycle, and then end the conversation.

## Procedures

### Switch to the InitiatorDB database

- Copy and paste the following code into a Query Editor window. Then, run it to switch context to the **InitiatorDB** database where you initiate the conversation.

    ```sql
        USE InitiatorDB;
        GO
    ```

### Start a conversation and send a request message

- Copy and paste the following code into a Query Editor window. Then, run it to start a conversation and send a request message to the **//TgtDB/2DBSample/TargetService** in the **TargetDB**. The code must be run in one block because a variable is used to pass a dialog handle from BEGIN DIALOG to the SEND statement. The batch runs the BEGIN DIALOG statement to begin the conversation and build a request message. Then, it uses the dialog handle in a SEND statement to send the request message on that conversation. The last SELECT statement displays the text of the message that was sent.

    ```sql
        DECLARE @InitDlgHandle UNIQUEIDENTIFIER;
        DECLARE @RequestMsg NVARCHAR(100);

        BEGIN TRANSACTION;

        BEGIN DIALOG @InitDlgHandle
             FROM SERVICE [//InitDB/2DBSample/InitiatorService]
             TO SERVICE N'//TgtDB/2DBSample/TargetService'
             ON CONTRACT [//BothDB/2DBSample/SimpleContract]
             WITH
                 ENCRYPTION = OFF;

        SELECT @RequestMsg =
           N'<RequestMsg>Message for Target service.</RequestMsg>';

        SEND ON CONVERSATION @InitDlgHandle
             MESSAGE TYPE [//BothDB/2DBSample/RequestMessage]
              (@RequestMsg);

        SELECT @RequestMsg AS SentRequestMsg;

        COMMIT TRANSACTION;
        GO
    ```

### Switch to the TargetDB database

- Copy and paste the following code into a Query Editor window. Then, run it to switch context to the **TargetDB** database where you will receive the request message and send a reply message back to the **InitiatorDB**.

    ```sql
        USE TargetDB;
        GO
    ```

### Receive the request and send a reply

- Copy and paste the following code into a Query Editor window. Then, run it to receive the reply message from the **TargetQueue2DB** and send a reply message back to the initiator. The RECEIVE statement retrieves the request message. Then, the following SELECT statement displays the text so that you can verify that it is the same message that was sent in the previous step. The IF statement tests whether the received message is a request message type, and if a SEND statement is used to send a reply message back to the initiator. It also tests whether the END CONVERSATION statement is used to end the target side of the conversation. The final SELECT statement displays the text of the reply message.

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
          FROM TargetQueue2DB
        ), TIMEOUT 1000;

        SELECT @RecvReqMsg AS ReceivedRequestMsg;

        IF @RecvReqMsgName =
           N'//BothDB/2DBSample/RequestMessage'
        BEGIN
             DECLARE @ReplyMsg NVARCHAR(100);
             SELECT @ReplyMsg =
                N'<ReplyMsg>Message for Initiator service.</ReplyMsg>';

             SEND ON CONVERSATION @RecvReqDlgHandle
                  MESSAGE TYPE
                    [//BothDB/2DBSample/ReplyMessage] (@ReplyMsg);

             END CONVERSATION @RecvReqDlgHandle;
        END

        SELECT @ReplyMsg AS SentReplyMsg;

        COMMIT TRANSACTION;
        GO
    ```

### Switch to the InitiatorDB database

- Copy and paste the following code into a Query Editor window. Then, run it to switch context back to the **InitiatorDB** database where you will receive the reply message and end the conversation.

    ```sql  
        USE InitiatorDB;
        GO
    ```

### Receive the reply and end the conversation

- Copy and paste the following code into a Query Editor window. Then, run it to receive the reply message and end the conversation. The RECEIVE statement retrieves the reply message from the **InitiatorQueue2DB**. The END CONVERSATION statement ends the initiator side of the conversation. The last SELECT statement displays the text of the reply message so that you can confirm it is the same as what was sent in the previous step.

    ```sql  
        DECLARE @RecvReplyMsg NVARCHAR(100);
        DECLARE @RecvReplyDlgHandle UNIQUEIDENTIFIER;

        BEGIN TRANSACTION;

        WAITFOR
        ( RECEIVE TOP(1)
            @RecvReplyDlgHandle = conversation_handle,
            @RecvReplyMsg = message_body
          FROM InitiatorQueue2DB
        ), TIMEOUT 1000;

        END CONVERSATION @RecvReplyDlgHandle;

        -- Display recieved request.
        SELECT @RecvReplyMsg AS ReceivedReplyMsg;

        COMMIT TRANSACTION;
        GO
    ```

## Next Steps

This concludes the tutorial. Tutorials are brief introductions only. They do not describe all available options. Tutorials use simplified logic and error handling, and should not be used in a production environment. To create efficient, reliable, and robust conversations, you need more complex code than the example in this tutorial.

## See also

- [BEGIN DIALOG CONVERSATION (Transact-SQL)](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)
- [END CONVERSATION (Transact-SQL)](../../t-sql/statements/end-conversation-transact-sql.md)
- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
- [SEND (Transact-SQL)](../../t-sql/statements/send-transact-sql.md)
- [WAITFOR (Transact-SQL)](../../t-sql/language-elements/waitfor-transact-sql.md)
- [Service Broker Applications](service-broker-applications.md)
