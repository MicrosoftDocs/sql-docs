---
title: Networking and Remote Security
description: "To help enable secure, reliable communication between different instances of SQL Server, Service Broker includes features to let you manage routing and establish security for the conversation."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# Networking and remote security

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

To help enable secure, reliable communication between different instances of SQL Server, Service Broker includes features to let you manage routing and establish security for the conversation.

## In this section

| Article | Description |
| --- | --- |
| [Remote service bindings](remote-service-bindings.md) | Describes setting the certificate that the broker uses for dialog security. Dialog security provides end-to-end encryption and remote authorization for conversations to specific services. |
| [Routes](routes.md) | Describes specifying the location of the service and the database that contains the service. A route is required for Service Broker to deliver messages. By default, each database contains a route that specifies that services with no other route defined are delivered within the current instance. |
| [Service Broker endpoints](service-broker-endpoints.md) | Describes configuring SQL Server to send and receive messages over TCP/IP connections. Endpoints can provide transport security, which prevents unauthorized connections to the endpoint. |

## Related content

- [Remote service bindings](remote-service-bindings.md)
- [Routes](routes.md)
- [Service Broker endpoints](service-broker-endpoints.md)
- [Service Broker dialog security](service-broker-dialog-security.md)
- [Service Broker transport security](service-broker-transport-security.md)
