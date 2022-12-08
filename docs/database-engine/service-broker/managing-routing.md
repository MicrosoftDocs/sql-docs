---
title: Managing Routing
description: "Service Broker uses routes to determine where to deliver messages."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Managing Routing

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker uses routes to determine where to deliver messages. This section describes considerations for managing routing.

## Managing AutoCreatedLocal

By default, each user database, including **msdb**, contains the route **AutoCreatedLocal**. This route matches any service name and broker instance and specifies that the message should be delivered within the current instance. **AutoCreatedLocal** has lower priority than routes that explicitly specify the service name or broker instance.

Because **AutoCreatedLocal** exists in **msdb** by default, Service Broker attempts to deliver all messages from outside of the instance within the current instance. In many cases, the database administrator restricts access to services from outside of the instance by dropping **AutoCreatedLocal** in **msdb**. The database administrator then creates a route for each service that communicates with a remote instance.

## Managing Route Expiration

In most cases, a route does not need to expire. The route remains active while the route object exists. If the destination address for the route changes, an administrator either alters the route to update the address or removes the route.

An application that uses dynamic routing, however, may use route expiration to ensure that the routing information remains up to date. Service Broker does not remove expired routes from the database. An application that uses route expiration should also create a SQL Server Agent job to periodically remove route objects that have expired.

## See also

- [Routes](routes.md)
- [Service Broker Routing](service-broker-routing.md)
