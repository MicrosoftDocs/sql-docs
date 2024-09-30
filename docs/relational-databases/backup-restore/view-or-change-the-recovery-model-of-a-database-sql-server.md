---
title: "Set database recovery model"
description: Learn how to switch a SQL Server database from one recovery model to another by using SQL Server Management Studio or Transact-SQL.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 09/27/2024
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords:
  - "database backups [SQL Server], recovery models"
  - "recovery [SQL Server], recovery model"
  - "backing up databases [SQL Server], recovery models"
  - "recovery models [SQL Server], switching"
  - "recovery models [SQL Server], viewing"
  - "database restores [SQL Server], recovery models"
  - "modifying database recovery models"
---
# View or change the recovery model of a database (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to view or change the database recovery model by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

A *recovery model* is a database property that controls how transactions are logged, whether the transaction log requires (and allows) backing up, and what kinds of restore operations are available. Three recovery models exist: simple, full, and bulk-logged. Typically, a database uses the full recovery model or simple recovery model. A database can be switched to another recovery model at any time. The `model` database sets the default recovery model of new databases.

For an in-depth explanation, see [recovery models](recovery-models-sql-server.md).

## <a id="BeforeYouBegin"></a> Before you begin

- [Back up the transaction log](back-up-a-transaction-log-sql-server.md) **before** switching from the [full recovery or bulk-logged recovery model](recovery-models-sql-server.md).

- Point-in-time recovery isn't possible with bulk-logged model. Running transactions under the bulk-logged recovery model that require a transaction log restore, can expose them to data loss. To maximize data recoverability in a disaster-recovery scenario, switch to the bulk-logged recovery model only under the following conditions:

  - Users are currently not allowed in the database.

  - All modifications made during bulk processing are recoverable without depending on taking a log backup; for example, by rerunning the bulk processes.

    If you satisfy these two conditions, you aren't exposed to any data loss while restoring a transaction log that was backed up under the bulk-logged recovery model.

  If you switch to the full recovery model during a bulk operation, bulk operations logging changes from *minimal logging* to *full logging*, and vice versa.

## Permissions

Requires ALTER permission on the database.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. After connecting to the appropriate instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, select the server name to expand the server tree.

1. Expand **Databases**, and, depending on the database, either select a user database or expand **System Databases** and select a system database.

1. Right-click the database, and then select **Properties**, which opens the **Database Properties** dialog box.

1. In the **Select a page** pane, select **Options**.

1. The current recovery model is displayed in the **Recovery model** list box.

1. Optionally, to change the recovery model select a different model list. The choices are **Full**, **Bulk-logged**, or **Simple**.

1. Select **OK**.

> [!NOTE]  
> Plan cache entries for the database will be flushed or cleared.

## <a id="TsqlProcedure"></a> Use Transact-SQL

#### View the recovery model

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to query the [sys.databases](../system-catalog-views/sys-databases-transact-sql.md) catalog view to learn the recovery model of the `model` database.

```sql
SELECT name, recovery_model_desc
FROM sys.databases
WHERE name = 'model';
GO
```

#### Change the recovery model

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to change the recovery model in the `model` database to `FULL` by using the `SET RECOVERY` option of the [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql-set-options.md) statement.

```sql
USE [master];
GO
ALTER DATABASE [model]
SET RECOVERY FULL;
GO
```

> [!NOTE]  
> Plan cache entries for the database will be flushed or cleared.

## <a id="FollowUp"></a> Recommendations: After you change the recovery model

#### After switching between the full and bulk-logged recovery models

- After completing the bulk operations, immediately switch back to the full recovery model.

- After switching from the bulk-logged recovery model back to the full recovery model, back up the log.

Your backup strategy remains the same: continue performing periodic database, log, and differential backups.

#### After switching from the simple recovery model

- Immediately after switching to the full recovery model or bulk-logged recovery model, take a full or differential database backup to start the log chain.

  The switch to the full or bulk-logged recovery model takes effect only after the first data backup.

- Schedule regular log backups, and update your restore plan accordingly.

  > [!IMPORTANT]  
  > **Back up your logs**. If you do not back up the log frequently enough, the transaction log can expand until it runs out of disk space.

#### After switching to the simple recovery model

- Discontinue any scheduled jobs for backing up the transaction log.

- Ensure periodic database backups are scheduled. Backing up your database is essential both to protect your data and to truncate the inactive portion of the transaction log.

## Related content

- [Recovery models (SQL Server)](recovery-models-sql-server.md)
- [The transaction log](../logs/the-transaction-log-sql-server.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [sys.databases (Transact-SQL)](../system-catalog-views/sys-databases-transact-sql.md)
- [Maintenance plans](../maintenance-plans/maintenance-plans.md)
- [Create a Full Database Backup](create-a-full-database-backup-sql-server.md)
- [Back up a transaction log](back-up-a-transaction-log-sql-server.md)
- [Create a Job](../../ssms/agent/create-a-job.md)
- [Disable or Enable a Job](../../ssms/agent/disable-or-enable-a-job.md)
