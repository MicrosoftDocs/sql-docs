---
title: "How To: Activate Service Broker Message Delivery in Databases (Transact-SQL)"
description: "By default, Service Broker message delivery is active in a database when the database is created."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 08/29/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# How to: Activate Service Broker message delivery in databases (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

By default, [Service Broker](../configure-windows/sql-server-service-broker.md) is enabled and message delivery is active in a database when the database is created. When message delivery isn't active, messages remain in the transmission queue. To determine whether Service Broker is active for a database, check the `is_broker_enabled` column of the `sys.databases` catalog view.

> [!NOTE]  
> Activating Service Broker allows messages to be delivered to the database. You must create a Service Broker endpoint to send and receive messages from outside of the instance.

## Disable Service Broker

Service Broker is enabled by default when a database is created. You can use the [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) statement to disable Service Broker message delivery in a database. When you disable Service Broker, messages remain in the transmission queue and aren't delivered to the database.

To disable Service Broker, run the following Transact-SQL script:

```sql
USE master;
GO
ALTER DATABASE [<database name>]
    SET DISABLE_BROKER;    
```

> [!NOTE]
> If Service Broker is disabled on a database migrated to Azure SQL Managed Instance, enabling Service Broker on the target SQL managed instance isn't available. To use Service Broker on the target SQL managed instance, enable it on the source SQL Server database before you migrate to SQL managed instance. 

## Enable Service Broker in a database

If Service Broker has been disabled, you can enable it by using the [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) statement to set the `ENABLE_BROKER` option.

To enable Service Broker, run the following Transact-SQL script:

```sql
USE master;
GO

ALTER DATABASE [<database name>]
    SET ENABLE_BROKER;
GO
```

## Check service broker status

To check the status of Service Broker for a database, run the following Transact-SQL script:

```sql
SELECT name AS [Database Name], is_broker_enabled AS [Service Broker Enabled]
FROM sys.databases
WHERE name = '[<database name>]';
```

## Service Broker in Azure SQL Managed Instance

In Azure SQL Managed Instance, Service Broker is enabled by default and can't be disabled. The following `ALTER DATABASE` options aren't supported:

- `ENABLE_BROKER`
- `DISABLE_BROKER`


## Related content

- [How to: Deactivate Service Broker message delivery in databases (Transact-SQL)](how-to-deactivate-service-broker-message-delivery-in-databases-transact-sql.md)
- [How to: Activate Service Broker networking (Transact-SQL)](how-to-activate-service-broker-networking-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [sys.databases (Transact-SQL)](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.transmission_queue (Transact-SQL)](../../relational-databases/system-catalog-views/sys-transmission-queue-transact-sql.md)
