---
title: Queues
description: "Queues store messages. When Service Broker receives a message for a service, Service Broker inserts the message into the queue for that service."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# Queues

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Queues store messages. When Service Broker receives a message for a service, Service Broker inserts the message into the queue for that service. To get messages sent to the service, an application receives messages from the queue. Service Broker manages queues and presents a view of a queue that is similar to a table.

Each service is associated with one queue. When a message arrives for a service, Service Broker places the message in the queue associated with that service.

Each message is a row in the queue. The row contains the content of the message as well as information about the message type, the service targeted by the message, the contract that the message follows, the validation performed on the message, the conversation that the message is a part of, and information internal to the queue. An application uses the information in the message row to identify each message uniquely and process the message appropriately.

Applications receive messages from the queue for the service. For each conversation, queues return messages in the order in which the sender sent the message. All the messages returned from a single receive operation are part of conversations that belong to one conversation group. In effect, a queue holds sets of related messages, one set for each conversation group. The queue returns one set of related messages each time the application performs a receive operation from the queue. The application can choose to receive messages for a specific conversation or a specific conversation group. Queues don't return messages in strict first-in-first-out order; instead, queues return messages for each conversation in the order in which the messages were sent. Therefore, an application doesn't need to include code to recover the original order of the messages.

A queue might be associated with a stored procedure. In this case, SQL Server activates the stored procedure when there are messages in the queue to be processed. SQL Server can start more than one instance of the stored procedure, up to a configured maximum. For more information, see [Service Broker activation](service-broker-activation.md).

## Related content

- [CREATE QUEUE (Transact-SQL)](../../t-sql/statements/create-queue-transact-sql.md)
- [ALTER QUEUE (Transact-SQL)](../../t-sql/statements/alter-queue-transact-sql.md)
- [DROP QUEUE (Transact-SQL)](../../t-sql/statements/drop-queue-transact-sql.md)
- [SEND (Transact-SQL)](../../t-sql/statements/send-transact-sql.md)
- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
- [Service Broker activation](service-broker-activation.md)
- [Choose a startup strategy](choosing-a-startup-strategy.md)
- [Understand when activation occurs](understanding-when-activation-occurs.md)
- [Create Service Broker queues](creating-service-broker-queues.md)
- [Benefits of programming with Service Broker](benefits-of-programming-with-service-broker.md)
