---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    3002    |    16    |    No    |    Cannot BACKUP or RESTORE a database snapshot.    |
|    3003    |    10    |    No    |    This BACKUP WITH DIFFERENTIAL will be based on more than one file backup. All those file backups must be restored before attempting to restore this differential backup.    |
|    3004    |    16    |    No    |    The primary filegroup cannot be backed up as a file backup because the database is using the SIMPLE recovery model. Consider taking a partial backup by specifying READ_WRITE_FILEGROUPS.    |
|    3005    |    10    |    No    |    The differential partial backup is including a read-only filegroup, '%ls'. This filegroup was read-write when the base partial backup was created, but was later changed to read-only access. We recommend that you create a separate file backup of the '%ls' filegroup now, and then create a new partial backup to provide a new base for later differential partial backups.    |
|    3006    |    16    |    No    |    The differential backup is not allowed because it would be based on more than one base backup. Multi-based differential backups are not allowed in the simple recovery model, and are never allowed for partial differential backups.    |
|    3007    |    16    |    No    |    The backup of the file or filegroup "%ls" is not permitted because it is not online. BACKUP can be performed by using the FILEGROUP or FILE clauses to restrict the selection to include only online data.    |
|    3008    |    16    |    No    |    The specified device type is not supported for backup mirroring.    |
|    3009    |    16    |    No    |    Could not insert a backup or restore history/detail record in the msdb database. This may indicate a problem with the msdb database. The backup/restore operation was still successful.    |
|    3010    |    16    |    No    |    Invalid backup mirror specification. All mirrors must have the same number of members.    |
|    3011    |    16    |    No    |    All backup devices must be of the same general class (for example, DISK and TAPE).    |
|    3012    |    17    |    No    |    VDI ran out of buffer when SQL Server attempted to send differential information to SQL Writer.    |
|    [3013](../../relational-databases/errors-events/mssqlserver-3013-database-engine-error.md)    |    16    |    No    |    BACKUP DATABASE is terminating abnormally.    |
|    3014    |    10    |    No    |    %hs successfully processed %I64d pages in %d.%03d seconds (%d.%03d MB/sec).    |
|    3015    |    10    |    No    |    %hs is not yet implemented.    |
|    3016    |    16    |    No    |    Backup of file '%ls' is not permitted because it contains pages subject to an online restore sequence. Complete the restore sequence before taking the backup, or restrict the backup to exclude this file.    |
|    3017    |    16    |    No    |    The restart-checkpoint file '%ls' could not be opened. Operating system error '%ls'. Correct the problem, or reissue the command without RESTART.    |
|    3018    |    10    |    No    |    The restart-checkpoint file '%ls' was not found. The RESTORE command will continue from the beginning as if RESTART had not been specified.    |
|    3019    |    16    |    No    |    The restart-checkpoint file '%ls' is from a previous interrupted RESTORE operation and is inconsistent with the current RESTORE command. The restart command must use the same syntax as the interrupted command, with the addition of the RESTART clause. Alternatively, reissue the current statement without the RESTART clause.    |
|    3021    |    16    |    No    |    Cannot perform a backup or restore operation within a transaction.    |
|    3022    |    10    |    No    |    This backup is a file backup of read-write data from a database that uses the simple recovery model. This is only appropriate if you plan to set the filegroup to read-only followed by a differential file backup. Consult Books Online for more information on managing read-only data for the simple recovery model. In particular, consider how partial backups are used.    |
|    [3023](../../relational-databases/errors-events/mssqlserver-3023-database-engine-error.md)    |    16    |    No    |    Backup, file manipulation operations (such as ALTER DATABASE ADD FILE) and encryption changes on a database must be serialized. Reissue the statement after the current backup or file manipulation operation is completed.    |
|    3024    |    16    |    No    |    You can only perform a full backup of the master database. Use BACKUP DATABASE to back up the entire master database.    |
|    3025    |    16    |    No    |    Missing database name. Reissue the statement specifying a valid database name.    |
|    3027    |    16    |    No    |    The filegroup "%.*ls" is not part of database "%.*ls".    |
|    3028    |    10    |    No    |    The restart-checkpoint file '%ls' was corrupted and is being ignored. The RESTORE command will continue from the beginning as if RESTART had not been specified.    |
|    3031    |    16    |    No    |    Option '%ls' conflicts with option(s) '%ls'. Remove the conflicting option and reissue the statement.    |
|    3032    |    16    |    No    |    One or more of the options (%ls) are not supported for this statement. Review the documentation for supported options.    |
|    3033    |    16    |    No    |    BACKUP DATABASE cannot be used on a database opened in emergency mode.    |
|    3034    |    16    |    No    |    No files were selected to be processed. You may have selected one or more filegroups that have no members.    |
|    3035    |    16    |    No    |    Cannot perform a differential backup for database "%ls", because a current database backup does not exist. Perform a full database backup by reissuing BACKUP DATABASE, omitting the WITH DIFFERENTIAL option.    |
|    3036    |    16    |    No    |    The database "%ls" is in warm-standby state (set by executing RESTORE WITH STANDBY) and cannot be backed up until the entire restore sequence is completed.    |
|    3038    |    16    |    No    |    The file name "%ls" is invalid as a backup device name. Reissue the BACKUP statement with a valid file name.    |
|    3039    |    16    |    No    |    Cannot perform a differential backup for file '%ls' because a current file backup does not exist. Reissue BACKUP DATABASE omitting the WITH DIFFERENTIAL option.    |
|    3040    |    10    |    No    |    An error occurred while informing replication of the backup. The backup will continue, but the replication environment should be inspected.    |
|    3041    |    16    |    Yes    |    BACKUP failed to complete the command %.*ls. Check the backup application log for detailed messages.    |
|    3042    |    10    |    No    |    BACKUP WITH CONTINUE_AFTER_ERROR successfully generated a backup of the damaged database. Refer to the SQL Server error log for information about the errors that were encountered.    |
|    [3043](../../relational-databases/errors-events/mssqlserver-3043-database-engine-error.md)    |    16    |    No    |    BACKUP '%ls' detected an error on page (%d:%d) in file '%ls'.    |
|    3044    |    16    |    No    |    Invalid zero-length device name. Reissue the BACKUP statement with a valid device name.    |
|    3045    |    16    |    No    |    BACKUP or RESTORE requires the NTFS file system for FILESTREAM and full-text support. The path "%.*ls" is not usable.    |
|    3046    |    16    |    No    |    Inconsistent metadata has been encountered. The only possible backup operation is a tail-log backup using the WITH CONTINUE_AFTER_ERROR or NO_TRUNCATE option.    |
|    3047    |    16    |    No    |    The BackupDirectory registry key is not configured correctly. This key should specify the root path where disk backup files are stored when full path names are not provided. This path is also used to locate restart checkpoint files for RESTORE.    |
|    3049    |    16    |    No    |    BACKUP detected corruption in the database log. Check the errorlog for more information.    |
|    3050    |    16    |    No    |    SQL Server could not send the differential information for database file '%ls' of database '%ls\\%ls' to the backup application because the differential information is too large to fit in memory, and an attempt to use a temporary file has failed.    |
|    3051    |    16    |    No    |    BACKUP LOG was unable to maintain mirroring consistency for database '%ls'. Database mirroring has been suspended.    |
|    3054    |    16    |    No    |    Differential file backups can include only read-only data for databases using the simple recovery model. Consider taking a partial backup by specifying READ_WRITE_FILEGROUPS.    |
|    3055    |    16    |    No    |    Backup destination "%.*ls" supports a FILESTREAM filegroup. This filegroup cannot be used as a backup destination. Rerun the BACKUP statement with a valid backup destination.    |
|    [3056](../../relational-databases/errors-events/mssqlserver-3056-database-engine-error.md)     |    16    |    No    |    The backup operation has detected an unexpected file in a FILESTREAM container. The backup operation will continue and include file '%ls'.    |
|    3057    |    16    |    No    |    Invalid device name. The length of the device name provided exceeds supported limit (maximum length is:%d). Reissue the BACKUP statement with a valid device name.    |
|    3058    |    10    |    No    |    File or device name exceeds the supported limit (maximum length is:%d) and will be truncated: %.*ls.    |
|    3098    |    16    |    No    |    The backup cannot be performed because '%ls' was requested after the media was formatted with an incompatible structure. To append to this media set, either omit '%ls' or specify '%ls'. Alternatively, you can create a new media set by using WITH FORMAT in your BACKUP statement. If you use WITH FORMAT on an existing media set, all its backup sets will be overwritten.    |
|    3101    |    16    |    No    |    Exclusive access could not be obtained because the database is in use.    |
|    3102    |    16    |    No    |    %ls cannot process database '%ls' because it is in use by this session. It is recommended that the master database be used when performing this operation.    |
|    3103    |    16    |    No    |    A partial restore sequence cannot be initiated by this command. To initiate a partial restore sequence, use the WITH PARTIAL clause of the RESTORE statement and provide a backup set which includes a full copy of at least the primary data file. The WITH PARTIAL clause of the RESTORE statement may not be used for any other purpose.    |
|    3104    |    16    |    No    |    RESTORE cannot operate on database '%ls' because it is configured for database mirroring. Use ALTER DATABASE to remove mirroring if you intend to restore the database.    |
|    3105    |    16    |    No    |    RESTORE cannot restore any more pages into file '%ls' because the maximum number of pages (%d) are already being restored. Either complete the restore sequence for the existing pages, or use RESTORE FILE to restore all pages in the file.    |
|    3106    |    16    |    No    |    The filegroup "%ls" is ambiguous. The identity in the backup set does not match the filegroup that is currently defined in the online database. To force the use of the filegroup in the backup set, take the database offline and then reissue the RESTORE command.    |
|    3107    |    16    |    No    |    The file "%ls" is ambiguous. The identity in the backup set does not match the file that is currently defined in the online database. To force the use of the file in the backup set, take the database offline and then reissue the RESTORE command.    |
|    3108    |    16    |    No    |    To restore the master database, the server must be running in single-user mode. For information on starting in single-user mode, see "How to: Start an Instance of SQL Server (sqlservr.exe)" in Books Online.    |
|    3109    |    16    |    No    |    Master can only be restored and fully recovered in a single step using a full database backup. Options such as NORECOVERY, STANDBY, and STOPAT are not supported.    |
|    3110    |    14    |    No    |    User does not have permission to RESTORE database '%.*ls'.    |
|    3111    |    16    |    No    |    Page %S_PGID is a control page which cannot be restored in isolation. To repair this page, the entire file must be restored.    |
|    3112    |    16    |    No    |    Cannot restore any database other than master when the server is in single user mode.    |
|    3113    |    16    |    No    |    Invalid data was detected.    |
|    3115    |    16    |    No    |    The database is using the simple recovery model. It is not possible to restore a subset of the read-write data.    |
|    3116    |    16    |    No    |    The supplied backup is not on the same recovery path as the database, and is ineligible for use for an online file restore.    |
|    3117    |    16    |    No    |    The log or differential backup cannot be restored because no files are ready to rollforward.    |
|    3118    |    16    |    No    |    The database "%ls" does not exist. RESTORE can only create a database when restoring either a full backup or a file backup of the primary file.    |
|    3119    |    16    |    No    |    Problems were identified while planning for the RESTORE statement. Previous messages provide details.    |
|    3120    |    16    |    No    |    This backup set will not be restored because all data has already been restored to a point beyond the time covered by this backup set.    |
|    3121    |    16    |    No    |    The file "%ls" is on a recovery path that is inconsistent with application of this backup set. RESTORE cannot continue.    |
|    3122    |    16    |    No    |    File initialization failed. RESTORE cannot continue.    |
|    3123    |    16    |    No    |    Invalid database name '%.*ls' specified for backup or restore operation.    |
|    3125    |    16    |    No    |    The database is using the simple recovery model. The data in the backup it is not consistent with the current state of the database. Restoring more data is required before recovery is possible. Either restore a full file backup taken since the data was marked read-only, or restore the most recent base backup for the target data followed by a differential file backup.    |
|    3127    |    16    |    Yes    |    The file '%.*ls' of restored database '%ls' is being left in the defunct state because the database is using the simple recovery model and the file is marked for read-write access. Therefore, only read-only files can be recovered by piecemeal restore.    |
|    3128    |    16    |    No    |    File '%ls' has an unsupported page size (%d).    |
|    3129    |    16    |    No    |    The contents of the file "%ls" are not consistent with a transition into the restore sequence. A restore from a backup set may be required.    |
|    3130    |    10    |    No    |    The filegroup "%ls" is selected. At the time of backup it was known by the name "%ls"'. RESTORE will continue operating upon the renamed filegroup.    |
|    3131    |    10    |    No    |    The file "%ls" is selected. At the time of backup it was known by the name "%ls". RESTORE will continue operating upon the renamed file.    |
|    3132    |    16    |    No    |    The media set has %d media families but only %d are provided. All members must be provided.    |
|    3133    |    16    |    No    |    The volume on device "%ls" is sequence number %d of media family %d, but sequence number %d of media family %d is expected. Check that the device specifications and loaded media are correct.    |
|    3134    |    10    |    Yes    |    The differential base attribute for file '%ls' of database '%ls' has been reset because the file has been restored from a backup taken on a conflicting recovery path. The restore was allowed because the file was read-only and was consistent with the current status of the database. Any future differential backup of this file will require a new differential base.    |
|    3135    |    16    |    No    |    The backup set in file '%ls' was created by %hs and cannot be used for this restore operation.    |
|    3136    |    16    |    No    |    This differential backup cannot be restored because the database has not been restored to the correct earlier state.    |
|    3137    |    16    |    No    |    Database cannot be reverted. Either the primary or the snapshot names are improperly specified, all other snapshots have not been dropped, or there are missing files.    |
|    3138    |    16    |    No    |    The database cannot be reverted because FILESTREAM BLOBs are present.    |
|    3139    |    16    |    No    |    Restore to snapshot is not allowed with the master database.    |
|    3140    |    16    |    No    |    Could not adjust the space allocation for file '%ls'.    |
|    3141    |    16    |    No    |    The database to be restored was named '%ls'. Reissue the statement using the WITH REPLACE option to overwrite the '%ls' database.    |
|    3142    |    16    |    No    |    File "%ls" cannot be restored over the existing "%ls". Reissue the RESTORE statement using WITH REPLACE to overwrite pre-existing files, or WITH MOVE to identify an alternate location.    |
|    3143    |    16    |    No    |    The data set on device '%ls' is not a SQL Server backup set.    |
|    3144    |    16    |    No    |    File '%.*ls' was not backed up in file %d on device '%ls'. The file cannot be restored from this backup set.    |
|    3145    |    16    |    No    |    The STOPAT option is not supported for databases that use the SIMPLE recovery model.    |
|    3147    |    16    |    No    |    Backup and restore operations are not allowed on database tempdb.    |
|    3148    |    16    |    No    |    This RESTORE statement is invalid in the current context. The 'Recover Data Only' option is only defined for secondary filegroups when the database is in an online state. When the database is in an offline state filegroups cannot be specified.    |
|    3149    |    16    |    No    |    The file or filegroup "%ls" is not in a valid state for the "Recover Data Only" option to be used. Only secondary files in the OFFLINE or RECOVERY_PENDING state can be processed.    |
|    3150    |    10    |    No    |    The master database has been successfully restored. Shutting down SQL Server.    |
|    [3151](../../relational-databases/errors-events/mssqlserver-3151-database-engine-error.md)    |    21    |    Yes    |    Failed to restore master database. Shutting down SQL Server. Check the error logs, and rebuild the master database. For more information about how to rebuild the master database, see SQL Server Books Online.    |
|    3153    |    16    |    No    |    The database is already fully recovered.    |
|    3154    |    16    |    No    |    The backup set holds a backup of a database other than the existing '%ls' database.    |
|    3155    |    16    |    No    |    The RESTORE operation cannot proceed because one or more files have been added or dropped from the database since the backup set was created.    |
|    [3156](../../relational-databases/errors-events/mssqlserver-3156-database-engine-error.md)    |    16    |    No    |    File '%ls' cannot be restored to '%ls'. Use WITH MOVE to identify a valid location for the file.    |
|    [3159](../../relational-databases/errors-events/mssqlserver-3159-database-engine-error.md)    |    16    |    No    |    The tail of the log for the database "%ls" has not been backed up. Use BACKUP LOG WITH NORECOVERY to backup the log if it contains work you do not want to lose. Use the WITH REPLACE or WITH STOPAT clause of the RESTORE statement to just overwrite the contents of the log.    |
|    3161    |    16    |    No    |    The primary file is unavailable. It must be restored or otherwise made available.    |
|    3163    |    16    |    No    |    The transaction log was damaged. All data files must be restored before RESTORE LOG can be attempted.    |
|    3165    |    16    |    No    |    Database '%ls' was restored, however an error was encountered while replication was being restored/removed. The database has been left offline. See the topic MSSQL_ENG003165 in SQL Server Books Online.    |
|    3166    |    16    |    No    |    RESTORE DATABASE could not drop database '%ls'. Drop the database and then reissue the RESTORE DATABASE statement.    |
|    3167    |    16    |    No    |    RESTORE could not start database '%ls'.    |
|    [3168](../../relational-databases/errors-events/mssqlserver-3168-database-engine-error.md)    |    16    |    No    |    The backup of the system database on the device %ls cannot be restored because it was created by a different version of the server (%ls) than this server (%ls).    |
|    [3169](../../relational-databases/errors-events/mssqlserver-3169-database-engine-error.md)    |    16    |    No    |    The database was backed up on a server running version %ls. That version is incompatible with this server, which is running version %ls. Either restore the database on a server that supports the backup, or use a backup that is compatible with this server.    |
|    3170    |    16    |    No    |    The STANDBY filename is invalid.    |
|    3171    |    16    |    No    |    File %ls is defunct and cannot be restored into the online database.    |
|    3172    |    16    |    No    |    Filegroup %ls is defunct and cannot be restored into the online database.    |
|    3173    |    16    |    No    |    The STOPAT clause provided with this RESTORE statement indicates that the tail of the log contains changes that must be backed up to reach the target point in time. The tail of the log for the database "%ls" has not been backed up. Use BACKUP LOG WITH NORECOVERY to back up the log, or use the WITH REPLACE clause in your RESTORE statement to overwrite the tail of the log.    |
|    3174    |    16    |    No    |    The file '%ls' cannot be moved by this RESTORE operation.    |
|    3175    |    10    |    No    |    RESTORE FILEGROUP="%ls" was specified, but not all of its files are present in the backup set. File "%ls" is missing. RESTORE will continue, but if you want all files to be restored, you must restore other backup sets.    |
|    [3176](../../relational-databases/errors-events/mssqlserver-3176-database-engine-error.md)    |    16    |    No    |    File '%ls' is claimed by '%ls'(%d) and '%ls'(%d). The WITH MOVE clause can be used to relocate one or more files.    |
|    3178    |    16    |    No    |    File %ls is not in the correct state to have this differential backup applied to it.    |
|    3179    |    16    |    No    |    The system database cannot be moved by RESTORE.    |
|    3180    |    16    |    No    |    This backup cannot be restored using WITH STANDBY because a database upgrade is needed. Reissue the RESTORE without WITH STANDBY.    |
|    [3181](../../relational-databases/errors-events/mssqlserver-3181-database-engine-error.md)    |    10    |    No    |    Attempting to restore this backup may encounter storage space problems. Subsequent messages will provide details.    |
|    3182    |    16    |    No    |    The backup set cannot be restored because the database was damaged when the backup occurred. Salvage attempts may exploit WITH CONTINUE_AFTER_ERROR.    |
|    [3183](../../relational-databases/errors-events/mssqlserver-3183-database-engine-error.md)    |    16    |    No    |    RESTORE detected an error on page (%d:%d) in database "%ls" as read from the backup set.    |
|    3184    |    10    |    No    |    RESTORE WITH CONTINUE_AFTER_ERROR was successful but some damage was encountered. Inconsistencies in the database are possible.    |
|    3185    |    16    |    No    |    RESTORE cannot apply this backup set because the database is suspect. Restore a backup set that repairs the damage.    |
|    3186    |    16    |    No    |    The backup set has been damaged. RESTORE will not attempt to apply this backup set.    |
|    3187    |    16    |    No    |    RESTORE WITH CHECKSUM cannot be specified because the backup set does not contain checksum information.    |
|    3188    |    10    |    No    |    The backup set was written with damaged data by a BACKUP WITH CONTINUE_AFTER_ERROR.    |
|    3189    |    16    |    No    |    Damage to the backup set was detected.    |
|    3190    |    16    |    No    |    Filegroup '%ls' cannot be restored because it does not exist in the backup set.    |
|    3191    |    16    |    No    |    Restore cannot continue because file '%ls' cannot be written. Ensure that all files in the database are writable.    |
|    3192    |    10    |    No    |    Restore was successful but deferred transactions remain. These transactions cannot be resolved because there are data that is unavailable. Either use RESTORE to make that data available or drop the filegroups if you never need this data again. Dropping the filegroup results in a defunct filegroup.    |
|    3194    |    16    |    No    |    Page %S_PGID is beyond the end of the file. Only pages that are in the current range of the file can be restored.    |
|    3195    |    16    |    No    |    Page %S_PGID cannot be restored from this backup set. RESTORE PAGE can only be used from full backup sets or from the first log or differential backup taken since the file was added to the database.    |
|    3196    |    16    |    No    |    RESTORE master WITH SNAPSHOT is not supported. To restore master from a snapshot backup, stop the service and copy the data and log file.    |
|    3197    |    10    |    Yes    |    I/O is frozen on database %ls. No user action is required. However, if I/O is not resumed promptly, you could cancel the backup.    |
|    3198    |    10    |    Yes    |    I/O was resumed on database %ls. No user action is required.    |
|    3199    |    16    |    No    |    RESTORE requires MAXTRANSFERSIZE=%u but %u was specified.    |
|    [3201](../../relational-databases/errors-events/mssqlserver-3201-database-engine-error.md)    |    16    |    No    |    Cannot open backup device '%ls'. Operating system error %ls.    |
|    3202    |    16    |    No    |    Write on "%ls" failed: %ls    |
|    3203    |    16    |    No    |    Read on "%ls" failed: %ls    |
|    3204    |    16    |    No    |    The backup or restore was aborted.    |
|    3205    |    16    |    No    |    Too many backup devices specified for backup or restore; only %d are allowed.    |
|    3206    |    16    |    No    |    Backup device '%.*ls' does not exist. To view existing backup devices, use the sys.backup_devices catalog view. To create a new backup device use either sp_addumpdevice or SQL Server Management Studio.    |
|    3207    |    16    |    No    |    Backup or restore requires at least one backup device. Rerun your statement specifying a backup device.    |
|    3208    |    16    |    No    |    Unexpected end of file while reading beginning of backup set. Confirm that the media contains a valid SQL Server backup set, and see the console error log for more details.    |
|    3209    |    16    |    No    |    Operation is not supported on user instances.    |
|    3210    |    16    |    No    |    The mirror member in drive "%ls" is inconsistent with the mirror member in drive "%ls".    |
|    3211    |    10    |    No    |    %d percent processed.    |
|    3212    |    16    |    No    |    The mirror device "%ls" and the mirror device "%ls" have different device specifications.    |
|    3213    |    16    |    No    |    Unable to unload one or more tapes. See the error log for details.    |
|    3214    |    16    |    No    |    Too many backup mirrors are specified. Only %d are allowed.    |
|    3215    |    16    |    No    |    Use WITH FORMAT to create a new mirrored backup set.    |
|    3216    |    16    |    No    |    RESTORE REWINDONLY is only applicable to tape devices.    |
|    3217    |    16    |    No    |    Invalid value specified for %ls parameter.    |
|    3218    |    16    |    No    |    Backup mirroring is not available in this edition of SQL Server. See Books Online for more details on feature support in different SQL Server editions.    |
|    3219    |    16    |    No    |    The file or filegroup "%.*ls" cannot be selected for this operation.    |
|    3221    |    16    |    No    |    The ReadFileEx system function executed on file '%ls' only read %d bytes, expected %d.    |
|    3222    |    16    |    No    |    The WriteFileEx system function executed on file '%ls' only wrote %d bytes, expected %d.    |
|    3224    |    16    |    No    |    Cannot create worker thread.    |
|    3227    |    16    |    No    |    The backup media on "%ls" is part of media family %d which has already been processed on "%ls". Ensure that backup devices are correctly specified. For tape devices, ensure that the correct volumes are loaded.    |
|    3229    |    16    |    No    |    Request for device '%ls' timed out.    |
|    3230    |    16    |    No    |    Operation on device '%ls' exceeded retry count.    |
|    3231    |    16    |    No    |    The media loaded on "%ls" is formatted to support %d media families, but %d media families are expected according to the backup device specification.    |
|    3232    |    16    |    No    |    The volume mounted on "%ls" does not have the expected backup set identity. The volume may be obsolete due to a more recent overwrite of this media family. In that case, locate the correct volume with sequence number %d of media family %d.    |
|    3234    |    16    |    No    |    Logical file '%.*ls' is not part of database '%ls'. Use RESTORE FILELISTONLY to list the logical file names.    |
|    3235    |    16    |    No    |    The file "%.*ls" is not part of database "%ls". You can only list files that are members of this database.    |
|    3239    |    16    |    No    |    The backup set on device '%ls' uses a feature of the Microsoft Tape Format not supported by SQL Server.    |
|    3240    |    16    |    No    |    Backup to mirrored media sets requires all mirrors to append. Provide all members of the set, or reformat a new media set.    |
|    3241    |    16    |    No    |    The media family on device '%ls' is incorrectly formed. SQL Server cannot process this media family.    |
|    3242    |    16    |    No    |    The file on device '%ls' is not a valid Microsoft Tape Format backup set.    |
|    3243    |    16    |    No    |    The media family on device '%ls' was created using Microsoft Tape Format version %d.%d. SQL Server supports version %d.%d.    |
|    3244    |    16    |    No    |    Descriptor block size exceeds %d bytes. Use a shorter name and/or description string and retry the operation.    |
|    3245    |    16    |    No    |    Could not convert a string to or from Unicode, %ls.    |
|    3246    |    16    |    No    |    The media family on device '%ls' is marked as nonappendable. Reissue the statement using the INIT option to overwrite the media.    |
|    3247    |    16    |    No    |    The volume on device '%ls' has the wrong media sequence number (%d). Remove it and insert volume %d.    |
|    3249    |    16    |    No    |    The volume on device '%ls' is a continuation volume for the backup set. Remove it and insert the volume holding the start of the backup set.    |
|    3250    |    16    |    No    |    The value '%d' is not within range for the %ls parameter.    |
|    3251    |    10    |    No    |    The media family on device '%ls' is complete. The device is now being reused for one of the remaining families.    |
|    3253    |    16    |    No    |    The block size parameter must supply a value that is a power of 2.    |
|    3254    |    16    |    No    |    The volume on device '%ls' is empty.    |
|    3255    |    16    |    No    |    The data set on device '%ls' is a SQL Server backup set not compatible with this version of SQL Server.    |
|    3256    |    16    |    No    |    The backup set on device '%ls' was terminated while it was being created and is incomplete. RESTORE sequence is terminated abnormally.    |
|    3257    |    16    |    No    |    There is insufficient free space on disk volume '%ls' to create the database. The database requires %I64u additional free bytes, while only %I64u bytes are available.    |
|    3258    |    16    |    No    |    The volume on the device "%ls" is not part of the media set that is currently being processed. Ensure that the backup devices are loaded with the correct media.    |
|    [3260](../../relational-databases/errors-events/mssqlserver-3260-database-engine-error.md)    |    16    |    No    |    An internal buffer has become full.    |
|    3261    |    16    |    No    |    SQL Server cannot use the virtual device configuration.    |
|    3262    |    10    |    No    |    The backup set on file %d is valid.    |
|    3263    |    16    |    No    |    Cannot use the volume on device '%ls' as a continuation volume. It is sequence number %d of family %d for the current media set. Insert a new volume, or sequence number %d of family %d for the current set.    |
|    3264    |    16    |    No    |    The operation did not proceed far enough to allow RESTART. Reissue the statement without the RESTART qualifier.    |
|    3265    |    16    |    No    |    The login has insufficient authority. Membership of the sysadmin role is required to use VIRTUAL_DEVICE with BACKUP or RESTORE.    |
|    3266    |    16    |    Yes    |    The backup data at the end of "%ls" is incorrectly formatted. Backup sets on the media might be damaged and unusable. To determine the backup sets on the media, use RESTORE HEADERONLY. To determine the usability of the backup sets, run RESTORE VERIFYONLY. If all of the backup sets are incomplete, reformat the media using BACKUP WITH FORMAT, which destroys all the backup sets.    |
|    3267    |    16    |    No    |    Insufficient resources to create UMS scheduler.    |
|    3268    |    16    |    No    |    Cannot use the backup file '%ls' because it was originally formatted with sector size %d and is now on a device with sector size %d.    |
|    3269    |    16    |    No    |    Cannot restore the file '%ls' because it was originally written with sector size %d; '%ls' is now on a device with sector size %d.    |
|    3270    |    16    |    No    |    An internal consistency error has occurred. This error is similar to an assert. Contact technical support for assistance.    |
|    [3271](../../relational-databases/errors-events/mssqlserver-3271-database-engine-error.md)    |    16    |    No    |    A nonrecoverable I/O error occurred on file "%ls:" %ls.    |
|    3272    |    16    |    No    |    The '%ls' device has a hardware sector size of %d, but the block size parameter specifies an incompatible override value of %d. Reissue the statement using a compatible block size.    |
|    3276    |    16    |    No    |    WITH SNAPSHOT can be used only if the backup set was created WITH SNAPSHOT.    |
|    3277    |    16    |    No    |    WITH SNAPSHOT must be used with only one virtual device.    |
|    3278    |    16    |    No    |    Failed to encrypt string %ls    |
|    3279    |    16    |    No    |    Access is denied due to a password failure    |
|    3280    |    16    |    No    |    Backups on raw devices are not supported. '%ls' is a raw device.    |
|    3281    |    10    |    No    |    Released and initiated rewind on '%ls'.    |
|    3283    |    16    |    No    |    The file "%ls" failed to initialize correctly. Examine the error logs for more details.    |
|    3284    |    16    |    No    |    Filemark on device '%ls' is not aligned. Re-issue the restore statement with the same blocksize used to create the backupset: '%d' looks like a possible value.    |
|    3285    |    10    |    Yes    |    Filemark on device '%ls' appears not to be aligned. The restore operation will proceed using less efficient I/O.  To avoid this, re-issue the Restore statement with the same blocksize used to create the backupset: '%d' looks like a possible value.    |
|    3301    |    21    |    Yes    |    The transaction log contains a record (logop %d) that is not valid. The log has been corrupted. Restore the database from a full backup, or repair the database.    |
|    3302    |    21    |    Yes    |    Redoing of logged operations in database '%.*ls' failed to reach end of log at log record ID %S_LSN. This indicates corruption around log record ID %S_LSN. Restore the database from a full backup, or repair the database.    |
|    [3313](../../relational-databases/errors-events/mssqlserver-3313-database-engine-error.md)    |    21    |    Yes    |    During redoing of a logged operation in database '%.*ls', an error occurred at log record ID %S_LSN. Typically, the specific failure is previously logged as an error in the Windows Event Log service. Restore the database from a full backup, or repair the database.    |
|    [3314](../../relational-databases/errors-events/mssqlserver-3314-database-engine-error.md)    |    21    |    Yes    |    During undoing of a logged operation in database '%.*ls', an error occurred at log record ID %S_LSN. Typically, the specific failure is logged previously as an error in the Windows Event Log service. Restore the database or file from a backup, or repair the database.    |
|    3315    |    21    |    Yes    |    During rollback, the following process did not hold an expected lock: process %d with mode %d at level %d for row %S_RID in database '%.*ls' under transaction %S_XID. Restore a backup of the database, or repair the database.    |
|    3316    |    21    |    Yes    |    During undo of a logged operation in database '%.*ls', an error occurred at log record ID %S_LSN. The row was not found. Restore the database from a full backup, or repair the database.    |
|    3401    |    10    |    Yes    |    Errors occurred during recovery while rolling back a transaction. The transaction was deferred. Restore the bad page or file, and re-run recovery.    |
|    3402    |    10    |    Yes    |    The database '%ls' is marked %ls and is in a state that does not allow recovery to be run.    |
|    3403    |    10    |    Yes    |    Recovering only master database because traceflag 3608 was specified. This is an informational message only. No user action is required.    |
|    3404    |    10    |    Yes    |    Failed to check for new installation or a renamed server at startup. The logic for this check has failed unexpectedly. Run setup again, or fix the problematic registry key.    |
|    3406    |    10    |    Yes    |    %d transactions rolled forward in database '%.*ls' (%d). This is an informational message only. No user action is required.    |
|    3407    |    10    |    Yes    |    %d transactions rolled back in database '%.*ls' (%d). This is an informational message only. No user action is required.    |
|    3408    |    10    |    Yes    |    Recovery is complete. This is an informational message only. No user action is required.    |
|    3409    |    16    |    Yes    |    Performance counter shared memory setup failed with error %d. Reinstall sqlctr.ini for this instance, and ensure that the instance login account has correct registry permissions.    |
|    3410    |    10    |    Yes    |    Data in filegroup %s is offline, and deferred transactions exist. Use RESTORE to recover the filegroup, or drop the filegroup if you never intend to recover it. Log truncation cannot occur until this condition is resolved.    |
|    3411    |    21    |    Yes    |    Configuration block version %d is not a valid version number. SQL Server is exiting. Restore the master database or reinstall.    |
|    3412    |    10    |    Yes    |    Warning: The server instance was started using minimal configuration startup option (-f). Starting an instance of SQL Server with minimal configuration places the server in single-user mode automatically. After the server has been started with minimal configuration, you should change the appropriate server option value or values, stop, and then restart the server.    |
|    [3413](../../relational-databases/errors-events/mssqlserver-3413-database-engine-error.md)    |    21    |    Yes    |    Database ID %d. Could not mark database as suspect. Getnext NC scan on sys.databases.database_id failed. Refer to previous errors in the error log to identify the cause and correct any associated problems.    |
|    [3414](../../relational-databases/errors-events/mssqlserver-3414-database-engine-error.md)    |    10    |    Yes    |    An error occurred during recovery, preventing the database '%.*ls' (database ID %d) from restarting. Diagnose the recovery errors and fix them, or restore from a known good backup. If errors are not corrected or expected, contact Technical Support.    |
|    3415    |    16    |    Yes    |    Database '%.*ls' cannot be upgraded because it is read-only or has read-only files. Make the database or files writeable, and rerun recovery.    |
|    3416    |    16    |    Yes    |    The server contains read-only files that must be made writable before the server can be recollated.    |
|    [3417](../../relational-databases/errors-events/mssqlserver-3417-database-engine-error.md)    |    21    |    Yes    |    Cannot recover the master database. SQL Server is unable to run. Restore master from a full backup, repair it, or rebuild it. For more information about how to rebuild the master database, see SQL Server Books Online.    |
|    3418    |    10    |    Yes    |    Recovery is unable to defer error %d. Errors can only be deferred in databases using the full recovery model and an active backup log chain.    |
|    3419    |    16    |    Yes    |    Recovery for database '%.*ls' is being skipped because it requires an upgrade but is marked for Standby. Use RESTORE DATABASE WITH NORECOVERY to take the database back to a Restoring state and continue the restore sequence.    |
|    3420    |    21    |    Yes    |    Database snapshot '%ls' has failed an IO operation and is marked suspect. It must be dropped and recreated.    |
|    3421    |    10    |    Yes    |    Recovery completed for database %ls (database ID %d) in %I64d second(s) (analysis %I64d ms, redo %I64d ms, undo %I64d ms.) This is an informational message only. No user action is required.    |
|    3422    |    10    |    Yes    |    Database %ls was shutdown due to error %d in routine '%hs'. Restart for non-snapshot databases will be attempted after all connections to the database are aborted.    |
|    3429    |    10    |    Yes    |    Recovery could not determine the outcome of a cross-database transaction %S_XID, named '%.*ls', in database '%.*ls' (database ID %d). The coordinating database (database ID %d) was unavailable. The transaction was assumed to be committed. If the transaction was not committed, you can retry recovery when the coordinating database is available.    |
|    [3431](../../relational-databases/errors-events/mssqlserver-3431-database-engine-error.md)    |    21    |    Yes    |    Could not recover database '%.*ls' (database ID %d) because of unresolved transaction outcomes. Microsoft Distributed Transaction Coordinator (MS DTC) transactions were prepared, but MS DTC was unable to determine the resolution. To resolve, either fix MS DTC, restore from a full backup, or repair the database.    |
|    3434    |    20    |    Yes    |    Cannot change sort order or locale. An unexpected failure occurred while trying to reindex the server to a new collation. SQL Server is shutting down. Restart SQL Server to continue with the sort order unchanged. Diagnose and correct previous errors and then retry the operation.    |
|    [3437](../../relational-databases/errors-events/mssqlserver-3437-database-engine-error.md)    |    21    |    Yes    |    An error occurred while recovering database '%.*ls'. Unable to connect to Microsoft Distributed Transaction Coordinator (MS DTC) to check the completion status of transaction %S_XID. Fix MS DTC, and run recovery again.    |
|    3441    |    21    |    Yes    |    During startup of warm standby database '%.*ls' (database ID %d), its standby file ('%ls') was inaccessible to the RESTORE statement. The operating system error was '%ls'. Diagnose the operating system error, correct the problem, and retry startup.    |
|    3442    |    21    |    Yes    |    Recovery of warm standby database '%.*ls' (database ID %d) failed. There is insufficient room in the undo file. Increase the size of the undo file and retry recovery.    |
|    3443    |    21    |    Yes    |    Database '%.*ls' (database ID %d) was marked for standby or read-only use, but has been modified. The RESTORE LOG statement cannot be performed. Restore the database from a backup.    |
|    3445    |    21    |    Yes    |    File '%ls' is not a valid undo file for database '%.*ls (database ID %d). Verify the file path, and specify the correct file.    |
|    3446    |    16    |    No    |    Primary log file is not available for database '%.*ls'. The log cannot be backed up.    |
|    3447    |    16    |    No    |    Could not activate or scan all of the log files for database '%.*ls'.    |
|    3448    |    21    |    Yes    |    Rollback encountered a page with a log sequence number (LSN) less than the original log record LSN. Could not undo log record %S_LSN, for transaction ID %S_XID, on page %S_PGID, database '%.*ls' (database ID %d). Page information: LSN = %S_LSN, type = %ld. Log information: OpCode = %ld, context %ld. Restore or repair the database.    |
|    3449    |    21    |    Yes    |    SQL Server must shut down in order to recover a database (database ID %d). The database is either a user database that could not be shut down or a system database. Restart SQL Server. If the database fails to recover after another startup, repair or restore the database.    |
|    3450    |    10    |    Yes    |    Recovery of database '%.*ls' (%d) is %d%% complete (approximately %d seconds remain). Phase %d of 3. This is an informational message only. No user action is required.    |
|    [3452](../../relational-databases/errors-events/mssqlserver-3452-database-engine-error.md)    |    10    |    Yes    |    Recovery of database '%.*ls' (%d) detected possible identity value inconsistency in table ID %d. Run DBCC CHECKIDENT ('%.*ls').    |
|    3453    |    16    |    No    |    This version cannot redo any index creation or non-logged operation done by SQL Server 7.0. Further roll forward is not possible.    |
|    3454    |    10    |    Yes    |    Recovery is writing a checkpoint in database '%.*ls' (%d). This is an informational message only. No user action is required.    |
|    [3456](../../relational-databases/errors-events/mssqlserver-3456-database-engine-error.md)    |    21    |    Yes    |    Could not redo log record %S_LSN, for transaction ID %S_XID, on page %S_PGID, database '%.*ls' (database ID %d). Page: LSN = %S_LSN, type = %ld. Log: OpCode = %ld, context %ld, PrevPageLSN: %S_LSN. Restore from a backup of the database, or repair the database.    |
|    3457    |    21    |    Yes    |    Transactional file system resource manager '%.*ls' failed to recover. For more information, see the accompanying error message, which determines the appropriate user action.    |
|    3458    |    16    |    No    |    Recovery cannot scan database "%.*ls" for dropped allocation units because an unexpected error has occurred. These allocation units cannot be cleaned up.    |
|    3505    |    14    |    No    |    Only the owner of database "%.*ls" or someone with relevant permissions can run the CHECKPOINT statement.    |
|    3604    |    10    |    No    |    Duplicate key was ignored.    |
|    3606    |    10    |    No    |    Arithmetic overflow occurred.    |
|    3607    |    10    |    No    |    Division by zero occurred.    |
|    3608    |    16    |    No    |    Cannot allocate a GUID for the token.    |
|    3609    |    16    |    No    |    The transaction ended in the trigger. The batch has been aborted.    |
|    3612    |    10    |    No    |    %hs SQL Server Execution Times:%hs CPU time = %lu ms, elapsed time = %lu ms.    |
|    3613    |    10    |    No    |    SQL Server parse and compile time: %hs CPU time = %lu ms, elapsed time = %lu ms.    |
|    3615    |    10    |    No    |    Table '%.*ls'. Scan count %d, logical reads %d, physical reads %d, read-ahead reads %d, lob logical reads %d, lob physical reads %d, lob read-ahead reads %d.    |
|    3616    |    16    |    No    |    An error was raised during trigger execution. The batch has been aborted and the user transaction, if any, has been rolled back.    |
|    [3619](../../relational-databases/errors-events/mssqlserver-3619-database-engine-error.md)    |    10    |    Yes    |    Could not write a checkpoint record in database ID %d because the log is out of space. Contact the database administrator to truncate the log or allocate more space to the database log files.    |
|    3620    |    10    |    Yes    |    Automatic checkpointing is disabled in database '%.*ls' because the log is out of space. Automatic checkpointing will be enabled when the database owner successfully checkpoints the database. Contact the database owner to either truncate the log file or add more disk space to the log. Then retry the CHECKPOINT statement.    |
|    3621    |    10    |    No    |    The statement has been terminated.    |
|    3622    |    10    |    No    |    Warning: An invalid floating point operation occurred.    |
|    3623    |    16    |    No    |    An invalid floating point operation occurred.    |
|    3624    |    20    |    Yes    |    A system assertion check has failed. Check the SQL Server error log for details. Typically, an assertion failure is caused by a software bug or data corruption. To check for database corruption, consider running DBCC CHECKDB. If you agreed to send dumps to Microsoft during setup, a mini dump will be sent to Microsoft. An update might be available from Microsoft in the latest Service Pack or in a QFE from Technical Support.    |
|    3625    |    20    |    Yes    |    '%hs' is not yet implemented.    |
|    3627    |    17    |    Yes    |    New parallel operation cannot be started due to too many parallel operations executing at this time. Use the "max worker threads" configuration option to increase the number of allowable threads, or reduce the number of parallel operations running on the system.    |
|    3628    |    24    |    Yes    |    The Database Engine received a floating point exception from the operating system while processing a user request. Try the transaction again. If the problem persists, contact your system administrator.    |
|    3633    |    16    |    Yes    |    The operating system returned the error '%ls' while attempting '%ls' on '%ls' at '%hs'(%d).    |
|    3634    |    16    |    Yes    |    The operating system returned the error '%ls' while attempting '%ls' on '%ls'.    |
|    3635    |    16    |    Yes    |    An error occurred while processing '%ls' metadata for database id %d, file id %d, and transaction='%.*ls'. Additional Context='%ls'. Location='%hs'(%d). Retry the operation; if the problem persists, contact the database administrator to review locking and memory configurations. Review the application for possible deadlock conflicts.    |
|    3636    |    16    |    No    |    An error occurred while processing '%ls' metadata for database id %d file id %d.    |
|    3637    |    16    |    No    |    A parallel operation cannot be started from a DAC connection.    |
|    3638    |    10    |    No    |    SQL text cache memory usage: %d pages. This is an informational message only; no user action is required.    |
|    3701    |    11    |    No    |    Cannot %S_MSG the %S_MSG '%.*ls', because it does not exist or you do not have permission.    |
|    3702    |    16    |    No    |    Cannot drop database "%.*ls" because it is currently in use.    |
|    3703    |    16    |    No    |    Cannot detach the %S_MSG '%.*ls' because it is currently in use.    |
|    3705    |    16    |    No    |    Cannot use DROP %ls with '%.*ls' because '%.*ls' is a %S_MSG. Use %ls.    |
|    3706    |    16    |    No    |    Cannot %S_MSG a database snapshot.    |
|    3707    |    16    |    No    |    Cannot detach a suspect or recovery pending database. It must be repaired or dropped.    |
|    3708    |    16    |    No    |    Cannot %S_MSG the %S_MSG '%.*ls' because it is a system %S_MSG.    |
|    3709    |    16    |    No    |    Cannot %S_MSG the database while the database snapshot "%.*ls" refers to it. Drop that database first.    |
|    3710    |    16    |    No    |    Cannot detach an opened database when the server is in minimally configured mode.    |
|    3716    |    16    |    No    |    The %S_MSG '%.*ls' cannot be dropped because it is bound to one or more %S_MSG.    |
|    3717    |    16    |    No    |    Cannot drop a default constraint by DROP DEFAULT statement. Use ALTER TABLE to drop a constraint default.    |
|    3721    |    16    |    No    |    Type '%.*ls' cannot be renamed because it is being referenced by object '%.*ls'.    |
|    3723    |    16    |    No    |    An explicit DROP INDEX is not allowed on index '%.*ls'. It is being used for %ls constraint enforcement.    |
|    3724    |    16    |    No    |    Cannot %S_MSG the %S_MSG '%.*ls' because it is being used for replication.    |
|    3725    |    16    |    No    |    The constraint '%.*ls' is being referenced by table '%.*ls', foreign key constraint '%.*ls'.    |
|    3726    |    16    |    No    |    Could not drop object '%.*ls' because it is referenced by a FOREIGN KEY constraint.    |
|    3727    |    10    |    No    |    Could not drop constraint. See previous errors.    |
|    3728    |    16    |    No    |    '%.*ls' is not a constraint.    |
|    3729    |    16    |    No    |    Cannot %ls '%.*ls' because it is being referenced by object '%.*ls'.    |
|    3730    |    16    |    No    |    Cannot drop the default constraint '%.*ls' while it is being used by a foreign key as SET DEFAULT referential action.    |
|    3732    |    16    |    No    |    Cannot drop type '%.*ls' because it is being referenced by object '%.*ls'. There may be other objects that reference this type.    |
|    3733    |    16    |    No    |    Constraint '%.*ls' does not belong to table '%.*ls'.    |
|    3734    |    16    |    No    |    Could not drop the primary key constraint '%.*ls' because the table has an XML or spatial index.    |
|    3735    |    16    |    No    |    The primary key constraint '%.*ls' on table '%.*ls' cannot be dropped because change tracking is enabled on the table. Change tracking requires a primary key constraint on the table. Disable change tracking before dropping the constraint.    |
|    3737    |    16    |    No    |    Could not delete file '%ls'. See the SQL Server error log for more information.    |
|    3738    |    10    |    No    |    Deleting database file '%ls'.    |
|    3739    |    11    |    No    |    Cannot %ls the index '%.*ls' because it is not a statistics collection.    |
|    3740    |    16    |    No    |    Cannot drop the %S_MSG '%.*ls' because at least part of the table resides on a read-only filegroup.    |
|    3741    |    16    |    No    |    Cannot drop the %S_MSG '%.*ls' because at least part of the table resides on an offline filegroup.    |
|    3743    |    16    |    No    |    The database '%.*ls' is enabled for database mirroring. Database mirroring must be removed before you drop the database.    |
|    3744    |    16    |    No    |    Only a single clause is allowed in a statement where an index is dropped online.    |
|    3745    |    16    |    No    |    Only a clustered index can be dropped online.    |
|    3746    |    16    |    No    |    Cannot drop the clustered index of view '%.*ls' because the view is being used for replication.    |
|    3747    |    16    |    No    |    Cannot drop a clustered index created on a view using drop clustered index clause. Clustered index '%.*ls' is created on view '%.*ls'.    |
|    3748    |    16    |    No    |    Cannot drop nonclustered index '%.*ls' using drop clustered index clause.    |
|    3749    |    16    |    No    |    Cannot drop XML Index '%.*ls' using old 'Table.Index' syntax, use 'Index ON Table' syntax instead.    |
|    3750    |    10    |    No    |    Warning: Index '%.*ls' on %S_MSG '%.*ls' was disabled as a result of disabling the clustered index on the %S_MSG.    |
|    3751    |    16    |    No    |    Cannot use SP_DROPEXTENDEDPROC or DBCC DROPEXTENDEDPROC with '%.*ls' because '%.*ls' is a %S_MSG. Use %ls.    |
|    3801    |    10    |    No    |    Warning: The index "%.*ls" on "%.*ls"."%.*ls" may be impacted by the collation upgrade. Run DBCC CHECKTABLE.    |
|    3802    |    10    |    No    |    Warning: The constraint "%.*ls" on "%.*ls"."%.*ls" may be impacted by the collation upgrade. Disable and enable WITH CHECK.    |
|    3803    |    10    |    No    |    Warning: The index "%.*ls" on "%.*ls"."%.*ls" is disabled because the implementation of the checksum function has changed.    |
|    3804    |    10    |    No    |    Warning: The check constraint "%.*ls" on table "%.*ls"."%.*ls" is disabled because the implementation of the checksum function has changed.    |
|    3805    |    10    |    No    |    Warning: Index "%.*ls" on table "%.*ls"."%.*ls" might be corrupted because it references computed column "%.*ls" containing a non-deterministic conversion from string to date. Run DBCC CHECKTABLE to verify index. Consider using explicit CONVERT with deterministic date style such as 121. Computed column indexes referencing non-deterministic expressions can't be created in 90 compatibility mode. See Books Online topic "Creating Indexes on Computed Columns" for more information.    |
|    3806    |    10    |    No    |    Warning: Indexed view "%.*ls"."%.*ls" might be corrupted because it contains a non-deterministic conversion from string to date. Run DBCC CHECKTABLE to verify view. Consider using explicit CONVERT with deterministic date style such as 121. Indexed views referencing non-deterministic expressions can't be created in 90 compatibility mode. See Books Online topic "Creating Indexed Views" for more information.    |
|    3807    |    17    |    No    |    Create failed because all available identifiers have been exhausted.    |
|    3808    |    10    |    No    |    Warning: The index "%.*ls" on "%.*ls"."%.*ls" is disabled because the index is defined on a view with ignore_dup_key index option. Drop the index and, if possible, recreate it without ignore_dup_key option. You may need to change the logical structure of the view to ensure all rows are unique.    |
|    3809    |    16    |    No    |    Upgrade of database "%.*ls" failed because index "%.*ls" on object ID %d has the same name as that of another index on the same table.    |
|    3810    |    10    |    No    |    Event notification "%.*ls" on assembly is dropped.    |
|    3811    |    10    |    No    |    Event notification "%.*ls" on service queue is dropped as broker instance is not specified.    |
|    3812    |    10    |    No    |    Event notification "%.*ls" on object is dropped.    |
|    3813    |    16    |    No    |    Upgrade of login '%.*ls' failed because its name or sid is a duplicate of another login or server role.    |
|    3814    |    16    |    No    |    Local login mapped to remote login '%.*ls' on server '%.*ls' is invalid. Drop and recreate the remote login before upgrade.    |
|    3815    |    16    |    No    |    Local login mapped to linked login '%.*ls' on server '%.*ls' is invalid. Drop and recreate the linked login before upgrade.    |
|    3816    |    16    |    No    |    Upgrade of login '%.*ls' failed because its password hash is invalid. Update the login password before upgrade.    |
|    3817    |    10    |    No    |    Warning: The index "%.*ls" on "%.*ls"."%.*ls" was disabled because the implementation of geometry and geography methods have changed.    |
|    3818    |    16    |    No    |    CUID column of 6 bytes cannot be added to index "%.*ls" on object %.*ls because row length would exceed the maximum permissible length of %d bytes.    |
|    3819    |    10    |    No    |    Warning: The check constraint "%.*ls" on "%.*ls"."%.*ls" was disabled and set as not trusted because the implementation of geometry and geography methods have changed.    |
|    3820    |    10    |    No    |    Warning: CUID column of 6 bytes has been added to index "%.*ls" on object %.*ls, but its maximum row size exceeds the allowed maximum of %d bytes. INSERT or UPDATE to this index will fail for some combination of large values.    |
|    3821    |    10    |    No    |    Warning: The foreign key constraint "%.*ls" on "%.*ls"."%.*ls" was disabled because the implementation of geometry and geography methods have changed.    |
|    3822    |    10    |    No    |    Warning: The heap "%.*ls"."%.*ls" has persisted computed columns that depends on a geometry or geography methods and may contain out-of-date information. Because of this, DBCC may report inconsistencies on this table. The persisted computed columns depending on geometry or geography methods should be unpersisted and persisted again to refresh the data.    |
|    3823    |    10    |    No    |    Warning: The object "%.*ls"."%.*ls" could not be bound and was ignored during upgrade. Consider reviewing and correcting its definition.    |
|    3851    |    10    |    No    |    An invalid row (%ls) was found in the system table sys.%ls%ls.    |
|    3852    |    10    |    No    |    Row (%ls) in sys.%ls%ls does not have a matching row (%ls) in sys.%ls%ls.    |
|    3853    |    10    |    No    |    Attribute (%ls) of row (%ls) in sys.%ls%ls does not have a matching row (%ls) in sys.%ls%ls.    |
|    3854    |    10    |    No    |    Attribute (%ls) of row (%ls) in sys.%ls%ls has a matching row (%ls) in sys.%ls%ls that is invalid.    |
|    3855    |    10    |    No    |    Attribute (%ls) exists without a row (%ls) in sys.%ls%ls.    |
|    3856    |    10    |    No    |    Attribute (%ls) exists but should not for row (%ls) in sys.%ls%ls.    |
|    3857    |    10    |    No    |    The attribute (%ls) is required but is missing for row (%ls) in sys.%ls%ls.    |
|    3858    |    10    |    No    |    The attribute (%ls) of row (%ls) in sys.%ls%ls has an invalid value.    |
|    [3859](../../relational-databases/errors-events/mssqlserver-3859-database-engine-error.md)    |    10    |    No    |    Warning: The system catalog was updated directly in database ID %d, most recently at %S_DATE.    |
|    3860    |    10    |    No    |    Cannot upgrade database ID 32767. This ID value is reserved for SQL Server internal use.    |
|    3862    |    10    |    No    |    CLR type '%.*ls'.'%.*ls' is disabled because the on disk format for this CLR type has been changed. Use DROP TYPE to remove this disabled type.    |
|    3864    |    23    |    Yes    |    Could not find an entry for index with ID %d on object with ID %d in database with ID %d. Possible schema corruption. Run DBCC CHECKDB.    |
|    3901    |    16    |    No    |    The transaction name must be specified when it is used with the mark option.    |
|    3902    |    16    |    No    |    The COMMIT TRANSACTION request has no corresponding BEGIN TRANSACTION.    |
|    3903    |    16    |    No    |    The ROLLBACK TRANSACTION request has no corresponding BEGIN TRANSACTION.    |
|    3904    |    21    |    No    |    Cannot unsplit logical page %S_PGID in object '%.*ls', in database '%.*ls'. Both pages together contain more data than will fit on one page.    |
|    3906    |    16    |    No    |    Failed to update database "%.*ls" because the database is read-only.    |
|    3908    |    16    |    No    |    Could not run BEGIN TRANSACTION in database '%.*ls' because the database is in bypass recovery mode.    |
|    3909    |    16    |    No    |    Session binding token is invalid.    |
|    3910    |    16    |    No    |    Transaction context in use by another session.    |
|    3912    |    16    |    No    |    Cannot bind using an XP token while the server is not in an XP call.    |
|    3913    |    16    |    Yes    |    TDS reset connection protocol error. Client driver requested both ResetConnectionKeepLocalXact and ResetConnectionKeepDTCXact at the same time. This is not expected in server.    |
|    3914    |    16    |    No    |    The data type "%s" is invalid for transaction names or savepoint names. Allowed data types are char, varchar, nchar, varchar(max), nvarchar, and nvarchar(max).    |
|    3915    |    16    |    No    |    Cannot use the ROLLBACK statement within an INSERT-EXEC statement.    |
|    3916    |    16    |    No    |    Cannot use the COMMIT statement within an INSERT-EXEC statement unless BEGIN TRANSACTION is used first.    |
|    3917    |    16    |    No    |    Session is bound to a transaction context that is in use. Other statements in the batch were ignored.    |
|    3918    |    16    |    No    |    The statement or function must be executed in the context of a user transaction.    |
|    3919    |    16    |    No    |    Cannot enlist in the transaction because the transaction has already been committed or rolled back.    |
|    3920    |    10    |    No    |    The WITH MARK option only applies to the first BEGIN TRAN WITH MARK statement. The option is ignored.    |
|    3921    |    16    |    No    |    Cannot get a transaction token if there is no transaction active. Reissue the statement after a transaction has been started    |
|    3922    |    16    |    No    |    Cannot enlist in the transaction because the transaction does not exist.    |
|    3923    |    10    |    No    |    Cannot use transaction marks on database '%.*ls' with bulk-logged operations that have not been backed up. The mark is ignored.    |
|    3924    |    10    |    No    |    The session was enlisted in an active user transaction while trying to bind to a new transaction. The session has defected from the previous user transaction.    |
|    3925    |    16    |    No    |    Invalid transaction mark name. The 'LSN:' prefix is reserved.    |
|    3926    |    10    |    No    |    The transaction active in this session has been committed or aborted by another session.    |
|    3927    |    10    |    No    |    The session had an active transaction when it tried to enlist in a Distributed Transaction Coordinator transaction.    |
|    3928    |    16    |    No    |    The marked transaction "%.*ls" failed. A timeout occurred while attempting to place a mark in the log by committing the marked transaction. This can be caused by contention with Microsoft Distributed Transaction Coordinator (MS DTC) transactions or other local marked transaction that have prepared, but not committed or aborted. Try the operation again and if the error persists, determine the source of the contention.    |
|    3929    |    16    |    No    |    No distributed or bound transaction is allowed in single user database.    |
|    3930    |    16    |    No    |    The current transaction cannot be committed and cannot support operations that write to the log file. Roll back the transaction.    |
|    3931    |    16    |    No    |    The current transaction cannot be committed and cannot be rolled back to a savepoint. Roll back the entire transaction.    |
|    3932    |    16    |    No    |    The save point name "%.*ls" that was provided is too long. The maximum allowed length is %d characters.    |
|    3933    |    16    |    No    |    Cannot promote the transaction to a distributed transaction because there is an active save point in this transaction.    |
|    3934    |    14    |    No    |    The current user cannot use this FILESTREAM transaction context. To obtain a valid FILESTREAM transaction context, use GET_FILESTREAM_TRANSACTION_CONTEXT.    |
|    3935    |    16    |    No    |    A FILESTREAM transaction context could not be initialized. This might be caused by a resource shortage. Retry the operation. Error code: 0x%x.    |
|    3936    |    16    |    No    |    The transaction could not be committed because an error occurred while tyring to flush FILESTREAM data to disk. A file may have been open at commit time or a disk I/O error may have occurred. '%.*ls' was one of the one or more files involved. ErorrCode: 0x%x    |
|    [3937](../../relational-databases/errors-events/mssqlserver-3937-database-engine-error.md)    |    16    |    No    |    While rolling back a transaction, an error occurred while trying to deliver a rollback notification to the FILESTREAM filter driver. Error code: 0x%0x.    |
|    3938    |    18    |    No    |    The transaction has been stopped because it conflicted with the execution of a FILESTREAM close operation using the same transaction. The transaction will be rolled back.    |
|    3939    |    16    |    No    |    An uncommittable transaction was detected at the beginning of the batch. The transaction was rolled back. This was caused by an error that occurred during the processing of a FILESTREAM request in the context of this transaction.    |
|    3950    |    16    |    No    |    Version store scan timed out when attempting to read the next row. Please try the statement again later when the system is not as busy.    |
|    3951    |    16    |    No    |    Transaction failed in database '%.*ls' because the statement was run under snapshot isolation but the transaction did not start in snapshot isolation. You cannot change the isolation level of the transaction to snapshot after the transaction has started unless the transaction was originally started under snapshot isolation level.    |
|    3952    |    16    |    No    |    Snapshot isolation transaction failed accessing database '%.*ls' because snapshot isolation is not allowed in this database. Use ALTER DATABASE to allow snapshot isolation.    |
|    3953    |    16    |    No    |    Snapshot isolation transaction failed in database '%.*ls' because the database was not recovered when the current transaction was started. Retry the transaction after the database has recovered.    |
|    3954    |    16    |    No    |    Snapshot isolation transaction failed to start in database '%.*ls' because the ALTER DATABASE command that disallows snapshot isolation had started before this transaction began. The database is in transition to OFF state. You will either need to change the isolation level of the transaction or re-enable the snapshot isolation in the database.    |
|    3955    |    16    |    No    |    Snapshot isolation transaction failed in database '%.*ls' because the recovery was skipped for this database. You must recover the database before you can run a transaction under snapshot isolation.    |
|    3956    |    16    |    No    |    Snapshot isolation transaction failed to start in database '%.*ls' because the ALTER DATABASE command which enables snapshot isolation for this database has not finished yet. The database is in transition to pending ON state. You must wait until the ALTER DATABASE Command completes successfully.    |
|    3957    |    16    |    No    |    Snapshot isolation transaction failed in database '%.*ls' because the database did not allow snapshot isolation when the current transaction started. It may help to retry the transaction.    |
|    3958    |    16    |    No    |    Transaction aborted when accessing versioned row in table '%.*ls' in database '%.*ls'. Requested versioned row was not found. Your tempdb is probably out of space. Please refer to BOL on how to configure tempdb for versioning.    |
|    3959    |    10    |    Yes    |    Version store is full. New version(s) could not be added. A transaction that needs to access the version store may be rolled back. Please refer to BOL on how to configure tempdb for versioning.    |
|    3960    |    16    |    No    |    Snapshot isolation transaction aborted due to update conflict. You cannot use snapshot isolation to access table '%.*ls' directly or indirectly in database '%.*ls' to update, delete, or insert the row that has been modified or deleted by another transaction. Retry the transaction or change the isolation level for the update/delete statement.    |
|    [3961](../../relational-databases/errors-events/mssqlserver-3961-database-engine-error.md)    |    16    |    No    |    Snapshot isolation transaction failed in database '%.*ls' because the object accessed by the statement has been modified by a DDL statement in another concurrent transaction since the start of this transaction. It is disallowed because the metadata is not versioned. A concurrent update to metadata can lead to inconsistency if mixed with snapshot isolation.    |
|    3962    |    16    |    No    |    Bind to another transaction while executing SQL Server internal query is not supported. Check your logon trigger definition and remove any sp_bindsession usage if any. If this error is not happening during logon trigger execution, contact production support team.    |
|    3963    |    16    |    No    |    Transaction failed in database '%.*ls' because distributed transactions are not supported under snapshot isolation.    |
|    3964    |    16    |    No    |    Transaction failed because this DDL statement is not allowed inside a snapshot isolation transaction. Since metadata is not versioned, a metadata change can lead to inconsistency if mixed within snapshot isolation.    |
|    3965    |    16    |    No    |    The PROMOTE TRANSACTION request failed because there is no local transaction active.    |
|    3966    |    17    |    No    |    Transaction is rolled back when accessing version store. It was earlier marked as victim when the version store was shrunk due to insufficient space in tempdb. This transaction was marked as a victim earlier because it may need the row version(s) that have already been removed to make space in tempdb. Retry the transaction    |
|    3967    |    17    |    Yes    |    Insufficient space in tempdb to hold row versions. Need to shrink the version store to free up some space in tempdb. Transaction (id=%I64d xsn=%I64d spid=%d elapsed_time=%d) has been marked as victim and it will be rolled back if it accesses the version store. If the problem persists, the likely cause is improperly sized tempdb or long running transactions. Please refer to BOL on how to configure tempdb for versioning.    |
|    3968    |    10    |    No    |    Snapshot isolation or read committed snapshot is not available in database '%.*ls' because SQL Server was started with one or more undocumented trace flags that prevent enabling database for versioning. Transaction started with snapshot isolation will fail and a query running under read committed snapshot will succeed but will resort back to lock based read committed.    |
|    3969    |    16    |    No    |    Distributed transaction is not supported while running SQL Server internal query. Check your logon trigger definition and remove any distributed transaction usage if any. If this error is not happening during logon trigger execution, contact production support team.    |
|    3970    |    16    |    No    |    This operation conflicts with another pending operation on this transaction. The operation failed.    |
|    3971    |    16    |    No    |    The server failed to resume the transaction. Desc:%I64x.    |
|    3972    |    20    |    Yes    |    Incoming Tabular Data Stream (TDS) protocol is incorrect. Transaction Manager event has wrong length. Event type: %d. Expected length: %d. Actual length: %d.    |
|    3973    |    16    |    No    |    The database is currently being used by another thread under the same workspace in exclusive mode. The operation failed.    |
|    3974    |    16    |    No    |    The number of databases in exclusive mode usage under a workspace is limited. Because the limit has been exceeded, the operation failed.    |
|    3975    |    16    |    No    |    The varchar(max) data type is not supported for sp_getbindtoken. The batch has been aborted.    |
|    3976    |    16    |    No    |    The transaction name has the odd length %d. The batch has been aborted.    |
|    3977    |    16    |    No    |    The savepoint name cannot be NULL. The batch has been aborted.    |
|    3978    |    16    |    No    |    Beginning a new transaction after rollback to save point is not allowed.    |
|    3979    |    16    |    No    |    The TM request is longer than expected. The request is not processed.    |
|    3980    |    16    |    No    |    The request failed to run because the batch is aborted, this can be caused by abort signal sent from client, or another request is running in the same session, which makes the session busy.    |
|    3981    |    16    |    No    |    The transaction operation cannot be performed because there are pending requests working on this transaction.    |
|    3982    |    16    |    No    |    New transaction is not allowed to be started while DTC or bound transaction is active.    |
|    3983    |    16    |    No    |    The operation failed because the session is not single threaded.    |
|    3984    |    16    |    No    |    Cannot acquire a database lock during a transaction change.    |
|    3985    |    16    |    No    |    An error occurred during the changing of transaction context. This is usually caused by low memory in the system. Try to free up more memory.    |
|    3986    |    19    |    No    |    The transaction timestamps ran out. Restart the server.    |
|    3987    |    10    |    No    |    SNAPSHOT ISOLATION is always enabled in this database.    |
|    [3988](../../relational-databases/errors-events/mssqlserver-3988-database-engine-error.md)    |    16    |    No    |    New transaction is not allowed because there are other threads running in the session.    |
|    [3989](../../relational-databases/errors-events/mssqlserver-3989-database-engine-error.md)   |    16    |    No    |    New request is not allowed to start because it should come with valid transaction descriptor.    |
|    3990    |    16    |    No    |    Transaction is not allowed to commit inside of a user defined routine, trigger or aggregate because the transaction is not started in that CLR level. Change application logic to enforce strict transaction nesting.    |
|    3991    |    16    |    No    |    The context transaction which was active before entering user defined routine, trigger or aggregate "%.*ls" has been ended inside of it, which is not allowed. Change application logic to enforce strict transaction nesting.    |
|    3992    |    16    |    No    |    Transaction count has been changed from %d to %d inside of user defined routine, trigger or aggregate "%.*ls". This is not allowed and user transaction will be rolled back. Change application logic to enforce strict transaction nesting.    |
|    3993    |    16    |    No    |    The user transaction that has been started in user defined routine, trigger or aggregate "%.*ls" is not ended upon exiting from it. This is not allowed and the transaction will be rolled back. Change application logic to enforce strict transaction nesting.    |
|    3994    |    16    |    No    |    User defined routine, trigger or aggregate tried to rollback a transaction that is not started in that CLR level. An exception will be thrown to prevent execution of rest of the user defined routine, trigger or aggregate.    |
|    3995    |    16    |    No    |    Unknown transaction isolation level %d, valid value range is 0 to 5.    |
|    3996    |    16    |    No    |    Snapshot isolation level is not supported for distributed transaction. Use another isolation level or do not use distributed transaction.    |
|    3997    |    16    |    No    |    A transaction that was started in a MARS batch is still active at the end of the batch. The transaction is rolled back.    |
|    3998    |    16    |    No    |    Uncommittable transaction is detected at the end of the batch. The transaction is rolled back.    |
|    3999    |    17    |    Yes    |    Failed to flush the commit table to disk in dbid %d due to error %d. Check the errorlog for more information.    |
