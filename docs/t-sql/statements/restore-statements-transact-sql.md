---
title: "RESTORE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/11/2019"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "RESTORE DATABASE"
  - "RESTORE_TSQL"
  - "RESTORE_DATABASE_TSQL"
  - "RESTORE"
  - "RESTORE_LOG_TSQL"
  - "RESTORE LOG"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "RESTORE DATABASE, see RESTORE statement"
  - "full backups [SQL Server]"
  - "RECOVERY option"
  - "database snapshots [SQL Server], reverting to"
  - "STOPAT syntax"
  - "differential backups, RESTORE statement"
  - "point in time recovery [SQL Server]"
  - "restoring [SQL Server]"
  - "NORECOVERY option"
  - "online restores [SQL Server], RESTORE statement"
  - "moving databases"
  - "RESTORE statement, syntax"
  - "cross-platform restores"
  - "restoring databases [SQL Server], options"
  - "RESTORE statement"
  - "snapshots [SQL Server database snapshots], reverting to"
  - "reverting database snapshots"
  - "transaction log backups [SQL Server], RESTORE statement"
  - "RESTORE LOG, see RESTORE statement"
ms.assetid: 877ecd57-3f2e-4237-890a-08f16e944ef1
author: mashamsft
ms.author: mathoma
manager: craigg
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current||>=aps-pdw-2016||=sqlallproducts-allversions"
---
# RESTORE Statements (Transact-SQL)
Restores SQL database backups taken using the BACKUP command. 

Click one of the following tabs for the syntax, arguments, remarks, permissions, and examples for a particular SQL version with which you are working.

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md). 

## Click a product!

In the following row, click whichever product name you are interested in. The click displays different content here, appropriate for whichever product you click:

::: moniker range=">=sql-server-2016||>=sql-server-linux-2017||=sqlallproducts-allversions"

> [!div class="mx-tdCol2BreakAll"]
> ||||
> |-|-|-|
> |**_\* SQL Server \*_**|[SQL Database<br />Managed Instance](restore-statements-transact-sql.md?view=azuresqldb-mi-current)|[Parallel<br />Data Warehouse](restore-statements-transact-sql.md?view=aps-pdw-2016)

&nbsp;

## SQL Server

This command enables you to perform the following restore scenarios:  
  
- Restore an entire database from a full database backup (a complete restore).
- Restore part of a database (a partial restore).
- Restore specific files or filegroups to a database (a file restore).
- Restore specific pages to a database (a page restore).  
- Restore a transaction log onto a database (a transaction log restore).  
- Revert a database to the point in time captured by a database snapshot.  
  
For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] restore scenarios, see [Restore and Recovery Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md). For more information about descriptions of the arguments, see [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md). When restoring a database from another instance, consider the information from [Manage Metadata When Making a Database Available on Another Server Instance (SQL Server)](../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md).
  
> [!NOTE] 
> For more information about restoring from the Microsoft Azure Blob storage service, see [SQL Server Backup and Restore with Microsoft Azure Blob Storage Service](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md).  
  
## Syntax  
  
```sql  
--To Restore an Entire Database from a Full database backup (a Complete Restore):  
RESTORE DATABASE { database_name | @database_name_var }   
 [ FROM <backup_device> [ ,...n ] ]  
 [ WITH   
   {  
    [ RECOVERY | NORECOVERY | STANDBY =   
        {standby_file_name | @standby_file_name_var }   
       ]  
   | ,  <general_WITH_options> [ ,...n ]  
   | , <replication_WITH_option>  
   | , <change_data_capture_WITH_option>  
   | , <FILESTREAM_WITH_option>  
   | , <service_broker_WITH options>   
   | , \<point_in_time_WITH_options-RESTORE_DATABASE>   
   } [ ,...n ]  
 ]  
[;]  
  
--To perform the first step of the initial restore sequence of a piecemeal restore:  
RESTORE DATABASE { database_name | @database_name_var }   
   <files_or_filegroups> [ ,...n ]  
 [ FROM <backup_device> [ ,...n ] ]   
   WITH   
      PARTIAL, NORECOVERY   
      [  , <general_WITH_options> [ ,...n ]   
       | , \<point_in_time_WITH_options-RESTORE_DATABASE>   
      ] [ ,...n ]   
[;]  
  
--To Restore Specific Files or Filegroups:   
RESTORE DATABASE { database_name | @database_name_var }   
   <file_or_filegroup> [ ,...n ]  
 [ FROM <backup_device> [ ,...n ] ]   
   WITH   
   {  
      [ RECOVERY | NORECOVERY ]  
      [ , <general_WITH_options> [ ,...n ] ]  
   } [ ,...n ]   
[;]  
  
--To Restore Specific Pages:   
RESTORE DATABASE { database_name | @database_name_var }   
   PAGE = 'file:page [ ,...n ]'   
 [ , <file_or_filegroups> ] [ ,...n ]  
 [ FROM <backup_device> [ ,...n ] ]   
   WITH   
       NORECOVERY     
      [ , <general_WITH_options> [ ,...n ] ]  
[;]  
  
--To Restore a Transaction Log:  
RESTORE LOG { database_name | @database_name_var }   
 [ <file_or_filegroup_or_pages> [ ,...n ] ]  
 [ FROM <backup_device> [ ,...n ] ]   
 [ WITH   
   {  
     [ RECOVERY | NORECOVERY | STANDBY =   
        {standby_file_name | @standby_file_name_var }   
       ]  
    | ,  <general_WITH_options> [ ,...n ]  
    | , <replication_WITH_option>  
    | , \<point_in_time_WITH_options-RESTORE_LOG>   
   } [ ,...n ]  
 ]   
[;]  
  
--To Revert a Database to a Database Snapshot:     
RESTORE DATABASE { database_name | @database_name_var }   
FROM DATABASE_SNAPSHOT = database_snapshot_name   
  
<backup_device>::=  
{   
   { logical_backup_device_name |  
      @logical_backup_device_name_var }  
 | { DISK    
     | TAPE  
     | URL   
   } = { 'physical_backup_device_name' |  
      @physical_backup_device_name_var }   
}   
Note: URL is the format used to specify the location and the file name for the Microsoft Azure Blob. Although Microsoft Azure storage is a service, the implementation is similar to disk and tape to allow for a consistent and seamless restore experience for all the three devices.  
<files_or_filegroups>::=   
{   
   FILE = { logical_file_name_in_backup | @logical_file_name_in_backup_var }   
 | FILEGROUP = { logical_filegroup_name | @logical_filegroup_name_var }   
 | READ_WRITE_FILEGROUPS  
}   
  
<general_WITH_options> [ ,...n ]::=   
--Restore Operation Options  
   MOVE 'logical_file_name_in_backup' TO 'operating_system_file_name'   
          [ ,...n ]   
 | REPLACE   
 | RESTART   
 | RESTRICTED_USER  | CREDENTIAL  
  
--Backup Set Options  
 | FILE = { backup_set_file_number | @backup_set_file_number }   
 | PASSWORD = { password | @password_variable }   
  
--Media Set Options  
 | MEDIANAME = { media_name | @media_name_variable }   
 | MEDIAPASSWORD = { mediapassword | @mediapassword_variable }   
 | BLOCKSIZE = { blocksize | @blocksize_variable }   
  
--Data Transfer Options  
 | BUFFERCOUNT = { buffercount | @buffercount_variable }   
 | MAXTRANSFERSIZE = { maxtransfersize | @maxtransfersize_variable }  
  
--Error Management Options  
 | { CHECKSUM | NO_CHECKSUM }   
 | { STOP_ON_ERROR | CONTINUE_AFTER_ERROR }   
  
--Monitoring Options  
 | STATS [ = percentage ]   
  
--Tape Options. 
 | { REWIND | NOREWIND }   
 | { UNLOAD | NOUNLOAD }   
  
<replication_WITH_option>::=  
 | KEEP_REPLICATION   
  
<change_data_capture_WITH_option>::=  
 | KEEP_CDC  
  
<FILESTREAM_WITH_option>::=  
 | FILESTREAM ( DIRECTORY_NAME = directory_name )  
  
<service_broker_WITH_options>::=   
 | ENABLE_BROKER   
 | ERROR_BROKER_CONVERSATIONS   
 | NEW_BROKER  
  
\<point_in_time_WITH_options-RESTORE_DATABASE>::=   
 | {  
   STOPAT = { 'datetime'| @datetime_var }   
 | STOPATMARK = 'lsn:lsn_number'  
                 [ AFTER 'datetime']   
 | STOPBEFOREMARK = 'lsn:lsn_number'  
                 [ AFTER 'datetime']   
   }   
  
\<point_in_time_WITH_options-RESTORE_LOG>::=   
 | {  
   STOPAT = { 'datetime'| @datetime_var }   
 | STOPATMARK = { 'mark_name' | 'lsn:lsn_number' }  
                 [ AFTER 'datetime']   
 | STOPBEFOREMARK = { 'mark_name' | 'lsn:lsn_number' }  
                 [ AFTER 'datetime']   
   }  
```  
  
