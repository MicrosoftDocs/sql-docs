---
title: Migration (Service Broker)
description: "The usual process for migrating a Service Broker application is to move the database that contains the application to another instance of the Database Engine."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
ms.custom: intro-migration
---

# Migration (Service Broker)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

The usual process for migrating a Service Broker application is to move the database that contains the application to another instance of the Database Engine. Many aspects of the Service Broker application move with the database. Some aspects of the application must be re-created or reconfigured in the new location.

The database contains the Service Broker objects, stored procedures, certificates, users, and outgoing routes for the application. These move with the database. Most Service Broker databases have a database master key. You must use the password for the master key when you attach the database in the new location.

After moving the database, you must do the following:

- Configure any required logins.

- Update the services that initiate conversations with the service that you are moving. In each database that contains a route for the service that you are moving, alter the route to use the new network address.

- Use the CREATE DATABASE or ALTER DATABASE statements to activate Service Broker message delivery in the restored database, and to set a different broker instance identifier. A broker instance identifier should be used by only one database on the network at a time. Typically, you do not change the instance identifier when you restore a backup that is intended to be identical to the original database. For example, you do not change the broker instance identifier when you attach a database for any of the following reasons:

    - To recover a database

    - To create a mirrored pair

    - To configure log shipping for a standby server

- Routes for incoming messages are not included in the database that contains the service. If your service uses an explicit route in the **msdb** database to route incoming messages to the service, you must re-create this route when you attach a database in a different instance.

- Service Broker endpoints and transport security apply to the instance as a whole instead of to a specific database. Attaching a database to a new instance does not affect endpoints or transport security for that instance. If your service sends or receives messages over the network, you must ensure that the new instance has a Service Broker endpoint. You must also ensure that transport security for the instance is configured as required for your application.

After moving a database, you can check for Service Broker errors by running the **ssbdiagnose** utility. For more information, see [Ssbdiagnose Utility](../../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md).

## See also

- [How to: Activate Service Broker Message Delivery in Databases (Transact-SQL)](how-to-activate-service-broker-message-delivery-in-databases-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [ALTER ROUTE (Transact-SQL)](../../t-sql/statements/alter-route-transact-sql.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
- [Copy Databases to Other Servers](../../relational-databases/databases/copy-databases-to-other-servers.md)
- [Managing Service Broker Identities](managing-service-broker-identities.md)
- [Routes](routes.md)
- [Service Broker Routing](service-broker-routing.md)
