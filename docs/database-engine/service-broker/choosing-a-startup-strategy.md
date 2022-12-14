---
title: Choosing a Startup Strategy
description: "This topic describes options for Service Broker activation."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Choosing a Startup Strategy

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This topic describes options for Service Broker activation.

Service Broker supports asynchronous, queued messaging. Because conversations can last for days, months, or years, many applications use activation to scale dynamically. This section describes some common strategies for starting an application that uses Service Broker.

## Startup Strategies

The strategies for starting an application fall into four broad categories:

- Internal activation

- Event-based activation

- Scheduled task

- Startup task

Each activation strategy has different advantages. An application can combine these strategies. For example, an application can use internal activation with a small number of queue readers most of the time. But, at certain times of the day, it can start more queue readers.

## Internal Activation

With Service Broker internal activation, a Service Broker queue monitor directly activates a stored procedure when it is necessary. This is often the most straightforward approach. By using direct activation of a stored procedure, you do not have to write additional code in the application to manage activation. However, internal activation requires that the application be written as a SQL Server stored procedure. When using internal activation, you write the application to exit when there are no more messages to process.

## Event-Based Activation

Some applications run in response to a specific event. For example, you can run an application when the CPU usage on the computer falls below a certain level. Or, you can run a logging application when a new table is created.

Service Broker external activation is a special case of event-based activation. For external activation, the application starts in response to the QUEUE_ACTIVATION event.

For events that can be triggered by event notifications, event-based activation can be combined with Service Broker internal activation. In this case, you use internal activation on the queue that receives the event notification. The activation stored procedure receives the notification message and starts the application.

For other events, you can use SQL Server Agent to start jobs on the same computer as the computer on which SQL Server runs. You can write an application that monitors Windows Management Instrumentation (WMI) events from a remote computer. The application can start a task when a WMI event occurs on the computer that runs SQL Server.

When using event-based activation, an application typically exits when there are no more messages to process.

## Scheduled Task

With a scheduled task, an application is activated on a set schedule. This strategy is convenient for batch-processing applications. An application that runs as a scheduled task can exit when there are no more messages to process, or the program can exit at a certain time.

For example, an application that processes orders to a supplier can store messages during the day and then process the messages overnight to produce a single order to the supplier. In this case, the application can use a SQL Server Agent job to start the application at a specific time each night.

## Startup Task

Some applications start one time, typically when the computer starts or when SQL Server starts. Examples of these tasks are a startup stored procedure in SQL Server, an application in the Windows startup group, or a Windows service. In this case, the application remains running and processes messages as they arrive. An application that runs continuously does not require startup time when a message arrives on the queue. However, because the application does not exit when there are no messages, the program consumes resources even when there is no work for the program to do.

This strategy can be useful for an application that processes a constant stream of messages and that is relatively resource-intensive during startup.

## See also

- [Create a Job](../../ssms/agent/create-a-job.md)
- [Service Broker Activation](service-broker-activation.md)
- [Implementing Internal Activation](implementing-internal-activation.md)