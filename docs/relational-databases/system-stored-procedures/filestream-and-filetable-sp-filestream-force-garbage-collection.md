---
title: "sp_filestream_force_garbage_collection (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/22/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_filestream_force_garbage_collection"
  - "sp_filestream_force_garbage_collection_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "FILESTREAM [SQL Server]"
  - "sp_filestream_force_garbage_collection"
ms.assetid: 9d1efde6-8fa4-42ac-80e5-37456ffebd0b
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_filestream_force_garbage_collection (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Forces the FILESTREAM garbage collector to run, deleting any unneeded FILESTREAM files.  
  
 A FILESTREAM container cannot be removed until all the deleted files within it have been cleaned up by the garbage collector. The FILESTREAM garbage collector runs automatically. However, if you need to remove a container before the garbage collector has run, you can use sp_filestream_force_garbage_collection to run the garbage collector manually.  
  
  
## Syntax  
  
```  
sp_filestream_force_garbage_collection  
    [ @dbname = ]  'database_name',  
    [ @filename = ] 'logical_file_name' ]  
```  
  
## Arguments  
 `[ @dbname = ]  'database_name'`
 Signifies the name of the database to run the garbage collector on.  
  
> [!NOTE]  
>  `dbname` is **sysname**. If not specified, current database is assumed.  
  
 `[ @filename = ] 'logical_file_name' ]`  
 Specifies the logical name of the FILESTREAM container to run the garbage collector on. `@filename` is optional. If no logical filename is specified, the garbage collector cleans all FILESTREAM containers in the specified database.  
  
## Return Code Values  
  
|||  
|-|-|  
|Value|Description|  
|0|Operation success|  
|1|Operation failure|  
  
## Result Sets  
  
|Value|Description|  
|-----------|-----------------|  
|*file_name*|Indicates the FILESTREAM container name|  
|*num_collected_items*|Indicates the number of FILESTREAM items (files/directories) that have been garbage collected (deleted) in this container.|  
|*num_marked_for_collection_items*|Indicates the number of FILESTREAM items (files/directories) that have been marked for garbage collection in this container. These items have not been deleted yet, but may be eligible for deletion following the garbage collection phase.|  
|*num_unprocessed_items*|Indicates the number of eligible FILESTREAM items (files or directories) that were not processed for garbage collection in this FILESTREAM container. Items may be unprocessed for various reasons, including the following:<br /><br /> Files that need to be pinned down because Log backup or CheckPoint has not been taken.<br /><br /> Files in the FULL or BULK_LOGGED recovery model.<br /><br /> There is a long-running active transaction.<br /><br /> The replication log reader job has not run. See the white paper [FILESTREAM Storage in SQL Server 2008](https://go.microsoft.com/fwlink/?LinkId=209156) for more information.|  
|*last_collected_xact_seqno*|Returns the last corresponding log sequence number (LSN) up to which the files have been garbage collected for the specified FILESTREAM container.|  
  
## Remarks  
 Explicitly runs FILESTREAM Garbage Collector task to completion on the requested database (and FILESTREAM container). Files that are no longer needed are removed by the garbage collection process. The time needed for this operation to complete depends on the size of the FILESTREAM data in that database or container as well as the amount of DML activity that has recently occurred on the FILESTREAM data. Though this operation can be run with the database online, this may affect the performance of the database during its run due to various I/O activities done by the garbage collection process.  
  
> [!NOTE]  
>  It is recommended that this operation be run only when necessary and outside usual operation hours.  
  
Multiple invocations of this stored procedure can be run simultaneously only on separate containers or separate databases.  

Due to 2-phase operations, the stored procedure should be run twice to actually delete underlying Filestream files.  

Garbage Collection (GC) relies on log truncation. Therefore, if files were deleted recently on a database using Full Recovery model, they are GC-ed only after a log backup of those transaction log portions is taken and the log portion is marked inactive. On a database using Simple recovery model, a log truncation occurs after a `CHECKPOINT` has been issued against the database.  


## Permissions  
 Requires membership in the db_owner database role.  
  
## Examples  
 The following examples run the garbage collector for FILESTREAM containers in the `FSDB` database.  
  
### A. Specifying no container  
  
```sql  
USE FSDB;  
GO  
EXEC sp_filestream_force_garbage_collection @dbname = N'FSDB';  
```  
  
### B. Specifying a container  
  
```sql  
USE FSDB;  
GO  
EXEC sp_filestream_force_garbage_collection @dbname = N'FSDB',
    @filename = N'FSContainer';  
```  
  
## See Also  
[Filestream](../../relational-databases/blob/filestream-sql-server.md)
<br>[Filetables](../../relational-databases/blob/filetables-sql-server.md)
<br>[Filestream and FileTable Dynamic Management Views (Transact-SQL)](../system-dynamic-management-views/filestream-and-filetable-dynamic-management-views-transact-sql.md)
<br>[Filestream and FileTable Catalog Views (Transact-SQL)](../system-catalog-views/filestream-and-filetable-catalog-views-transact-sql.md)
<br>[sp_kill_filestream_non_transacted_handles (Transact-SQL)](filestream-and-filetable-sp-kill-filestream-non-transacted-handles.md)
  
  
