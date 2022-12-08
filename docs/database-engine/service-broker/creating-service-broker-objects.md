---
title: Creating Service Broker Objects
description: "An application uses Service Broker by executing Transact-SQL statements that operate on Service Broker objects defined in a database."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Creating Service Broker Objects

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

An application uses Service Broker by executing Transact-SQL statements that operate on Service Broker objects defined in a database. This section describes general considerations when you create the Service Broker objects for an application.

## Overview

Service Broker objects define the metadata and storage for a specific set of tasks:

- Message types define the data that is exchanged in a conversation.

- Contracts define tasks. Each contract specifies the message types that can be used in a particular conversation, and which side of the conversation can send the message.

- A queue stores incoming messages for a service.

- A service represents a related set of business tasks. The name of the service is also used to locate the queue for the service.

Notice that a contract depends on one or more message types. A service depends on a queue, and can depend on one or more contracts. Therefore, contracts are created after message types and dropped before message types. Services are created after queues and contracts, and dropped before queues and contracts.

- For more information on these objects, see [Conversation Architecture](conversation-architecture.md).

## Creating Objects for a Service

The procedure for creating a service follows the same basic outline regardless of whether your service is an initiating service, a target service, or both.

The definition of a service specifies the contracts for which the service can be a target. In contrast, an application can use a service to initiate a conversation that uses any contract defined in the database. Service Broker takes this approach to enforce the general rule that a service should only receive messages that the application can process. To ensure that the application does not receive messages of an arbitrary or unknown type, Service Broker accepts a new dialog only if the dialog follows a contract specified in the service. An initiating service specifies the contract to use when the conversation begins, so an initiating service does not need to include the contract in the service definition.

To create the objects for a service, do the following:

1. Create message types that define the messages your service will use to accomplish any required communication. You can define these types yourself or obtain scripts to create the types from the creator of the service with which your service will communicate. You skip this step when the database already contains the message types that your service needs.

2. Create one or more contracts that define the structure of the conversations in which this service may participate. You can define this contract yourself or obtain scripts to create the contract from the creator of the service that your service will communicate with. You skip this step when the database already contains the contracts that your service needs.

3. Create a queue. Service Broker uses this queue to receive and store incoming messages for the service. For more information on creating queues, see CREATE QUEUE. Every service must have a queue. To make programming and administration more straightforward, each service generally uses a queue dedicated to that service. If your service requires message retention, specify message retention for the queue.

4. Create a service. The service definition specifies the queue that the service uses and the contracts for which this service is the target.

In most cases, you create the target service and then use the contracts and message types created for the target service to create an initiating service. In some cases, however, you may create a target service for an initiating service that is already defined. In these cases, the target service uses the message types and contracts that the initiating service uses. For example, if you are creating a target service to receive event notifications, you use the contract `https://schemas.microsoft.com/SQL/Notifications/PostEventNotification`, because this is the contract that the initiating service uses.

## Managing Object Definitions

It is recommended that you create a Transact-SQL script for the Service Broker objects that your application uses. This Transact-SQL script makes it easy to refer to the specifics of your Service Broker objects. The script also provides a way to deploy the service on a different system or to re-create the service if necessary.

If your application involves sending messages between SQL Server instances, it is recommended that you create one script that defines the message types and contracts for the service, and a second script that defines the queue and the service. The first script defines the interface for the service, the objects that are common to both the initiating service and the target service. The second script defines the service name and the queue, the objects for one side of the conversation.

## In This Section

- [Creating Service Broker Message Types](creating-service-broker-message-types.md)  
    Describes message types and how they are used.

- [Creating Service Broker Contracts](creating-service-broker-contracts.md)  
    Describes contracts and how they are used.

- [Creating Service Broker Queues](creating-service-broker-queues.md)  
    Describes queues and how they are used.

- [Creating Service Broker Services](creating-service-broker-services.md)  
    Describes services and how they are used.

- [Naming Service Broker Objects](naming-service-broker-objects.md)  
    Describes considerations for naming service broker objects.

- [Service Script Example](service-script-example.md)  
    Describes a Transact-SQL code sample and defines a service.

## See also

- [Contracts](contracts.md)
- [Queues](queues.md)
- [Message Types](message-types.md)
- [Service Broker Routing](service-broker-routing.md)
