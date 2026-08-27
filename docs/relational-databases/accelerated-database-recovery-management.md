---
title: "Manage Accelerated Database Recovery"
description: "Best practices for managing and configuring accelerated database recovery (ADR)."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: wiassaf, derekw, dfurman, randolphwest
ms.date: 05/01/2025
ms.service: sql
ms.subservice: backup-restore
ms.topic: how-to
helpviewer_keywords:
  - "accelerated database recovery [SQL Server], recovery-only"
  - "database recovery [SQL Server]"
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15"
ms.custom:
  - build-2025
---

# Manage accelerated database recovery

[!INCLUDE [SQL Server 2019](../includes/applies-to-version/sqlserver2019-and-later.md)]

This article teaches you to enable and disable [accelerated database recovery (ADR)](accelerated-database-recovery-concepts.md) with Transact-SQL (T-SQL) in [!INCLUDE[sssql19-md](../includes/sssql19-md.md)] and later versions, as well as how to change the persistent version store (PVS) filegroup used by ADR.

> [!NOTE]
> In [!INCLUDE [ssazure-sqldb](../includes/ssazure-sqldb.md)], [!INCLUDE[ssazuremi-md](../includes/ssazuremi-md.md)], and [!INCLUDE[fabric-sqldb](../includes/fabric-sqldb.md)], accelerated database recovery (ADR) is always enabled. If you observe issues, such as high storage usage by PVS or slow ADR cleanup, see [Monitor and troubleshoot accelerated database recovery](accelerated-database-recovery-troubleshoot.md) or contact [Azure support](https://azure.microsoft.com/support/options/). 

## Who should consider accelerated database recovery

Many customers find accelerated database recovery (ADR) a valuable technology to improve database recovery time and avoid lengthy rollbacks.

If your database workloads frequently encounter the following scenarios, you can benefit from ADR:

- Long running transactions that can't be avoided. For example, ADR helps in cases where long-running transactions are at risk of being rolled back.
- Active transactions that cause the transaction log to grow significantly.
- Long-running database recovery that affects the availability of the database (for example, after an unexpected SQL Server restart or manual transaction rollback).

If your application uses a high volume of single-row modifications in individual transactions, your workload might not be optimal for ADR. Consider batching modifications in multi-row statements where possible, and avoid a high volume of small DML transactions.

## Enable ADR

ADR is off by default and available starting with [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)].

Use the following Transact-SQL (T-SQL) command to enable ADR:

```sql
ALTER DATABASE [<db_name>] SET ACCELERATED_DATABASE_RECOVERY = ON;
```

An exclusive database lock is necessary to enable or disable ADR. That means the `ALTER DATABASE` command is blocked until all active sessions are gone, and that any new sessions wait behind the `ALTER DATABASE` command. If it's important to complete the operation and remove the lock, you can use the termination clause, `WITH ROLLBACK [IMMEDIATE | AFTER {number} SECONDS | NO_WAIT]` to abort any active sessions in the database. For more information, see [ALTER DATABASE SET options](../t-sql/statements/alter-database-transact-sql-set-options.md).

If enabling or disabling ADR in `tempdb`, an exclusive database lock is not required and the termination clause shouldn't be specified. However, a restart of the [!INCLUDE [ssDE](../includes/ssde-md.md)] must occur for the change to take effect.

## Disable ADR

Use the following T-SQL command to disable ADR:

```sql
ALTER DATABASE [<db_name>] SET ACCELERATED_DATABASE_RECOVERY = OFF;
GO
```

Even after ADR is disabled, there might be versions stored in PVS that the system still needs for logical revert until all active transactions complete. If disabling ADR in `tempdb`, a restart of the [!INCLUDE [ssDE](../includes/ssde-md.md)] is required for the change to take effect.

## Change the PVS filegroup

By default, the persistent version store (PVS) data is on the `PRIMARY` filegroup. You can move PVS to a different filegroup if necessary. For example, it might require more space or faster storage.

To change the location of the PVS to a different filegroup, follow these steps:

1. Create the filegroup for PVS and add at least one data file to this filegroup. For example:

   ```sql
   ALTER DATABASE [<db_name>] ADD FILEGROUP [VersionStoreFG];
   GO

   ALTER DATABASE [<db_name>]
   ADD FILE
   (
      NAME = N'VersionStoreFG',
      FILENAME = N'E:\DATA\VersionStore.ndf',
      SIZE = 8192 MB,
      FILEGROWTH = 64 MB
   )
   TO FILEGROUP [VersionStoreFG];
   ```

1. Disable ADR with the following T-SQL command:

   ```sql
   ALTER DATABASE [<db_name>] SET ACCELERATED_DATABASE_RECOVERY = OFF;
   GO
   ```

1. Wait until all versions stored in PVS are removed.

   To enable ADR using a new PVS location, first ensure all version information has been purged from the previous PVS location. You can force the cleanup to happen with the [sys.sp_persistent_version_cleanup](system-stored-procedures/sys-sp-persistent-version-cleanup-transact-sql.md) stored procedure:

   ```sql
   EXEC sys.sp_persistent_version_cleanup [<db_name>];
   ```

   The `sys.sp_persistent_version_cleanup` stored procedure is synchronous, which means it won't complete until all version information is cleaned up from the current PVS. Once it completes, and assuming ADR is disabled, you can verify version information is removed by querying [sys.dm_tran_persistent_version_store_stats](system-dynamic-management-views/sys-dm-tran-persistent-version-store-stats.md) and examining the value of `persistent_version_store_size_kb`. For example:

   ```sql
   SELECT DB_NAME(database_id),
          persistent_version_store_size_kb
   FROM sys.dm_tran_persistent_version_store_stats
   WHERE database_id = [MyDatabaseID];
   ```

   When the value of `persistent_version_store_size_kb` is `0`, you can re-enable ADR and place PVS on the new filegroup.

1. Enable ADR and specify the new PVS location with the following T-SQL command:

   ```sql
   ALTER DATABASE [<db_name>] SET ACCELERATED_DATABASE_RECOVERY = ON
   (PERSISTENT_VERSION_STORE_FILEGROUP = [VersionStoreFG]);
   ```

> [!NOTE]
> Moving PVS to a different filegroup is not supported in `tempdb` because you [cannot add filegroups](./databases/tempdb-database.md#restrictions) in `tempdb`.

## Monitor the size of the PVS

Once you enable ADR in a database, monitor the size of the persistent version store (PVS) and PVS cleanup performance. For more information, see [Monitor and troubleshoot accelerated database recovery](accelerated-database-recovery-troubleshoot.md).

## Related content

- [Accelerated database recovery](accelerated-database-recovery-concepts.md)
- [Monitor and troubleshoot accelerated database recovery](accelerated-database-recovery-troubleshoot.md)
