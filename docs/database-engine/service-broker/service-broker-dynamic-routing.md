---
title: Service Broker Dynamic Routing
description: "For most scenarios, Service Broker applications use routes configured by an administrator."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# Service Broker dynamic routing

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

For most scenarios, Service Broker applications use routes configured by an administrator. In some cases, you might need to dynamically configure routes. In this case, you implement a Broker Configuration Notice service.

## Dynamic routing overview

SQL Server doesn't provide a built-in solution for dynamically creating routing tables. Instead, SQL Server provides functionality that allows developers to create applications that provide dynamic routing.

When Service Broker can't find a route for a conversation, Service Broker checks the routing table for a service named **SQL/ServiceBroker/BrokerConfiguration**. If an entry exists for that service, Service Broker creates a new conversation to that service and sends a message on that conversation requesting that a route be created. When the conversation to the **BrokerConfiguration** service ends, Service Broker again attempts to route the message. If no route exists at that point, Service Broker marks all messages for the conversation as `DELAYED`. After a timeout period, Service Broker again requests a route from the **BrokerConfiguration** service.

## Create the Broker Configuration Notice service

To create a service for dynamic routing, create a service named **SQL/ServiceBroker/BrokerConfiguration** that accepts conversations on the contract `https://schemas.microsoft.com/SQL/ServiceBroker/BrokerConfigurationNotice`. Then, create a route to the service in the routing table for database that will use dynamic routing.

Requests for routes use the message type `https://schemas.microsoft.com/SQL/ServiceBroker/BrokerConfigurationNotice/MissingRoute`. The message is in XML format, and contains the name of the service for which routing information should be available.

For example, the following message is a request for a route to the service `https://Adventure-Works.com/Elsewhere`:

```xml
    <MissingRoute xmlns="https://schemas.microsoft.com/SQL/ServiceBroker/BrokerConfigurationNotice/MissingRoute">
      <SERVICE_NAME>https://Adventure-Works.com/Elsewhere</SERVICE_NAME>
    </MissingRoute>
```

For more information, see [Service Broker Configuration Notice service](service-broker-configuration-notice-service.md).

## Implement dynamic routing

Most of the work involved in implementing dynamic routing is determining the address for the service name. You use whatever technique is appropriate for your application.

The responsibilities of the application for the Broker Configuration Notice service are simple. The application reads a **MissingRoute** message from the queue. If the application can determine the address for the service, the application creates a route for the service and then ends the conversation. How the application determines the address depends on the specific needs of your environment. For example, the application might consult an external service, run a query in another database, or produce a route to a Service Broker configured for message forwarding. However, if the application can't determine the address for the service, the application ends the conversation with an error.

In either case, the application that implements the Broker Configuration Notice service ends the conversation. Service Broker sends one **MissingRoute** message at a time for a specific service, regardless of the number of conversations to the service. Further, Service Broker uses the largest possible timeout for requests to the Broker Configuration service. Therefore, if the Broker Configuration service doesn't end the conversation, Service Broker doesn't create a new request to the service. If the Broker Configuration service didn't create a route, or the lifetime of the route that the service created expires, messages to the service will remain delayed until the conversation lifetime expires.

## Related content

- [Service Broker routing](service-broker-routing.md)
