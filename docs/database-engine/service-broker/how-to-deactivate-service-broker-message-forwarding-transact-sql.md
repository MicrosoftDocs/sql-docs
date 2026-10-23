---
title: "How To: Deactivate Service Broker Message Forwarding (Transact-SQL)"
description: "Message forwarding allows an instance of SQL Server to accept messages from outside the instance and send those messages to a different instance."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/02/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# How to: Deactivate Service Broker message forwarding (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Message forwarding allows an instance of SQL Server to accept messages from outside the instance and send those messages to a different instance. Message forwarding is configured on a Service Broker endpoint.

## Deactivate Service Broker message forwarding

- Alter the endpoint to deactivate message forwarding.

## Examples

```sql
USE master;
GO

ALTER ENDPOINT BrokerEndpoint
    FOR SERVICE_BROKER (MESSAGE_FORWARDING = DISABLED);
GO
```

## Related content

- [How to: Activate Service Broker networking (Transact-SQL)](how-to-activate-service-broker-networking-transact-sql.md)
- [How to: Activate Service Broker message forwarding (Transact-SQL)](how-to-activate-service-broker-message-forwarding-transact-sql.md)
