---
title: "How to: Deactivate Service Broker Message Forwarding (Transact-SQL)"
description: "Message forwarding allows an instance of SQL Server to accept messages from outside the instance and send those messages to a different instance."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# How to: Deactivate Service Broker Message Forwarding (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Message forwarding allows an instance of SQL Server to accept messages from outside the instance and send those messages to a different instance. Message forwarding is configured on a Service Broker endpoint.

## To deactivate Service Broker message forwarding

- Alter the endpoint to deactivate message forwarding.

## Example

```sql
    USE master ;
    GO

    ALTER ENDPOINT BrokerEndpoint
        FOR SERVICE_BROKER ( MESSAGE_FORWARDING = DISABLED) ;
    GO
```

## See also

- [How to: Activate Service Broker Networking (Transact-SQL)](how-to-activate-service-broker-networking-transact-sql.md)
- [How to: Activate Service Broker Message Forwarding (Transact-SQL)](how-to-activate-service-broker-message-forwarding-transact-sql.md)
