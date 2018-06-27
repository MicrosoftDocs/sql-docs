---
title: "Progress Reports Event Category | Microsoft Docs"
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
# Progress Reports Event Category
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The Progress Reports event category has the event classes described in the following table.  
  
|Event Class|Event Id|Description|  
|-----------------|--------------|-----------------|  
|Progress Report Begin|5|Collects all progress report begin events since the trace was started.|  
|Progress Report End|6|Collects all progress report end events since the trace was started.|  
|Progress Report Current|7|Collects all progress report current events since the trace was started. For example, during processing, current reports include processing information about objects being processed (dimensions, partitions, cube, etc).|  
|Progress Report Error|8|Collects all progress report error events since the trace was started.|  
  
 Each Progress Report Begin event begins with a stream of progress events and is terminated with a Progress Report End event. The stream may contain any number of Progress Report Current and Progress Report Error events.  
  
 For information about the columns associated with each of the Progress Reports event classes, see [Progress Reports Data Columns](../../analysis-services/trace-events/progress-reports-data-columns.md).  
  
## See Also  
 [Analysis Services Trace Events](../../analysis-services/trace-events/analysis-services-trace-events.md)  
  
  
