---
title: "Lesson 3: Drop the Conversation Objects"
description: "In this lesson, you learn to drop the objects that enabled a database to support a conversation in the database."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/08/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Lesson 3: Drop the conversation objects

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you learn to drop the objects that enabled a database to support a conversation in the database.

## Procedures

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

### Switch to the AdventureWorks2008R2 database

- Copy and paste the following code into a Query Editor window, then run it to switch context to the AdventureWorks2008R2 database.

  ```sql
  USE AdventureWorks2008R2;
  GO
  ```

### Drop the conversation objects

- Copy and paste the following code into a Query Editor window, then run it to drop the objects that were used to support the conversation.

  ```sql
  IF EXISTS (SELECT *
             FROM sys.services
             WHERE name = N'//AWDB/1DBSample/TargetService')
      DROP SERVICE [//AWDB/1DBSample/TargetService];

  IF EXISTS (SELECT *
             FROM sys.service_queues
             WHERE name = N'TargetQueue1DB')
      DROP QUEUE TargetQueue1DB;
  -- Drop the initiator queue and service if they already exist.

  IF EXISTS (SELECT *
             FROM sys.services
             WHERE name = N'//AWDB/1DBSample/InitiatorService')
      DROP SERVICE [//AWDB/1DBSample/InitiatorService];

  IF EXISTS (SELECT *
             FROM sys.service_queues
             WHERE name = N'InitiatorQueue1DB')
      DROP QUEUE InitiatorQueue1DB;

  IF EXISTS (SELECT *
             FROM sys.service_contracts
             WHERE name = N'//AWDB/1DBSample/SampleContract')
      DROP CONTRACT [//AWDB/1DBSample/SampleContract];

  IF EXISTS (SELECT *
             FROM sys.service_message_types
             WHERE name = N'//AWDB/1DBSample/RequestMessage')
      DROP MESSAGE TYPE [//AWDB/1DBSample/RequestMessage];

  IF EXISTS (SELECT *
             FROM sys.service_message_types
             WHERE name = N'//AWDB/1DBSample/ReplyMessage')
      DROP MESSAGE TYPE [//AWDB/1DBSample/ReplyMessage];
  GO
  ```

## Related content

- [DROP SERVICE (Transact-SQL)](../../t-sql/statements/drop-service-transact-sql.md)
- [DROP QUEUE (Transact-SQL)](../../t-sql/statements/drop-queue-transact-sql.md)
- [DROP MESSAGE TYPE (Transact-SQL)](../../t-sql/statements/drop-message-type-transact-sql.md)
- [DROP CONTRACT (Transact-SQL)](../../t-sql/statements/drop-contract-transact-sql.md)
