---
title: "Full database backups (SQL Server)"
description: In SQL Server, a full database backup backs up the whole database. Full database backups represent the database at the time the backup finished.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 12/01/2023
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords:
  - "full backups [SQL Server]"
  - "backups [SQL Server], database"
  - "backing up databases [SQL Server], full backups"
  - "estimating database backup size"
  - "backing up [SQL Server], size of backup"
  - "database backups [SQL Server], full backups"
  - "size [SQL Server], backups"
  - "database backups [SQL Server], about backing up databases"
---
# Full database backups (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

A full database backup backs up the whole database. This includes part of the transaction log so that the full database can be recovered after a full database backup is restored. Full database backups represent the database at the time the backup finished.

As a database increases in size, full database backups take more time to finish and require more storage space. Therefore, for a large database, you might want to supplement a full database backup with a series of *differential database backups*. For more information, see [Differential backups (SQL Server)](differential-backups-sql-server.md).

> [!IMPORTANT]  
> `TRUSTWORTHY` is set to OFF on a database backup. For information about how to set `TRUSTWORTHY` to `ON`, see [ALTER DATABASE SET Options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md).

## Database backups under the simple recovery model

Under the simple recovery model, after each backup, the database is exposed to potential work loss if a disaster were to occur. The work-loss exposure increases with each update until the next backup, when the work-loss exposure returns to zero and a new cycle of work-loss exposure starts. Work-loss exposure increases over time between backups. The following illustration shows the work-loss exposure for a backup strategy that uses only full database backups.

:::image type="content" source="media/bnr-rmsimple-1-fulldb-backups.gif" alt-text="Diagram showing the work-loss exposure between database backups.":::

### Example ([!INCLUDE [tsql](../../includes/tsql-md.md)])

The following example shows how to create a full database backup by using `WITH FORMAT` to overwrite any existing backups and create a new media set.

```sql
-- Back up the AdventureWorks2022 database to new media set.
BACKUP DATABASE AdventureWorks2022
    TO DISK = 'Z:\SQLServerBackups\AdventureWorksSimpleRM.bak'
    WITH FORMAT;
GO
```

## Database backups under the full recovery model

For databases that use full and bulk-logged recovery, database backups are necessary but not sufficient. Transaction log backups are also required. The following illustration shows the least complex backup strategy that is possible under the full recovery model.

:::image type="content" source="media/bnr-rmfull-1-fulldb-log-backups.gif" alt-text="Diagram showing the series of full database backups and log backups.":::

For information about how to create log backups, see [Transaction log backups (SQL Server)](transaction-log-backups-sql-server.md).

### Example ([!INCLUDE [tsql](../../includes/tsql-md.md)])

The following example shows how to create a full database backup by using `WITH FORMAT` to overwrite any existing backups and create a new media set. Then, the example backs up the transaction log. In a real-life situation, you would have to perform a series of regular log backups. For this example, the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database is set to use the full recovery model.

```sql
USE master;
GO
ALTER DATABASE AdventureWorks2022 SET RECOVERY FULL;
GO
-- Back up the AdventureWorks2022 database to new media set (backup set 1).
BACKUP DATABASE AdventureWorks2022
  TO DISK = 'Z:\SQLServerBackups\AdventureWorks2022FullRM.bak'
  WITH FORMAT;
GO
--Create a routine log backup (backup set 2).
BACKUP LOG AdventureWorks2022 TO DISK = 'Z:\SQLServerBackups\AdventureWorks2022FullRM.bak';
GO
```

## Use a full database backup to restore the database

You can re-create a whole database in one step by restoring the database from a full database backup to any location. Enough of the transaction log is included in the backup to let you recover the database to the time when the backup finished. The restored database matches the state of the original database when the database backup finished, minus any uncommitted transactions. Under the full recovery model, you should then restore all subsequent transaction log backups. When the database is recovered, uncommitted transactions are rolled back.

For more information, see [Complete Database Restores (Simple Recovery Model)](complete-database-restores-simple-recovery-model.md) or [Complete Database Restores (Full Recovery Model)](complete-database-restores-full-recovery-model.md).

## Related content

- [Create a Full Database Backup](create-a-full-database-backup-sql-server.md)
- <xref:Microsoft.SqlServer.Management.Smo.Backup.SqlBackup%2A> (SMO)
- [Use the Maintenance Plan Wizard](../maintenance-plans/use-the-maintenance-plan-wizard.md)
- [Back Up and Restore of SQL Server Databases](back-up-and-restore-of-sql-server-databases.md)
- [Backup overview (SQL Server)](backup-overview-sql-server.md)
- [Backup and Restore of Analysis Services Databases](/analysis-services/multidimensional-models/backup-and-restore-of-analysis-services-databases)
