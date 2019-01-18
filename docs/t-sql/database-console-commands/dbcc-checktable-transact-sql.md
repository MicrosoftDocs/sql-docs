---
title: "DBCC CHECKTABLE (Transact-SQL) | Microsoft Docs"
ms.date: "11/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.custom: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CHECKTABLE_TSQL"
  - "DBCC_CHECKTABLE_TSQL"
  - "DBCC CHECKTABLE"
  - "CHECKTABLE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "indexed views [SQL Server], DBCC CHECKTABLE"
  - "page integrity checks [SQL Server]"
  - "consistency [SQL Server], tables"
  - "consistency [SQL Server], indexed views"
  - "DBCC CHECKTABLE statement"
  - "integrity [SQL Server]"
  - "low overhead checks"
  - "table integrity checks [SQL Server]"
ms.assetid: 0d6cb620-eb58-4745-8587-4133a1b16994
author: uc-msft
ms.author: umajay
manager: craigg
---
# DBCC CHECKTABLE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Checks the integrity of all the pages and structures that make up the table or indexed view.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
    
## Syntax    
    
```    
DBCC CHECKTABLE     
(    
    table_name | view_name    
    [ , { NOINDEX | index_id }    
     |, { REPAIR_ALLOW_DATA_LOSS | REPAIR_FAST | REPAIR_REBUILD }     
    ]     
)    
    [ WITH     
        { ALL_ERRORMSGS ]    
          [ , EXTENDED_LOGICAL_CHECKS ]     
          [ , NO_INFOMSGS ]    
          [ , TABLOCK ]     
          [ , ESTIMATEONLY ]     
          [ , { PHYSICAL_ONLY | DATA_PURITY } ]     
          [ , MAXDOP = number_of_processors ]    
        }    
    ]    
```    
    
## Arguments    
 *table_name* | *view_name*  
 Is the table or indexed view for which to run integrity checks. Table or view names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).  
    
NOINDEX  
 Specifies that intensive checks of nonclustered indexes for user tables should not be performed. This decreases the overall execution time. NOINDEX does not affect system tables because the integrity checks are always performed on all system table indexes.  
    
 *index_id*  
 Is the index identification (ID) number for which to run integrity checks. If *index_id* is specified, DBCC CHECKTABLE runs integrity checks only on that index, together with the heap or clustered index.  
    
REPAIR_ALLOW_DATA_LOSS | REPAIR_FAST | REPAIR_REBUILD  
 Specifies that DBCC CHECKTABLE repair the found errors. To use a repair option, the database must be in single-user mode.  
    
REPAIR_ALLOW_DATA_LOSS  
 Tries to repair all reported errors. These repairs can cause some data loss.  
    
REPAIR_FAST  
 Syntax is maintained for backward compatibility only. No repair actions are performed.  
    
REPAIR_REBUILD  
 Performs repairs that have no possibility of data loss. This can include quick repairs, such as repairing missing rows in non-clustered indexes, and more time-consuming repairs, such as rebuilding an index.  
 This argument does not repair errors involving FILESTREAM data.  
    
 > [!NOTE]  
 > Use the REPAIR options only as a last resort. To repair errors, we recommend restoring from a backup. Repair operations do not consider any of the constraints that may exist on or between tables. 
 > If the specified table is involved in one or more constraints, we recommend running `DBCC CHECKCONSTRAINTS` after a repair operation.
 > If you must use REPAIR, run `DBCC CHECKTABLE` without a repair option to find the repair level to use. 
 > If you are going to use the REPAIR_ALLOW_DATA_LOSS level, we recommend that you back up the database before you run `DBCC CHECKTABLE` with this option.  
    
ALL_ERRORMSGS  
 Displays an unlimited number of errors. All error messages are displayed by default. Specifying or omitting this option has no effect.  
    
