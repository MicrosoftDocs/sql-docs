---
title: Offload Backups to Secondary Availability Group Replica
description: Learn about the different supported backup types when offloading backups to a secondary replica of an Always On availability group.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: dinethi, randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "backup priority"
  - "backup on secondary replicas"
  - "Availability Groups [SQL Server], availability replicas"
  - "Availability Groups [SQL Server], backup on secondary replicas"
  - "active secondary replicas [SQL Server], backup on"
  - "automated backup preference"
  - "Availability Groups [SQL Server], active secondary replicas"
---
# Offload supported backups to secondary replicas of an availability group

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] active secondary capabilities include support for taking backups on secondary replicas. Backup operations can put significant strain on I/O and CPU (with backup compression). Offloading backups to a synchronized or synchronizing secondary replica allows you to use the resources on server instance that hosts the primary replica for your tier-1 workloads.

> [!NOTE]  
> `RESTORE` statements aren't allowed on either the primary or secondary databases of an availability group.

<a id="SupportedBuTypes"></a>

## Backup types supported on secondary replicas

To perform a full database backup on a secondary replica, you must take a [Copy-only backups](../../../relational-databases/backup-restore/copy-only-backups-sql-server.md), since copy-only backups don't impact the log chain or clear the differential bitmap. Consider:

- Copy-only backups don't prevent the truncation of the transaction log on other replicas.

- A copy-only backup prevents log truncation on the secondary replica while it's executing the copy-only backup, for the duration of the backup.

- If the transaction log is truncated on the primary replica to an LSN that is between the first and last LSN of the transaction log of the secondary replica executing the copy-only backup, you might see the following error in the log of the secondary replica:

  `Error 9019: The virtual log file sequence 0x%08x at offset 0x%016I64x bytes in file '%ls' is active and cannot be overwritten with sequence 0x%08x for database '%ls'.`

  While the backup is likely to succeed, synchronization fails for that secondary replica until the copy-only backup completes, and, if the secondary replica is set to synchronous commit, write workloads on the primary replica might be blocked until the log can harden on the secondary replica. After the backup completes, the log is truncated on the secondary replica, at which point it should synchronize again. If you encounter error 9019 while running a copy-only backup on a secondary replica, then run the full backup on the primary replica instead.

When you perform backups on secondary replicas, consider:

- To back up a secondary database, a secondary replica must be able to communicate with the primary replica, and must be `SYNCHRONIZED` or `SYNCHRONIZING`.

- Differential backups aren't supported on secondary replicas.

- Concurrent backups, such as executing a transaction log backup on the primary replica while a full database backup is executing on the secondary replica, is currently not supported.

- `BACKUP LOG` supports only regular log backups (the COPY_ONLY option isn't supported for log backups on secondary replicas). A consistent log chain is ensured across log backups taken on any of the replicas (primary or secondary), irrespective of their availability mode (synchronous-commit or asynchronous-commit).

In a [distributed availability group](distributed-availability-groups.md), you can perform backups on secondary replicas in the same availability group as the active primary replica, or on the primary replica of any secondary availability groups. Backups can't be performed on a secondary replica in a secondary availability group, because secondary replicas only communicate with the primary replica in their own availability group. Only replicas that communicate directly with the global primary replica can perform backup operations.

<a id="new-for-sql-server-2025"></a>

## Full and differential backups on secondary replicas

**Applies to:** [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] and later versions.

Starting with [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)], in addition to existing copy-only and transaction log backups, you can also perform full, and differential backups on any secondary replica.

<a id="WhereBuJobsRun"></a>

## Configure where backup jobs run

Performing backups on a secondary replica to offload the backup workload from the primary production server is a great benefit. However, performing backups on secondary replicas introduce significant complexity to the process of determining where backup jobs should run. To address this, configure where backup jobs run as follows:

1. Configure the availability group to specify which availability replicas where you would prefer backups to be performed. For more information, see `AUTOMATED_BACKUP_PREFERENCE` and `BACKUP_PRIORITY` parameters in [CREATE AVAILABILITY GROUP](../../../t-sql/statements/create-availability-group-transact-sql.md) or [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md).

1. Create scripted backup jobs for every availability database on every server instance that hosts an availability replica that is a candidate for performing backups. For more information, see the "Follow Up: After Configuring Backup on Secondary Replicas" section of [Configure backups on secondary replicas of an Always On availability group](configure-backup-on-availability-replicas-sql-server.md).

<a id="RelatedTasks"></a>

## Related content

- [Configure backups on secondary replicas of an Always On availability group](configure-backup-on-availability-replicas-sql-server.md)
- [sys.fn_hadr_backup_is_preferred_replica (Transact-SQL)](../../../relational-databases/system-functions/sys-fn-hadr-backup-is-preferred-replica-transact-sql.md)
- [Use the Maintenance Plan Wizard](../../../relational-databases/maintenance-plans/use-the-maintenance-plan-wizard.md)
- [Implement Jobs](/ssms/agent/implement-jobs)
- [What is an Always On availability group?](overview-of-always-on-availability-groups-sql-server.md)
- [Copy-only backups](../../../relational-databases/backup-restore/copy-only-backups-sql-server.md)
- [CREATE AVAILABILITY GROUP (Transact-SQL)](../../../t-sql/statements/create-availability-group-transact-sql.md)
- [ALTER AVAILABILITY GROUP (Transact-SQL)](../../../t-sql/statements/alter-availability-group-transact-sql.md)
