---
title: "SQL Server Agent, Alerts object"
description: Learn about the SQL Server Agent Alerts performance object, which contains performance counters that report information about SQL Server Agent alerts.
author: MikeRayMSFT
ms.author: mikeray
ms.date: 12/04/2023
ms.service: sql
ms.subservice: performance
ms.topic: reference
helpviewer_keywords:
  - "Alerts object"
  - "SQLAgent:Alerts"
---
# SQL Server Agent, Alerts object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The SQL Server Agent **Alerts** performance object contains performance counters that report information about SQL Server Agent alerts. The table below lists the counters that this object contains.  
  
 The table below contains the **SQLAgent:Alerts** counters.  
  
|Name|Description|  
|----------|-----------------|  
|**Activated alerts**|This counter reports the total number of alerts that SQL Server Agent has activated since the last time that SQL Server Agent restarted.|  
|**Alerts activated/minute**|This counter reports the number of alerts that SQL Server Agent activated within the last minute.|  
  
> [!NOTE]  
>  To use this SQL Server Agent object, users must be a member of the **sysadmin** fixed server role.  
  
## Related content

- [Alerts](../../ssms/agent/alerts.md)
- [Use Performance Objects](../../ssms/agent/use-performance-objects.md)
- [Monitor Resource Usage (Performance Monitor)](monitor-resource-usage-system-monitor.md)
