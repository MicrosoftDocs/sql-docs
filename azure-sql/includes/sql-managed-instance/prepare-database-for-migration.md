---
author: bluefooted
ms.author: pamela
ms.reviewer: vanto
ms.date: 03/06/2026
ms.service: azure-sql-managed-instance
ms.topic: include
---

### Enable accelerated database recovery

For SQL Server 2019 and later versions, enable [accelerated database recovery](/sql/relational-databases/accelerated-database-recovery-concepts), and ensure the persistent version store (PVS) is set to `PRIMARY`. If accelerated database recovery isn't enabled on the source SQL Server database, you can't enable it on the target SQL managed instance after the database is migrated. If the persistent version store (PVS) isn't set to `PRIMARY`, you can experience issues with restore operations on the target SQL managed instance.

For SQL Server 2017 and earlier versions, accelerated database recovery isn't supported, so this step isn't necessary.

To configure accelerated database recovery properly on the source SQL Server database, follow these steps:

1. Enable accelerated database recovery by running the following Transact-SQL script on SQL Server:

   ```sql
   ALTER DATABASE [<database name>] SET ACCELERATED_DATABASE_RECOVERY = ON;
   ```
1. The persistent version store (PVS) must be set to `PRIMARY` on the source database, which is the default configuration. If this was changed previously, you must [change it back to PRIMARY](/sql/relational-databases/accelerated-database-recovery-management#change-the-pvs-filegroup) before starting the migration.

### Enable Service Broker

[Service Broker](/sql/database-engine/configure-windows/sql-server-service-broker) is enabled by default for all versions of SQL Server. If Service Broker was disabled and you plan to use it on SQL Managed Instance, enable Service Broker on the source SQL Server database before you migrate to SQL Managed Instance. If Service Broker isn't enabled on the source SQL Server database, you can't use it on the target SQL managed instance.

To check if Service Broker is enabled, run the following Transact-SQL script on SQL Server instance:

```sql
SELECT name AS [Database Name], is_broker_enabled AS [Service Broker Enabled]
FROM sys.databases
WHERE name = '<database name>';
```

If Service Broker is disabled, enable it by running the following Transact-SQL script on the source SQL Server database:

```sql
USE master;
GO

ALTER DATABASE [<database name>]
    SET ENABLE_BROKER;
GO
```

