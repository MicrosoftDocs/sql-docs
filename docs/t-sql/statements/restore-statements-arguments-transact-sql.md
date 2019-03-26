---
title: "RESTORE Arguments (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/08/2018"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "RESTORE statement, arguments"
  - "RESTORE statement"
ms.assetid: 4bfe5734-3003-4165-afd4-b1131ea26e2b
author: mashamsft
ms.author: mathoma
manager: craigg
---
# RESTORE Statements - Arguments (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

This topic documents the arguments that are described in the Syntax sections of the RESTORE {DATABASE|LOG} statement and of the associated set of auxiliary statements: RESTORE FILELISTONLY, RESTORE HEADERONLY, RESTORE LABELONLY, RESTORE REWINDONLY, and RESTORE VERIFYONLY. Most of the arguments are supported by only a subset of these six statements. The support for each argument is indicated in the description of the argument.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
 For syntax, see the following topics:  
  
-   [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)  
  
-   [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md)  
  
-   [RESTORE HEADERONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-headeronly-transact-sql.md)  
  
-   [RESTORE LABELONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-labelonly-transact-sql.md)  
  
-   [RESTORE REWINDONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-rewindonly-transact-sql.md)  
  
-   [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)  
  
## Arguments  
 DATABASE  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md)  
  
 Specifies the target database. If a list of files and filegroups is specified, only those files and filegroups are restored.  
  
 For a database using the full or bulk-logged recovery model, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires in most cases that you back up the tail of the log before restoring the database. Restoring a database without first backing up the tail of the log results in an error, unless the RESTORE DATABASE statement contains either the WITH REPLACE or the WITH STOPAT clause, which must specify a time or transaction that occurred after the end of the data backup. For more information about tail-log backups, see [Tail-Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/tail-log-backups-sql-server.md).  
  
 LOG  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md)  
  
 Specifies that a transaction log backup is to be applied to this database. Transaction logs must be applied in sequential order. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] checks the backed up transaction log to ensure that the transactions are being loaded into the correct database and in the correct sequence. To apply multiple transaction logs, use the NORECOVERY option on all restore operations except the last.  
  
> [!NOTE]  
>  Typically, the last log restored is the tail-log backup. A *tail-log backup* is a log backup taken right before restoring a database, typically after a failure on the database. Taking a tail-log backup from the possibly damaged database prevents work loss by capturing the log that has not yet been backed up (the tail of the log). For more information, see [Tail-Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/tail-log-backups-sql-server.md).  
  
 For more information, see [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md).  
  
 { _database\_name_ | **@**_database\_name\_var_}  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md)  
  
 Is the database that the log or complete database is restored into. If supplied as a variable (**@**_database\_name\_var_), this name can be specified either as a string constant (**@**_database\_name\_var_ = *database*\_*name*) or as a variable of character string data type, except for the **ntext** or **text** data types.  
  
 \<file_or_filegroup_or_page> [ **,**...*n* ]  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md)  
  
 Specifies the name of a logical file or filegroup or page to include in a RESTORE DATABASE or RESTORE LOG statement. You can specify a list of files or filegroups.  
  
 For a database that uses the simple recovery model, the FILE and FILEGROUP options are allowed only if the target files or filegroups are read only, or if this is a PARTIAL restore (which results in a [defunct filegroup](../../relational-databases/backup-restore/remove-defunct-filegroups-sql-server.md)).  
  
 For a database that uses the full or bulk-logged recovery model, after using RESTORE DATABASE to restore one or more files, filegroups, and/or pages, typically, you must apply the transaction log to the files containing the restored data; applying the log makes those files consistent with the rest of the database. The exceptions to this are as follows:  
  
-   If the files being restored were read-only before they were last backed up-then a transaction log does not have to be applied, and the RESTORE statement informs you of this situation.  
  
-   If the backup contains the primary filegroup and a partial restore is being performed. In this case, the restore log is not needed because the log is restored automatically from the backup set.  
  
FILE **=** { *logical_file_name_in_backup*| **@**_logical\_file\_name\_in\_backup\_var_}  
 Names a file to include in the database restore.  
  
FILEGROUP **=** { *logical_filegroup_name* | **@**_logical\_filegroup\_name\_var_ }  
 Names a filegroup to include in the database restore.  
  
 **Note** FILEGROUP is allowed in simple recovery model only if the specified filegroup is read-only and this is a partial restore (that is, if WITH PARTIAL is used). Any unrestored read-write filegroups are marked as defunct and cannot subsequently be restored into the resulting database.  
  
READ_WRITE_FILEGROUPS  
 Selects all read-write filegroups. This option is particularly useful when you have read-only filegroups that you want to restore after read-write filegroups before the read-only filegroups.  
  
PAGE = **'**_file_**:**_page* [ **,**...*n* ]**'**  
 Specifies a list of one or more pages for a page restore (which is supported only for databases using the full or bulk-logged recovery models). The values are as follows:  
  
PAGE  
 Indicates a list of one or more files and pages.  
  
 *file*  
 Is the file ID of the file containing a specific page to be restored.  
  
 *page*  
 Is the page ID of the page to be restored in the file.  
  
 *n*  
 Is a placeholder indicating that multiple pages can be specified.  
  
 The maximum number of pages that can be restored into any single file in a restore sequence is 1000. However, if you have more than a small number of damaged pages in a file, consider restoring the whole file instead of the pages.  
  
