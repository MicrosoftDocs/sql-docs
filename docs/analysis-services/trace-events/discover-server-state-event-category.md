---
title: "Discover Server State Event Category | Microsoft Docs"
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
# Discover Server State Event Category
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The Discover Server State event category has the event classes described in the following table.  
  
|Event Class|Event Id|Description|  
|-----------------|--------------|-----------------|  
|Server State Discover Begin|33|Collects all server-state XMLA discover begin events since the trace started.|  
|Server State Discover Data|34|Collects all server-state XMLA discover data events since the trace started. These events capture the contents of the response to the discover request.|  
|Server State Discover End|35|Collects all server-state XMLA discover end events since the trace started.|  
  
 For information about the columns associated with each of the Query Events event classes, see [Discover Server State Events Data Columns](../../analysis-services/trace-events/discover-server-state-events-data-columns.md).  
  
## See Also  
 [Analysis Services Trace Events](../../analysis-services/trace-events/analysis-services-trace-events.md)  
  
  
