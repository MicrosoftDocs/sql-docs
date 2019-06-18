---
title: "DBCC CHECKALLOC (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CHECKALLOC_TSQL"
  - "DBCC CHECKALLOC"
  - "DBCC_CHECKALLOC_TSQL"
  - "CHECKALLOC"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DBCC CHECKALLOC statement"
  - "checking database space allocation"
  - "database space allocation [SQL Server]"
  - "consistency [SQL Server], disk space allocations"
  - "space allocation [SQL Server]"
  - "allocation checks"
  - "disk space [SQL Server], allocation consistency checks"
  - "space allocation [SQL Server], checking"
ms.assetid: bc1218eb-ffff-44ce-8122-6e4fa7d68a79
author: pmasl
ms.author: umajay
manager: craigg
---
# DBCC CHECKALLOC (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Checks the consistency of disk space allocation structures for a specified database.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```
DBCC CHECKALLOC   
[  
    ( database_name | database_id | 0   
      [ , NOINDEX   
      | , { REPAIR_ALLOW_DATA_LOSS | REPAIR_FAST | REPAIR_REBUILD } ]  
    )  
    [ WITH   
        {   
          [ ALL_ERRORMSGS ]  
          [ , NO_INFOMSGS ]   
          [ , TABLOCK ]   
          [ , ESTIMATEONLY ]   
        }  
    ]  
]  
```  
  
## Arguments  
 *database_name* | *database_id* | 0   
 The name or the ID of the database for which to check allocation and page usage.
 If not specified, or if 0 is specified, the current database is used.
 Database names must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).

 NOINDEX  
 Specifies that nonclustered indexes for user tables should not be checked.<br>NOINDEX is maintained for backward compatibility only and does not affect DBCC CHECKALLOC.

 REPAIR_ALLOW_DATA_LOSS \| REPAIR_FAST \| REPAIR_REBUILD  
 Specifies that DBCC CHECKALLOC repair the found errors. *database_name* must be in single-user mode.

 REPAIR_ALLOW_DATA_LOSS  
 Tries to repair any errors that are found. These repairs can cause some data loss. REPAIR_ALLOW_DATA_LOSS is the only option that allows for allocation errors to be repaired.

 REPAIR_FAST  
 Syntax is maintained for backward compatibility only. No repair actions are performed.

 REPAIR_REBUILD  
 Not applicable.  
 Use the REPAIR options only as a last resort. To repair errors, we recommend restoring from a backup. Repair operations do not consider any of the constraints that may exist on or between tables. If the specified table is involved in one or more constraints, we recommend running DBCC CHECKCONSTRAINTS after a repair operation. If you must use REPAIR, run DBCC CHECKDB without a repair option to find the repair level to use. If you use the REPAIR_ALLOW_DATA_LOSS level, we recommend that you back up the database before you run DBCC CHECKDB with this option.

 WITH  
 Enables options to be specified.

 ALL_ERRORMSGS  
 Displays all error messages. All error messages are displayed by default. Specifying or omitting this option has no effect.

 NO_INFOMSGS  
 Suppresses all informational messages and the report of space used.

 TABLOCK  
 Causes the DBCC command to obtain an exclusive database lock.

 ESTIMATE ONLY  
 Displays the estimated amount of tempdb space that is required to run DBCC CHECKALLOC when all the other options are specified.
  
## Remarks  
DBCC CHECKALLOC checks the allocation of all pages in the database, regardless of the type of page or type of object to which they belong. It also validates the various internal structures that are used to keep track of these pages and the relationships between them.
If NO_INFOMSGS is not specified, DBCC CHECKALLOC collects space usage information for all objects in the database. This information is printed together with any errors that are found.
  
> [!NOTE]  
> The DBCC CHECKALLOC functionality is included in [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) and [DBCC CHECKFILEGROUP](../../t-sql/database-console-commands/dbcc-checkfilegroup-transact-sql.md). This means that you do not have to run DBCC CHECKALLOC separately from these statements.   DBCC CHECKALLOC does not check FILESTREAM data. FILESTREAM stores binary large objects (BLOBS) on the file system.  
  
## Internal Database Snapshot  
DBCC CHECKALLOC uses an internal database snapshot to provide the transactional consistency that it needs to perform these checks. If a snapshot cannot be created, or TABLOCK is specified, DBCC CHECKALLOC tries to acquire an exclusive (X) lock on the database to obtain the required consistency.
  
> [!NOTE]  
> Running DBCC CHECKALLOC against tempdb does not perform any checks. This is because, for performance reasons, database snapshots are not available on tempdb. This means that the required transactional consistency cannot be obtained. Stop and start the MSSQLSERVER service to resolve any tempdb allocation issues. This action drops and re-creates the tempdb database.  
  
## Understanding DBCC Error Messages  
After the DBCC CHECKALLOC command finishes, a message is written to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log. If the DBCC command successfully executes, the message indicates a successful completion and the amount of time that the command ran. If the DBCC command stops before completing the check because of an error, the message indicates the command was terminated, a state value, and the amount of time the command ran. The following table lists and describes the state values that can be included in the message.
  
|State|Description|  
|---|---|  
|0|Error number 8930 was raised. This indicates a metadata corruption that caused the DBCC command to terminate.|  
|1|Error number 8967 was raised. There was an internal DBCC error.|  
|2|A failure occurred during emergency mode database repair.|  
|3|This indicates a metadata corruption that caused the DBCC command to terminate.|  
|4|An assert or access violation was detected.|  
|5|An unknown error occurred that terminated the DBCC command.|  
  
## Error Reporting  
A mini-dump file (SQLDUMP*nnnn*.txt) is created in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] LOG directory whenever DBCC CHECKALLOC detects a corruption error. When the Feature Usage data collection and Error Reporting features are enabled for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the file is automatically forwarded to [!INCLUDE[msCoName](../../includes/msconame-md.md)]. The collected data is used to improve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] functionality.
The dump file contains the results of the DBCC CHECKALLOC command and additional diagnostic output. The file has restricted discretionary access-control lists (DACLs). Access is limited to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account and members of the sysadmin role. By default, the sysadmin role contains all members of the Windows BUILTIN\Administrators group and the local administrator's group. The DBCC command does not fail if the data collection process fails.
  
## Resolving Errors  
If DBCC CHECKALLOC reports any errors, we recommend that you restore the database from the database backup instead of running a repair. If a backup does not exist, running a repair can correct the reported errors; however, correcting the errors might require some pages, and therefore data, to be deleted.
A repair can be performed in a user transaction. This allows for changes to be rolled back. If changes are rolled back, the database will still contain errors and must be restored from a backup. After the repairs finish, back up the database.
  
## Result Sets  
The following tables describe the information that DBCC CHECKALLOC returns.
  
|Item|Description|  
|---|---|  
|FirstIAM|Internal use only.|  
|Root|Internal use only.|  
|Dpages|Data page count.|  
|Pages used|Allocated pages.|  
|Dedicated extents|Extents allocated to the object.<br /><br /> If mixed allocation pages are used, there might be pages allocated without extents.|  
  
DBCC CHECKALLOC also reports an allocation summary for each index and partition in each file. This summary describes the distribution of the data.
  
|Item|Description|  
|---|---|  
|Reserved pages|Pages allocated to the index and the unused pages in allocated extents.|  
|Used pages|Pages allocated and being used by the index.|  
|Partition ID|Internal use only.|  
|Alloc unit ID|Internal use only.|  
|In-row data|Pages contain index or heap data.|  
|LOB data|Pages contain **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **text**, **ntext**, **xml**, and **image** data.|  
|Row-overflow data|Pages contain variable-length column data that has been pushed off-row.|  
  
DBCC CHECKALLOC returns the following result set (values may vary), except when ESTIMATEONLY or NO_INFOMSGS is specified.
  
```
DBCC results for 'master'.  
***************************************************************  
Table sysobjects                Object ID 1.  
Index ID 1         FirstIAM (1:11)   Root (1:12)    Dpages 22.  
    Index ID 1. 24 pages used in 5 dedicated extents.  
