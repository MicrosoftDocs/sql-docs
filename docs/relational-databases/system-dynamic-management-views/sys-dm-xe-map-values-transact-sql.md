---
title: "sys.dm_xe_map_values (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_xe_map_values"
  - "dm_xe_map_values"
  - "dm_xe_map_values_TSQL"
  - "sys.dm_xe_map_values_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_xe_map_values dynamic management view"
  - "xe"
ms.assetid: c0c5dd7e-9cee-47e2-b65a-88194c00aa1f
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_xe_map_values (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a mapping of internal numeric keys to human-readable text.  
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|name|**nvarchar(256)**|The name of the map. name is unique across the local system. Is not nullable.|  
|object_package_guid|**uniqueidentifier**|The GUID of the package that contains the map. Is not nullable.|  
|map_key|**int**|The internal key value. Is not nullable.|  
|map_value|**nvarchar(3072)**|A description of the key value. Is not nullable.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
### Relationship Cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|dm_xe_map_values.object_package_guid<br /><br /> dm_xe_map_values.name|sys.dm_xe_objects.package_guid<br /><br /> sys.dm_xe_objects.name|Many-to-one| 
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)  
  
  

