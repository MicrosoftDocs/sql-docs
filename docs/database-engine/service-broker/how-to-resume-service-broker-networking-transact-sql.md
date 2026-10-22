---
title: "How To: Resume Service Broker Networking (Transact-SQL)"
description: "Service Broker sends and receives messages over the network while any endpoint for Service Broker is in the STARTED state"
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/02/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# How to: Resume Service Broker networking (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker sends and receives messages over the network while any endpoint for Service Broker is in the `STARTED` state. To resume Service Broker networking, alter the endpoints to set the state to `STARTED`.

> [!NOTE]  
> Activating Service Broker networking allows Service Broker to send and receive messages over the network. The authentication level set on the endpoint controls which network connections the endpoint accepts. For more information on Service Broker networking and security, see [Security overview (Service Broker)](security-overview.md).

## Resume Service Broker networking

- Alter an endpoint to set the state to `STARTED`.

## Examples

```sql
USE master;
GO

ALTER ENDPOINT BrokerEndpoint
    STATE = STARTED;
GO
```

## Related content

- [How to: Activate Service Broker networking (Transact-SQL)](how-to-activate-service-broker-networking-transact-sql.md)
- [How to: Deactivate Service Broker networking (Transact-SQL)](how-to-deactivate-service-broker-networking-transact-sql.md)
- [How to: Pause Service Broker networking (Transact-SQL)](how-to-pause-service-broker-networking-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [ALTER ENDPOINT (Transact-SQL)](../../t-sql/statements/alter-endpoint-transact-sql.md)
- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
