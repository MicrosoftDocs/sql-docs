---
title: "sys.sp_change_feed_enable_db (Transact-SQL)"
description: "The sys.sp_change_feed_enable_db system stored procedure enables the current database for Azure Synapse Link or Fabric Mirrored Database publishing."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala
ms.date: 03/08/2024
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_change_feed_enable_db_TSQL"
  - "sys.sp_change_feed_enable_db"
  - "sp_change_feed_enable_db_TSQL"
  - "sp_change_feed_enable_db"
helpviewer_keywords:
  - "sp_change_feed_enable_db"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||=azuresqldb-current||=fabric||=azure-sqldw-latest"
---
# sys.sp_change_feed_enable_db (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asa-fabric](../../includes/applies-to-version/sqlserver2022-asdb-asa-fabric.md)]

Enables current database for [Azure Synapse Link for SQL](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview) or [Microsoft Fabric mirrored databases (Preview)](/fabric/database/mirrored-database/overview).

> [!NOTE]  
> This stored procedure is used internally and is not recommended for direct administrative use. Use Synapse Studio or the Fabric portal instead. Using this procedure could introduce inconsistency.

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
EXECUTE sys.sp_change_feed_enable_db
    [ [ @maxtrans = ] max_trans ]
    [ , [ @pollinterval = ] polling_interval ]
GO
```

## Arguments

#### [ [ @maxtrans = ] *max_trans* ]

Data type is **int**. Indicates the maximum number of transactions to process in each scan cycle. Default value if not specified is `500`. If specified, the value must be a positive integer.

#### [ @pollinterval = ] *polling_interval*

Data type is **int**. Describes the frequency that the log is scanned for any new changes in seconds. Default interval if not specified is 5 seconds. The value must be `5` or larger.

## Permissions

A user with [CONTROL database permissions](../security/permissions-database-engine.md), **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Examples

The following sample enables the change feed.

```sql
EXECUTE sys.sp_change_feed_enable_db;
```

Verify the database is enabled.

```sql
SELECT 
    [name]
  , is_data_lake_replication_enabled 
FROM sys.databases;
```

## Related content

- [sys.sp_change_feed_enable_table (Transact-SQL)](sp-change-feed-enable-table.md)
- [sys.sp_change_feed_create_table_group (Transact-SQL)](sp-change-feed-create-table-group.md)
- [sys.sp_help_change_feed (Transact-SQL)](sp-help-change-feed.md)
- [sys.sp_help_change_feed_table (Transact-SQL)](sp-help-change-feed-table.md)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-views/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-views/sys-dm-change-feed-errors.md)

**For Microsoft Fabric mirrored databases**:

- [Microsoft Fabric mirrored databases (Preview)](/fabric/database/mirrored-database/overview)
- [Microsoft Fabric mirrored databases monitoring](/fabric/database/mirrored-database/monitor)
- [Explore data in your Mirrored database using Microsoft Fabric](/fabric/database/mirrored-database/explore)

**For Azure Synapse Link**:

- [sp_change_feed_disable_db (Transact-SQL)](sp-change-feed-disable-db.md)
- [sp_change_feed_drop_table_group (Transact-SQL)](sp-change-feed-drop-table-group.md)
- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [changefeed.change_feed_tables (Transact-SQL)](../system-tables/changefeed-change-feed-tables-transact-sql.md)
- [changefeed.change_feed_table_groups (Transact-SQL)](../system-tables/changefeed-change-feed-table-groups-transact-sql.md)
- [changefeed.change_feed_settings (Transact-SQL)](../system-tables/changefeed-change-feed-settings.md)
- [Troubleshoot: Azure Synapse Link for SQL initial snapshot issues](/azure/synapse-analytics/synapse-link/troubleshoot/troubleshoot-sql-snapshot-issues)
