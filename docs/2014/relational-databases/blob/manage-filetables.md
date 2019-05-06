---
title: "Manage FileTables | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FileTables [SQL Server], security"
  - "FileTables [SQL Server], managing access"
ms.assetid: 93af982c-b4fe-4be0-8268-11f86dae27e1
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Manage FileTables
  Describes common administrative tasks for managing FileTables.  
  
##  <a name="HowToEnumerate"></a> How To: Get a List of FileTables and Related Objects  
 To get a list of FileTables, query one of the following catalog views:  
  
-   [sys.filetables &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-filetables-transact-sql)  
  
-   [sys.tables &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-tables-transact-sql) (Check the value of the **is_filetable** column.)  
  
```sql  
SELECT * FROM sys.filetables;  
GO  
  
SELECT * FROM sys.tables WHERE is_filetable = 1;  
GO  
```  
  
 To get a list of the system-defined objects that were created when the associated FileTables were created, query the catalog view [sys.filetable_system_defined_objects &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-filetable-system-defined-objects-transact-sql).  
  
```sql  
SELECT object_id, OBJECT_NAME(object_id) AS 'Object Name'  
    FROM sys.filetable_system_defined_objects  
    WHERE object_id = filetable_object_id;  
GO  
```  
  
##  <a name="BasicsDisabling"></a> Disabling and Re-enabling Non-Transactional Access at the Database Level  
 To acquire the exclusive access that is required for certain administrative tasks, you may have to disable non-transactional access temporarily.  
  
 **Behavior of the ALTER DATABASE statement when changing the level of non-transactional access**  
  
-   When you set non-transactional access to READ_ONLY or OFF, the ALTER DATABASE command does not return control to the user as long as there are open file handles that conflict with the requested operation. The file handles that conflict with this operation include the following:  
  
    -   When you are setting access to **NONE**, all open file handles.  
  
    -   When you are setting access to **READ_ONLY**, all file handles opened for write access.  
  
     For information about killing open file handles, see [Killing Open File Handles Associated with a FileTable](#BasicsKilling) in this topic.  
  
     If the ALTER DATABASE command is canceled or ends with a timeout, then the level of transactional access is not changed.  
  
-   If you call the ALTER DATABASE statement with a WITH \<termination> clause (ROLLBACK AFTER integer [ SECONDS ] | ROLLBACK IMMEDIATE | NO_WAIT), then all open non-transactional file handles are killed.  
  
> [!WARNING]  
>  Killing open file handles may cause users to lose unsaved data. This behavior is consistent with the behavior of the file system itself.  
  
 **Effects of disabling non-transactional access**  
  
 Changing the level of non-transactional access at the database level has the following effects on the FileTable directories under the database-level directory:  
  
-   When you set access to **NONE**, then all the FileTable directories and their contents are no longer accessible or visible.  
  
-   When you set access to **READ_ONLY**, then all the FileTable directories and their contents are also read-only.  
  
 Disabling FILESTREAM at the instance level has the following effects on the database-level directories on that instance, and the FileTable directories under them:  
  
-   None of the database-level directories on the instance are visible if FILESTREAM is disabled at the instance level.  
  
###  <a name="HowToDisable"></a> How To: Disable and Re-enable Non-Transactional Access at the Database Level  
 For more information, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-set-options).  
  
 **To disable full non-transactional access**  
 Call the **ALTER DATABASE** statement and SET the value of **NON_TRANSACTED_ACCESS** to **READ_ONLY** or **OFF**.  
  
```sql  
-- Disable write access.  
ALTER DATABASE database_name  
    SET FILESTREAM ( NON_TRANSACTED_ACCESS = READ_ONLY );  
GO  
  
-- Disable non-transactional access.  
ALTER DATABASE database_name  
    SET FILESTREAM ( NON_TRANSACTED_ACCESS = OFF );  
GO  
```  
  
 **To re-enable full non-transactional access**  
 Call the **ALTER DATABASE** statement and SET the value of **NON_TRANSACTED_ACCESS** to **FULL**.  
  
