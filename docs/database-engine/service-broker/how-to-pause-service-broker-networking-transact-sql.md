---
title: "How To: Pause Service Broker Networking (Transact-SQL)"
description: "Service Broker sends and receives messages over the network while any endpoint for Service Broker is in the STARTED state."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/02/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# How to: Pause Service Broker networking (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker sends and receives messages over the network while any endpoint for Service Broker is in the `STARTED` state. To pause Service Broker networking, alter all Service Broker endpoints to set the state to `STOPPED`. This state prevents Service Broker from transmitting messages out of the instance or receiving messages from outside of the instance, but doesn't affect message delivery within the instance. To prevent message delivery to a specific database, use `ALTER DATABASE` to deactivate Service Broker in that database.

## Pause Service Broker networking

- Alter all Service Broker endpoints to set the state to `STOPPED`.

## Examples

```sql
USE master;
GO

ALTER ENDPOINT BrokerEndpoint
    STATE = STOPPED;
GO
```

## Related content

- [How to: Deactivate Service Broker networking (Transact-SQL)](how-to-deactivate-service-broker-networking-transact-sql.md)
- [How to: Resume Service Broker networking (Transact-SQL)](how-to-resume-service-broker-networking-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [ALTER ENDPOINT (Transact-SQL)](../../t-sql/statements/alter-endpoint-transact-sql.md)
- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
