---
title: Service Broker Activation
description: "Service Broker activation helps applications to scale dynamically to match the message traffic."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Service Broker Activation

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker activation helps applications to scale dynamically to match the message traffic. In general, an application uses activation if traffic to the service varies unpredictably or if the service must dynamically scale to match the traffic the service receives.

Activation uses Service Broker to start an application when there is work for the program to do.

There are two distinct types of activation: internal activation and external activation. Internal activation works with SQL Server stored procedures. In this case, Service Broker directly activates the stored procedure. External activation works with programs that run independently of SQL Server. For external activation, Service Broker produces a SQL Server event indicating that the external program should start another queue reader.

Not all Service Broker applications use activation. If an application requires substantial resources during startup, or if response time for infrequent messages is paramount, the application might be better designed to start when SQL Server starts and remain running. For tasks that are better performed at certain times, it might be better to design the application to run as a scheduled job. For more information about choosing a strategy to start an application that uses Service Broker, see [Choosing a Startup Strategy](choosing-a-startup-strategy.md).

## In This Section

- [Understanding When Activation Occurs](understanding-when-activation-occurs.md)  
    Describes the two steps of the Service Broker activation process.

- [Internal Activation Context](internal-activation-context.md)  
    Describes the execution context for a stored procedure that is started by internal activation.

- [Event-Based Activation](event-based-activation.md)  
    Describes the event and strategies for receiving and responding to the event

## See also

- [sys.dm_broker_activated_tasks (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-broker-activated-tasks-transact-sql.md)
- [sys.dm_broker_queue_monitors (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-broker-queue-monitors-transact-sql.md)
- [Implementing Internal Activation](implementing-internal-activation.md)