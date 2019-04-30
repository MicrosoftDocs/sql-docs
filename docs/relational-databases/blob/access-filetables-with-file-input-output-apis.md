---
title: "Access FileTables with File Input-Output APIs | Microsoft Docs"
ms.custom: ""
ms.date: "08/25/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FileTables [SQL Server], accessing files with file APIs"
ms.assetid: fa504c5a-f131-4781-9a90-46e6c2de27bb
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Access FileTables with File Input-Output APIs
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Describes how file system I/O works on a FileTable.  
  
##  <a name="accessing"></a> Get Started Using File I/O APIs with FileTables  
 The primary usage of FileTables is expected to be through the Windows file system and file I/O APIs. FileTables support non-transactional access through the rich set of available file I/O APIs.  
  
1.  File I/O API access typically begins by acquiring a logical UNC path for the file or directory. Applications can use a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement with the [GetFileNamespacePath &#40;Transact-SQL&#41;](../../relational-databases/system-functions/getfilenamespacepath-transact-sql.md) function to obtain the logical path for the file or directory. For more information, see [Work with Directories and Paths in FileTables](../../relational-databases/blob/work-with-directories-and-paths-in-filetables.md).  
  
2.  Then the application uses this logical path to obtain a handle to the file or directory and do something with the object. The path can be passed to any supported file system API function, such as CreateFile() or CreateDirectory(), to create or open a file and obtain a handle. The handle can then be used to stream data, to enumerate or organize directories, to get or set file attributes, to delete files or directories, and so forth.  
  
##  <a name="create"></a> Creating Files and Directories in a FileTable  
 A file or directory can be created in a FileTable by calling file I/O APIs such as CreateFile or CreateDirectory.  
  
-   All creation disposition flags, share modes, and access modes are supported. This includes file creation, deletion and in-place modification. Also supported are File Namespace updates i.e. directory creation/deletion, rename and move operations.  
  
-   The creation of a new file or directory corresponds to the creation of a new row in the underlying FileTable.  
  
-   For files, the stream data is stored in the **file_stream** column; for directories, this column is null.  
  
-   For files, the **is_directory** column contains **false**. For directories, this column contains **true**.  
  
-   Sharing and concurrency of access are enforced when multiple concurrent file I/O operations or [!INCLUDE[tsql](../../includes/tsql-md.md)] operations affect the same file or directory in the hierarchy.  
  
##  <a name="read"></a> Reading Files and Directories in a FileTable  
 Read Committed isolation semantics are enforced in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for all file I/O access operations on stream and attribute data.  
  
##  <a name="write"></a> Writing and Updating Files and Directories in a FileTable  
  
-   All file I/O write or update operations on a FileTable are non-transactional. That is, no [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] transaction is bound to these operations, and no ACID guarantees are provided.  
  
-   All file I/O streaming/in-place updates are supported for the FileTable.  
  
-   Updates to the FILESTREAM data or attributes through file I/O APIs result in updates of the corresponding **file_stream** and file attribute columns in the FileTable.  
  
##  <a name="delete"></a> Deleting Files and Directories in a FileTable  
 All Windows file I/O API semantics are enforced when you delete a file or directory.  
  
-   Deleting a directory fails if the directory contains any files or subdirectories.  
  
-   Deleting a file or directory removes the corresponding row from the FileTable. This is equivalent to deleting the row through a [!INCLUDE[tsql](../../includes/tsql-md.md)] operation.  
  
##  <a name="supported"></a> Supported File System Operations  
 FileTables support the file system APIs related to the following file system operations:  
  
-   Directory Management  
  
-   File Management  
  
 FileTables do not support the following operations:  
  
-   Disk Management  
  
-   Volume Management  
  
-   Transactional NTFS  
  
##  <a name="considerations"></a> Additional Considerations for File I/O Access to FileTables  
  
###  <a name="vnn"></a> Using Virtual Network Names (VNNs) with Always On Availability Groups  
 When the database that contains FILESTREAM or FileTable data belongs to an Always On availability group, then all access to FILESTREAM or FileTable data through the file system APIs should use VNNs instead of computer names. For more information, see [FILESTREAM and FileTable with Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/filestream-and-filetable-with-always-on-availability-groups-sql-server.md).  
  
###  <a name="partial"></a> Partial Updates  
 A writable handle obtained for FILESTREAM data in a FileTable by using the [GetFileNamespacePath &#40;Transact-SQL&#41;](../../relational-databases/system-functions/getfilenamespacepath-transact-sql.md) function can be used to make in-place, partial updates to the FILESTREAM content. This behavior is different from the transacted FILESTREAM access through a handle obtained by calling **OpenSQLFILESTREAM()** and passing an explicit transaction context.  
  
###  <a name="trans"></a> Transactional Semantics  
 When you access the files in a FileTable by using file I/O APIs, these operations are not associated with any user transactions, and have the following additional characteristics:  
  
-   Since non-transacted access to FILESTREAM data in a FileTable is not associated with any transaction, it does not have any specific isolation semantics. However [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] may use internal transactions to enforce locking or concurrency semantics on the FileTable data. Any internal transactions of this type are done with read-committed isolation.  
  
-   There are no ACID guarantees for these non-transacted operations on FILESTREAM data. The consistency guarantees are similar to those for file updates made by applications in the file system.  
  
-   These changes cannot be rolled back.  
  
 However, the FILESTREAM column in a FileTable can also be accessed with transactional FILESTREAM access by calling **OpenSqlFileStream()**. This kind of access can be fully transactional and will honor all the levels of transactional consistently that are currently supported.  
  
###  <a name="concurrency"></a> Concurrency Control  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] enforces concurrency control for FileTable access among file system applications, and between file system applications and [!INCLUDE[tsql](../../includes/tsql-md.md)] applications. This concurrency control is achieved by taking appropriate locks on the FileTable rows.  
  
###  <a name="triggers"></a> Triggers  
 Creating, modifying, or deleting files or directories or their attributes through the file system results in corresponding insert, update, or delete operations in the FileTable. Any associated [!INCLUDE[tsql](../../includes/tsql-md.md)] DML triggers are fired as part of these operations.  
  
##  <a name="funclist"></a> File System Functionality Supported in FileTables  
  
|Capability|Supported|Comments|  
|----------------|---------------|--------------|  
|**Oplocks**|Yes|There is support for Level 2, Level 1, Batch and Filter oplocks.|  
|**Extended Attributes**|No||  
|**Reparse Points**|No||  
|**Persistent ACLs**|No||  
|**Named Streams**|No||  
|**Sparse Files**|Yes|Sparseness can be set only on files, and affects the storage of the data stream. Since FILESTREAM data is stored on NTFS volumes, the FileTable feature supports sparse files by forwarding the requests to the NTFS file system.|  
|**Compression**|Yes||  
|**Encryption**|Yes||  
|**TxF**|No||  
|**File Ids**|No||  
|**Object Ids**|No||  
|**Symbolic links**|No||  
|**Hard links**|No||  
|**Short names**|No||  
|**Directory change notifications**|No||  
|**Byte range locking**|Yes|Requests for byte range locking are passed to the NTFS file system.|  
|**Memory mapped files**|No||  
|**Cancel I/O**|Yes||  
|**Security**|No|Windows share level security and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table and column level security are enforced.|  
|**USN journal**|No|Metadata changes to files and directories in a FileTable are DML operations on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Therefore they are logged in the corresponding database log file. However they are not logged in the NTFS USN journal (except for changes in size).<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] change tracking capabilities can be used to capture similar information.|  
  
## See Also  
 [Load Files into FileTables](../../relational-databases/blob/load-files-into-filetables.md)   
 [Work with Directories and Paths in FileTables](../../relational-databases/blob/work-with-directories-and-paths-in-filetables.md)   
 [Access FileTables with Transact-SQL](../../relational-databases/blob/access-filetables-with-transact-sql.md)   
 [FileTable DDL, Functions, Stored Procedures, and Views](../../relational-databases/blob/filetable-ddl-functions-stored-procedures-and-views.md)  
  
  
