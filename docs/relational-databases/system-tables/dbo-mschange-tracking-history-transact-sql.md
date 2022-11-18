---
title: "dbo.MSchange_tracking_history (Transact-SQL)"
description: dbo.MSchange_tracking_history (Transact-SQL)
author: JetterMcTedder
ms.author: bspendolini
ms.date: "10/20/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dbo.MSchange_tracking_history_TSQL"
  - "dbo.MSchange_tracking_history"
helpviewer_keywords:
  - "dbo.MSchange_tracking_history"
dev_langs:
  - "TSQL"
ms.assetid:
---
# dbo.MSchange_tracking_history (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This system table contains the history of Change Tracking cleanup jobs.

## Column description

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**internal_table_name**|**sysname**|Name of the side table used to track changes for a change tracking enabled table or the syscommittab table.|  
|**table_name**|**sysname**|Name of the change tracking enabled table or the syscommittab table.|  
|**start_time**|**datetime**|Start time of the last auto cleanup process.|  
|**end_time**|**datetime**|End time of the last auto cleanup process.|  
|**rows_cleaned_up**|**bigint**|How many rows were processed by the auto cleanup process.|  
|**cleanup_version**|**bigint**|Current valid change tracking version.|  
|**comments**|**ntext**|Clean up job comments.|  
  
## See Also

 [About Change Tracking &#40;Transact-SQL&#41;](../../relational-databases/track-changes/about-change-tracking-sql-server.md)  
 [Change Tracking Cleanup and Troubleshooting &#40;Transact-SQL&#41;](../../relational-databases/track-changes/cleanup-and-troubleshoot-change-tracking-sql-server.md)  
 [Change Tracking Functions &#40;Transact-SQL&#41;](../../relational-databases/system-functions/change-tracking-functions-transact-sql.md)  
 [Change Tracking Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/change-tracking-stored-procedures-transact-sql.md)  
