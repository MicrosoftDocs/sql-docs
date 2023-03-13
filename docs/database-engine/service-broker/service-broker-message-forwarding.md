---
title: Service Broker Message Forwarding
description: "Service Broker message forwarding allows an instance of SQL Server to accept messages from outside the instance and send those messages to a different instance."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Service Broker Message Forwarding

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker message forwarding allows an instance of SQL Server to accept messages from outside the instance and send those messages to a different instance.

An administrator can use message forwarding to:

- Provide connectivity between servers in different trust domains

- Simplify administration by creating a single centralized instance that holds the routing information for a domain

- Distribute work among several instances

When forwarding is enabled, the routing table in **msdb.sys.routes** determines whether a message that arrives from another instance is forwarded. If the address for the matching route is not LOCAL, SQL Server forward the message to the address specified. Otherwise, the message is delivered locally.

Each Service Broker message contains a maximum lifetime and a count of the number of times that the message has been forwarded. When an instance forward the message, that instance increases the count in the message. If the message exceeds the maximum lifetime, the forwarding instance discards the message. This strategy helps avoid problems in situations where a routing loop may exist.

## Forwarding and Reliable Delivery

An instance that forward a message does not acknowledge the message to the sender. Only the final destination acknowledges the message. If the sender does not receive an acknowledgment from the destination after a period of time, the sender retries the message.

An instance that performs message forwarding does not need to store forwarded messages. Instead, SQL Server holds messages to be forwarded in memory. The amount of memory available for message forwarding is specified as part of the Service Broker endpoint configuration. This strategy allows efficient, stateless message forwarding. In the event that an instance that performs message forwarding fails, no messages are lost. Each message is always maintained at the sender until the final destination acknowledges the message, as described in [Service Broker Communication Protocols](service-broker-communication-protocols.md).

The management view **sys.dm_broker_forwarded_messages** contains information about messages that are currently in the process of being forwarded by the instance. An instance does not persist messages in the process of being forwarded; these messages exist only in memory. The instance that sent the message and the instance that receives the message persist the messages. The sending instance does not remove the message until the receiving instance acknowledges receipt of the message.

## Security and Forwarding

Service Broker message forwarding does not require a forwarding instance to decrypt the forwarded message. Therefore, only the databases that participate in the conversation must have dialog security configured.

However, because transport security applies to the connections between SQL Server instances each SQL Server instance must have transport security correctly configured for the instances that it communicates with directly. For example, if Instance A and Instance B communicate through a forwarding instance, then both Instance A and Instance B must have transport security correctly configured for the forwarding instance. Because the instances do not exchange messages directly, the instances should not have transport security configured to communicate with each other.

## See also

- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
- [ALTER ENDPOINT (Transact-SQL)](../../t-sql/statements/alter-endpoint-transact-sql.md)
- [sys.dm_broker_forwarded_messages (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-broker-forwarded-messages-transact-sql.md)
- [sys.routes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-routes-transact-sql.md)
- [sys.transmission_queue (Transact-SQL)](../../relational-databases/system-catalog-views/sys-transmission-queue-transact-sql.md)
- [Service Broker Routing and Networking](service-broker-routing-and-networking.md)
- [Service Broker Endpoints](service-broker-endpoints.md)
- [Service Broker Dialog Security](service-broker-dialog-security.md)
- [Service Broker Transport Security](service-broker-transport-security.md)
