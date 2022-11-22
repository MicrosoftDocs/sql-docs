---
title: "DBCC CHECKDB (Transact-SQL)"
description: "DBCC CHECKDB (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "12/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "CHECKDB_TSQL"
  - "DBCC_CHECKDB_TSQL"
  - "DBCC CHECKDB"
  - "CHECKDB"
helpviewer_keywords:
  - "CHECKDB [DBCC statement]"
  - "database objects [SQL Server], checking"
  - "counting pages"
  - "per-index row counts"
  - "per-table row counts"
  - "DBCC CHECKDB statement"
  - "per-table page counts"
  - "allocation checks"
  - "integrity [SQL Server], database objects"
  - "per-index page counts"
  - "counting rows"
  - "table integrity checks [SQL Server]"
  - "row count accuracy [SQL Server]"
  - "negative counts"
  - "checking database objects"
  - "page count accuracy [SQL Server]"
dev_langs:
  - "TSQL"
---
# DBCC CHECKDB (Transact-SQL)
[!INCLUDE [SQL Server SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Checks the logical and physical integrity of all the objects in the specified database by performing the following operations:    
    
-   Runs [DBCC CHECKALLOC](../../t-sql/database-console-commands/dbcc-checkalloc-transact-sql.md) on the database.    
-   Runs [DBCC CHECKTABLE](../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md) on every table and view in the database.    
-   Runs [DBCC CHECKCATALOG](../../t-sql/database-console-commands/dbcc-checkcatalog-transact-sql.md) on the database.    
-   Validates the contents of every indexed view in the database.    
-   Validates link-level consistency between table metadata and file system directories and files when storing **varbinary(max)** data in the file system using FILESTREAM.    
-   Validates the [!INCLUDE[ssSB](../../includes/sssb-md.md)] data in the database.
    
This means that the DBCC CHECKALLOC, DBCC CHECKTABLE, or DBCC CHECKCATALOG commands don't have to be run separately from DBCC CHECKDB. For more detailed information about the checks that these commands perform, see the descriptions of these commands.
 
> [!NOTE]
> DBCC CHECKDB is supported on databases that contain memory-optimized tables but validation only occurs on disk-based tables. However, as part of database backup and recovery, a CHECKSUM validation is done for files in memory-optimized filegroups.    
>     
> Since DBCC repair options are not available for memory-optimized tables, you must back up your databases regularly and test the backups. If data integrity issues occur in a memory-optimized table, you must restore from the last known good backup.    

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)    
    
## Syntax    
    
```syntaxsql
DBCC CHECKDB     
    [ ( database_name | database_id | 0    
        [ , NOINDEX     
        | , { REPAIR_ALLOW_DATA_LOSS | REPAIR_FAST | REPAIR_REBUILD } ]    
    ) ]    
    [ WITH     
        {    
            [ ALL_ERRORMSGS ]    
            [ , EXTENDED_LOGICAL_CHECKS ]     
            [ , NO_INFOMSGS ]    
            [ , TABLOCK ]    
            [ , ESTIMATEONLY ]    
            [ , { PHYSICAL_ONLY | DATA_PURITY } ]    
            [ , MAXDOP  = number_of_processors ]    
        }    
    ]    
]    
```    
    

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *database_name* | *database_id* | 0  
 Is the name or ID of the database for which to run integrity checks. If not specified, or if 0 is specified, the current database is used. Database names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).  
    
NOINDEX  
 Specifies that intensive checks of nonclustered indexes for user tables will not be performed. This choice decreases the overall execution time. NOINDEX doesn't affect system tables because integrity checks are always performed on system table indexes.  
    
REPAIR_ALLOW_DATA_LOSS | REPAIR_FAST | REPAIR_REBUILD  
 Specifies that DBCC CHECKDB repairs the errors found. Use the REPAIR options only as a last resort. The specified database must be in single-user mode to use one of the following repair options.  
    
