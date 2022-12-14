---
title: "Lesson 6: Receiving the Reply and Ending the Conversation"
description: "In this lesson, you will learn to receive the reply message from the target service and end the conversation."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Lesson 6: Receiving the Reply and Ending the Conversation

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you will learn to receive the reply message from the target service and end the conversation. Open SQL Server Management Studio (SSMS) and connect to the SQL Server which has the Service Broker initiator. Then run these steps from the query window in SSMS.

## Procedures

### Switch to the InitiatorDB database

- Copy and paste the following code into a Query Editor window. Then, run it to switch context back to the **InstInitiatorDB** database where you will receive the reply message and end the conversation.

    ```sql
        USE InstInitiatorDB;
        GO
    ```

### Receive the reply and end the conversation

- Copy and paste the following code into a Query Editor window. Then, run it to receive the reply message and end the conversation. The RECEIVE statement retrieves the reply message from the **InstInitiatorQueue**. The END CONVERSATION statement ends the initiator side of the conversation. The last SELECT statement displays the text of the reply message so that you can confirm it is the same as what was sent in the last step.

    ```sql
        DECLARE @RecvReplyMsg NVARCHAR(100);
        DECLARE @RecvReplyDlgHandle UNIQUEIDENTIFIER;

        BEGIN TRANSACTION;

        WAITFOR
        ( RECEIVE TOP(1)
            @RecvReplyDlgHandle = conversation_handle,
            @RecvReplyMsg = message_body
          FROM InstInitiatorQueue
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

- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
- [END CONVERSATION (Transact-SQL)](../../t-sql/statements/end-conversation-transact-sql.md)
- [WAITFOR (Transact-SQL)](../../t-sql/language-elements/waitfor-transact-sql.md)
- [Service Broker Applications](service-broker-applications.md)
