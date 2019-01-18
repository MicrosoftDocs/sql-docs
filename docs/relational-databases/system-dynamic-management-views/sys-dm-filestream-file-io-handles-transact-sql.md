---
title: "sys.dm_filestream_file_io_handles (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_filestream_file_io_handles"
  - "sys.dm_filestream_file_io_handles"
  - "dm_filestream_file_io_handles_TSQL"
  - "sys.dm_filestream_file_io_handles_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_filestream_file_io_handle catalog view"
ms.assetid: e59632f4-3292-419f-9217-ca375749f1a5
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_filestream_file_io_handles (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays the file handles that the Namespace Owner (NSO) knows about. Filestream handles that a client got using **OpenSqlFilestream** are displayed by this view.  
  
|Column|Type|Description|  
|------------|----------|-----------------|  
|**handle_context_address**|**varbinary(8)**|Shows the address of the internal NSO structure associated with the client's handle. Is nullable.|  
|**creation_request_id**|**int**|Shows a field from the REQ_PRE_CREATE I/O request used to create this handle. Is not nullable.|  
|**creation_irp_id**|**int**|Shows a field from the REQ_PRE_CREATE I/O request used to create this handle. Is not nullable|  
|**handle_id**|**int**|Shows the unique ID of this handle that is assigned by the driver. Is not nullable.|  
|**creation_client_thread_id**|**varbinary(8)**|Shows a field from the REQ_PRE_CREATE I/O request used to create this handle. Is nullable.|  
|**creation_client_process_id**|**varbinary(8)**|Shows a field from the REQ_PRE_CREATE I/O request used to create this handle. Is nullable.|  
|**filestream_transaction_id**|**varbinary(128)**|Shows the ID of the transaction associated with the given handle. This is the value returned by the **get_filestream_transaction_context** function. Use this field to join to the **sys.dm_filestream_file_io_requests** view. Is nullable.|  
|**access_type**|**nvarchar(60)**|Is not nullable.|  
|**logical_path**|**nvarchar(256)**|Shows the logical pathname of the file that this handle opened. This is the same pathname that is returned by the **.PathName** method of **varbinary**(**max**) filestream. Is nullable.|  
|**physical_path**|**nvarchar(256)**|Shows the actual NTFS pathname of the file. This is the same pathname returned by the **.PhysicalPathName** method of the **varbinary**(**max**) filestream. It is enabled by trace flag 5556. Is nullable.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Filestream and FileTable Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/filestream-and-filetable-dynamic-management-views-transact-sql.md)  
  
  
