---
title: Understanding When Activation Occurs
description: "The Service Broker activation process consists of two steps. First, Service Broker determines whether activation is necessary."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Understanding When Activation Occurs

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

The Service Broker activation process consists of two steps. First, Service Broker determines whether activation is necessary. Second, Service Broker determines whether activation occurs. Although the exact process is different for internal activation and external activation, the overall concepts involved are the same for either strategy.

## Determining Whether Activation Is Necessary

Activation is necessary whenever a new queue reader would have useful work to perform. Queue monitors determine whether activation is necessary. Service Broker creates a queue monitor for each queue with activation STATUS = ON or for which a QUEUE_ACTIVATION event notification has been registered. The dynamic management view [sys.dm_broker_queue_monitors (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-broker-queue-monitors-transact-sql.md) lists the queue monitors active in the instance. Each queue monitor tracks the following:

Whether the queue contains messages that are ready for receive

How recently a RECEIVE statement on the queue returned an empty result set

How many activation stored procedures are currently running for the queue.

A queue monitor checks whether activation is necessary every few seconds and when one or more of the following events occurs:

- A new message arrives on the queue.

- SQL Server executes a RECEIVE statement for the queue.

- A transaction that contains a RECEIVE statement rolls back.

- All stored procedures started by the queue monitor exit.

- SQL Server executes an ALTER statement for the queue.

Activation is necessary if either of the following is true:

- A new message arrives on a queue that contains no unread messages and there are no activation stored procedures running for the queue.

- The queue contains unread messages, there is no session waiting in a GET CONVERSATION GROUP statement or a RECEIVE statement without a WHERE clause, and no GET CONVERSATION GROUP statement or RECEIVE statement without a WHERE clause has returned an empty result set for a few seconds. In other words when messages are accumulating on the queue because the activated procedures aren't able to read them fast enough.

In effect, this procedure allows the queue monitor to tell whether the number of queue readers processing the queue is keeping up with the incoming message traffic. Notice that this approach takes conversation group locking into account. Because only one queue reader at a time can process messages for a conversation, starting queue readers in response to a simpler approach, such as the number of unread messages in the queue, might waste resources. Instead, Service Broker activation considers whether a new queue reader will have useful work to do.

For example, a queue may contain a large number of unprocessed messages on a single conversation. In this case, only one queue reader can process the messages. The queue monitor activates another queue reader. The second queue reader waits in the RECEIVE statement, since all of the messages belong to a single conversation. As long as all the messages in the queue belong to the same conversation, and the second queue reader remains running, the queue monitor does not start another queue reader.

## Determining Whether Activation Occurs

Once Service Broker determines that activation is necessary, Service Broker must decide whether activation occurs.

For internal activation, the queue monitor activates a new instance of the activation stored procedure when the number of running programs is lower than the MAX_QUEUE_READERS value set for the queue. If the number of running programs is equal to or greater than the MAX_QUEUE_READERS value, the queue monitor does not start a new instance of the stored procedure. The management view [sys.dm_broker_activated_tasks (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-broker-activated-tasks-transact-sql.md) contains information on stored procedures started by Service Broker.

For external applications, Service Broker has no information on the number of distinct queue readers that may be working with the queue. Further, there may be some start up time required between the time that the activation event is raised and the time that a reader begins reading the queue. Therefore, Service Broker provides a time-out for an external application to respond. During the time-out, Service Broker will not produce another notification. Once an application calls RECEIVE on the queue or the time-out expires, Service Broker creates another event notification if activation is required. An external application monitors the event notifications while the program is running to determine whether more queue readers are required to read events.

## See also

- [Troubleshooting Activation Stored Procedures](troubleshooting-activation-stored-procedures.md)
- [CREATE QUEUE (Transact-SQL)](../../t-sql/statements/create-queue-transact-sql.md)
- [Implementing Internal Activation](implementing-internal-activation.md)
