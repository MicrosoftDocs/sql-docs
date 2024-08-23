---
title: "sys.sp_change_feed_create_table_group (Transact-SQL)"
description: "The sys.sp_change_feed_create_table_group system stored procedure enables the creation of new change feed table group within the current database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala
ms.date: 03/18/2024
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_change_feed_create_table_group_TSQL"
  - "sys.sp_change_feed_create_table_group"
  - "sp_change_feed_create_table_group_TSQL"
  - "sp_change_feed_create_table_group"
helpviewer_keywords:
  - "sp_change_feed_create_table_group"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current || =fabric || =azure-sqldw-latest"
---
# sys.sp_change_feed_create_table_group (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asa-fabric](../../includes/applies-to-version/sqlserver2022-asdb-asa-fabric.md)]

Creates a source to maintain metadata specific to each table group. A table group represents the container for all the individual tables that will be replicated.

> [!NOTE]  
> This system stored procedure is used internally and isn't recommended for direct administrative use. Use Synapse Studio or the Fabric portal instead. Using this procedure could introduce inconsistency.

This system stored procedure is used for:

- The Azure Synapse Link feature for SQL Server instances and Azure SQL Database. For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).
- The Fabric Mirrored Database feature for Azure SQL Database. For more information, see [What is Mirroring in Fabric?](/fabric/database/mirrored-database/overview).

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
EXECUTE sys.sp_change_feed_create_table_group
    @table_group_id
    , @table_group_name
    , @workspace_id
    , @destination_location
    , @destination_credential
[ ; ]
```

## Arguments

#### @table_group_id

The unique identifier of the table group.

#### @table_group_name

The name of the table group.

#### @workspace_id

Azure resourceID for the workspace requesting creation of the new table group.

#### @destination_location

URL string of the landing zone folder.

#### @destination_credential

The credential name to access the landing zone.

## Permissions

A user with [CONTROL database permissions](../security/permissions-database-engine.md), **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Related content

- [sys.sp_help_change_feed (Transact-SQL)](sp-help-change-feed.md)
- [sys.sp_help_change_feed_table (Transact-SQL)](sp-help-change-feed-table.md)
- [sys.sp_change_feed_configure_parameters (Transact-SQL)](sp-change-feed-configure-parameters.md)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-views/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-views/sys-dm-change-feed-errors.md)

**For Microsoft Fabric mirrored databases**:

- [What is Mirroring in Fabric?](/fabric/database/mirrored-database/overview)
- [Monitor Fabric mirrored database replication](/fabric/database/mirrored-database/monitor)
- [Explore data in your Mirrored database using Microsoft Fabric](/fabric/database/mirrored-database/explore)

**For Azure Synapse Link**:

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [Troubleshoot: Azure Synapse Link for SQL initial snapshot issues](/azure/synapse-analytics/synapse-link/troubleshoot/troubleshoot-sql-snapshot-issues)