REPAIR_ALLOW_DATA_LOSS  
 Tries to repair all reported errors. These repairs can cause some data loss.  
    
> [!WARNING]
> The REPAIR_ALLOW_DATA_LOSS option is a supported feature but it may not always be the best option for bringing a database to a physically consistent state. If successful, the REPAIR_ALLOW_DATA_LOSS option may result in some data loss. In fact, it may result in more data lost than if a user were to restore the database from the last known good backup. 
>
> [!INCLUDE[msCoName](../../includes/msconame-md.md)] always recommends a user restore from the last known good backup as the primary method to recover from errors reported by DBCC CHECKDB. The REPAIR_ALLOW_DATA_LOSS option is not an alternative for restoring from a known good backup. It is an emergency "last resort" option recommended for use only if restoring from a backup is not possible.    
>     
> Certain errors, that can only be repaired using the REPAIR_ALLOW_DATA_LOSS option, may involve deallocating a row, page, or series of pages to clear the errors. Any deallocated data is no longer accessible or recoverable for the user, and the exact contents of the deallocated data cannot be determined. Therefore, referential integrity may not be accurate after any rows or pages are deallocated because foreign key constraints are not checked or maintained as part of this repair operation. The user must inspect the referential integrity of their database (using DBCC CHECKCONSTRAINTS) after using the REPAIR_ALLOW_DATA_LOSS option.    
>     
> Before performing the repair, create physical copies of the files that belong to this database. This includes the primary data file (.mdf), any secondary data files (.ndf), all transaction log files (.ldf), and other containers that form the database including full text catalogs, file stream folders, memory optimized data, etc.    
>     
> Before performing the repair, consider changing the state of the database to EMERGENCY mode and trying to extract as much information possible from the critical tables and save that data.    
    
REPAIR_FAST  
 Maintains syntax for backward compatibility only. No repair actions are performed.  
    
REPAIR_REBUILD  
 Performs repairs that have no possibility of data loss. This option may include quick repairs, such as repairing missing rows in nonclustered indexes, and more time-consuming repairs, such as rebuilding an index.  
 This argument doesn't repair errors involving FILESTREAM data.  
    
> [!IMPORTANT] 
> Since DBCC CHECKDB with any of the REPAIR options are completely logged and recoverable, [!INCLUDE[msCoName](../../includes/msconame-md.md)] always recommends a user use CHECKDB with any REPAIR options within a transaction (execute BEGIN TRANSACTION before running the command) so that the user can confirm he/she wants  to accept the results of the operation. Then the user can execute COMMIT TRANSACTION to commit all work done by the repair operation. If the user does not want to accept the results of the operation, he/she can execute a ROLLBACK TRANSACTION to undo the effects of the repair operations.    
>     
> To repair errors, we recommend restoring from a backup. Repair operations do not consider any of the constraints that may exist on or between tables. If the specified table is involved in one or more constraints, we recommend running DBCC CHECKCONSTRAINTS after a repair operation. If you must use REPAIR, run DBCC CHECKDB without a repair option to find the repair level to use. If you use the REPAIR_ALLOW_DATA_LOSS level, we recommend that you back up the database before you run DBCC CHECKDB with this option.    
    
ALL_ERRORMSGS  
 Displays all reported errors per object. All error messages are displayed by default. Specifying or omitting this option has no effect. Error messages are sorted by object ID, except for those messages generated from [tempdb database](../../relational-databases/databases/tempdb-database.md).     

