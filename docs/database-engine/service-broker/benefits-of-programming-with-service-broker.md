---
title: Benefits of Programming with Service Broker
description: "Queuing and asynchronous messaging are needed for many database applications today."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Benefits of Programming with Service Broker

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Queuing and asynchronous messaging are needed for many database applications today. Service Broker provides a new, queue-based durable messaging framework to address these needs. Using the Transact-SQL API provided by Service Broker, you can easily develop services to handle application requirements for queuing or asynchronous communications.

Some of the benefits of programming with Service Broker are:

- **Flexible development:** The programs used in a single distributed application can be written in multiple languages. Each program provides the functionality of each distributed application component.

- **Improved security:** You can express security requirements via certificates, so application components do not need to share the same security context. Service Broker uses SQL Server security features to help you secure your applications.

- **Transactional processing:** Message processing occurs within SQL Server transactions to ensure data integrity. Service Broker supports remote transactional messaging over a standard connection to the database.

- **Guaranteed ordering:** Service Broker provides strong guarantees regarding the delivery and processing of a related set of messages exactly once and in order, so no additional coding is required to provide this functionality.

- **Reliable delivery:** All of the data needed for a conversation--a set of related communications between two or more services--is persisted in SQL Server. Service Broker supports clustering and database mirroring. A conversation may be maintained through system restarts, server failover, network outages, and so on without failing or losing data.

- **Improved scalability:** Service Broker routing delivers messages based on the name of the service, rather than on the network address of the computer where the service runs. This allows you to install an application on multiple computers without changing application code.

- **Ability to use existing knowledge:** Service Broker uses Transact-SQL to create objects. Applications that use Service Broker are most often implemented in Transact-SQL or Microsoft .NET Framework-compatible languages. You do not have to learn a new language to create Service Broker applications.

## See also

- [Creating Service Broker Objects](creating-service-broker-objects.md)
- [Overview (Service Broker)](overview.md)
- [Creating Service Broker Applications](creating-service-broker-applications.md)
