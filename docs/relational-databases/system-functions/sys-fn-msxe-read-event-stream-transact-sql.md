---
title: "sys.fn_MSxe_read_event_stream (Transact-SQL)"
description: "sys.fn_MSxe_read_event_stream (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 09/10/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "fn_MSxe_read_event_stream_TSQL"
  - "sys.fn_MSxe_read_event_stream_TSQL"
  - "sys.fn_MSxe_read_event_stream"
  - "fn_MSxe_read_event_stream"
helpviewer_keywords:
  - "sys.fn_MSxe_read_event_stream"
  - "fn_MSxe_read_event_stream"
dev_langs:
  - "TSQL"
---

# sys.fn_MSxe_read_event_stream (Transact-SQL)

[!INCLUDE [SQL Server SQL Database SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

`sys.fn_MSxe_read_event_stream` returns binary data for internal use by the [QueryableXEventData](/dotnet/api/microsoft.sqlserver.xevent.linq.queryablexeventdata) .NET class. Extended Events UI in the [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS) uses this class to read event session data.

To view human-readable event data, use one of the following instead of calling `sys.fn_MSxe_read_event_stream` directly:

- Extended Events UI in SSMS.
- [sys.fn_xe_file_target_read_file](sys-fn-xe-file-target-read-file-transact-sql.md) table-valued function.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.fn_MSxe_read_event_stream ( source , source_option )
```

## Arguments

#### *source*

The specific source of event data returned by the function. *Source* is **nvarchar(260)** with no default.

*Source* is interpreted differently depending on the value of *source_option*:

| *source_option* | Interpretation of *source* |
| --- | --- |
| `0` | *Source* is the name of a started event session. |
| `1` | *Source* is the path to the event session log files to read.<br /><br />When used with files in the local file system, *source* must include the name of an event session log file and can contain `*` as a wildcard.<br /><br />When used with blobs in an Azure Storage container, *source* is an HTTP URL constructed by the concatenation of two parts:<br /><br />1. The path to an Azure Storage storage container, followed by a slash (`/`).<br />2. A common prefix for the names of blobs in the container that should be read.<br /><br />For example, using `https://<storage-account-name>.blob.core.windows.net/container-name>/xe_session` as *source* retrieves data from all blobs with names starting with `xe_session`, and regardless of the remainder of the blob name including the extension. Wildcards can't be specified. A credential allowing access to the Azure Storage container must exist. |

#### *source_option*

The option that determines the type of the event data source. Possible values are:

| Value | Description |
| --- | --- |
| `0` | Returns event data from the [event_stream](../extended-events/targets-for-extended-events-in-sql-server.md#event_stream-target) target of a started event session. The function executes indefinitely, returning new event data as the session produces it. |
| `1` | Returns event data from the extended event log files specified by *source*. |

## Table returned

| Column name | Data type | Description |
| --- | --- | --- |
| type | **int** | The event type. Not nullable. |
| data | **image** | Binary event data. Is nullable. |

## Related content

- [Extended Events Dynamic Management Views](../system-dynamic-management-objects/extended-events-dynamic-management-views.md)
- [Extended Events Catalog Views (Transact-SQL)](../system-catalog-views/extended-events-catalog-views-transact-sql.md)
- [Extended Events overview](../extended-events/extended-events.md)
