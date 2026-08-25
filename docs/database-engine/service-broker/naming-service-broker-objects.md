---
title: Name Service Broker Objects
description: "This topic describes considerations for naming service broker objects."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: maghan
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# Name Service Broker objects

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes considerations for naming service broker objects. The conventions differ slightly for public interface objects, network and security configuration objects, and queues.

## Public interface objects

Contracts, services, and message types form the public interface of a Service Broker application. Because the names of these objects are contained in messages, naming conventions for these objects often follow Uniform Resource Identifier (URI) naming conventions. This helps to ensure unique names for the objects.

Services names can also use the conventions to specify a network address in a route. In this case, the name of the service can be used in a transport route. For more information on routing, see [Service Broker routing](service-broker-routing.md).

When sending and receiving messages, Service Broker uses binary matching for the names of these objects. Therefore, characters that have more than one binary representation require special care when public interface objects are named.

## Network and security configuration objects

The names for routes and remote service bindings are never included in a message. For convenience, these names can use the name of the service that the object configures.

These objects can't be temporary objects. Therefore, the number sign (\#) isn't considered significant in names for these objects. An object with a name that begins with \# is a permanent object rather than a temporary object.

## Queues

Queue names can be used for many of the statements that accept table names. Therefore, queues names follow standard SQL Server identifier conventions, with one exception. Because queues can't be temporary objects, the name of a queue can't start with the number sign (\#). Queues are schema-owned objects, so queue names can include a schema name and database name.

## Related content

- [CREATE CONTRACT (Transact-SQL)](../../t-sql/statements/create-contract-transact-sql.md)
- [CREATE MESSAGE TYPE (Transact-SQL)](../../t-sql/statements/create-message-type-transact-sql.md)
- [CREATE QUEUE (Transact-SQL)](../../t-sql/statements/create-queue-transact-sql.md)
- [CREATE REMOTE SERVICE BINDING (Transact-SQL)](../../t-sql/statements/create-remote-service-binding-transact-sql.md)
- [CREATE ROUTE (Transact-SQL)](../../t-sql/statements/create-route-transact-sql.md)
- [CREATE SERVICE (Transact-SQL)](../../t-sql/statements/create-service-transact-sql.md)
