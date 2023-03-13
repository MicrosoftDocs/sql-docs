---
title: Service Broker Scalability
description: "Service Broker is designed to help your database applications scale well, whether you scale up or scale out."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Service Broker Scalability

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker is designed to help your database applications scale well, whether you scale up or scale out. This topic provides general guidelines for designing applications that take advantage of Service Broker.

Service Broker activation makes it easy to scale up applications as more processing power becomes available. Conversation group locking ensures that service programs can easily avoid the most common sources of contention.

Each Service Broker application is a set of tasks that can operate independently. Service Broker routing allows an application that uses Service Broker to move services to different instances. Because Service Broker, rather than the application, handles message routing, services can be distributed to different computers without changing the application code.

When you design your Service Broker application for scalability, carefully consider how the tasks in the application relate to each other. Services constructed with clear separation between tasks are generally most successful in both scale-up and scale-out scenarios. In general, divide tasks into services by considering the data needed to complete the task. When two related tasks do not modify the same data, consider structuring these tasks as different services. For example, although both a customer management application and a shipping application require access to the customer address, only the customer management application modifies the address. In this case, messages to the shipping application can contain the address information necessary to ship an order. Because there is no need for the shipping application and the customer application to access the same tables, these tasks can be separated cleanly into different services.

## See also

- [Administration (Service Broker)](administration.md)