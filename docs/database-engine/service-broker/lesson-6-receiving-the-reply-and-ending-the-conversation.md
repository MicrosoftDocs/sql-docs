---
title: "Lesson 6: Receive the Reply and Ending the Conversation"
description: "In this lesson, you learn to receive the reply message from the target service and end the conversation."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Lesson 6: Receive the reply and end the conversation

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you learn to receive the reply message from the target service and end the conversation. Open SQL Server Management Studio (SSMS) and connect to the SQL Server which has the Service Broker initiator, then run these steps from the query window in SSMS.

## Procedures

### Switch to the InitiatorDB database

- Copy and paste the following code into a Query Editor window, then run it to switch context back to the **InstInitiatorDB** database where you receive the reply message and end the conversation.

  ```sql
  USE InstInitiatorDB;
  GO
  ```

### Receive the reply and end the conversation

- Copy and paste the following code into a Query Editor window, then run it to receive the reply message and end the conversation. The `RECEIVE` statement retrieves the reply message from the **InstInitiatorQueue**. The `END CONVERSATION` statement ends the initiator side of the conversation. The last `SELECT` statement displays the text of the reply message so you can confirm it's the same as what was sent in the last step.

  ```sql
  DECLARE @RecvReplyMsg AS NVARCHAR (100);
  DECLARE @RecvReplyDlgHandle AS UNIQUEIDENTIFIER;

  BEGIN TRANSACTION;

  WAITFOR (RECEIVE TOP (1) @RecvReplyDlgHandle = conversation_handle,
      @RecvReplyMsg = message_body FROM InstInitiatorQueue),
  TIMEOUT 1000;

  END CONVERSATION @RecvReplyDlgHandle;

  -- Display received request.
  SELECT @RecvReplyMsg AS ReceivedReplyMsg;

  COMMIT TRANSACTION;
  GO
  ```

## Related content

- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
- [END CONVERSATION (Transact-SQL)](../../t-sql/statements/end-conversation-transact-sql.md)
- [WAITFOR (Transact-SQL)](../../t-sql/language-elements/waitfor-transact-sql.md)
- [Service Broker applications](service-broker-applications.md)
