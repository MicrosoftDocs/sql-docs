---
title: "Recovery models (SQL Server)"
description: In SQL Server, a recovery model controls how to log transactions, whether the transaction log requires backing up, and what restore operations are available.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 07/25/2024
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
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

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup and restore operations occur within the context of the *recovery model* of the database. Recovery models are designed to control transaction log maintenance. A recovery model is a database property that controls how transactions are logged, whether the transaction log requires (and allows) backing up, and what kinds of restore operations are available.

Three recovery models exist: *simple*, *full*, and *bulk-logged*. Typically, a database uses the full recovery model or simple recovery model. A database can be switched to another recovery model at any time.

## Recovery model overview

The following table summarizes the three recovery models.

| Recovery model | Description | Work loss exposure | Recover to point in time? |
| --- | --- | --- | --- |
| **Simple** | No log backups.<br /><br />Automatically reclaims log space to keep space requirements small, essentially eliminating the need to manage the transaction log space. For information about database backups under the simple recovery model, see [Full Database Backups (SQL Server)](full-database-backups-sql-server.md).<br /><br />Operations that require transaction log backups aren't supported by the simple recovery model.<br /><br />The following features can't be used in the simple recovery model:<br /><br />- Log shipping<br />- Always On or Database mirroring<br />- Media recovery without data loss<br />- Point-in-time restores | Changes since the most recent backup are unprotected. In the event of a disaster, those changes must be redone. | Can recover only to the end of a backup. For more information, see [Complete Database Restores (Simple Recovery Model)](complete-database-restores-simple-recovery-model.md). |
| **Full** | Requires log backups.<br /><br />No work is lost due to a lost or damaged data file. Can recover to an arbitrary point in time (for example, before application or user error). For information about database backups under the full recovery model, see [Full Database Backups (SQL Server)](full-database-backups-sql-server.md) and [Complete Database Restores (Full Recovery Model)](complete-database-restores-full-recovery-model.md). | Normally none.<br /><br />If the tail of the log is damaged, changes since the most recent log backup must be redone. | Can recover to a specific point in time, assuming that your backups are complete up to that point in time. For information about using log backups to restore to the point of failure, see [Restore a SQL Server Database to a Point in Time (Full Recovery Model)](restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md).<br /><br />**Note:** If you have two or more full-recovery-model databases that must be logically consistent, you might have to implement special procedures to make sure the recoverability of these databases. For more information, see [Recovery of Related Databases That Contain Marked Transaction](recovery-of-related-databases-that-contain-marked-transaction.md). |
| **Bulk logged** | Requires log backups.<br /><br />An adjunct of the full recovery model that permits high-performance bulk copy operations.<br /><br />Reduces log space usage by using minimal logging for most bulk operations. For information about operations that can be minimally logged, see [The Transaction Log (SQL Server)](../logs/the-transaction-log-sql-server.md).<br /><br />Log backups might be of a significant size because the minimally logged operations are captured in the log backup. For information about database backups under the bulk-logged recovery model, see [Full Database Backups (SQL Server)](full-database-backups-sql-server.md) and [Complete Database Restores (Full Recovery Model)](complete-database-restores-full-recovery-model.md). | If the log is damaged or bulk-logged operations occurred since the most recent log backup, changes since that last backup must be redone. Otherwise, no work is lost. | Can recover to the end of any backup. Point-in-time recovery isn't supported. |

## Related tasks

- [View or Change the Recovery Model of a Database (SQL Server)](view-or-change-the-recovery-model-of-a-database-sql-server.md)
- [Troubleshoot a Full Transaction Log (SQL Server Error 9002)](../logs/troubleshoot-a-full-transaction-log-sql-server-error-9002.md)

## Related content

- [backupset (Transact-SQL)](../system-tables/backupset-transact-sql.md)
- [sys.databases (Transact-SQL)](../system-catalog-views/sys-databases-transact-sql.md)
- [ALTER DATABASE SET Options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [Back Up and Restore of SQL Server Databases](back-up-and-restore-of-sql-server-databases.md)
- [The Transaction Log (SQL Server)](../logs/the-transaction-log-sql-server.md)
- [Automated Administration Tasks (SQL Server Agent)](../../ssms/agent/automated-administration-tasks-sql-server-agent.md)
- [Restore and Recovery Overview (SQL Server)](restore-and-recovery-overview-sql-server.md)
