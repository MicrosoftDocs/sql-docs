---
title: "sysarticleupdates (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sysarticleupdates_TSQL"
  - "sysarticleupdates"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysarticleupdates system table"
ms.assetid: 11a53bcd-a215-4d0b-9db8-233981d3ef5d
author: stevestein
ms.author: sstein
manager: craigg
---
# sysarticleupdates (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
  
