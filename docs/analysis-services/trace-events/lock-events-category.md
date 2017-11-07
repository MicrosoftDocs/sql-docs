---
title: "Lock Events Category | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 06427f8e-89bb-4710-a0c1-dc5e42b7e95e
caps.latest.revision: 6
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Lock Events Category
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
  
  