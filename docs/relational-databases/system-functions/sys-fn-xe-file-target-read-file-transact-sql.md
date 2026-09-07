---
title: "sys.fn_xe_file_target_read_file (Transact-SQL)"
description: "The sys.fn_xe_file_target_read_file system function reads files created by the Extended Events asynchronous file target."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 08/24/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "fn_xe_file_target_read_file_TSQL"
  - "fn_xe_file_target_read_file"
  - "sys.fn_xe_file_target_read_file_TSQL"
  - "sys.fn_xe_file_target_read_file"
helpviewer_keywords:
  - "extended events [SQL Server], functions"
  - "fn_xe_file_target_read_file function"
  - "sys.fn_xe_file_target_read_file function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# sys.fn_xe_file_target_read_file (Transact-SQL)

[!INCLUDE [SQL Server SQL Database SQL Managed Instance-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Reads the event log XEL files created by the Extended Events `event_file` target. Each row in the result set represents an event. Event data is returned in XML format.

XEL files can also be read by [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For a walkthrough, see [Quickstart: Extended Events](../extended-events/quick-start-extended-events-in-sql-server.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.fn_xe_file_target_read_file ( path , mdpath , initial_file_name , initial_offset )
```

## Arguments

#### *path*

The path to the files to read. *path* is **nvarchar(260)** with no default.

- When used with files in the local file system, *path* must include the name of an event session log file. The file name can contain `*` as a wildcard to read data from multiple files.

- When used with blobs in an Azure Storage container, *path* is an HTTP URL constructed by the concatenation of two parts:

  1. The path to an Azure Storage storage container, followed by a slash (`/`).

  1. A common prefix for the names of blobs in the container that should be read. To read a single blob, use the full name of the blob.

  For example, using `https://<storage-account-name>.blob.core.windows.net/container-name>/xe_session` as *path* retrieves data from all blobs with names starting with `xe_session`, and regardless of the remainder of the blob name including the extension. Wildcards can't be specified.

#### *mdpath*

The path to the metadata file that corresponds to the file or files specified by the *path* argument. *mdpath* is **nvarchar(260)** with no default.

In [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and later versions, you don't need this parameter. It's retained for backward compatibility, for log files generated in previous versions of SQL Server. In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, this parameter can be given as `NULL`, as `.xem` files are no longer used.

#### *initial_file_name*

The first file to read from *path*. *initial_file_name* is **nvarchar(260)** with no default. If `NULL` is specified as the argument, all the files found in *path* are read.

The file name must be a value returned in the result set of a `sys.fn_xe_file_target_read_file` function call with the same *path*.

#### *initial_offset*

When an [event_file](../extended-events/targets-for-extended-events-in-sql-server.md#event_file-target) target writes events to an XEL file, it writes one memory buffer at a time. A buffer typically contains multiple events. The `file_offset` column in the result set of `sys.fn_xe_file_target_read_file` represents the byte offset of a buffer within an XEL file. Consequently, multiple events in the result set commonly have the same offset.

Use the *initial_offset* argument to specify the last buffer you read. When you specify this argument, the `sys.fn_xe_file_target_read_file` function skips all events in the buffer specified by the offset and the previous buffers, and returns only the events in the following buffers. In this way, you can read the contents of an XEL file incrementally at the buffer granularity.

*initial_offset* is **bigint**. If you specify `NULL` as the argument, the function reads the entire file.

> [!NOTE]  
> *initial_file_name* and *initial_offset* are paired arguments. If you specify a value for either argument, you must specify a value for the other argument.

## Table returned

| Column name | Data type | Description |
| --- | --- | --- |
| `module_guid` | **uniqueidentifier** | The event module GUID. Not nullable. |
| `package_guid` | **uniqueidentifier** | The event package GUID. Not nullable. |
| `object_name` | **nvarchar(256)** | The name of the event. Not nullable. |
| `event_data` | **nvarchar(max)** | The event contents, in XML format. Not nullable. |
| `file_name` | **nvarchar(260)** | The name of the file that contains the event. Not nullable. |
| `file_offset` | **bigint** | The offset of the buffer in the file that contains the event. Not nullable. |
| `timestamp_utc` | **datetime2(7)** | The date and time (UTC timezone) of the event. Not nullable.<br /><br />**Applies to**: [!INCLUDE [sssql17](../../includes/sssql17-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]. |

## Remarks

Reading large result sets by executing `sys.fn_xe_file_target_read_file` in [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)] might result in an error. Use the **Results to File** mode (in [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)], **Ctrl+Shift+F**) to export large result sets to a human-readable file, to read the file with another tool instead.

[!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] accept trace results generated in XEL and XEM format. [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] Extended Events only support trace results in XEL format. We recommend that you use [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)] to read trace results in XEL format.

### Azure SQL

In [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] or [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the files created by the `event_file` target are always stored as blobs in an Azure Storage container.

You can use `sys.fn_xe_file_target_read_file` to read data from these blobs if a credential allowing access to the Azure Storage container exists. For a walkthrough, review [Create an event session with an event_file target in Azure Storage](/azure/azure-sql/database/xevent-code-event-file).

You can specify a local file name to read the `system_health` session in [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] without creating a credential. For more information, see the last example in [A. Retrieve data from files in the local file system](#a-retrieve-data-from-files-in-the-local-file-system).

In all other cases, if you specify a local file system path, you receive an error message similar to:

```output
Msg 40538, Level 16, State 3, Line 15
A valid URL beginning with 'https://' is required as value for any filepath specified.
```

## Permissions

In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions, requires `VIEW SERVER STATE` permission on the server.

In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, requires `VIEW SERVER PERFORMANCE STATE` permission on the server, or `VIEW DATABASE PERFORMANCE STATE` permission on the database.

## Examples

### A. Retrieve data from files in the local file system

For [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and previous versions, the following example gets all the rows from all the files, including both the `.xel` and `.xem` file. In this example, the file targets and metafiles are located in the trace folder in the `C:\traces\` folder.

```sql
SELECT *
FROM sys.fn_xe_file_target_read_file('C:\traces\*.xel', 'C:\traces\metafile.xem', NULL, NULL);
```

In [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] and later versions, the following example retrieves events inside all `.xel` files in the default folder. The default location is `\MSSQL\Log` within the installation folder of the instance.

```sql
SELECT *
FROM sys.fn_xe_file_target_read_file('*.xel', NULL, NULL, NULL);
```

In [!INCLUDE [sssql17](../../includes/sssql17-md.md)] and later versions, the following example retrieves only data from the last day, from the built-in `system_health` session. The `system_health` session is an Extended Events session that is included by default with SQL Server. For more information, see [Use the system_health session](../extended-events/use-the-system-health-session.md).

```sql
SELECT *
FROM sys.fn_xe_file_target_read_file('system_health*.xel', NULL, NULL, NULL)
WHERE CAST (timestamp_utc AS DATETIME2 (7)) > DATEADD(DAY, -1, GETUTCDATE());
```

### B. Retrieve data from blobs in an Azure Storage container

Read data from all blobs in the container with names starting with `xe_session_`.

```sql
SELECT *
FROM sys.fn_xe_file_target_read_file(
    'https://<storage-account-name>.blob.core.windows.net/<container-name>/xe_session_',
    NULL,
    NULL,
    NULL
);
```

Read data from the `xe_session_0_133614763336380000.xel` blob.

```sql
SELECT *
FROM sys.fn_xe_file_target_read_file(
    'https://<storage-account-name>.blob.core.windows.net/<container-name>/xe_session_0_133614763336380000.xel',
    NULL,
    NULL,
    NULL
);
```

Read data from the `xe_session_0_133614763336380000.xel` blob starting with offset 33280.

```sql
SELECT *
FROM sys.fn_xe_file_target_read_file(
    'https://<storage-account-name>.blob.core.windows.net/<container-name>/xe_session_',
    NULL,
    'https://<storage-account-name>.blob.core.windows.net/<container-name>/xe_session_0_133614763336380000.xel',
    33280
);
```

## Related content

- [Extended Events Dynamic Management Views](../system-dynamic-management-objects/extended-events-dynamic-management-views.md)
- [Extended Events Catalog Views (Transact-SQL)](../system-catalog-views/extended-events-catalog-views-transact-sql.md)
- [Extended Events overview](../extended-events/extended-events.md)
- [Extended Events targets](../extended-events/targets-for-extended-events-in-sql-server.md)
- [View event data in SQL Server Management Studio](../extended-events/advanced-viewing-of-target-data-from-extended-events-in-sql-server.md)
- [Convert an Existing SQL Trace Script to an Extended Events Session](../extended-events/convert-an-existing-sql-trace-script-to-an-extended-events-session.md)
- [Use the system_health session](../extended-events/use-the-system-health-session.md)
