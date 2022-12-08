---
title: "How to: Deactivate Service Broker Message Delivery in Databases (Transact-SQL)"
description: "When message delivery is not active, messages remain in the transmission queue."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# How to: Deactivate Service Broker Message Delivery in Databases (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

When message delivery is not active, messages remain in the transmission queue. To determine whether Service Broker is active for a database, check the **is_broker_enabled** column of the **sys.databases** catalog view.

> [!NOTE]
> Deactivating Service Broker prevents messages from being sent from or delivered to the database. However, this does not prevent messages from arriving in the instance. To prevent messages from arriving in the instance, you must remove or stop the Service Broker endpoint.### To deactivate Service Broker in a database

- Alter the database to set the DISABLE_BROKER option.

## Example

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

```sql
    USE master ;
    GO

    ALTER DATABASE AdventureWorks2008R2 SET DISABLE_BROKER ;
    GO
```

## See also

- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [sys.databases (Transact-SQL)](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.transmission_queue (Transact-SQL)](../../relational-databases/system-catalog-views/sys-transmission-queue-transact-sql.md)