EXTENDED_LOGICAL_CHECKS  
 If the compatibility level is 100 ( [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]) or higher, performs logical consistency checks on an indexed view, XML indexes, and spatial indexes, where present.  
 For more information, see *Performing Logical Consistency Checks on Indexes*, in the [Remarks](#remarks) section later in this article.  
    
NO_INFOMSGS  
 Suppresses all informational messages.  
    
TABLOCK  
 Causes DBCC CHECKDB to obtain locks instead of using an internal database snapshot. This includes a short-term exclusive (X) lock on the database. TABLOCK will cause DBCC CHECKDB to run faster on a database under heavy load, but will decrease the concurrency available on the database while DBCC CHECKDB is running.  
    
> [!IMPORTANT] 
> TABLOCK limits the checks that are performed; DBCC CHECKCATALOG is not run on the database, and [!INCLUDE[ssSB](../../includes/sssb-md.md)] data is not validated.
    
ESTIMATEONLY  
 Displays the estimated amount of tempdb space that is required to run DBCC CHECKDB with all the other specified options. The actual database check is not performed.  
    
PHYSICAL_ONLY  
 Limits the checking to the integrity of the physical structure of the page and record headers and the allocation consistency of the database. This check is designed to provide a small overhead check of the physical consistency of the database, but it can also detect torn pages, checksum failures, and common hardware failures that can compromise a user's data.  
 A full run of DBCC CHECKDB may take considerably longer to complete than earlier versions. This behavior occurs because:  
 -   The logical checks are more comprehensive.  
 -   Some of the underlying structures to be checked are more complex.  
 -   Many new checks have been introduced to include the new features.  
 Therefore, using the PHYSICAL_ONLY option may cause a much shorter run-time for DBCC CHECKDB on large databases and is recommended for frequent use on production systems. We still recommend that a full run of DBCC CHECKDB be performed periodically. The frequency of these runs depends on factors specific to individual businesses and production environments.    
This argument always implies NO_INFOMSGS and isn't allowed with any one of the repair options.  
    
> [!WARNING] 
> Specifying PHYSICAL_ONLY causes DBCC CHECKDB to skip all checks of FILESTREAM data.
    
DATA_PURITY  
 Causes DBCC CHECKDB to check the database for column values that aren't valid or out-of-range. For example, DBCC CHECKDB detects columns with date and time values that are larger than or less than the acceptable range for the **datetime** data type; or **decimal** or approximate-numeric data type columns with scale or precision values that aren't valid.  
 Column-value integrity checks are enabled by default and don't require the DATA_PURITY option. For databases upgraded from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], column-value checks are not enabled by default until DBCC CHECKDB WITH DATA_PURITY has been run error free on the database. After this, DBCC CHECKDB checks column-value integrity by default. For more information about how CHECKDB might be affected by upgrading database from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see the Remarks section later in this topic.  
    
> [!WARNING]
> If PHYSICAL_ONLY is specified, column-integrity checks are not performed.
    
 Validation errors reported by this option cannot be fixed by using DBCC repair options. For information about manually correcting these errors, see Knowledge Base article 923247: [Troubleshooting DBCC error 2570 in SQL Server 2005 and later versions](https://support.microsoft.com/kb/923247).  
    
 MAXDOP  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2 and later).  
    
 Overrides the **max degree of parallelism** configuration option of **sp_configure** for the statement. The MAXDOP can exceed the value configured with sp_configure. If MAXDOP exceeds the value configured with Resource Governor, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] uses the Resource Governor MAXDOP value, described in [ALTER WORKLOAD GROUP](../../t-sql/statements/alter-workload-group-transact-sql.md). All semantic rules used with the max degree of parallelism configuration option are applicable when you use the MAXDOP query hint. For more information, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).  
 
> [!WARNING] 
> If MAXDOP is set to zero then SQL Server chooses the max degree of parallelism to use.    

## Remarks    
DBCC CHECKDB does not examine disabled indexes. For more information about disabled indexes, see [Disable Indexes and Constraints](../../relational-databases/indexes/disable-indexes-and-constraints.md).    

If a user-defined type is marked as being byte ordered, there must only be one serialization of the user-defined type. Not having a consistent serialization of byte-ordered user-defined types causes error 2537 when DBCC CHECKDB is run. For more information, see [User-Defined Type Requirements](../../relational-databases/clr-integration-database-objects-user-defined-types/creating-user-defined-types-requirements.md).    

