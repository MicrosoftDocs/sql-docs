---
title: "sys.sp_change_feed_enable_table (Transact-SQL)"
description: "The sys.sp_change_feed_enable_table system stored procedure enables the addition of a new table to an existing table group."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala
ms.date: 03/08/2024
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_change_feed_enable_table_TSQL"
  - "sys.sp_change_feed_enable_table"
  - "sp_change_feed_enable_table_TSQL"
  - "sp_change_feed_enable_table"
helpviewer_keywords:
  - "sp_change_feed_enable_table"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||=azuresqldb-current||=fabric||=azure-sqldw-latest"
---
# sys.sp_change_feed_enable_table (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asa-fabric](../../includes/applies-to-version/sqlserver2022-asdb-asa-fabric.md)]

Stored procedure to enable the creation of a new table to an existing table group.

> [!NOTE]  
> This system stored procedure is used internally and is not recommended for direct administrative use. Use Synapse Studio or the Fabric portal instead. Using this procedure could introduce inconsistency.

This system stored procedure is used for:

- The Azure Synapse Link feature for SQL Server instances and Azure SQL Database. For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).
- The Fabric Mirrored Database feature for Azure SQL Database. For more information, see [Microsoft Fabric mirrored databases (Preview)](/fabric/database/mirrored-database/overview).

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sys.sp_change_feed_enable_table
    @table_group_id
    , @table_id
    , @source_schema
    , @source_name
[ ; ]
```

## Arguments

#### @table_group_id

The unique identifier of the table group.

#### @table_id

The unique identifier for the change feed table generated during setup workflow.

#### @source_schema

The source table schema name.

#### @source_name

The source table name.

## Permissions

A user with [CONTROL database permissions](../security/permissions-database-engine.md), **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Related content

- [sys.sp_help_change_feed (Transact-SQL)](sp-help-change-feed.md)
- [sys.sp_help_change_feed_table (Transact-SQL)](sp-help-change-feed-table.md)
- [sys.sp_change_feed_configure_parameters (Transact-SQL)](sp-change-feed-configure-parameters.md)
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
