---
title: "How to: Allow Service Broker Network Access by Using Windows Authentication (Transact-SQL)"
description: "To allow another instance to send messages using Windows Authentication for transport security, you create a user in the master database for the startup service account for the other instance."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# How to: Allow Service Broker Network Access by Using Windows Authentication (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

To allow another instance to send messages using Windows Authentication for transport security, you create a user in the **master** database for the startup service account for the other instance.

## To permit Service Broker access using Windows Authentication

1. Create a login for the startup service account for the other instance.

2. Grant that user CONNECT permission to the Service Broker endpoint.

Once access is configured in each instance, then communications between the two instances use Service Broker transport security when the transport security configuration option is set in both databases.

> [!NOTE]
> If both instances run as the same domain account, then the instances can always communicate using Windows Authentication for transport security. If the instances run as the **LocalSystem** account, the login name is MachineName$, and Kerberos must be available on the network to use the machine account.

## Example

```sql
    USE master ;
    GO

    CREATE LOGIN [DOMAIN\user] FROM WINDOWS ;
    GO

    GRANT CONNECT ON ENDPOINT::ThisInstanceEndpoint to [DOMAIN\user] ;
    GO
```

## See also

- [CREATE LOGIN (Transact-SQL)](../../t-sql/statements/create-login-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
