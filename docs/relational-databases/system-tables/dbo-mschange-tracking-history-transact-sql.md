---
title: "dbo.MSchange_tracking_history (Transact-SQL)"
description: dbo.MSchange_tracking_history (Transact-SQL)
author: MashaMSFT
ms.author: mathoma
ms.reviewer: roblescarlos, bspendolini, randolphwest
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
  
## Related content

- [About Change Tracking (SQL Server)](../track-changes/about-change-tracking-sql-server.md)
- [Troubleshoot change tracking auto cleanup issues](../track-changes/cleanup-and-troubleshoot-change-tracking-sql-server.md)
- [Change Tracking Functions (Transact-SQL)](../system-functions/change-tracking-functions-transact-sql.md)
- [Change Tracking stored procedures (Transact-SQL)](../system-stored-procedures/change-tracking-stored-procedures-transact-sql.md)
