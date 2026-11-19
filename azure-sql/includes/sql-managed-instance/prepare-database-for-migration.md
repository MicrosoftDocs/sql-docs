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

If you've disabled [Service Broker](/sql/database-engine/configure-windows/sql-server-service-broker), which is enabled by default on SQL Server, re-enable it on the source database before migration if you plan to use it on SQL Managed Instance. Otherwise, the migrated database retains the disabled setting, and you must contact Microsoft Support to enable Service Broker. This setting applies only to the migrated database and doesn't affect other databases on the managed instance.

Run this script on the source SQL Server instance. Replace `<database name>` with your database name.

```sql
USE master;
GO
DECLARE @database_name sysname = N'<database name>';
DECLARE @sql nvarchar(max) =
    N'ALTER DATABASE ' + QUOTENAME(@database_name) + N' SET ENABLE_BROKER;';

IF (SELECT is_broker_enabled FROM sys.databases WHERE name = @database_name) = 0
    EXEC sys.sp_executesql @sql;
GO
```
