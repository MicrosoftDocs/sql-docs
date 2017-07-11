---
title: "Progress Reports Event Category | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "progress events [Analysis Services]"
  - "Progress Reports event category"
  - "event classes [Analysis Services], progress reports"
ms.assetid: c130f160-28ef-49bc-9ee6-da47dc9aab2a
caps.latest.revision: 23
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Progress Reports Event Category
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
  
  