---
title: "Restore database (General page)"
description: While restoring a database using SQL Server Management Studio, use the General page to specify information about the target and source databases for a database restore operation.
author: MashaMSFT
ms.author: mathoma
ms.date: 09/27/2024
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.restoredb.general.f1"
---

# Restore database (General page)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes the various options found on the **General** page of the **Restore database** wizard in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. The **General** page is used to specify information about the target and source databases for a database restore operation when you [restore a full database backup using SQL Server Management Studio](restore-a-database-backup-using-ssms.md) (SSMS). 

> [!NOTE]  
> When you specify a restore task by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you can generate the corresponding [!INCLUDE [tsql](../../includes/tsql-md.md)] [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) script by selecting **Script** and then selecting a destination for the script.

## Permissions

If the database being restored does not exist, the user must have `CREATE DATABASE` permissions to be able to successfully restore the database. If the database exists, RESTORE permissions default to members of the `sysadmin` and `dbcreator` fixed server roles and the owner (`dbo`) of the database.

RESTORE permissions are given to roles in which membership information is always readily available to the server. Because fixed database role membership can be checked only when the database is accessible and undamaged, which isn't always the case when RESTORE is executed, members of the `db_owner` fixed database role don't have RESTORE permissions.

Restoring from an encrypted backup requires the **VIEW DEFINITION** permission to the certificate or asymmetric key used to encrypt the backup.

## Open the Restore Database wizard 

To open the **Restore database** wizard in SQL Server Management Studio, right-click the database name in Object Explorer > **Tasks** >  **Restore** > **Database** to open the **Restore Database** wizard: 

:::image type="content" source="media/restore-database-general-page/open-restore-page.png" alt-text="Screenshot of selecting restore database in SQL Server Management Studio.":::

## Options

### Source

These options identify the location of the backup sets for the database and which backup sets you want to restore.

| Term | Definition |
| --- | --- |
| **Database** | Select the database to restore from the dropdown list. The list contains only databases that have been backed up based on `msdb` backup history. |
| **Device** | Select the logical or physical backup devices: tapes, URL, or files that contain the backup or backups you want to restore. The device is required if the database backup was taken on a different instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />To select one or more logical or physical backup devices, select the browse button that opens the **Select backup devices** dialog box. You can select up to 64 devices that belong to a single media set. Tape devices must be physically connected to the computer that is running the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. A backup file can be on a local or remote disk device. For more information, see [Backup Devices (SQL Server)](backup-devices-sql-server.md). You can also select **URL** as the device type for backup files stored in Azure storage.<br />When you exit the **Select backup devices** dialog box, the selected device will appear as read-only values in the **Device** list. |
| **Database** | Select the database name from which the backups should be restored from the dropdown list.<br /><br />Note: This list is only available when **Device** is selected. Only databases that have backups on the selected devices will be available. |

### Destination

The options of the **Restore to** panel identify the database and restore point.

| Term | Definition |
| --- | --- |
| **Database** | Enter the database to restore in the list. You can enter a new database or choose an existing database from the dropdown list. The list includes all databases on the server, excluding the system databases `master` and `tempdb`.<br /><br />Note: To restore a password-protected backup, you must use the [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) statement. |
| **Restore to** | The **Restore to** box will be set "To the last backup taken" by default. You can also select **Timeline** to show the **Backup Timeline** dialog box, which displays the database backup history in the form of a timeline. Select **Timeline** to choose a specific **datetime** to which you want to restore the database. The database will be restored to the state it was in at this specified time. See [Backup Timeline](backup-timeline.md). |

### Restore Plan

This section defines terms used in the **Restore Plan** section of the **Restore Database** wizard.

**Back up sets to restore**   

Displays the backup sets available for the specified location. A backup operation creates a backup set that is distributed across all of the devices in the media set. By default, a recovery plan is suggested to achieve the goal of the restore operation that is based on the selection of the required backup sets. [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] uses the backup history in `msdb`. The history is used to identify which backups are required to restore a database, and creates a restore plan. For example, for a database restore, the restore plan selects the most recent full database backup followed by the most recent differential database backup, if any. Under the full recovery model, the restore plan then selects all the log backups.

To override the suggested recovery plan, you can change the selections in the grid. Any backups that depend on a deselected backup are deselected automatically.

The checkboxes are only enabled when the **Manual Selection** box is checked. You can select which backup-sets are to be restored.

When the **Manual Selection** box is checked, the accuracy of the Restore Plan is checked each time it's modified. If the sequence of backups is incorrect, an error message appears.

