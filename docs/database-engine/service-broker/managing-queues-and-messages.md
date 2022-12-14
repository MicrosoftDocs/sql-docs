---
title: Managing Queues and Messages
description: "When a Service Broker application is in production, most day-to-day management occurs as a normal part of maintenance of the Database Engine."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Managing Queues and Messages

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

When a Service Broker application is in production, most day-to-day management occurs as a normal part of maintenance of the Database Engine. Service Broker provides performance counters and event notifications to monitor a service. However, you might have to work directly with a message queue or with the messages in a queue. This might be necessary to troubleshoot a service or to collect information about the traffic that is received by a queue.

## In This Section

- [Starting and Stopping the Queue](starting-and-stopping-the-queue.md)  
    Describes how to start and stop a queue.

- [Querying Queues](querying-queues.md)  
    Describes the data that a queue contains, and the process for running queries against a queue.

- [Removing Poison Messages](removing-poison-messages.md)  
    Describes how to handle messages that cannot be processed by the service.

- [Managing Conversation Priorities](managing-conversation-priorities.md)  
    Describes how to enable, specify, and query conversation priorities.

## See also

- [Managing Service Broker Applications](managing-service-broker-applications.md)