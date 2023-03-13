---
title: "How to: Configure Permissions for a Local Service (Transact-SQL)"
description: "SQL Server enforces SEND permission for each service and RECEIVE permissions for each queue."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# How to: Configure Permissions for a Local Service (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

SQL Server enforces SEND permission for each service and RECEIVE permissions for each queue. The security principal that owns the initiating service must have SEND permission on the target service. The security principal for an application must have RECEIVE permission for each queue that the application receives messages from.

This procedure is a simplified form of the procedure for creating a remote security configuration. In both cases, you grant SEND permission on the destination service and RECEIVE permission on the queue for the service that sends the messages. For a remote security configuration, however, you must also configure Service Broker security to correctly identify the remote user. For a configuration within a single database, you only need to grant permissions.

## Granting permissions for a local service

1. Grant permission for the user to receive from the queue that the application uses.

2. Grant permission for the user that owns the initiating service to send messages to the services that the application communicates with.

## Example

This example configures permissions to allow BrokerApplicationUser to send messages from the service that uses the queue StoreFrontQueue to the service Ordering. This procedure assumes that the user, the services, and the queue already exist.

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

```sql
    USE AdventureWorks2008R2 ;
    GO

    -- This example sets permissions for a service
    -- program that sends messages to the Ordering service
    -- and receives messages from the StoreFrontQueue queue.

    -- Grant SEND permission on the service to the owner
    -- of the initiating service.
    GRANT SEND ON SERVICE::[Ordering]
    TO [BrokerApplicationUser] ;
    GO

    -- Grant RECEIVE permission on the queue.
    GRANT RECEIVE ON [StoreFrontQueue]
    TO [BrokerApplicationUser] ;
    GO
```

## See also

- [How to: Configure Target Services for Anonymous Dialog Security (Transact-SQL)](how-to-configure-target-services-for-anonymous-dialog-security-transact-sql.md)
- [How to: Configure Target Services for Full Dialog Security (Transact-SQL)](how-to-configure-target-services-for-full-dialog-security-transact-sql.md)
- [How to: Configure Initiating Services for Full Dialog Security (Transact-SQL)](how-to-configure-initiating-services-for-full-dialog-security-transact-sql.md)
- [How to: Configure Initiating Services for Anonymous Dialog Security (Transact-SQL)](how-to-configure-initiating-services-for-anonymous-dialog-security-transact-sql.md)
- [Grant T-SQL permissions for Parallel Data Warehouse](../../analytics-platform-system/grant-permissions.md)
- [GRANT Service Broker Permissions (Transact-SQL)](../../t-sql/statements/grant-service-broker-permissions-transact-sql.md)
- [Identity and Access Control (Service Broker)](identity-and-access-control.md)