Because the [Resource database](../../relational-databases/databases/resource-database.md) is modifiable only in single-user mode, the DBCC CHECKDB command cannot be run on it directly. However, when DBCC CHECKDB is executed against the [master database](../../relational-databases/databases/master-database.md), a second CHECKDB is also run internally on the Resource database. This means that DBCC CHECKDB can return extra results. The command returns extra result sets when no options are set, or when either the `PHYSICAL_ONLY` or `ESTIMATEONLY` option is set.    

Starting with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] SP2, executing DBCC CHECKDB **no longer** clears the plan cache for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Before [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] SP2, executing DBCC CHECKDB clears the plan cache. Clearing the plan cache causes recompilation of all later execution plans and may cause a sudden, temporary decrease in query performance. 
    
## Performing Logical Consistency Checks on Indexes    
Logical consistency checking on indexes varies according to the compatibility level of the database, as follows:
-   If the compatibility level is 100 ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]) or higher:    
-   Unless `NOINDEX` is specified, DBCC CHECKDB performs both physical and logical consistency checks on a single table and on all its nonclustered indexes. However, on XML indexes, spatial indexes, and indexed views only physical consistency checks are performed by default.
-   If `WITH EXTENDED_LOGICAL_CHECKS` is specified, logical checks are performed on an indexed view, XML indexes, and spatial indexes, where present. By default, physical consistency checks are performed before the logical consistency checks. If `NOINDEX` is also specified, only the logical checks are performed.
    
These logical consistency checks cross check the internal index table of the index object with the user table that it is referencing. To find outlying rows, an internal query is constructed to perform a full intersection of the internal and user tables. Running this query can have a very high effect on performance, and its progress cannot be tracked. Therefore, we recommend that you specify `WITH EXTENDED_LOGICAL_CHECKS` only if you suspect index issues that are unrelated to physical corruption, or if page-level checksums have been turned off and you suspect column-level hardware corruption.
-   If the index is a filtered index, DBCC CHECKDB performs consistency checks to verify that the index entries satisfy the filter predicate.
-   If the compatibility level is 90 or less, unless `NOINDEX` is specified, DBCC CHECKDB performs both physical and logical consistency checks on a single table or indexed view and on all its nonclustered and XML indexes. Spatial indexes are not supported.  
- Starting with SQL Server 2016, additional checks on persisted computed columns, UDT columns, and filtered indexes will not run by default to avoid the expensive expression evaluations. This change greatly reduces the duration of CHECKDB against databases containing these objects. However, the physical consistency check of  these objects is always completed. Only when `EXTENDED_LOGICAL_CHECKS` option is specified, will the expression evaluations be performed in addition to the logical checks that are already present  as part of the `EXTENDED_LOGICAL_CHECKS` option (indexed view, XML indexes, and spatial indexes).
    
**To learn the compatibility level of a database**
-   [View or Change the Compatibility Level of a Database](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md)    
    
## Internal Database Snapshot    
DBCC CHECKDB uses an internal database snapshot for the transactional consistency needed to perform these checks. This prevents blocking and concurrency problems when these commands are executed. For more information, see [View the Size of the Sparse File of a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/view-the-size-of-the-sparse-file-of-a-database-snapshot-transact-sql.md) and the DBCC Internal Database Snapshot Usage section in [DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md). If a snapshot cannot be created, or TABLOCK is specified, DBCC CHECKDB acquires locks to obtain the required consistency. In this case, an exclusive database lock is required to perform the allocation checks, and shared table locks are required to perform the table checks.
DBCC CHECKDB fails when run against master if an internal database snapshot cannot be created.
Running DBCC CHECKDB against tempdb does not perform any allocation or catalog checks and must acquire shared table locks to perform table checks. This is because, for performance reasons, database snapshots are not available on tempdb. This means that the required transactional consistency cannot be obtained.


