---
title: "changefeed.change_feed_settings (Transact-SQL)"
description: "changefeed.change_feed_settings contains metadata that is used to configure change feed."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala
ms.date: 03/08/2024
ms.service: synapse-analytics
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "changefeed.change_feed_settings"
  - "changefeed.change_feed_settings_TSQL"
helpviewer_keywords:
  - "changefeed.change_feed_settings"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||=azuresqldb-current||=azure-sqldw-latest"
---
# changefeed.change_feed_settings (Transact-SQL)
[!INCLUDE [sqlserver2022-asdb-asa](../../includes/applies-to-version/sqlserver2022-asdb-asa.md)]

Contains metadata that is used to configure change feed for Azure Synapse Link for SQL.

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
| `maxtrans` |**int**| Maximum transactions to process in each cycle.|
| `seqno` |**binary(10)**|Log Sequence Number (LSN) marker to track the last published LSN (log record).|
| `schema_version` |**int**| Tracks current schema version of database. Determines whether a schema needs to be updated or not on startup. |
| `pollinterval` |**int**| The frequency that the log is scanned for any new changes in seconds.|

## Remarks

The `changefeed.change_feed_settings` system table isn't used in [Fabric mirrored databases](/fabric/database/mirrored-database/overview), instead use the [sys.sp_help_change_feed_settings (Transact-SQL)](../system-stored-procedures/sp-help-change-feed-settings.md) system stored procedure.

## Related content

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [sys.sp_help_change_feed (Transact-SQL)](../system-stored-procedures/sp-help-change-feed.md)
