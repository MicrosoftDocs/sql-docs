---
title: Transmission Queue
description: "Service Broker uses a transmission queue as a holding area for messages."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# Transmission queue

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker uses a transmission queue as a holding area for messages. Each database contains a separate transmission queue. The transmission queue is stored in the primary filegroup for the database.

## Outgoing messages

Outgoing messages are added to the transmission queue in the database that sends the message. The message remains in the transmission queue until the destination has acknowledged receipt of the message.

## Incoming messages

Incoming messages are generally directly added to the queue for the destination service. However, if the destination queue is `OFF`, incoming messages are held in the transmission queue for the database that contains the destination queue.

## Single-instance optimization

In cases where both sides of the conversation are in the same instance, Service Broker might optimize message delivery by placing the message directly on the destination queue.

If Service Broker message delivery isn't active in the database that sends the message, or the destination database is unavailable, messages remain in the transmission queue for the database that sent the message.

If the destination database is available, but the destination queue is unavailable, the message is held in the transmission queue for the database that contains the destination service.

## Related content

- [sys.transmission_queue (Transact-SQL)](../../relational-databases/system-catalog-views/sys-transmission-queue-transact-sql.md)
- [Conversation architecture](conversation-architecture.md)
- [Service Broker communication protocols](service-broker-communication-protocols.md)
