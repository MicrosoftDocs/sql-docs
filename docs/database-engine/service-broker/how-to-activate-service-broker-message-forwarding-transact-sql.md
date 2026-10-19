---
title: "How To: Activate Service Broker Message Forwarding (Transact-SQL)"
description: "Message forwarding allows an instance of SQL Server to accept messages from outside the instance and send those messages to a different instance. Message forwarding is configured on a Service Broker endpoint."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 08/29/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# How to: Activate Service Broker message forwarding (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Message forwarding allows an instance of SQL Server to accept messages from outside the instance and send those messages to a different instance. Message forwarding is configured on a Service Broker endpoint.

## Activate Service Broker message forwarding

1. Activate Service Broker networking if networking isn't already active. For more information on Service Broker networking, see [How to: Activate Service Broker networking](how-to-activate-service-broker-networking-transact-sql.md).

1. Alter the endpoint to activate message forwarding, and specify the maximum size, in megabytes, for forwarded messages.

## Examples

```sql
USE master;
GO

ALTER ENDPOINT BrokerEndpoint FOR SERVICE_BROKER
(
    MESSAGE_FORWARDING = ENABLED,
    MESSAGE_FORWARD_SIZE = 10
);
GO
```

## Related content

- [How to: Activate Service Broker networking (Transact-SQL)](how-to-activate-service-broker-networking-transact-sql.md)
- [How to: Deactivate Service Broker message forwarding (Transact-SQL)](how-to-deactivate-service-broker-message-forwarding-transact-sql.md)
