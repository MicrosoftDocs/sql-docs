---
title: Message Retention
description: "When a queue specifies message retention, Service Broker does not delete messages from the queue until the conversation ends."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Message Retention

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

When a queue specifies message retention, Service Broker does not delete messages from the queue until the conversation ends. Further, Service Broker also copies outgoing messages to the queue. This allows the service to maintain a precise record of the incoming and outgoing messages.

Message retention allows you to maintain an exact record of a conversation for a queue while the conversation is active. For applications that require detailed auditing, or that must perform compensating transactions when the conversation fails, this can be more convenient than copying each message to a state table while the conversation is in progress.

Message retention increases the number of messages in the queue for active conversations and increases the amount of work that SQL Server performs when sending a message. Therefore, message retention reduces performance. The exact performance impact depends on the communication patterns for the services that use the queue. In general, you should use message retention any time that message retention is required for an application to operate correctly. If the application does not require an exact record of all sent and received messages while the conversation is active, maintaining state in a state table may improve performance. Also remember that when the conversation ends, the retained messages are removed from the queue so if you are using retention for auditing purposes, you must be sure to copy the messages to permanent storage before ending the conversation.

> [!NOTE]
> Using message retention can reduce performance. This setting should only be used if the application service-level agreement requires that the application retain the exact messages sent and received.Messages in a queue that are ready to be received have a status of 1. The RECEIVE statement returns messages that show a status of 1. After the RECEIVE statement returns a message, it sets the status to 0 and leaves the message in the queue if message retention is on. If message retention is off, the RECEIVE statement deletes the message from the queue. Any service that uses the queue saves both incoming and outgoing messages. In this case, the SEND command copies messages to the queue for the service (with a **status** of **3**) as well as adding the message to the transmission queue. When the conversation ends, the queue deletes all of the messages for the conversation.

An application cannot receive the same message twice, and an application cannot receive a message that was added to the queue as an outgoing message. To work with retained messages, you use a SELECT statement to query the queue. For auditing, an application inserts the retained messages into an audit table before ending the conversation. For compensating transactions, an application typically works backward through the processed messages and undoes the work for each message in turn, until all of the messages have been processed.

For more information on using SELECT statements to access a queue, see [Querying Queues](querying-queues.md).

## See also

- [ALTER QUEUE (Transact-SQL)](../../t-sql/statements/alter-queue-transact-sql.md)
- [CREATE QUEUE (Transact-SQL)](../../t-sql/statements/create-queue-transact-sql.md)
- [Querying Queues](querying-queues.md)