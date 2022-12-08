---
title: Service Broker Routing and Networking
description: "Service Broker communications are designed around reliable, asynchronous message delivery."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Service Broker Routing and Networking

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker communications are designed around reliable, asynchronous message delivery. Service Broker uses the authentication features that are provided by SQL Server to help protect against unauthorized access to a service. To help protect message data, Service Broker lets you encrypt messages that leave the instance.

Communication between two applications occurs through messages. When an application sends messages, Service Broker locates a route for the service and transmits the message to the network address that is specified by the route.

Service Broker communicates the status of a conversation to an application through messages. Service Broker indicates errors, the end of a conversation, and timer events by sending a message to the service. Like all messages, these messages are associated with a specific conversation.

Messages from Service Broker and messages from the other side of the conversation arrive and are processed in the same way. By using the same programming model to process all messages, you simplify application development. This also lets Service Broker applications be redeployed without changes to application code.

## In This Section

- [Service Broker Routing](service-broker-routing.md)  
    Describes how Service Broker routes messages.

- [Service Broker Communication Protocols](service-broker-communication-protocols.md)  
    Describes how Service Broker uses a broker-specific protocol to communicate with remote brokers.

- [Service Broker Message Forwarding](service-broker-message-forwarding.md)  
    Describes the message forwarding system.

## See also

- [Conversation Architecture](conversation-architecture.md)