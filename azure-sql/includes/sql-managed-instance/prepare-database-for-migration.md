---
author: danimir
ms.author: danil
ms.reviewer: vanto
ms.date: 11/18/2026
ms.service: azure-sql-managed-instance
ms.topic: include
ai-usage: ai-assisted
---

### Migrate a certificate of a TDE-protected database

If your source SQL Server database uses Transparent Data Encryption (TDE), configure your managed instance to use customer-managed TDE protection and migrate the SQL Server database's TDE protector certificate to the managed instance before you start the migration. For instructions, see [Migrate a certificate of a TDE-protected database](/azure/azure-sql/managed-instance/tde-certificate-migrate). After the migration completes, you can optionally switch your managed instance to a service-managed TDE protector.

> [!NOTE]
> Azure Key Vault is supported by SQL Server on Linux starting with [Cumulative Update 14 for SQL Server 2022](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate14).

### Prepare databases that use In-Memory OLTP

Databases that use In-Memory OLTP can only be migrated to a managed instance in the Business Critical service tier.

If a database previously used In-Memory OLTP but no longer uses it, you can migrate it to the General Purpose service tier after you meet these requirements:

- Drop all memory-optimized tables, even if they're empty.
- Drop all memory-optimized table types.
- Drop natively compiled SQL modules that interact with memory-optimized objects.

Memory-optimized filegroups can remain in the database; they're the only In-Memory OLTP remnants that can remain.

To check whether your database is ready, run the Transact-SQL queries in [In-Memory OLTP (memory-optimized tables)](/data-migration/sql-server/managed-instance/overview#in-memory-oltp-memory-optimized-tables).

### Prepare databases that use accelerated database recovery (ADR)

If you use [accelerated database recovery (ADR)](/sql/relational-databases/accelerated-database-recovery-concepts) on the database you're migrating, ensure that the persistent version store (PVS) is set to `PRIMARY`. If the PVS isn't set to `PRIMARY`, you might experience issues with restore operations on the target SQL managed instance. These settings apply to the individual database and don't affect ADR on other databases on the managed instance.

### Prepare databases with Service Broker disabled

[Service Broker](/sql/database-engine/configure-windows/sql-server-service-broker) is enabled by default for all versions of SQL Server. If you disabled Service Broker on the database you're migrating and you plan to use it on SQL Managed Instance, enable Service Broker on the source SQL Server database before you migrate to SQL Managed Instance. If you don't enable Service Broker on the source SQL Server database, the migration process preserves the disabled setting. You can't enable Service Broker yourself on a migrated database, but you can contact Microsoft Support to enable it. This is a database-level setting, so other databases on the managed instance aren't affected.

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

### Check database integrity (optional)

As a best practice, consider running [DBCC CHECKDB](/sql/t-sql/database-console-commands/dbcc-checkdb-transact-sql) on each source SQL Server database before you take the full backup required for migration. This check identifies physical and logical consistency errors. Existing database corruption can transfer to the managed instance during migration. Investigate and resolve any errors before you proceed.

`DBCC CHECKDB` can consume significant resources, so schedule the check during a suitable maintenance window.
