---
title: "FILESTREAM (SQL Server) | Microsoft Docs"
description: Learn about FILESTREAM, a SQL Server feature that stores data in the file system. Read about how it stores, secures, and provides access to data.
ms.custom: ""
ms.date: "01/11/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FILESTREAM [SQL Server]"
  - "FILESTREAM [SQL Server], about"
  - "FILESTREAM [SQL Server], overview"
ms.assetid: 9a5a8166-bcbe-4680-916c-26276253eafa
author: MikeRayMSFT
ms.author: mikeray
monikerRange: ">=sql-server-2016||=azuresqldb-mi-current"
---
# FILESTREAM (SQL Server)
[!INCLUDE [SQL Server Windows Only - ASDBMI ](../../includes/applies-to-version/sql-windows-only.md)]

FILESTREAM enables [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-based applications to store unstructured data, such as documents and images, on the file system. Applications can use the rich streaming APIs and performance of the file system and at the same time maintain transactional consistency between the unstructured data and corresponding structured data.  
  
FILESTREAM integrates the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] with an NTFS or ReFS file systems by storing **varbinary(max)** binary large object (BLOB) data as files on the file system. [!INCLUDE[tsql](../../includes/tsql-md.md)] statements can insert, update, query, search, and back up FILESTREAM data. Win32 file system interfaces provide streaming access to the data.  
  
FILESTREAM uses the NT system cache for caching file data. Caching files in the system cache helps reduce any impact that FILESTREAM data might have on [!INCLUDE[ssDE](../../includes/ssde-md.md)] performance. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] buffer pool isn't used; therefore, this memory is available for query processing.  
  
FILESTREAM is not automatically enabled when you install or upgrade [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You must enable FILESTREAM by using SQL Server Configuration Manager and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. To use FILESTREAM, you must create or modify a database to contain a special type of filegroup. Then, create or modify a table so that it contains a **varbinary(max)** column with the FILESTREAM attribute. After you complete these tasks, you can use [!INCLUDE[tsql](../../includes/tsql-md.md)] and Win32 to manage the FILESTREAM data.  

## When to Use FILESTREAM

In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], BLOBs can be standard **varbinary(max)** data that stores the data in tables, or FILESTREAM **varbinary(max)** objects that store the data in the file system. The size and use of the data determines whether you should use database storage or file system storage. If the following conditions are true, you should consider using FILESTREAM:  

- Objects that are being stored are, on average, larger than 1 MB.  
- Fast read access is important.
- You're developing applications that use a middle tier for application logic.  

For smaller objects, storing **varbinary(max)** BLOBs in the database often provides better streaming performance.  

## FILESTREAM Storage

FILESTREAM storage is implemented as a **varbinary(max)** column in which the data is stored as BLOBs in the file system. The sizes of the BLOBs are limited only by the volume size of the file system. The standard **varbinary(max)** limitation of 2-GB file sizes doesn't apply to BLOBs that are stored in the file system.  
  
To specify that a column should store data on the file system, specify the FILESTREAM attribute on a **varbinary(max)** column. This attribute causes the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to store all data for that column on the file system, but not in the database file.  
  
FILESTREAM data must be stored in FILESTREAM filegroups. A FILESTREAM filegroup is a special filegroup that contains file system directories instead of the files themselves. These file system directories are called *data containers*. Data containers are the interface between [!INCLUDE[ssDE](../../includes/ssde-md.md)] storage and file system storage. 

When you use FILESTREAM storage, consider the following:  

- When a table contains a FILESTREAM column, each row must have a nonnull unique row ID.  
- Multiple data containers can be added to a FILESTREAM filegroup.  
- FILESTREAM data containers can't be nested.  
- When you're using failover clustering, the FILESTREAM filegroups must be on shared disk resources.  
- FILESTREAM filegroups can be on compressed volumes.

### Integrated Management

Because FILESTREAM is implemented as a **varbinary(max)** column and integrated directly into the [!INCLUDE[ssDE](../../includes/ssde-md.md)], most [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] management tools and functions work without modification for FILESTREAM data. For example, you can use all backup and recovery models with FILESTREAM data, and the FILESTREAM data is backed up with the structured data in the database. If you don't want to back up FILESTREAM data with relational data, you can use a partial backup to exclude FILESTREAM filegroups.  

### Integrated Security

In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], FILESTREAM data is secured just like other data is secured: by granting permissions at the table or column levels. If a user has permission to the FILESTREAM column in a table, the user can open the associated files.  

> [!NOTE]
> Encryption is not supported on FILESTREAM data.  

Only the account under which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account runs is granted permissions to the FILESTREAM container. We recommend that no other account be granted permissions on the data container.

