---
title: "How to: Pause Service Broker Networking (Transact-SQL)"
description: "Service Broker sends and receives messages over the network while any endpoint for Service Broker is in the STARTED state."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# How to: Pause Service Broker Networking (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker sends and receives messages over the network while any endpoint for Service Broker is in the **STARTED** state. To pause Service Broker networking, alter all Service Broker endpoints to set the state to **STOPPED**. Notice that this prevents Service Broker from transmitting messages out of the instance or receiving messages from outside of the instance, but does not affect message delivery within the instance. To prevent message delivery to a specific database, use **ALTER DATABASE** to deactivate Service Broker in that database.

## To pause Service Broker networking

- Alter all Service Broker endpoints to set the state to **STOPPED**.

## Example

```sql
    USE master ;
    GO

    ALTER ENDPOINT BrokerEndpoint
        STATE = STOPPED ;
    GO
```

## See also

- [How to: Deactivate Service Broker Networking (Transact-SQL)](how-to-deactivate-service-broker-networking-transact-sql.md)
- [How to: Resume Service Broker Networking (Transact-SQL)](how-to-resume-service-broker-networking-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [ALTER ENDPOINT (Transact-SQL)](../../t-sql/statements/alter-endpoint-transact-sql.md)
- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
