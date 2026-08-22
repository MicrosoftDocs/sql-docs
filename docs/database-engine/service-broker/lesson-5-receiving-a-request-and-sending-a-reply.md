---
title: "Lesson 5: Receive a Request and Sending a Reply"
description: "In this lesson, you learn how to receive a request message from the target queue and send a reply message to the initiator service."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: maghan
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Lesson 5: Receive a request and send a reply

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you learn how to receive a request message from the target queue and send a reply message to the initiator service. Run these steps from a copy of Management Studio that's running on the same computer as the target instance of the Database Engine.

## Procedures

### Switch to the TargetDB database

- Copy and paste the following code into a Query Editor window, then run it to switch context to the **InstTargetDB** database where you receive the request message and send a reply message back to the **InstInitiatorDB**.

  ```sql
  USE InstTargetDB;
  GO
  ```

### Receive the request and send a reply

- Copy and paste the following code into a Query Editor window. Then, run it to receive the reply message from the **InstTargetQueue** and send a reply message back to the initiator. The `RECEIVE` statement retrieves the request message, then the following `SELECT` statement displays the text so that you can verify that it's the same message that was sent in the previous step. The `IF` statement tests whether the received message is a request message type, and if a `SEND` statement is used to send a reply message back to the initiator. The `END CONVERSATION` statement is used to end the target side of the conversation. The final `SELECT` statement displays the text of the reply message.

  ```sql
  DECLARE @RecvReqDlgHandle AS UNIQUEIDENTIFIER;
  DECLARE @RecvReqMsg AS NVARCHAR (100);
  DECLARE @RecvReqMsgName AS sysname;

  BEGIN TRANSACTION;

  WAITFOR (RECEIVE TOP (1) @RecvReqDlgHandle = conversation_handle,
      @RecvReqMsg = message_body,
      @RecvReqMsgName = message_type_name FROM InstTargetQueue),
  TIMEOUT 1000;

  SELECT @RecvReqMsg AS ReceivedRequestMsg;

  IF @RecvReqMsgName = N'//BothDB/2InstSample/RequestMessage'
      BEGIN
          DECLARE @ReplyMsg AS NVARCHAR (100);
          SELECT @ReplyMsg = N'<ReplyMsg>Message for Initiator service.</ReplyMsg>';
          SEND ON CONVERSATION (@RecvReqDlgHandle)
              MESSAGE TYPE [//BothDB/2InstSample/ReplyMessage] (@ReplyMsg);
          END CONVERSATION @RecvReqDlgHandle;
      END

  SELECT @ReplyMsg AS SentReplyMsg;

  COMMIT TRANSACTION;
  GO
  ```

## Next step

You've successfully received the request message and sent a reply message to the initiator service. Next, you receive the reply message from the initiator queue and end the conversation.

> [!div class="nextstepaction"]
> [Lesson 6: Receive the reply and end the conversation](lesson-6-receiving-the-reply-and-ending-the-conversation.md)

## Related content

- [END CONVERSATION (Transact-SQL)](../../t-sql/statements/end-conversation-transact-sql.md)
- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
- [SEND (Transact-SQL)](../../t-sql/statements/send-transact-sql.md)
- [WAITFOR (Transact-SQL)](../../t-sql/language-elements/waitfor-transact-sql.md)
- [Service Broker applications](service-broker-applications.md)
