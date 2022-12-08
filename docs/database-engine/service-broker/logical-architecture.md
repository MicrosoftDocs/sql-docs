---
title: Logical Architecture (Service Broker)
description: "Service Broker applications consist of Service Broker database objects and one or more applications that use those objects."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Logical Architecture \(Service Broker\)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker applications consist of Service Broker database objects and one or more applications that use those objects. This section describes each of the objects that are used in a Service Broker application.

There are three types of Service Broker components:

- Conversation components. The run-time structure of the conversation. Applications exchange messages as part of a conversation.

- Service definition objects. These are design-time components that specify the basic design of the application. These components define the message types for the application, the conversation flow for the application, and the database storage for the application.

- Routing and security components. These components define the infrastructure for exchanging messages between databases and instances of the Database Engine.

- This section also presents short overview of building applications by using Service Broker.

## In This Section

- [Conversation Architecture](conversation-architecture.md)  
    Describes conversations, conversation groups, and messages. These are the Service Broker objects that are created and managed at runtime in application code.

- [Service Architecture](service-architecture.md)  
    Describes the design-time objects that are created in databases as part of the Service Broker infrastructure.

- [Networking and Remote Security](networking-and-remote-security.md)  
    Describes objects that control how Service Broker communicates between databases and instances of the Database Engine.

## See also

- [Benefits of Programming with Service Broker](benefits-of-programming-with-service-broker.md)