> [!NOTE]  
>  Page restores are never recovered.  
  
 For more information about page restore, see [Restore Pages &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-pages-sql-server.md).  
  
 [ **,**...*n* ]  
 Is a placeholder indicating that multiple files and filegroups and pages can be specified in a comma-separated list. The number is unlimited.  
  
FROM { \<backup_device> [ **,**...*n* ]| \<database_snapshot> } 
 Typically, specifies the backup devices from which to restore the backup. Alternatively, in a RESTORE DATABASE statement, the FROM clause can specify the name of a database snapshot to which you are reverting the database, in which case, no WITH clause is permitted.  
  
 If the FROM clause is omitted, the restore of a backup does not take place. Instead, the database is recovered. This allows you to recover a database that has been restored with the NORECOVERY option or to switch over to a standby server. If the FROM clause is omitted, NORECOVERY, RECOVERY, or STANDBY must be specified in the WITH clause.  
  
 \<backup_device> [ **,**...*n* ] 
 Specifies the logical or physical backup devices to use for the restore operation.  
  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md), [RESTORE FILELISTONLY](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md), [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md), [RESTORE LABELONLY](../../t-sql/statements/restore-statements-labelonly-transact-sql.md), [RESTORE REWINDONLY](../../t-sql/statements/restore-statements-rewindonly-transact-sql.md), and [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).  
  
 \<backup_device>::= 
 Specifies a logical or physical backup device to use for the backup operation, as follows:  
  
 { _logical\_backup\_device\_name_ | **@**_logical\_backup\_device\_name\_var_ }  
 Is the logical name, which must follow the rules for identifiers, of the backup device(s) created by **sp_addumpdevice** from which the database is restored. If supplied as a variable (**@**_logical\_backup\_device\_name\_var_), the backup device name can be specified either as a string constant (**@**_logical\_backup\_device\_name\_var_ = _logical\_backup\_device\_name_) or as a variable of character string data type, except for the **ntext** or **text** data types.  
  
 {DISK | TAPE } **=** { **'**_physical\_backup\_device\_name_**'** | **@**_physical\_backup\_device\_name\_var_ }  
 Allows backups to be restored from the named disk or tape device. The device types of disk and tape should be specified with the actual name (for example, complete path and file name) of the device: `DISK ='Z:\SQLServerBackups\AdventureWorks.bak'` or `TAPE ='\\\\.\TAPE0'`. If specified as a variable (**@**_physical\_backup\_device\_name\_var_), the device name can be specified either as a string constant (**@**_physical\_backup\_device\_name\_var_ = '*physical_backup_device_name*') or as a variable of character string data type, except for the **ntext** or **text** data types.  
  
 If using a network server with a UNC name (which must contain machine name), specify a device type of disk. For more information about how to use UNC names, see [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md).  
  
 The account under which you are running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must have READ access to the remote computer or network server in order to perform a RESTORE operation.  
  
 *n*  
 Is a placeholder indicating that up to 64 backup devices may be specified in a comma-separated list.  
  
 Whether a restore sequence requires as many backup devices as were used to create the media set to which the backups belong, depends on whether the restore is offline or online, as follows:  
  
-   Offline restore allows a backup to be restored using fewer devices than were used to create the backup.  
  
-   Online restore requires all the backup devices of the backup. An attempt to restore with fewer devices fails.  
  
 For example, consider a case in which a database was backed up to four tape drives connected to the server. An online restore requires that you have four drives connected to the server; an offline restore allows you to restore the backup if there are less than four drives on the machine.  
  
> [!NOTE]  
>  When you are restoring a backup from a mirrored media set, you can specify only a single mirror for each media family. In the presence of errors, however, having the other mirrors enables some restore problems to be resolved quickly. You can substitute a damaged media volume with the corresponding volume from another mirror. Be aware that for offline restores you can restore from fewer devices than media families, but each family is processed only once.  
  
\<database_snapshot>::=  
**Supported by:**  [RESTORE DATABASE](../../t-sql/statements/restore-statements-transact-sql.md)  
  
DATABASE_SNAPSHOT **=**_database\_snapshot\_name_  
 Reverts the database to the database snapshot specified by *database_snapshot_name*. The DATABASE_SNAPSHOT option is available only for a full database restore. In a revert operation, the database snapshot takes the place of a full database backup.  
  
 A revert operation requires that the specified database snapshot is the only one on the database. During the revert operation, the database snapshot and the destination database and are both marked as `In restore`. For more information, see the "Remarks" section in [RESTORE DATABASE](../../t-sql/statements/restore-statements-transact-sql.md).  
  
### WITH Options  
 Specifies the options to be used by a restore operation. For a summary of which statements use each option, see "Summary of Support for WITH Options," later in this topic.  
  
