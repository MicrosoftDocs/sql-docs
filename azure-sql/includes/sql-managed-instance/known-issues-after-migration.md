---
author: bluefooted
ms.author: pamela
ms.reviewer: vanto
ms.date: 06/02/2026
ms.service: azure-sql-managed-instance
ms.topic: include
---

### Restore operation failures after migrating to SQL Managed Instance

If you migrate a database to Azure SQL Managed Instance from SQL Server 2019 and later versions with [accelerated database recovery](/sql/relational-databases/accelerated-database-recovery-concepts) enabled, but configured with the persistent version store (PVS) set to something other than the `PRIMARY` file group, you can experience restore operation failures on the target SQL managed instance. 

To work around this issue, make sure you set the [persistent version store (PVS) to PRIMARY](/sql/relational-databases/accelerated-database-recovery-management#change-the-pvs-filegroup) on the source SQL Server database before you migrate it to SQL Managed Instance. If you already migrated the database without setting the PVS to `PRIMARY`, you can set it on the source SQL Server database, and then re-migrate the database to SQL Managed Instance.

### Unable to use accelerated database recovery after migrating to SQL Managed Instance

Starting with SQL Server 2019, if you migrate a database to Azure SQL Managed Instance, and the source database has [accelerated database recovery](/sql/relational-databases/accelerated-database-recovery-concepts) disabled, you can't use accelerated database recovery on the target SQL managed instance. 

To work around this issue, make sure you [enable accelerated database recovery](/sql/relational-databases/accelerated-database-recovery-management#enable-accelerated-database-recovery) on the source SQL Server database before you migrate it to SQL Managed Instance. If you already migrated the database without enabling accelerated database recovery, you can enable it on the source SQL Server database, and then re-migrate the database to SQL managed instance.

SQL Server 2017 and earlier versions don't support accelerated database recovery, so this issue doesn't apply to databases migrated from those versions of SQL Server.

### Unable to use Service Broker after migrating to SQL Managed Instance

If you migrate a database to Azure SQL Managed Instance, and [Service Broker is disabled on the source database](/sql/database-engine/service-broker/how-to-activate-service-broker-message-delivery-in-databases-transact-sql), you can't use Service Broker on the target SQL managed instance.

To work around this problem, make sure you enable Service Broker on the source SQL Server database before you migrate it to SQL Managed Instance. If you already migrated the database without enabling Service Broker, you can enable it on the source SQL Server database, and then re-migrate the database to SQL Managed Instance.