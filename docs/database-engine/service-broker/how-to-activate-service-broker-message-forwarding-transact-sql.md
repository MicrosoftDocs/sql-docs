---
title: "How to: Activate Service Broker Message Forwarding (Transact-SQL)"
description: "Message forwarding allows an instance of SQL Server to accept messages from outside the instance and send those messages to a different instance. Message forwarding is configured on a Service Broker endpoint."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# How to: Activate Service Broker Message Forwarding (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Message forwarding allows an instance of SQL Server to accept messages from outside the instance and send those messages to a different instance. Message forwarding is configured on a Service Broker endpoint.

## To activate Service Broker message forwarding

1. Activate Service Broker networking if networking is not already active. For more information on Service Broker networking, see [How to: Activate Service Broker Networking (Transact-SQL)](how-to-activate-service-broker-networking-transact-sql.md).

2. Alter the endpoint to activate message forwarding, and specify the maximum size, in megabytes, for forwarded messages.

## Example

```sql
    USE master ;
    GO

    ALTER ENDPOINT BrokerEndpoint
        FOR SERVICE_BROKER ( MESSAGE_FORWARDING = ENABLED,
                             MESSAGE_FORWARD_SIZE = 10 ) ;
    GO
```

## See also

- [How to: Activate Service Broker Networking (Transact-SQL)](how-to-activate-service-broker-networking-transact-sql.md)
- [How to: Deactivate Service Broker Message Forwarding (Transact-SQL)](how-to-deactivate-service-broker-message-forwarding-transact-sql.md)
