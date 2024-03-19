---
title: "sys.fn_has_change_feed_permissions (Transact-SQL)"
description: "The internal sys.fn_has_change_feed_permissions system function checks for permissions when enabling change feed publishing."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala
ms.date: 03/18/2024
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.fn_has_change_feed_permissions_TSQL"
  - "sys.fn_has_change_feed_permissions"
  - "fn_has_change_feed_permissions_TSQL"
  - "fn_has_change_feed_permissions"
helpviewer_keywords:
  - "fn_has_change_feed_permissions"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||=azuresqldb-current||=fabric||=azure-sqldw-latest"
---
# sys.fn_has_change_feed_permissions (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asa-fabric](../../includes/applies-to-version/sqlserver2022-asdb-asa-fabric.md)]

Internal procedure that checks for permissions when enabling change feed publishing.

> [!NOTE]
> This system function is used internally and is not recommended for direct administrative use. Use Synapse Studio or the Fabric portal instead.

This system function is used for:

- The Azure Synapse Link feature for SQL Server instances and Azure SQL Database. For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).
- The Fabric Mirrored Database feature for Azure SQL Database. For more information, see [Microsoft Fabric mirrored databases (Preview)](/fabric/database/mirrored-database/overview).

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sys.fn_has_change_feed_permissions (@destination_type int);
```

## Arguments

#### destination_type

Int. `0` = Azure Synapse Link. `2` = Fabric mirroring. Default is `2`.

## Result set

`0` (success) or `1` (failure).

## Permissions

Only **public** role membership is required to query this function.

## Related content

- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-views/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-views/sys-dm-change-feed-errors.md)

**For Microsoft Fabric mirrored databases**:

- [Microsoft Fabric mirrored databases (Preview)](/fabric/database/mirrored-database/overview)
- [Microsoft Fabric mirrored databases monitoring](/fabric/database/mirrored-database/monitor)
- [Explore data in your Mirrored database using Microsoft Fabric](/fabric/database/mirrored-database/explore)

**For Azure Synapse Link**:

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [Troubleshoot: Azure Synapse Link for SQL initial snapshot issues](/azure/synapse-analytics/synapse-link/troubleshoot/troubleshoot-sql-snapshot-issues)
