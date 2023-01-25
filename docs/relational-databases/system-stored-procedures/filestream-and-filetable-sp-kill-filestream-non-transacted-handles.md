---
description: "sp_kill_filestream_non_transacted_handles (Transact-SQL)"
title: "sp_kill_filestream_non_transacted_handles (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_kill_filestream_non_transacted_handles_TSQL"
  - "sp_kill_filestream_non_transacted_handles"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_kill_filestream_non_transacted_handles"
ms.assetid: 7188353e-ab29-49a0-8f25-7fb8ab122589
author: markingmyname
ms.author: maghan
---
# sp_kill_filestream_non_transacted_handles (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Closes non-transactional file handles to FileTable data.  
  
## Syntax  
  
```sql  
sp_kill_filestream_non_transacted_handles [[ @table_name = ] 'table_name', [[ @handle_id = ] @handle_id]]  
```  
  
## Arguments  
 *table_name*  
 The name of the table in which to close non-transactional handles.  
  
 You can pass *table_name* without *handle_id* to close all open non-transactional handles for the FileTable.  
  
 You can pass NULL for the value of *table_name* to close all open non-transactional handles for all FileTables in the current database. NULL is the default value.  
  
 *handle_id*  
 The optional ID of the individual handle to be closed. You can get the *handle_id* from the [sys.dm_filestream_non_transacted_handles &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-filestream-non-transacted-handles-transact-sql.md) dynamic management view. Each ID is unique in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. If you specify *handle_id*, then you also have to provide a value for *table_name*.  
  
 You can pass NULL for the value of *handle_id* to close all open non-transactional handles for the FileTable specified by *table_name*. NULL is the default value.  
  
## Return Code Value  
 **0** (success) or **1** (failure)  
  
## Result Set  
 None.  
  
## General Remarks  
 The *handle_id* required by **sp_kill_filestream_non_transacted_handles** is not related to the session_id or unit of work that is used in other **kill** commands.  
  
 For more information, see [Manage FileTables](../../relational-databases/blob/manage-filetables.md).  
  
## Metadata  
 For information about open non-transactional file handles, query the dynamic management view [sys.dm_filestream_non_transacted_handles &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-filestream-non-transacted-handles-transact-sql.md).  
  
## Security  
  
### Permissions  
 You must have **VIEW DATABASE STATE** permission to get file handles from the **sys.dm_FILESTREAM_non_transacted_handles** dynamic management view and to run **sp_kill_filestream_non_transacted_handles**.  
  
## Examples  
 The following examples show how to call **sp_kill_filestream_non_transacted_handles** to close non-transactional file handles for FileTable data.  
  
```sql  
-- Close all open handles in the current database.  
sp_kill_filestream_non_transacted_handles  
  
-- Close all open handles in myFileTable.  
sp_kill_filestream_non_transacted_handles @table_name = 'myFileTable'  
  
-- Close a specific handle in myFileTable.  
sp_kill_filestream_non_transacted_handles @table_name = 'myFileTable', @handle_id = 0xFFFAAADD  
```  
  
 The following example shows how to use a script to get a *handle_id* and close it.  
  
```sql  
DECLARE @handle_id varbinary(16);  
DECLARE @table_name sysname;  
  
SELECT TOP 1 @handle_id = handle_id, @table_name = Object_name(table_id)  
FROM sys.dm_FILESTREAM_non_transacted_handles;  
  
EXEC sp_kill_filestream_non_transacted_handles @dbname, @table_name, @handle_id;  
GO  
```  
  
## See Also  
 [Manage FileTables](../../relational-databases/blob/manage-filetables.md)  
 [Filestream and FileTable Dynamic Management Views (Transact-SQL)](../system-dynamic-management-views/filestream-and-filetable-dynamic-management-views-transact-sql.md)
 <br>[Filestream and FileTable Catalog Views (Transact-SQL)](../system-catalog-views/filestream-and-filetable-catalog-views-transact-sql.md)
 <br>[sp_filestream_force_garbage_collection (Transact-SQL)](filestream-and-filetable-sp-filestream-force-garbage-collection.md)
  
