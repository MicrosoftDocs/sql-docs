---
title: "sysarticleupdates (Transact-SQL)"
description: sysarticleupdates (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sysarticleupdates_TSQL"
  - "sysarticleupdates"
helpviewer_keywords:
  - "sysarticleupdates system table"
dev_langs:
  - "TSQL"
---

# sysarticleupdates (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Contains one row for each article that supports immediate-updating subscriptions. This table is stored in the replicated database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**artid**|**int**|The identity column providing a unique ID number for the article.|  
|**pubid**|**int**|The ID of the publication to which the article belongs.|  
|**sync_ins_proc**|**int**|The ID of the stored procedure handling Insert Sync Transactions.|  
|**sync_upd_proc**|**int**|The ID of the stored procedure handling Update Sync Transactions.|  
|**sync_del_proc**|**int**|The ID of the stored procedure handling Delete Sync Transactions.|  
|**autogen**|**bit**|Indicates that stored procedures are automatically generated:<br /><br /> **0** = False, not automatic.<br /><br /> **1** = True, automatic.|  
|**sync_upd_trig**|**int**|The ID of the automatic versioning trigger on the article table.|  
|**conflict_tableid**|**int**|The ID for the conflict table.|  
|**ins_conflict_proc**|**int**|The ID of the procedure used to write the conflict to the **conflict_table**.|  
|**identity_support**|**bit**|Specifies whether automatic identity range handling is enabled when queued updating is used. **0** means that there is no identity range support.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