## Arguments  
For descriptions of the arguments, see [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md).  
  
## About Restore Scenarios  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports a variety of restore scenarios:  
  
- Complete database restore  
  
  Restores the entire database, beginning with a full database backup, which may be followed by restoring a differential database backup (and log backups). For more information, see [Complete Database Restores &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-simple-recovery-model.md) or [Complete Database Restores &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-full-recovery-model.md).  
  
- File restore  
  
  Restores a file or filegroup in a multi-filegroup database. Note that under the simple recovery model, the file must belong to a read-only filegroup. After a full file restore, a differential file backup can be restored. For more information, see [File Restores &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/file-restores-full-recovery-model.md) and [File Restores &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/file-restores-simple-recovery-model.md).  
  
- Page restore  
  
  Restores individual pages. Page restore is available only under the full and bulk-logged recovery models. For more information, see [Restore Pages &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-pages-sql-server.md).  
  
- Piecemeal restore  
  
  Restores the database in stages, beginning with the primary filegroup and one or more secondary filegroups. A piecemeal restore begins with a RESTORE DATABASE using the PARTIAL option and specifying one or more secondary filegroups to be restored. For more information, see [Piecemeal Restores &#40;SQL Server&#41;](../../relational-databases/backup-restore/piecemeal-restores-sql-server.md).  
  
- Recovery only  
  
  Recovers data that is already consistent with the database and needs only to be made available. For more information, see [Recover a Database Without Restoring Data &#40;Transact-SQL&#41;](../../relational-databases/backup-restore/recover-a-database-without-restoring-data-transact-sql.md).  
  
- Transaction log restore.  
  
  Under the full or bulk-logged recovery model, restoring log backups is required to reach the desired recovery point. For more information about restoring log backups, see [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md).  
  
- Prepare an availability database for an Always On availability group  
  
  For more information, see [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md).  
  
- Prepare a mirror database for database mirroring  
  
  For more information, see [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md).  
  
- Online Restore  
  
  > [!NOTE] 
  > Online restore is allowed only in Enterprise edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
  Where online restore is supported, if the database is online, file restores and page restores are automatically online restores and, also, restores of secondary filegroup after the initial stage of a piecemeal restore.  
  
  > [!NOTE] 
  > Online restores can involve [deferred transactions](../../relational-databases/backup-restore/deferred-transactions-sql-server.md).  
  
  For more information, see [Online Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/online-restore-sql-server.md).  
  
## Additional Considerations About RESTORE Options  
  
### Discontinued RESTORE Keywords  
The following keywords were discontinued in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]:  
|Discontinued keyword|Replaced by...|Example of replacement keyword|  
|--------------------------|------------------|------------------------------------|  
|LOAD|RESTORE|`RESTORE DATABASE`|  
|TRANSACTION|LOG|`RESTORE LOG`|  
|DBO_ONLY|RESTRICTED_USER|`RESTORE DATABASE ... WITH RESTRICTED_USER`|  
  
### RESTORE LOG  
RESTORE LOG can include a file list to allow for creation of files during roll forward. This is used when the log backup contains log records written when a file was added to the database.  
  
> [!NOTE] 
> For a database using the full or bulk-logged recovery model, in most cases you must back up the tail of the log before restoring the database. Restoring a database without first backing up the tail of the log results in an error, unless the RESTORE DATABASE statement contains either the WITH REPLACE or the WITH STOPAT clause, which must specify a time or transaction that occurred after the end of the data backup. For more information about tail-log backups, see [Tail-Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/tail-log-backups-sql-server.md).  
  
### Comparison of RECOVERY and NORECOVERY  
Roll back is controlled by the RESTORE statement through the [ RECOVERY | NORECOVERY ] options:  
  
- NORECOVERY specifies that roll back not occur. This allows roll forward to continue with the next statement in the sequence.  
  
In this case, the restore sequence can restore other backups and roll them forward.  
  
- RECOVERY (the default) indicates that roll back should be performed after roll forward is completed for the current backup.  
  
Recovering the database requires that the entire set of data being restored (the *roll forward set*) is consistent with the database. If the roll forward set has not been rolled forward far enough to be consistent with the database and RECOVERY is specified, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] issues an error.  
  