### How DBCC CHECKDB creates an internal snapshot database beginning with SQL Server 2014

1. DBCC CHECKDB creates an internal snapshot database.

1. The internal snapshot database is created by using physical files. For example, for a database with database_ID = 10 that has three files `E:\Data\my_DB.mdf`, `E:\Data\my_DB.ndf`, and `E:\Data\my_DB.ldf`, the internal snapshot database will be created using `E:\Data\my_DB.mdf_MSSQL_DBCC11` and `E:\Data\my_DB.ndf_MSSQL_DBCC11` files. Note that the database_id of the snapshot is database_id + 1.  Also note that the new files are created in the same folder using the naming convention <filename.extension>_MSSQL_DBCC<database_id_of_snapshot>. No sparse file is created for the transaction log.

1. The new files are marked as [sparse files](/windows/win32/fileio/sparse-files) at the file system level. The *Size on Disk* used by the new files will increase based on how much data is updated in the source database during the DBCC CHECKDB command. The *Size* of the new files will be the same file as the .mdf or .ndf file.

1. The new files are deleted at the end of DBCC CHECKDB processing. These sparse files that are created by DBCC CHECKDB have the "Delete on Close" attributes set.

> [!WARNING]
> If the operating system encounters an unexpected shutdown while the DBCC CHECKDB command is in progress, then these files will not be cleaned up. They will take up space, and can potentially cause failures on future DBCC CHECKDB executions. In that case, you can delete these new files after you confirm that there is no DBCC CHECKDB command currently being executed.

The new files are visible by using ordinary file utilities such as Windows Explorer.

> [!NOTE]
> In SQL Server versions 2012 and earlier, named [file streams](/windows/win32/fileio/file-streams) were used instead to create the internal snapshot files. Named file streams are not visible by using ordinary file utilities such as Windows Explorer. Therefore, in SQL Server 2012 or an earlier version, you may encounter error messages 7926 and 5030 when you run the DBCC CHECKDB command for database files located on an [ReFS](/windows-server/storage/refs/refs-overview)-formatted volume. This is because file streams cannot be created on [Resilient File System (RefS)](/windows-server/storage/refs/refs-overview). For more information, see Knowledge Base article 2974455: [DBCC CHECKDB behavior when the SQL Server database is located on an ReFS volume.](https://support.microsoft.com/kb/2974455).
    
## Checking and Repairing FILESTREAM Data    
When FILESTREAM is enabled for a database and table, you can optionally store **varbinary(max)** binary large objects (BLOBs) in the file system. When using DBCC CHECKDB on a database that stores BLOBs in the file system, DBCC checks link-level consistency between the file system and database.
For example, if a table contains a **varbinary(max)** column that uses the FILESTREAM attribute, DBCC CHECKDB will check that there is a one-to-one mapping between file system directories and files and table rows, columns, and column values. DBCC CHECKDB can repair corruption if you specify the REPAIR_ALLOW_DATA_LOSS option. To repair FILESTREAM corruption, DBCC will delete any table rows that are missing file system data.
    
## Best Practices    
We recommend that you use the `PHYSICAL_ONLY` option for frequent use on production systems. Using PHYSICAL_ONLY can greatly shorten run-time for DBCC CHECKDB on large databases. We also recommend that you periodically run DBCC CHECKDB with no options. How frequently you should perform these runs depends on individual businesses and their production environments.
    
## Checking Objects in Parallel    
By default, DBCC CHECKDB performs parallel checking of objects. The degree of parallelism is automatically determined by the query processor. The maximum degree of parallelism is configured just like parallel queries. To restrict the maximum number of processors available for DBCC checking, use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md). For more information, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md). Parallel checking can be disabled by using trace flag 2528. For more information, see [Trace Flags &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).
    
> [!NOTE]
> This feature is not available in every edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see parallel consistency check in the RDBMS Manageability section of [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).    
    
