---
title: "SQL Server Agent, Jobs object"
description: Learn about the SQL Server Agent Jobs performance object, which contains performance counters that report information about SQL Server Agent jobs.
author: MikeRayMSFT
ms.author: mikeray
ms.date: 12/04/2023
ms.service: sql
ms.subservice: performance
ms.topic: reference
helpviewer_keywords:
  - "SQLAgent:Jobs"
  - "Jobs object"
---
# SQL Server Agent, Jobs object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

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
|**Others**|Information for jobs that were not started by alerts or schedules. Typically these are jobs started manually using `sp_start_job`.|  
|**Schedules**|Information for jobs started by schedules.|  
  
## Related content

- [Implement Jobs](../../ssms/agent/implement-jobs.md)
- [Use Performance Objects](../../ssms/agent/use-performance-objects.md)
- [Monitor Resource Usage (Performance Monitor)](monitor-resource-usage-system-monitor.md)
