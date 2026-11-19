---
author: danimir
ms.author: danil
ms.reviewer: vanto
ms.date: 11/19/2026
ms.service: azure-sql-managed-instance
ms.topic: include
ai-usage: ai-assisted
---

### Restore operation failures after migrating to SQL Managed Instance

If you migrate a database to Azure SQL Managed Instance from SQL Server 2019 and later versions with [accelerated database recovery](/sql/relational-databases/accelerated-database-recovery-concepts) enabled, but configured with the persistent version store (PVS) set to something other than the `PRIMARY` file group, you can experience restore operation failures on the target SQL managed instance. 

To work around this issue, make sure you set the [persistent version store (PVS) to PRIMARY](/sql/relational-databases/accelerated-database-recovery-management#change-the-pvs-filegroup) on the source SQL Server database before you migrate it to SQL Managed Instance. If you already migrated the database without setting the PVS to `PRIMARY`, you can set it on the source SQL Server database, and then re-migrate the database to SQL Managed Instance.

### Unable to use Service Broker after migrating to SQL Managed Instance

If you migrate a database to Azure SQL Managed Instance with [Service Broker disabled on the source database](/sql/database-engine/service-broker/how-to-activate-service-broker-message-delivery-in-databases-transact-sql), the disabled setting is migrated with the database, and you can't enable it yourself. This is a per-database setting, so other databases on the managed instance aren't affected. To resolve this issue, either enable Service Broker on the source database and migrate it again, or contact Microsoft Support.