---
title: "sys.dm_filestream_file_io_requests (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_filestream_file_io_requests"
  - "dm_filestream_file_io_requests"
  - "sys.dm_filestream_file_io_requests_TSQL"
  - "dm_filestream_file_io_requests_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_filestream_file_io_requests catalog view"
ms.assetid: d41e39a5-14d5-4f3d-a2e3-a822b454c1ed
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_filestream_file_io_requests (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays a list of I/O requests being processed by the Namespace Owner (NSO) at the given moment.  
  
|Column|Type|Description|  
|------------|----------|-----------------|  
|**request_context_address**|**varbinary(8)**|Shows the internal address of the NSO memory block that contains the I/O request from the driver. Is not nullable.|  
|**current_spid**|**smallint**|Shows the system process id (SPID) for the current SQL Server's connection. Is not nullable.|  
|**request_type**|**nvarchar(60)**|Shows the I/O request packet (IRP) type. The possible request types are REQ_PRE_CREATE, REQ_POST_CREATE, REQ_RESOLVE_VOLUME, REQ_GET_VOLUME_INFO, REQ_GET_LOGICAL_NAME, REQ_GET_PHYSICAL_NAME, REQ_PRE_CLEANUP, REQ_POST_CLEANUP, REQ_CLOSE, REQ_FSCTL, REQ_QUERY_INFO, REQ_SET_INFO, REQ_ENUM_DIRECTORY, REQ_QUERY_SECURITY, and REQ_SET_SECURITY. Is not nullable|  
|**request_state**|**nvarchar(60)**|Shows the state of the I/O request in NSO. Possible values are REQ_STATE_RECEIVED, REQ_STATE_INITIALIZED, REQ_STATE_ENQUEUED, REQ_STATE_PROCESSING, REQ_STATE_FORMATTING_RESPONSE, REQ_STATE_SENDING_RESPONSE, REQ_STATE_COMPLETING, and REQ_STATE_COMPLETED. Is not nullable.|  
|**request_id**|**int**|Shows the unique request ID assigned by the driver to this request. Is not nullable.|  
|**irp_id**|**int**|Shows the unique IRP ID. This is useful for identifying all I/O requests related to the given IRP. Is not nullable.|  
|**handle_id**|**int**|Indicated the namespace handle ID. This is the NSO specific identifier and is unique across an instance. Is not nullable.|  
|**client_thread_id**|**varbinary(8)**|Shows the client application's thread ID that originates the request.<br /><br /> **\*\* Warning \*\*** This is meaningful only if the client application is running on the same machine as SQL Server. When the client application is running remotely, the **client_thread_id** shows the thread ID of some system process that works on behalf of the remote client.<br /><br /> Is nullable.|  
|**client_process_id**|**varbinary(8)**|Shows the process ID of the client application if the client application runs on the same machine as SQL Server. For a remote client, this shows the system process ID that is working on behalf of the client application. Is nullable.|  
|**handle_context_address**|**varbinary(8)**|Shows the address of the internal NSO structure associated with the client's handle. Is nullable.|  
|**filestream_transaction_id**|**varbinary(128)**|Shows the ID of the transaction associated with the given handle and all the requests associated with this handle. It is the value returned by the **get_filestream_transaction_context** function. Is nullable.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Filestream and FileTable Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/filestream-and-filetable-dynamic-management-views-transact-sql.md)  
  
  
