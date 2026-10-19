---
title: "Migration (Service Broker)"
description: "The usual process for migrating a Service Broker application is to move the database that contains the application to another instance of the Database Engine."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
ms.custom:
  - intro-migration
---

# Migration (Service Broker)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

The usual process for migrating a Service Broker application is to move the database that contains the application to another instance of the Database Engine. Many aspects of the Service Broker application move with the database. Some aspects of the application must be re-created or reconfigured in the new location.

The database contains the Service Broker objects, stored procedures, certificates, users, and outgoing routes for the application. These objects move with the database. Most Service Broker databases have a database master key (DMK). You must use the password for the DMK when you attach the database in the new location.

After moving the database, you must do the following steps:

- Configure any required logins.

- Update the services that initiate conversations with the service that you're moving. In each database that contains a route for the service that you're moving, alter the route to use the new network address.

- Use the `CREATE DATABASE` or `ALTER DATABASE` statements to activate Service Broker message delivery in the restored database, and to set a different broker instance identifier. You should only use one broker instance identifier on one database on the network at a time. Typically, you don't change the instance identifier when you restore a backup that's intended to be identical to the original database. For example, you don't change the broker instance identifier when you attach a database for any of the following reasons:

  - To recover a database
  - To create a mirrored pair
  - To configure log shipping for a standby server

- Routes for incoming messages aren't included in the database that contains the service. If your service uses an explicit route in the `msdb` database to route incoming messages to the service, you must re-create this route when you attach a database in a different instance.

- Service Broker endpoints and transport security apply to the instance as a whole instead of to a specific database. Attaching a database to a new instance doesn't affect endpoints or transport security for that instance. If your service sends or receives messages over the network, you must ensure that the new instance has a Service Broker endpoint. You must also ensure that transport security for the instance is configured as required for your application.

After moving a database, you can check for Service Broker errors by running the **ssbdiagnose** utility. For more information, see [ssbdiagnose utility (Service Broker)](../../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md).

## Related content

- [How to: Activate Service Broker message delivery in databases (Transact-SQL)](how-to-activate-service-broker-message-delivery-in-databases-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [ALTER ROUTE (Transact-SQL)](../../t-sql/statements/alter-route-transact-sql.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
- [Copy databases to other servers](../../relational-databases/databases/copy-databases-to-other-servers.md)
- [Manage Service Broker identities](managing-service-broker-identities.md)
- [Routes](routes.md)
- [Service Broker routing](service-broker-routing.md)
