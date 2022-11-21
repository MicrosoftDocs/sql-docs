---
title: "syscollector_config_store (Transact-SQL)"
description: syscollector_config_store (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "syscollector_config_store_TSQL"
  - "syscollector_config_store"
helpviewer_keywords:
  - "data collector view"
  - "syscollector_config_store view"
dev_langs:
  - "TSQL"
ms.assetid: f15f6b05-6808-4b76-b6a8-48dec844cf63
---
# syscollector_config_store (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns properties that apply to the entire data collector, as opposed to a collection set instance. Each row in this view describes a specific data collector property, such as the name of the management data warehouse, and the instance name where the management data warehouse is located.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|parameter_name|**nvarchar(128)**|The name of the property. Is not nullable.|  
|parameter_value|**sql_variant**|The actual value of the property. Is nullable.|  
  
## Permissions  
 Requires SELECT permission on the view or membership in the dc_operator, dc_proxy, or dc_admin fixed database roles.  
  
## Remarks  
 The list of properties available is fixed and their values can only be changed using the appropriate stored procedure. The following table describes the properties that are exposed through this view.  
  
|Property name|Description|  
|-------------------|-----------------|  
|CacheDirectory|The name of the directory in the file system where the collector type packages store temporary information.<br /><br /> NULL = the default temporary [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] directory is used.|  
|CacheWindow|Indicates the data retention policy of the cache directory for failed data uploads.<br /><br /> -1 = Retain the data from all upload failures.<br /><br /> 0 = Do not retain any data from upload failures.<br /><br /> *n* = Retain data from *n* previous upload failures, where *n* >= 1.<br /><br /> Use the sp_syscollector_set_cache_window stored procedure to change this value.|  
|CollectorEnabled|Indicates the state of the data collector.<br /><br /> 0 = disabled<br /><br /> 1 = enabled<br /><br /> Use either the sp_syscollector_enable_collector or sp_syscollector_disable_collector stored procedure to change this value.|  
|MDWDatabase|The name of the management data warehouse. Use the sp_syscollector_set_warehouse_database_name stored procedure to change this value.|  
|MDWInstance|The name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance for the management data warehouse. Use the sp_syscollector_set_warehouse_instance_name stored procedure to change this value.|  
  
## Examples  
 The following example queries the syscollector_config_store view.  
  
```sql  
SELECT parameter_name, parameter_value  
FROM msdb.dbo.syscollector_config_store;  
```  
  
## See Also  
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)   
 [Data Collector Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/data-collector-views-transact-sql.md)   
 [Data Collection](../../relational-databases/data-collection/data-collection.md)   
 [sp_syscollector_enable_collector &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-enable-collector-transact-sql.md)   
 [sp_syscollector_disable_collector &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-disable-collector-transact-sql.md)   
 [sp_syscollector_set_warehouse_database_name &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-set-warehouse-database-name-transact-sql.md)   
 [sp_syscollector_set_warehouse_instance_name &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-set-warehouse-instance-name-transact-sql.md)   
 [sp_syscollector_set_cache_window &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-syscollector-set-cache-window-transact-sql.md)  
  
  
