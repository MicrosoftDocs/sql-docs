---
title: "Back Up Database (Media Options page)"
description: In SQL Server, use Media Options in the Back Up Database dialog box to view/modify media options, including Overwrite media, Reliability, and Transaction log.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/08/2024
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
f1_keywords:
  - "swb.backupdatabase.mediaoptions.f1"
  - "sql13.swb.backupdatabase.mediaoptions.f1"
---
# Back Up Database (Media Options page)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the **Media Options** page of the **Back Up Database** dialog box to view or modify database media options.

You can create a backup by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS).

- [Create a Full Database Backup](create-a-full-database-backup-sql-server.md)
- [Create a Differential Database Backup (SQL Server)](create-a-differential-database-backup-sql-server.md)

You can define a database maintenance plan to create database backups. For more information, see [Maintenance plans](../maintenance-plans/maintenance-plans.md) and [Use the Maintenance Plan Wizard](../maintenance-plans/use-the-maintenance-plan-wizard.md).

> [!NOTE]  
> When you specify a backup task by using SSMS, you can generate the corresponding [!INCLUDE [tsql-md](../../includes/tsql-md.md)] [BACKUP](../../t-sql/statements/backup-transact-sql.md) script, by selecting the **Script** button and then selecting a destination for the script.

## Options

### Overwrite media

The options of the **Overwrite media** panel control how the backup is written to the media. If you select URL (Azure Storage) as the backup destination on the General page of the Back Up Database dialog box, the options under the **Overwrite media** section are disabled. You can overwrite a backup using the `BACKUP TO URL ... WITH FORMAT` Transact-SQL statement. For more information, see [SQL Server backup to URL for Microsoft Azure Blob Storage](sql-server-backup-to-url.md).

The **Overwrite media** option is disabled if you selected **URL** as the backup destination in the **General** page.

Only the option, **Backup to a new media, and erase all existing backup sets** is supported with encryption options. If you select the options under the **Back up to existing media** section, the encryptions options on the **Backup Options** page are disabled.

For information about media sets, see [Media Sets, Media Families, and Backup Sets (SQL Server)](media-sets-media-families-and-backup-sets-sql-server.md).

**Back up to the existing media set**: Back up a database to an existing media set. Selecting this option button activates three options.

Choose one of the following options:

- **Append to the existing backup set**: Append the backup set to the existing media set, preserving any prior backups.

- **Overwrite all existing backup sets**: Replace any prior backups on the existing media set with the current backup. This option is the same as setting the `INIT` option for the `BACKUP` statement. For more information, see [Media set options](../../t-sql/statements/backup-transact-sql.md#media-set-options).

- **Check media set name and backup set expiration**: Optionally, if backing up to an existing media set, require the backup operation to verify the name and the expiration date of the backup sets.

- **Media set name**: If **Check media set name and backup set expiration** is selected, optionally, specify the name of the media set to be used for this backup operation.

- **Back up to a new media set, and erase all existing backup sets**: Use a new media set, erasing the previous backup sets.

Selecting this option activates the following options:

- **New media set name**: Optionally, enter a new name for the media set.

- **New media set description**: Optionally, enter a meaningful description of the new media set. This description should be specific enough to communicate the contents accurately.

### Reliability

The options of the **Transaction log** panel control error management by the backup operation.

- **Verify backup when finished**: Verify that the backup set is complete and that all volumes are readable.

- **Perform checksum before writing to media**: Verify the checksums before writing to the backup media. Selecting this option is equivalent to specifying the `CHECKSUM` option in the `BACKUP` statement of [!INCLUDE [tsql](../../includes/tsql-md.md)]. Selecting this option might increase the workload, and decrease the backup throughput of the backup operation. For information about backup checksums, see [Possible Media Errors During Backup and Restore (SQL Server)](possible-media-errors-during-backup-and-restore-sql-server.md).

- **Continue on error**: The backup operation is to continue even after encountering one or more errors.

### Transaction log

The options of the **Transaction log** panel control the behavior of a transaction log backup. These options are relevant only under the full recovery model or bulk-logged recovery model. They activate only if **Transaction log** is selected in the **Backup type** field on the [Back Up Database (General Page)](back-up-database-general-page.md) page of the **Back Up Database** dialog box.

For information about transaction log backups, see [Transaction log backups (SQL Server)](transaction-log-backups-sql-server.md).

- **Truncate the transaction log**: Back up the transaction log and truncate it to free log space. The database remains online. This option is the default.

- **Back up the tail of the log, and leave the database in the restoring state**: Back up the tail of the log, and leave the database in a restoring state. This option creates a *tail-log backup*, which backs up logs that aren't backed up yet (the active log), typically, in preparation for restoring a database. The database is unavailable to users until it completely restores.

  Selecting this option is equivalent to specifying `WITH NO_TRUNCATE, NORECOVERY` in a [BACKUP](../../t-sql/statements/backup-transact-sql.md) statement. For more information, see [Tail-log backups (SQL Server)](tail-log-backups-sql-server.md).

### Tape drive

The options of the **Tape drive** panel control tape management during the backup operation. These options are activated only if **Tape** is selected in the **Destination** panel on the [Back Up Database (General Page)](back-up-database-general-page.md) page of the **Back Up Database** dialog box.

For information about how to use tape devices, see [Backup Devices (SQL Server)](backup-devices-sql-server.md).

- **Unload the tape after backup**: After the backup is complete, unload the tape.

- **Rewind the tape before unloading**: Before unloading the tape, release, and rewind it. This option is enabled only if **Unload the tape after backup** is selected.

## Related content

- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [Back up a transaction log](back-up-a-transaction-log-sql-server.md)
- [Back Up Files and Filegroups](back-up-files-and-filegroups-sql-server.md)
- [Back Up the Transaction Log When the Database Is Damaged (SQL Server)](back-up-the-transaction-log-when-the-database-is-damaged-sql-server.md)