The following table describes the columns in the **Back up sets to restore** field: 

| Column name | Definition | 
| --- | ---
|Restore | The selected check boxes indicate the backup sets to be restored.|
|Name | The name of the backup set.|
|Component | The backed-up component: **Database**, **File**, or **\<blank>** (for transaction logs).|
|Type | The type of backup: **Full**, **Differential**, or **Transaction Log**.|
|Server | The name of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] instance that completed the backup operation.|
|Database | The name of the database involved in the backup operation.|
|Position | The position of the backup set in the volume.|
|First LSN | The log sequence number of the first transaction in the backup set. Blank for file backups.|
|Last LSN | The log sequence number of the last transaction in the backup set. Blank for file backups.|
|Checkpoint LSN | The log sequence number (LSN) of the most recent checkpoint at the time the backup was created.|
|Full LSN | The log sequence number of the most recent full database backup.|
|Start Date | The date and time when the backup operation began, presented in the regional setting of the client.|
|Finish Date | The date and time when the backup operation finished, presented in the regional setting of the client.|
|Size | The size of the backup set in bytes.|
|User Name | The name of the user who completed the backup operation.|
|Expiration | The date and time the backup set expires.|

**Verify Backup Media**   

Calls a RESTORE VERIFY_ONLY statement on the selected backup-sets. Verify is a long-running operation, and its progress can be tracked and canceled by using the Progress Monitor on the Dialog Framework.

The button allows you to check the integrity of the selected backup files prior to restoring them.<br />When checking the integrity of backup sets, the progress status at the bottom left of the dialog box will read "Verifying" rather than "Executing."


## Compatibility support

In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and greater, you can restore a user database from a database backup that was created by using [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] or a later version. Backups of `master`, **model**, and `msdb` that were created by using [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] through [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] cannot be restored by [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and greater. Also, backups created in newer versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] cannot be restored by any earlier version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

Newer versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] use a different default path than versions prior to [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]. To restore a database that was created in the default location of an earlier version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you must use the MOVE option.

After you restore an earlier version database to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], the database internal version is automatically upgraded. Typically, the database becomes available immediately. However, if a [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] database has full-text indexes, the upgrade process either imports, resets, or rebuilds them, depending on the setting of the **Full-Text Upgrade Option** server property. If the upgrade option is set to **Import** or **Rebuild**, the full-text indexes will be unavailable during the upgrade. Depending upon the amount of data being indexed, importing can take several hours, and rebuilding can take up to 10 times longer. Note also that when the upgrade option is set to **Import**, if a full-text catalog is not available, the associated full-text indexes are rebuilt.

## Restore from an encrypted backup

Restore requires that the certificate or asymmetric key that was originally used to create the backup is available on the instance you're restoring to. The account performing the restore should have the **VIEW DEFINITION** permission on the certificate or asymmetric key. Don't renew or update certificates used to encrypt backups.

## Restore from Microsoft Azure Storage

Select **URL** from the **Backup media type:** dropdown list from the **Select backup devices** dialog box. Next, select **Add** to open the **Select a Backup File Location**. Select an existing SQL Server credential and Azure storage container. Add a new Azure storage container with a shared access signature, or generate a shared access signature and SQL Server credential for an existing storage container. Once connected to the storage account, the backup files are displayed in the **Locate Backup File in Microsoft Azure** dialog where you can select the file to use for the restore. Find more information in [Connect to A Microsoft Azure Subscription](connect-to-a-microsoft-azure-subscription.md).

## Related content

- [Backup Devices (SQL Server)](backup-devices-sql-server.md)
- [Restore a Backup from a Device (SQL Server)](restore-a-backup-from-a-device-sql-server.md)
- [RESTORE Statements - Arguments (Transact-SQL)](../../t-sql/statements/restore-statements-arguments-transact-sql.md)
- [Restore a database to a marked transaction (SQL Server Management Studio)](restore-a-database-to-a-marked-transaction-sql-server-management-studio.md)
- [Restore a Transaction Log Backup (SQL Server)](restore-a-transaction-log-backup-sql-server.md)
- [View the contents of a backup tape or file (SQL Server)](view-the-contents-of-a-backup-tape-or-file-sql-server.md)
- [View the Properties and Contents of a Logical Backup Device (SQL Server)](view-the-properties-and-contents-of-a-logical-backup-device-sql-server.md)
- [Media Sets, Media Families, and Backup Sets (SQL Server)](media-sets-media-families-and-backup-sets-sql-server.md)
- [Apply Transaction Log Backups (SQL Server)](apply-transaction-log-backups-sql-server.md)
