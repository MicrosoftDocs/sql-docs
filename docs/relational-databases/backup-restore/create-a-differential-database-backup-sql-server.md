---
title: Differential Backup
description: Learn how to create a differential database backup in SQL Server using SQL Server Management Studio, Transact-SQL, and PowerShell.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/27/2026
ms.service: sql
ms.subservice: backup-restore
ms.topic: how-to
helpviewer_keywords:
  - "full differential backups [SQL Server]"
  - "database backups [SQL Server], full differential backups"
  - "backing up databases [SQL Server], full differential backups"
  - "backups [SQL Server], creating"
---
# Create a differential database backup (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to create a differential database backup in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE [tsql](../../includes/tsql-md.md)], or PowerShell.

For an overview of backup concepts and tasks, see [Backup overview (SQL Server)](backup-overview-sql-server.md).

<a id="BeforeYouBegin"></a>
<a id="Prerequisites"></a>

## Prerequisites

A differential database backup requires a previous full database backup. If the database doesn't have one, take a full database backup first. For more information, see [Create a full database backup](create-a-full-database-backup-sql-server.md).

<a id="Recommendations"></a>

## Recommendations

Because differential backups increase in size over time, restoring a differential backup can significantly extend the time to restore a database. Take a new full backup at set intervals to establish a new differential base.

For example, you can take a weekly full database backup, followed by daily differential backups during the week.

<a id="Security"></a>
<a id="Permissions"></a>

<a id="Restrictions"></a>
<a id="limitations-and-restrictions"></a>

## Limitations

You can't use the `BACKUP` statement in an explicit or implicit transaction.

## Permissions

`BACKUP DATABASE` and `BACKUP LOG` permissions default to members of the **sysadmin** fixed server role, and the **db_owner** and **db_backupoperator** fixed database roles.

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account must have read and write permissions on the backup device's physical file. Ownership or permission problems on that file prevent backup and restore operations, and only appear when the backup or restore operation runs.

For example, the [sp_addumpdevice](../system-stored-procedures/sp-addumpdevice-transact-sql.md) system stored procedure, which adds a backup device entry to the system tables, **doesn't** validate file access.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In Object Explorer, connect to the [!INCLUDE [ssde-md](../../includes/ssde-md.md)], and then expand the server tree.

1. Expand **Databases**, and then select a user database. Or, expand **System Databases** and select a system database.

1. Right-click the database, point to **Tasks**, and then select **Back Up**. The **Back Up Database** dialog box appears.

1. In the **Database** dropdown list, verify the database name. Optionally, select a different database.

   You can perform a differential backup with any [recovery model](recovery-models-sql-server.md) (full, bulk-logged, or simple).

1. In the **Backup type** dropdown list, select **Differential**.

   > [!IMPORTANT]  
   > When you select **Differential**, make sure the **Copy-only backup** check box is cleared. You can't create a differential backup on a copy-only full backup. For more information, see [Copy-only backups](copy-only-backups-sql-server.md).

1. For **Backup component**, select **Database**.

1. In the **Name** text box, accept the default backup set name or enter a new one.

1. Optionally, enter a description in the **Description** text box.

1. Specify when the backup set expires:

   - To expire the backup set after a specific number of days, select **After** (the default), and enter the number of days. Values range from `0` to `99999`. A value of `0` means the backup set never expires.

     The default value is set in the **Default backup media retention (in days)** option on the **Database Settings** page of the **Server Properties** dialog box. To open it, right-click the server name in Object Explorer, select **Properties**, and then select the **Database Settings** page.

   - To expire the backup set on a specific date, select **On**, and enter the date.

1. For the backup destination, select **Disk** or **URL**. To add up to 64 disk drives for a single media set, select **Add**. The selected paths appear in the **Backup to** dropdown list.

   To remove a backup destination, select it and select **Remove**. To view the contents of a backup destination, select it and select **Contents**.

   > [!NOTE]  
   > For more information about backing up to URL, see [SQL Server backup to URL for Azure Blob Storage](sql-server-backup-to-url.md).

1. To view or select the advanced options, select **Options** in the **Select a page** pane.