EXTENDED_LOGICAL_CHECKS  
 If the compatibility level is 100 ( [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]) or higher, performs logical consistency checks on an indexed view, XML indexes, and spatial indexes, where present.  
 For more information, see *Performing Logical Consistency Checks on Indexes* in the [Remarks](#remarks) section later in this topic.  
    
NO_INFOMSGS  
 Suppresses all informational messages.  
    
TABLOCK  
 Causes DBCC CHECKTABLE to obtain a shared table lock instead of using an internal database snapshot. TABLOCK will cause DBCC CHECKTABLE to run faster on a table under heavy load, but decreases the concurrency available on the table while DBCC CHECKTABLE is running.  
    
ESTIMATEONLY  
 Displays the estimated amount of tempdb space needed to run DBCC CHECKTABLE with all the other specified options.  
    
PHYSICAL_ONLY  
 Limits the checking to the integrity of the physical structure of the page, record headers and the physical structure of B-trees. Designed to provide a small overhead check of the physical consistency of the table, this check can also detect torn pages, and common hardware failures that can compromise data. A full run of DBCC CHECKTABLE may take considerably longer than in earlier versions. This behavior occurs because of the following reasons:  
 -   The logical checks are more comprehensive.  
 -   Some of the underlying structures to be checked are more complex.  
 -   Many new checks have been introduced to include the new features.  
   
Therefore, using the PHYSICAL_ONLY option may cause a much shorter run-time for DBCC CHECKTABLE on large tables and is therefore recommended for frequent use on production systems. We still recommend that a full run of DBCC CHECKTABLE be performed periodically. The frequency of these runs depends on factors specific to individual businesses and production environments. PHYSICAL_ONLY always implies NO_INFOMSGS and is not allowed with any one of the repair options.  
    
 > [!NOTE]  
 > Specifying PHYSICAL_ONLY causes DBCC CHECKTABLE to skip all checks of FILESTREAM data.  
    
DATA_PURITY  
 Causes DBCC CHECKTABLE to check the table for column values that are not valid or out-of-range. For example, DBCC CHECKTABLE detects columns with date and time values that are larger than or less than the acceptable range for the **datetime** data type; or **decimal** or approximate-numeric data type columns with scale or precision values that are not valid.  
 Column-value integrity checks are enabled by default and do not require the DATA_PURITY option. For databases upgraded from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can use DBCC CHECKTABLE WITH DATA_PURITY to find and correct errors on a specific table; however, column-value checks on the table are not enabled by default until DBCC CHECKDB WITH DATA_PURITY has been run error free on the database. After this, DBCC CHECKDB and DBCC CHECKTABLE check column-value integrity by default.  
 Validation errors reported by this option cannot be fixed by using DBCC repair options. For information about manually correcting these errors, see Knowledge Base article 923247: [Troubleshooting DBCC error 2570 in SQL Server 2005 and later versions](https://support.microsoft.com/kb/923247).  
 If PHYSICAL_ONLY is specified, column-integrity checks are not performed.  
    
MAXDOP  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2 through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]).  
 
 Overrides the **max degree of parallelism** configuration option of **sp_configure** for the statement. The MAXDOP can exceed the value configured with sp_configure. If MAXDOP exceeds the value configured with Resource Governor, the Database Engine uses the Resource Governor MAXDOP value, described in ALTER WORKLOAD GROUP (Transact-SQL). All semantic rules used with the max degree of parallelism configuration option are applicable when you use the MAXDOP query hint. For more information, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).  
    
 > [!NOTE]  
 > If MAXDOP is set to zero then the server chooses the max degree of parallelism.  
    
## Remarks    
    
> [!NOTE]    
> To perform DBCC CHECKTABLE on every table in the database, use [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md).    
    
For the specified table, DBCC CHECKTABLE checks for the following:
-   Index, in-row, LOB, and row-overflow data pages are correctly linked.    
-   Indexes are in their correct sort order.    
-   Pointers are consistent.    
-   The data on each page is reasonable, included computed columns.    
-   Page offsets are reasonable.    
-   Every row in the base table has a matching row in each nonclustered index, and vice-versa.    
-   Every row in a partitioned table or index is in the correct partition.    
-   Link-level consistency between the file system and table when storing **varbinary(max)** data in the file system using FILESTREAM.    
    
