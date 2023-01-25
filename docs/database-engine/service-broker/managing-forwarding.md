---
title: Managing Forwarding (Service Broker)
description: "Message forwarding allows a SQL Server instance to forward Service Broker messages between two or more other instances of SQL Server."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Managing Forwarding (Service Broker)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Message forwarding allows a SQL Server instance to forward Service Broker messages between two or more other instances of SQL Server. Several considerations apply to management of a SQL Server instance that performs message forwarding.

Service Broker uses the routes in the **msdb** database for both forwarded messages and incoming messages. After you make changes to the routing configuration for forwarding, you must back up **msdb**.

SQL Server stores messages to be forwarded in memory, in a data structure called the transmitter queue. The endpoint option **MESSAGE_FORWARDING_SIZE** sets the maximum amount of memory (in megabytes) that SQL Server uses for storing messages to be forwarded. SQL Server allocates memory as necessary to hold messages to be forwarded, up to this limit. If a message arrives that would cause the size of the transmitter queue to exceed this limit, SQL Server drops the message. However, if a large message has been fragmented, the forwarding instance does not reassemble the fragments, but instead forward the message fragments to the destination. In this manner, a forwarding instance can successfully forward a message that is larger than the **MESSAGE_FORWARDING_SIZE** option that is configured for the instance.

An instance that performs message forwarding often functions as a bridge between two networks. For this configuration, the **MESSAGE_FORWARDING_SIZE** option for the Service Broker endpoint may need to be relatively large, since all traffic between the two networks passes through the instance.

The dynamic management view **sys.dm_broker_forwarded_messages** shows the messages that are stored for forwarding.

## See also

- [ALTER ENDPOINT (Transact-SQL)](../../t-sql/statements/alter-endpoint-transact-sql.md)
- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
- [sys.dm_broker_forwarded_messages (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-broker-forwarded-messages-transact-sql.md)
- [Service Broker Endpoints](service-broker-endpoints.md)
- [Service Broker Message Forwarding](service-broker-message-forwarding.md)
