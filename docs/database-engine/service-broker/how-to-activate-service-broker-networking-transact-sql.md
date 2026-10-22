---
title: "How To: Activate Service Broker Networking (Transact-SQL)"
description: Learn how to activate Service Broker networking in an instance by creating an endpoint for Service Broker.
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 08/29/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# How to: Activate Service Broker networking (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker doesn't send or receive messages over the network by default. To activate Service Broker networking in an instance, create an endpoint for Service Broker.

Creating a Service Broker endpoint causes SQL Server to accept TCP/IP connections on the port specified. Service Broker transport security allows you to require authorization for connections to the port. If the computer that runs SQL Server has a firewall enabled, the firewall configuration on that computer must allow both incoming connections for the port specified in the endpoint. For more information on Service Broker transport security, see [Service Broker transport security](service-broker-transport-security.md).

## Activate Service Broker networking

Create a Service Broker endpoint, specifying the port number and the authentication level.

## Examples

```sql
USE master;
GO

CREATE ENDPOINT BrokerEndpoint
    STATE = STARTED
    AS TCP (
        LISTENER_PORT = 4037
    )
    FOR SERVICE_BROKER (
        AUTHENTICATION = WINDOWS
    );
GO
```

## Related content

- [How to: Pause Service Broker networking (Transact-SQL)](how-to-pause-service-broker-networking-transact-sql.md)
- [How to: Resume Service Broker networking (Transact-SQL)](how-to-resume-service-broker-networking-transact-sql.md)
- [How to: Deactivate Service Broker networking (Transact-SQL)](how-to-deactivate-service-broker-networking-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