## Performing Logical Consistency Checks on Indexes    
Logical consistency checking on indexes varies according to the compatibility level of the database, as follows:
-   If the compatibility level is 100 ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]) or higher:    
    -   Unless NOINDEX is specified, DBCC CHECKTABLE performs both physical and logical consistency checks on a single table and on all its nonclustered indexes. However, on XML indexes, spatial indexes, and indexed views only physical consistency checks are performed by default.    
    -   If WITH EXTENDED_LOGICAL_CHECKS is specified, logical checks are performed on an indexed view, XML indexes, and spatial indexes, where present. By default, physical consistency checks are performed before the logical consistency checks. If NOINDEX is also specified, only the logical checks are performed.    
         These logical consistency checks cross check the internal index table of the index object with the user table that it is referencing. To find outlying rows, an internal query is constructed to perform a full intersection of the internal and user tables. Running this query can have a very high effect on performance, and its progress cannot be tracked. Therefore, we recommend that you specify WITH EXTENDED_LOGICAL_CHECKS only if you suspect index issues that are unrelated to physical corruption, or if page-level checksums have been turned off and you suspect column-level hardware corruption.    
    -   If the index is a filtered index, DBCC CHECKDB performs consistency checks to verify that the index entries satisfy the filter predicate.   
      
- Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], additional checks on persisted computed columns, UDT columns, and filtered indexes will not run by default to avoid the expensive expression evaluations. This change greatly reduces the duration of CHECKDB against databases containing these objects. However, the physical consistency checks of  these objects is always completed. Only when EXTENDED_LOGICAL_CHECKS option is specified will the expression evaluations be performed in addition to already present logical checks (indexed view, XML indexes, and spatial indexes) as part of the EXTENDED_LOGICAL_CHECKS option.
-  If the compatibility level is 90 ([!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]) or less, unless NOINDEX is specified, DBCC CHECKTABLE performs both physical and logical consistency checks on a single table or indexed view and on all its nonclustered and XML indexes. Spatial indexes are not supported.
    
 **To learn the compatibility level of a database**    
[View or Change the Compatibility Level of a Database](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md)    
    
## Internal Database Snapshot    
DBCC CHECKTABLE uses an internal database snapshot to provide the transactional consistency that it must have to perform these checks. For more information, see [View the Size of the Sparse File of a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/view-the-size-of-the-sparse-file-of-a-database-snapshot-transact-sql.md) and the "DBCC Internal Database Snapshot Usage" section in [DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md).
If a snapshot cannot be created, or TABLOCK is specified, DBCC CHECKTABLE acquires a shared table lock to obtain the required consistency.
    
> [!NOTE]    
> If DBCC CHECKTABLE is run against tempdb, it must acquire a shared table lock. This is because, for performance reasons, database snapshots are not available on tempdb. This means that the required transactional consistency cannot be obtained.    
    
## Checking and Repairing FILESTREAM Data    
When FILESTREAM is enabled for a database and table, you can optionally store **varbinary(max)** binary large objects (BLOBs) in the file system. When using DBCC CHECKTABLE on a table that stores BLOBs in the file system, DBCC checks link-level consistency between the file system and database.
For example, if a table contains a **varbinary(max)** column that uses the FILESTREAM attribute, DBCC CHECKTABLE will check that there is a one-to-one mapping between file system directories and files and table rows, columns, and column values. DBCC CHECKTABLE can repair corruption if you specify the REPAIR_ALLOW_DATA_LOSS option. To repair FILESTREAM corruption, DBCC will delete any table rows that are missing file system data and will delete any directories and files that do not map to a table row, column, or column value.
    
## Checking Objects in Parallel    
By default, DBCC CHECKTABLE performs parallel checking of objects. The degree of parallelism is automatically determined by the query processor. The maximum degree of parallelism is configured in the same manner as that of parallel queries. To restrict the maximum number of processors available for DBCC checking, use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md). For more information, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).
Parallel checking can be disabled by using trace flag 2528. For more information, see [Trace Flags &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).
    
> [!NOTE]    
> During a DBCC CHECKTABLE operation, the bytes that are stored in a byte-ordered user-defined type column must be equal to the computed serialization of the user-defined type value. If this is not true, the DBCC CHECKTABLE routine will report a consistency error.    
    
