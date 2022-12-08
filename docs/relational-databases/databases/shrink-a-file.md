---
title: "Shrink a file"
description: Learn how to shrink a data or log file in SQL Server by using SQL Server Management Studio or Transact-SQL.
ms.custom:
- event-tier1-build-2022
ms.date: "05/24/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.shrinkfile.f1"
helpviewer_keywords: 
  - "shrinking files"
  - "decreasing file size"
  - "databases [SQL Server], shrinking"
  - "reducing file size"
  - "size [SQL Server], files"
  - "file size [SQL Server]"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Shrink a file

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to shrink a data or log file in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 Shrinking data files recovers space by moving pages of data from the end of the file to unoccupied space closer to the front of the file. When enough free space is created at the end of the file, data pages at end of the file can be deallocated and returned to the file system.  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The primary data file cannot be made smaller than the size of the primary file in the model database.  
  
###  <a name="Recommendations"></a> Recommendations  

-   A shrink operation is most effective after an operation that creates a large amount of unused storage space, such as a large DELETE statement, truncate table, or a drop table operation.  

- Most databases require some free space to be available for regular day-to-day operations. If you shrink a database file repeatedly and notice that the database size grows again, this indicates that the free space is required for regular operations. In these cases, repeatedly shrinking the database file is a wasted operation. Autogrow events necessary to grow the database file a hinder performance.
  
-   Data that is moved to shrink a file can be scattered to any available location in the file. This causes index fragmentation and can slow the performance of queries that search a range of the index. To eliminate the fragmentation, consider rebuilding the indexes on the file after shrinking.  

-   Unless you have a specific requirement, do not set the AUTO_SHRINK database option to ON.  

## Remarks 

Shrink operations in progress can block other queries on the database, and can be blocked by queries already in progress. Introduced in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], shrink file operations have a WAIT_AT_LOW_PRIORITY option. This feature is a new additional option for `DBCC SHRINKDATABASE` and `DBCC SHRINKFILE`. If a new shrink operation in WAIT_AT_LOW_PRIORITY mode cannot obtain the necessary locks due to a long-running query already in progress, the shrink operation will eventually time out after one minute and silently exit, preventing other queries from being blocked. WAIT_AT_LOW_PRIORITY applies to data files (.mdf & .ndf). It does not apply to transaction log files. For more information, see [DBCC SHRINKFILE](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md).

###  <a name="Security"></a><a name="Permissions"></a>Permissions  
 Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To shrink a data or log file  
  
1.  In Object Explorer, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
  
2.  Expand **Databases** and then right-click the database that you want to shrink.  
  
3.  Point to **Tasks**, point to **Shrink**, and then select **Files**.  
  
     **Database**  
     Displays the name of the selected database.  
  
     **File type**  
     Select the file type for the file. The available choices are **Data** and **Log** files. The default selection is **Data**. Selecting a different filegroup type changes the selections in the other fields accordingly.  
  
     **Filegroup**  
     Select a filegroup from the list of Filegroups associated with the selected **File type** above. Selecting a different filegroup changes the selections in the other fields accordingly.  
  
     **File name**  
     Select a file from the list of available files of the selected filegroup and file type.  
  
     **Location**  
     Displays the full path to the currently selected file. The path is not editable, but it can be copied to the clipboard.  
  
     **Currently allocated space**  
     For data files, displays the current allocated space. For log files, displays the current allocated space computed from the output of DBCC SQLPERF(LOGSPACE).  
  
     **Available free space**  
     For data files, displays the current available free space computed from the output of DBCC SHOWFILESTATS(fileid). For log files, displays the current available free space computed from the output of DBCC SQLPERF(LOGSPACE).  
  
     **Release unused space**  
     Cause any unused space in the files to be released to the operating system and shrink the file to the last allocated extent, reducing the file size without moving any data. No attempt is made to relocate rows to unallocated pages.  
  
     **Reorganize pages before releasing unused space**  
     Equivalent to executing DBCC SHRINKFILE specifying the target file size. When this option is selected, the user must specify a target file size in the **Shrink file to** box.  
  
     **Shrink file to**  
     Specifies the target file size for the shrink operation. The size cannot be less than the current allocated space or more than the total extents allocated to the file. Entering a value beyond the minimum or the maximum will revert to the min or the max once the focus is changed or when any of the buttons on the toolbar are clicked.  
  
     **Empty file by migrating the data to other files in the same filegroup**  
     Migrate all data from the specified file. This option allows the file to be dropped using the ALTER DATABASE statement. This option is equivalent to executing DBCC SHRINKFILE with the EMPTYFILE option.  
  
4.  Select the file type and file name.  
  
5.  Optionally, select the **Release unused space** check box.  
  
     Selecting this option causes any unused space in the file to be released to the operating system and shrinks the file to the last allocated extent. This reduces the file size without moving any data.  
  
6.  Optionally, select the **Reorganize files before releasing unused space** check box. If this is selected, the **Shrink file to** value must be specified. By default, the option is cleared.  
  
     Selecting this option causes any unused space in the file to be released to the operating system and tries to relocate rows to unallocated pages.  
  
7.  Optionally, enter the maximum percentage of free space to be left in the database file after the database has been shrunk. Permissible values are between 0 and 99. This option is only available when **Reorganize files before releasing unused space** is enabled.  
  
8.  Optionally, select the **Empty file by migrating the data to other files in the same filegroup** check box.  
  
     Selecting this option moves all data from the specified file to other files in the filegroup. The empty file can then be deleted. This option is the same as executing DBCC SHRINKFILE with the EMPTYFILE option.  
  
9. Select **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To shrink a data or log file  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. This example uses [DBCC SHRINKFILE](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md) to shrink the size of a data file named `DataFile1` in the `UserDB` database to 7 MB.  
  
 [!code-sql[DBCC#DBCC_SHRINKFILE1](../../relational-databases/databases/codesnippet/tsql/shrink-a-file_1.sql)]  
  
## See also  

 - [Considerations for the autogrow and autoshrink settings in SQL Server](/troubleshoot/sql/admin/considerations-autogrow-autoshrink)
 - [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md)  
 - [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 - [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)  
 - [FILE_ID &#40;Transact-SQL&#41;](../../t-sql/functions/file-id-transact-sql.md)  

## Next steps

 - [DBCC SHRINKDATABASE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md)   
 - [DBCC SHRINKFILE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md)
 - [Delete Data or Log Files from a Database](../../relational-databases/databases/delete-data-or-log-files-from-a-database.md)   
 - [Shrink a database](../../relational-databases/databases/shrink-a-database.md)
