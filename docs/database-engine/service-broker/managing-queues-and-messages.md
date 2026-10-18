---
title: Manage Queues and Messages
description: "When a Service Broker application is in production, most day-to-day management occurs as a normal part of maintenance of the Database Engine."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: reference
---

# Manage queues and messages

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

When a Service Broker application is in production, most day-to-day management occurs as a normal part of maintenance of the Database Engine. Service Broker provides performance counters and event notifications to monitor a service. However, you might have to work directly with a message queue or with the messages in a queue. This might be necessary to troubleshoot a service or to collect information about the traffic that is received by a queue.

## In this section

| Article | Description |
| --- | --- |
| [Start and stop the Service Broker queue](starting-and-stopping-the-queue.md) | Describes how to start and stop a queue. |
| [Query queues](querying-queues.md) | Describes the data that a queue contains, and the process for running queries against a queue. |
| [Remove poison messages](removing-poison-messages.md) | Describes how to handle messages that can't be processed by the service. |
| [Manage conversation priorities](managing-conversation-priorities.md) | Describes how to enable, specify, and query conversation priorities. |

## Related content

- [Manage Service Broker applications](managing-service-broker-applications.md)
