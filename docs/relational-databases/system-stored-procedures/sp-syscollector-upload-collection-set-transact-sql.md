---
title: "sp_syscollector_upload_collection_set (Transact-SQL)"
description: Starts an upload of collection set data if the collection set is enabled.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_upload_collection_set"
  - "sp_syscollector_upload_collection_set_TSQL"
helpviewer_keywords:
  - "sp_syscollector_upload_collection_set"
  - "data collector [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# sp_syscollector_upload_collection_set (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Starts an upload of collection set data if the collection set is enabled.

> [!IMPORTANT]  
> This stored procedure can only be used for collection sets that are configured for cached mode data collection and upload.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_upload_collection_set
    [ [ @collection_set_id = ] collection_set_id ]
    [ , [ @name = ] N'name' ]
[ ; ]
```

## Arguments

#### [ @collection_set_id = ] *collection_set_id*

The unique local identifier for the collection set. *@collection_set_id* is **int**, and must have a value if *@name* is `NULL`.

#### [ @name = ] N'*name*'

The name of the collection set. *@name* is **sysname**, and must have a value if *@collection_set_id* is `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

Either *@collection_set_id* or *@name* must have a value; both can't be `NULL`.

This procedure can be used for starting an on-demand upload for a running collection set. It can only be used for collection sets that are configured for cached mode data collection and upload. This enables a user to obtain data to analyze without having to wait for a scheduled upload.

## Permissions

Requires membership in the **dc_operator** (with EXECUTE permission) fixed database role to execute this procedure.

## Examples

Does an on-demand upload of a collection set named `Simple Collection Set`.

```sql
USE msdb;
GO
EXEC sp_syscollector_upload_collection_set @name = 'Simple Collection Set';
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Data collection](../data-collection/data-collection.md)
