---
title: "Lesson 1: Create the Base Conversation Objects"
description: "In this lesson, you learn to build all the objects that enable a database to support a conversation in the database"
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/03/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Lesson 1: Create the base conversation objects

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, learn how to build all the objects that enable a database to support a conversation in the database.

## Procedures

### Enable Service Broker and switch to the AdventureWorks database

[!INCLUDE [service-broker-adventureworks-2008-r2](../../includes/service-broker-adventureworks-2008-r2.md)]

Copy and paste the following code into a Query Editor window. Then, run it to ensure that Service Broker is enabled in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database, and switch context to the database.

```sql
USE master;
GO

ALTER DATABASE AdventureWorks2022
    SET ENABLE_BROKER;
GO

USE AdventureWorks2022;
GO
```

### Create the message types

Copy and paste the following code into a Query Editor window. Then, run it to create the message types for the conversation. Because Service Broker objects are often referenced across multiple instances of the Database Engine, most Service Broker objects are given names in a URI format. This helps to ensure that they're unique across multiple computers. Both of these message types specify that Service Broker only validates that the messages are well-formed XML documents, and that the XML isn't validated against a specific schema.

```sql
CREATE MESSAGE TYPE [//AWDB/InternalAct/RequestMessage]
    VALIDATION = WELL_FORMED_XML;

CREATE MESSAGE TYPE [//AWDB/InternalAct/ReplyMessage]
    VALIDATION = WELL_FORMED_XML;
GO
```

### Create the contract

Copy and paste the following code into a Query Editor window. Then, run it to create the contract for the conversation. The contract specifies that conversations that use it must send messages of the **//AWDB/InternalAct/RequestMessage** type from the initiator to the target, and messages of the **//AWDB/InternalAct/ReplyMessage** type from the target to the initiator.

```sql
CREATE CONTRACT [//AWDB/InternalAct/SampleContract]
    ([//AWDB/InternalAct/RequestMessage] SENT BY INITIATOR,
    [//AWDB/InternalAct/ReplyMessage] SENT BY TARGET);
GO
```

### Create the target queue and service

Copy and paste the following code into a Query Editor window, then run it to create the queue and service that's used for the target. Because queues are referenced from the same database in a manner similar to tables and views, queue names are formatted like table or view names. The `CREATE SERVICE` statement associates the service with the **TargetQueueIntAct**. Therefore, all messages that are sent to the service are received into the **TargetQueueIntAct**. The `CREATE SERVICE` also specifies that only conversations that use the **//AWDB/InternalAct/SampleContract** created earlier can use the service as a target service.

```sql
CREATE QUEUE TargetQueueIntAct;

CREATE SERVICE [//AWDB/InternalAct/TargetService]
    ON QUEUE TargetQueueIntAct
    ([//AWDB/InternalAct/SampleContract]);
GO
```

### Create the initiator queue and service

Copy and paste the following code into a Query Editor window, then run it to create the queue and service that's used for the initiator. Because no contract name is specified, no other services can use this service as a target service.

```sql
CREATE QUEUE InitiatorQueueIntAct;

CREATE SERVICE [//AWDB/InternalAct/InitiatorService]
    ON QUEUE InitiatorQueueIntAct;
GO
```

## Next step

You successfully configured [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] to support a conversation between the `//AWDB/InternalAct/InitiatorService` and the `//AWDB/InternalAct/TargetService`.

> [!div class="nextstepaction"]
> [Lesson 2: Create an internal activation procedure](lesson-2-creating-an-internal-activation-procedure.md)
