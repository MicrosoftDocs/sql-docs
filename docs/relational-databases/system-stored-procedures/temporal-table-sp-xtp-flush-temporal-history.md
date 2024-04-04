---
title: "sp_xtp_flush_temporal_history"
description: "Invokes the data flush task to move all committed rows from in-memory staging table to the disk-based history table."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: conceptual
f1_keywords:
  - "sp_xtp_flush_temporal_history"
  - "sp_xtp_flush_temporal_history_TSQL"
  - "sys.sp_xtp_flush_temporal_history"
  - "sys.sp_xtp_flush_temporal_history_TSQL"
helpviewer_keywords:
  - "sp_xtp_flush_temporal_history"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_xtp_flush_temporal_history (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Invokes the data flush task to move all committed rows from in-memory staging table to the disk-based history table.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_xtp_flush_temporal_history
    [ @schema_name = ] N'schema_name'
    , [ @object_name = ] N'object_name'
```

## Arguments

#### [ @schema_name = ] N'*schema_name*'

The schema name for the current or temporal table.

#### [ @object_name = ] N'*object_name*'

The name of the current or temporal table.

## Return code values

`0` (success) or `> 0` (failure).

## Permissions

Requires **db_owner** permissions.

## Related content

- [Performance Considerations with Memory-Optimized System-Versioned Temporal Tables](../tables/memory-optimized-system-versioned-temporal-tables-performance.md)
