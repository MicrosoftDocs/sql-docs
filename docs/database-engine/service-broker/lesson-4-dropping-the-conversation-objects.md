---
title: "Lesson 4: Drop the Conversation Objects"
description: "In this lesson, you learn to drop the objects that enabled a database to support a conversation using an internal activation stored procedure."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Lesson 4: Drop the conversation objects

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you learn to drop the objects that enabled a database to support a conversation using an internal activation stored procedure.

## Procedures

[!INCLUDE [service-broker-adventureworks-2008-r2](../../includes/service-broker-adventureworks-2008-r2.md)]

### Switch to the AdventureWorks database

Copy and paste the following code into a Query Editor window, then run it to switch context to the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

```sql
USE AdventureWorks2022;
GO
```

### Drop the conversation objects

Copy and paste the following code into a Query Editor window, then run it to drop the objects that were used to support the conversation.

```sql
IF EXISTS (SELECT *
           FROM sys.objects
           WHERE name = N'TargetActiveProc')
    DROP PROCEDURE TargetActiveProc;

IF EXISTS (SELECT *
           FROM sys.services
           WHERE name = N'//AWDB/InternalAct/TargetService')
    DROP SERVICE [//AWDB/InternalAct/TargetService];

IF EXISTS (SELECT *
           FROM sys.service_queues
           WHERE name = N'TargetQueueIntAct')
    DROP QUEUE TargetQueueIntAct;

-- Drop the initiator queue and service if they already exist.
IF EXISTS (SELECT *
           FROM sys.services
           WHERE name = N'//AWDB/InternalAct/InitiatorService')
    DROP SERVICE [//AWDB/InternalAct/InitiatorService];

IF EXISTS (SELECT *
           FROM sys.service_queues
           WHERE name = N'InitiatorQueueIntAct')
    DROP QUEUE InitiatorQueueIntAct;

-- Drop contract and message type if they already exist.
IF EXISTS (SELECT *
           FROM sys.service_contracts
           WHERE name = N'//AWDB/InternalAct/SampleContract')
    DROP CONTRACT [//AWDB/InternalAct/SampleContract];

IF EXISTS (SELECT *
           FROM sys.service_message_types
           WHERE name = N'//AWDB/InternalAct/RequestMessage')
    DROP MESSAGE TYPE [//AWDB/InternalAct/RequestMessage];

IF EXISTS (SELECT *
           FROM sys.service_message_types
           WHERE name = N'//AWDB/InternalAct/ReplyMessage')
    DROP MESSAGE TYPE [//AWDB/InternalAct/ReplyMessage];
```

## Next step

This lesson concludes the tutorial. Tutorials are brief introductions only; they don't describe all available options. Tutorials use simplified logic and error handling, and you shouldn't use them in a production environment. To create efficient, reliable, and robust conversations, you need more complex code than the example in this tutorial.

> [!div class="nextstepaction"]
> [Implement internal activation in Service Broker](implementing-internal-activation.md)
