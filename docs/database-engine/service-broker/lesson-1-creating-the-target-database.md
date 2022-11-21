---
title: "Lesson 1: Creating the Target Database"
description: "In this lesson, you will learn to create the target database and all the Service Broker target objects that do not have dependencies on the initiator database."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Lesson 1: Creating the Target Database

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you will learn to create the target database and all the Service Broker target objects that do not have dependencies on the initiator database. Run these steps from a copy of Management Studio that is running on the same computer as the target instance the Database Engine.

## Procedures

### Create a Service Broker endpoint

- Copy and paste the following code into a Query Editor window. Then, run it to create a Service Broker endpoint for this instance of the Database Engine. A Service Broker endpoint establishes the network address to which Service Broker messages are sent. This endpoint uses the Service Broker default of TCP port 4022, and establishes that the remote instances of the Database Engine will use Windows Authentication connections to send messages.

    Windows Authentication works when both computers are in the same domain or trusted domains. If the computers are not in trusted domains, use certificate security for the endpoints. For more information, see [How to: Create Certificates for Service Broker Transport Security (Transact-SQL)](how-to-create-certificates-for-service-broker-transport-security-transact-sql.md).

    ```sql
        USE master;
        GO
        IF EXISTS (SELECT * FROM master.sys.endpoints
                   WHERE name = N'InstTargetEndpoint')
             DROP ENDPOINT InstTargetEndpoint;
        GO
        CREATE ENDPOINT InstTargetEndpoint
        STATE = STARTED
        AS TCP ( LISTENER_PORT = 4022 )
        FOR SERVICE_BROKER (AUTHENTICATION = WINDOWS );
        GO
    ```

### Create the target database, master key, and user

- Copy and paste the following code into a Query Editor window. Change the password on the CREATE MASTER KEY statement. Then, run the code to create the target database used for this tutorial. By default, new databases have the ENABLE_BROKER option set to on. The code also creates the master key and user that will be used to support encryption and remote connections.

    ```sql
        USE master;
        GO
        IF EXISTS (SELECT * FROM sys.databases
                   WHERE name = N'InstTargetDB')
             DROP DATABASE InstTargetDB;
        GO
        CREATE DATABASE InstTargetDB;
        GO
        USE InstTargetDB;
        GO
        CREATE MASTER KEY
               ENCRYPTION BY PASSWORD = N'<EnterStrongPassword1Here>';
        GO
        CREATE USER TargetUser WITHOUT LOGIN;
        GO
    ```

### Create the target certificate

- Copy and paste the following code into a Query Editor window. Change the file name that is specified in the BACKUP CERTIFICATE statement to refer to a folder on your system. Then, run the code to create the target certificate that is used to encrypt messages. The folder that you specify should have permissions that prevent access from accounts other than your Windows account and the Windows account the instance of the Database Engine is running under. For Lesson 2, you must manually copy the **InstTargetCertificate.cer** file to a folder that can be accessed from the initiator instance.

    ```sql
        CREATE CERTIFICATE InstTargetCertificate
             AUTHORIZATION TargetUser
             WITH SUBJECT = 'Target Certificate',
                  EXPIRY_DATE = N'12/31/2010';

        BACKUP CERTIFICATE InstTargetCertificate
          TO FILE =
        N'C:\storedcerts\$ampleSSBCerts\InstTargetCertificate.cer';
        GO
    ```

### Create the message types

- Copy and paste the following code into a Query Editor window then run it to create the message types for the conversation. The message type names and properties specified here must be identical to the ones that you will create in the **InstInitiatorDB** in the next lesson.

    ```sql
        CREATE MESSAGE TYPE [//BothDB/2InstSample/RequestMessage]
               VALIDATION = WELL_FORMED_XML;
        CREATE MESSAGE TYPE [//BothDB/2InstSample/ReplyMessage]
               VALIDATION = WELL_FORMED_XML;
        GO
    ```

### Create the contract

- Copy and paste the following code into a Query Editor window. Then, run it to create the contract for the conversation. The contract name and properties that are specified here must be identical to the contract that you will create in the **InstInitiatorDB** in the next lesson.

    ```sql
        CREATE CONTRACT [//BothDB/2InstSample/SimpleContract]
              ([//BothDB/2InstSample/RequestMessage]
                 SENT BY INITIATOR,
               [//BothDB/2InstSample/ReplyMessage]
                 SENT BY TARGET
              );
        GO
    ```

### Create the target queue and service

- Copy and paste the following code into a Query Editor window. Then, run it to create the queue and service that is used for the target. The CREATE SERVICE statement associates the service with the **InstTargetQueue**, so that all messages sent to the service will be received into the **InstTargetQueue**. The CREATE SERVICE also specifies that only conversations that use the **//BothDB/ 2InstSample/SimpleContract** that was created earlier can use the service as a target service.

    ```sql
        CREATE QUEUE InstTargetQueue;

        CREATE SERVICE [//TgtDB/2InstSample/TargetService]
               AUTHORIZATION TargetUser
               ON QUEUE InstTargetQueue
               ([//BothDB/2InstSample/SimpleContract]);
        GO
    ```

## Next Steps

You have successfully created the databases that will be used for the tutorial. Next, you will create the **InstInitiatorDB** and configure it with the objects required to support the initiator end of a Service Broker Conversation. For more information, see [Lesson 2: Creating the Initiator Database](lesson-2-creating-the-initiator-database.md).

## See also

- [BACKUP CERTIFICATE (Transact-SQL)](../../t-sql/statements/backup-certificate-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md)
- [CREATE CONTRACT (Transact-SQL)](../../t-sql/statements/create-contract-transact-sql.md)[CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
- [CREATE MASTER KEY (Transact-SQL)](../../t-sql/statements/create-master-key-transact-sql.md)
- [CREATE MESSAGE TYPE (Transact-SQL)](../../t-sql/statements/create-message-type-transact-sql.md)
- [CREATE QUEUE (Transact-SQL)](../../t-sql/statements/create-queue-transact-sql.md)
- [CREATE SERVICE (Transact-SQL)](../../t-sql/statements/create-service-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
- [Service Broker Dialog Security](service-broker-dialog-security.md)
- [Conversation Architecture](conversation-architecture.md)
- [Service Architecture](service-architecture.md)
