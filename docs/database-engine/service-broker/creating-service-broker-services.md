---
title: Creating Service Broker Services
description: "The definition of a Service Broker service includes the names of the contracts for which the service is a target."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Creating Service Broker Services

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

The definition of a Service Broker service includes the names of the contracts for which the service is a target. A target service represents an address that accepts requests for the tasks identified by the contracts that the service specifies. An initiating service represents a return address for a conversation with a target service.

A service represents a business process as a distinct set of tasks. Each contract within the service represents a specific task. A service can specify more than one contract, and a contract can be used by more than one service.

Each service uses a queue to store messages. Messages sent to the service are delivered to the queue. In general, applications are easiest to implement when only one service uses a given queue. However, for flexibility, Service Broker allows multiple services to specify the same queue. In this case, the application either treats all messages of the same type the same way, or inspects both the message type name and the service name to determine how to process the message. This strategy can be convenient when an application supports multiple versions of the same service.

The network format for a message includes the name of the service. Therefore, service names are often chosen to avoid collation issues and naming conflicts. For more information on naming, see [Naming Service Broker Objects](naming-service-broker-objects.md).

To create a service, you must do the following:

1. Create message types that define the data that can be sent back and forth.

2. Create a contract that identifies the message types that can be used, and which endpoint can send them, in order to accomplish a particular task.

3. Create an application to receive, process, and send messages as necessary to accomplish the given task.

4. Create a queue to store the incoming messages for the service. You may associate the queue with an activation stored procedure so that the broker automatically activates the stored procedure to process messages as messages arrive.

5. Create a service and associate it with the queue that will receive the messages for the service. The service exposes the contracts that define the tasks that the service will perform on behalf of other services. The service does not need to specify contracts for tasks that the service requests from other services.

The exact steps involved in creating a service may differ somewhat depending on the specific needs of the service. For example, when you create a service to handle event notifications, the message type and contract are already defined by SQL Server, so there is no need to create them.

## See also

- [Conversation Architecture](conversation-architecture.md)
- [Creating Service Broker Queues](creating-service-broker-queues.md)