```sql  
ALTER DATABASE database_name  
    SET FILESTREAM ( NON_TRANSACTED_ACCESS = FULL );  
GO  
```  
  
###  <a name="visible"></a> How to: Ensure the Visibility of the FileTables in a Database  
 A database-level directory and the FileTable directories under it are visible when all of these conditions are true:  
  
1.  FILESTREAM is enabled at the instance level.  
  
2.  Non-transactional access is enabled at the database level.  
  
3.  A valid directory has been specified at the database level.  
  
##  <a name="BasicsEnabling"></a> Disabling and Re-enabling the FileTable Namespace at the Table Level  
 Disabling the FileTable namespace disables all the system-defined constraints and triggers that were created with the FileTable. This is useful in cases where a FileTable has to be reorganized on a large scale by using [!INCLUDE[tsql](../../includes/tsql-md.md)] operations without incurring the expense of enforcing FileTable semantics. However these operations can leave the FileTable in an inconsistent state, and can prevent the re-enabling of the FileTable namespace.  
  
 Disabling a FileTable namespace has the following results:  
  
-   FileTable columns and data are not physically dropped from the table.  
  
-   The FileTable directory and the files and directories that it contains disappear from the file system and are not available for file i/o access.  
  
-   System-defined FileTable columns cannot be dropped and recreated; otherwise, however, they behave like ordinary columns for DML operations.  
  
-   Open file handles prevent the FileTable constraints from being disabled, since this operation requires a schema lock on the table.  
  
-   Enforcement of all the FileTable semantics, including system-defined constraints and triggers, stops after the FileTable namespace is disabled.  
  
 Re-enabling a FileTable namespace has the following results:  
  
-   The FileTable is checked for consistency. If inconsistencies are found, then an error is raised and the FileTable remains disabled; otherwise, the FileTable is re-enabled.  
  
-   The enforcement of FileTable semantics, including system-defined constraints and triggers, is restored.  
  
-   The FileTable directory and the files and directories that it contains become visible in the file system and become available for file i/o access.  
  
###  <a name="HowToEnableNS"></a> How To: Disable and Re-enable the FileTable Namespace at the Table Level  
 Call the ALTER TABLE statement with the **{ ENABLE | DISABLE } FILETABLE_NAMESPACE** option.  
  
 **To disable the FileTable namespace**  
 ```sql  
ALTER TABLE filetable_name  
    DISABLE FILETABLE_NAMESPACE;  
GO  
```  
  
 **To re-enable the FileTable namespace**  
 ```sql  
ALTER TABLE filetable_name  
    ENABLE FILETABLE_NAMESPACE;  
GO  
```  
  
##  <a name="BasicsKilling"></a> Killing Open File Handles Associated with a FileTable  
 Open handles to the files stored in a FileTable can prevent the exclusive access that is required for certain administrative tasks. To enable urgent tasks, you may have to kill open file handles associated with one or more FileTables.  
  
> [!WARNING]  
>  Killing open file handles may cause users to lose unsaved data. This behavior is consistent with the behavior of the file system itself.  
  
###  <a name="HowToListOpen"></a> How To: Get a List of Open File Handles Associated with a FileTable  
 Query the catalog view [sys.dm_filestream_non_transacted_handles &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-filestream-non-transacted-handles-transact-sql).  
  
```sql  
SELECT * FROM sys.dm_filestream_non_transacted_handles;  
GO  
```  
  
###  <a name="HowToKill"></a> How To: Kill Open File Handles Associated with a FileTable  
 Call the stored procedure [sp_kill_filestream_non_transacted_handles &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/filestream-and-filetable-sp-kill-filestream-non-transacted-handles) with the appropriate arguments to kill all open file handles in the database or in the FileTable, or to kill a specific handle.  
  
