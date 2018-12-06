---
title: "DBCC SHRINKFILE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SHRINKFILE"
  - "DBCC_SHRINKFILE_TSQL"
  - "DBCC SHRINKFILE"
  - "SHRINKFILE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "data shrinking [SQL Server]"
  - "TRUNCATEONLY option"
  - "shrinking files"
  - "NOTRUNCATE option"
  - "shrinking databases"
  - "decreasing database size"
  - "file shrinking [SQL Server]"
  - "database shrinking [SQL Server]"
  - "logs [SQL Server], shrinking"
  - "reducing database size"
  - "DBCC SHRINKFILE statement"
ms.assetid: e02b2318-bee9-4d84-a61f-2fddcf268c9f
author: uc-msft
ms.author: umajay
manager: craigg
---
# DBCC SHRINKFILE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Shrinks the size of the specified data or log file for the current database, or empties a file by moving the data from the specified file to other files in the same filegroup, allowing the file to be removed from the database. You can shrink a file to a size that is less than the size specified when it was created. This resets the minimum file size to the new value.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
  
DBCC SHRINKFILE   
(  
    { file_name | file_id }   
    { [ , EMPTYFILE ]   
    | [ [ , target_size ] [ , { NOTRUNCATE | TRUNCATEONLY } ] ]  
    }  
)  
[ WITH NO_INFOMSGS ]  
```  
  
## Arguments  
*file_name*  
Is the logical name of the file to be shrunk.
  
*file_id*  
Is the identification (ID) number of the file to be shrunk. To obtain a file ID, use the [FILE_IDEX](../../t-sql/functions/file-idex-transact-sql.md) system function or query the [sys.database_files](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md) catalog view in the current database.
  
*target_size*  
Is the size for the file in megabytes, expressed as an integer. If not specified, DBCC SHRINKFILE reduces the size to the default file size. The default size is the size specified when the file was created.
  
> [!NOTE]  
>  You can reduce the default size of an empty file by using DBCC SHRINKFILE *target_size*. For example, if you create a 5-MB file and then shrink the file to 3 MB while the file is still empty, the default file size is set to 3 MB. This applies only to empty files that have never contained data.  
  
This option is not supported for FILESTREAM filegroup containers.  
If *target_size* is specified, DBCC SHRINKFILE tries to shrink the file to the specified size. Used pages in the part of the file to be freed are relocated to available free space in the part of the file retained. For example, if there is a 10-MB data file, a DBCC SHRINKFILE operations with a *target_size* of 8 causes all used pages in the last 2 MB of the file to be reallocated into any unallocated pages in the first 8 MB of the file. DBCC SHRINKFILE does not shrink a file past the size needed to store the data in the file. For example, if 7 MB of a 10-MB data file is used, a DBCC SHRINKFILE statement with a *target_size* of 6 shrinks the file to only 7 MB, not 6 MB.
  
EMPTYFILE  
Migrates all data from the specified file to other files in the **same filegroup**. In other words, EmptyFile will migrate the data from the specified file to other files in the same filegroup. Emptyfile assures you that no new data will be added to the file, despite this file not being marked as read only.The file can be removed by using the [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) statement. If the file size is altered using [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) statement, the read only flag is reset and data can be added.

For FILESTREAM filegroup containers, the file cannot be removed using ALTER DATABASE until the FILESTREAM Garbage Collector has run and deleted all the unnecessary filegroup container files that EMPTYFILE has copied to another container. For more information, see [sp_filestream_force_garbage_collection &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/filestream-and-filetable-sp-filestream-force-garbage-collection.md)
  
> [!NOTE]  
>  For information on removing a FILESTREAM container, see the corresponding section in [ALTER DATABASE File and Filegroup Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md)  
  
NOTRUNCATE  
Moves allocated pages from the end of a data file to unallocated pages in the front of the file with or without specifying *target_percent*. The free space at the end of the file is not returned to the operating system, and the physical size of the file does not change. Therefore, when NOTRUNCATE is specified, the file appears not to shrink.
NOTRUNCATE is applicable only to data files. The log files are not affected.   This option is not supported for FILESTREAM filegroup containers.
  
TRUNCATEONLY  
Releases all free space at the end of the file to the operating system but does not perform any page movement inside the file. The data file is shrunk only to the last allocated extent.
*target_size* is ignored if specified with TRUNCATEONLY.  
The TRUNCATEONLY option does not move information in the log, but does remove inactive VLFs from the end of the log file. This option is not supported for FILESTREAM filegroup containers.
  
WITH NO_INFOMSGS  
Suppresses all informational messages.
  
## Result Sets  
The following table describes the columns in the result set.
  
|Column name|Description|  
|---|---|
|**DbId**|Database identification number of the file the [!INCLUDE[ssDE](../../includes/ssde-md.md)] tried to shrink.|  
|**FileId**|The file identification number of the file the [!INCLUDE[ssDE](../../includes/ssde-md.md)] tried to shrink.|  
|**CurrentSize**|Number of 8-KB pages the file currently occupies.|  
|**MinimumSize**|Number of 8-KB pages the file could occupy, at minimum. This corresponds to the minimum size or originally created size of a file.|  
|**UsedPages**|Number of 8-KB pages currently used by the file.|  
|**EstimatedPages**|Number of 8-KB pages that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] estimates the file could be shrunk down to.|  
  
## Remarks  
DBCC SHRINKFILE applies to the files in the current database. For more information about how to change the current database, see [USE &#40;Transact-SQL&#41;](../../t-sql/language-elements/use-transact-sql.md).
  
DBCC SHRINKFILE operations can be stopped at any point in the process, and any completed work is retained. If the EMPTYFILE parameter is used on a file and the operation is cancelled, the file will not be marked to prevent additional data from being added.
  
When a DBCC SHRINKFILE operation fails, an error is raised.
  
 The database being shrunk does not have to be in single-user mode; other users can be working in the database when the file is shrunk. You do not have to run the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode to shrink the system databases.  
  
## Shrinking a Log File  
For log files, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] uses *target_size* to calculate the target size for the whole log; therefore, *target_size* is the amount of free space in the log after the shrink operation. Target size for the whole log is then translated to target size for each log file. DBCC SHRINKFILE tries to shrink each physical log file to its target size immediately. However, if part of the logical log resides in the virtual logs beyond the target size, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] frees as much space as possible, and then issues an informational message. The message describes what actions are required to move the logical log out of the virtual logs at the end of the file. After the actions are performed, DBCC SHRINKFILE can be used to free the remaining space.
  
Because a log file can only be shrunk to a virtual log file boundary, shrinking a log file to a size smaller than the size of a virtual log file might not be possible, even if it is not being used. The size of the virtual log file is chosen dynamically by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] when log files are created or extended.
  
## Best Practices  
Consider the following information when you plan to shrink a file:
-   A shrink operation is most effective after an operation that creates lots of unused space, such as a truncate table or a drop table operation.  
-   Most databases require some free space to be available for regular day-to-day operations. If you shrink a database repeatedly and notice that the database size grows again, this indicates that the space that was shrunk is required for regular operations. In these cases, repeatedly shrinking the database is a wasted operation.  
-   A shrink operation does not preserve the fragmentation state of indexes in the database, and generally increases fragmentation to a degree. This is another reason not to repeatedly shrink the database.  
-   Shrink multiple files in the same database sequentially instead of concurrently. Contention on system tables can cause delays due to blocking.  
  
## Troubleshooting  
This section describes how to diagnose and correct issues that can occur when running the DBCC SHRINKFILE command.
  
### The File Does Not Shrink  
If the shrink operation runs without error, but the file does not appear to have changed in size, verify that the file has adequate free space to remove by performing one of the following operations:
- Run the following query.  
  
```sql
SELECT name ,size/128.0 - CAST(FILEPROPERTY(name, 'SpaceUsed') AS int)/128.0 AS AvailableSpaceInMB
FROM sys.database_files;
```

-   Run the [DBCC SQLPERF](../../t-sql/database-console-commands/dbcc-sqlperf-transact-sql.md) command to return the space used in the transaction log.  
If insufficient free space is available, the shrink operation cannot reduce the file size any further.
  
Typically it is the log file that appears not to shrink. This is usually the result of a log file that has not been truncated. You can truncate the log by setting the database recovery model to SIMPLE, or by backing up the log and then running the DBCC SHRINKFILE operation again.
  
### The Shrink Operation Is Blocked  
It is possible for shrink operations to be blocked by a transaction that is running under a [row versioning-based isolation level](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md). For example, if a large delete operation running under a row versioning-based isolation level is in progress when a DBCC SHRINK DATABASE operation is executed, the shrink operation will wait for the delete operation to complete before shrinking the files. When this happens, DBCC SHRINKFILE and DBCC SHRINKDATABASE operations print out an informational message (5202 for SHRINKDATABASE and 5203 for SHRINKFILE) to the SQL Server error log every five minutes in the first hour and then every hour after that. For example, if the error log contains the following error message then the following error will occur:
  
```sql
DBCC SHRINKFILE for file ID 1 is waiting for the snapshot   
transaction with timestamp 15 and other snapshot transactions linked to   
timestamp 15 or with timestamps older than 109 to finish.  
```  
  
This means that the shrink operation is blocked by snapshot transactions that have timestamps older than 109, which is the last transaction that the shrink operation completed. It also indicates that the **transaction_sequence_num**, or **first_snapshot_sequence_num** columns in the [sys.dm_tran_active_snapshot_database_transactions](../../relational-databases/system-dynamic-management-views/sys-dm-tran-active-snapshot-database-transactions-transact-sql.md) dynamic management view contains a value of 15. If either the **transaction_sequence_num**, or **first_snapshot_sequence_num** columns in the view contains a number that is less than the last transaction completed by a shrink operation (109), the shrink operation will wait for those transactions to finish.
  
To resolve the issue, you can do one of the following tasks:
-   Terminate the transaction that is blocking the shrink operation.
-   Terminate the shrink operation. If the shrink operation is terminated, any completed work is retained.  
-   Do nothing and allow the shrink operation to wait until the blocking transaction completes.  
  
## Permissions  
Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.
  
## Examples  
  
### A. Shrinking a data file to a specified target size  
The following example shrinks the size of a data file named `DataFile1` in the `UserDB` user database to 7 MB.
  
```sql  
USE UserDB;  
GO  
DBCC SHRINKFILE (DataFile1, 7);  
GO  
```  
  
### B. Shrinking a log file to a specified target size  
The following example shrinks the log file in the `AdventureWorks` database to 1 MB. To allow the DBCC SHRINKFILE command to shrink the file, the file is first truncated by setting the database recovery model to SIMPLE.
  
```sql  
USE AdventureWorks2012;  
GO  
-- Truncate the log by changing the database recovery model to SIMPLE.  
ALTER DATABASE AdventureWorks2012  
SET RECOVERY SIMPLE;  
GO  
-- Shrink the truncated log file to 1 MB.  
DBCC SHRINKFILE (AdventureWorks2012_Log, 1);  
GO  
-- Reset the database recovery model.  
ALTER DATABASE AdventureWorks2012  
SET RECOVERY FULL;  
GO  
```  
  
### C. Truncating a data file  
The following example truncates the primary data file in the `AdventureWorks` database. The `sys.database_files` catalog view is queried to obtain the `file_id` of the data file.
  
```sql  
USE AdventureWorks2012;  
GO  
SELECT file_id, name  
FROM sys.database_files;  
GO  
DBCC SHRINKFILE (1, TRUNCATEONLY);  
```  
  
### D. Emptying a file  
The following example demonstrates the procedure for emptying a file so that it can be removed from the database. For the purposes of this example, a data file is first created and it is assumed that the file contains data.
  
```sql  
USE AdventureWorks2012;  
GO  
-- Create a data file and assume it contains data.  
ALTER DATABASE AdventureWorks2012   
ADD FILE (  
    NAME = Test1data,  
    FILENAME = 'C:\t1data.ndf',  
    SIZE = 5MB  
    );  
GO  
-- Empty the data file.  
DBCC SHRINKFILE (Test1data, EMPTYFILE);  
GO  
-- Remove the data file from the database.  
ALTER DATABASE AdventureWorks2012  
REMOVE FILE Test1data;  
GO  
```  
  
## See Also  
[ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)  
[DBCC SHRINKDATABASE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md)  
[FILE_ID &#40;Transact-SQL&#41;](../../t-sql/functions/file-id-transact-sql.md)  
[sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)  
[Shrink a File](../../relational-databases/databases/shrink-a-file.md)
  
  
