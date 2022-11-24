---
title: "Shrink a database"
description: Learn how to shrink a database by using Object in SQL Server by using SQL Server Management Studio or Transact-SQL.
ms.custom:
- FY22Q2Fresh
- event-tier1-build-2022
ms.date: "05/24/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.shrinkdatabase.f1"
helpviewer_keywords: 
  - "shrinking databases"
  - "databases [SQL Server], shrinking"
  - "decreasing database size"
  - "database shrinking [SQL Server]"
  - "reducing database size"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Shrink a database

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to shrink a database in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using Object Explorer in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 Shrinking data files recovers space by moving pages of data from the end of the file to unoccupied space closer to the front of the file. When enough free space is created at the end of the file, data pages at end of the file can be deallocated and returned to the file system.  
  
##  <a name="Restrictions"></a> Limitations and restrictions  
  
-   The database cannot be made smaller than the minimum size of the database. The minimum size is the size specified when the database was originally created, or the last explicit size set by using a file-size-changing operation, such as `DBCC SHRINKFILE`. For example, if a database was originally created with a size of 10 MB and grew to 100 MB, the smallest size the database could be reduced to is 10 MB, even if all the data in the database has been deleted.  
  
-   You cannot shrink a database while the database is being backed up. Conversely, you cannot backup a database while a shrink operation on the database is in process.
  
##  <a name="Recommendations"></a> Recommendations  
  
-   To view the current amount of free (unallocated) space in the database. For more information, see [Display Data and Log Space Information for a Database](../../relational-databases/databases/display-data-and-log-space-information-for-a-database.md)  
  
-   Consider the following information when you plan to shrink a database:  
  
    -   A shrink operation is most effective after an operation that creates a large amount of unused storage space, such as a large DELETE statement, truncate table, or a drop table operation.  
  
    -   Most databases require some free space to be available for regular day-to-day operations. If you shrink a database repeatedly and notice that the database size grows again, this indicates that the free space is required for regular operations. In these cases, repeatedly shrinking the database is a wasted operation. Autogrow events necessary to grow the database file(s) hinder performance.
  
    -   A shrink operation does not preserve the fragmentation state of indexes in the database, and generally increases fragmentation to a degree. This is another reason not to repeatedly shrink the database.  
  
    -   Unless you have a specific requirement, do not set the AUTO_SHRINK database option to ON.  
  
##  <a name="Security"></a><a name="Permissions"></a> Permissions  
 Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.  

## Remarks

Shrink operations in progress can block other queries on the database, and can be blocked by queries already in progress. Introduced in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], shrink database operations have a WAIT_AT_LOW_PRIORITY option. This feature is a new additional option for `DBCC SHRINKDATABASE` and `DBCC SHRINKFILE`. If a new shrink operation in WAIT_AT_LOW_PRIORITY mode cannot obtain the necessary locks due to a long-running query already in progress, the shrink operation will eventually time out after one minute and silently exit, preventing other queries from being blocked. For more information, see [DBCC SHRINKDATABASE](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md).

##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  
  
#### Shrink a database
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Expand **Databases**, and then right-click the database that you want to shrink.  
  
3.  Point to **Tasks**, point to **Shrink**, and then select **Database**.  
  
     **Database**  
     Displays the name of the selected database.  
  
     **Current allocated space**  
     Displays the total used and unused space for the selected database.  
  
     **Available free space**  
     Displays the sum of free space in the log and data files of the selected database.  
  
     **Reorganize files before releasing unused space**  
     Selecting this option is equivalent to executing DBCC SHRINKDATABASE specifying a target percent option. Clearing this option is equivalent to executing DBCC SHRINKDATABASE with TRUNCATEONLY option. By default, this option is not selected when the dialog is opened. If this option is selected, the user must specify a target percent option.  
  
     **Maximum free space in files after shrinking**  
     Enter the maximum percentage of free space to be left in the database files after the database has been shrunk. Permissible values are between 0 and 99.  
  
4.  Select **OK**.  

##  <a name="TsqlProcedure"></a> Use Transact-SQL  
  
#### Shrink a database 
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. This example uses [DBCC SHRINKDATABASE](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md) to decrease the size of the data and log files in the `UserDB` database, and to allow for `10` percent free space in the database.  
  
 [!code-sql[DBCC#DBCC_SHRINKDB1](../../relational-databases/databases/codesnippet/tsql/shrink-a-database_1.sql)]  
  
## <a name="FollowUp"></a> Follow Up: After you shrink a database  

 Data that is moved to shrink a file can be scattered to any available location in the file. This causes index fragmentation and can slow the performance of queries that search a range of the index. To eliminate the fragmentation, consider rebuilding the indexes on the file after shrinking. For more information, see [Rebuild an index](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md#rebuild-an-index).
  
## See also

 - [Considerations for the autogrow and autoshrink settings in SQL Server](/troubleshoot/sql/admin/considerations-autogrow-autoshrink)
 - [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md)  
 - [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 - [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   

## Next steps  

 - [DBCC SHRINKDATABASE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md)   
 - [DBCC SHRINKFILE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md)   
 - [Delete Data or Log Files from a Database](../../relational-databases/databases/delete-data-or-log-files-from-a-database.md)
 - [Shrink a file](../../relational-databases/databases/shrink-a-file.md)
