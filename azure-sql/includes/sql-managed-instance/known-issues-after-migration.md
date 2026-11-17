---
author: bluefooted
ms.author: pamela
ms.reviewer: vanto
ms.date: 11/05/2026
ms.service: azure-sql-managed-instance
ms.topic: include
---

### Restore operation failures after migrating to SQL Managed Instance

If you migrate a database to Azure SQL Managed Instance from SQL Server 2019 and later versions with [accelerated database recovery](/sql/relational-databases/accelerated-database-recovery-concepts) enabled, but configured with the persistent version store (PVS) set to something other than the `PRIMARY` file group, you can experience restore operation failures on the target SQL managed instance. 

To work around this issue, make sure you set the [persistent version store (PVS) to PRIMARY](/sql/relational-databases/accelerated-database-recovery-management#change-the-pvs-filegroup) on the source SQL Server database before you migrate it to SQL Managed Instance. If you already migrated the database without setting the PVS to `PRIMARY`, you can set it on the source SQL Server database, and then re-migrate the database to SQL Managed Instance.

### Unable to use accelerated database recovery after migrating to SQL Managed Instance

Starting with SQL Server 2019, if you migrate a database to Azure SQL Managed Instance, and the source database has [accelerated database recovery](/sql/relational-databases/accelerated-database-recovery-concepts) disabled, the disabled setting is transferred with the database. You can't enable accelerated database recovery yourself on the migrated database. This is a per-database setting, so other databases on the managed instance aren't affected.

To prevent this issue, [enable accelerated database recovery](/sql/relational-databases/accelerated-database-recovery-management#enable-accelerated-database-recovery) on the source SQL Server database before you migrate it to SQL Managed Instance. If you already migrated the database without enabling accelerated database recovery, contact Microsoft Support to enable it on the migrated database. Alternatively, you can resolve the issue yourself by enabling accelerated database recovery on the source SQL Server database, and then re-migrating the database to SQL Managed Instance.

SQL Server 2017 and earlier versions don't support accelerated database recovery, so this issue doesn't apply to databases migrated from those versions of SQL Server.

### Unable to use Service Broker after migrating to SQL Managed Instance

If you migrate a database to Azure SQL Managed Instance, and [Service Broker is disabled on the source database](/sql/database-engine/service-broker/how-to-activate-service-broker-message-delivery-in-databases-transact-sql), the disabled setting is transferred with the database. You can't enable Service Broker yourself on the migrated database. This is a per-database setting, so other databases on the managed instance aren't affected.

To prevent this issue, enable Service Broker on the source SQL Server database before you migrate it to SQL Managed Instance. If you already migrated the database without enabling Service Broker, contact Microsoft Support to enable it on the migrated database. Alternatively, you can resolve the issue yourself by enabling Service Broker on the source SQL Server database, and then re-migrating the database to SQL Managed Instance.