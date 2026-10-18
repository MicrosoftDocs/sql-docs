---
title: "How To: Deactivate Service Broker Networking (Transact-SQL)"
description: "Service Broker sends and receives messages over the network while any Service Broker endpoint is in the STARTED state."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/02/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# How to: Deactivate Service Broker networking (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker sends and receives messages over the network while any Service Broker endpoint is in the `STARTED` state. To deactivate Service Broker networking, drop all Service Broker endpoints.

## Deactivate Service Broker networking

- Drop all Service Broker endpoints.

## Examples

```sql
USE master;
GO

DROP ENDPOINT BrokerEndpoint;
GO
```

## Related content

- [How to: Activate Service Broker networking (Transact-SQL)](how-to-activate-service-broker-networking-transact-sql.md)
- [How to: Pause Service Broker networking (Transact-SQL)](how-to-pause-service-broker-networking-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [ALTER ENDPOINT (Transact-SQL)](../../t-sql/statements/alter-endpoint-transact-sql.md)
- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