Index ID 2         FirstIAM (1:1368)   Root (1:1362)    Dpages 10.  
    Index ID 2. 12 pages used in 2 dedicated extents.  
Index ID 3         FirstIAM (1:1392)   Root (1:1408)    Dpages 4.  
    Index ID 3. 6 pages used in 0 dedicated extents.  
Total number of extents is 7.  
***************************************************************  
'...'  
***************************************************************  
Table spt_server_info                Object ID 1938105945.  
Index ID 1         FirstIAM (1:520)   Root (1:508)    Dpages 1.  
    Index ID 1. 3 pages used in 0 dedicated extents.  
Total number of extents is 0.  
***************************************************************  
Processed 52 entries in sysindexes for database ID 1.  
File 1. Number of extents = 210, used pages = 1126, reserved pages = 1280.  
           File 1 (number of mixed extents = 73, mixed pages = 184).  
    Object ID 1, Index ID 0, data extents 5, pages 24, mixed extent pages 9.  
'...'  
    Object ID 1938105945, Index ID 0, data extents 0, pages 3, mixed extent pages 3.  
Total number of extents = 210, used pages = 1126, reserved pages = 1280 in this database.  
       (number of mixed extents = 73, mixed pages = 184) in this database.  
CHECKALLOC found 0 allocation errors and 0 consistency errors in database 'master'.  
DBCC results for 'master'.  
***************************************************************  
Table sys.sysrowsetcolumns                Object ID 4.  
Index ID 1, partition ID 262144, alloc unit ID 262144 (type In-row data). FirstIAM (1:98). Root (1:94). Dpages 7.  
Index ID 1, partition ID 262144, alloc unit ID 262144 (type In-row data). 9 pages used in 1 dedicated extents.  
Index ID 1, partition ID 262144, alloc unit ID 262398 (type Row-overflow data). FirstIAM (0:0). Root (0:0). Dpages 0.  
Index ID 1, partition ID 262144, alloc unit ID 262398 (type Row-overflow data). 0 pages used in 0 dedicated extents.  
Total number of extents is 1.  
...  
***************************************************************  
Processed 201 entries in system catalog for database ID 1.  
File 1. Number of extents = 44, used pages = 300, reserved pages = 345.  
           File 1 (number of mixed extents = 29, mixed pages = 225).  
    Object ID 4, index ID 1, partition ID 262144, alloc unit ID 262144 (type In-row data), data extents 1, pages 9, mixed extent pages 8.  
    Object ID 5, index ID 1, partition ID 327680, alloc unit ID 327680 (type In-row data), data extents 0, pages 2, mixed extent pages 2.  
    Object ID 7, index ID 1, partition ID 458752, alloc unit ID 458752 (type In-row data), data extents 0, pages 5, mixed extent pages 5.  
    Object ID 8, index ID 0, partition ID 524288, alloc unit ID 524288 (type In-row data), data extents 0, pages 2, mixed extent pages 2.  
    Object ID 13, index ID 1, partition ID 851968, alloc unit ID 851968 (type In-row data), data extents 1, pages 9, mixed extent pages 8.  
    Object ID 15, index ID 1, partition ID 983040, alloc unit ID 983040 (type In-row data), data extents 0, pages 2, mixed extent pages 2.  
    Object ID 26, index ID 1, partition ID 281474978414592, alloc unit ID 1703937 (type In-row data), data extents 0, pages 3, mixed extent pages 3.  
    Object ID 27, index ID 1, partition ID 281474978480128, alloc unit ID 1769473 (type In-row data), data extents 0, pages 3, mixed extent pages 3.  
    Object ID 27, index ID 2, partition ID 562949955190784, alloc unit ID 1769474 (type In-row data), index extents 0, pages 3, mixed extent pages 3.  
