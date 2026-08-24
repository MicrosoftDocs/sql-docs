---
title: Benefits of Programming with Service Broker
description: "Queuing and asynchronous messaging are needed for many database applications today."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: maghan
ms.date: 08/29/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# Benefits of programming with Service Broker

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Queuing and asynchronous messaging are needed for many database applications today. Service Broker provides a queue-based durable messaging framework to address these needs. Using the Transact-SQL API provided by Service Broker, you can easily develop services to handle application requirements for queuing or asynchronous communications.

Some of the benefits of programming with Service Broker are:

- **Flexible development:** You can write the programs used in a single distributed application in multiple languages. Each program provides the functionality of each distributed application component.

- **Improved security:** You can express security requirements via certificates, so application components don't need to share the same security context. Service Broker uses SQL Server security features to help you secure your applications.

- **Transactional processing:** Message processing occurs within SQL Server transactions to ensure data integrity. Service Broker supports remote transactional messaging over a standard connection to the database.

- **Guaranteed ordering:** Service Broker provides strong guarantees regarding the delivery and processing of a related set of messages exactly once and in order, so there's no additional coding required to provide this functionality.

- **Reliable delivery:** All of the data needed for a conversation, or a set of related communications between two or more services, is persisted in SQL Server. Service Broker supports clustering and database mirroring. A conversation can be maintained through system restarts, server failover, network outages, and so on without failing or losing data.

- **Improved scalability:** Service Broker routing delivers messages based on the name of the service, rather than on the network address of the computer where the service runs. This allows you to install an application on multiple computers without changing application code.

- **Ability to use existing knowledge:** Service Broker uses Transact-SQL to create objects. Applications that use Service Broker are most often implemented in Transact-SQL or Microsoft .NET Framework-compatible languages. You don't have to learn a new language to create Service Broker applications.

## Related content

- [Create Service Broker objects](creating-service-broker-objects.md)
- [Service Broker](../configure-windows/sql-server-service-broker.md)
- [Create Service Broker applications](creating-service-broker-applications.md)
