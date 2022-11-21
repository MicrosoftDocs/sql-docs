---
title: Creating Service Broker Queues
description: "A queue holds incoming messages for a service."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Creating Service Broker Queues

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

A queue holds incoming messages for a service. To simplify processing, applications typically create one queue per service instead of using the same queue for multiple services.

Setting the retention option for a queue cause messages to be retained after they are processed. Because retention reduces application performance, specify retention only if the application requires persistent access to the exact messages sent and received. For more information about message retention, see [Message Retention](message-retention.md).

For applications that do not use internal activation, do not specify an activation clause on the queue definition.

For applications that use internal activation, the queue definition includes the name of the stored procedure, the maximum number of readers for SQL Server to start, and the name of the database principal to impersonate before starting the stored procedure.

The name of a queue is not included in the network format of a message. Queues are schema-owned objects. Therefore, queue names follow SQL Server naming conventions. For more information about naming, see [Naming Service Broker Objects](naming-service-broker-objects.md).

## Stored Procedure Activation

A queue can be associated with a stored procedure. SQL Server activates the stored procedure when there are messages in the queue to be processed. This process of automatic activation allows a Service Broker application to scale dynamically according to the current processing load on the application. Each stored procedure activated by Service Broker runs in a separate thread. When a queue specifies a stored procedure, Service Broker starts new instances of the stored procedure as required, up to the maximum number of instances specified for the queue.

An activated stored procedure typically processes one or more messages, and returns a response to the service that originated the messages. When messages arrive faster than the stored procedure processes messages, Service Broker starts another instance of the stored procedure, up to the maximum number defined by the queue. An activated stored procedure typically exits when the procedure finds no messages available on the queue for a while.

Using activation stored procedures is a common way to design Service Broker applications. However, other designs might better suit the needs of a specific application. Any application that can run Transact-SQL batches in SQL Server can send and receive messages. Messages can be also be processed by any stored procedure, regardless of whether the stored procedure is activated by SQL Server, started by SQL Server Agent, run by an external application, or run interactively from a tool such as SQL Server Management Studio or SQL Server Express Management Studio.

## See also

- [CREATE QUEUE (Transact-SQL)](../../t-sql/statements/create-queue-transact-sql.md)
- [Conversation Architecture](conversation-architecture.md)
- [Queues](queues.md)
- [Understanding When Activation Occurs](understanding-when-activation-occurs.md)