## Understanding DBCC Error Messages    
After the DBCC CHECKDB command finishes, a message is written to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log. If the DBCC command successfully executes, the message indicates success and the amount of time that the command ran. If the DBCC command stops before completing the check because of an error, the message indicates that the command was terminated, a state value, and the amount of time the command ran. The following table lists and describes the state values that can be included in the message.
    
|State|Description|    
|-----------|-----------------|    
|0|Error number 8930 was raised. This indicates a corruption in metadata that terminated the DBCC command.|    
|1|Error number 8967 was raised. There was an internal DBCC error.|    
|2|A failure occurred during emergency mode database repair.|    
|3|This indicates a corruption in metadata that terminated the DBCC command.|    
|4|An assert or access violation was detected.|    
|5|An unknown error occurred that terminated the DBCC command.|    

> [!NOTE]
> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] records the date and time when a consistency check was run for a database with no errors (or "clean" consistency check). This is known as the `last known clean check`. When a database is first started, this date is written to the EventLog (EventID-17573) and ERRORLOG in the following format: 
>
>`CHECKDB for database '<database>' finished without errors on 2019-05-05 18:08:22.803 (local time). This is an informational message only; no user action is required.`
    

## Error Reporting    
A dump file (`SQLDUMP*nnnn*.txt`) is created in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] LOG directory whenever DBCC CHECKDB detects a corruption error. When the *Feature Usage* data collection and *Error Reporting* features are enabled for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the file is automatically forwarded to [!INCLUDE[msCoName](../../includes/msconame-md.md)]. The collected data is used to improve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] functionality.
The dump file contains the results of the DBCC CHECKDB command and additional diagnostic output. Access is limited to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account and members of the sysadmin role. By default, the sysadmin role contains all members of the Windows `BUILTIN\Administrators` group and the local administrator's group. The DBCC command does not fail if the data collection process fails.
    
## Resolving Errors    
If any errors are reported by DBCC CHECKDB, we recommend restoring the database from the database backup instead of running REPAIR with one of the REPAIR options. If no backup exists, running repair corrects the errors reported. The repair option to use is specified at the end of the list of reported errors. However, correcting the errors by using the REPAIR_ALLOW_DATA_LOSS option might require deleting some pages, and therefore some data.    

Under some circumstances, values might be entered into the database that are not valid or out-of-range based on the data type of the column. DBCC CHECKDB can detect column values that aren't valid for all column data types. Therefore, running DBCC CHECKDB with the DATA_PURITY option on databases that have been upgraded from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might reveal preexisting column-value errors. Because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot automatically repair these errors, the column value must be manually updated. If CHECKDB detects such an error, CHECKDB returns a warning, the error number 2570, and information to identify the affected row and manually correct the error.    

The repair can be performed under a user transaction to let the user roll back the changes that were made. If repairs are rolled back, the database will still contain errors and must be restored from a backup. After repairs are completed, back up the database.
    
## Resolving Errors in Database Emergency Mode    
When a database has been set to emergency mode by using the [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) statement, DBCC CHECKDB can perform some special repairs on the database if the REPAIR_ALLOW_DATA_LOSS option is specified. These repairs may allow for ordinarily unrecoverable databases to be brought back online in a physically consistent state. These repairs should be used as a last resort and only when you cannot restore the database from a backup. When the database is set to emergency mode, the database is marked READ_ONLY, logging is disabled, and access is limited to members of the sysadmin fixed server role.
    
> [!NOTE]
> You cannot run the DBCC CHECKDB command in emergency mode inside a user transaction and roll back the transaction after execution.    
    
When the database is in emergency mode and DBCC CHECKDB with the REPAIR_ALLOW_DATA_LOSS clause is run, the following actions are taken:
-   DBCC CHECKDB uses pages that have been marked inaccessible because of I/O or checksum errors, as if the errors have not occurred. Doing this increases the chances for data recovery from the database.    
-   DBCC CHECKDB attempts to recover the database using regular log-based recovery techniques.    
-   If, because of transaction log corruption, database recovery is unsuccessful, the transaction log is rebuilt. Rebuilding the transaction log may result in the loss of transactional consistency.    
    
