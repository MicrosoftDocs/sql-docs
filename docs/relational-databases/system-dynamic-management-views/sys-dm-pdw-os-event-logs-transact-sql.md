---
title: "sys.dm_pdw_os_event_logs (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: a0daa8cf-72e2-4349-8be1-d3cc0f9b1e02
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">= aps-pdw-2016 || = sqlallproducts-allversions"
---
# sys.dm_pdw_os_event_logs (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md)]

  Holds information regarding the different Windows Event logs on the different nodes.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|pdw_node_id|**int**|Appliance node this log is from.<br /><br /> pdw_node_id and log_name form the key for this view.||  
|log_name|**nvarchar(255)**|Windows event log name.<br /><br /> pdw_node_id and log_name form the key for this view.||  
|log_source|**nvarchar(255)**|Windows event log source name.||  
|event_id|**int**|ID of the event. Not unique.||  
|event_type|**nvarchar(255)**|Type of the event, identifying severity.|'Information', 'Warning', 'Error'|  
|event_message|**nvarchar(4000)**|Details of the event.||  
|generate_time|**datetime**|Time the event was created.||  
|write_time|**datetime**|Time the event was actually written to the log.||  
  
 For information about the maximum rows retained by this view, see the Metadata section in the [Capacity limits](/azure/sql-data-warehouse/sql-data-warehouse-service-capacity-limits#metadata) topic. 
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
  
  