> [!NOTE]  
>  WITH options are organized here in the same order as in the "Syntax" section in [RESTORE {DATABASE|LOG}](../../t-sql/statements/restore-statements-transact-sql.md).  
  
 PARTIAL  
 **Supported by:**  [RESTORE DATABASE](../../t-sql/statements/restore-statements-transact-sql.md)  
  
 Specifies a partial restore operation that restores the primary filegroup and any specified secondary filegroup(s). The PARTIAL option implicitly selects the primary filegroup; specifying FILEGROUP = 'PRIMARY' is unnecessary. To restore a secondary filegroup, you must explicitly specify the filegroup using the FILE option or FILEGROUP option.  
  
 The PARTIAL option is not allowed on RESTORE LOG statements.  
  
 The PARTIAL option starts the initial stage of a piecemeal restore, which allows remaining filegroups to be restored at a later time. For more information, see [Piecemeal Restores &#40;SQL Server&#41;](../../relational-databases/backup-restore/piecemeal-restores-sql-server.md).  
  
 [ **RECOVERY** | NORECOVERY | STANDBY ]  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md)  
  
 **RECOVERY**  
 Instructs the restore operation to roll back any uncommitted transactions. After the recovery process, the database is ready for use. If neither NORECOVERY, RECOVERY, nor STANDBY is specified, RECOVERY is the default.  
  
 If subsequent RESTORE operations (RESTORE LOG, or RESTORE DATABASE from differential) are planned, NORECOVERY or STANDBY should be specified instead.  
  
 When restoring backup sets from an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a database upgrade might be required. This upgrade is performed automatically when WITH RECOVERY is specified. For more information, see [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md).  
  
> [!NOTE]  
>  If the FROM clause is omitted, NORECOVERY, RECOVERY, or STANDBY must be specified in the WITH clause.  
  
 NORECOVERY  
 Instructs the restore operation to not roll back any uncommitted transactions. If another transaction log has to be applied later, specify either the NORECOVERY or STANDBY option. If neither NORECOVERY, RECOVERY, nor STANDBY is specified, RECOVERY is the default. During an offline restore operation using the NORECOVERY option, the database is not usable.  
  
 For restoring a database backup and one or more transaction logs or whenever multiple RESTORE statements are necessary (for example, when restoring a full database backup followed by a differential database backup), RESTORE requires the WITH NORECOVERY option on all but the final RESTORE statement. A best practice is to use WITH NORECOVERY on ALL statements in a multi-step restore sequence until the desired recovery point is reached, and then to use a separate RESTORE WITH RECOVERY statement for recovery only.  
  
 When used with a file or filegroup restore operation, NORECOVERY forces the database to remain in the restoring state after the restore operation. This is useful in either of these situations:  
  
-   A restore script is being run and the log is always being applied.  
  
-   A sequence of file restores is used and the database is not intended to be usable between two of the restore operations.  
  
 In some cases RESTORE WITH NORECOVERY rolls the roll forward set far enough forward that it is consistent with the database. In such cases, roll back does not occur and the data remains offline, as expected with this option. However, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] issues an informational message that states that the roll-forward set can now be recovered by using the RECOVERY option.  
  
STANDBY **=**_standby\_file\_name_  
 Specifies a standby file that allows the recovery effects to be undone. The STANDBY option is allowed for offline restore (including partial restore). The option is disallowed for online restore. Attempting to specify the STANDBY option for an online restore operation causes the restore operation to fail. STANDBY is also not allowed when a database upgrade is necessary.  
  
 The standby file is used to keep a "copy-on-write" pre-image for pages modified during the undo pass of a RESTORE WITH STANDBY. The standby file allows a database to be brought up for read-only access between transaction log restores and can be used with either warm standby server situations or special recovery situations in which it is useful to inspect the database between log restores. After a RESTORE WITH STANDBY operation, the undo file is automatically deleted by the next RESTORE operation. If this standby file is manually deleted before the next RESTORE operation, then the entire database must be re-restored. While the database is in the STANDBY state, you should treat this standby file with the same care as any other database file. Unlike other database files, this file is only kept open by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] during active restore operations.  
  
 The *standby_file_name* specifies a standby file whose location is stored in the log of the database. If an existing file is using the specified name, the file is overwritten; otherwise, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] creates the file.  
  
 The size requirement of a given standby file depends on the volume of undo actions resulting from uncommitted transactions during the restore operation.  
  
> [!IMPORTANT]  
>  If free disk space is exhausted on the drive containing the specified standby file name, the restore operation stops.  
  
 For a comparison of RECOVERY and NORECOVERY, see the "Remarks" section in [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md).  
  
LOADHISTORY  
 **Supported by:**  [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)  
  
 Specifies that the restore operation loads the information into the **msdb** history tables. The LOADHISTORY option loads information, for the single backup set being verified, about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups stored on the media set to the backup and restore history tables in the **msdb** database. For more information about history tables, see [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md).  
  
#### \<general_WITH_options> [ ,...n ]  
 The general WITH options are all supported in RESTORE DATABASE and RESTORE LOG statements. Some of these options are also supported by one or more auxiliary statements, as noted below.  
  
##### Restore Operation Options  
 These options affect the behavior of the restore operation.  
  
MOVE **'**_logical\_file\_name\_in\_backup_**'** TO **'**_operating\_system\_file\_name_**'** [ ...*n* ]  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) and [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)  
  
 Specifies that the data or log file whose logical name is specified by *logical_file_name_in_backup* should be moved by restoring it to the location specified by *operating_system_file_name*. The logical file name of a data or log file in a backup set matches its logical name in the database when the backup set was created.  
  
