---
title: "Lesson 2: Creating the Initiator Database"
description: "In this lesson, you will learn to create the initiator database and all the initiator Service Broker objects that are used in this tutorial."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Lesson 2: Creating the Initiator Database

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you will learn to create the initiator database and all the initiator Service Broker objects that are used in this tutorial. Run these steps from a copy of Management Studio that is running on the same computer as the initiator instance the Database Engine.

## Procedures

### Create a Service Broker endpoint

- Copy and paste the following code into a Query Editor window. Then, run it to create a Service Broker endpoint for this instance of the Database Engine. A Service Broker endpoint specifies the network address to which Service Broker messages are sent. This endpoint uses the Service Broker default of TCP port 4022, and specifies that remote instances of the Database Engine will use Windows Authentication connections to send messages.

    Windows Authentication works when both computers are in the same domain, or are in trusted domains. If the computers are not in trusted domains, use certificate security for the endpoints. For more information, see [How to: Create Certificates for Service Broker Transport Security (Transact-SQL)](how-to-create-certificates-for-service-broker-transport-security-transact-sql.md).

    ```sql
        USE master;
        GO
        IF EXISTS (SELECT * FROM sys.endpoints
                   WHERE name = N'InstInitiatorEndpoint')
             DROP ENDPOINT InstInitiatorEndpoint;
        GO
        CREATE ENDPOINT InstInitiatorEndpoint
        STATE = STARTED
        AS TCP ( LISTENER_PORT = 4022 )
        FOR SERVICE_BROKER (AUTHENTICATION = WINDOWS );
        GO
    ```

### Create the initiator database, master key, and user

- Copy and paste the following code into a Query Editor window. Change the password on the CREATE MASTER KEY statement. Then, run the code to create the target database that is used for this tutorial. By default, new databases have the ENABLE_BROKER option set to on. The code also creates the master key and user that will be used to support encryption and remote connections.

    ```sql
        USE master;
        GO
        IF EXISTS (SELECT * FROM sys.databases
                   WHERE name = N'InstInitiatorDB')
             DROP DATABASE InstInitiatorDB;
        GO
        CREATE DATABASE InstInitiatorDB;
        GO
        USE InstInitiatorDB;
        GO

        CREATE MASTER KEY
               ENCRYPTION BY PASSWORD = N'<EnterStrongPassword2Here>';
        GO
        CREATE USER InitiatorUser WITHOUT LOGIN;
        GO
    ```

### Create the initiator certificate

- Copy and paste the following code into a Query Editor window. Change the file name that is specified in the BACKUP CERTIFICATE statement to refer to a folder on your system. Then, run the code to create the initiator certificate that is used to encrypt messages. The folder that you specify should have permissions that prevent access from accounts other than your Windows account and the Windows account the instance of the Database Engine is running under. For Lesson 3, you must manually copy the **InstInitiatorCertificate.cer** file to a folder that can be accessed from the target instance.

    ```sql
        CREATE CERTIFICATE InstInitiatorCertificate
             AUTHORIZATION InitiatorUser
             WITH SUBJECT = N'Initiator Certificate',
                  EXPIRY_DATE = N'12/31/2010';

        BACKUP CERTIFICATE InstInitiatorCertificate
          TO FILE =
        N'C:\storedcerts\$ampleSSBCerts\InstInitiatorCertificate.cer';
        GO
    ```

### Create the message types

- Copy and paste the following code into a Query Editor window. Then, run it to create the message types for the conversation. The message type names and properties specified here must be identical to the ones that were created in the **InstTargetDB** in the previous lesson.

    ```sql
        CREATE MESSAGE TYPE [//BothDB/2InstSample/RequestMessage]
               VALIDATION = WELL_FORMED_XML;
        CREATE MESSAGE TYPE [//BothDB/2InstSample/ReplyMessage]
               VALIDATION = WELL_FORMED_XML;
        GO
    ```

### Create the contract

- Copy and paste the following code into a Query Editor window. Then, run it to create the contract for the conversation. The contract name and properties that are specified here must be identical to the contract that you will create in the **InstInitiatorDB** during the next lesson.

    ```sql
        CREATE CONTRACT [//BothDB/2InstSample/SimpleContract]
              ([//BothDB/2InstSample/RequestMessage]
                 SENT BY INITIATOR,
               [//BothDB/2InstSample/ReplyMessage]
                 SENT BY TARGET
              );
        GO
    ```

