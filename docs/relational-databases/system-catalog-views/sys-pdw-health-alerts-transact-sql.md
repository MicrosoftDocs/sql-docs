---
title: "sys.pdw_health_alerts (Transact-SQL)"
description: See a reference for the system catalog view sys.pdw_health_alerts (Transact-SQL) for Analytics Platform System.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "05/12/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom: kr2b-contr-experiment
dev_langs:
  - "TSQL"
ms.assetid: 49c01e5f-ee47-41a0-871d-35a759f50851
monikerRange: ">=aps-pdw-2016"
---
# sys.pdw_health_alerts (Transact-SQL)
[!INCLUDE [pdw](../../includes/applies-to-version/pdw.md)]

This view stores properties for the different alerts that can occur on the system. This table is a catalog table for alerts.
  
|Column Name|Data Type|Description|Range|
|-----------------|---------------|-----------------|-----------|
|alert_id|**int**|Unique identifier of the alert.<br />Key for this view.|NOT NULL|
|component_id|**int**|ID of the component this alert applies to. The component is a general component identifier, such as "Power Supply," and is not specific to an installation. See [sys.pdw_health_components &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-health-components-transact-sql.md).|NOT NULL|
|alert_name|**nvarchar(255)**|Name of the alert.|NOT NULL|
|state|**nvarchar(32)**|State of the alert.|NOT NULL<br /><br /> Possible values:<br />'Operational'<br />'NonOperational'<br />'Degraded'<br />'Failed'|
|severity|**nvarchar(32)**|Severity of the alert.|NOT NULL<br /><br /> Possible values:<br />'Informational'<br />'Warning'<br />'Error'|
|type|**nvarchar(32)**|Type of alert.|NOT NULL<br /><br /> Possible values:<br /><br /> StatusChange - The device status has changed.<br /><br /> Threshold - A value has exceeded the threshold value.|
|description|**nvarchar(4000)**|Description of the alert.|NOT NULL|
|condition|**nvarchar(255)**|Used when type = Threshold. Defines how the alert threshold is calculated.|NULL|
|status|**nvarchar(32)**|Alert status|NULL|
|condition_value|**bit**|Indicates whether the alert is allowed to occur during system operation.|NULL<br /><br /> Possible values<br />0 - Alert is not generated.<br />1 - Alert is generated.|

## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  
