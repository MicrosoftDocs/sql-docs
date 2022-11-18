---
title: Overview (Service Broker)
description: "Service Broker helps database developers build reliable and scalable applications."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: 02/10/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Service Broker Overview

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker helps database developers build reliable and scalable applications. Because Service Broker is part of the Database Engine, administration of these applications is part of the routine administration of the database.

Service Broker provides queuing and reliable messaging for SQL Server. Service Broker is used both for applications that use a single SQL Server instance and applications that distribute work across multiple instances.

Within a single SQL Server instance, Service Broker provides a robust asynchronous programming model. Database applications typically use asynchronous programming to shorten interactive response time and increase overall application throughput.

Service Broker also provides reliable messaging between SQL Server instances. Service Broker helps developers compose applications from independent, self-contained components called services. Applications that require the functionality exposed in these services use messages to interact with the services. Service Broker uses TCP/IP to exchange messages between instances. Service Broker includes features to help prevent unauthorized access from the network and to encrypt messages sent over the network.

## In This Section

- [What Does Service Broker Do?](what-does-service-broker-do.md)  
    Describes the functionality that Service Broker provides.

- [Typical Uses of Service Broker](typical-uses-of-service-broker.md)  
    Presents scenarios for using Service Broker.

## Related Sections

- [Planning for Service Broker Development](planning-for-service-broker-development.md)
    Find sources of Service Broker information, overviews, system requirements, installation instructions, and more.

## See also

- [What Does Service Broker Do?](what-does-service-broker-do.md)
- [Benefits of Programming with Service Broker](benefits-of-programming-with-service-broker.md)
- [Logical Architecture](logical-architecture.md)
