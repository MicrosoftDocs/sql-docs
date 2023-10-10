---
title: "FileTable Compatibility"
description: Find out how FileTables work with other SQL Server features. Read about which features SQL Server supports with FileTables and which constraints it enforces.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 10/02/2023
ms.service: sql
ms.subservice: filestream
ms.topic: conceptual
helpviewer_keywords:
  - "FileTables [SQL Server], using with other features"
---
# FileTable compatibility with other SQL Server features

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Describes how FileTables work with other features of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## <a id="alwayson"></a> Always On availability groups and FileTables

When the database that contains FILESTREAM or FileTable data belongs to an Always On availability group:

- FileTable functionality is partially supported by [!INCLUDE [ssHADR](../../includes/sshadr-md.md)]. After a failover, FileTable data is accessible on the primary replica, but FileTable data isn't accessible on readable secondary replicas.

  > [!NOTE]  
  > After a failover all FILESTREAM functionality is supported. FILESTREAM data is accessible on both readable secondary replicas and on the new primary.

- The FILESTREAM and FileTable functions accept or return virtual network names (VNNs) instead of computer names. For more information about these functions, see [FILESTREAM and FileTable Functions (Transact-SQL)](../system-functions/filestream-and-filetable-functions-transact-sql.md).

- All access to FILESTREAM or FileTable data through the file system APIs should use VNNs instead of computer names. For more information, see [Use FILESTREAM and FileTable with Always On availability groups](../../database-engine/availability-groups/windows/filestream-and-filetable-with-always-on-availability-groups-sql-server.md).

## <a id="OtherPartitioning"></a> Partition and FileTables

Partitioning isn't supported on FileTables. With the support for multiple FILESTREAM file groups, pure scale-up issues can be handled without having to resort to partitioning  in most scenarios (unlike SQL 2008 FILESTREAMs).

## <a id="OtherRepl"></a> Replication and FileTables

Replication and related features (including transactional replication, merge replication, change data capture, and change tracking) aren't supported with FileTables.

## <a id="OtherIsolation"></a> Transaction semantics and FileTables

#### Windows applications

Windows applications don't understand database transactions, so Windows write operations don't provide the ACID properties of a database transaction. Therefore transactional rollbacks and recovery aren't possible with Windows update operations.

#### Transact-SQL applications

For Transact-SQL applications working on the FILESTREAM (file_stream) column in a FileTable, the isolation semantics are the same as with FILESTREAM data type in a regular user table.

## <a id="OtherQueryNot"></a> Query notifications and FileTables

The query can't contain reference to the FILESTREAM column in the FileTable, in the WHERE clause or any other part of the query.

## <a id="OtherSelectInto"></a> SELECT INTO and FileTables

SELECT INTO statements from a FileTable don't propagate the FileTable semantics on the created destination table (just like FILESTREAM columns in a regular table). All the destination table columns behave just like normal columns. They don't have any FileTable semantics associated with them.

## <a id="OtherTriggers"></a> Triggers and FileTables

#### DDL (Data Definition Language) triggers

There are no special considerations for DDL triggers with FileTables. Normal DDL triggers fire for Create/Alter database operations as well as CREATE/ALTER TABLE operations for FileTables. Triggers can retrieve the actual event data that includes the DDL command text and other information by calling the EVENTDATA() function. There are no new events or changes to the existing Eventdata schema.

#### DML (Data Manipulation Language) triggers

These restrictions are enforced during the DDL operation to create triggers.

- FileTables **don't** support INSTEAD OF triggers for DML operations. This is an existing restriction on all tables that contain FILESTREAM columns.

- FileTables supports AFTER triggers for DML operations.

- Triggers defined on a FileTable can't update any FileTables (including the parent FileTable). This restriction exists mainly to prevent a trigger from getting into locking conflicts with the locks held by the file system access in the same transaction.

#### Non-transactional access and its effects on triggers

- When non-transactional update access is allowed in a database, it is possible to do in-place update of the FILESTREAM data in any table, including FileTable in that database. Due to this possibility, the before image of the FILESTREAM contents may not be available for use by the trigger.

