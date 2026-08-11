---
title: File-Snapshot Backups for Database Files in Azure
description: SQL Server file-snapshot backup uses Azure snapshots to speed up backups and restores of database files stored in Azure Blob Storage.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/10/2026
ms.service: sql
ms.subservice: backup-restore
ms.topic: concept-article
---
# File-snapshot backups for database files in Azure

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] file-snapshot backup uses Azure snapshots to provide nearly instantaneous backups (and faster restores) of database files stored in Azure Blob Storage. Use file-snapshot backups to simplify your backup and restore policies.

For more information about storing database files in Azure Blob Storage, see [SQL Server data files in Microsoft Azure](../databases/sql-server-data-files-in-microsoft-azure.md).

:::image type="content" source="media/file-snapshot-backups-for-database-files-in-azure/snapshot-backup.png" alt-text="Diagram explaining the snapshot backup architecture." lightbox="media/file-snapshot-backups-for-database-files-in-azure/snapshot-backup.png":::

Already have an Azure account? Visit [SQL Server on Azure Virtual Machines](https://azure.microsoft.com/services/virtual-machines/sql-server/) to create a virtual machine that has [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] preinstalled.

## Use Azure snapshots to back up database files stored in Azure

### What is a SQL Server file-snapshot backup?

A file-snapshot backup is a set of Azure snapshots of the blobs that contain the database files, plus a backup file with pointers to those file-snapshots. Each file-snapshot resides in the container with the base blob.

You can write the backup file to URL, disk, or tape, but you should back up to URL whenever possible. For more information, see [BACKUP](../../t-sql/statements/backup-transact-sql.md) and [SQL Server backup to URL for Azure Blob Storage](sql-server-backup-to-url.md).

:::image type="content" source="media/file-snapshot-backups-for-database-files-in-azure/snapshot-backup-flat.png" alt-text="Diagram of the architecture of snapshot feature." lightbox="media/file-snapshot-backups-for-database-files-in-azure/snapshot-backup-flat.png":::

Deleting the base blob invalidates the backup set. You can't drop a blob that contains file-snapshots unless you explicitly choose to delete the blob along with all its file-snapshots. Dropping a database or a data file doesn't delete the base blob or any of its file-snapshots, and deleting the backup file doesn't delete any of the file-snapshots in the backup set. To delete a file-snapshot backup set, use the `sys.sp_delete_backup` system stored procedure.

**Full database backup**: A full file-snapshot backup takes an Azure snapshot of each data and log file in the database, establishes the transaction log backup chain, and writes the file-snapshot locations to the backup file.

**Transaction log backup**: A transaction log file-snapshot backup takes a file-snapshot of every database file (not just the transaction log), records the file-snapshot locations in the backup file, and truncates the transaction log.

> [!IMPORTANT]  
> After the initial full backup that establishes the transaction log backup chain (which can itself be a file-snapshot backup), you need only transaction log backups. Each transaction log file-snapshot backup set contains file-snapshots of every database file, so you can use it for either a database restore or a log restore. You don't need extra full or differential backups because Azure Blob Storage handles the differences between each file-snapshot and the current state of the base blob.

For a tutorial on using SQL Server with Azure Blob Storage, see [Tutorial: Use Azure Blob Storage with SQL Server](../tutorial-use-azure-blob-storage-service-with-sql-server.md).

### Restore using file-snapshot backups

Each file-snapshot backup set contains a file-snapshot of every database file, so a restore needs at most two adjacent file-snapshot backup sets. This requirement is true whether the backup set comes from a full database backup or a log backup. With traditional streaming backup, the restore process needs the full backup, a differential backup, and one or more transaction log backups. The recovery step is the same for both approaches.

**To the time of any backup set**: To restore a database to the time of a specific file-snapshot backup set, `RESTORE DATABASE` needs only that backup set and the base blobs. Because `RESTORE DATABASE` works with a transaction log file-snapshot backup set, you typically use a transaction log backup set and rarely use a full database backup set. See the example later in this article.

**To a point in time between two file-snapshot backup sets**: To restore a database to a specific point in time between two adjacent transaction log backup sets, `RESTORE DATABASE` needs only two transaction log backup sets: one from before the point in time to which you want to restore the database, and one from after. Run `RESTORE DATABASE ... WITH NORECOVERY` using the earlier backup set, and then run `RESTORE LOG ... WITH RECOVERY` using the later one. Use the `STOPAT` argument to specify the point in time at which to stop the recovery from the transaction log backup. See the example later in this article.

### File-backup set maintenance

**Deleting a file-snapshot backup set**: You can't overwrite a file-snapshot backup set with the `FORMAT` argument. The `FORMAT` argument isn't permitted because it can orphan file-snapshots from the original file-snapshot backup. To delete a backup set, use the `sys.sp_delete_backup` system stored procedure. It removes the backup file and every file-snapshot in the set. Other deletion methods might remove the backup file but leave the file-snapshots behind.

**Deleting orphaned backup file-snapshots**: You might end up with orphaned file-snapshots if you delete the backup file without `sys.sp_delete_backup`, or if you drop a database or data file while its blobs still have associated backup file-snapshots. To find candidates, use the `sys.fn_db_backup_file_snapshots` system function to list every file-snapshot for the database files. To see which file-snapshots belong to a specific backup set, use the `RESTORE FILELISTONLY` system stored procedure. Then remove an orphan with the `sys.sp_delete_backup_file_snapshot` system stored procedure. See the examples later in this article.

For more information, see:

- [sp_delete_backup](../system-stored-procedures/snapshot-backup-sp-delete-backup.md)
- [sys.fn_db_backup_file_snapshots](../system-functions/sys-fn-db-backup-file-snapshots-transact-sql.md)
- [sp_delete_backup_file_snapshot](../system-stored-procedures/snapshot-backup-sp-delete-backup-file-snapshot.md)
- [RESTORE Statements - FILELISTONLY](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md)

### Considerations and limitations

**Premium storage**: When you use premium storage, the following limitations apply:

- You can't store the backup file itself by using premium storage.
- You can't set the frequency of backups to be shorter than 10 minutes.
- You can store up to 100 snapshots.
- You must use `RESTORE WITH MOVE`.

- For more information about premium storage, see [Premium Storage: High-Performance Storage for Azure Virtual Machine Workloads](/azure/virtual-machines/disks-types).

**Single storage account**: The file-snapshot and destination blobs must use the same storage account.

**Bulk recovery model**: If you use the bulk-logged recovery model with a transaction log backup that contains minimally logged transactions, you can't restore the log (including point in time recovery) from that backup. Instead, restore the database to the time of the file-snapshot backup set. The same limitation applies to streaming backup.

**Online restore**: You can't perform an online restore from a file-snapshot backup. For more information about online restore, see [Online Restore (SQL Server)](online-restore-sql-server.md).

**Billing**: SQL Server file-snapshot backup adds charges as data changes. For more information, see [Understanding How Snapshots Accrue Charges](/rest/api/storageservices/Understanding-How-Snapshots-Accrue-Charges).

**Archival**: You can archive a file-snapshot backup to Azure Blob Storage or to streaming backup. For Azure Blob Storage, copy the snapshots in the file-snapshot backup set into separate blobs. For streaming backup, restore the file-snapshot backup as a new database, and then take a regular streaming backup with compression and/or encryption. You can keep the archive as long as desired, independent of the base blobs.

> [!IMPORTANT]  
> Maintaining multiple file-snapshot backups has only a small performance overhead, but maintaining too many can affect I/O performance on the database. Keep only the file-snapshot backups you need to meet your recovery point objective.

## Back up the database and log using a file-snapshot backup

The following example uses a file-snapshot backup to back up the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database to a URL.

```sql
-- To permit log backups, before the full database backup, modify the database

-- to use the full recovery model.
USE master;
GO

ALTER DATABASE AdventureWorks2025
    SET RECOVERY FULL;
GO

-- Back up the full AdventureWorks2025 database.
BACKUP DATABASE AdventureWorks2025
TO URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2025.bak'
WITH FILE_SNAPSHOT;
GO

-- Back up the AdventureWorks2025 log using a timestamp in the backup file name.
DECLARE @Log_Filename AS VARCHAR (300);

SET @Log_Filename = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2025_Log_'+
REPLACE (REPLACE (REPLACE (CONVERT (VARCHAR (40), GETDATE (), 120), '-','_'),':', '_'),' ', '_') + '.trn';

BACKUP LOG AdventureWorks2025
 TO URL = @Log_Filename WITH FILE_SNAPSHOT;
GO
```

## Restore from a SQL Server file-snapshot backup

The following example restores the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database by using a transaction log file-snapshot backup set, and shows a recovery operation. You can restore a database from a single transaction log file-snapshot backup set.

```sql
RESTORE DATABASE AdventureWorks2025 FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2025_2026_05_18_16_00_00.trn'
    WITH RECOVERY, REPLACE;
GO
```

## Restore from a SQL Server file-snapshot backup to a point in time

The following example uses two transaction log file-snapshot backup sets to restore the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database to its state at a specified point in time, and shows a recovery operation.

```sql
RESTORE DATABASE AdventureWorks2025
    FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2025_2026_05_18_16_00_00.trn'
    WITH NORECOVERY,
         REPLACE;
GO

RESTORE LOG AdventureWorks2025
    FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2025_2026_05_18_18_00_00.trn'
    WITH RECOVERY,
         STOPAT = 'May 18, 2026 5:35 PM';
GO
```

## Delete a database file-snapshot backup set

To delete a file-snapshot backup set, use the `sys.sp_delete_backup` system stored procedure. Include the database name so the procedure verifies that the backup set is a backup for that database. If you omit the database name, the procedure skips that check and deletes the backup set and its file-snapshots. For more information, see [sp_delete_backup](../system-stored-procedures/snapshot-backup-sp-delete-backup.md).

> [!WARNING]  
> If you delete a file-snapshot backup set another way, for example, from the Azure portal or the Azure Storage node in SQL Server Management Studio's Object Explorer, the file-snapshots stay behind. Those tools only remove the backup file, which holds the pointers to the file-snapshots. To find leftover file-snapshots, use the `sys.fn_db_backup_file_snapshots` system function, and then delete each one with the `sys.sp_delete_backup_file_snapshot` system stored procedure.

The following example deletes a file-snapshot backup set, including its backup file and every file-snapshot in the set.

```sql
EXECUTE sys.sp_delete_backup
    'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2025.bak',
    'AdventureWorks2025';
GO
```

## View database backup file snapshots

To list the file snapshots of the base blob for each database file, use the `sys.fn_db_backup_file_snapshots` system function. You can find backup file snapshots that remain when something other than `sys.sp_delete_backup` deletes the backup file for a file-snapshot backup set. To see which file snapshots belong to an intact backup set, use `RESTORE FILELISTONLY` to list the file snapshots that each backup file references. For more information, see [sys.fn_db_backup_file_snapshots](../system-functions/sys-fn-db-backup-file-snapshots-transact-sql.md) and [RESTORE Statements - FILELISTONLY](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md).

The following example lists every backup file snapshot for a database.

```sql
--Either specify the database name or set the database context
USE AdventureWorks2025;

SELECT *
FROM sys.fn_db_backup_file_snapshots(NULL);
GO

SELECT *
FROM sys.fn_db_backup_file_snapshots('AdventureWorks2025');
GO
```

## Delete an individual database backup file snapshot

To delete an individual backup file snapshot of a database base blob, use the `sys.sp_delete_backup_file_snapshot` system stored procedure. Use it mainly to clean up orphaned file snapshots that remain when a process other than `sys.sp_delete_backup` deletes the backup file. For more information, see [sp_delete_backup_file_snapshot](../system-stored-procedures/snapshot-backup-sp-delete-backup-file-snapshot.md).

> [!WARNING]  
> Deleting an individual file snapshot that belongs to a file snapshot backup set invalidates the backup set.

The following example deletes a backup file snapshot. You can get the URL from `sys.fn_db_backup_file_snapshots`.

```sql
EXECUTE sys.sp_delete_backup_file_snapshot
    N'AdventureWorks2025',
    N'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2025Data.mdf?snapshot=2026-05-29T21:31:31.6502195Z';
GO
```

## Related content

- [Tutorial: Use Azure Blob Storage with SQL Server](../tutorial-use-azure-blob-storage-service-with-sql-server.md)
