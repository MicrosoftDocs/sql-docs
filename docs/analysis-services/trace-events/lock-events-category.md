---
title: "Lock Events Category | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: trace-events
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Lock Events Category
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The Locks Events event category has the event classes described in the following table.  
  
|Event Class|Event Id|Description|  
|-----------------|--------------|-----------------|  
|Deadlock|50|Collects all metadata locks deadlock events since the trace started.|  
|Lock timeout|51|Collects all metadata lock timeout events since the trace started.|  
|Lock Acquired|52|Collects information about locks acquired, since the trace started.|  
|Lock Released|53|Collects information about locks released, since the trace started.|  
|Lock Waiting|54|Collects information about locks in waiting status, since trace started.|  
  
 For information about the columns associated with each of the Lock event classes, see [Lock Events Data Columns](../../analysis-services/trace-events/lock-events-data-columns.md).  
  
## See Also  
 [Analysis Services Trace Events](../../analysis-services/trace-events/analysis-services-trace-events.md)  
  
  
