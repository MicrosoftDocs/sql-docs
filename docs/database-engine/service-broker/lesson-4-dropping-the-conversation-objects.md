---
title: "Lesson 4: Dropping the Conversation Objects"
description: "In this lesson, you will learn to drop the objects that enabled a database to support a conversation using an internal activation stored procedure."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Lesson 4: Dropping the Conversation Objects

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you will learn to drop the objects that enabled a database to support a conversation using an internal activation stored procedure.

## Procedures

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

### Switch to the AdventureWorks2008R2 database

- Copy and paste the following code into a Query Editor window. Then, run it to switch context to the **AdventureWorks2008R2** database.
    ```

        USE AdventureWorks2008R2;
        GO
    ```

### Drop the conversation objects

- Copy and paste the following code into a Query Editor window. Then, run it to drop the objects that were used to support the conversation.


    ```sql
        IF EXISTS (SELECT * FROM sys.objects
                   WHERE name =
                   N'TargetActivProc')
             DROP PROCEDURE TargetActivProc;

        IF EXISTS (SELECT * FROM sys.services
                   WHERE name =
                   N'//AWDB/InternalAct/TargetService')
             DROP SERVICE
             [//AWDB/InternalAct/TargetService];

        IF EXISTS (SELECT * FROM sys.service_queues
                   WHERE name = N'TargetQueueIntAct')
             DROP QUEUE TargetQueueIntAct;

        -- Drop the intitator queue and service if they already exist.
        IF EXISTS (SELECT * FROM sys.services
                   WHERE name =
                   N'//AWDB/InternalAct/InitiatorService')
             DROP SERVICE
             [//AWDB/InternalAct/InitiatorService];

        IF EXISTS (SELECT * FROM sys.service_queues
                   WHERE name = N'InitiatorQueueIntAct')
             DROP QUEUE InitiatorQueueIntAct;

        -- Drop contract and message type if they already exist.
        IF EXISTS (SELECT * FROM sys.service_contracts
                   WHERE name =
                   N'//AWDB/InternalAct/SampleContract')
             DROP CONTRACT
             [//AWDB/InternalAct/SampleContract];

        IF EXISTS (SELECT * FROM sys.service_message_types
                   WHERE name =
                   N'//AWDB/InternalAct/RequestMessage')
             DROP MESSAGE TYPE
             [//AWDB/InternalAct/RequestMessage];

        IF EXISTS (SELECT * FROM sys.service_message_types
                   WHERE name =
                   N'//AWDB/InternalAct/ReplyMessage')
             DROP MESSAGE TYPE
             [//AWDB/InternalAct/ReplyMessage];
    ```

## Next Steps

This concludes the tutorial. Tutorials are brief introductions only. They do not describe all available options. Tutorials use simplified logic and error handling, and should not be used in a production environment. To create efficient, reliable, and robust conversations, you need more complex code than the example in this tutorial.

## See also

- [DROP PROCEDURE (Transact-SQL)](../../t-sql/statements/drop-procedure-transact-sql.md)
- [DROP SERVICE (Transact-SQL)](../../t-sql/statements/drop-service-transact-sql.md)
- [DROP QUEUE (Transact-SQL)](../../t-sql/statements/drop-queue-transact-sql.md)
- [DROP MESSAGE TYPE (Transact-SQL)](../../t-sql/statements/drop-message-type-transact-sql.md)
- [DROP CONTRACT (Transact-SQL)](../../t-sql/statements/drop-contract-transact-sql.md)ms190475\(v=SQL.105\))
