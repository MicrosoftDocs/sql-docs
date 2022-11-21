---
title: "Lesson 2: Beginning a Conversation and Transmitting Messages"
description: "In this lesson, you will learn to start a conversation, complete a simple request-reply message cycle, and then end the conversation."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Lesson 2: Beginning a Conversation and Transmitting Messages

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you will learn to start a conversation, complete a simple request-reply message cycle, and then end the conversation.

## Procedures

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

### Switch to the AdventureWorks2008R2 database

- Copy and paste the following code into a Query Editor window. Then, run it to switch context to the AdventureWorks2008R2 database.

    ```sql
        USE AdventureWorks2008R2;
        GO
    ```

### Begin a conversation and send a request message

- Copy and paste the following code into a Query Editor window. Then, run it to start a conversation and send a request message to the //AWDB/1DBSample/TargetService. The code must be run in one block because a variable is used to pass a dialog handle from BEGIN DIALOG to the SEND statement. The batch runs the BEGIN DIALOG statement to start the conversation. It builds a request message, and then uses the dialog handle in a SEND statement to send the request message on that conversation. The last SELECT statement displays the text of the message that was sent.

    ```sql
        DECLARE @InitDlgHandle UNIQUEIDENTIFIER;
        DECLARE @RequestMsg NVARCHAR(100);

        BEGIN TRANSACTION;

        BEGIN DIALOG @InitDlgHandle
             FROM SERVICE
              [//AWDB/1DBSample/InitiatorService]
             TO SERVICE
              N'//AWDB/1DBSample/TargetService'
             ON CONTRACT
              [//AWDB/1DBSample/SampleContract]
             WITH
                 ENCRYPTION = OFF;

        SELECT @RequestMsg =
               N'<RequestMsg>Message for Target service.</RequestMsg>';

        SEND ON CONVERSATION @InitDlgHandle
             MESSAGE TYPE
             [//AWDB/1DBSample/RequestMessage]
             (@RequestMsg);

        SELECT @RequestMsg AS SentRequestMsg;

        COMMIT TRANSACTION;
        GO
    ```

### Receive the request and send a reply

- Copy and paste the following code into a Query Editor window. Then, run it to receive the reply message from the **TargetQueue1DB** and send a reply message back to the initiator. The RECEIVE statement retrieves the request message. The following SELECT statement displays the text so that you can verify that it is the same message sent in the last step. The IF statement tests whether the received message is a request message type, and if a SEND statement is used to send a reply message back to the initiator. The END CONVERSATION statement is used to end the target side of the conversation. The final SELECT statement displays the text of the reply message.

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
          FROM TargetQueue1DB
        ), TIMEOUT 1000;

        SELECT @RecvReqMsg AS ReceivedRequestMsg;

        IF @RecvReqMsgName =
           N'//AWDB/1DBSample/RequestMessage'
        BEGIN
             DECLARE @ReplyMsg NVARCHAR(100);
             SELECT @ReplyMsg =
             N'<ReplyMsg>Message for Initiator service.</ReplyMsg>';

             SEND ON CONVERSATION @RecvReqDlgHandle
                  MESSAGE TYPE
                  [//AWDB/1DBSample/ReplyMessage]
                  (@ReplyMsg);

             END CONVERSATION @RecvReqDlgHandle;
        END

        SELECT @ReplyMsg AS SentReplyMsg;

        COMMIT TRANSACTION;
        GO
    ```

### Receive the reply and end the conversation

- Copy and paste the following code into a Query Editor window. Then, run it to receive the reply message and end the conversation. The RECEIVE statement retrieves the reply message from the **InitiatorQueue1DB**. The END CONVERSATION statement ends the initiator side of the conversation. The last SELECT statement displays the text of the reply message so that you can confirm it is the same as what was sent in the previous step.

    ```sql
        DECLARE @RecvReplyMsg NVARCHAR(100);
        DECLARE @RecvReplyDlgHandle UNIQUEIDENTIFIER;

        BEGIN TRANSACTION;

        WAITFOR
        ( RECEIVE TOP(1)
            @RecvReplyDlgHandle = conversation_handle,
            @RecvReplyMsg = message_body
          FROM InitiatorQueue1DB
        ), TIMEOUT 1000;

        END CONVERSATION @RecvReplyDlgHandle;

        SELECT @RecvReplyMsg AS ReceivedReplyMsg;

        COMMIT TRANSACTION;
        GO
    ```

## Next Steps

You have successfully completed a request-reply message cycle between the **//AWDB/1DBSample/InitiatorService** and the **//AWDB/1DBSample/TargetService**. You can repeat the steps in this lesson as many times as you want to transmit a request-reply pair of messages. When you have finished investigating the SEND and REPLY statements, you can drop all the objects that were used by the conversation. For more information, see [Lesson 3: Dropping the Conversation Objects](lesson-3-dropping-the-conversation-objects.md).

## See also

- [BEGIN DIALOG CONVERSATION (Transact-SQL)](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)
- [SEND (Transact-SQL)](../../t-sql/statements/send-transact-sql.md)
- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
- [END CONVERSATION (Transact-SQL)](../../t-sql/statements/end-conversation-transact-sql.md)
- [Service Broker Applications](service-broker-applications.md)
