---
title: "DBCC SHRINKDATABASE (Transact-SQL)"
description: "DBCC SHRINKDATABASE shrinks the size of the data and log files in the specified database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: umajay, KevinConanMSFT, dplessMSFT
ms.date: "08/30/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
ms.custom: event-tier1-build-2022
f1_keywords:
  - "DBCC_SHRINKDATABASE_TSQL"
  - "DBCC SHRINKDATABASE"
  - "SHRINKDATABASE_TSQL"
  - "SHRINKDATABASE"
helpviewer_keywords:
  - "data shrinking [SQL Server]"
  - "shrinking files"
  - "shrinking databases"
  - "DBCC SHRINKDATABASE statement"
  - "decreasing database size"
  - "file shrinking [SQL Server]"
  - "database shrinking [SQL Server]"
  - "logs [SQL Server], shrinking"
  - "reducing database size"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current ||>= sql-server-2016 ||>= sql-server-linux-2017|| =azure-sqldw-latest|| =azuresqldb-mi-current"
---
# DBCC SHRINKDATABASE (Transact-SQL)
[!INCLUDE [SQL Server SQL Database Azure SQL Managed Instance Azure Synapse Analytics](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Shrinks the size of the data and log files in the specified database.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```syntaxsql
DBCC SHRINKDATABASE   
( database_name | database_id | 0   
     [ , target_percent ]   
     [ , { NOTRUNCATE | TRUNCATEONLY } ]   
)  
[ WITH 
    NO_INFOMSGS ,
    {     
         [ WAIT_AT_LOW_PRIORITY 
            [ ( 
                  <wait_at_low_priority_option_list>
             )] 
         ] 
    }
]
       
< wait_at_low_priority_option > ::= 
 ABORT_AFTER_WAIT = { SELF | BLOCKERS } 
```  

```syntaxsql
-- Azure Synapse Analytics

DBCC SHRINKDATABASE   
( database_name   
     [ , target_percent ]   
)  
[ WITH NO_INFOMSGS ]

```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### _database\_name_ | _database\_id_ | 0  
Is the database name or ID to be shrunk. 0 specifies that the current database is used.  
  
#### _target\_percent_  
Is the percentage of free space that you want left in the database file after the database has been shrunk.  
  
#### NOTRUNCATE  
Moves assigned pages from the file's end to unassigned pages in the front of the file. This action compacts the data within the file. _target\_percent_ is optional. [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)] doesn't support this option. 
  
The free space at the end of the file isn't returned to the operating system, and the physical size of the file doesn't change. As such, the database appears not to shrink when you specify NOTRUNCATE.  
  
NOTRUNCATE is applicable only to data files. NOTRUNCATE doesn't affect the log file.  
  
#### TRUNCATEONLY  
Releases all free space at the end of the file to the operating system. Doesn't move any pages inside the file. The data file shrinks only to the last assigned extent. Ignores _target\_percent_ if specified with TRUNCATEONLY. [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)] doesn't support this option.
  
DBCC SHRINKDATBASE with the TRUNCATEONLY option affects the database transaction log file only. To truncate the data file, use `DBCC SHRINKFILE` instead. For more information, see [DBCC SHRINKFILE](dbcc-shrinkfile-transact-sql.md).
  
#### WITH NO_INFOMSGS  
Suppresses all informational messages that have severity levels from 0 through 10.  

### WAIT_AT_LOW_PRIORITY with shrink operations

**Applies to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]**

The wait at low priority feature reduces lock contention. For more information, see [Understanding concurrency issues with DBCC SHRINKDATABASE](#understanding-concurrency-issues-with-dbcc-shrinkdatabase).

This feature is similar to the [WAIT_AT_LOW_PRIORITY with online index operations](../statements/alter-table-transact-sql.md#wait_at_low_priority), with some differences.

- You cannot modify MAX_DURATION. The wait duration is 60000 milliseconds (1 minute).
- You cannot specify ABORT_AFTER_WAIT option NONE.

#### WAIT_AT_LOW_PRIORITY

When a shrink command is executed in WAIT_AT_LOW_PRIORITY mode, new queries requiring schema stability (Sch-S) locks are not blocked by the waiting shrink operation until the shrink operation stops waiting and starts executing. The shrink operation will execute when it is able to obtain a schema modify lock (Sch-M) lock.  If a new shrink operation in WAIT_AT_LOW_PRIORITY mode cannot obtain a lock due to a long-running query, the shrink operation will eventually timeout after 60000 milliseconds (1 minute) and will exit with no error.

If a new shrink operation in WAIT_AT_LOW_PRIORITY mode cannot obtain a lock due to a long-running query, the shrink operation will eventually timeout after 60000 milliseconds (1 minute) and will exit with no error. This will occur if the shrink operation cannot obtain the Sch-M lock due to concurrent query or queries holding Sch-S locks. When a timeout occurs, an error 49516 message will be sent to the SQL Server error log, for example: `Msg 49516, Level 16, State 1, Line 134 Shrink timeout waiting to acquire schema modify lock in WLP mode to process IAM pageID 1:2865 on database ID 5`. At this point, you can simply retry the shrink operation in WAIT_AT_LOW_PRIORITY mode knowing that there would be no impact to the application.

#### ABORT_AFTER_WAIT = [ **SELF** | BLOCKERS ]


SELF

SELF is the default option. Exit the shrink database operation currently being executed without taking any action.

BLOCKERS

Kill all user transactions that block the shrink database operation so that the operation can continue. The BLOCKERS option requires the login to have ALTER ANY CONNECTION permission.

## Result Sets  
The following table describes the columns in the result set.
  
|Column name|Description|  
|-----------------|-----------------|  
|**DbId**|Database identification number of the file the [!INCLUDE[ssDE](../../includes/ssde-md.md)] tried to shrink.|  
|**FileId**|File identification number of the file the [!INCLUDE[ssDE](../../includes/ssde-md.md)] tried to shrink.|  
|**CurrentSize**|Number of 8-KB pages the file currently occupies.|  
|**MinimumSize**|Number of 8-KB pages the file could occupy, at minimum. This value corresponds to the minimum size or originally created size of a file.|  
|**UsedPages**|Number of 8-KB pages currently used by the file.|  
|**EstimatedPages**|Number of 8-KB pages that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] estimates the file could be shrunk down to.|  
  
>[!NOTE]
> The [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not display rows for those files not shrunk.  
  
## Remarks  

>[!NOTE]
> In Azure Synapse, running a shrink command is not recommended as this is an I/O intensive operation and can take your dedicated SQL pool (formerly SQL DW) offline. In addition, there will be costing implications to your data warehouse snapshots after running this command. 

To shrink all data and log files for a specific database, execute the DBCC SHRINKDATABASE command. To shrink one data or log file at a time for a specific database, execute the [DBCC SHRINKFILE](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md) command.
  
To view the current amount of free (unallocated) space in the database, run [sp_spaceused](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md).
  
DBCC SHRINKDATABASE operations can be stopped at any point in the process, and any completed work is kept.
  
The database can't be smaller than the configured minimum size of the database. You specify the minimum size when the database is originally created. Or, the minimum size can be the last size explicitly set by using a file size changing operation. Operations like DBCC SHRINKFILE or ALTER DATABASE are examples of file-size changing operations. 

Consider a database is originally created with a size of 10 MB in size. Then, it grows to 100 MB. The smallest the database can be reduced to is 10 MB, even if all the data in the database has been deleted.
  
Specify either the NOTRUNCATE option or the TRUNCATEONLY option when you run DBCC SHRINKDATABASE. If you don't, the result is the same as if you run a DBCC SHRINKDATABASE operation with NOTRUNCATE followed by running a DBCC SHRINKDATABASE operation with TRUNCATEONLY.
  
The shrunk database doesn't have to be in single user mode. Other users can be working in the database when it's shrunk, including system databases.
  
You can't shrink a database while the database is being backed up. Conversely, you can't back up a database while a shrink operation on the database is in process.
  
## How DBCC SHRINKDATABASE Works  

DBCC SHRINKDATABASE shrinks data files on a per-file basis, but shrinks log files as if all the log files existed in one contiguous log pool. Files are always shrunk from the end.
  
Assume you have a couple of log files, a data file, and a database named `mydb`. The data and log files are 10 MB each and the data file contains 6 MB of data. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] calculates a target size for each file. This value is the size to which the file is to be shrunk. When DBCC SHRINKDATABASE is specified with _target\_percent_, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] calculates target size to be the _target\_percent_ amount of space free in the file after shrinking. 

For example, if you specify a _target\_percent_ of 25 for shrinking `mydb`, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] calculates the target size for the data file to be 8 MB (6 MB of data plus 2 MB of free space). As such, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] moves any data from the data file's last 2 MB to any free space in the data file's first 8 MB and then shrinks the file.
  
