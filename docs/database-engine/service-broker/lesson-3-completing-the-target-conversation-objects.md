---
title: "Lesson 3: Completing the Target Conversation Objects"
description: "In this lesson, you will learn to create the linked server and routes from the target instance of the Database Engine to the initiator instance."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Lesson 3: Completing the Target Conversation Objects

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

In this lesson, you will learn to create the linked server and routes from the target instance of the Database Engine to the initiator instance. Run these steps from a copy of Management Studio that is running on the same computer as the target instance.

## Procedures

### Create references to initiator objects

- Copy and paste the following code into a Query Editor window. Change the FROM FILE clause to reference the folder to which you copied the **InstInitiatorCertficate.cer** file from step 4 in Lesson 2. Then, run the code to create an initiator user and pull in the initiator certificate.

    ```sql  
        USE InstTargetDB
        GO
        CREATE USER InitiatorUser WITHOUT LOGIN;

        CREATE CERTIFICATE InstInitiatorCertificate
           AUTHORIZATION InitiatorUser
           FROM FILE =
        N'C:\storedcerts\$ampleSSBCerts\InstInitiatorCertificate.cer';
        GO
    ```

### Create routes

- Copy and paste the following code into a Query Editor window. Change the string **MyInitiatorComputer** to the name of the computer that is running your initiator instance. Then, run the code to create routes to the target service and initiator service, and a remote service binding that associates the **InitiatorUser** with the initiator service route.

    The following CREATE ROUTE statements assume that there are no duplicate service names in the target instance. If multiple databases on the target instance contain services that have the same name, use the BROKER_INSTANCE clause to specify the database on which you want to open a conversation.

    ```sql
        DECLARE @Cmd NVARCHAR(4000);

        SET @Cmd = N'USE InstTargetDB;
        CREATE ROUTE InstInitiatorRoute
        WITH SERVICE_NAME =
               N''//InstDB/2InstSample/InitiatorService'',
             ADDRESS = N''TCP://MyInitiatorComputer:4022'';';

        EXEC (@Cmd);

        SET @Cmd = N'USE msdb
        CREATE ROUTE InstTargetRoute
        WITH SERVICE_NAME =
                N''//TgtDB/2InstSample/TargetService'',
             ADDRESS = N''LOCAL''';

        EXEC (@Cmd);
        GO
        GRANT SEND
              ON SERVICE::[//TgtDB/2InstSample/TargetService]
              TO InitiatorUser;
        GO
        CREATE REMOTE SERVICE BINDING InitiatorBinding
              TO SERVICE N'//InstDB/2InstSample/InitiatorService'
              WITH USER = InitiatorUser;
        GO
    ```

## Next Steps

You have successfully finished configuring the target database to support a Service Broker conversation to the initiator database. Next, you will begin a conversation in the initiator database and send a request message to the target service. For more information, see [Lesson 4: Beginning the Conversation](lesson-4-beginning-the-conversation.md).

## See also

- [CREATE REMOTE SERVICE BINDING (Transact-SQL)](../../t-sql/statements/create-remote-service-binding-transact-sql.md)
- [CREATE ROUTE (Transact-SQL)](../../t-sql/statements/create-route-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
- [EXECUTE (Transact-SQL)](../../t-sql/language-elements/execute-transact-sql.md)
- [sp_addlinkedserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)
- [Service Broker Routing and Networking](service-broker-routing-and-networking.md)
- [Networking and Remote Security](networking-and-remote-security.md)