> [!NOTE]
> SQL logins will not work with FILESTREAM containers. Only NTFS or ReFS authentication will work with FILESTREAM containers.

## <a name="dual"></a> Accessing BLOB Data with Transact-SQL and File System Streaming Access

After you store data in a FILESTREAM column, you can access the files by using [!INCLUDE[tsql](../../includes/tsql-md.md)] transactions or by using Win32 APIs.  
  
### Transact-SQL Access

By using [!INCLUDE[tsql](../../includes/tsql-md.md)], you can insert, update, and delete FILESTREAM data:  

- You can use an insert operation to prepopulate a FILESTREAM field with a null value, empty value, or relatively short inline data. However, a large amount of data is more efficiently streamed into a file that uses Win32 interfaces.  
- When you update a FILESTREAM field, you modify the underlying BLOB data in the file system. When a FILESTREAM field is set to NULL, the BLOB data associated with the field is deleted. You can't use a [!INCLUDE[tsql](../../includes/tsql-md.md)] chunked update, implemented as UPDATE**.**Write(), to perform partial updates to the data. 
- When you delete a row or delete or truncate a table that contains FILESTREAM data, you delete the underlying BLOB data in the file system.

### File System Streaming Access

The Win32 streaming support works in the context of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] transaction. Within a transaction, you can use FILESTREAM functions to obtain a logical UNC file system path of a file. You then use the OpenSqlFilestream API to obtain a file handle. This handle can then be used by Win32 file streaming interfaces, such as ReadFile() and WriteFile(), to access and update the file by way of the file system.  

Because file operations are transactional, you can't delete or rename FILESTREAM files through the file system.  

>[!WARNING]
>The FILESTREAM container is a folder managed by SQL Server. Do not add or remove files in the FILESTREAM folder manually or through other applications. If you do, this will result in backup and inconsistency errors. For more information, see [MSSQLSERVER_3056](../errors-events/mssqlserver-3056-database-engine-error.md), [MSSQLSERVER_7908](../errors-events/mssqlserver-7908-database-engine-error.md), and [MSSQLSERVER_7906](../errors-events/mssqlserver-7906-database-engine-error.md). 


**Statement Model**

The FILESTREAM file system access models a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement by using file open and close. The statement starts when a file handle is opened and ends when the handle is closed. For example, when a write handle is closed, any possible AFTER trigger that is registered on the table, fires as if an UPDATE statement is completed.

**Storage Namespace**

In FILESTREAM, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] controls the BLOB physical file system namespace. A new intrinsic function, [PathName](../../relational-databases/system-functions/pathname-transact-sql.md), provides the logical UNC path of the BLOB that corresponds to each FILESTREAM cell in the table. The application uses this logical path to obtain the Win32 handle and operate on the BLOB data by using regular Win32 file system interfaces. The function returns NULL if the value of the FILESTREAM column is NULL.  

**Transacted File System Access**

A new intrinsic function, [GET_FILESTREAM_TRANSACTION_CONTEXT()](../../t-sql/functions/get-filestream-transaction-context-transact-sql.md), provides the token that represents the current transaction that the session is associated with. The transaction must have been started and not yet aborted or committed. By obtaining a token, the application binds the FILESTREAM file system streaming operations with a started transaction. The function returns NULL in case of no explicitly started transaction.  

All file handles must be closed before the transaction commits or aborts. If a handle is left open beyond the transaction scope, additional reads against the handle will cause a failure; additional writes against the handle will succeed, but the actual data won't be written to disk. Similarly, if the database or instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] shuts down, all open handles are invalidated.  

**Transactional Durability**

With FILESTREAM, upon transaction commit, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] ensures transaction durability for FILESTREAM BLOB data that is modified from the file system streaming access.  

**Isolation Semantics**

The isolation semantics are governed by [!INCLUDE[ssDE](../../includes/ssde-md.md)] transaction isolation levels. Read-committed isolation level is supported for [!INCLUDE[tsql](../../includes/tsql-md.md)] and file system access. Repeatable read operations, serializable and snapshot isolation levels are supported. Dirty read isn't supported.  

The file system access open operations do not wait for any locks. Instead, the open operations fail immediately if they can't access the data because of transaction isolation. The streaming API calls fail with ERROR_SHARING_VIOLATION if the open operation cannot continue because of isolation violation.  

To allow for partial updates to be made, the application can issue a device FS control (FSCTL_SQL_FILESTREAM_FETCH_OLD_CONTENT) to fetch the old content into the file that the opened handle references. This will trigger a server-side old content copy. For better application performance and to avoid running into potential time-outs when you are working with very large files, we recommend that you use asynchronous I/O.  

If the FSCTL is issued after the handle has been written to, the last write operation will persist, and prior writes that were made to the handle are lost.

