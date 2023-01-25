---
title: "Lesson 3: Creating the Initiator Conversation Objects"
description: "In this lesson, you will learn to build all the objects that enable a database to initiate a conversation with another database."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Lesson 3: Creating the Initiator Conversation Objects

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you will learn to build all the objects that enable a database to initiate a conversation with another database.

## Procedures

### Switch to the InitiatorDB database

- Copy and paste the following code into a Query Editor window. Then, run it to switch context to the **InitiatorDB** database.

    ```sql
        USE InitiatorDB;
        GO
    ```

### Create the message types

- Copy and paste the following code into a Query Editor window. Then, run it to create the message types for the conversation. The message type names and properties that are specified here must be identical to the ones that were created in the **TargetDB** in the previous lesson.

    ```sql
        CREATE MESSAGE TYPE [//BothDB/2DBSample/RequestMessage]
               VALIDATION = WELL_FORMED_XML;
        CREATE MESSAGE TYPE [//BothDB/2DBSample/ReplyMessage]
               VALIDATION = WELL_FORMED_XML;
        GO
    ```

### Create the contract

- Copy and paste the following code into a Query Editor window. Then, run it to create the contract for the conversation. The contract name and properties that are specified here must be identical to the contract that was created in the **TargetDB** in the previous lesson.

    ```sql
        CREATE CONTRACT [//BothDB/2DBSample/SimpleContract]
              ([//BothDB/2DBSample/RequestMessage]
                 SENT BY INITIATOR,
               [//BothDB/2DBSample/ReplyMessage]
                 SENT BY TARGET
              );
        GO
    ```

### Create the initiator queue and service

- Copy and paste the following code into a Query Editor window. Then, run it to create the queue and service that is used for the initiator. Because no contract name is specified, no other services can use this service as a target service.

    ```sql
        CREATE QUEUE InitiatorQueue2DB;

        CREATE SERVICE [//InitDB/2DBSample/InitiatorService]
               ON QUEUE InitiatorQueue2DB;
        GO
    ```

## Next Steps

You have successfully configured the **InitiatorDB** and **TargetDB** to support a conversation between the two databases. Next, you will complete a conversation that uses the configuration. For more information, see [Lesson 4: Beginning a Conversation and Transmitting Messages](lesson-4-beginning-a-conversation-and-transmitting-messages.md).

## See also

- [CREATE MESSAGE TYPE (Transact-SQL)](../../t-sql/statements/create-message-type-transact-sql.md)
- [CREATE CONTRACT (Transact-SQL)](../../t-sql/statements/create-contract-transact-sql.md)[CREATE QUEUE (Transact-SQL)](../../t-sql/statements/create-queue-transact-sql.md)
- [CREATE SERVICE (Transact-SQL)](../../t-sql/statements/create-service-transact-sql.md)
- [Conversation Architecture](conversation-architecture.md)
- [Service Architecture](service-architecture.md)
