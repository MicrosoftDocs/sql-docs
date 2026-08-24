---
title: "sys.sp_help_change_feed_settings (Transact-SQL)"
description: "The sys.sp_help_change_feed_settings system stored procedure returns state information for Microsoft Fabric Mirroring."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala, ajayj, randolphwest
ms.date: 12/17/2025
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.sp_help_change_feed_settings_TSQL"
  - "sys.sp_help_change_feed_settings"
  - "sp_help_change_feed_settings_TSQL"
  - "sp_help_change_feed_settings"
helpviewer_keywords:
  - "sp_help_change_feed_settings"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current || =azuresqldb-mi-current || =fabric || =fabric-sqldb || =azure-sqldw-latest"
---
# sys.sp_help_change_feed_settings (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb](../../includes/applies-to-version/sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb.md)]

Provides the status and configuration of the [Fabric Mirrored Database](/fabric/database/mirrored-database/overview) change feed feature. Changes to change feed settings are made with [sys.sp_change_feed_configure_parameters](sp-change-feed-configure-parameters.md).

This system stored procedure is used for:

- [SQL database in Microsoft Fabric](/fabric/database/sql/overview)
- [Microsoft Fabric mirrored databases](/fabric/database/mirrored-database/overview)
- [Azure Synapse Link](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [What is change event streaming (preview)?](../track-changes/change-event-streaming/overview.md) introduced in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and Azure SQL Database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_help_change_feed_settings
[ ; ]
```

## Arguments

None.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `maxtrans` | **int** | Maximum transactions to process in each cycle. The default is 10,000. |
| `seqno` | **binary(10)** | Log Sequence Number (LSN) marker to track the last published LSN (log record). |
| `schema_version` | **int** | Tracks current schema version of database. Determines whether a schema needs to be updated or not on startup. |
| `pollinterval` | **int** | The frequency that the log is scanned for any new changes in seconds. |
| `reseed_state` | **tinyint** | `0` = Normal.<br /><br />`1` = The database has started the process of reinitializing to Fabric. Transitionary state.<br /><br />`2` = The database is being reinitialized to Fabric and waiting for replication to restart. Transitionary state. When replication is established, reseed state moves to `0`.<br /><br />**Applies to**: Fabric Mirrored Database only. |
| `destination_type` | **sysname** | Change event streaming destination type.<br /><br />`AzureEventHubsAmqp`<br />`AzureEventHubsKafka`<br /><br />Introduced in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] |
| `partition_scheme` | **tinyint** | Change event streaming partition scheme.<br /><br />`0` = None<br />`1` = Table group.<br />`2` = Table<br />`3` = Column<br /><br />Introduced in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] |
| `encoding` | **tinyint** | Change event streaming message encoding.<br /><br />`0` = JSON<br />`1` = Avro Binary<br /><br />Introduced in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] |
| `autoreseed` | **tinyint** | Whether or not automatic reseeding is enabled for the current database in Fabric Mirroring.<br /><br />`0` = Disabled<br />`1` = Enabled<br /><br />The autoreseed feature is disabled by default in SQL Server 2025. The autoreseed feature is enabled and can't be managed or disabled in Azure SQL Database and Azure SQL Managed Instance. For more information, see [Configure automatic reseed for Fabric mirrored databases from SQL Server](/fabric/database/mirrored-database/sql-server-configure-automatic-reseed). |
| `autoreseedthreshold` | **tinyint** | If `autoreseed` is enabled, the transactions log usage percentage at which to trigger automatic reseed. The default is `70`. For SQL Server 2025 (Preview), this must be configured when `autoreseed` is enabled. |
| `dynamicmaxtrans` | **int** | Whether or not the dynamic maximum transactions setting is enabled. The dynamic maximum transactions feature is enabled by default in SQL Server 2025 (Preview). The dynamic maximum transactions feature is enabled and can't be managed or disabled in Azure SQL Database and Azure SQL Managed Instance. Fabric mirroring always follows a maximum number of transactions to process in each scan cycle as defined by the `maxtrans` setting. When `dynamicmaxtrans` = `1`, Fabric mirroring dynamically adjusts the number of transactions to process per scan between configured values for `dynamicmaxtranslowerbound` and `maxtrans`. For more information, [Mirrored databases from SQL Server performance](/fabric/database/mirrored-database/sql-server-performance). |
| `dynamicmaxtranslowerbound` | **int** | The lower bound for dynamic maxtrans setting for Fabric Mirroring. By default, the lower bound value is `200` but can be modified by [sys.sp_change_feed_configure_parameters](sp-change-feed-configure-parameters.md). |

## Permissions

A user with `CONTROL` database permissions, **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Related content

- [sys.sp_change_feed_configure_parameters (Transact-SQL)](sp-change-feed-configure-parameters.md)
- [sys.sp_help_change_feed (Transact-SQL)](sp-help-change-feed.md)
- [sys.sp_help_change_feed_table (Transact-SQL)](sp-help-change-feed-table.md)
- [sys.sp_help_change_feed_table_groups (Transact-SQL)](sp-help-change-feed-table-groups.md)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-errors.md)
