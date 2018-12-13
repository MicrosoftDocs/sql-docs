---
title: "SQL Server Agent, Jobs Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLAgent:Jobs"
  - "Jobs object"
ms.assetid: 225b5e2d-4a78-4178-b2b6-b419df83c4aa
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server Agent, Jobs Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The SQL Server Agent **Jobs** performance object contains performance counters that report information about SQL Server Agent jobs. The table below lists the counters that this object contains.  
  
 The table below contains the **SQLAgent:Jobs** counters.  
  
|Name|Description|  
|----------|-----------------|  
|**Active Jobs**|This counter reports the number of jobs currently running.|  
|**Failed jobs**|This counter reports the number of jobs that exited with failure.|  
|**Job success rate**|This counter reports the percentage of executed jobs that completed successfully.|  
|**Jobs activated/minute**|This counter reports the number of jobs launched within the last minute.|  
|**Queued jobs**|This counter reports the number of jobs that are ready for SQL Server Agent to run, but which have not yet started running.|  
|**Successful jobs**|This counter reports the number of jobs that exited with success.|  
  
 Each counter in the object contains the following instances:  
  
|Instance|Description|  
|--------------|-----------------|  
|**_Total**|Information for all jobs.|  
|**Alerts**|Information for jobs started by alerts.|  
|**Others**|Information for jobs that were not started by alerts or schedules. Typically these are jobs started manually using **sp_start_job**.|  
|**Schedules**|Information for jobs started by schedules.|  
  
## See Also  
 [Implement Jobs](../../ssms/agent/implement-jobs.md)   
 [Use Performance Objects](../../ssms/agent/use-performance-objects.md)   
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
