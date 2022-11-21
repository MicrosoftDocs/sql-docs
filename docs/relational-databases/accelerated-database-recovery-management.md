---
description: "Best practices for managing and configuring accelerated database recovery (ADR)."
title: "Manage accelerated database recovery | Microsoft Docs"
ms.date: 07/12/2022
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "accelerated database recovery [SQL Server], recovery-only"
  - "database recovery [SQL Server]"
author: mashamsft
ms.author: mathoma
ms.reviewer: kfarlee, wiassaf
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
---
# Manage accelerated database recovery

[!INCLUDE [SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

This article contains information on best practices for managing and configuring accelerated database recovery (ADR) in [!INCLUDE[sssql19-md](../includes/sssql19-md.md)] and later. For more information on ADR on Azure SQL, see [Accelerated Database Recovery in Azure SQL](/azure/azure-sql/accelerated-database-recovery).

> [!NOTE]
> In [!INCLUDE[ssSDSfull](../includes/sssdsfull-md.md)] and [!INCLUDE[ssazuremi_md](../includes/ssazuremi_md.md)], accelerated database recovery (ADR) is enabled on all databases and cannot be disabled. If you observe issues either with storage usage, high abort transaction and other factors, review [Troubleshoot accelerated database recovery](accelerated-database-recovery-troubleshoot.md) or contact [Azure Support](https://azure.microsoft.com/support/options/). 

## Who should consider accelerated database recovery

Many customers find accelerated database recovery (ADR) a valuable technology to improve recovery time. An accumulation of the factors below should be considered before deciding which databases should use ADR, evaluate the factors below and if the accumulation of factors weighs in favor or against using ADR. 

- ADR is recommended for workloads with long running transactions that cannot be avoided. For example, in cases where long-running transactions are at risk of being rolled back, ADR can help.

- ADR is recommended for workloads that have seen cases where active transactions are causing the transaction log to grow significantly.  

- ADR is recommended for workloads that have experienced long periods of database unavailability due to SQL Server long running recovery (such as unexpected SQL Server restart or manual transaction rollback).

- ADR is not supported for databases enrolled in database mirroring.

- ADR is not recommended for databases larger than 100 terabytes due to the single-threaded persistent version store (PVS) version cleaner.  

- If your application performs many non-batched, incremental updates, such as updating a record every time there is a row accessed/inserted, your workload may not be optimal for ADR. Consider rewriting the application queries to batch updates, where possible, until the end of the command and reduce a high number of small update transactions.

### Evaluate whether your workload is a good fit for ADR

Once you have enabled ADR in your workload, look for signs that the persistent version store (PVS) is unable to keep up. It is recommended to monitor ADR health using the methods found in [Troubleshooting accelerated database recovery](accelerated-database-recovery-troubleshoot.md). 

ADR is not recommended for database environments with a high count of update/deletes, such as high-volume OLTP, without a period of rest/recovery for the PVS cleanup process to reclaim space. Typically, business operation cycles allow for this time, but in some scenarios you may want to initiate the PVS cleanup process manually to take advantage of application activity conditions.

   - If the PVS cleanup process is running for a long period time, you may find that the count of aborted transactions will grow which will also cause the PVS size to increase. Use the `sys.dm_tran_aborted_transactions` DMV to report the aborted transaction count, and use `sys.dm_tran_persistent_version_store_stats` to report the cleanup start/end times along with the PVS size. For more information, see [sys.dm_tran_persistent_version_store_stats](system-dynamic-management-views/sys-dm-tran-persistent-version-store-stats.md).  

   - To activate the PVS cleanup process manually between workloads or during maintenance windows, use `sys.sp_persistent_version_cleanup`. For more information, see [sys.sp_persistent_version_cleanup](system-stored-procedures/sys-sp-persistent-version-cleanup-transact-sql.md). 
   
   - Workloads featuring long-running queries in SNAPSHOT or READ COMMITTED SNAPSHOT isolation modes may delay ADR PVS cleanup in other databases, causing the PVS file to grow. For more information, see the section on long active snapshot scan(s) in [Troubleshoot accelerated database recovery](accelerated-database-recovery-troubleshoot.md#pvs-active-snapshot-scans). This applies to instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and [!INCLUDE[ssazuremi_md](../includes/ssazuremi_md.md)], or in an [!INCLUDE[ssSDSfull](../includes/sssdsfull-md.md)] elastic pool.

## Best practices for accelerated database recovery

This section contains guidance and recommendations for ADR. 

- For SQL Server, isolate the PVS version store to a [filegroup on higher tier storage](#managing-the-persistent-version-store-filegroup), such as high-end SSD or advanced SSD or Persistent Memory (PMEM), sometimes referred to as Storage Class Memory (SCM). For more information, see [Change the location of the PVS to a different filegroup](#change-the-location-of-the-pvs-to-a-different-filegroup). This option is not available for [!INCLUDE[ssSDSfull](../includes/sssdsfull-md.md)] and [!INCLUDE[ssazuremi_md](../includes/ssazuremi_md.md)].

- Ensure there is sufficient space on the database to account for PVS usage. If the database does not have enough room for the PVS to grow, ADR will fail to generate versions. ADR saves space in the version store compared to `tempdb` version store. 

- Avoid multiple long-running transactions in the database. Though one objective of ADR is to speed up database recovery due to redo, multiple long-running transactions can delay version cleanup and increase the size of the PVS.

- Avoid large transactions with data definition changes or DDL operations. ADR uses a SLOG (system log stream) mechanism to track DDL operations used in recovery. The SLOG is only used while the transaction active. SLOG is checkpointed, so avoiding large transactions that use SLOG can help overall performance. These scenarios can cause the SLOG to take up more space:

   - Many DDLs are executed in one transaction. For example, in one transaction, rapidly creating and dropping temp tables. 

   - A table has large number of partitions/indexes that are modified. For example, a DROP TABLE operation on such table would require a large reservation of SLOG memory, which would delay truncation of the transaction log and delay undo/redo operations. The workaround can be to drop the indexes individually and gradually, then drop the table. For more information on the SLOG, see [ADR recovery components](accelerated-database-recovery-concepts.md#adr-recovery-components).

- Prevent or reduce unnecessary aborted situations. A high abort rate will put pressure on the PVS cleaner and lower ADR performance. The aborts may come from a high rate of deadlocks, duplicate keys, or other constraint violations.  

    - The `sys.dm_tran_aborted_transactions` DMV shows all aborted transactions on the SQL Server instance. The `nested_abort` column indicates that the transaction committed but there are portions that aborted (savepoints or nested transactions) which can block the PVS cleanup process. For more information, see [sys.dm_tran_aborted_transactions (Transact-SQL)](system-dynamic-management-views/sys-dm-tran-aborted-transactions.md). 

## Enabling and controlling ADR

> [!NOTE]
> In [!INCLUDE[ssSDSfull](../includes/sssdsfull-md.md)] and [!INCLUDE[ssazuremi_md](../includes/ssazuremi_md.md)], accelerated database recovery (ADR) is enabled on all databases and cannot be disabled or moved to a different filegroup.

ADR is off by default in [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)], and can be controlled using DDL syntax:

```syntaxsql
ALTER DATABASE [DB] SET ACCELERATED_DATABASE_RECOVERY = {ON | OFF};
```

Use this syntax to control whether the feature is on or off, and designate a specific filegroup for the *persistent version store* (PVS) data. If no filegroup is specified, the PVS will be stored in the PRIMARY filegroup. 

An exclusive lock is necessary on the database to change this state.  That means that the ALTER DATABASE command will stall until all active sessions are gone, and that any new sessions will wait behind the ALTER DATABASE command.  If it is important to complete the operation and remove the lock, you can use the termination clause, `WITH ROLLBACK [IMMEDIATE | AFTER {number} SECONDS | NO_WAIT]` to abort any active sessions in the database. For more information, see [ALTER DATABASE set options](../t-sql/statements/alter-database-transact-sql-set-options.md).

## Managing the persistent version store filegroup

The ADR feature is based on having changes versioned, with different versions of a data element kept in the PVS. There are considerations to locating where the PVS is located and in how to manage the size of the data in the PVS.

### To enable ADR without specifying a filegroup

This operation requires exclusive access to the database.

```sql
ALTER DATABASE [MyDatabase] SET ACCELERATED_DATABASE_RECOVERY = ON;
GO
```

In this case, when the PVS filegroup is not specified, the `PRIMARY` filegroup holds the PVS data.

### To enable ADR and specify that the PVS should be stored in a filegroup

You can configure ADR to use another filegroup, aside from the default `PRIMARY` filegroup, to hold PVS data.

Before enabling ADR in a filegroup besides `PRIMARY`, you must create the filegroup and data file. 

Create the `VersionStoreFG` filegroup and create a new data file in the filegroup. For example:

```sql
ALTER DATABASE [MyDatabase] ADD FILEGROUP [VersionStoreFG];
GO
ALTER DATABASE [MyDatabase] ADD FILE ( NAME = N'VersionStoreFG'
, FILENAME = N'E:\DATA\VersionStore.ndf'
, SIZE = 8192KB , FILEGROWTH = 65536KB )
TO FILEGROUP [VersionStoreFG];
GO
```

Only after the filegroup and a secondary data file have been created, enable ADR and specify that the PVS should be stored in a specific filegroup. This operation requires exclusive access to the database. 

```sql
ALTER DATABASE [MyDatabase] SET ACCELERATED_DATABASE_RECOVERY = ON
(PERSISTENT_VERSION_STORE_FILEGROUP = [VersionStoreFG]);
```

### To disable the ADR feature

```sql
ALTER DATABASE [MyDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF;
GO
```

Even after the ADR feature is disabled, there will be versions stored in the persistent version store that are still needed by the system for logical revert.

### Change the location of the PVS to a different filegroup

In SQL Server, you may need to move the location of the PVS to a different filegroup for a variety of reasons. For example, PVS may require more space, or faster storage. 

Changing the location of the PVS is a three-step process.

1. Turn off the ADR feature.

   ```sql
   ALTER DATABASE [MyDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF;
   GO
   ```

2. Wait until all of the versions stored in the PVS can be freed.

   In order to be able to turn on ADR with a new location for the persistent version store, you must first make sure that all of the version information has been purged from the previous PVS location. In order to force that cleanup to happen, run the command:

   ```sql
   EXEC sys.sp_persistent_version_cleanup [database name];
   ```

   `sys.sp_persistent_version_cleanup` stored procedure is synchronous, meaning that it will not complete until all version information is cleaned up from the current PVS.  Once it completes, you can verify that the version information is indeed removed by querying the DMV `sys.dm_persistent_version_store_stats` and examining the value of `persistent_version_store_size_kb`.

   ```sql
   SELECT DB_Name(database_id), persistent_version_store_size_kb 
   FROM sys.dm_tran_persistent_version_store_stats where database_id = [MyDatabaseID];
   ```

   When the value of `persistent_version_store_size_kb` is `0`, you can re-enable the ADR feature, configuring the PVS to be located in the new filegroup.

3. Turn on ADR, specifying the new location for the PVS:

   ```sql
   ALTER DATABASE [MyDatabase] SET ACCELERATED_DATABASE_RECOVERY = ON
   (PERSISTENT_VERSION_STORE_FILEGROUP = [VersionStoreFG]);
   ```

## Next steps 

- [Accelerated database recovery concepts](accelerated-database-recovery-concepts.md)
- [Troubleshoot accelerated database recovery](accelerated-database-recovery-troubleshoot.md)
- [Accelerated Database Recovery in Azure SQL](/azure/azure-sql/accelerated-database-recovery)