## Compatibility Support  
Backups of **master**, **model** and **msdb** that were created by using an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot be restored by [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
> [!NOTE]
> No [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup be restored to an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] than the version on which the backup was created.  
  
Each version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses a different default path than earlier versions. Therefore, to restore a database that was created in the default location for earlier version backups, you must use the MOVE option. For information about the new default path, see [File Locations for Default and Named Instances of SQL Server](../../sql-server/install/file-locations-for-default-and-named-instances-of-sql-server.md).  
  
After you restore an earlier version database to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the database is automatically upgraded. Typically, the database becomes available immediately. However, if a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database has full-text indexes, the upgrade process either imports, resets, or rebuilds them, depending on the setting of the  **upgrade_option** server property. If the upgrade option is set to import (**upgrade_option** = 2) or rebuild (**upgrade_option** = 0), the full-text indexes will be unavailable during the upgrade. Depending the amount of data being indexed, importing can take several hours, and rebuilding can take up to ten times longer. Note also that when the upgrade option is set to import, the associated full-text indexes are rebuilt if a full-text catalog is not available. To change the setting of the **upgrade_option** server property, use [sp_fulltext_service](../../relational-databases/system-stored-procedures/sp-fulltext-service-transact-sql.md).  
  
When a database is first attached or restored to a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a copy of the database master key (encrypted by the service master key) is not yet stored in the server. You must use the **OPEN MASTER KEY** statement to decrypt the database master key (DMK). Once the DMK has been decrypted, you have the option of enabling automatic decryption in the future by using the **ALTER MASTER KEY REGENERATE** statement to provision the server with a copy of the DMK, encrypted with the service master key (SMK). When a database has been upgraded from an earlier version, the DMK should be regenerated to use the newer AES algorithm. For more information about regenerating the DMK, see [ALTER MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/alter-master-key-transact-sql.md). The time required to regenerate the DMK key to upgrade to AES depends upon the number of objects protected by the DMK. Regenerating the DMK key to upgrade to AES is only necessary once, and has no impact on future regenerations as part of a key rotation strategy.  
  
## General Remarks  
During an offline restore, if the specified database is in use, RESTORE forces the users off after a short delay. For online restore of a non-primary filegroup, the database can stay in use except when the filegroup being restored is being taken offline. Any data in the specified database is replaced by the restored data.  
  
For more information about database recovery, see [Restore and Recovery Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md).  
  
Cross-platform restore operations, even between different processor types, can be performed as long as the collation of the database is supported by the operating system.  
  
RESTORE can be restarted after an error. In addition, you can instruct RESTORE to continue despite errors, and it restores as much data as possible (see the CONTINUE_AFTER_ERROR option).  
  
RESTORE is not allowed in an explicit or implicit transaction.  
  
Restoring a damaged **master** database is performed using a special procedure. For more information, see [Back Up and Restore of System Databases &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md).  
  
Restoring a database clears the plan cache for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Clearing the plan cache causes a recompilation of all subsequent execution plans and can cause a sudden, temporary decrease in query performance. For each cleared cachestore in the plan cache, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log contains the following informational message: " [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has encountered %d occurrence(s) of cachestore flush for the '%s' cachestore (part of plan cache) due to some database maintenance or reconfigure operations". This message is logged every five minutes as long as the cache is flushed within that time interval.  
  
To restore an availability database, first restore the database to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and then add the database to the availability group.  

## Interoperability  
  
### Database Settings and Restoring  
During a restore, most of the database options that are settable using ALTER DATABASE are reset to the values in force at the time of the end of backup.  
  
Using the WITH RESTRICTED_USER option, however, overrides this behavior for the user access option setting. This setting is always set following a RESTORE statement, which includes the WITH RESTRICTED_USER option.  
  
### Restoring an Encrypted Database  
To restore a database that is encrypted, you must have access to the certificate or asymmetric key that was used to encrypt the database. Without the certificate or asymmetric key, the database cannot be restored. As a result, the certificate that is used to encrypt the database encryption key must be retained as long as the backup is needed. For more information, see [SQL Server Certificates and Asymmetric Keys](../../relational-databases/security/sql-server-certificates-and-asymmetric-keys.md).  
  
### Restoring a Database Enabled for vardecimal Storage  
Backup and restore work correctly with the **vardecimal** storage format. For more information about **vardecimal** storage format, see [sp_db_vardecimal_storage_format &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-db-vardecimal-storage-format-transact-sql.md).  
  
### Restore Full-Text Data  
Full-text data is restored together with other database data during a complete restore. Using the regular `RESTORE DATABASE database_name FROM backup_device` syntax, the full-text files are restored as part of the database file restore.  
  
The RESTORE statement also can be used to perform restores to alternate locations, differential restores, file and filegroup restores, and differential file and filegroup restores of full-text data. In addition, RESTORE can restore full-text files only, as well as with database data.  
  
> [!NOTE] 
> Full-text catalogs imported from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] are still treated as database files. For these, the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] procedure for backing up full-text catalogs remains applicable, except that pausing and resuming during the backup operation are no longer necessary. For more information, see [Backing Up and Restoring Full-Text Catalogs](https://go.microsoft.com/fwlink/?LinkId=107381).  
  
## Metadata  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] includes backup and restore history tables that track the backup and restore activity for each server instance. When a restore is performed, the backup history tables are also modified. For information on these tables, see [Backup History and Header Information &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-history-and-header-information-sql-server.md).  
  
##  <a name="REPLACEoption"></a> REPLACE Option Impact  
REPLACE should be used rarely and only after careful consideration. Restore normally prevents accidentally overwriting a database with a different database. If the database specified in a RESTORE statement already exists on the current server and the specified database family GUID differs from the database family GUID recorded in the backup set, the database is not restored. This is an important safeguard.  
  
The REPLACE option overrides several important safety checks that restore normally performs. The overridden checks are as follows:  
  
- Restoring over an existing database with a backup taken of another database.  
  
  With the REPLACE option, restore allows you to overwrite an existing database with whatever database is in the backup set, even if the specified database name differs from the database name recorded in the backup set. This can result in accidentally overwriting a database by a different database.  
  
- Restoring over a database using the full or bulk-logged recovery model where a tail-log backup has not been taken and the STOPAT option is not used.  
  
  With the REPLACE option, you can lose committed work, because the log written most recently has not been backed up.  
  
- Overwriting existing files.  
  
  For example, a mistake could allow overwriting files of the wrong type, such as .xls files, or that are being used by another database that is not online. Arbitrary data loss is possible if existing files are overwritten, although the restored database is complete.  
  
## Redoing a Restore  
Undoing the effects of a restore is not possible; however, you can negate the effects of the data copy and roll forward by starting over on a per-file basis. To start over, restore the desired file and perform the roll forward again. For example, if you accidentally restored too many log backups and overshot your intended stopping point, you would have to restart the sequence.  
  
A restore sequence can be aborted and restarted by restoring the entire contents of the affected files.  
  
## Reverting a Database to a Database Snapshot  
A *revert database operation* (specified using the DATABASE_SNAPSHOT option) takes a full source database back in time by reverting it to the time of a database snapshot, that is, overwriting the source database with data from the point in time maintained in the specified database snapshot. Only the snapshot to which you are reverting can currently exist. The revert operation then rebuilds the log (therefore, you cannot later roll forward a reverted database to the point of user error).  
  
Data loss is confined to updates to the database since the snapshot's creation. The metadata of a reverted database is the same as the metadata at the time of snapshot creation. However, reverting to a snapshot drops all the full-text catalogs.  
  
Reverting from a database snapshot is not intended for media recovery. Unlike a regular backup set, the database snapshot is an incomplete copy of the database files. If either the database or the database snapshot is corrupted, reverting from a snapshot is likely to be impossible. Furthermore, even when possible, reverting in the event of corruption is unlikely to correct the problem.  
  
### Restrictions on Reverting  
Reverting is unsupported under the following conditions:  
  
- The source database contains any read-only or compressed filegroups.  
- Any files are offline that were online when the snapshot was created.  
- More than one snapshot of the database currently exists.  
  
For more information, see [Revert a Database to a Database Snapshot](../../relational-databases/databases/revert-a-database-to-a-database-snapshot.md).  
  
## Security  
A backup operation may optionally specify passwords for a media set, a backup set, or both. When a password has been defined on a media set or backup set, you must specify the correct password or passwords in the RESTORE statement. These passwords prevent unauthorized restore operations and unauthorized appends of backup sets to media using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools. However, password-protected media can be overwritten by the BACKUP statement's FORMAT option.  
  
> [!IMPORTANT]  
>  The protection provided by this password is weak. It is intended to prevent an incorrect restore using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools by authorized or unauthorized users. It does not prevent the reading of the backup data by other means or the replacement of the password. [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]The best practice for protecting backups is to store backup tapes in a secure location or back up to disk files that are protected by adequate access control lists (ACLs). The ACLs should be set on the directory root under which backups are created.  
   
> [!NOTE]
> For information specific to SQL Server backup and restore with the Microsoft Azure Blob storage, see [SQL Server Backup and Restore with Microsoft Azure Blob Storage Service](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md).  
  
### Permissions  
If the database being restored does not exist, the user must have CREATE DATABASE permissions to be able to execute RESTORE. If the database exists, RESTORE permissions default to members of the **sysadmin** and **dbcreator** fixed server roles and the owner (**dbo**) of the database (for the FROM DATABASE_SNAPSHOT option, the database always exists).  
  
RESTORE permissions are given to roles in which membership information is always readily available to the server. Because fixed database role membership can be checked only when the database is accessible and undamaged, which is not always the case when RESTORE is executed, members of the **db_owner** fixed database role do not have RESTORE permissions.  
  
##  <a name="examples"></a> Examples  
All the examples assume that a full database backup has been performed.  
  
The RESTORE examples include the following:  
  
- A. [Restoring a full database](#restoring_full_db)  
- B. [Restoring full and differential database backups](#restoring_full_n_differential_db_backups)  
- C. [Restoring a database using RESTART syntax](#restoring_db_using_RESTART)  
- D. [Restoring a database and move files](#restoring_db_n_move_files)  
- E. [Copying a database using BACKUP and RESTORE](#copying_db_using_bnr)  
- F. [Restoring to a point-in-time using STOPAT](#restoring_to_pit_using_STOPAT)  
- G. [Restoring the transaction log to a mark](#restoring_transaction_log_to_mark)  
- H. [Restoring using TAPE syntax](#restoring_using_TAPE)  
- I. [Restoring using FILE and FILEGROUP syntax](#restoring_using_FILE_n_FG)  
- J. [Reverting from a database snapshot](#reverting_from_db_snapshot)  
- K. [Restoring from the Microsoft Azure Blob storage service](#Azure_Blob)  
  
> [!NOTE] 
> For additional examples, see the restore how-to topics that are listed in [Restore and Recovery Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md).  
  
###  <a name="restoring_full_db"></a> A. Restoring a full database  
The following example restores a full database backup from the `AdventureWorksBackups` logical backup device. For an example of creating this device, see [Backup Devices](../../relational-databases/backup-restore/backup-devices-sql-server.md).  
  
```sql  
RESTORE DATABASE AdventureWorks2012   
   FROM AdventureWorks2012Backups;  
```  
  
> [!NOTE] 
> For a database using the full or bulk-logged recovery model, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires in most cases that you back up the tail of the log before restoring the database. For more information, see [Tail-Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/tail-log-backups-sql-server.md).  
  
[&#91;Top of examples&#93;](#examples)  
  
###  <a name="restoring_full_n_differential_db_backups"></a> B. Restoring full and differential database backups  
The following example restores a full database backup followed by a differential backup from the `Z:\SQLServerBackups\AdventureWorks2012.bak` backup device, which contains both backups. The full database backup to be restored is the sixth backup set on the device (`FILE = 6`), and the differential database backup is the ninth backup set on the device (`FILE = 9`). As soon as the differential backup is recovered, the database is recovered.  
  
```sql  
RESTORE DATABASE AdventureWorks2012  
   FROM DISK = 'Z:\SQLServerBackups\AdventureWorks2012.bak'  
   WITH FILE = 6  
      NORECOVERY;  
RESTORE DATABASE AdventureWorks2012  
   FROM DISK = 'Z:\SQLServerBackups\AdventureWorks2012.bak'  
   WITH FILE = 9  
      RECOVERY;  
```  
  
[&#91;Top of examples&#93;](#examples)  
  
###  <a name="restoring_db_using_RESTART"></a> C. Restoring a database using RESTART syntax  
The following example uses the `RESTART` option to restart a `RESTORE` operation interrupted by a server power failure.  
  
```sql  
-- This database RESTORE halted prematurely due to power failure.  
RESTORE DATABASE AdventureWorks2012  
   FROM AdventureWorksBackups;  
-- Here is the RESTORE RESTART operation.  
RESTORE DATABASE AdventureWorks2012   
   FROM AdventureWorksBackups WITH RESTART;  
```  
  
[&#91;Top of examples&#93;](#examples)  
  
###  <a name="restoring_db_n_move_files"></a> D. Restoring a database and move files  
The following example restores a full database and transaction log and moves the restored database into the `C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data` directory.  
  
```sql  
RESTORE DATABASE AdventureWorks2012  
   FROM AdventureWorksBackups  
   WITH NORECOVERY,   
      MOVE 'AdventureWorks2012_Data' TO   
'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\NewAdvWorks.mdf',   
      MOVE 'AdventureWorks2012_Log'   
TO 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Data\NewAdvWorks.ldf';  
RESTORE LOG AdventureWorks2012  
   FROM AdventureWorksBackups  
   WITH RECOVERY;  
```  
  
[&#91;Top of examples&#93;](#examples)  
  
###  <a name="copying_db_using_bnr"></a> E. Copying a database using BACKUP and RESTORE  
The following example uses both the `BACKUP` and `RESTORE` statements to make a copy of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. The `MOVE` statement causes the data and log file to be restored to the specified locations. The `RESTORE FILELISTONLY` statement is used to determine the number and names of the files in the database being restored. The new copy of the database is named `TestDB`. For more information, see [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md).  
  
```sql  
BACKUP DATABASE AdventureWorks2012   
   TO AdventureWorksBackups ;  
  
RESTORE FILELISTONLY   
   FROM AdventureWorksBackups ;  
  
RESTORE DATABASE TestDB   
   FROM AdventureWorksBackups   
   WITH MOVE 'AdventureWorks2012_Data' TO 'C:\MySQLServer\testdb.mdf',  
   MOVE 'AdventureWorks2012_Log' TO 'C:\MySQLServer\testdb.ldf';  
GO  
```  
  
[&#91;Top of examples&#93;](#examples)  
  
###  <a name="restoring_to_pit_using_STOPAT"></a> F. Restoring to a point-in-time using STOPAT  
The following example restores a database to its state as of `12:00 AM` on `April 15, 2020` and shows a restore operation that involves multiple log backups. On the backup device, `AdventureWorksBackups`, the full database backup to be restored is the third backup set on the device (`FILE = 3`), the first log backup is the fourth backup set (`FILE = 4`), and the second log backup is the fifth backup set (`FILE = 5`).  
  
```sql  
RESTORE DATABASE AdventureWorks2012  
   FROM AdventureWorksBackups  
   WITH FILE=3, NORECOVERY;  
  
RESTORE LOG AdventureWorks2012  
   FROM AdventureWorksBackups  
   WITH FILE=4, NORECOVERY, STOPAT = 'Apr 15, 2020 12:00 AM';  
  
RESTORE LOG AdventureWorks2012  
   FROM AdventureWorksBackups  
   WITH FILE=5, NORECOVERY, STOPAT = 'Apr 15, 2020 12:00 AM';  
RESTORE DATABASE AdventureWorks2012 WITH RECOVERY;  
  
```  
  
[&#91;Top of examples&#93;](#examples)  
  
###  <a name="restoring_transaction_log_to_mark"></a> G. Restoring the transaction log to a mark  
The following example restores the transaction log to the mark in the marked transaction named `ListPriceUpdate`.  
  
```sql  
USE AdventureWorks2012  
GO  
BEGIN TRANSACTION ListPriceUpdate  
   WITH MARK 'UPDATE Product list prices';  
GO  
  
UPDATE Production.Product  
   SET ListPrice = ListPrice * 1.10  
   WHERE ProductNumber LIKE 'BK-%';  
GO  
  
COMMIT TRANSACTION ListPriceUpdate;  
GO  
  
-- Time passes. Regular database   
-- and log backups are taken.  
-- An error occurs in the database.  
USE master;  
GO  
  
RESTORE DATABASE AdventureWorks2012  
FROM AdventureWorksBackups  
WITH FILE = 3, NORECOVERY;  
GO  
  
RESTORE LOG AdventureWorks2012  
   FROM AdventureWorksBackups   
   WITH FILE = 4,  
   RECOVERY,   
   STOPATMARK = 'UPDATE Product list prices';  
```  
  
[&#91;Top of examples&#93;](#examples)  
  
###  <a name="restoring_using_TAPE"></a> H. Restoring using TAPE syntax  
The following example restores a full database backup from a `TAPE` backup device.  
  
```sql  
RESTORE DATABASE AdventureWorks2012   
   FROM TAPE = '\\.\tape0';  
```  
  
[&#91;Top of examples&#93;](#examples)  
  
###  <a name="restoring_using_FILE_n_FG"></a> I. Restoring using FILE and FILEGROUP syntax  
The following example restores a database named `MyDatabase` that has two files, one secondary filegroup, and one transaction log. The database uses the full recovery model.  
  
The database backup is the ninth backup set in the media set on a logical backup device named `MyDatabaseBackups`. Next, three log backups, which are in the next three backup sets (`10`, `11`, and `12`) on the `MyDatabaseBackups` device, are restored by using `WITH NORECOVERY`. After restoring the last log backup, the database is recovered.  
  
> [!NOTE] 
> Recovery is performed as a separate step to reduce the possibility of you recovering too early, before all of the log backups have been restored.  
  
In the `RESTORE DATABASE`, notice that there are two types of `FILE` options. The `FILE` options preceding the backup device name specify the logical file names of the database files that are to be restored from the backup set; for example, `FILE = 'MyDatabase_data_1'`. This backup set is not the first database backup in the media set; therefore, its position in the media set is indicated by using the `FILE` option in the `WITH` clause, `FILE=9`.  
  
```sql  
RESTORE DATABASE MyDatabase  
   FILE = 'MyDatabase_data_1',  
   FILE = 'MyDatabase_data_2',  
   FILEGROUP = 'new_customers'  
   FROM MyDatabaseBackups  
   WITH   
      FILE = 9,  
      NORECOVERY;  
GO  
-- Restore the log backups.  
RESTORE LOG MyDatabase  
   FROM MyDatabaseBackups  
   WITH FILE = 10,   
      NORECOVERY;  
GO  
RESTORE LOG MyDatabase  
   FROM MyDatabaseBackups  
   WITH FILE = 11,   
      NORECOVERY;  
GO  
RESTORE LOG MyDatabase  
   FROM MyDatabaseBackups  
   WITH FILE = 12,   
      NORECOVERY;  
GO  
--Recover the database:  
RESTORE DATABASE MyDatabase WITH RECOVERY;  
GO  
```  
  
[&#91;Top of examples&#93;](#examples)  
  
###  <a name="reverting_from_db_snapshot"></a> J. Reverting from a database snapshot  
The following example reverts a database to a database snapshot. The example assumes that only one snapshot currently exists on the database. For an example of how to create this database snapshot, see [Create a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/create-a-database-snapshot-transact-sql.md).  
  
> [!NOTE] 
> Reverting to a snapshot drops all the full-text catalogs.  
  
```sql  
USE master;    
RESTORE DATABASE AdventureWorks2012 FROM DATABASE_SNAPSHOT = 'AdventureWorks_dbss1800';  
GO  
```  
For more information, see [Revert a Database to a Database Snapshot](../../relational-databases/databases/revert-a-database-to-a-database-snapshot.md).  

[&#91;Top of examples&#93;](#examples)  
  
###  <a name="Azure_Blob"></a> K. Restoring from the Microsoft Azure Blob storage service  
The three examples below involve the use of the Microsoft Azure storage service.  The storage Account name is `mystorageaccount`.  The container for data files is called `myfirstcontainer`.  The container for backup files is called `mysecondcontainer`.  A stored access policy has been created with read, write, delete, and list, rights for each container.  SQL Server credentials were created using Shared Access Signatures that are associated with the Stored Access Policies.  For information specific to SQL Server backup and restore with the Microsoft Azure Blob storage, see [SQL Server Backup and Restore with Microsoft Azure Blob Storage Service](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md).  

**K1.  Restore a full database backup from the Microsoft Azure storage service**  
A full database backup, located at `mysecondcontainer`, of `Sales` will be restored to `myfirstcontainer`.  `Sales` does not currently exist on the server. 

```sql
RESTORE DATABASE Sales
  FROM URL = 'https://mystorageaccount.blob.core.windows.net/mysecondcontainer/Sales.bak'   
  WITH  MOVE 'Sales_Data1' to 'https://mystorageaccount.blob.core.windows.net/myfirstcontainer/Sales_Data1.mdf', 
  MOVE 'Sales_log' to 'https://mystorageaccount.blob.core.windows.net/myfirstcontainer/Sales_log.ldf', 
  STATS = 10;
```

**K2. Restore a full database backup from the Microsoft Azure storage service to local storage**  
A full database backup, located at `mysecondcontainer`, of `Sales` will be restored to local storage.  `Sales` does not currently exist on the server.

```sql
RESTORE DATABASE Sales
  FROM URL = 'https://mystorageaccount.blob.core.windows.net/mysecondcontainer/Sales.bak'   
  WITH  MOVE 'Sales_Data1' to 'H:\DATA\Sales_Data1.mdf', 
  MOVE 'Sales_log' to 'O:\LOG\Sales_log.ldf', 
  STATS = 10;
```
  
**K3. Restore a full database backup from local storage to the Microsoft Azure storage service**  
```sql
RESTORE DATABASE Sales
  FROM DISK = 'E:\BAK\Sales.bak'
  WITH  MOVE 'Sales_Data1' to 'https://mystorageaccount.blob.core.windows.net/myfirstcontainer/Sales_Data1.mdf', 
  MOVE 'Sales_log' to 'https://mystorageaccount.blob.core.windows.net/myfirstcontainer/Sales_log.ldf', 
  STATS = 10;
```  
  

  
[&#91;Top of examples&#93;](#examples)  
  
## Much more information!!  
- [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md) 
- [Back Up and Restore of System Databases (SQL Server)](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md) 
- [Restore a Database Backup Using SSMS](../../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md)
- [Back Up and Restore Full-Text Catalogs and Indexes](../../relational-databases/search/back-up-and-restore-full-text-catalogs-and-indexes.md)   
- [Back Up and Restore Replicated Databases](../../relational-databases/replication/administration/back-up-and-restore-replicated-databases.md)   
- [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
- [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md)   
- [RESTORE REWINDONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-rewindonly-transact-sql.md)   
- [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)   
- [RESTORE FILELISTONLY (Transact-SQL)](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md)  
- [RESTORE HEADERONLY (Transact-SQL)](../../t-sql/statements/restore-statements-headeronly-transact-sql.md)  
- [Backup History and Header Information &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-history-and-header-information-sql-server.md)  
  
::: moniker-end
::: moniker range="=azuresqldb-mi-current||=sqlallproducts-allversions"

> [!div class="mx-tdCol2BreakAll"]
> ||||
> |-|-|-|
> |[SQL Server](restore-statements-transact-sql.md?view=sql-server-2016)|**_\* SQL Database<br />Managed Instance \*_**|[Parallel<br />Data Warehouse](restore-statements-transact-sql.md?view=aps-pdw-2016)

&nbsp;

## Azure SQL Database Managed Instance

This command enables you to restore an entire database from a full database backup (a complete restore) from Azure Blob Storage account.

For other supported RESTORE commands, see:
- [RESTORE FILELISTONLY (Transact-SQL)](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md)  
- [RESTORE HEADERONLY (Transact-SQL)](../../t-sql/statements/restore-statements-headeronly-transact-sql.md) 
- [RESTORE LABELONLY ONLY (Transact-SQL)](../../t-sql/statements/restore-statements-labelonly-transact-sql.md)
- [RESTORE VERIFYONLY (Transact-SQL)](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)   

> [!IMPORTANT]
> To restore from Azure SQL Database Managed Instance automatic backups, see [SQL Database Restore](https://docs.microsoft.com/azure/sql-database/sql-database-recovery-using-backups).
  
## Syntax  
  
```sql  
--To Restore an Entire Database from a Full database backup (a Complete Restore):  
RESTORE DATABASE { database_name | @database_name_var }   
 FROM URL = { 'physical_device_name' | @physical_device_name_var } [ ,...n ]   
[;]  
  
```  
   
## Arguments  

DATABASE  
  
Specifies the target database.  
  
FROM URL

Specifies one or more backup devices placed on URLs that will be used for the restore operation. The URL format is used for restoring backups from the Microsoft Azure storage service. 

> [!IMPORTANT]  
> In order to restore from multiple devices when restoring from URL, you must use Shared Access Signature (SAS) tokens. For examples creating a Shared Access Signature, see [SQL Server Backup to URL](../../relational-databases/backup-restore/sql-server-backup-to-url.md) and [Simplifying creation of SQL Credentials with Shared Access Signature (SAS) tokens on Azure Storage with Powershell](https://blogs.msdn.com/b/sqlcat/archive/2015/03/21/simplifying-creation-sql-credentials-with-shared-access-signature-sas-keys-on-azure-storage-containers-with-powershell.aspx).  
  
*n*  
Is a placeholder that indicates that up to 64 backup devices may be specified in a comma-separated list.  
 
## General Remarks

As a prerequisite, you need to create a credential with the name that matches the blob storage account url, and Shared Access Signature placed as secret. RESTORE command will lookup credential using the blob storage url to find the information required to read the backup device.

RESTORE operation is asynchronous - the restore continues even if client connection breaks. If your connection is dropped, you can check [sys.dm_operation_status](../../relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database.md) view for the status of a restore operation (as well as for CREATE and DROP database). 

The following database options are set/overridden and cannot be changed later:

- NEW_BROKER (if broker is not enabled in .bak file)
- ENABLE_BROKER (if broker is not enabled in .bak file)
- AUTO_CLOSE=OFF (if a database in .bak file has AUTO_CLOSE=ON)
- RECOVERY FULL (if a database in .bak file has SIMPLE or BULK_LOGGED recovery mode)
- Memory optimized filegroup is added and called XTP if it was not in the source .bak file. Any existing memory optimized filegroup is renamed to XTP
- SINGLE_USER and RESTRICTED_USER options are converted to MULTI_USER

## Limitations - SQL Database Managed Instance
These limitations apply:

- .BAK files containing multiple backup sets cannot be restored.
- .BAK files containing multiple log files cannot be restored.
- Restore will fail if .bak contains FILESTREAM data.
- Backups containing databases that have active In-memory objects cannot be restored to a General Purpose managed instance.
- Backups containing databases in read-only mode cannot currently be restored. This limitation will be removed soon.

For more information, see [Managed Instance](/azure/sql-database/sql-database-managed-instance)

## Restoring an Encrypted Database  
To restore a database that is encrypted, you must have access to the certificate or asymmetric key that was used to encrypt the database. Without the certificate or asymmetric key, the database cannot be restored. As a result, the certificate that is used to encrypt the database encryption key must be retained as long as the backup is needed. For more information, see [SQL Server Certificates and Asymmetric Keys](../../relational-databases/security/sql-server-certificates-and-asymmetric-keys.md).  
    
## Permissions  
The user must have CREATE DATABASE permissions to be able to execute RESTORE.  
```
CREATE LOGIN mylogin WITH PASSWORD = 'Very Strong Pwd123!';
GRANT CREATE ANY DATABASE TO [mylogin];
```  
RESTORE permissions are given to roles in which membership information is always readily available to the server. Because fixed database role membership can be checked only when the database is accessible and undamaged, which is not always the case when RESTORE is executed, members of the **db_owner** fixed database role do not have RESTORE permissions.  
  
##  <a name="examples"></a> Examples  
The following examples restore a copy only database backup from URL, including the creation of a credential.  
  
###  <a name="restore-mi-database"></a> A. Restore database from four backup devices.   
```sql

-- Create credential
CREATE CREDENTIAL [https://mybackups.blob.core.windows.net/wide-world-importers]
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
       SECRET = 'sv=2017-11-09&ss=bq&srt=sco&sp=rl&se=2022-06-19T22:41:07Z&st=2018-06-01T14:41:07Z&spr=https&sig=s7wddcf0w%3D';
GO
-- Restore database
RESTORE DATABASE WideWorldImportersStandard
FROM URL = N'https://mybackups.blob.core.windows.net/wide-world-importers/00-WideWorldImporters-Standard.bak',
URL = N'https://mybackups.blob.core.windows.net/wide-world-importers/01-WideWorldImporters-Standard.bak',
URL = N'https://mybackups.blob.core.windows.net/wide-world-importers/02-WideWorldImporters-Standard.bak',
URL = N'https://mybackups.blob.core.windows.net/wide-world-importers/03-WideWorldImporters-Standard.bak'
```
The following error is shown if the database already exists:
```
Msg 1801, Level 16, State 1, Line 9
Database 'WideWorldImportersStandard' already exists. Choose a different database name.
```
###  <a name="restore-mi-database-variables"></a> B. Restore database specified via variable.  

```
DECLARE @db_name sysname = 'WideWorldImportersStandard';
DECLARE @url nvarchar(400) = N'https://mybackups.blob.core.windows.net/wide-world-importers/WideWorldImporters-Standard.bak';

RESTORE DATABASE @db_name 
FROM URL = @url
```  

### <a name="restore-mi-database-progress"></a> C. Track progress of restore statement. 

```
SELECT	query = a.text, start_time, percent_complete,
		eta = dateadd(second,estimated_completion_time/1000, getdate()) 
FROM sys.dm_exec_requests r
	CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) a 
WHERE r.command = 'RESTORE DATABASE'
```

> [!Note]
> This view will probably show two restore requests. One is original RESTORE statement sent by the client, and the another one is background RESTORE statement that is executing even if the client connection fails.

::: moniker-end
::: moniker range=">=aps-pdw-2016||=sqlallproducts-allversions"

> [!div class="mx-tdCol2BreakAll"]
> ||||
> |-|-|-|
> |[SQL Server](restore-statements-transact-sql.md?view=sql-server-2016)|[SQL Database<br />Managed Instance](restore-statements-transact-sql.md?view=azuresqldb-mi-current)|**_\* Parallel<br />Data Warehouse \*_**

&nbsp;

## Parallel Data Warehouse


Restores a [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] user database from a database backup to a [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] appliance. The database is restored from a backup that was previously created by the [!INCLUDE[ssPDW](../../includes/sspdw-md.md)][BACKUP DATABASE &#40;Parallel Data Warehouse&#41;](../../t-sql/statements/backup-transact-sql.md) command. Use the backup and restore operations to build a disaster recovery plan, or to move databases from one appliance to another.  
  
> [!NOTE]  
>  Restoring master includes restoring appliance login information. To restore master, use the [Restore the master Database &#40;Transact-SQL&#41;](../../relational-databases/backup-restore/restore-the-master-database-transact-sql.md) page in the **Configuration Manager** tool. An administrator with access to the Control node can perform this operation.  
For more information about [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] database backups, see "Backup and Restore" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].  
  
## Syntax  
  
```sql  
  
-- Restore the master database  
-- Use the Configuration Manager tool.  
  
Restore a full user database backup.  
RESTORE DATABASE database_name   
    FROM DISK = '\\UNC_path\full_backup_directory'  
[;]  
  
--Restore a full user database backup and then a differential backup.  
RESTORE DATABASE database_name  
    FROM DISK = '\\UNC_path\differential_backup_directory'   
    WITH [ ( ] BASE = '\\UNC_path\full_backup_directory' [ ) ]   
[;]  
  
--Restore header information for a full or differential user database backup.  
RESTORE HEADERONLY   
    FROM DISK = '\\UNC_path\backup_directory'  
[;]  
```  
  
## Arguments  
RESTORE DATABASE *database_name*  
Specifies to restore a user database to a database called *database_name*. The restored database can have a different name than the source database that was backed up. *database_name* cannot already exist as a database on the destination appliance. For more details on permitted database names, see "Object Naming Rules" in the [!INCLUDE[pdw-product-documentation](../../includes/pdw-product-documentation-md.md)].  
  
Restoring a user database restores a full database backup and then optionally restores a differential backup to the appliance. A restore of a user database includes restoring database users, and database roles.  
  
FROM DISK = '\\\\*UNC_path*\\*backup_directory*'  
The network path and directory from which [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] will restore the backup files. For example, FROM DISK = '\\\xxx.xxx.xxx.xxx\backups\2012\Monthly\08.2012.Mybackup'.  
  
*backup_directory*  
Specifies the name of a directory that contains the full or differential backup. For example, you can perform a RESTORE HEADERONLY operation on a full or differential backup.  
  
*full_backup_directory*  
Specifies the name of a directory that contains the full backup.  
  
*differential_backup_directory*  
Specifies the name of the directory that contains the differential backup.  
  
- The path and backup directory must already exist and must be specified as a fully qualified universal naming convention (UNC) path.  
- The path to the backup directory cannot be a local path and it cannot be a location on any of the [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] appliance nodes.  
- The maximum length of the UNC path and backup directory name is 200 characters.  
- The server or host must be specified as an IP address.  
  
RESTORE HEADERONLY  
Specifies to return only the header information for one user database backup. Among other fields, the header includes the text description of the backup, and the backup name. The backup name does not need to be the same as the name of the directory that stores the backup files.  
  
RESTORE HEADERONLY results are patterned after the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] RESTORE HEADERONLY results. The result has over 50 columns, which are not all used by [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]. For a description of the columns in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] RESTORE HEADERONLY results, see [RESTORE HEADERONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-headeronly-transact-sql.md).  
  
## Permissions  
Requires the **CREATE ANY DATABASE** permission.  
  
Requires a Windows account that has permission to access and read from the backup directory. You must also store the Windows account name and password in [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].  
  
- To verify the credentials are already there, use [sys.dm_pdw_network_credentials &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-network-credentials-transact-sql.md).  
- To add or update the credentials, use [sp_pdw_add_network_credentials &#40;SQL Data Warehouse&#41;](../../relational-databases/system-stored-procedures/sp-pdw-add-network-credentials-sql-data-warehouse.md).  
- To remove credentials from [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use [sp_pdw_remove_network_credentials &#40;SQL Data Warehouse&#41;](../../relational-databases/system-stored-procedures/sp-pdw-remove-network-credentials-sql-data-warehouse.md).  
  
## Error Handling  
The RESTORE DATABASE command results in errors under the following conditions:  
  
- The name of the database to restore already exists on the target appliance. To avoid this, choose a unique database name, or drop the existing database before running the restore.  
- There is an invalid set of backup files in the backup directory.  
- The login permissions are not sufficient to restore a database.  
- [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] does not have the correct permissions to the network location where the backup files are located.  
- The network location for the backup directory does not exist, or is not available.  
- There is insufficient disk space on the Compute nodes or Control node. [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] does not confirm that sufficient disk space exists on the appliance before initiating the restore. Therefore, it is possible to generate an out-of-disk-space error while running the RESTORE DATABASE statement. When insufficient disk space occurs, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] rolls back the restore.  
- The target appliance to which the database is being restored has fewer Compute nodes than the source appliance from which the database was backed up.  
- The database restore is attempted from within a transaction.  
  
## General Remarks  
[!INCLUDE[ssPDW](../../includes/sspdw-md.md)] tracks the success of database restores. Before restoring a differential database backup, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] verifies the full database restore finished successfully.  
  
After a restore, the user database will have database compatibility level 120. This is true for all databases regardless of their original compatibility level.  
  
**Restoring to an Appliance With a Larger Number of Compute Nodes**  
Run [DBCC SHRINKLOG (Azure SQL Data Warehouse)](../../t-sql/database-console-commands/dbcc-shrinklog-azure-sql-data-warehouse.md) after restoring a database from a smaller to larger appliance since redistribution will increase transaction log.  

Restoring a backup to an appliance with a larger number of Compute nodes grows the allocated database size in proportion to the number of Compute nodes.  
  
For example, when restoring a 60 GB database from a 2-node appliance (30 GB per node) to a 6-node appliance, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] creates a 180 GB database (6 nodes with 30 GB per node) on the 6-node appliance. [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] initially restores the database to 2 nodes to match the source configuration, and then redistributes the data to all 6 nodes.  
  
After the redistribution each Compute node will contain less actual data and more free space than each Compute node on the smaller source appliance. Use the additional space to add more data to the database. If the restored database size is larger than you need, you can use [ALTER DATABASE &#40;Parallel Data Warehouse&#41;](../../t-sql/statements/alter-database-transact-sql.md?&tabs=sqlpdw) to shrink the database file sizes.  
  
## Limitations and Restrictions  
For these limitations and restrictions, the source appliance is the appliance from which the database backup was created, and the target appliance is the appliance to which the database will be restored.  
  
- Restoring a database does not automatically rebuild statistics.  
- Only one RESTORE DATABASE or BACKUP DATABASE statement can be running on the appliance at any given time. If multiple backup and restore statements are submitted concurrently, the appliance will put them into a queue and process them one at a time.  
- You can only restore a database backup to a [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] target appliance that has the same number or more Compute nodes than the source appliance. The target appliance cannot have fewer Compute nodes than the source appliance.  
- You cannot restore a backup that was created on an appliance that has SQL Server 2012 PDW hardware to an appliance that has SQL Server 2008 R2 hardware. This holds true even if the appliance was originally purchased with the SQL Server 2008 R2 PDW hardware and is now running SQL Server 2012 PDW software.  
  
## Locking  
Takes an exclusive lock on the DATABASE object.  
  
## Examples  
  
### A. Simple RESTORE examples  
The following example restores a full backup to the `SalesInvoices2013` database. The backup files are stored in the \\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Full directory. The SalesInvoices2013 database cannot already exist on the target appliance or this command will fail with an error.  
  
```sql  
RESTORE DATABASE SalesInvoices2013  
FROM DISK = '\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Full';  
```  
  
### B. Restore a full and differential backup  
The following example restores a full, and then a differential backup to the SalesInvoices2013 database  
  
The full backup of the database is restored from the full backup which is stored in the '\\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Full' directory. If the restore completes successfully, the differential backup is restored to the SalesInvoices2013 database.  The differential backup is stored in the '\\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Diff' directory.  
  
```sql  
RESTORE DATABASE SalesInvoices2013  
    FROM DISK = '\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Diff'  
    WITH BASE = '\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Full'  
[;]  
  
```  
  
### C. Restoring the backup header  
This example restores the header information for database backup '\\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Full' . The command results in one row of information for the Invoices2013Full backup.  
  
```sql  
RESTORE HEADERONLY  
    FROM DISK = '\\xxx.xxx.xxx.xxx\backups\yearly\Invoices2013Full'  
[;]  
```  
  
You can use the header information to check the contents of a backup, or to make sure the target restoration appliance is compatible with the source backup appliance before attempting to restore the backup.  
  
## See Also  
[BACKUP DATABASE &#40;Parallel Data Warehouse&#41;](../../t-sql/statements/backup-transact-sql.md)  

::: moniker-end