*n* is a placeholder indicating that you can specify additional MOVE statements. Specify a MOVE statement for every logical file you want to restore from the backup set to a new location. By default, the *logical_file_name_in_backup* file is restored to its original location.  
  
> [!NOTE]  
>  To obtain a list of the logical files from the backup set, use RESTORE FILELISTONLY.  
  
 If a RESTORE statement is used to relocate a database on the same server or copy it to a different server, the MOVE option might be necessary to relocate the database files and to avoid collisions with existing files.  
  
 When used with RESTORE LOG, the MOVE option can be used only to relocate files that were added during the interval covered by the log being restored. For example, if the log backup contains an add file operation for file `file23`, this file may be relocated using the MOVE option on RESTORE LOG.  
  
 When used with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Snaphot Backup, the MOVE option can be used only to relocate files to an Azure blob within the same storage account as the original blob. The MOVE option cannot be used to restore the snapshot backup to a local file or to a different storage account.  
  
 If a RESTORE VERIFYONLY statement is used when you plan to relocate a database on the same server or copy it to a different server, the MOVE option might be necessary to verify that sufficient space is available in the target and to identify potential collisions with existing files.  
  
 For more information, see [Copy Databases with Backup and Restore](../../relational-databases/databases/copy-databases-with-backup-and-restore.md).  
  
CREDENTIAL  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md), [RESTORE FILELISTONLY](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md), [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md), [RESTORE LABELONLY](../../t-sql/statements/restore-statements-labelonly-transact-sql.md), and [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 CU2 through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]
  
 Used only when restoring a backup from the Microsoft Azure Blob storage service.  
  
> [!NOTE]  
>  With [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 CU2 until  [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], you can only restore from a single device when restoring from URL. In order to restore from multiple devices when restoring from URL you must use [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [current version](https://go.microsoft.com/fwlink/p/?LinkId=299658)) and you must use Shared Access Signature (SAS) tokens. For more information, see [Enable SQL Server Managed Backup to Microsoft Azure](../../relational-databases/backup-restore/enable-sql-server-managed-backup-to-microsoft-azure.md) and [Simplifying creation of SQL Credentials with Shared Access Signature ( SAS ) tokens on Azure Storage with Powershell](https://blogs.msdn.com/b/sqlcat/archive/2015/03/21/simplifying-creation-sql-credentials-with-shared-access-signature-sas-keys-on-azure-storage-containers-with-powershell.aspx).  
  
 REPLACE  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md)  
  
 Specifies that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should create the specified database and its related files even if another database already exists with the same name. In such a case, the existing database is deleted. When the REPLACE option is not specified, a safety check occurs. This prevents overwriting a different database by accident. The safety check ensures that the RESTORE DATABASE statement does not restore the database to the current server if the following conditions both exist:  
  
-   The database named in the RESTORE statement already exists on the current server, and  
  
-   The database name is different from the database name recorded in the backup set.  
  
 REPLACE also allows RESTORE to overwrite an existing file that cannot be verified as belonging to the database being restored. Normally, RESTORE refuses to overwrite pre-existing files. WITH REPLACE can also be used in the same way for the RESTORE LOG option.  
  
 REPLACE also overrides the requirement that you back up the tail of the log before restoring the database.  
  
 For information the impact of using the REPLACE option, see [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md).  
  
RESTART  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md)  
  
 Specifies that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should restart a restore operation that has been interrupted. RESTART restarts the restore operation at the point it was interrupted.  
  
RESTRICTED_USER  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md).  
  
 Restricts access for the newly restored database to members of the **db_owner**, **dbcreator**, or **sysadmin** roles.  RESTRICTED_USER replaces the DBO_ONLY option. DBO_ONLY has been discontinued with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].  
  
 Use with the RECOVERY option.  
  
##### Backup Set Options  
 These options operate on the backup set containing the backup to be restored.  
  
FILE **=**{ *backup_set_file_number* | **@**_backup\_set\_file\_number_ }  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md), [RESTORE FILELISTONLY](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md), [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md), and [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).  
  
 Identifies the backup set to be restored. For example, a *backup_set_file_number* of **1** indicates the first backup set on the backup medium and a *backup_set_file_number* of **2** indicates the second backup set. You can obtain the *backup_set_file_number* of a backup set by using the [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md) statement.  
  
 When not specified, the default is **1**, except for RESTORE HEADERONLY in which case all backup sets in the media set are processed. For more information, see "Specifying a Backup Set," later in this topic.  
  
> [!IMPORTANT]  
>  This FILE option is unrelated to the FILE option for specifying a database file, FILE **=** { *logical_file_name_in_backup* | **@**_logical\_file\_name\_in\_backup\_var_ }.  
  
 PASSWORD  **=** { *password* | **@**_password\_variable_ }  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md), [RESTORE FILELISTONLY](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md), [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md), and [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).  
  
 Supplies the password of the backup set. A backup-set password is a character string.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
 If a password was specified when the backup set was created, that password is required to perform any restore operation from the backup set. It is an error to specify the wrong password or to specify a password if the backup set does not have one.  
  
> [!IMPORTANT]  
>  This password provides only weak protection for the media set. For more information, see the Permissions section for the relevant statement.  
  
