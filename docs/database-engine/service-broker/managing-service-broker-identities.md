---
title: Managing Service Broker Identities
description: "Each database contains a unique identifier that is used for routing Service Broker messages to that database."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Managing Service Broker Identities

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Each database contains a unique identifier that is used for routing Service Broker messages to that database. This topic describes Service Broker identifiers, how Service Broker protects against message misdirection, and the options that are available to manage Service Broker identifiers.

## Service Broker Identifiers

Each database contains a Service Broker identifier that distinguishes it from all other databases in the network. The **service_broker_guid** column of the **sys.databases** catalog view shows the Service Broker identifier for each database in the instance. Service Broker systems can be designed to run multiple copies of a service. Each copy of the service runs in a separate database. In a system that has multiple copies of a service, use the BROKER_INSTANCE clause of the CREATE ROUTE statement to create a route to a specific copy of the service.

Service Broker routing uses the Service Broker identifier to ensure that all messages for a conversation are delivered to the same database. The BEGIN DIALOG CONVERSATION statement opens a conversation with a destination service. If a conversation is successfully opened, the acknowledgment message from the destination service contains the Service Broker identifier for the destination database. Service Broker then routes all messages for the conversation to the specified database.

Service Broker identifiers can be specified in the TO SERVICE clause of the BEGIN DIALOG CONVERSATION statement to control the type of routing to be performed:

- To route conversations to a specific copy of a service, specify a *service_broker_guid*. For example, you could have three copies of a service in three databases on the network: a development database, a test database, and a production database. The BEGIN DIALOG CONVERSATION statements in each system should specify *service_broker_guid* to ensure that all messages go to the correct database.

- To let Service Broker balance conversation loads across multiple copies of a service, do not specify *service_broker_guid*. Service Broker will alternatively pick among the routes with the same service name as is specified in the TO SERVICE clause of BEGIN DIALOG CONVERSATION.

By default, if there is only one copy of a service in a network, Service Broker correctly routes the conversations. You do not have to specify the Service Broker identifier in CREATE ROUTE or BEGIN DIALOG CONVERSATION statements.

For more information about Service Broker route matching, see [Service Broker Routing](service-broker-routing.md).

To correctly support message delivery, each Service Broker identifier should be unique across all instances of the Database Engine on the same network. Otherwise, messages could be misdirected. When a new database is created, it is assigned a new Service Broker identifier that should be unique in the network. The identifier is restored when the database is either restored or attached. Be careful when you restore and attach databases. You should not have multiple databases that are actively performing Service Broker operations and using the same identifiers.

## Service Broker Message Delivery

SQL Server provides a mechanism for deactivating Service Broker message delivery in a database if it has the same Service Broker identifier as another database in the same network. When message delivery is deactivated in a database, all messages sent from that database remain in the transmission queue for the database. Further, Service Broker does not consider services in that database to be available for receiving messages. These services are not considered when Service Broker routing locates a destination service in an instance.

Deactivating Service Broker message delivery lets you safely attach a backup of a database for troubleshooting or data recovery purposes without the risk of misdirected messages. The **is_broker_enabled** column of **sys.databases** shows the current state of Service Broker message delivery for each database.

When you attach or restore a database, use care to ensure that only one database that has a given Service Broker identifier has message delivery active. Otherwise, messages could be misdirected, and processing for a conversation might occur in the wrong copy of the database.

## Managing Identifiers and Message Delivery

The CREATE DATABASE command, the ALTER DATABASE command, and the RESTORE DATABASE command contain options to activate Service Broker message delivery. They also contain options to change the Service Broker identifier for a database.

By default, when you attach or restore a database, the Service Broker identifier and message delivery status are unchanged. Typically, you do not change the Service Broker identifier in the following situations:

- When you restore a backup for recovery purposes.

- When you configure a mirrored pair.

- When you set up log shipping for a standby server. When you are making a copy of the database, you change the instance identifier.

There are four options to manage identifiers and message delivery:

- ENABLE_BROKER. This option activates Service Broker message delivery, preserving the existing Service Broker identifier for the database.




    > [!NOTE]
    > Enabling SQL Server Service Broker in any database require a database lock. To enable Service Broker in the **msdb** database, first stop SQL Server Agent. Then, Service Broker can obtain the necessary lock.


- DISABLE_BROKER. This option deactivates Service Broker message delivery, preserving the existing Service Broker identifier for the database.

- NEW_BROKER. This option activates Service Broker message delivery and creates a new Service Broker identifier for the database. This option ends all existing conversations in the database, and returns an error for each conversation. This is because these conversations do not use the new identifier. Any route that references the old Service Broker identifier must be re-created with the new identifier.

- ERROR_BROKER_CONVERSATIONS. This option activates Service Broker message delivery, preserving the existing Service Broker identifier for the database. Service Broker ends all conversations in the database, and returns an error for each conversation. This option is typically used when you must restore a database to a different point in time than other databases with which it has open conversations. All conversations in the restored database must be ended with an error because they are now out of sync with the other databases. The Service Broker identifier is retained so that all routes that reference the identifier are still valid.

Regardless of the specified option, SQL Server does not allow for two databases that have the same Service Broker identifier to both have message delivery active in the same instance of SQL Server. If you attach a database that has the same Service Broker identifier as an existing database, SQL Server deactivates Service Broker message delivery in the database that is being attached.

For more information about the options for attaching databases, see [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md) and [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md). For information about how to activate Service Broker message delivery in a database, see [How to: Activate Service Broker Message Delivery in Databases (Transact-SQL)](how-to-activate-service-broker-message-delivery-in-databases-transact-sql.md).

## See also

- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
- [sys.databases (Transact-SQL)](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