...  
    Object ID 1179151246, index ID 1, partition ID 72057594038845440, alloc unit ID 13435136 (type In-row data), data extents 2, pages 18, mixed extent pages 8.  
    Object ID 1179151246, index ID 2, partition ID 72057594038910976, alloc unit ID 13566208 (type In-row data), index extents 1, pages 16, mixed extent pages 8.  
    Object ID 1911677858, index ID 0, partition ID 72057594039631872, alloc unit ID 15073536 (type In-row data), data extents 0, pages 2, mixed extent pages 2.  
Total number of extents = 41, used pages = 289, reserved pages = 323 in this database.  
       (number of mixed extents = 27, mixed pages = 211) in this database.  
CHECKALLOC found 0 allocation errors and 0 consistency errors in database 'master'.  
DBCC execution completed. If DBCC printed error messages, contact your system administrator.  
```  
  
When ESTIMATEONLY is specified, DBCC CHECKALLOC returns the following result set.
  
```
Estimated TEMPDB space needed for CHECKALLOC (KB)   
-------------------------------------------------   
34  
  
(1 row(s) affected)  
  
DBCC execution completed. If DBCC printed error messages, contact your system administrator.  
```  
  
## Permissions  
Requires membership in the sysadmin fixed server role or the db_owner fixed database role.
  
## Examples  
The following example executes `DBCC CHECKALLOC` for the current database and for the `AdventureWorks2012` database.
  
```sql  
-- Check the current database.  
DBCC CHECKALLOC;  
GO  
-- Check the AdventureWorks2012 database.  
DBCC CHECKALLOC (AdventureWorks2012);  
GO  
```  
  
## See Also  
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)
  
  

