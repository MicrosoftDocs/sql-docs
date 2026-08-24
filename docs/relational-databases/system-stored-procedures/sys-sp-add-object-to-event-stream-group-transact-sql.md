---
title: "sys.sp_add_object_to_event_stream_group (Transact-SQL)"
description: sys.sp_add_object_to_event_stream_group adds an object to a stream group for the change event streaming feature.
author: nzagorac-ms
ms.author: nzagorac
ms.reviewer: mathoma,randolphwest
ms.date: 06/19/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys_sp_add_object_to_event_stream_group_TSQL"
  - "sys_sp_add_object_to_event_stream_group"
helpviewer_keywords:
  - "sys_sp_add_object_to_event_stream_group"
dev_langs:
  - "TSQL"
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17"
---
# sys.sp_add_object_to_event_stream_group (Transact-SQL)

[!INCLUDE [sqlserver2025](../../includes/applies-to-version/sqlserver2025-asdb.md)]

Adds an object (that is, a table) to the stream group for the [change event streaming (CES)](../track-changes/change-event-streaming/overview.md) feature introduced in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and Azure SQL Database.

[!INCLUDE [change-event-streaming-preview](../../includes/change-event-streaming-preview.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_add_object_to_event_stream_group
    [ @stream_group_name = ] N'stream_group_name'
    , [ @object_name = ] N'schema_name_dot_object_name'
    [ , [ @include_all_columns = ] include_all_columns ]
    [ , [ @include_old_values = ] include_old_values ]
    [ , [ @include_old_lob_values = ] include_old_lob_values ]
[ ; ]
```

## Arguments

#### [ @stream_group_name = ] N'*stream_group_name*'

Specifies the name of the event stream group you want to add the table to. *@stream_group_name* is **sysname**, with no default, and can't be `NULL`.

#### [ @object_name = ] N'*schema_name_dot_object_name*'

Specifies the name of the table you want to stream as part of the specified stream group. *@object_name* is **nvarchar(512)**, with no default, and can't be `NULL`.

> [!NOTE]
> Only tables are currently supported. Configuring CES on views results in an error.

Use a two-part name for the column that includes both the schema name and column name. For example, a valid value is `dbo.Addresses`.

#### [ @include_all_columns = ] *include_all_columns*

Defines what columns to include within the payload of the message that streams the changes made to the specified table.

*@include_all_columns* is **bit**, and must be one of the following values:

| Value | Description |
| --- | --- |
| `1` (default) | Includes all columns within the payload of the message that streams the changes made to the specified table. |
| `0` | Only includes columns that have changed within the payload of the message that streams the changes made to the specified table. |

#### [ @include_old_values = ] *include_old_values*

If specified, *@include_old_values* determines whether old values are included within the payload of the message that streams the changes made to the specified table.

*@include_old_values* is **bit**, and must be one of the following values:

| Value | Description |
| --- | --- |
| `1` (default) | Includes old values within the payload of the message that streams the changes made to the specified table. |
| `0` | Skips old values within the payload of the message that streams the changes made to the specified table. |

Old values are the values of the column before the change was made. For example, if a row is updated, the old values are the values of the columns before the update.

#### [ @include_old_lob_values = ] *include_old_lob_values*

Determines if old changed large objects (LOB) values are included within the payload of the message that streams the changes made to the specified table.

*@include_old_lob_values* is **bit**, and must be one of the following values:

| Value | Description |
| --- | --- |
| `1` | Includes all columns within the payload of the message that streams the changes made to the specified table. |
| `0` (default) | Only includes columns that have changed within the payload of the message that streams the changes made to the specified table. |

Old values are the values of the column before the change was made. For example, if a row is updated, the old values are the values of the columns before the update.

## Return code values

`0` (success) or `1` (failure).

## Permissions

A user with `CONTROL` database permissions, **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Related content

- [What is change event streaming (preview)?](../track-changes/change-event-streaming/overview.md)
- [Configure change event streaming (preview) to Azure Event Hubs](../track-changes/change-event-streaming/configure.md)
