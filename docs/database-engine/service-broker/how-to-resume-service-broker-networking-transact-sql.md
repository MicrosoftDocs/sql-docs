---
title: "How to: Resume Service Broker Networking (Transact-SQL)"
description: "Service Broker sends and receives messages over the network while any endpoint for Service Broker is in the STARTED state"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# How to: Resume Service Broker Networking (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker sends and receives messages over the network while any endpoint for Service Broker is in the **STARTED** state. To resume Service Broker networking, alter the endpoints to set the state to **STARTED**.

> [!NOTE]
> Activating Service Broker networking allows Service Broker to send and receive messages over the network. The authentication level set on the endpoint controls which network connections are accepted by the endpoint. For more information on Service Broker networking and security, see [Security Overview (Service Broker)](security-overview.md).

## To resume Service Broker networking

- Alter an endpoint to set the state to **STARTED**.

## Example

```sql
    USE master ;
    GO

    ALTER ENDPOINT BrokerEndpoint
        STATE = STARTED ;
    GO
```

## See also

- [How to: Activate Service Broker Networking (Transact-SQL)](how-to-activate-service-broker-networking-transact-sql.md)
- [How to: Deactivate Service Broker Networking (Transact-SQL)](how-to-deactivate-service-broker-networking-transact-sql.md)
- [How to: Pause Service Broker Networking (Transact-SQL)](how-to-pause-service-broker-networking-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [ALTER ENDPOINT (Transact-SQL)](../../t-sql/statements/alter-endpoint-transact-sql.md)
- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
