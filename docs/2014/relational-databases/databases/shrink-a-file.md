---
title: "Shrink a File | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.shrinkfile.f1"
helpviewer_keywords: 
  - "shrinking files"
  - "decreasing file size"
  - "databases [SQL Server], shrinking"
  - "reducing file size"
  - "size [SQL Server], files"
  - "file size [SQL Server]"
ms.assetid: ce5c8798-c039-4ab2-81e7-90a8d688b893
author: stevestein
ms.author: sstein
manager: craigg
---
# Shrink a File
  This topic describes how to shrink a data or log file in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 Shrinking data files recovers space by moving pages of data from the end of the file to unoccupied space closer to the front of the file. When enough free space is created at the end of the file, data pages at end of the file can deallocated and returned to the file system.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To shrink a data or log file, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The primary data file cannot be made smaller than the size of the primary file in the model database.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   Data that is moved to shrink a file can be scattered to any available location in the file. This causes index fragmentation and can slow the performance of queries that search a range of the index. To eliminate the fragmentation, consider rebuilding the indexes on the file after shrinking.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To shrink a data or log file  
  
1.  In Object Explorer, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
  
2.  Expand **Databases** and then right-click the database that you want to shrink.  
  
3.  Point to **Tasks**, point to **Shrink**, and then click **Files**.  
  
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
  
9. Click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To shrink a data or log file  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. This example uses [DBCC SHRINKFILE](/sql/t-sql/database-console-commands/dbcc-shrinkfile-transact-sql) to shrink the size of a data file named `DataFile1` in the `UserDB` database to 7 MB.  
  
 [!code-sql[DBCC#DBCC_SHRINKFILE1](../../snippets/tsql/SQL14/tsql/dbcc/transact-sql/dbcc_other.sql#dbcc_shrinkfile1)]  
  
## See Also  
 [DBCC SHRINKDATABASE &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql)   
 [Shrink a Database](shrink-a-database.md)   
 [Delete Data or Log Files from a Database](delete-data-or-log-files-from-a-database.md)   
 [sys.databases &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql)   
 [sys.database_files &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-database-files-transact-sql)  
  
  