**File System APIs and Supported Isolation Levels**

When a file system API cannot open a file because of an isolation violation, an ERROR_SHARING_VIOLATION exception is returned. This isolation violation occurs when two transactions try to access the same file. The outcome of the access operation depends on the mode the file was opened in and the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that the transaction is running on. The following table outlines the possible outcomes for two transactions that are accessing the same file.

|Transaction 1|Transaction 2|Outcome on SQL Server 2008|Outcome on SQL Server 2008 R2 and later versions|  
|-------------------|-------------------|--------------------------------|------------------------------------------------------|  
|Open for read.|Open for read.|Both succeed.|Both succeed.|  
|Open for read.|Open for write.|Both succeed. Write operations under transaction 2 do not affect read operations performed in transaction 1.|Both succeed. Write operations under transaction 2 do not affect read operations performed in transaction 1.|  
|Open for write.|Open for read.|Open for transaction 2 fails with an ERROR_SHARING_VIOLATION exception.|Both succeed.|  
|Open for write.|Open for write.|Open for transaction 2 fails with an ERROR_SHARING_VIOLATION exception.|Open for transaction 2 fails with an ERROR_SHARING_VIOLATION exception.|  
|Open for read.|Open for SELECT.|Both succeed.|Both succeed.|  
|Open for read.|Open for UPDATE or DELETE.|Both succeed. Write operations under transaction 2 do not affect read operations performed in transaction 1.|Both succeed. Write operations under transaction 2 do not affect read operations performed in transaction 1.|  
|Open for write.|open for SELECT.|Transaction 2 blocks until transaction 1 commits or ends the transaction, or the transaction lock times out.|Both succeed.|  
|Open for write.|Open for UPDATE or DELETE.|Transaction 2 blocks until transaction 1 commits or ends the transaction, or the transaction lock times out.|Transaction 2 blocks until transaction 1 commits or ends the transaction, or the transaction lock times out.|  
|Open for SELECT.|Open for read.|Both succeed.|Both succeed.|  
|Open for SELECT.|Open for write.|Both succeed. Write operations under transaction 2 do not affect transaction 1.|Both succeed. Write operations under transaction 2 do not affect transaction 1.|  
|Open for UPDATE or DELETE.|Open for read.|The open operation on transaction 2 fails with an ERROR_SHARING_VIOLATION exception.|Both succeed.|  
|Open for UPDATE or DELETE.|Open for write.|The open operation on transaction 2 fails with an ERROR_SHARING_VIOLATION exception.|The open operation on transaction 2 fails with an ERROR_SHARING_VIOLATION exception.|  
|Open for SELECT with repeatable read.|Open for read.|Both succeed.|Both succeed.|  
|Open for SELECT with repeatable read.|Open for write.|The open operation on transaction 2 fails with an ERROR_SHARING_VIOLATION exception.|The open operation on transaction 2 fails with an ERROR_SHARING_VIOLATION exception.|

**Write-Through from Remote Clients**

Remote file system access to FILESTREAM data is enabled over the Server Message Block (SMB) protocol. If the client is remote, no write operations are cached by the client side. The write operations will always be sent to the server. The data can be cached on the server side. We recommend that applications that are running on remote clients consolidate small write operations into larger size operations. The goal is to perform fewer writes.  

Creating memory mapped views (memory mapped I/O) by using a FILESTREAM handle is not supported. If memory mapping is used for FILESTREAM data, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] cannot guarantee consistency and durability of the data or the integrity of the database.  

## Recommendations and guidelines for improving FILESTREAM performance

The SQL Server FILESTREAM feature allows you to store varbinary(max) binary large object data as files in the file system. When you have a large number of rows in FILESTREAM containers, which are the underlying storage for both FILESTREAM columns and FileTables, you can end up with a file system volume that contains large number of files. To achieve best performance when processing the integrated data from the database and the file system, it is important to ensure the file system is tuned optimally. The following are some of the tuning options that are available from a file system perspective:

- Altitude check for the SQL Server FILESTREAM filter driver [for example, rsfx0100.sys]. Evaluate all the filter drivers loaded for the storage stack associated with a volume where the FILESTREAM feature stores files and make sure that rsfx driver is located at the bottom of the stack. You can use the FLTMC.EXE control program to enumerate the filter drivers for a specific volume. Here's a sample output from the FLTMC utility: `C:\Windows\System32>fltMC.exe` filters

    |Filter Name|Num Instances|Altitude|Frame|
    |---|---|---|---|
    |Sftredir|1|406000|0|
    |MpFilter|9|328000|0|
    |luafv|1|135000|0|
    |FileInfo|9|45000|0|
    |RsFx0103|1|41001.03|0|

