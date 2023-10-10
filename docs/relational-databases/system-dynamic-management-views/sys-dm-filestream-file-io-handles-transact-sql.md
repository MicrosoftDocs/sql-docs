---
title: "sys.dm_filestream_file_io_handles (Transact-SQL)"
description: sys.dm_filestream_file_io_handles (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_filestream_file_io_handles"
  - "sys.dm_filestream_file_io_handles"
  - "dm_filestream_file_io_handles_TSQL"
  - "sys.dm_filestream_file_io_handles_TSQL"
helpviewer_keywords:
  - "sys.dm_filestream_file_io_handle catalog view"
dev_langs:
  - "TSQL"
---
# sys.dm_filestream_file_io_handles (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Displays the file handles that the Namespace Owner (NSO) knows about. `sys.dm_filestream_file_io_handles` displays FILESTREAM handles that a client got using `OpenSqlFilestream`.

| Column | Type | Description |
| --- | --- | --- |
| `handle_context_address` | **varbinary(8)** | Shows the address of the internal NSO structure associated with the client's handle. Nullable. |
| `creation_request_id` | **int** | Shows a field from the `REQ_PRE_CREATE` I/O request used to create this handle. Not nullable. |
| `creation_irp_id` | **int** | Shows a field from the `REQ_PRE_CREATE` I/O request used to create this handle. Not nullable. |
| `handle_id` | **int** | Shows the unique ID of this handle that is assigned by the driver. Not nullable. |
| `creation_client_thread_id` | **varbinary(8)** | Shows a field from the `REQ_PRE_CREATE` I/O request used to create this handle. Nullable. |
| `creation_client_process_id` | **varbinary(8)** | Shows a field from the `REQ_PRE_CREATE` I/O request used to create this handle. Nullable. |
| `filestream_transaction_id` | **varbinary(128)** | Shows the ID of the transaction associated with the given handle. This is the value returned by the     `get_filestream_transaction_context` function. Use this field to join to the `sys.dm_filestream_file_io_requests` view. Nullable. |
| `access_type` | **nvarchar(60)** | Not nullable. |
| `logical_path` | **nvarchar(256)** | Shows the logical pathname of the file that this handle opened. This is the same pathname that is returned by the `.PathName` method of **varbinary(max)** FILESTREAM. Nullable. |
| `physical_path` | **nvarchar(256)** | Shows the actual NTFS pathname of the file. This is the same pathname returned by the `.PhysicalPathName` method of the **varbinary**(**max**) FILESTREAM. Enabled by Trace Flag 5556. Nullable. |

## Permissions

For [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions, requires VIEW SERVER STATE permission on the server.

For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Related content

- [FILESTREAM and FileTable Dynamic Management Views (Transact-SQL)](filestream-and-filetable-dynamic-management-views-transact-sql.md)
