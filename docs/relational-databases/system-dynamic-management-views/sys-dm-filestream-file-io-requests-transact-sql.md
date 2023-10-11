---
title: "sys.dm_filestream_file_io_requests (Transact-SQL)"
description: sys.dm_filestream_file_io_requests (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_filestream_file_io_requests"
  - "dm_filestream_file_io_requests"
  - "sys.dm_filestream_file_io_requests_TSQL"
  - "dm_filestream_file_io_requests_TSQL"
helpviewer_keywords:
  - "sys.dm_filestream_file_io_requests catalog view"
dev_langs:
  - "TSQL"
---
# sys.dm_filestream_file_io_requests (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Displays a list of I/O requests processed by the Namespace Owner (NSO) at a given moment.

| Column | Type | Description |
| --- | --- | --- |
| `request_context_address` | **varbinary(8)** | Shows the internal address of the NSO memory block that contains the I/O request from the driver. Not nullable. |
| `current_spid` | **smallint** | Shows the system process ID (SPID) for the current SQL Server's connection. Not nullable. |
| `request_type` | **nvarchar(60)** | Shows the I/O request packet (IRP) type. The possible request types are `REQ_PRE_CREATE`, `REQ_POST_CREATE`, `REQ_RESOLVE_VOLUME`, `REQ_GET_VOLUME_INFO`, `REQ_GET_LOGICAL_NAME`, `REQ_GET_PHYSICAL_NAME`, `REQ_PRE_CLEANUP`, `REQ_POST_CLEANUP`, `REQ_CLOSE`, `REQ_FSCTL`, `REQ_QUERY_INFO`, `REQ_SET_INFO`, `REQ_ENUM_DIRECTORY`, `REQ_QUERY_SECURITY`, and `REQ_SET_SECURITY`. Not nullable. |
| `request_state` | **nvarchar(60)** | Shows the state of the I/O request in NSO. Possible values are `REQ_STATE_RECEIVED`, `REQ_STATE_INITIALIZED`, `REQ_STATE_ENQUEUED`, `REQ_STATE_PROCESSING`, `REQ_STATE_FORMATTING_RESPONSE`, `REQ_STATE_SENDING_RESPONSE`, `REQ_STATE_COMPLETING`, and `REQ_STATE_COMPLETED`. Not nullable. |
| `request_id` | **int** | Shows the unique request ID assigned by the driver to this request. Not nullable. |
| `irp_id` | **int** | Shows the unique IRP ID. This is useful for identifying all I/O requests related to the given IRP. Not nullable. |
| `handle_id` | **int** | Indicated the namespace handle ID. This is the NSO specific identifier and is unique across an instance. Not nullable. |
| `client_thread_id` | **varbinary(8)** | Shows the client application's thread ID that originates the request.<br /><br />**Warning:** This is meaningful only if the client application is running on the same machine as SQL Server. When the client application is running remotely, the `client_thread_id` shows the thread ID of some system process that works on behalf of the remote client.<br /><br />Nullable. |
| `client_process_id` | **varbinary(8)** | Shows the process ID of the client application if the client application runs on the same machine as SQL Server. For a remote client, this shows the system process ID that is working on behalf of the client application. Nullable. |
| `handle_context_address` | **varbinary(8)** | Shows the address of the internal NSO structure associated with the client's handle. Nullable. |
| `filestream_transaction_id` | **varbinary(128)** | Shows the ID of the transaction associated with the given handle and all the requests associated with this handle. It is the value returned by the `get_filestream_transaction_context` function. Nullable. |

## Permissions

For [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions, requires VIEW SERVER STATE permission on the server.

For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Related content

- [FILESTREAM and FileTable Dynamic Management Views (Transact-SQL)](filestream-and-filetable-dynamic-management-views-transact-sql.md)
