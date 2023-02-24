---
title: "conflict_&lt;schema&gt;_&lt;table&gt; (Transact-SQL)"
description: conflict_&lt;schema&gt;_&lt;table&gt; (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "01/15/2016"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "conflict_"
  - "conflict_TSQL"
helpviewer_keywords:
  - "conflict_<schema>_<table>"
dev_langs:
  - "TSQL"
ms.assetid: 15ddd536-db03-454e-b9b5-36efe1f756d7
---
# conflict_&lt;schema&gt;_&lt;table&gt; (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The conflict_\<schema>_\<table> table contains information about conflicting rows in peer-to-peer replication. A conflict table exists for each replicated table in a publication, where the name of the conflict table is appended with the schema and article name. These article-specific conflict tables exist in each publication database.  
  
 For peer-to-peer replication, by default, the Distribution Agent fails when it detects a conflict. A conflict error is logged into the error log, but no conflict data is logged into the conflict table; therefore it is not available for viewing. If the Distribution Agent is allowed to continue, a conflict is logged locally on each node where it was detected. For more information, see "Handling Conflicts" in [Conflict Detection in Peer-to-Peer Replication](../../relational-databases/replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|__$originator_id|**int**|ID of the node at which the conflicting change originated. For a list of IDs, execute [sp_help_peerconflictdetection](../../relational-databases/system-stored-procedures/sp-help-peerconflictdetection-transact-sql.md).|  
|__$origin_datasource|**int**|Node at which the conflicting change originated.|  
|__$tranid|**nvarchar (40)**|Log sequence number (LSN) of the conflicting change when it was applied at the __$origin_datasource.|  
|__$conflict_type|**int**|Type of conflict, which can be one of the following values:<br /><br /> 1: An update failed because the local row was changed by another update or it was deleted and then reinserted.<br /><br /> 2: An update failed because the local row was already deleted.<br /><br /> 3: A delete failed because the local row was changed by another update or it was deleted and then reinserted.<br /><br /> 4: A delete failed because the local row was already deleted.<br /><br /> 5: An insert failed because the local row was already inserted or it was inserted and then updated.|  
|__$is_winner|**bit**|Indicates whether the row in this table was the winner of the conflict, which means it was applied to the local node.|  
|__$pre_version|**varbinary (32)**|Version of the database at which the conflicting change originated.|  
|__$reason_code|**int**|Resolution code for the conflict. Can be one of the following values:<br /><br /> 0<br /><br /> 1<br /><br /> 2<br /><br /> <br /><br /> For more information, see **__$reason_text**.|  
|__$reason_text|**nvarchar (720)**|Resolution for the conflict. Can be one of the following values:<br /><br /> Resolved (1)<br /><br /> Unresolved (2)<br /><br /> Unknown (0)|  
|__$update_bitmap|**varbinary(** *n* **)**. Size varies depending upon content.|Bitmap that indicates which columns were updated in the case of an update-update conflict.|  
|__$inserted_date|**datetime**|Date and time that the conflicting row was inserted into this table.|  
|__$row_id|**timestamp**|Row version that is associated with the row that caused the conflict.|  
|__$change_id|**binary (8)**|For a local row, this value is equal to the __$row_id of the incoming row that conflicted with the local row. This value is NULL for an incoming row.|  
|\<base table column names>|\<base table column types>|The conflict table contains one column for each column in the base table.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