- Check that the server has the "last access time" property disabled for the files. This file system attribute is maintained in the registry:  
Key Name: `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem`  
Name: NtfsDisableLastAccessUpdate  
Type: REG_DWORD  
Value: 1

- Check that the server has 8.3 naming disabled. This file system attribute is maintained in the registry:  
Key Name: `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem`  
Name: NtfsDisable8dot3NameCreation  
Type: REG_DWORD  
Value: 1

- Check that the FILESTREAM directory containers do not have file system encryption or file system compression enabled, as these can introduce a level of overhead when accessing these files.

- From an elevated command prompt, run fltmc instances and make sure that no filter drivers are attached to the volume where you try to restore.

- Check that FILESTREAM directory containers do not have more than 300,000 files. You can use the information from `sys.database_files` catalog view to find out which directories in the file system store `FILESTREAM-related` files. This can be prevented by having multiple containers. (See the next bullet item for more information.)

- With only one FILESTREAM filegroup, all data files are created under the same folder. File creation of very large numbers of files may be impacted by large NTFS indices, which can also become fragmented.

  - Having multiple filegroups generally should help with this (the application uses partitioning or has multiple tables, each going to its own filegroup).

  - With SQL Server 2012 and later versions, you can have multiple containers or files under a FILESTREAM filegroup, and a round-robin allocation scheme will apply. Therefore the number of NTFS files per directory will get smaller.

- Backup and restore can become faster with multiple FILESTREAM containers, if multiple volumes storing containers are used.

    SQL Server 2012 supports multiple containers per filegroup and can make things much easier. No complicated partitioning schemes may be needed to manage larger number of files.

-  When there are a very large number of FILESTREAM containers in a SQL instance, starting the databases with many FILESTREAM containers might take a long time to register them in the FILESTREAM filter driver. Spreading them in multiple different volumes will help with improving database startup time.


- The NTFS MFT may become fragmented, and that can cause performance issues. The MFT reserved size does depend on volume size, so you may or may not encounter this.

  - You can check the MFT fragmentation with `defrag /A /V C:` (change C: to the actual volume name).

  - You can reserve more MFT space by using fsutil behavior set mftzone 2.

  - FILESTREAM data files should be excluded from antivirus software scanning.

    > [!NOTE]
    > Windows Server 2016 automatically enables Windows Defender. Make sure that Windows Defender is configured to exclude Filestream files. Failure to do this can result in decreased performance for backup and restore operations.
  
    For more information, see [Configure and validate exclusions for Windows Defender Antivirus scans](/windows/security/threat-protection/microsoft-defender-antivirus/configure-exclusions-microsoft-defender-antivirus).

## Related Tasks

[Enable and Configure FILESTREAM](../../relational-databases/blob/enable-and-configure-filestream.md)  
[Create a FILESTREAM-Enabled Database](../../relational-databases/blob/create-a-filestream-enabled-database.md)  
[Create a Table for Storing FILESTREAM Data](../../relational-databases/blob/create-a-table-for-storing-filestream-data.md)  
[Access FILESTREAM Data with Transact-SQL](../../relational-databases/blob/access-filestream-data-with-transact-sql.md)  
[Create Client Applications for FILESTREAM Data](../../relational-databases/blob/create-client-applications-for-filestream-data.md)  
[Access FILESTREAM Data with OpenSqlFilestream](../../relational-databases/blob/access-filestream-data-with-opensqlfilestream.md)  
[Make Partial Updates to FILESTREAM Data](../../relational-databases/blob/make-partial-updates-to-filestream-data.md)  
[Avoid Conflicts with Database Operations in FILESTREAM Applications](../../relational-databases/blob/avoid-conflicts-with-database-operations-in-filestream-applications.md)  
[Move a FILESTREAM-Enabled Database](../../relational-databases/blob/move-a-filestream-enabled-database.md)  
[Set Up FILESTREAM on a Failover Cluster](../../relational-databases/blob/set-up-filestream-on-a-failover-cluster.md)  
[Configure a Firewall for FILESTREAM Access](../../relational-databases/blob/configure-a-firewall-for-filestream-access.md)

## Related Content

[FILESTREAM Compatibility with Other SQL Server Features](../../relational-databases/blob/filestream-compatibility-with-other-sql-server-features.md)  
[Filestream and FileTable Dynamic Management Views (Transact-SQL)](../system-dynamic-management-views/filestream-and-filetable-dynamic-management-views-transact-sql.md)  
[Filestream and FileTable Catalog Views (Transact-SQL)](../system-catalog-views/filestream-and-filetable-catalog-views-transact-sql.md)  
[Filestream and FileTable System Stored Procedures (Transact-SQL)](../system-stored-procedures/filestream-and-filetable-system-stored-procedures.md)  
