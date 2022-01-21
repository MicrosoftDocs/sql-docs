---
description: "Manage accelerated database recovery"
title: "Manage accelerated database recovery | Microsoft Docs"
ms.date: "02/02/2021"
ms.prod: sql
ms.prod_service: backup-restore
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "accelerated database recovery [SQL Server], recovery-only"
  - "database recovery [SQL Server]"
author: mashamsft
ms.author: mathoma
ms.reviewer: kfarlee
monikerRange: ">=sql-server-ver15"
---
# Manage accelerated database recovery

[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver2019.md)]

## Enabling and controlling ADR

ADR is off by default in [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)], and can be controlled using DDL syntax:
```syntaxsql
ALTER DATABASE [DB] SET ACCELERATED_DATABASE_RECOVERY = {ON | OFF}
[(PERSISTENT_VERSION_STORE_FILEGROUP = { filegroup name }) ];

```

Use this syntax to control whether the feature is on or off, and designate a specific filegroup for the *persistent version store* (PVS) data. If no filegroup is specified, the PVS will be stored in the PRIMARY filegroup.

Note that SQL will take an exclusive lock on the database to change this state.  That means that the ALTER DATABASE command will stall until all active sessions are gone, and that any new sessions will wait behind the ALTER DATABASE command.  If it is important to complete the operation and remove the lock, you can use the termination clause, WITH ROLLBACK [IMMEDIATE | AFTER {number} SECONDS | NO_WAIT] to abort any active sessions in the database.

## Managing the persistent version store Filegroup
The ADR feature is based on having changes versioned, with different versions of a data element kept in the PVS.
There are considerations to locating where the PVS is located and in how to manage the size of the data in the PVS.

### To enable ADR without specifying a filegroup

```sql
ALTER DATABASE [MyDatabase] SET ACCELERATED_DATABASE_RECOVERY = ON;
GO
```

In this case, when the PVS filegroup is not specified, the `PRIMARY` filegroup holds the PVS data.

### To enable ADR and specify that the PVS should be stored in the [VersionStoreFG] filegroup

Before running this script, create the filegroup.

```sql
ALTER DATABASE [MyDatabase] SET ACCELERATED_DATABASE_RECOVERY = ON
(PERSISTENT_VERSION_STORE_FILEGROUP = [VersionStoreFG])
```

### To disable the ADR feature

```sql
ALTER DATABASE [MyDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF;
GO
```

Even after the ADR feature is disabled, there will be versions stored in the persistent version store that are still needed by the system for logical revert.

### Change the location of the PVS to a different filegroup

You may need to move the location of the PVS to a different filegroup for a variety of reasons. For example, PVS may require more space, or faster storage.

Changing the location of the PVS is a three-step process.

1. Turn the ADR feature off.

   ```sql
   ALTER DATABASE [MyDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF;
   GO
   ```

2. Wait until all of the versions stored in the PVS can be freed

   In order to be able to turn on ADR with a new location for the persistent version store, you must first make sure that all of the version information has been purged from the previous PVS location. In order to force that cleanup to happen, run the command:

   ```sql
   EXEC sys.sp_persistent_version_cleanup [database name]
   ```

   `sys.sp_persistent_version_cleanup` stored procedure is synchronous, meaning that it will not complete until all version information is cleaned up from the current PVS.  Once it completes, you can verify that the version information is indeed removed by querying the DMV `sys.dm_persistent_version_store_stats` and examining the value of `persistent_version_store_size_kb`.

   ```sql
   SELECT DB_Name(database_id), persistent_version_store_size_kb 
   FROM sys.dm_tran_persistent_version_store_stats where database_id = [MyDatabaseID]
   ```

   When the value of persistent_version_store_size_kb is 0, you can re-enable the ADR feature, configuring the PVS to be located in the new filegroup.

1. Turn on ADR specifying the new location for PVS

   ```sql
   ALTER DATABASE [MyDatabase] SET ACCELERATED_DATABASE_RECOVERY = ON
   (PERSISTENT_VERSION_STORE_FILEGROUP = [VersionStoreFG])
   ```

## Best practices for ADR

This section contains guidance and recommendations for ADR. 

> [!NOTE]
> In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)], accelerated database recovery (ADR) is enabled on all databases and cannot be disabled. If you observe issues either with storage usage, high abort transaction and other factors, please contact [Azure Support](https://azure.microsoft.com/support/options/). 

1. ADR is not recommended for databases larger than 100 terabytes due to the single-threaded PVS version cleaner.  

2. ADR is not recommended for database environments with a high count of update/deletes, such as high-volume OLTP, without a period of rest/recovery for the PVS cleanup process to reclaim space. Typically, business operation cycles allow for this time, but in some scenarios you may want to initiate the PVS cleanup process manually to take advantage of application activity conditions.

- To activate the PVS cleanup process manually between workloads or during maintenance windows, use `sys.sp_persistent_version_cleanup`. For more information, see [sys.sp_persistent_version_cleanup](../../relational-databases/system-stored-procedures/sys-sp-persistent-version-cleanup-transact-sql.md). 

- If the PVS cleanup process is running for a long period time, you may find that the count of aborted transactions will grow which will also cause the PVS size to increase. Leverage the `sys.dm_tran_aborted_transactions` DMV to report the aborted transaction count, and leverage `sys.dm_tran_persistent_version_store_stats` to report the cleanup start/end times along with the PVS size.

3. If your application performs many non-batched, incremental updates, such as updating a record every time there is a row accessed/inserted, your workload may not be optimal for ADR. Consider rewriting the application queries to batch updates, where possible, until the end of the command and reduce a high number of small update transactions.

4. For SQL Server, isolate the PVS version store to a filegroup on higher tier storage, such as high-end SSD or advanced SSD or Persistent Memory (PMEM), sometimes referred to as Storage Class Memory (SCM). For more information, see the previous section on how to [Change the location of the PVS to a different filegroup](#change-the-location-of-the-pvs-to-a-different-filegroup).

5. Ensure there is sufficient space on the database to account for PVS usage.

For more troubleshooting options, see [Troubleshoot accelerated database recovery](accelerated-database-recovery-troubleshooting.md).

## Next steps 

- [Troubleshoot accelerated database recovery](accelerated-database-recovery-troubleshooting.md)