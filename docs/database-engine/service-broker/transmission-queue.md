---
title: Transmission Queue
description: "Service Broker uses a transmission queue as a holding area for messages."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Transmission Queue

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker uses a transmission queue as a holding area for messages. Each database contains a separate transmission queue. The transmission queue is stored in the primary filegroup for the database.

## Outgoing Messages

Outgoing messages are added to the transmission queue in the database that sends the message. The message remains in the transmission queue until the destination has acknowledged receipt of the message.

## Incoming Messages

Incoming messages are generally directly added to the queue for the destination service. However, if the destination queue is OFF, incoming messages are held in the transmission queue for the database that contains the destination queue.

## Single Instance Optimization

In cases where both sides of the conversation are in the same instance, Service Broker may optimize message delivery by placing the message directly on the destination queue.

If Service Broker message delivery is not active in the database that sends the message, or the destination database is unavailable, messages remain in the transmission queue for the database that sent the message.

If the destination database is available, but the destination queue is unavailable, the message is held in the transmission queue for the database that contains the destination service.

## See also

- [sys.transmission_queue (Transact-SQL)](../../relational-databases/system-catalog-views/sys-transmission-queue-transact-sql.md)
- [Conversation Architecture](conversation-architecture.md)
- [Service Broker Communication Protocols](service-broker-communication-protocols.md)