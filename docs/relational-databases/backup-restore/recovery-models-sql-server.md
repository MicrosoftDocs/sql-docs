---
title: "Recovery Models (SQL Server)"
description: Learn about SQL Server recovery models, which control how to log transactions, whether the transaction log requires backing up, and what restore operations are available.
author: dnethi
ms.author: dinethi
ms.reviewer: vanto
ms.date: 02/19/2026
ms.service: sql
ms.subservice: backup-restore
ms.topic: concept-article
ms.update-cycle: 1825-days
helpviewer_keywords:
  - "database backups [SQL Server], recovery models"
  - "bulk-logged recovery model [SQL Server]"
  - "recovery [SQL Server], recovery model"
  - "restoring transaction logs [SQL Server], recovery models"
  - "backing up databases [SQL Server], recovery models"
  - "recovery models [SQL Server], about"
  - "transaction log backups [SQL Server]"
  - "simple recovery model [SQL Server]"
  - "backups [SQL Server], recovery models"
  - "recovery models [SQL Server]"
  - "recovery models [SQL Server], transaction log management"
  - "database restores [SQL Server], recovery models"
  - "transaction log restores [SQL Server]"
  - "logs [SQL Server], recovery models"
  - "restoring databases [SQL Server], recovery models"
  - "full recovery model [SQL Server]"
  - "backing up transaction logs [SQL Server], recovery models"
---
# Recovery models (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup and restore operations happen in the context of the *recovery model* of the database. Recovery models define and control transaction log maintenance. A recovery model is a database property that controls how transactions are logged, whether the transaction log requires (and allows) backing up, and what kinds of restore operations are available.

Three recovery models are available:

- [Simple recovery model](#simple-recovery-model)
- [Full recovery model](#full-recovery-model)
- [Bulk-logged recovery model](#bulk-logged-recovery-model)

Typically, a database uses the *full* recovery model or *simple* recovery model. You can switch a database to another recovery model at any time.

> [!NOTE]  
> [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Enterprise and Standard editions use the *full* recovery model by default, while [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Express edition uses the *simple* recovery model by default.

## Recovery model overview

The following section summarizes the three recovery models.

### Simple recovery model

The simple recovery model doesn't support transaction log backups.

The [!INCLUDE [ssde-md](../../includes/ssde-md.md)] automatically reclaims log space to keep space requirements low, so you don't need to manage the transaction log space. For information about database backups under the simple recovery model, see [Full database backups (SQL Server)](full-database-backups-sql-server.md).

The simple recovery model doesn't support operations that require transaction log backups.

You can't use the following features with the simple recovery model:

- Log shipping
- Always On availability groups and database mirroring
- Media recovery without data loss
- Point-in-time restores

### Full recovery model

The full recovery model requires transaction log backups.

> [!NOTE]  
> In this recovery model, the transaction log continues to grow until you perform a transaction log backup.

No work is lost due to a lost or damaged data file. You can recover to an arbitrary point in time (for example, before an application or user error).

For information about database backups under the full recovery model, see [Full database backups (SQL Server)](full-database-backups-sql-server.md) and [Complete database restores (full recovery model)](complete-database-restores-full-recovery-model.md).

### Bulk-logged recovery model

The bulk-logged recovery model requires transaction log backups.

> [!NOTE]  
> In this recovery model, the transaction log continues to grow until you perform a transaction log backup.

A variant of the full recovery model that permits high-performance bulk copy operations.

Reduces log space usage by using minimal logging for most bulk operations. For information about operations that can be minimally logged, see [The transaction log](../logs/the-transaction-log-sql-server.md).

Log backups might be large because the minimally logged operations are captured in the log backup. For information about database backups under the bulk-logged recovery model, see [Full database backups (SQL Server)](full-database-backups-sql-server.md) and [Complete database restores (full recovery model)](complete-database-restores-full-recovery-model.md).

## Recovery time and recovery point objectives

The following table describes the effect of each recovery model on the recovery time objective (RTO), indicated by the **Can recover to point in time** column, and the recovery point objective (RPO), indicated by the **Work loss exposure** column.

| Recovery model | Work loss exposure (RPO) | Recover to point in time? (RTO) |
| --- | --- | --- |
| **Simple** | Changes since the most recent backup aren't protected. If there's a disaster, those changes must be redone. | Can recover only to the end of a backup. For more information, see [Complete database restores (simple recovery model)](complete-database-restores-simple-recovery-model.md). |
| **Full** | Normally none.<br /><br />If the tail of the log is damaged, changes since the most recent log backup must be redone. | Can recover to a specific point in time, assuming that your backups are complete up to that point in time. For information about using log backups to restore to the point of failure, see [Restore a SQL Server database to a point in time (full recovery model)](restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md). |
| **Bulk-logged** | If the log is damaged or if bulk-logged operations occurred since the most recent log backup, changes since that last backup must be redone. Otherwise, no work is lost. | Can recover to the end of any backup. Point-in-time recovery isn't supported. |

> [!NOTE]  
> If you have two or more related databases in the full recovery model that must be logically consistent, you might have to implement special procedures to ensure the recoverability of these databases. For more information, see [Recovery of related databases that contain marked transaction](recovery-of-related-databases-that-contain-marked-transaction.md).

## Related tasks

- [View or change the recovery model of a database (SQL Server)](view-or-change-the-recovery-model-of-a-database-sql-server.md)
- [Troubleshoot a full transaction log (SQL Server Error 9002)](../logs/troubleshoot-a-full-transaction-log-sql-server-error-9002.md)

## Related content

- [backupset (Transact-SQL)](../system-tables/backupset-transact-sql.md)
- [sys.databases (Transact-SQL)](../system-catalog-views/sys-databases-transact-sql.md)
- [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [Back up and restore of SQL Server databases](back-up-and-restore-of-sql-server-databases.md)
- [The transaction log](../logs/the-transaction-log-sql-server.md)
- [Automated administration tasks (SQL Server Agent)](/ssms/agent/automated-administration-tasks-sql-server-agent)
- [Restore and recovery overview (SQL Server)](restore-and-recovery-overview-sql-server.md)