##### Media Set Options  
 These options operate on the media set as a whole.  
  
 MEDIANAME **=** { *media_name* | **@**_media\_name\_variable_}  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md), [RESTORE FILELISTONLY](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md), [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md), [RESTORE LABELONLY](../../t-sql/statements/restore-statements-labelonly-transact-sql.md), and [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).  
  
 Specifies the name for the media. If provided, the media name must match the media name on the backup volumes; otherwise, the restore operation terminates. If no media name is given in the RESTORE statement, the check for a matching media name on the backup volumes is not performed.  
  
> [!IMPORTANT]  
>  Consistently using media names in backup and restore operations provides an extra safety check for the media selected for the restore operation.  
  
 MEDIAPASSWORD **=** { *mediapassword* | **@**_mediapassword\_variable_ }  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md), [RESTORE FILELISTONLY](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md), [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md), [RESTORE LABELONLY](../../t-sql/statements/restore-statements-labelonly-transact-sql.md), and [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).  
  
 Supplies the password of the media set. A media-set password is a character string.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
 If a password was provided when the media set was formatted, that password is required to access any backup set on the media set. It is an error to specify the wrong password or to specify a password if the media set does not have any.  
  
> [!IMPORTANT]  
>  This password provides only weak protection for the media set. For more information, see the "Permissions" section for the relevant statement.  
  
 BLOCKSIZE **=** { *blocksize* | **@**_blocksize\_variable_ }  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md)  
  
 Specifies the physical block size, in bytes. The supported sizes are 512, 1024, 2048, 4096, 8192, 16384, 32768, and 65536 (64 KB) bytes. The default is 65536 for tape devices and 512 otherwise. Typically, this option is unnecessary because RESTORE automatically selects a block size that is appropriate to the device. Explicitly stating a block size overrides the automatic selection of block size.  
  
 If you are restoring a backup from a CD-ROM, specify BLOCKSIZE=2048.  
  
> [!NOTE]  
>  This option typically affects performance only when reading from tape devices.  
  
##### Data Transfer Options  
 The options enable you to optimize data transfer from the backup device.  
  
 BUFFERCOUNT **=** { *buffercount* | **@**_buffercount\_variable_ }  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md)  
  
 Specifies the total number of I/O buffers to be used for the restore operation. You can specify any positive integer; however, large numbers of buffers might cause "out of memory" errors because of inadequate virtual address space in the Sqlservr.exe process.  
  
 The total space used by the buffers is determined by: _buffercount_**\**_maxtransfersize_.  
  
 MAXTRANSFERSIZE **=** { _maxtransfersize_ | **@**_maxtransfersize\_variable_ }  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md)  
  
 Specifies the largest unit of transfer in bytes to be used between the backup media and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The possible values are multiples of 65536 bytes (64 KB) ranging up to 4194304 bytes (4 MB).  
> [!NOTE]  
>  When the database has configured FILESTREAM, or includes or In-Memory OLTP File Groups, `MAXTRANSFERSIZE` at the time of restore should be greater than or equal to what was used when the backup was created.  
  
##### Error Management Options  
 These options allow you to determine whether backup checksums are enabled for the restore operation and whether the operation stops on encountering an error.    
  
 { CHECKSUM | NO_CHECKSUM }  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md), [RESTORE FILELISTONLY](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md), [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md), [RESTORE LABELONLY](../../t-sql/statements/restore-statements-labelonly-transact-sql.md), and [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).  
  
 The default behavior is to verify checksums if they are present and proceed without verification if they are not present.  
  
 CHECKSUM  
 Specifies that backup checksums must be verified and, if the backup lacks backup checksums, causes the restore operation to fail with a message indicating that checksums are not present.  
  
> [!NOTE]  
>  Page checksums are relevant to backup operations only if backup checksums are used.  
  
 By default, on encountering an invalid checksum, RESTORE reports a checksum error and stops. However, if you specify CONTINUE_AFTER_ERROR, RESTORE proceeds after returning a checksum error and the number of the page containing the invalid checksum, if the corruption permits.  
  
 For more information about working with backup checksums, see [Possible Media Errors During Backup and Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/possible-media-errors-during-backup-and-restore-sql-server.md).  
  
 NO_CHECKSUM  
 Explicitly disables the validation of checksums by the restore operation.  
  
 { **STOP_ON_ERROR** | CONTINUE_AFTER_ERROR }  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md), [RESTORE FILELISTONLY](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md), [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md), [RESTORE LABELONLY](../../t-sql/statements/restore-statements-labelonly-transact-sql.md), and [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).  
  
 STOP_ON_ERROR  
 Specifies that the restore operation stops with the first error encountered. This is the default behavior for RESTORE, except for VERIFYONLY which has CONTINUE_AFTER_ERROR as the default.  
  
 CONTINUE_AFTER_ERROR  
 Specifies that the restore operation is to continue after an error is encountered.  
  
 If a backup contains damaged pages, it is best to repeat the restore operation using an alternative backup that does not contain the errors-for example, a backup taken before the pages were damaged. As a last resort, however, you can restore a damaged backup using the CONTINUE_AFTER_ERROR option of the restore statement and try to salvage the data.  
  
##### FILESTREAM Options  
 FILESTREAM ( DIRECTORY_NAME =*directory_name* )  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) and [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]
  
 A windows-compatible directory name. This name should be unique among all the database-level FILESTREAM directory names in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Uniqueness comparison is done in a case-insensitive fashion, regardless of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collation settings.  
  
