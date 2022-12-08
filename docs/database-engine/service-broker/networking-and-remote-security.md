---
title: Networking and Remote Security
description: "To help enable secure, reliable communication between different instances of SQL Server, Service Broker includes features to let you manage routing and establish security for the conversation."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Networking and Remote Security

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

To help enable secure, reliable communication between different instances of SQL Server, Service Broker includes features to let you manage routing and establish security for the conversation.

## In This Section

- [Remote Service Bindings](remote-service-bindings.md)  
    Describes setting the certificate that the broker uses for dialog security. Dialog security provides end-to-end encryption and remote authorization for conversations to specific services.

- [Routes](routes.md)  
    Describes specifying the location of the service and the database that contains the service. A route is required for Service Broker to deliver messages. By default, each database contains a route that specifies that services with no other route defined are delivered within the current instance.

- [Service Broker Endpoints](service-broker-endpoints.md)  
    Describes configuring SQL Server to send and receive messages over TCP/IP connections. Endpoints can provide transport security, which prevents unauthorized connections to the endpoint.

## See also

- [Remote Service Bindings](remote-service-bindings.md)
- [Routes](routes.md)
- [Service Broker Endpoints](service-broker-endpoints.md)
- [Service Broker Dialog Security](service-broker-dialog-security.md)
- [Service Broker Transport Security](service-broker-transport-security.md)
