---
title: Event-Based Activation
description: "SQL Server provides a queue activation event to notify external applications when a queue has messages to process."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 08/29/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# Event-based activation

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

SQL Server provides a queue activation event to notify external applications when a queue has messages to process. This topic describes the event and strategies for receiving and responding to the event.

## Queue activation event

SQL Server includes a QUEUE_ACTIVATION event. This event reports that there's useful work for another queue reader. The activation event contains the name of the queue and the name of the database and schema that contain the queue. An external program can use this information to start the correct program to read from the queue.

SQL Server can't track the capacity or the number of external processes that are reading from the queue. Therefore, SQL Server produces queue activation events periodically for as long as activation is required.

## Monitor activation events from an external application

An external application that uses event-based activation typically creates an event notification on the queue that receives messages for the service. The external application creates a service and queue for receiving the activation messages, then monitors that queue for messages that report QUEUE_ACTIVATION events.

This strategy allows the external application to use the activation logic that's built in to Service Broker to determine when there's more work for a queue reader. Further, it's possible for one external application to monitor activation for a number of queues and start the appropriate program when activation is required.

## Related content

- [CREATE EVENT NOTIFICATION (Transact-SQL)](../../t-sql/statements/create-event-notification-transact-sql.md)
- [Event notifications](../../relational-databases/service-broker/event-notifications.md)