- For non-transactional update operations through the file system, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] creates an internal transaction to capture the CloseHandle operation, and any defined DML triggers may be fired as part of that transaction. A rollback such a transaction inside the trigger body, while not prevented, doesn't roll back the changes done to the FILESTREAM. Such a rollback may also prevent the Update triggers from being fired, even though the FILESTREAM contents may have been changed.

- In addition to these impacts, triggers on FileTables need to deal with couple of additional behaviors

  - With non-transactional update operations on FileTable through the file system, it is possible that the FILESTREAM contents may be exclusively locked by other Win32 operations and may not be accessible for read/write through the trigger body. In such cases, any attempt to access the FILESTREAM contents within the trigger body may give a "Sharing Violation" error. Triggers should be designed to handle such errors appropriately.

  - AFTER image of the FILESTREAM may not be stable since in some cases it may be actively being written by other non-transactional updates at the same time (due to the sharing modes permitted in the file system access).

- Abnormal termination of Win32 handles, like explicit killing of Win32 handles by an admin OR a database crash, don't execute user triggers during the recovery operations, even though the FILESTREAM content may have been changed by the abnormally terminated Win32 application.

## <a id="OtherViews"></a> Views and FileTables

#### Views

A view can be created on a FileTable as on any other table. However the following considerations apply to a view created on a FileTable:

- Views can't have any FileTable semantics. That is, the columns in the view (including file attribute columns) behave just like normal view columns without any special semantics and same is true for rows representing Files/directories.

- Views may be updateable based on the "updateable view" semantics, but the underlying table constraints can reject the updates just like in the table.

- The file path for a file can be visualized in the view by adding it as an explicit column in the view. For example:

  `CREATE VIEW MP3FILES AS SELECT column1, column2, ..., GetFileNamespacePath() AS PATH, column3,...  FROM Documents`

#### Indexed views

Currently indexed views can't include FILESTREAM columns or computed/persisted computed columns that depend on the FILESTREAM columns. This behavior remains unchanged with views defined on the FileTable also.

## <a id="OtherSnapshots"></a> Snapshot isolation and FileTables

Read Committed Snapshot Isolation (RCSI) and Snapshot Isolation (SI) rely on the ability to have a snapshot of the data available for readers, even when update operations are happening on the data. However FileTables allow non-transactional write access to Filestream data. As a result, the following restrictions apply to these features in databases that contain FileTables:

- A database that contains FileTables can be altered to enable RCSI/SI.

- When non_transactional access is set to FULL for the database, then a transaction running under RCSI or SI has the following behavior:

  - Any [!INCLUDE [tsql](../../includes/tsql-md.md)] reads of the FileTable file_stream column fail. INSERT and UPDATE to the column still succeed, as long as they don't read from the file_stream column.

  - If the [!INCLUDE [tsql](../../includes/tsql-md.md)] statement specifies READCOMMITTEDLOCK table hints, the reads succeed, and take locks on the rows, rather than use row versioning.

  - Transacted Win32 FileStream open requests also fail.

  - Non-transacted FileTable Win32 access succeeds. All internal queries done by FileTable aren't affected.

  - Fulltext indexing always succeeds, no matter what the database options are (READ_COMMITTED_SNAPSHOT or ALLOW_SNAPSHOT_ISOLATION).

## <a id="readsec"></a> Readable secondary databases

The same considerations apply to readable secondary databases as to snapshots, as described in the preceding section, [Snapshot Isolation and FileTables](#OtherSnapshots).

## <a id="CDB"></a> Contained databases and FileTables

The FILESTREAM feature on which the FileTable feature depends requires some configuration outside of the database. Therefore a database that uses FILESTREAM or FileTable isn't fully contained.

You can set database containment to PARTIAL if you want to use certain features of contained databases, such as contained users. In this case, however, some of the database settings aren't contained in the database, and aren't automatically moved when the database moves.

## Related content

- [Manage FileTables](manage-filetables.md)
