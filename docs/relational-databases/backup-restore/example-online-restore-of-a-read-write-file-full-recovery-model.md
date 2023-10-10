---
title: "Online restore of a read-write file (full recovery model)"
description: This example shows an online restore in SQL Server of a read-write file for a database using the full recovery model with multiple filegroups.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 09/22/2023
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords:
  - "full recovery model [SQL Server], RESTORE example"
  - "online restores [SQL Server], full recovery model"
  - "restore sequences [SQL Server], online"
---
# Example: Online restore of a read-write file (full recovery model)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article is relevant for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] databases under the full recovery model that contain multiple files or filegroups.

In this example, a database named `adb`, which uses the full recovery model, contains three filegroups. Filegroup `A` is read/write, and filegroup `B` and filegroup `C` are read-only. Initially, all of the filegroups are online.

File `a1` in filegroup `A` appears to be damaged, and the database administrator decides to restore it while the database remains online.

> [!NOTE]  
> Under the simple recovery model, online restore of read/write data isn't allowed.

## Restore sequences

The syntax for an online restore sequence is the same as for an offline restore sequence.

1. Online restore of file `a1`.

    ```sql
    RESTORE DATABASE adb FILE = 'a1' FROM backup
    WITH NORECOVERY;
    ```

     At this point, file `a1` is in the `RESTORING` state, and filegroup `A` is offline.

1. After you restore the file, take a new log backup to make sure that the point at which the file went offline is captured.

    ```sql
    BACKUP LOG adb TO log_backup3;
    ```

1. Online restore of log backups.

   You restore all the log backups taken since the restored file backup, ending with the latest log backup (`log_backup3`, taken in the previous step). After the last backup is restored, the database is recovered.

    ```sql
    RESTORE LOG adb FROM log_backup1 WITH NORECOVERY;
    RESTORE LOG adb FROM log_backup2 WITH NORECOVERY;
    RESTORE LOG adb FROM log_backup3 WITH NORECOVERY;
    RESTORE DATABASE adb WITH RECOVERY;
    ```

     File `a1` is now online.

## Additional examples

- [Example: Piecemeal Restore of Database (Simple Recovery Model)](example-piecemeal-restore-of-database-simple-recovery-model.md)
- [Example: Piecemeal Restore of Only Some Filegroups (Simple Recovery Model)](example-piecemeal-restore-of-only-some-filegroups-simple-recovery-model.md)
- [Example: Online Restore of a Read-Only File (Simple Recovery Model)](example-online-restore-of-a-read-only-file-simple-recovery-model.md)
- [Example: Piecemeal Restore of Database (Full Recovery Model)](example-piecemeal-restore-of-database-full-recovery-model.md)
- [Example: Piecemeal Restore of Only Some Filegroups (Full Recovery Model)](example-piecemeal-restore-of-only-some-filegroups-full-recovery-model.md)
- [Example: Online Restore of a Read-Only File (Full Recovery Model)](example-online-restore-of-a-read-only-file-full-recovery-model.md)

## Related content

- [Online Restore (SQL Server)](online-restore-sql-server.md)
- [Piecemeal Restores (SQL Server)](piecemeal-restores-sql-server.md)
- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [Restore and Recovery Overview (SQL Server)](restore-and-recovery-overview-sql-server.md)
- [Apply Transaction Log Backups (SQL Server)](apply-transaction-log-backups-sql-server.md)
- [RESTORE (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
