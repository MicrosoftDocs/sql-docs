---
title: "Lesson 2: Create the Target Conversation Objects"
description: "In this lesson, you learn to build all the objects that enable a database to be the target of a conversation from another database."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/03/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Lesson 2: Create the target conversation objects

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you learn to build all the objects that enable a database to be the target of a conversation from another database.

## Procedures

### Switch to the TargetDB database

- Copy and paste the following code into a Query Editor window, then run it to switch context to the `TargetDB` database.

  ```sql
  USE TargetDB;
  GO
  ```

### Create the message types

- Copy and paste the following code into a Query Editor window, then run it to create the message types for the conversation. The message type names and properties that you specify must be identical to the ones that you create in the `InitiatorDB` in the next lesson.

  ```sql
  CREATE MESSAGE TYPE [//BothDB/2DBSample/RequestMessage]
      VALIDATION = WELL_FORMED_XML;

  CREATE MESSAGE TYPE [//BothDB/2DBSample/ReplyMessage]
      VALIDATION = WELL_FORMED_XML;
  GO
  ```

### Create the contract

- Copy and paste the following code into a Query Editor window, then run it to create the contract for the conversation. The contract name and properties that you specify must be identical to the contract you create in the `InitiatorDB` in the next lesson.

  ```sql
  CREATE CONTRACT [//BothDB/2DBSample/SimpleContract]
      ([//BothDB/2DBSample/RequestMessage] SENT BY INITIATOR,
      [//BothDB/2DBSample/ReplyMessage] SENT BY TARGET);
  GO
  ```

### Create the target queue and service

- Copy and paste the following code into a Query Editor window, then run it to create the queue and service that's used for the target. The `CREATE SERVICE` statement associates the service with the `TargetQueue2DB` so that all messages that are sent to the service will be received into the `TargetQueue2DB`. The `CREATE SERVICE` also specifies that only conversations that use the **//BothDB/2DBSample/SimpleContract** that you created earlier can use the service as a target service.

  ```sql
  CREATE QUEUE TargetQueue2DB;

  CREATE SERVICE [//TgtDB/2DBSample/TargetService]
      ON QUEUE TargetQueue2DB
      ([//BothDB/2DBSample/SimpleContract]);
  GO
  ```

## Related tasks

- [CREATE MESSAGE TYPE (Transact-SQL)](../../t-sql/statements/create-message-type-transact-sql.md)
- [CREATE CONTRACT (Transact-SQL)](../../t-sql/statements/create-contract-transact-sql.md)
- [CREATE QUEUE (Transact-SQL)](../../t-sql/statements/create-queue-transact-sql.md)
- [CREATE SERVICE (Transact-SQL)](../../t-sql/statements/create-service-transact-sql.md)
- [Conversation architecture](conversation-architecture.md)
- [Service architecture](service-architecture.md)

## Next step

You've successfully configured `TargetDB` to support a conversation between it and the `InitiatorDB`. Next, you configure the `InitiatorDB` to initiate a conversation to the `TargetDB`.

> [!div class="nextstepaction"]
> [Lesson 3: Create the initiator conversation objects](lesson-3-creating-the-initiator-conversation-objects.md)
