---
title: "changefeed.change_feed_settings (Transact-SQL)"
description: "changefeed.change_feed_settings contains metadata that is used to configure change feed."
author: im-microsoft
ms.author: imotiwala
ms.reviewer: wiassaf
ms.date: 11/07/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "changefeed.change_feed_settings"
  - "changefeed.change_feed_settings_TSQL"
helpviewer_keywords:
  - "changefeed.change_feed_settings"
dev_langs:
  - "TSQL"
---
# changefeed.change_feed_settings (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Contains metadata that is used to configure change feed for Azure Synapse Link for SQL.

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**maxtrans**|**int**| Maximum transactions to process in each cycle.|
|**seqno**|**binary(10)**|Log Sequence Number (LSN) marker to track the last published LSN (log record).|
|**schema_version**|**int**| Tracks current schema version of database is ON. Determines whether a DB schema needs to be updated or not on startup. |
|**pollinterval**|**int**| The frequency that the log is scanned for any new changes in seconds.|
  
## See also  

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)

## Next steps

- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [sys.sp_help_change_feed (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-help-change-feed.md)  
