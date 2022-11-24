---
title: "SQL Server Agent, Alerts object"
description: Learn about the SQL Server Agent Alerts performance object, which contains performance counters that report information about SQL Server Agent alerts.
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Alerts object"
  - "SQLAgent:Alerts"
author: WilliamDAssafMSFT
ms.author: wiassaf
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
  
## See also  
 [Alerts](../../ssms/agent/alerts.md)   
 [Use Performance Objects](../../ssms/agent/use-performance-objects.md)   
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
