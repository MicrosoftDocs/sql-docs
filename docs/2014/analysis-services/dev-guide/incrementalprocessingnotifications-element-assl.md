---
title: "IncrementalProcessingNotifications Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "IncrementalProcessingNotifications Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "IncrementalProcessingNotifications element"
ms.assetid: 46f3c9d0-46cc-4833-8f15-7831207f57ce
caps.latest.revision: 16
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# IncrementalProcessingNotifications Element (ASSL)
  Contains the collection of [IncrementalProcessingNotification](../../../2014/analysis-services/dev-guide/incrementalprocessingnotification-element-assl.md) elements that provide information to the [ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md) element about queries to execute to determine the progress of incremental processing.  
  
## Syntax  
  
```xml  
  
<ProactiveCachingIncrementalProcessingBinding>  
   <IncrementalProcessingNotifications>  
      <IncrementalProcessingNotification>...  
   ...</IncrementalProcessingNotification>  
   </IncrementalProcessingNotifications>  
</ProactiveCachingQueryBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[ProactiveCachingIncrementalProcessingBinding](../../../2014/analysis-services/dev-guide/proactivecachingincrementalprocessingbinding-data-type-assl.md)|  
|Child elements|[IncrementalProcessingNotification](../../../2014/analysis-services/dev-guide/incrementalprocessingnotification-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.IncrementalProcessingNotificationCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  