---
title: "cdc.change_feed_settings (Transact-SQL)"
description: cdc.change_feed_settings (Transact-SQL)
author: IdrisMotiwalaMSFT
ms.author: imotiwala
ms.date: "10/29/2022"
ms.prod: sql
ms.prod_service: "database-engine"
ms.technology: system-objects
ms.topic: "reference"
f1_keywords:
  - "cdc.change_feed_settings"
  - "cdc.change_feed_settings_TSQL"
helpviewer_keywords:
  - "cdc.change_feed_settings"
dev_langs:
  - "TSQL"
ms.assetid: 
---
# cdc.change_feed_settings (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Azure Synapse Link for SQL table contains metadata that is used to configure change feed.


|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**maxtrans**|**int**| Maximum transactions to process in each cycle.|
|**seqno**|**binary(10)**|Log Sequence Number (LSN) marker to track the last published LSN (log record) |
|**schema_version**|**int**| Tracks current schema version of database is ON. Determines whether a DB schema needs to be updated or not on startup. |
|**pollinterval**|**int**| The frequency that the log is scanned for any new changes in seconds.|


  
## See Also  
 [sys.sp_help_change_feed &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-change-feed.md)  
