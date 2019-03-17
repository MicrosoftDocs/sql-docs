---
title: "sys.dm_filestream_non_transacted_handles (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_filestream_non_transacted_handles_TSQL"
  - "dm_filestream_non_transacted_handles"
  - "dm_filestream_non_transacted_handles_TSQL"
  - "sys.dm_filestream_non_transacted_handles"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_filestream_non_transacted_handles dynamic management view"
ms.assetid: 507ec125-67dc-450a-9081-94cde5444a92
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_filestream_non_transacted_handles (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays the currently open non-transactional file handles associated with FileTable data.  
  
 This view contains one row per open file handle. Because the data in this view corresponds to the live internal state of the server, the data is constantly changing as handles are opened and closed. This view does not contain historical information.  
  
 For more information, see [Manage FileTables](../../relational-databases/blob/manage-filetables.md).  
  
|**Column**|**Type**|**Description**|  
|----------------|--------------|---------------------|  
|database_id|int|ID of the database associated with the handle.|  
|object_id|int|Object ID of the FileTable the handle is associated with.|  
|handle_id|int|Unique handle context identifier. Used by the [sp_kill_filestream_non_transacted_handles &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/filestream-and-filetable-sp-kill-filestream-non-transacted-handles.md) stored procedure to kill a specific handle.|  
|file_object_type|int|Type of the handle. This indicates the level of the hierarchy the handle was opened against, ie. database or item.|  
|file_object_type_desc|nvarchar(120)|"UNDEFINED",<br />"SERVER_ROOT",<br />"DATABASE_ROOT",<br />"TABLE_ROOT",<br />"TABLE_ITEM"|  
|correlation_process_id|varbinary(8)|Contains a unique identifier for the process that originated the request.|  
|correlation_thread_id|varbinary(8)|Contains a unique identifier for the thread that originated the request.|  
|file_context|varbinary(8)|Pointer to the file object used by this handle.|  
|state|int|Current state of the handle. May be active, closed or killed.|  
|state_desc|nvarchar(120)|"ACTIVE",<br />"CLOSED",<br />"KILLED"|  
|current_workitem_type|int|State this handle is currently being processed by.|  
|current_workitem_type_desc|nvarchar(120)|"NoSetWorkItemType",<br />"FFtPreCreateWorkitem",<br />"FFtGetPhysicalFileNameWorkitem",<br />"FFtPostCreateWorkitem",<br />"FFtPreCleanupWorkitem",<br />"FFtPostCleanupWorkitem",<br />"FFtPreCloseWorkitem",<br />"FFtQueryDirectoryWorkItem",<br />"FFtQueryInfoWorkItem",<br />"FFtQueryVolumeInfoWorkItem",<br />"FFtSetInfoWorkitem",<br />"FFtWriteCompletionWorkitem"|  
|fcb_id|bigint|FileTable File Control Block ID.|  
|item_id|varbinary(892)|The Item ID for a file or directory. May be null for server root handles.|  
|is_directory|bit|Is this a directory.|  
|item_name|nvarchar(512)|Name of the item.|  
|opened_file_name|nvarchar(512)|Originally requested path to be opened.|  
|database_directory_name|nvarchar(512)|Portion of the opened_file_name that represents the database directory name.|  
|table_directory_name|nvarchar(512)|Portion of the opened_file_name that represents the table directory name.|  
|remaining_file_name|nvarchar(512)|Portion of the opened_file_name that represents the remaining directory name.|  
|open_time|datetime|Time the handle was opened.|  
|flags|int|ShareFlagsUpdatedToFcb = 0x1,<br />DeleteOnClose = 0x2,<br />NewFile = 0x4,<br />PostCreateDoneForNewFile = 0x8,<br />StreamFileOverwritten = 0x10,<br />RequestCancelled = 0x20,<br />NewFileCreationRolledBack = 0x40|  
|login_id|int|ID of the principal that opened the handle.|  
|login_name|nvarchar(512)|Name of the principal that opened the handle.|  
|login_sid|varbinary(85)|SID of the principal that opened the handle.|  
|read_access|bit|Opened for read access.|  
|write_access|bit|Opened for write access.|  
|delete_access|bit|Opened for delete access.|  
|share_read|bit|Opened with share_read allowed.|  
|share_write|bit|Opened with share_write allowed.|  
|share_delete|bit|Opened with share_delete allowed.|  
  
## See Also  
 [Manage FileTables](../../relational-databases/blob/manage-filetables.md)  
  
  
