---
title: "sysmergepartitioninfo (Transact-SQL)"
description: sysmergepartitioninfo (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sysmergepartitioninfo_TSQL"
  - "sysmergepartitioninfo"
helpviewer_keywords:
  - "sysmergepartitioninfo system table"
dev_langs:
  - "TSQL"
ms.assetid: 7429ad2c-dd33-4f7d-89cc-700e083af518
---
# sysmergepartitioninfo (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Provides information on partitions for each article. Contains one row for each merge article defined in the local database. This table is stored in the publication and subscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**artid**|**uniqueidentifier**|The unique identification number for the given article.|  
|**pubid**|**uniqueidentifier**|The unique identification number for this publication; generated when the publication is added.|  
|**partition_view_id**|**int**|The ID of the partition view over this table. The view shows a mapping of each row in the article to the different partition id's it belongs to.|  
|**repl_view_id**|**int**|To be added.|  
|**partition_deleted_view_rule**|**nvarchar(4000)**|The SQL statement used inside a merge replication trigger to retrieve the partition ID for each deleted or updated row based on its old column values|  
|**partition_inserted_view_rule**|**nvarchar(4000)**|The SQL statement used inside a merge replication trigger to retrieve the partition ID for each inserted or updated based on its new column values.|  
|**membership_eval_proc_name**|**sysname**|The name of the procedure that evaluates the current partition IDs of rows in **MSmerge_contents**.|  
|**column_list**|**nvarchar(4000)**|The comma-separated list of columns replicated in an article.|  
|**column_list_blob**|**nvarchar(4000)**|The comma-separated list of columns replicated in an article, including binary large object columns.|  
|**expand_proc**|**sysname**|The name of the procedure that reevaluates partition IDs for all child rows of a newly inserted parent row, and for parent rows that have undergone a partition change or have been deleted.|  
|**logical_record_parent_nickname**|**int**|The nickname of the top-level parent of a given article in a logical record.|  
|**logical_record_view**|**int**|A view that outputs the top-level parent article rowguid corresponding to each child rowguid.|  
|**logical_record_deleted_view_rule**|**nvarchar(4000)**|Similar to **logical_record_view**, except the it shows child rows in the "deleted" table in update and delete triggers.|  
|**logical_record_level_conflict_detection**|**bit**|Indicates whether conflicts should be detected at the logical record level or at the row or column level.<br /><br /> **0** = Row- or column-level conflict detection is used.<br /><br /> **1** = Logical record conflict detection is used, where a change in a row at the Publisher and change in a separate row the same logical record at the Subscriber is handled as a conflict.<br /><br /> When this value is **1**, only logical record level conflict resolution can be used.|  
|**logical_record_level_conflict_resolution**|**bit**|Indicates whether conflicts should be resolved at the logical record level or at the row or column level.<br /><br /> **0** = Row- or column-level resolution is used.<br /><br /> **1** = In case of a conflict, the entire logical record from the winner overwrites the entire logical record on the losing side.<br /><br /> A value of **1** can be used with both logical record-level detection and with row- or column-level detection.|  
|**partition_options**|**tinyint**|Defines the way in which data in the article is partitioned, which enables performance optimizations when all rows belong in only one partition or in only one subscription. *partition_options* can be one of the following values.<br /><br /> **0** = The filtering for the article either is static or does not yield a unique subset of data for each partition, i.e. an "overlapping" partition.<br /><br /> **1** = The partitions are overlapping, and DML updates made at the Subscriber cannot change the partition to which a row belongs.<br /><br /> **2** = The filtering for the article yields non-overlapping partitions, but multiple Subscribers can receive the same partition.<br /><br /> **3** = The filtering for the article yields non-overlapping partitions that are unique for each subscription.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