> [!WARNING]
> The REPAIR_ALLOW_DATA_LOSS option is a supported feature of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. However, it may not always be the best option for bringing a database to a physically consistent state. If successful, the REPAIR_ALLOW_DATA_LOSS option may result in some data loss. 
> In fact, it may result in more data lost than if a user were to restore the database from the last known good backup. [!INCLUDE[msCoName](../../includes/msconame-md.md)] always recommends a user restore from the last known good backup as the primary method to recover from errors reported by DBCC CHECKDB. 
> The REPAIR_ALLOW_DATA_LOSS option is **not** an alternative for restoring from a known good backup. It is an emergency "last resort" option recommended for use only if restoring from a backup is not possible.    
>     
>  After rebuilding the log, there is no full ACID guarantee.    
>     
>  After rebuilding the log, DBCC CHECKDB will be automatically performed and will both report and correct physical consistency issues.    
>     
>  Logical data consistency and business logic enforced constraints must be validated manually.    
>     
>  The transaction log size will be left to its default size and must be manually adjusted back to its recent size.    
    
If the DBCC CHECKDB command succeeds, the database is in a physically consistent state and the database status is set to ONLINE. However, the database may contain one or more transactional inconsistencies. We recommend that you run [DBCC CHECKCONSTRAINTS](../../t-sql/database-console-commands/dbcc-checkconstraints-transact-sql.md) to identify any business logic flaws and immediately back up the database.
If the DBCC CHECKDB command fails, the database cannot be repaired.
    
## Running DBCC CHECKDB with REPAIR_ALLOW_DATA_LOSS in Replicated Databases    
Running the DBCC CHECKDB command with the REPAIR_ALLOW_DATA_LOSS option can affect user databases (publication and subscription databases) and the distribution database used by replication. Publication and subscription databases include published tables and replication metadata tables. Be aware of the following potential issues in these databases:
-   Published tables. Actions performed by the CHECKDB process to repair corrupt user data might not be replicated:    
-   Merge replication uses triggers to track changes to published tables. If rows are inserted, updated, or deleted by the CHECKDB process, triggers do not fire; therefore, the change is not replicated.
-   Transactional replication uses the transaction log to track changes to published tables. The Log Reader Agent then moves these changes to the distribution database. Some DBCC repairs, although logged, cannot be replicated by the Log Reader Agent. For example, if a data page is deallocated by the CHECKDB process, the Log Reader Agent does not translate this to a DELETE statement; therefore, the change is not replicated.
-   Replication metadata tables. Actions performed by the CHECKDB process to repair corrupt replication metadata tables require removing and reconfiguring replication.    
    
If you have to run the DBCC CHECKDB command with the REPAIR_ALLOW_DATA_LOSS option on a user database or distribution database:
1.  Quiesce the system: Stop activity on the database and at all other databases in the replication topology, and then try to synchronize all nodes. For more information, see [Quiesce a Replication Topology &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/administration/quiesce-a-replication-topology-replication-transact-sql-programming.md).    
1.  Execute DBCC CHECKDB.    
1.  If the DBCC CHECKDB report includes repairs for any tables in the distribution database or any replication metadata tables in a user database, remove and reconfigure replication. For more information, see [Disable Publishing and Distribution](../../relational-databases/replication/disable-publishing-and-distribution.md).    
1.  If the DBCC CHECKDB report includes repairs for any replicated tables, perform data validation to determine whether there are differences between the data in the publication and subscription databases.    
    
## Result Sets    
DBCC CHECKDB returns the following result set. The values might vary except when the ESTIMATEONLY, PHYSICAL_ONLY, or NO_INFOMSGS options are specified:
    