##### Monitoring Options  
 These options enable you to monitor the transfer of data transfer from the backup device.  
  
 STATS [ **=** _percentage_ ]  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) and [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)  
  
 Displays a message each time another percentage completes, and is used to gauge progress. If *percentage* is omitted, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] displays a message after each 10 percent is completed (approximately).  
  
 The STATS option reports the percentage complete as of the threshold for reporting the next interval. This is at approximately the specified percentage; for example, with STATS=10, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] reports at approximately that interval; for instance, instead of displaying precisely 40%, the option might display 43%. For large backup sets, this is not a problem because the percentage complete moves very slowly between completed I/O calls.  
  
##### Tape Options  
 These options are used only for TAPE devices. If a nontape device is being used, these options are ignored.  
  
 { **REWIND** | NOREWIND }  
 These options are used only for TAPE devices. If a non-tape device is being used, these options are ignored.  
  
 REWIND  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md), [RESTORE FILELISTONLY](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md), [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md), [RESTORE LABELONLY](../../t-sql/statements/restore-statements-labelonly-transact-sql.md), and [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).  
  
 Specifies that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] release and rewind the tape. REWIND is the default.  
  
 NOREWIND  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) and [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)  
  
 Specifying NOREWIND in any other restore statement generates an error.  
  
 Specifies that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will keep the tape open after the backup operation. You can use this option to improve performance when performing multiple backup operations to a tape.  
  
 NOREWIND implies NOUNLOAD, and these options are incompatible within a single RESTORE statement.  
  
> [!NOTE]  
>  If you use NOREWIND, the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] retains ownership of the tape drive until a BACKUP or RESTORE statement running in the same process uses either the REWIND or UNLOAD option, or the server instance is shut down. Keeping the tape open prevents other processes from accessing the tape. For information about how to display a list of open tapes and to close an open tape, see [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md).  
  
 { **UNLOAD** | NOUNLOAD }  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md), [RESTORE FILELISTONLY](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md), [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md), [RESTORE LABELONLY](../../t-sql/statements/restore-statements-labelonly-transact-sql.md), [RESTORE REWINDONLY](../../t-sql/statements/restore-statements-rewindonly-transact-sql.md), and [RESTORE VERIFYONLY](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).  
  
 These options are used only for TAPE devices. If a non-tape device is being used, these options are ignored.  
  
> [!NOTE]  
>  UNLOAD/NOUNLOAD is a session setting that persists for the life of the session or until it is reset by specifying the alternative.  
  
 UNLOAD  
 Specifies that the tape is automatically rewound and unloaded when the backup is finished. UNLOAD is the default when a session begins.  
  
 NOUNLOAD  
 Specifies that after the RESTORE operation the tape remains loaded on the tape drive.  
  
#### <replication_WITH_option>  
 This option is relevant only if the database was replicated when the backup was created.  
  
 KEEP_REPLICATION  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md)  
  
Use KEEP_REPLICATION when setting up replication to work with log shipping. It prevents replication settings from being removed when a database backup or log backup is restored on a warm standby server and the database is recovered. Specifying this option when restoring a backup with the NORECOVERY option is not permitted. To ensure replication functions properly after restore:  
  
-   The **msdb** and **master** databases at the warm standby server must be in sync with the **msdb** and **master** databases at the primary server.  
  
-   The warm standby server must be renamed to use the same name as the primary server.  
  
#### <change_data_capture_WITH_option>  
 This option is relevant only if the database was enabled for change data capture when the backup was created.  
  
 KEEP_CDC  
 **Supported by:**  [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md)  
  
 KEEP_CDC should be used to prevent change data capture settings from being removed when a database backup or log backup is restored on another server and the database is recovered. Specifying this option when restoring a backup with the NORECOVERY option is not permitted.  
  
 Restoring the database with KEEP_CDC does not create the change data capture jobs. To extract changes from the log after restoring the database, recreate the capture process job and the cleanup job for the restored database. For information, see [sys.sp_cdc_add_job &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-cdc-add-job-transact-sql.md).  
  
 For information about using change data capture with database mirroring, see [Change Data Capture and Other SQL Server Features](../../relational-databases/track-changes/change-data-capture-and-other-sql-server-features.md).  
  
#### \<service_broker_WITH_options>  
 Turns [!INCLUDE[ssSB](../../includes/sssb-md.md)] message delivery on or off or sets a new [!INCLUDE[ssSB](../../includes/sssb-md.md)] identifier. This option is relevant only if [!INCLUDE[ssSB](../../includes/sssb-md.md)] was enabled (activated) for the database when the backup was created.  
  
 { ENABLE_BROKER  | ERROR_BROKER_CONVERSATIONS  | NEW_BROKER }  
 **Supported by:**  [RESTORE DATABASE](../../t-sql/statements/restore-statements-transact-sql.md)  
  
 ENABLE_BROKER  
 Specifies that [!INCLUDE[ssSB](../../includes/sssb-md.md)] message delivery is enabled at the end of the restore so that messages can be sent immediately. By default [!INCLUDE[ssSB](../../includes/sssb-md.md)] message delivery is disabled during a restore. The database retains the existing Service Broker identifier.  
  
 ERROR_BROKER_CONVERSATIONS  
 Ends all conversations with an error stating that the database is attached or restored. This enables your applications to perform regular clean up for existing conversations. Service Broker message delivery is disabled until this operation is completed, and then it is enabled. The database retains the existing Service Broker identifier.  
  
 NEW_BROKER  
 Specifies that the database be assigned a new Service Broker identifier. Because the database is considered to be a new Service Broker, existing conversations in the database are immediately removed without producing end dialog messages. Any route referencing the old Service Broker identifier must be recreated with the new identifier.  
  
