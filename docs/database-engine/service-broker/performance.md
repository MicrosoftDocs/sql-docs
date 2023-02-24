---
title: Performance (Service Broker)
description: "The performance of a Service Broker application is generally determined by two factors."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Performance (Service Broker)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

The performance of a Service Broker application is generally determined by two factors:

- The number of messages arriving within a specified period of time.

- The speed with which the application processes each message.

Monitoring these two factors is the key to understanding the performance of the application.

Service Broker provides a set of performance counters that provide information on its activities. Service Broker also logs serious errors to the SQL Server error log and the Windows application event log. For more information, see the following articles:

- [Service Broker Related Dynamic Management Views (Transact-SQL)](../../relational-databases/system-dynamic-management-views/service-broker-related-dynamic-management-views-transact-sql.md)
- [SQL Server, Broker Statistics object](../../relational-databases/performance-monitor/sql-server-broker-statistics-object.md)
- [Broker Event Category](../../relational-databases/event-classes/broker-event-category.md)## Tuning a Service Broker Stored Procedure
For the most part, tuning a stored procedure that uses Service Broker is no different from tuning any other stored procedure. However, there are a few additional considerations.

First, use the WAITFOR clause. Messages seldom arrive at predictable intervals. Even in a service where messages arrive at roughly the same rate that the stored procedure processes the messages, there may be times when no messages are available. Therefore, the procedure should use a WAITFOR clause with a RECEIVE statement or with a GET CONVERSATION GROUP statement. Without WAITFOR, these statements return immediately when there are no available messages on the queue. Depending on the implementation of the stored procedure, the procedure may then loop back through the statement, consuming resources needlessly, or the procedure may exit only to be reactivated shortly thereafter, consuming more resources than simply continuing to run.

You allow for the unpredictability in timing by using the WAITFOR clause with the RECEIVE or GET CONVERSATION GROUP statement. If your application runs continuously as a background service, you do not specify a time-out in the WAITFOR statement. If your application is activated by Service Broker, or runs as a scheduled job, you specify a short time-out, for example, 500 milliseconds. An application that uses the WAITFOR statement gracefully handles unpredictable intervals between messages. Likewise, an activated application that exits after a short time-out does not consume resources when there are no messages to process.

Service Broker guarantees that only one instance of an application at a time can receive messages for conversations that share a conversation group identifier. Design your applications to take advantage of conversation group locking for synchronization. If your application maintains state, consider using the conversation group identifier to identify the state for the conversation. Process multiple messages for a conversation group in the same transaction. In general, however, only process messages for a single conversation group in a given transaction. This helps ensure that more than one instance of the application can process messages, even when the number of conversation groups is relatively small.

In addition, avoid using message retention. Maintaining a separate log table that saves the most important information from a message improves performance. Use message retention only in the event that your application requires the exact messages sent and received.

Next, end conversations when the task completes. Service Broker maintains state for each active conversation. Although the amount of state for a particular conversation is small, an application that does not end conversations may suffer reduced performance over time.

Finally, keep transactions short. For example, if the conversation pattern for the service involves a large number of messages on the same conversation group, limiting the number of messages processed in each transaction may improve overall throughput.

## See also

[Conversation Group Locks](conversation-group-locks.md)

[Message Retention](message-retention.md)
