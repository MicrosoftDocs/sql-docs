---
title: "cdc.change_feed_table_groups (Transact-SQL)"
description: cdc.change_feed_table_groups (Transact-SQL)
author: IdrisMotiwalaMSFT
ms.author: imotiwala
ms.date: "10/29/2022"
ms.prod: sql
ms.prod_service: "database-engine"
ms.technology: system-objects
ms.topic: "reference"
f1_keywords:
  - "cdc.change_feed_table_groups"
  - "cdc.change_feed_table_groups_TSQL"
helpviewer_keywords:
  - "cdc.change_feed_table_groups"
dev_langs:
  - "TSQL"
ms.assetid: 
---
# cdc.change_feed_table_groups (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Azure Synapse Link for SQL table to store metadata with their associated indexes  


|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**table_group_id**|**uniqueidentifier**| Unique Identifier of the change feed table group.|
|**table_group_name**|**nvarchar(140)**| The name of the table group.|
|**destination_location**|**nvarchar(512)**| URL string of the landing zone folder. |
|**destination_credential**|**sysname**| The credential name to access the landing zone.|
|**workspace_id**|**nvarchar(247)**| The related Synapse workspace Azure resource ID.|  
|**synapse_workgroup_name**|**nvarchar(50)**| The related Synapse workspace name.|  
|**enabled**|**bit**|Tracks if the table group is enabled for change feed 1 - Yes. 0 - No. |  
|**destination_type**|**tinyint** |The type of destination assigned to this table group. Valid destination_type values: 0 - Synapse. 1 - Event Hub. 2 - Kafka |  
|**max_message_size_bytes**|**int**| Maximum message size that the destination supports.|  
|**partition_scheme**|**tinyint**| Partitioning scheme to partition the change data while publishing table group|  

  
## See Also  
 [sys.sp_help_change_feed &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-change-feed.md)  
  
  
