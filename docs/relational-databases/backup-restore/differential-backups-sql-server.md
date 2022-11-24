---
title: "Differential Backups (SQL Server)"
description: In SQL Server, a differential backup captures only data that has changed since the last full backup, which is the base of the differential backup.
ms.custom: ""
ms.date: 05/10/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords:
  - "differential backups"
  - "differential backups, about"
author: rwestMSFT
ms.author: randolphwest
---
# Differential backups (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This backup and restore article is relevant for all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.

A differential backup is based on the most recent, previous full data backup. A differential backup captures only the data that has changed since that full backup. The full backup upon which a differential backup is based is known as the *base* of the differential. Full backups, except for copy-only backups, can serve as the base for a series of differential backups, including database backups, partial backups, and file backups. The base backup for a file differential backup can be contained within a full backup, a file backup, or a partial backup.

## Benefits

Creating a differential backup can be much faster than creating a full backup. A differential backup records only the data that has changed since the full backup upon the differential backup is based. This facilitates taking frequent data backups, which decrease the risk of data loss. However, before you restore a differential backup, you must restore its base. Therefore restoring from a differential backup will necessarily take more steps and time than restoring from a full backup because two backup files are required.

Differential database backups are especially useful if a subset of a database is modified more frequently than the rest of the database. In these cases, differential database backups enable you back up frequently without the overhead of full database backups.

Under the full recovery model, using differential backups can reduce the number of log backups that you have to restore.

## Overview of differential backups

A differential backup captures the state of any *extents* (collections of eight physically contiguous pages) that have changed between when the differential base was created and when the differential backup is created. This means that the size of a given differential backup depends on the amount of data that has changed since the base. Generally, the older a base is, the larger a new differential backup will be. In a series of differential backups, a frequently updated extent is likely to contain different data in each differential backup.

The following illustration shows how a differential backup works. The figure shows 24 data extents, 6 of which have changed. The differential backup contains only these six data extents. The differential backup operation relies on a bitmap page that contains a bit for every extent. For each extent updated since the base, the bit is set to 1 in the bitmap.

:::image type="content" source="media/differential-backups-sql-server/how-differential-backups-work.png" alt-text="Diagram of how the differential bitmap identifies changed extents.":::

> [!NOTE]  
> The differential bitmap is not updated by a copy-only backup. Therefore, a copy-only backup does not affect subsequent differential backups.

A differential backup that is taken fairly soon after its base can be significantly smaller than the differential base. This saves storage space and backup time. However, as a database changes over time, the difference between the database and a specific differential base increases. The longer the time between a differential backup and its base, the larger the differential backup is likely to be. This means that the differential backups can eventually approach the differential base in size. A large differential backup loses the advantages of a faster and smaller backup.

As the differential backups increase in size, restoring a differential backup can significantly increase the time that is required to restore a database. Therefore, we recommend that you take a new full backup at set intervals to establish a new differential base for the data. For example, you might take a weekly full backup of the whole database (that is, a full database backup) followed by a regular series of differential database backups during the week.

At restore time, before you restore a differential backup, you must restore its base. Then, restore only the most recent differential backup to bring the database forward to the time when that differential backup was created. Typically, you would restore the most recent full backup followed by the most recent differential backup that is based on that full backup.

## Differential backups of databases with memory-optimized tables

For information about differential backups and databases with memory-optimized tables, see [Backing Up a Database with Memory-Optimized Tables](../../relational-databases/in-memory-oltp/backing-up-a-database-with-memory-optimized-tables.md).

## Differential backups of read-only databases

For read-only databases, full backups used alone are easier to manage than when they're used with differential backups. When a database is read-only, backup and other operations can't change the metadata that is contained in the file. Therefore, metadata that is required by a differential backup, such as the log sequence number at which the differential backup begins (the differential base LSN) is stored in the `master` database. If the differential base is taken when the database is read-only, the differential bitmap indicates more changes than have occurred since the base backup. The extra data is read by backup, but isn't written to the backup, because the `differential_base_lsn` stored in the [backupset](../../relational-databases/system-tables/backupset-transact-sql.md) system table is used to determine whether the data has changed since the base.

When a read-only database is rebuilt, restored, or detached and attached, the differential-base information is lost. This occurs because the `master` database isn't synchronized with the user database. The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] can't detect or prevent this problem. Any later differential backups aren't based on the most recent full backup and could provide unexpected results. To establish a new differential base, we recommend that you create a full database backup.

### Best practices for using differential backups with a read-only database

After you create a full database backup of a read-only database, if you intend to create a subsequent differential backup, back up the `master` database.

If the `master` database is lost, restore it before you restore any differential backup of a user database.

If you detach and attach a read-only database for which you plan to later use differential backups, as soon as it's practical, take a full database backup of both the read-only database and of the `master` database.

## Related tasks

- [Create a Differential Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-differential-database-backup-sql-server.md)

- [Restore a Differential Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-differential-database-backup-sql-server.md)

## See also

- [Backup Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-overview-sql-server.md)
- [Full Database Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/full-database-backups-sql-server.md)
- [Complete Database Restores &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-full-recovery-model.md)
- [Complete Database Restores &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-simple-recovery-model.md)
- [Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md)