## Understanding DBCC Error Messages    
After the DBCC CHECKTABLE command finishes, a message is written to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log. If the DBCC command successfully executes, the message indicates a successful completion and the amount of time that the command ran. If the DBCC command stops before completing the check because of an error, the message indicates the command was terminated, a state value, and the amount of time the command ran. The following table lists and describes the state values that can be included in the message.
    
|State|Description|    
|-----------|-----------------|    
|0|Error number 8930 was raised. This indicates a metadata corruption that caused the DBCC command to terminate.|    
|1|Error number 8967 was raised. There was an internal DBCC error.|    
|2|A failure occurred during emergency mode database repair.|    
|3|This indicates a metadata corruption that caused the DBCC command to terminate.|    
|4|An assert or access violation was detected.|    
|5|An unknown error occurred that terminated the DBCC command.|    
    
## Error Reporting    
A mini-dump file (`SQLDUMP*nnnn*.txt`) is created in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] LOG directory whenever DBCC CHECKTABLE detects a corruption error. When the *Feature Usage* data collection and *Error Reporting* features are enabled for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the file is automatically forwarded to [!INCLUDE[msCoName](../../includes/msconame-md.md)]. The collected data is used to improve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] functionality.
The dump file contains the results of the DBCC CHECKTABLE command and additional diagnostic output. The file has restricted discretionary access-control lists (DACLs). Access is limited to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account and members of the sysadmin role. By default, the sysadmin role contains all members of the Windows BUILTIN\Administrators group and the local administrator's group. The DBCC command does not fail if the data collection process fails.
    
## Resolving Errors    
 If DBCC CHECKTABLE reports any errors, we recommend restoring the database from the database backup instead of running REPAIR with one of the REPAIR options. If no backup exists, running REPAIR can correct the errors that are reported. The REPAIR option to use is specified at the end of the list of reported errors. However, that correcting the errors by using the REPAIR_ALLOW_DATA_LOSS option might require that some pages, and therefore data, be deleted.    
The repair can be performed under a user transaction to allow the user to roll back the changes that have been made. If repairs are rolled back, the database will still contain errors and must be restored from a backup. After you have completed all repairs, back up the database.
    
## Result Sets    
DBCC CHECKTABLE returns the following result set. The same result set is returned if you specify only the table name or any of the options.
    
```
DBCC results for 'HumanResources.Employee'.    
There are 288 rows in 13 pages for object 'Employee'.    
DBCC execution completed. If DBCC printed error messages, contact your system administrator.    
```    
    
DBCC CHECKTABLE returns the following result set if the ESTIMATEONLY option is specified:
    
```
Estimated TEMPDB space needed for CHECKTABLES (KB)     
--------------------------------------------------     
21    
(1 row(s) affected)    
DBCC execution completed. If DBCC printed error messages, contact your system administrator.    
```    
    
## Permissions    
User must own the table, or be a member of the sysadmin fixed server role, the db_owner fixed database role, or the db_ddladmin fixed database role.    
    
## Examples    
    
### A. Checking a specific table    
The following example checks the data page integrity of the `HumanResources.Employee` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.
    
```sql    
DBCC CHECKTABLE ('HumanResources.Employee');    
GO    
```    
    
### B. Performing a low-overhead check of the table    
 The following example performs a low overhead check of the `Employee` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.    
    
```sql    
DBCC CHECKTABLE ('HumanResources.Employee') WITH PHYSICAL_ONLY;    
GO    
```    
    
### C. Checking a specific index    
 The following example checks a specific index, obtained by accessing `sys.indexes`.    
    
```sql    
DECLARE @indid int;    
SET @indid = (SELECT index_id     
              FROM sys.indexes    
              WHERE object_id = OBJECT_ID('Production.Product')    
                    AND name = 'AK_Product_Name');    
DBCC CHECKTABLE ('Production.Product',@indid);    
```    
    
## See Also    
[DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)     
[DBCC CHECKDB &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)    
    
  