```  
USE database_name;  
  
-- Kill all open handles in all the filetables in the database.  
EXEC sp_kill_filestream_non_transacted_handles;  
GO  
  
-- Kill all open handles in a single filetable.  
EXEC sp_kill_filestream_non_transacted_handles @table_name = 'filetable_name';  
GO  
  
-- Kill a single handle.  
EXEC sp_kill_filestream_non_transacted_handles @handle_id = integer_handle_id;  
GO  
```  
  
###  <a name="HowToIdentifyLocks"></a> How to: Identify the Locks Held by FileTables  
 Most locks taken by FileTables correspond to files opened by applications.  
  
 **To identify open files and the associated locks**  
 Join the **request_owner_id** field in the dynamic management view [sys.dm_tran_locks &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql) with the **fcb_id** field in [sys.dm_filestream_non_transacted_handles &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-filestream-non-transacted-handles-transact-sql). In some cases, the lock does not correspond to a single open file handle.  
  
```sql  
SELECT opened_file_name  
    FROM sys.dm_filestream_non_transacted_handles  
    WHERE fcb_id IN  
        ( SELECT request_owner_id FROM sys.dm_tran_locks );  
GO  
```  
  
##  <a name="BasicsSecurity"></a> FileTable Security  
 The files and directories stored in FileTables are secured by SQL Server security only. Table and column-based security is enforced for file system access as well as [!INCLUDE[tsql](../../includes/tsql-md.md)] access. Windows file system security APIs and ACL settings are not supported.  
  
 The security and access permissions that are applicable to FILESTREAM filegroups and containers also apply to FileTables, since the file data is stored as a FILESTREAM column in the FileTable.  
  
 **FileTable Security and Transact-SQL Access**  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] access to data in FileTables is secured in the same way as any other table. Appropriate table and column-level security checks are done for every operation that accesses or changes the data.  
  
 **FileTable Security and File System Access**  
 File system APIs require appropriate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] permissions on the entire row in the FileTable (that is, table-level permission) to open a handle to a file or directory stored in the FileTable. If the user does not have the appropriate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] permission on any column in the FileTable, then file system access is denied.  
  
##  <a name="OtherBackup"></a> Backup and FileTables  
 When you use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to back up a FileTable, the FILESTREAM data is backed up with the structured data in the database. If you do not want to back up FILESTREAM data with relational data, you can use a partial backup to exclude FILESTREAM filegroups.  
  
 **Transactional Consistency of FileTable Backups**  
  
 Many administrative tools and operations, (including backup, log backup, and transactional replication) read transactionally consistent data by reading the transaction logs. At this time, they read any FILESTREAM data updated as part of a transaction. When non-transactional access is not enabled at the database level, these tools and operations work with full transactional consistency.  
  
 However, when full non-transactional access is enabled, then a FileTable could contain data that was updated more recently (through a non-transactional update) than the transaction that the tool or process is reading from the transaction log. This means that a "point in time" restore operation to a specific transaction may contain FILESTREAM data that is more recent than that transaction. This is the expected behavior when non-transactional updates are allowed on FileTables.  
  
##  <a name="Monitor"></a> SQL Server Profiler and FileTables  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Profiler can capture the Windows File Open and File Close operations in trace output for files that are stored in a FileTable.  
  
##  <a name="OtherAuditing"></a> Auditing and FileTables  
 FileTable can be audited just like any other table. Howerver, Win32 access patterns are not set based operations. A single action in the file system translates into multiple Transact-SQL DML operations. For example, opening a file in Microsoft Word translates into multiple open/close/create/rename/delete operations and corresponding Transact-SQL DML activities. This results in verbose audit records where it is hard to correlate records between file system actions and corresponding Transact-SQL DML audit records.  
  
##  <a name="OtherDBCC"></a> DBCC and FileTables  
 You can use DBCC CHECKCONSTRAINTS to validate the constraints on a FileTable including system-defined constraints.  
  
## See Also  
 [FileTable Compatibility with Other SQL Server Features](filetable-compatibility-with-other-sql-server-features.md)   
 [FileTable DDL, Functions, Stored Procedures, and Views](../views/views.md)  
  
  
