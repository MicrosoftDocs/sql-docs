---
title: Stop and Start Service Broker Applications
description: "Sometimes it may be necessary to temporarily stop a Service Broker application."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Stop and start Service Broker applications

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Sometimes it might be necessary to temporarily stop a Service Broker application. For example, you might stop a busy application to install an updated version of the activation stored procedure for the queue. Although you can alter the queue without stopping the application, the copies of the activation stored procedure that are currently running might never run out of messages to process, and therefore might never exit. In this case, stopping the queue causes the running stored procedures to exit immediately. When you start the service again, the Service Broker starts the updated activation stored procedure.

The information in this section applies to stopping an application temporarily for maintenance purposes. For information about permanently removing a service from the database, see [Uninstall Service Broker applications](uninstalling-service-broker-applications.md).

To temporarily stop a service, alter the queue that the application uses so that the queue status is `OFF`. When the queue status is `OFF`, the queue is unavailable for receive operations, which effectively stops the application. Service Broker doesn't deliver new messages to the queue and doesn't allow an application to receive messages from the queue. When an application attempts to receive a message from a queue that's unavailable, the application receives a Transact-SQL error. An application can call `END CONVERSATION` on a conversation at any time, regardless of whether the queue that the conversation uses is available or not. However, Service Broker doesn't deliver the End Dialog message to the remote service until the queue becomes available.

When a message arrives for a stopped queue, Service Broker holds the message in the transmission queue for the database until the destination queue becomes available. Service Broker doesn't consider a message arriving for a stopped queue to be an error, and doesn't notify the sender. When the queue becomes available, Service Broker delivers the messages in the transmission queue to the service queue. This delivery uses the normal retry logic for messages. Messages are marked delayed in the transmission queue, and retried periodically. Service Broker holds the message in the transmission queue and delays the message regardless of whether the message originates in the same instance as the queue or arrives from the network.

Stopping a queue doesn't reset the conversation timer or the dialog lifetime timer for messages in the queue. If either timer expires while the queue is stopped, Service Broker generates the appropriate messages when the queue starts again.

To restart the application, alter the queue that the application uses so that the queue status is `ON`. This starts the queue. When the queue has an activation stored procedure specified and the queue contains messages, Service Broker immediately starts the activation stored procedure when the queue starts. Because the queue is now available, Service Broker also generates messages for conversation timers and dialog lifetime timers that expired while the queue was stopped. For an application that doesn't use activation, restart the application using the startup procedure defined for that application.

## Related content

- [ALTER QUEUE (Transact-SQL)](../../t-sql/statements/alter-queue-transact-sql.md)
- [Start and stop the Service Broker queue](starting-and-stopping-the-queue.md)
- [Uninstall Service Broker applications](uninstalling-service-broker-applications.md)
