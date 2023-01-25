---
title: "sys.dm_broker_activated_tasks (Transact-SQL)"
description: sys.dm_broker_activated_tasks returns a row for each stored procedure activated by Service Broker.
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/03/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_broker_activated_tasks"
  - "sys.dm_broker_activated_tasks_TSQL"
  - "dm_broker_activated_tasks"
  - "dm_broker_activated_tasks_TSQL"
helpviewer_keywords:
  - "sys.dm_broker_activated_tasks dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_broker_activated_tasks (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row for each stored procedure activated by Service Broker.  
 

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**spid**|**int**|ID of the session of the activated stored procedure. NULLABLE.|  
|**database_id**|**smallint**|ID of the database in which the queue is defined. NULLABLE.|  
|**queue_id**|**int**|ID of the object of the queue for which the stored procedure was activated. NULLABLE.|  
|**procedure_name**|**nvarchar(650)**|Name of the activated stored procedure. NULLABLE.|  
|**execute_as**|**int**|ID of the user that the stored procedure runs as. NULLABLE.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Physical joins  
 
:::image type="content" source="../../relational-databases/system-dynamic-management-views/media/join-dm-broker-activated-tasks-1.svg" alt-text="Diagram of physical joins for sys.dm_broker_activated_tasks.":::
  
## Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|`dm_broker_activated_tasks`.`spid`|`dm_exec_sessions`.`session_id`|One-to-one|  
  
## Next steps
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Service Broker Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/service-broker-related-dynamic-management-views-transact-sql.md)  
