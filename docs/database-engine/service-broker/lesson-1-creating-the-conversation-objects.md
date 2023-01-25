---
title: "Lesson 1: Creating the Conversation Objects"
description: "In this lesson, you will learn to build all the objects that enable a database to support a conversation in the database."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Lesson 1: Creating the Conversation Objects

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you will learn to build all the objects that enable a database to support a conversation in the database.

## Procedures

### Enable Service Broker and switch to the AdventureWorks2008R2 database

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

- Copy and paste the following code into a Query Editor window. Then, run it to ensure that Service Broker is enabled in the AdventureWorks2008R2 database, and switch context to the database.

    ```sql  
        USE master;
        GO
        ALTER DATABASE AdventureWorks2008R2
              SET ENABLE_BROKER;
        GO
        USE AdventureWorks2008R2;
        GO
    ```

### Create the message types

- Copy and paste the following code into a Query Editor window. Then, run it to create the message types for the conversation. Because Service Broker objects are often referenced across multiple instances of the Database Engine, most Service Broker objects are given names in a URI format. This help ensure that they are unique across multiple computers. Both of these message types specify that Service Broker will only validate that the messages are well-formed XML documents, and that it will not validate the XML against a specific schema.

    ```sql
        CREATE MESSAGE TYPE
               [//AWDB/1DBSample/RequestMessage]
               VALIDATION = WELL_FORMED_XML;
        CREATE MESSAGE TYPE
               [//AWDB/1DBSample/ReplyMessage]
               VALIDATION = WELL_FORMED_XML;
        GO
    ```

### Create the contract

- Copy and paste the following code into a Query Editor window. Then, run it to create the contract for the conversation. The contract specifies that conversations that use it must send messages of the **//AWDB/1DBSample/RequestMessage** type from the initiator to the target, and messages of the **//AWDB/1DBSample/ReplyMessage** type from the target to the initiator.

    ```sql  
        CREATE CONTRACT [//AWDB/1DBSample/SampleContract]
              ([//AWDB/1DBSample/RequestMessage]
               SENT BY INITIATOR,
               [//AWDB/1DBSample/ReplyMessage]
               SENT BY TARGET
              );
        GO
    ```

### Create the target queue and service

- Copy and paste the following code into a Query Editor window. Then, run it to create the queue and service that is used for the target. Because queues are referenced from the same database in a manner similar to tables and views, queue names are formatted like table or view names. The CREATE SERVICE statement associates the service with the **TargetQueue1DB**. Therefore, all messages that are sent to the service will be received into the **TargetQueue1DB**. The CREATE SERVICE also specifies that only conversations that use the **//AWDB/1DBSample/SampleContract** created earlier can use the service as a target service.

    ```sql  
        CREATE QUEUE TargetQueue1DB;

        CREATE SERVICE
               [//AWDB/1DBSample/TargetService]
               ON QUEUE TargetQueue1DB
               ([//AWDB/1DBSample/SampleContract]);
        GO
    ```

### Create the initiator queue and service

- Copy and paste the following code into a Query Editor window. Then, run it to create the queue and service that is used for the initiator. Because no contract name is specified, no other services can use this service as a target service.

    ```sql
        CREATE QUEUE InitiatorQueue1DB;

        CREATE SERVICE
               [//AWDB/1DBSample/InitiatorService]
               ON QUEUE InitiatorQueue1DB;
        GO
    ```

## Next Steps

You have successfully configured **AdventureWorks2008R2** to support a conversation between the **//AWDB/1DBSample/InitiatorService** and the **//AWDB/1DBSample/TargetService**. Next, you will complete a conversation using the configuration. For more information, see [Lesson 2: Beginning a Conversation and Transmitting Messages](lesson-2-beginning-a-conversation-and-transmitting-messages.md).

## See also

- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [CREATE MESSAGE TYPE (Transact-SQL)](../../t-sql/statements/create-message-type-transact-sql.md)
- [CREATE CONTRACT (Transact-SQL)](../../t-sql/statements/create-contract-transact-sql.md)[CREATE QUEUE (Transact-SQL)](../../t-sql/statements/create-queue-transact-sql.md)
- [CREATE SERVICE (Transact-SQL)](../../t-sql/statements/create-service-transact-sql.md)
- [Conversation Architecture](conversation-architecture.md)
- [Service Architecture](service-architecture.md)
