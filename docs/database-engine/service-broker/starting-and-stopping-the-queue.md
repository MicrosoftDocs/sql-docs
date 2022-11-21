---
title: Starting and Stopping the Queue
description: "Use the ALTER QUEUE statement to start and stop a queue."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Starting and Stopping the Queue

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Use the ALTER QUEUE statement to start and stop a queue. When a queue is started, the queue is available to send and receive messages. When a queue is stopped, Service Broker does not deliver new messages to the queue, and does not allow applications to receive messages from the queue.

Administrators often create queues in an unavailable state. This prevents Service Broker from placing messages on the queue until all of the objects for a service are created. When the service is ready to receive messages, the administrator makes the queue available with the following statement:

```sql
    ALTER QUEUE dbo.ExpenseQueue WITH STATUS = ON ;
```

The following statement makes the queue unavailable for receive:

```sql
    ALTER QUEUE dbo.ExpenseQueue WITH STATUS = OFF ;
```

When a queue is unavailable for receive, an application cannot process the messages in the queue. Because Service Broker guarantees that a message only leaves the queue as part of a receive operation, there is no way to remove messages from a stopped queue. Service Broker is designed to allow maintenance while the queue continues to receive messages, so it is rarely necessary to stop a queue once the queue is made available. Because most activation stored procedures exit when a RECEIVE operation fails or when no more messages are available, one reason for stopping the queue is to replace an activation stored procedure for a service that receives messages constantly.

When a message arrives for a queue that is unavailable, Service Broker holds the message in the transmission queue for the database, rather than delivering the message to the queue.

## See also

- [ALTER QUEUE (Transact-SQL)](../../t-sql/statements/alter-queue-transact-sql.md)