```
 DBCC results for 'model'.    
    
 Service Broker Msg 9675, Level 10, State 1: Message Types analyzed: 13.    
    
 Service Broker Msg 9676, Level 10, State 1: Service Contracts analyzed: 5.    
    
 Service Broker Msg 9667, Level 10, State 1: Services analyzed: 3.    
    
 Service Broker Msg 9668, Level 10, State 1: Service Queues analyzed: 3.    
    
 Service Broker Msg 9669, Level 10, State 1: Conversation Endpoints analyzed: 0.    
    
 Service Broker Msg 9674, Level 10, State 1: Conversation Groups analyzed: 0.    
    
 Service Broker Msg 9670, Level 10, State 1: Remote Service Bindings analyzed: 0.    
    
 DBCC results for 'sys.sysrowsetcolumns'.    
    
 There are 630 rows in 7 pages for object 'sys.sysrowsetcolumns'.    
    
 DBCC results for 'sys.sysrowsets'.    
    
 There are 97 rows in 1 pages for object 'sys.sysrowsets'.    
    
 DBCC results for 'sysallocunits'.    
    
 There are 195 rows in 3 pages for object 'sysallocunits'.    
    
 There are 0 rows in 0 pages for object "sys.sysasymkeys".    
    
 DBCC results for 'sys.syssqlguides'.    
    
 There are 0 rows in 0 pages for object "sys.syssqlguides".    
    
 DBCC results for 'sys.queue_messages_1977058079'.    
    
 There are 0 rows in 0 pages for object "sys.queue_messages_1977058079".    
    
 DBCC results for 'sys.queue_messages_2009058193'.    
    
 There are 0 rows in 0 pages for object "sys.queue_messages_2009058193".    
    
 DBCC results for 'sys.queue_messages_2041058307'.    
    
 There are 0 rows in 0 pages for object "sys.queue_messages_2041058307".    
    
 CHECKDB found 0 allocation errors and 0 consistency errors in database 'model'.    
    
 DBCC execution completed. If DBCC printed error messages, contact your system administrator.    
```

DBCC CHECKDB returns the following result set (message) when NO_INFOMSGS is specified:
    
```
 The command(s) completed successfully.
 ```
 
DBCC CHECKDB returns the following result set when PHYSICAL_ONLY is specified:
    
```
 DBCC results for 'model'.    
    
 CHECKDB found 0 allocation errors and 0 consistency errors in database 'master'.  
    
 DBCC execution completed. If DBCC printed error messages, contact your system administrator.
 ```
 
DBCC CHECKDB returns the following result set when ESTIMATEONLY is specified.
    
```
 Estimated TEMPDB space needed for CHECKALLOC (KB)    
    
 -------------------------------------------------  
    
 13   
    
 (1 row(s) affected)   
    
 Estimated TEMPDB space needed for CHECKTABLES (KB)    
    
 --------------------------------------------------    
    
 57 
    
 (1 row(s) affected)  
    
 DBCC execution completed. If DBCC printed error messages, contact your system administrator.
```
    
## Permissions    
Requires membership in the sysadmin fixed server role or the db_owner fixed database role.
    
## Examples    
    
### A. Checking both the current and another database    
The following example executes `DBCC CHECKDB` for the current database and for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.
    
```sql    
-- Check the current database.    
DBCC CHECKDB;    
GO    
-- Check the AdventureWorks2012 database without nonclustered indexes.    
DBCC CHECKDB (AdventureWorks2012, NOINDEX);    
GO    
```    
    
### B. Checking the current database, suppressing informational messages    
The following example checks the current database and suppresses all informational messages.
    
```sql    
DBCC CHECKDB WITH NO_INFOMSGS;    
GO    
```    
    
## See Also    
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)  
[View the Size of the Sparse File of a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/view-the-size-of-the-sparse-file-of-a-database-snapshot-transact-sql.md)  
[sp_helpdb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdb-transact-sql.md)  
[System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  

