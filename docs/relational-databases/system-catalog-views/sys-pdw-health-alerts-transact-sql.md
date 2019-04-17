---
title: "sys.pdw_health_alerts (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: ""
ms.technology: system-objects
ms.topic: conceptual
ms.assetid: 49c01e5f-ee47-41a0-871d-35a759f50851
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: ">= aps-pdw-2016 || = sqlallproducts-allversions"
---
# sys.pdw_health_alerts (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md)]

  Stores properties for the different alerts that can occur on the system; this is a catalog table for alerts.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|alert_id|**int**|Unique identifier of the alert.<br /><br /> Key for this view.|NOT NULL|  
|component_id|**int**|ID of the component this alert applies to. The component is a general component identifier, such as "Power Supply," and is not specific to an installation. See [sys.pdw_health_components &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-health-components-transact-sql.md).|NOT NULL|  
|alert_name|**nvarchar(255)**|Name of the alert.|NOT NULL|  
|state|**nvarchar(32)**|State of the alert.|NOT NULL<br /><br /> Possible values:<br /><br /> 'Operational'<br /><br /> 'NonOperational'<br /><br /> 'Degraded'<br /><br /> 'Failed'|  
|severity|**nvarchar(32)**|Severity of the alert.|NOT NULL<br /><br /> Possible values:<br /><br /> 'Informational'<br /><br /> 'Warning'<br /><br /> 'Error'|  
|type|**nvarchar(32)**|Type of alert.|NOT NULL<br /><br /> Possible values:<br /><br /> StatusChange - The device status has changed.<br /><br /> Threshold - A value has exceeded the threshold value.|  
|description|**nvarchar(4000)**|Description of the alert.|NOT NULL|  
|condition|**nvarchar(255)**|Used when type = Threshold. Defines how the alert threshold is calculated.|NULL|  
|status|**nvarchar(32)**|Alert status|NULL|  
|condition_value|**bit**|Indicates whether the alert is allowed to occur during system operation.|NULL<br /><br /> Possible values<br /><br /> 0 - alert is not generated.<br /><br /> 1 - alert is generated.|  
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  
