---
title: Routes
description: "Service Broker uses routes to determine where to deliver messages"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Routes

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker uses routes to determine where to deliver messages. When a service sends a message on a conversation, SQL Server uses routes to locate the service that will receive the message. When that service responds, SQL Server again uses routes to locate the initiating service. By default, each database contains a route that specifies that messages for any service which does not have an explicit route are delivered within the SQL Server instance.

There are three basic components of a route:

- Service name  
  The name of the service that this route specifies addressing for. This name must be an exact match for the Service Name in the BEGIN DIALOG command.

- Broker instance identifier  
  A unique identifier for a specific database to send the messages to. This is the **service_broker_guid** column in the **sys.databases** table row for the database that this route points to.

- Network address  
  An actual machine address, a keyword that restricts the route to the local machine, or a keyword that indicates that the transport layer deduces the address from the service name. A network address can be the address of the broker that hosts the service, or it can be the address of a forwarding broker.

To determine the route for a conversation, SQL Server matches the service name and the broker instance identifier that were specified in the BEGIN DIALOG CONVERSATION statement against the service name and broker instance identifier that are specified in the route. Routes that do not provide a service name match any service name. Routes that do not provide a broker instance identifier match any broker instance identifier. When more than one route matches a conversation, SQL Server selects a route, as described in [Service Broker Routing](service-broker-routing.md).

SQL Server guarantees that once the target acknowledges the first message, all subsequent messages on that conversation route to the same database. However, other conversations on the same conversation group are not guaranteed to route to the same database. If an application requires that messages on related conversations route to the same database, the application must provide a broker instance identifier when the application begins a conversation.

By default, each user database contains the route **AutoCreatedLocal**. This route matches any service name and broker instance, and specifies that the message should be delivered within the current instance. For simple scenarios where both the initiator and the target for the conversation exist in the same SQL Server instance, no additional routes are necessary. However, creation of a route for each service helps to safeguard against modification or dropping of the **AutoCreatedLocal** route.

## See also

- [BEGIN DIALOG CONVERSATION (Transact-SQL)](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)
- [CREATE ROUTE (Transact-SQL)](../../t-sql/statements/create-route-transact-sql.md)
- [Service Broker Routing](service-broker-routing.md)
- [Managing Service Broker Identities](managing-service-broker-identities.md)