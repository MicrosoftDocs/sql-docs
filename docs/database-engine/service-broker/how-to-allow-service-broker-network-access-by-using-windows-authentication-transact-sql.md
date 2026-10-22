---
title: "How To: Allow Service Broker Network Access by Using Windows Authentication (Transact-SQL)"
description: Learn how to allow instances to send Service Broker messages using Windows authentication.
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 08/29/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# How to: Allow Service Broker network access by using Windows Authentication (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

To allow another instance to send messages using Windows Authentication for transport security, you create a user in the `master` database for the startup service account for the other instance.

## Permit Service Broker access using Windows Authentication

1. Create a login for the startup service account for the other instance.

1. Grant that user `CONNECT` permission to the Service Broker endpoint.

Once access is configured in each instance, then communications between the two instances use Service Broker transport security when the transport security configuration option is set in both databases.

> [!NOTE]  
> If both instances run as the same domain account, then the instances can always communicate using Windows Authentication for transport security. If the instances run as the `LocalSystem` account, the login name is MachineName$, and Kerberos must be available on the network to use the machine account.

## Examples

```sql
USE master;
GO

CREATE LOGIN [DOMAIN\user] FROM WINDOWS;
GO

GRANT CONNECT ON ENDPOINT::ThisInstanceEndpoint TO [DOMAIN\user];
GO
```

## Related content

- [CREATE LOGIN (Transact-SQL)](../../t-sql/statements/create-login-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
