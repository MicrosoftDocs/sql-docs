---
title: "Lesson 4: Begin a Conversation and Transmit Messages"
description: "In this lesson, you learn to start a conversation that spans two databases in the same instance of the Database Engine."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Lesson 4: Begin a conversation and transmit messages

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you learn to start a conversation that spans two databases in the same instance of the Database Engine. You also learn how to complete a simple request-reply message cycle, and then end the conversation.

## Procedures

### Switch to the InitiatorDB database

- Copy and paste the following code into a Query Editor window, then run it to switch context to the **InitiatorDB** database where you initiate the conversation.

  ```sql
  USE InitiatorDB;
  GO
  ```

### Start a conversation and send a request message

- Copy and paste the following code into a Query Editor window, then run it to start a conversation and send a request message to the **//TgtDB/2DBSample/TargetService** in the **TargetDB**. The code must be run in one block because a variable is used to pass a dialog handle from `BEGIN DIALOG` to the `SEND` statement. The batch runs the `BEGIN DIALOG` statement to begin the conversation and build a request message. Then, it uses the dialog handle in a `SEND` statement to send the request message on that conversation. The last `SELECT` statement displays the text of the message that was sent.

  ```sql
  DECLARE @InitDlgHandle AS UNIQUEIDENTIFIER;
  DECLARE @RequestMsg AS NVARCHAR (100);

  BEGIN TRANSACTION;

  BEGIN DIALOG @InitDlgHandle
      FROM SERVICE [//InitDB/2DBSample/InitiatorService]
      TO SERVICE N'//TgtDB/2DBSample/TargetService'
      ON CONTRACT [//BothDB/2DBSample/SimpleContract]
      WITH ENCRYPTION = OFF;

  SELECT @RequestMsg = N'<RequestMsg>Message for Target service.</RequestMsg>';

  SEND ON CONVERSATION (@InitDlgHandle)
      MESSAGE TYPE [//BothDB/2DBSample/RequestMessage] (@RequestMsg);

  SELECT @RequestMsg AS SentRequestMsg;

  COMMIT TRANSACTION;
  GO
  ```

### Switch to the TargetDB database

- Copy and paste the following code into a Query Editor window. Then, run it to switch context to the `TargetDB` database where you receive the request message and send a reply message back to the `InitiatorDB`.

  ```sql
  USE TargetDB;
  GO
  ```

### Receive the request and send a reply

- Copy and paste the following code into a Query Editor window, then run it to receive the reply message from the **TargetQueue2DB** and send a reply message back to the initiator. The `RECEIVE` statement retrieves the request message. Then, the following `SELECT` statement displays the text so that you can verify that it's the same message that was sent in the previous step. The `IF` statement tests whether the received message is a request message type, and if a `SEND` statement is used to send a reply message back to the initiator. It also tests whether the `END CONVERSATION` statement is used to end the target side of the conversation. The final `SELECT` statement displays the text of the reply message.

  ```sql
  DECLARE @RecvReqDlgHandle AS UNIQUEIDENTIFIER;
  DECLARE @RecvReqMsg AS NVARCHAR (100);
  DECLARE @RecvReqMsgName AS sysname;

  BEGIN TRANSACTION;

  WAITFOR (RECEIVE TOP (1) @RecvReqDlgHandle = conversation_handle,
      @RecvReqMsg = message_body,
      @RecvReqMsgName = message_type_name FROM TargetQueue2DB),
  TIMEOUT 1000;

  SELECT @RecvReqMsg AS ReceivedRequestMsg;

  IF @RecvReqMsgName = N'//BothDB/2DBSample/RequestMessage'
      BEGIN
          DECLARE @ReplyMsg AS NVARCHAR (100);
          SELECT @ReplyMsg = N'<ReplyMsg>Message for Initiator service.</ReplyMsg>';
          SEND ON CONVERSATION (@RecvReqDlgHandle)
              MESSAGE TYPE [//BothDB/2DBSample/ReplyMessage] (@ReplyMsg);
          END CONVERSATION @RecvReqDlgHandle;
      END

  SELECT @ReplyMsg AS SentReplyMsg;

  COMMIT TRANSACTION;
  GO
  ```

### Switch to the InitiatorDB database

- Copy and paste the following code into a Query Editor window. Then, run it to switch context back to the **InitiatorDB** database where you receive the reply message and end the conversation.

  ```sql
  USE InitiatorDB;
  GO
  ```

### Receive the reply and end the conversation

- Copy and paste the following code into a Query Editor window. Then, run it to receive the reply message and end the conversation. The `RECEIVE` statement retrieves the reply message from the **InitiatorQueue2DB**. The `END CONVERSATION` statement ends the initiator side of the conversation. The last `SELECT` statement displays the text of the reply message so that you can confirm it's the same as what was sent in the previous step.

  ```sql
  DECLARE @RecvReplyMsg AS NVARCHAR (100);
  DECLARE @RecvReplyDlgHandle AS UNIQUEIDENTIFIER;

  BEGIN TRANSACTION;

  WAITFOR (RECEIVE TOP (1) @RecvReplyDlgHandle = conversation_handle,
      @RecvReplyMsg = message_body FROM InitiatorQueue2DB),
  TIMEOUT 1000;

  END CONVERSATION @RecvReplyDlgHandle;

  -- Display received request.
  SELECT @RecvReplyMsg AS ReceivedReplyMsg;

  COMMIT TRANSACTION;
  GO
  ```

## Related content

- [BEGIN DIALOG CONVERSATION (Transact-SQL)](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)
- [END CONVERSATION (Transact-SQL)](../../t-sql/statements/end-conversation-transact-sql.md)
- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
- [SEND (Transact-SQL)](../../t-sql/statements/send-transact-sql.md)
- [WAITFOR (Transact-SQL)](../../t-sql/language-elements/waitfor-transact-sql.md)
- [Service Broker applications](service-broker-applications.md)
