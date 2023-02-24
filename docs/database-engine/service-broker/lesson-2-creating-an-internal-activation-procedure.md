---
title: "Lesson 2: Creating an Internal Activation Procedure"
description: "In this lesson, you will learn to create a stored procedure to process messages from a Service Broker queue."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Lesson 2: Creating an Internal Activation Procedure

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you will learn to create a stored procedure to process messages from a Service Broker queue. You will also learn how to specify that the procedure be activated any time there are messages in the queue.

## Procedures

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

### Switch to the AdventureWorks2008R2 database

- Copy and paste the following code into a Query Editor window. Then, run it to switch context to the AdventureWorks2008R2 database.

  ```sql
      USE AdventureWorks2008R2;
      GO
  ```

### Create an internal activation stored procedure

- Copy and paste the following code into a Query Editor window. Then, run it to create a stored procedure. When it is run, the stored procedure keeps receiving messages as long as there are messages in the queue. If the receive times out without returning a message, the stored procedure ends. If the received message was a request message, the stored procedure returns a reply message. If the received message is an **EndDialog** message, the stored procedure ends the target side of the conversation. If the received message is and **Error** message, it rolls back the transaction.

  ```sql
      CREATE PROCEDURE TargetActivProc
      AS
        DECLARE @RecvReqDlgHandle UNIQUEIDENTIFIER;
        DECLARE @RecvReqMsg NVARCHAR(100);
        DECLARE @RecvReqMsgName sysname;

        WHILE (1=1)
        BEGIN

          BEGIN TRANSACTION;

          WAITFOR
          ( RECEIVE TOP(1)
              @RecvReqDlgHandle = conversation_handle,
              @RecvReqMsg = message_body,
              @RecvReqMsgName = message_type_name
            FROM TargetQueueIntAct
          ), TIMEOUT 5000;

          IF (@@ROWCOUNT = 0)
          BEGIN
            ROLLBACK TRANSACTION;
            BREAK;
          END

          IF @RecvReqMsgName =
             N'//AWDB/InternalAct/RequestMessage'
          BEGIN
             DECLARE @ReplyMsg NVARCHAR(100);
             SELECT @ReplyMsg =
             N'<ReplyMsg>Message for Initiator service.</ReplyMsg>';

             SEND ON CONVERSATION @RecvReqDlgHandle
                    MESSAGE TYPE
                    [//AWDB/InternalAct/ReplyMessage]
                    (@ReplyMsg);
          END
          ELSE IF @RecvReqMsgName =
              N'https://schemas.microsoft.com/SQL/ServiceBroker/EndDialog'
          BEGIN
             END CONVERSATION @RecvReqDlgHandle;
          END
          ELSE IF @RecvReqMsgName =
              N'https://schemas.microsoft.com/SQL/ServiceBroker/Error'
          BEGIN
             END CONVERSATION @RecvReqDlgHandle;
          END

          COMMIT TRANSACTION;

        END
      GO
  ```

### Alter the target queue to specify internal activation

- Copy and paste the following code into a Query Editor window. Then, run it to specify that Service Broker activate the **TargetActiveProc** stored procedure to process messages from **TargetQueueIntAct**. Service Broker will run a copy of **TargetActiveProc** any time a message is received in **TargetQueueIntAct** and no copy of the procedure is already running. Service Broker will run additional copies of **TargetActiveProc** whenever the existing copies do not keep up with the number of incoming messages.

  ```sql
      ALTER QUEUE TargetQueueIntAct
          WITH ACTIVATION
          ( STATUS = ON,
            PROCEDURE_NAME = TargetActivProc,
            MAX_QUEUE_READERS = 10,
            EXECUTE AS SELF
          );
      GO
  ```

## Next Steps

You have successfully configured AdventureWorks2008R2 to support a conversation between the **//AWDB/InternalAct/InitiatorService** and the **//AWDB/InternalAct/TargetService**. Next, you will complete a conversation using the configuration. For more information, see [Lesson 3: Beginning a Conversation and Transmitting Messages](lesson-3-beginning-a-conversation-and-transmitting-messages.md).

## See also

- [CREATE PROCEDURE (Transact-SQL)](../../t-sql/statements/create-procedure-transact-sql.md)
- [SEND (Transact-SQL)](../../t-sql/statements/send-transact-sql.md)
- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
- [END CONVERSATION (Transact-SQL)](../../t-sql/statements/end-conversation-transact-sql.md)
- [Service Broker Applications](service-broker-applications.md)
