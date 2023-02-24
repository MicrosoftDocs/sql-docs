---
title: "Lesson 2: Creating the Target Conversation Objects"
description: "In this lesson, you will learn to build all the objects that enable a database to be the target of a conversation from another database."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Lesson 2: Creating the Target Conversation Objects

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you will learn to build all the objects that enable a database to be the target of a conversation from another database.

## Procedures

### Switch to the TargetDB database

- Copy and paste the following code into a Query Editor window. Then, run it to switch context to the **TargetDB** database.

  ```sql
        USE TargetDB;
        GO
  ```

### Create the message types

- Copy and paste the following code into a Query Editor window. Then, run it to create the message types for the conversation. The message type names and properties that you specify must be identical to the ones that you will create in the **InitiatorDB** in the next lesson.

    ```sql
        CREATE MESSAGE TYPE [//BothDB/2DBSample/RequestMessage]
               VALIDATION = WELL_FORMED_XML;
        CREATE MESSAGE TYPE [//BothDB/2DBSample/ReplyMessage]
               VALIDATION = WELL_FORMED_XML;
        GO
    ```

### Create the contract

- Copy and paste the following code into a Query Editor window. Then, run it to create the contract for the conversation. The contract name and properties that you specify must be identical to the contract you will create in the **InitiatorDB** in the next lesson.

    ```sql
        CREATE CONTRACT [//BothDB/2DBSample/SimpleContract]
              ([//BothDB/2DBSample/RequestMessage]
                 SENT BY INITIATOR,
               [//BothDB/2DBSample/ReplyMessage]
                 SENT BY TARGET
              );
        GO
    ```

### Create the target queue and service

- Copy and paste the following code into a Query Editor window. Then, run it to create the queue and service that is used for the target. The CREATE SERVICE statement associates the service with the **TargetQueue2DB** so that all messages that are sent to the service will be received into the **TargetQueue2DB**. The CREATE SERVICE also specifies that only conversations that use the **//BothDB/2DBSample/SimpleContract** that you created earlier can use the service as a target service.

    ```sql
        CREATE QUEUE TargetQueue2DB;

        CREATE SERVICE [//TgtDB/2DBSample/TargetService]
               ON QUEUE TargetQueue2DB
               ([//BothDB/2DBSample/SimpleContract]);
        GO
    ```

## Next Steps

You have successfully configured **TargetDB** to support a conversation between it and the **InitiatorDB**. Next, you will configure the **InitiatorDB** to initiate a conversation to the **TargetDB**. For more information, see [Lesson 3: Creating the Initiator Conversation Objects](lesson-3-creating-the-initiator-conversation-objects.md).

## See also

- [CREATE MESSAGE TYPE (Transact-SQL)](../../t-sql/statements/create-message-type-transact-sql.md)
- [CREATE CONTRACT (Transact-SQL)](../../t-sql/statements/create-contract-transact-sql.md)[CREATE QUEUE (Transact-SQL)](../../t-sql/statements/create-queue-transact-sql.md)
- [CREATE SERVICE (Transact-SQL)](../../t-sql/statements/create-service-transact-sql.md)
- [Conversation Architecture](conversation-architecture.md)
-[Service Architecture](service-architecture.md)
