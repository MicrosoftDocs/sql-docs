---
title: "sys.sp_remove_object_from_event_stream_group (Transact-SQL)"
description: sys.sp_remove_object_from_event_stream_group removes a table from a stream group for the change event streaming feature.
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
  - "sys_sp_remove_object_from_event_stream_group_TSQL"
  - "sys_sp_remove_object_from_event_stream_group"
helpviewer_keywords:
  - "sys_sp_remove_object_from_event_stream_group"
dev_langs:
  - "TSQL"
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17"
---
# sys.sp_remove_object_from_event_stream_group (Transact-SQL)

[!INCLUDE [sqlserver2025](../../includes/applies-to-version/sqlserver2025-asdb.md)]

Removes an object (that is, a table) from the stream group for the [change event streaming (CES)](../track-changes/change-event-streaming/overview.md) feature introduced in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and Azure SQL Database.

[!INCLUDE [change-event-streaming-preview](../../includes/change-event-streaming-preview.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_remove_object_from_event_stream_group
    [ @stream_group_name = ] N'stream_group_name'
    , [ @object_name = ] N'schema_name_dot_object_name'
[ ; ]
```

## Arguments

#### [ @stream_group_name = ] N'*stream_group_name*'

Specifies the name of the event stream group from which you want to remove the table. *@stream_group_name* is **sysname**, with no default, and can't be `NULL`

#### [ @object_name = ] N'*schema_name_dot_object_name*'

Specifies the name of the table you want to remove from the specified stream group. *@object_name* is **nvarchar(512)**, with no default, and can't be `NULL`

## Return code values

`0` (success) or `1` (failure).

## Permissions

A user with `CONTROL` database permissions, **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Related content

- [What is change event streaming (preview)?](../track-changes/change-event-streaming/overview.md)
- [Configure change event streaming (preview) to Azure Event Hubs](../track-changes/change-event-streaming/configure.md)