Assume the data file of `mydb` contains 7 MB of data. Specifying a _target\_percent_ of 30 allows for this data file to be shrunk to the free percentage of 30. However, specifying a _target\_percent_ of 40 doesn't shrink the data file because not enough free space can be created in the current total size of the data file.


You can think of this issue another way: 40 percent wanted free space + 70 percent full data file (7 MB out of 10 MB) is more than 100 percent. Any _target\_percentage_ greater than 30 won't shrink the data file. It won't shrink because the percentage free you want plus the current percentage that the data file occupies is over 100 percent.

  
For log files, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] uses _target\_percent_ to calculate the target size for the whole log. That's why _target\_percent_ is the amount of free space in the log after the shrink operation. Target size for the whole log is then translated to a target size for each log file.
  
DBCC SHRINKDATABASE tries to shrink each physical log file to its target size immediately. Let's say no part of the logical log stays in the virtual logs beyond the target size of the log file. Then the file is successfully truncated and DBCC SHRINKDATABASE finishes without any messages. However, if part of the logical log stays in the virtual logs beyond the target size, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] frees as much space as possible, and then issues an informational message. The message describes what actions are required to move the logical log out of the virtual logs at the end of the file. After the actions are run, DBCC SHRINKDATABASE can be used to free the remaining space.
  