1. For **Overwrite Media**, select one of the following options:

   - **Back up to the existing media set**

     Select either **Append to the existing backup set** or **Overwrite all existing backup sets**. Optionally, select the **Check media set name and backup set expiration** check box and enter a name in the **Media set name** text box.

     If you don't specify a name, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] creates the media set with a blank name. If you specify a name, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] checks the media to confirm the name matches.

     If you leave the media name blank and select the check box, the check succeeds only if the name on the media is also blank.

   - **Back up to a new media set, and erase all existing backup sets**

     Enter a name in the **New media set name** text box. Optionally, describe the media set in the **New media set description** text box.

1. In the **Reliability** section, optionally select:

   - **Verify backup when finished**.

   - **Perform checksum before writing to media**, and optionally **Continue on checksum error**. For information about checksums, see [Possible Media Errors During Backup and Restore (SQL Server)](possible-media-errors-during-backup-and-restore-sql-server.md).

1. [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] supports [backup compression](backup-compression-sql-server.md). By default, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] compresses backups based on the `backup-compression default` server configuration option. Regardless of the server-level default, select **Compress backup** to compress the backup, or select **Do not compress backup** to prevent compression.

   To view the current backup compression default, see [Server configuration: backup compression default](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md). For more information about backup compression, see [Backup compression (SQL Server)](backup-compression-sql-server.md).

### Remarks

- You can use the Maintenance Plan Wizard to create differential database backups instead.

- The **Transaction log** section options are inactive unless you're backing up a transaction log. Specify this option in the **Backup type** section of the **General** page.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

Execute the `BACKUP DATABASE` statement to create the differential database backup. Specify:

   - The name of the database to back up.

   - The backup device where the full database backup is written.

   - The `DIFFERENTIAL` clause to back up only the parts of the database that changed after the last full database backup.

   Use the following syntax:

   `BACKUP DATABASE` *database_name* `TO` <backup_device> `WITH DIFFERENTIAL`

<a id="TsqlExample"></a>

### Example (Transact-SQL)

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

```sql
-- Create a full database backup first.
BACKUP DATABASE AdventureWorks2025
    TO AdventureWorks2025_1
    WITH INIT;
GO

-- Time elapses.
-- Create a differential database backup, appending the backup
-- to the backup device containing the full database backup.
BACKUP DATABASE AdventureWorks2025
    TO AdventureWorks2025_1
    WITH DIFFERENTIAL;
GO
```

### Remarks

For information about backing up to tape, see [BACKUP tape options](../../t-sql/statements/backup-transact-sql.md#tape-options) and the [Back up to a tape device](create-a-full-database-backup-sql-server.md#b-back-up-to-a-tape-device) example in [Create a full database backup](create-a-full-database-backup-sql-server.md).

> [!NOTE]  
> The `TAPE` option will be removed in a future version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.

<a id="PowerShellProcedure"></a>

## Use PowerShell

Use the `Backup-SqlDatabase` cmdlet. To specify a differential database backup, use the `-Incremental` parameter along with `-BackupAction Database`.

> [!NOTE]  
> This example requires the `SqlServer` module. For more information, see [SQL Server PowerShell Provider](/powershell/sql-server/sql-server-powershell-provider).

### Example (PowerShell)

The following example creates a differential database backup of the `AdventureWorks2025` database to the default backup location of the server instance `Computer\Instance`. A full database backup must already exist.

For full syntax examples, see [Backup-SqlDatabase](/powershell/module/sqlserver/backup-sqldatabase).

```powershell
$credential = Get-Credential

Backup-SqlDatabase -ServerInstance Computer[\Instance] -Database AdventureWorks2025 -BackupAction Database -Incremental -Credential $credential
```

## Related content

- [Differential backups (SQL Server)](differential-backups-sql-server.md)
- [Create a full database backup](create-a-full-database-backup-sql-server.md)
- [Back Up Files and Filegroups](back-up-files-and-filegroups-sql-server.md)
- [Restore a differential database backup (SQL Server)](restore-a-differential-database-backup-sql-server.md)
- [Restore a Transaction Log Backup (SQL Server)](restore-a-transaction-log-backup-sql-server.md)
- [Maintenance plans](../maintenance-plans/maintenance-plans.md)
- [Full File Backups (SQL Server)](full-file-backups-sql-server.md)
