---
title: "FileTable Compatibility with Other SQL Server Features | Microsoft Docs"
ms.custom: ""
ms.date: "08/26/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FileTables [SQL Server], using with other features"
ms.assetid: f12a17e4-bd3d-42b0-b253-efc36876db37
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# FileTable Compatibility with Other SQL Server Features
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Describes how FileTables work with other features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
##  <a name="alwayson"></a> AlwaysOn Availability Groups and FileTables  
 When the database that contains FILESTREAM or FileTable data belongs to an AlwaysOn availability group:  
  
-   FileTable functionality is partially supported by [!INCLUDE[ssHADR](../../includes/sshadr-md.md)]. After a failover, FileTable data is accessible on the primary replica, but FileTable data is not accessible on readable secondary replicas.  
  
    > **NOTE:**  Notice that after a failover all FILESTREAM functionality is supported. FILESTREAM data is accessible on both readable secondary replicas and on the new primary.  
  
-   The FILESTREAM and FileTable functions accept or return virtual network names (VNNs) instead of computer names. For more information about these functions, see [Filestream and FileTable Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/filestream-and-filetable-functions-transact-sql.md).  
  
-   All access to FILESTREAM or FileTable data through the file system APIs should use VNNs instead of computer names. For more information, see [FILESTREAM and FileTable with Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/filestream-and-filetable-with-always-on-availability-groups-sql-server.md).  
  
##  <a name="OtherPartitioning"></a> Partitioning and FileTables  
 Partitioning is not supported on FileTables. With the support for multiple FILESTREAM file groups, pure scale-up issues can be handled without having to resort to partitioning  in most scenarios (unlike SQL 2008 FILESTREAMs).  
  
##  <a name="OtherRepl"></a> Replication and FileTables  
 Replication and related features (including transactional replication, merge replication, change data capture, and change tracking) are not supported with FileTables.  
  
##  <a name="OtherIsolation"></a> Transaction Semantics and FileTables  
 **Windows applications**  
 Windows applications do not understand database transactions, so Windows write operations do not provide the ACID properties of a database transaction. Therefore transactional rollbacks and recovery are not possible with Windows update operations.  
  
 **Transact-SQL applications**  
 For TSQL applications working on the FILESTREAM (file_stream) column in a FileTable, the isolation semantics are the same as with FILESTREAM datatype in a regular user table.  
  
##  <a name="OtherQueryNot"></a> Query Notifications and FileTables  
 The query cannot contain reference to the FILESTREAM column in the FileTable, in the WHERE clause or any other part of the query.  
  
##  <a name="OtherSelectInto"></a> SELECT INTO and FileTables  
 SELECT INTO statements from a FileTable will not propagate the FileTable semantics on the created destination table (just like FILESTREAM columns in a regular table). All the destination table columns will behave just like normal columns. They will not have any FileTable semantics associated with them.  
  
##  <a name="OtherTriggers"></a> Triggers and FileTables  
 **DDL (Data Definition Language) Triggers**  
 There are no special considerations for DDL triggers with FileTables. Normal DDL triggers will fire for Create/Alter database operations as well as CREATE/ALTER TABLE operations for FileTables. Triggers can retrieve the actual event data that includes the DDL command text and other information by calling the EVENTDATA() function. There are no new events or changes to the existing Eventdata schema.  
  
 **DML (Data Manipulation Language) Triggers**  
 These restrictions will be enforced during the DDL operation to create triggers.  
  
-   FileTables will NOT support INSTEAD OF triggers for DML operations. This is an existing restriction on all tables that contain FILESTREAM columns.  
  
-   FileTables will support AFTER triggers for DML operations.  
  
-   Triggers defined on a FileTable cannot update any FileTables (including the parent FileTable). This restriction exists mainly to prevent a trigger from getting into locking conflicts with the locks held by the file system access in the same transaction.  
  
 **Non-transactional access and its effects on triggers**  
 -   When non-transactional update access is allowed in a database, it is possible to do in-place update of the FILESTREAM data in any table, including FileTable in that database. Due to this possibility, the before image of the FILESTREAM contents may not be available for use by the trigger.  
  