A log file can only be shrunk to a virtual log file boundary. That's why shrinking a log file to a size smaller than the size of a virtual log file might not be possible. It might not be possible even if it isn't being used. The size of the virtual log file is chosen dynamically by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] when log files are created or extended.

### Understanding concurrency issues with DBCC SHRINKDATABASE

The shrink database and shrink file commands can lead to concurrency issues, especially with active maintenance such as rebuilding indexes, or on busy OLTP environments. When your application executes queries against database tables, these queries will acquire and maintain a schema stability lock (Sch-S) until the queries complete their operations. When attempting to reclaim space during regular usage, shrink database and shrink file operations currently require a schema modify lock (Sch-M) when moving or deleting Index Allocation Map (IAM) pages, blocking the Sch-S locks needed by user queries. As a result, long-running queries will block a shrink operation until the queries complete. This means that any new queries requiring Sch-S locks are also queued behind the waiting shrink operation and will also be blocked, further exacerbating this concurrency issue. This can significantly impact application query performance and will also cause difficulties completing the necessary maintenance to shrink database files. Introduced in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], the shrink wait at low priority (WLP) feature addresses this problem by taking a schema modify lock in WAIT_AT_LOW_PRIORITY mode. For more information, see [WAIT_AT_LOW_PRIORITY with shrink operations](#wait_at_low_priority-with-shrink-operations). 

For more information on Sch-S and Sch-M locks, see [Transaction locking and row versioning guide](../../relational-databases/sql-server-transaction-locking-and-row-versioning-guide.md).

## Best Practices  
Consider the following information when you plan to shrink a database:
-   A shrink operation is most effective after an operation that creates unused space, such as a truncate table or a drop table operation.
- Most databases require some free space to be available for regular day-to-day operations. If you shrink a database file repeatedly and notice that the database size grows again, this indicates that the free space is required for regular operations. In these cases, repeatedly shrinking the database file is a wasted operation. Autogrow events necessary to grow the database file hinder performance.
-   A shrink operation doesn't preserve the fragmentation state of indexes in the database, and generally increases fragmentation to a degree. This result is another reason not to repeatedly shrink the database.  
-   Unless you have a specific requirement, don't set the AUTO_SHRINK database option to ON.  
  
## Troubleshooting  
It's possible to block shrink operations by a transaction that is running under a [row versioning-based isolation level](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md). For example, a large delete operation running under a row versioning-based isolation level is in progress when a DBCC SHRINK DATABASE operation is executed. When this situation happens, the shrink operation will wait for the delete operation to complete before it shrinks the files. When the shrink operation waits, DBCC SHRINKFILE and DBCC SHRINKDATABASE operations print out an informational message (5202 for SHRINKDATABASE and 5203 for SHRINKFILE). This message prints to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log every five minutes in the first hour and then every upcoming hour. For example, if the error log contains the following error message:  
  
```
DBCC SHRINKDATABASE for database ID 9 is waiting for the snapshot   
transaction with timestamp 15 and other snapshot transactions linked to   
timestamp 15 or with timestamps older than 109 to finish.  
```  
  
This error means snapshot transactions that have timestamps older than 109 will block the shrink operation. That transaction is the last transaction that the shrink operation completed. It also indicates the **transaction_sequence_num** or **first_snapshot_sequence_num** columns in the [sys.dm_tran_active_snapshot_database_transactions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-tran-active-snapshot-database-transactions-transact-sql.md) dynamic management view contain a value of 15. The **transaction_sequence_num** or **first_snapshot_sequence_num** column in the view might contain a number that is less than the last transaction completed by a shrink operation (109). If so, the shrink operation will wait for those transactions to finish.
  
To resolve the problem, you can do one of the following tasks:
-   End the transaction that is blocking the shrink operation.  
-   End the shrink operation. Any completed work is kept.  
-   Do nothing and allow the shrink operation to wait until the blocking transaction completes.  
  
## Permissions  
Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.  
  
## Examples  
  
### A. Shrink a database and specifying a percentage of free space  
The following example reduces the size of the data and log files in the `UserDB` user database to allow for 10 percent free space in the database.  
  
```sql  
DBCC SHRINKDATABASE (UserDB, 10);  
GO  
```  
  
### B. Truncate a database  
The following example shrinks the data and log files in the `AdventureWorks` sample database to the last assigned extent.  
  
```sql  
DBCC SHRINKDATABASE (AdventureWorks2012, TRUNCATEONLY);  
```  

### C. Shrink an Azure Synapse Analytics database

```sql
DBCC SHRINKDATABASE (database_A);
DBCC SHRINKDATABASE (database_B, 10); 
```

### D. Shrink a database with WAIT_AT_LOW_PRIORITY 

The following example attempts to reduce the size of the data and log files in the `AdventureWorks2022` database to allow for 20% free space in the database. If a lock cannot be obtained within one minute, the shrink operation will abort.

```sql
DBCC SHRINKDATABASE ([AdventureWorks2022], 20) WITH WAIT_AT_LOW_PRIORITY (ABORT_AFTER_WAIT = SELF);
```

## See also  

 - [Considerations for the autogrow and autoshrink settings in SQL Server](/troubleshoot/sql/admin/considerations-autogrow-autoshrink)
 - [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md)  
 - [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 - [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)  

## Next steps

 - [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)  
 - [DBCC SHRINKFILE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md)  
 - [Shrink a database](../../relational-databases/databases/shrink-a-database.md)
 - [Shrink a file](../../relational-databases/databases/shrink-a-file.md)
