---
title: "sys.sp_change_feed_disable_db (Transact-SQL)"
description: "The sys.sp_change_feed_disable_db system stored procedure disables the SQL change feed at the database level."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala
ms.date: 03/13/2024
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_change_feed_disable_db_TSQL"
  - "sys.sp_change_feed_disable_db"
  - "sp_change_feed_disable_db_TSQL"
  - "sp_change_feed_disable_db"
helpviewer_keywords:
  - "sp_change_feed_disable_db"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current || =azure-sqldw-latest || =fabric"
---
# sys.sp_change_feed_disable_db (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asa-fabric](../../includes/applies-to-version/sqlserver2022-asdb-asa-fabric.md)]

Disable the change feed at the database level, and then the metadata for all the associated tables for [Azure Synapse Link for SQL](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview). For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sys.sp_change_feed_disable_db
[ ; ]
```

## Permissions

A user with [CONTROL database permissions](../security/permissions-database-engine.md), **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Remarks

When the change feed is disabled with active table groups, all connections and schedulers are stopped immediately/forcefully without waiting for the current operations are completed. No new change feed table groups can be created for the database, and all the existing metadata describing the table groups will be deleted without waiting for the current operations to complete. Re-enabling change feed results in clean initializations of all table groups and reseeding of all the data.

You should only execute this stored procedure when unsupported actions or unexpected errors have occurred, that require the Mirroring feature to be disabled manually, and can't be removed via the Synapse workspace or Fabric portal.

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