-   For non-transactional update operations through File system, SQL Server will create an internal transaction to capture the CloseHandle operation and the any defined DML triggers may be fired as part of that transaction. A rollback such a transaction inside the trigger body, while not prevented, does not roll back the changes done to the FILESTREAM.  Such a rollback may also prevent the Update triggers from being fired, even though the FILESTREAM contents may have been changed.  
  
-   In addition to these impacts, triggers on FileTables need to deal with couple of additional behaviors  
  
    -   In case of non-transactional update operations on FileTable through the File system, it is possible that the FILESTREAM contents may be exclusively locked by other Win32 operations and may not be accessible for read/write through the trigger body. In such cases, any attempt to access the FILESTREAM contents within the trigger body may give a "Sharing Violation" error. Triggers should be designed to handle such errors appropriately.  
  
    -   AFTER image of the FILESTREAM may not be stable since in some cases it may be actively being written by other non-transactional updates at the same time (due to the sharing modes permitted in the File system access).  
  
-   Abnormal termination of Win32 handles, like explicit killing of Win32 handles by an admin OR a database crash, will not execute user triggers during the recovery operations, even though the FILESTREAM content may have been changed by the abnormally terminated Win32 application.  
  
##  <a name="OtherViews"></a> Views and FileTables  
 **Views**  
 A view can be created on a FileTable as on any other table. However the following considerations apply to a view created on a FileTable:  
  
-   View will not have any FileTable semantics. i.e. the columns in the view (including File Attribute columns) behave just like normal view columns without any special semantics and same is true for rows representing Files/directories.  
  
-   View may be updateable based on the "updateable view" semantics, but the underlying table constraints can reject the updates just like in the table.  
  
-   File path for a file can be visualized in the view by adding it as an explicit column in the view. For example:  
  
     `CREATE VIEW MP3FILES AS SELECT column1, column2, ..., GetFileNamespacePath() AS PATH, column3,...  FROM Documents`  
  
 **Indexed Views**  
 Currently Indexed views cannot include FILESTREAM columns or computed/persisted computed columns that depend on the FILESTREAM columns. This behavior remains unchanged with views defined on the FileTable also.  
  
##  <a name="OtherSnapshots"></a> Snapshot Isolation and FileTables  
 Read Committed Snapshot Isolation (RCSI) and Snapshot Isolation (SI) rely on the ability to have a snapshot of the data available for readers even when update operations are happening on the data. However FileTables allow non-transactional write access to Filestream data. As a result, the following restrictions apply to the use of these features in databases that contain FileTables:  
  
-   A database that contains FileTables can be altered to enable RCSI/SI.  
  
-   When non_transactional access is set to FULL for the database, then a transaction running under RCSI or SI has the following behavior:  
  
    -   Any [!INCLUDE[tsql](../../includes/tsql-md.md)] reads of the FileTable file_stream column fail. INSERT and UPDATE to the column still succeed, as long as they do not read from the file_stream column.  
  
    -   If the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement specifies READCOMMITTEDLOCK table hints, the reads succeed, and take locks on the rows, rather than use row versioning.  
  
    -   Transacted Win32 FileStream open requests also fail.  
  
    -   Non-transacted FileTable Win32 access succeeds. All internal queries done by FileTable are not affected.  
  
    -   Fulltext indexing always succeeds, no matter what the database options are (READ_COMMITTED_SNAPSHOT or ALLOW_SNAPSHOT_ISOLATION).  
  
##  <a name="readsec"></a> Readable Secondary Databases  
 The same considerations apply to readable secondary databases as to snapshots, as described in the preceding section, [Snapshot Isolation and FileTables](#OtherSnapshots).  
  
##  <a name="CDB"></a> Contained Databases and FileTables  
 The FILESTREAM feature on which the FileTable feature depends requires some configuration outside of the database. Therefore a database that uses FILESTREAM or FileTable is not fully contained.  
  
 You can set database containment to PARTIAL if you want to use certain features of contained databases, such as contained users. In this case, however, you must be aware that some of the database settings are not contained in the database and are not automatically moved when the database moves.  
  
## See Also  
 [Manage FileTables](../../relational-databases/blob/manage-filetables.md)  
  
  