#### \<point_in_time_WITH_options>  
 **Supported by:**  [RESTORE {DATABASE|LOG}](../../t-sql/statements/restore-statements-transact-sql.md) and only for the full or bulk-logged recovery models.  
  
 You can restore a database to a specific point in time or transaction, by specifying the target recovery point in a STOPAT, STOPATMARK, or STOPBEFOREMARK clause. A specified time or transaction is always restored from a log backup. In every RESTORE LOG statement of the restore sequence, you must specify your target time or transaction in an identical STOPAT, STOPATMARK, or STOPBEFOREMARK clause.  
  
 As a prerequisite to a point-in-time restore, you must first restore a full database backup whose end point is earlier than your target recovery point. To help you identify which database backup to restore, you can optionally specify your WITH STOPAT, STOPATMARK, or STOPBEFOREMARK clause in a RESTORE DATABASE statement to raise an error if a data backup is too recent for the specified target time. But the complete data backup is always restored, even if it contains the target time.  
  
> [!NOTE]  
>  The RESTORE_DATABASE and RESTORE_LOG point-in-time WITH options are similar, but only RESTORE LOG supports the *mark_name* argument.  
  
 { STOPAT | STOPATMARK | STOPBEFOREMARK }   
 
 STOPAT **=** { **'**_datetime_**'** | **@**_datetime\_var* }  
 Specifies that the database be restored to the state it was in as of the date and time specified by the *datetime* or **@**_datetime\_var_ parameter. For information about specifying a date and time, see [Date and Time Data Types and Functions &#40;Transact-SQL&#41;](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md).  
  
 If a variable is used for STOPAT, the variable must be **varchar**, **char**, **smalldatetime**, or **datetime** data type. Only transaction log records written before the specified date and time are applied to the database.  
  
> [!NOTE]  
>  If the specified STOPAT time is after the last LOG backup, the database is left in the unrecovered state, just as if RESTORE LOG ran with NORECOVERY.  
  
 For more information, see [Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md).  
  
 STOPATMARK **=** { **'**_mark\_name_**'** | **'**lsn:_lsn\_number_**'** } [ AFTER **'**_datetime_**'** ]  
 Specifies recovery to a specified recovery point. The specified transaction is included in the recovery, but it is committed only if it was originally committed when the transaction was actually generated.  
  
 Both RESTORE DATABASE and RESTORE LOG support the *lsn_number* parameter. This parameter specifies a log sequence number.  
  
 The *mark_name* parameter is supported only by the RESTORE LOG statement. This parameter identifies a transaction mark in the log backup.  
  
 In a RESTORE LOG statement, if AFTER *datetime* is omitted, recovery stops at the first mark with the specified name. If AFTER *datetime* is specified, recovery stops at the first mark having the specified name exactly at or after *datetime*.  
  
> [!NOTE]  
>  If the specified mark, LSN, or time is after the last LOG backup, the database is left in the unrecovered state, just as if RESTORE LOG ran with NORECOVERY.  
  
 For more information, see [Use Marked Transactions to Recover Related Databases Consistently &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/use-marked-transactions-to-recover-related-databases-consistently.md) and [Recover to a Log Sequence Number &#40;SQL Server&#41;](../../relational-databases/backup-restore/recover-to-a-log-sequence-number-sql-server.md).  
  
 STOPBEFOREMARK **=** { **'**_mark\_name_**'** | **'**lsn:_lsn\_number_**'** } [ AFTER **'**_datetime_**'** ]  
 Specifies recovery up to a specified recovery point. The specified transaction is not included in the recovery, and is rolled back when WITH RECOVERY is used.  
  
 Both RESTORE DATABASE and RESTORE LOG support the *lsn_number* parameter. This parameter specifies a log sequence number.  
  
 The *mark_name* parameter is supported only by the RESTORE LOG statement. This parameter identifies a transaction mark in the log backup.  
  
 In a RESTORE LOG statement, if AFTER *datetime* is omitted, recovery stops just before the first mark with the specified name. If AFTER *datetime* is specified, recovery stops just before the first mark having the specified name exactly at or after *datetime*.  
  
> [!IMPORTANT]  
>  If a partial restore sequence excludes any FILESTREAM filegroup, point-in-time restore is not supported. You can force the restore sequence to continue. However, the FILESTREAM filegroups that are omitted from the RESTORE statement can never be restored. To force a point-in-time restore, specify the CONTINUE_AFTER_ERROR option together with the STOPAT, STOPATMARK, or STOPBEFOREMARK option. If you specify CONTINUE_AFTER_ERROR, the partial restore sequence succeeds and the FILESTREAM filegroup becomes unrecoverable.  
  
## Result Sets  
 For result sets, see the following topics:  
  
-   [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md)  
  
-   [RESTORE HEADERONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-headeronly-transact-sql.md)  
  
-   [RESTORE LABELONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-labelonly-transact-sql.md)  
  
## Remarks  
 For additional remarks, see the following topics:  
  
-   [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)  
  
-   [RESTORE HEADERONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-headeronly-transact-sql.md)  
  
-   [RESTORE LABELONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-labelonly-transact-sql.md)  
  
-   [RESTORE REWINDONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-rewindonly-transact-sql.md)  
  
-   [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)  
  
## Specifying a Backup Set  
 A *backup set* contains the backup from a single, successful backup operation. RESTORE, RESTORE FILELISTONLY, RESTORE HEADERONLY, and RESTORE VERIFYONLY statements operate on a single backup set within the media set on the specified backup device or devices. You should specify the backup you need from within the media set. You can obtain the *backup_set_file_number* of a backup set by using the [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md) statement.  
  
 The option for specifying the backup set to restore is:  
  
 FILE **=**{ *backup_set_file_number* | **@**_backup\_set\_file\_number_ }  
  
 Where *backup_set_file_number* indicates the position of the backup in the media set. A *backup_set_file_number* of 1 (FILE = 1) indicates the first backup set on the backup medium and a *backup_set_file_number* of 2 (FILE = 2) indicates the second backup set, and so on.  
  
 The behavior of this option varies depending on the statement, as described in the following table:  
  
|Statement|Behavior of backup-set FILE option|  
|---------------|-----------------------------------------|  
|RESTORE|The default backup set file number is 1. Only one backup-set FILE option is allowed in a RESTORE statement. It is important to specify backup sets in order.|  
|RESTORE FILELISTONLY|The default backup set file number is 1.|  
|RESTORE HEADERONLY|By default, all backup sets in the media set are processed. The RESTORE HEADERONLY results set returns information about each backup set, including its **Position** in the media set. To return information on a given backup set, use its position number as the *backup_set_file_number* value in the FILE option.<br /><br /> Note: For tape media, RESTORE HEADER only processes backup sets on the loaded tape.|  
|RESTORE VERIFYONLY|The default *backup_set_file_number* is 1.|  
  
> [!NOTE]  
>  The FILE option for specifying a backup set is unrelated to the FILE option for specifying a database file, FILE **=** { *logical_file_name_in_backup* | **@**_logical\_file\_name\_in\_backup\_var_ }.  
  
## Summary of Support for WITH Options  
 The following WITH options are supported by only the RESTORE statement: BLOCKSIZE, BUFFERCOUNT, MAXTRANSFERSIZE, PARTIAL, KEEP_REPLICATION, { RECOVERY | NORECOVERY | STANDBY }, REPLACE, RESTART, RESTRICTED_USER, and { STOPAT | STOPATMARK | STOPBEFOREMARK }  
  
> [!NOTE]  
>  The PARTIAL option is supported only by RESTORE DATABASE.  
  
 The following table lists the WITH options that are used by one or more statements and indicates which statements support each option. A check mark (√) indicates that an option is supported; a dash (-) indicates that an option is not supported.  
  
|WITH option|RESTORE|RESTORE FILELISTONLY|RESTORE HEADERONLY|RESTORE LABELONLY|RESTORE REWINDONLY|RESTORE VERIFYONLY|  
|-----------------|-------------|--------------------------|------------------------|-----------------------|------------------------|------------------------|  
|{ CHECKSUM<br /><br /> &#124; NO_CHECKSUM }|√|√|√|√|-|√|  
|{ CONTINUE_AFTER_ERROR<br /><br /> &#124; STOP_ON_ERROR }|√|√|√|√|-|√|  
|FILE<sup>1</sup>|√|√|√|-|-|√|  
|LOADHISTORY|-|-|-|-|-|√|  
|MEDIANAME|√|√|√|√|-|√|  
|MEDIAPASSWORD|√|√|√|√|-|√|  
|MOVE|√|-|-|-|-|√|  
|PASSWORD|√|√|√|-|-|√|  
|{ REWIND &#124; NOREWIND }|√|Only REWIND|Only REWIND|Only REWIND|-|√|  
|STATS|√|-|-|-|-|√|  
|{ UNLOAD &#124; NOUNLOAD }|√|√|√|√|√|√|  
  
 <sup>1</sup> FILE **=**_backup\_set\_file\_number_, which is distinct from {FILE | FILEGROUP}.  
  
## Permissions  
 For permissions, see the following topics:  
  
-   [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)  
  
-   [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md)  
  
-   [RESTORE HEADERONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-headeronly-transact-sql.md)  
  
-   [RESTORE LABELONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-labelonly-transact-sql.md)  
  
-   [RESTORE REWINDONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-rewindonly-transact-sql.md)  
  
-   [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)  
  
## Examples  
 For examples, see the following topics:  
  
-   [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)  
  
-   [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md)  
  
-   [RESTORE HEADERONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-headeronly-transact-sql.md)  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md)   
 [RESTORE HEADERONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-headeronly-transact-sql.md)   
 [RESTORE LABELONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-labelonly-transact-sql.md)   
 [RESTORE REWINDONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-rewindonly-transact-sql.md)   
 [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)   
 [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [FILESTREAM &#40;SQL Server&#41;](../../relational-databases/blob/filestream-sql-server.md)  
  
  

