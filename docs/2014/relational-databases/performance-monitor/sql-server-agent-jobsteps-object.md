---
title: "SQL Server Agent, JobSteps Object | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "JobSteps object"
  - "SQLAgent:JobSteps"
ms.assetid: 44f9983c-1753-4fe0-8475-973aa2460b3a
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server Agent, JobSteps Object
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent **JobSteps** performance object contains performance counters that report information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job steps. The table below lists the counters that this object contains.  
  
 The table below contains the **SQLAgent:JobSteps** counters.  
  
|Name|Description|  
|----------|-----------------|  
|**Active steps**|This counter reports the number of job steps currently running.|  
|**Queued steps**|This counter reports the number of job steps that are ready for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to run, but which have not yet started running.|  
|**Total step retries**|This counter reports the total number of times that [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has retried a job step since the last server restart.|  
  
 Each counter in the object contains the following instances:  
  
|Instance|Description|  
|--------------|-----------------|  
|**_Total**|Information for all job steps.|  
|**ActiveScripting**|Information for job steps that use the **ActiveScripting** subsystem.|  
|**ANALYSISCOMMAND**|Information for job steps that use the ANALYSISCOMMAND subsystem.|  
|**ANALYSISQUERY**|Information for job steps that use the ANALYSISQUERY subsystem.|  
|**CmdExec**|Information for job steps that use the **CmdExec** subsystem.|  
|**Distribution**|Information for job steps that use the **Distribution** subsystem.|  
|**Dts**|Information for job steps that use the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] subsystem.|  
|**LogReader**|Information for job steps that use the **LogReader** subsystem.|  
|**Merge**|Information for job steps that use the **Merge** subsystem.|  
|**PowerShell**|Information for job steps that use the **PowerShell** subsystem.|  
|**QueueReader**|Information for job steps that use the **QueueReader** subsystem.|  
|**Snapshot**|Information for job steps that use the **Snapshot** subsystem.|  
|**TSQL**|Information for job steps that execute [!INCLUDE[tsql](../../includes/tsql-md.md)].|  
  
## See Also  
 [Manage Job Steps](../../ssms/agent/manage-job-steps.md)   
 [Use Performance Objects](../../ssms/agent/use-performance-objects.md)   
 [Monitor Resource Usage &#40;System Monitor&#41;](monitor-resource-usage-system-monitor.md)  
  
  