### Create the initiator queue and service

- Copy and paste the following code into a Query Editor window. Then, run it to create the queue and service that is used for the target. The CREATE SERVICE statement associates the service with the **InstInitiatorQueue**. Therefore, all messages that are sent to the service will be received into the **InstInitiatorQueue**. The CREATE SERVICE also specifies that only conversations that use the **//BothDB/ 2InstSample/SimpleContract** that was created earlier can use the service as a target service.

    ```sql
        CREATE QUEUE InstInitiatorQueue;

        CREATE SERVICE [//InstDB/2InstSample/InitiatorService]
               AUTHORIZATION InitiatorUser
               ON QUEUE InstInitiatorQueue;
        GO
    ```

### Create references to target objects

- Copy and paste the following code into a Query Editor window. Change the FROM FILE clause to reference the folder to which you copied the **InstTargetCertficate.cer** file from step 3 in Lesson 1. Then, run the code to create a target user and pull in the target certificate.

    ```sql
        CREATE USER TargetUser WITHOUT LOGIN;

        CREATE CERTIFICATE InstTargetCertificate
           AUTHORIZATION TargetUser
           FROM FILE =
        N'C:\storedcerts\$ampleSSBCerts\InstTargetCertificate.cer'
        GO
    ```

### Create routes

- Copy and paste the following code into a Query Editor window. Change the string **MyTargetComputer** to the name of the computer that is running your target instance. Then, run the code to create routes to the target service and initiator service, and a remote service binding that associates the **TargetUser** with the target service route.

    The following CREATE ROUTE statements assume that there are no duplicate service names in the target instance. If multiple databases on the target instance have services with the same name, use the BROKER_INSTANCE clause to specify the database on which you want to open a conversation.

    ```sql
        DECLARE @Cmd NVARCHAR(4000);

        SET @Cmd = N'USE InstInitiatorDB;
        CREATE ROUTE InstTargetRoute
        WITH SERVICE_NAME =
               N''//TgtDB/2InstSample/TargetService'',
             ADDRESS = N''TCP://MyTargetComputer:4022'';';

        EXEC (@Cmd);

        SET @Cmd = N'USE msdb
        CREATE ROUTE InstInitiatorRoute
        WITH SERVICE_NAME =
               N''//InstDB/2InstSample/InitiatorService'',
             ADDRESS = N''LOCAL''';

        EXEC (@Cmd);
        GO
        CREATE REMOTE SERVICE BINDING TargetBinding
              TO SERVICE
                 N'//TgtDB/2InstSample/TargetService'
              WITH USER = TargetUser;

        GO
    ```

## Next Steps

You have successfully created the initiator databases that will be used for the tutorial. Next, you will finish configuring the target database by creating the target objects that have dependencies on initiator objects. For more information, see [Lesson 3: Completing the Target Conversation Objects](lesson-3-completing-the-target-conversation-objects.md).

## See also

- [BACKUP CERTIFICATE (Transact-SQL)](../../t-sql/statements/backup-certificate-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md)
- [CREATE CONTRACT (Transact-SQL)](../../t-sql/statements/create-contract-transact-sql.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
- [CREATE MASTER KEY (Transact-SQL)](../../t-sql/statements/create-master-key-transact-sql.md)
- [CREATE MESSAGE TYPE (Transact-SQL)](../../t-sql/statements/create-message-type-transact-sql.md)
- [CREATE QUEUE (Transact-SQL)](../../t-sql/statements/create-queue-transact-sql.md)
- [CREATE REMOTE SERVICE BINDING (Transact-SQL)](../../t-sql/statements/create-remote-service-binding-transact-sql.md)
- [CREATE ROUTE (Transact-SQL)](../../t-sql/statements/create-route-transact-sql.md)
- [CREATE SERVICE (Transact-SQL)](../../t-sql/statements/create-service-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
- [EXECUTE (Transact-SQL)](../../t-sql/language-elements/execute-transact-sql.md)
- [sp_addlinkedserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)
- [Service Broker Dialog Security](service-broker-dialog-security.md)
- [Conversation Architecture](conversation-architecture.md)
- [Service Architecture](service-architecture.md)
