---
title: "syscollector_collection_sets (Transact-SQL)"
description: syscollector_collection_sets (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "syscollector_collection_sets_TSQL"
  - "syscollector_collection_sets"
helpviewer_keywords:
  - "data collector view"
  - "syscollector_collection_sets view"
dev_langs:
  - "TSQL"
ms.assetid: db0def92-f25b-45da-9709-eab972b33800
---
# syscollector_collection_sets (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Provides information about a collection set, including schedule, collection mode, and its state.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|collection_set_id|**int**|The local identifier for the collection set. Is not nullable.|  
|collection_set_uid|**uniqueidentifier**|The globally unique identifier for the collection set. Is not nullable.|  
|name|**nvarchar(4000)**|The name of the collection set. Is nullable.|  
|target|**nvarchar(max)**|Identifies the target for the collection set. Is nullable.|  
|is_system|**bit**|Turned on (1) or off (0) to indicate if the collection set was included with the data collector or if it was added later by the dc_admin. This could be a custom collection set developed in-house or by a third party. Is not nullable.|  
|is_running|**bit**|Indicates whether or not the collection set is running. Is not nullable.|  
|collection_mode|**smallint**|Specifies the collection mode for the collection set. Is not nullable.<br /><br /> Collection mode is one of the following:<br /><br /> 0 - Cached mode. Data collection and upload are on separate schedules.<br /><br /> 1 - Non-cached mode. Data collection and upload are on the same schedule.|  
|proxy_id|**int**|Identifies the proxy that is used to run the collection set job step. Is nullable.|  
|schedule_uid|**uniqueidentifier**|Provides a pointer to the collection set schedule. Is nullable.|  
|collection_job_id|**uniqueidentifier**|Identifies the collection job. Is nullable.|  
|upload_job_id|**uniqueidentifier**|Identifies the collection upload job. Is nullable.|  
|logging_level|**smallint**|Specifies the logging level (0, 1 or 2). Is not nullable.|  
|days_until_expiration|**smallint**|The number of days that the collected data is saved in the management data warehouse. Is not nullable.|  
|description|**nvarchar(4000)**|Describes the collection set. Is nullable.|  
|dump_on_any_error|**bit**|Turned on (1) or off (0) to indicate whether to create an [!INCLUDE[ssIS](../../includes/ssis-md.md)] dump file on any error. Is not nullable.|  
|dump_on_codes|**nvarchar(max)**|Contains the list of [!INCLUDE[ssIS](../../includes/ssis-md.md)] error codes that are used to trigger the dump file. Is nullable.|  
  
## Permissions  
 Requires SELECT for dc_operator, dc_proxy.  
  
## Remarks  
 The data collector API only allows you to change or delete the collection sets that you create. The collection sets that are provided with the system cannot be modified or deleted. However, you can still enable or disable a system collection set, and change its configuration.  
  
## See Also  
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)   
 [Data Collector Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/data-collector-views-transact-sql.md)   
 [Data Collection](../../relational-databases/data-collection/data-collection.md)  
  
  
