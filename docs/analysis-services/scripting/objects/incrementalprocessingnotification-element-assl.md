---
title: "IncrementalProcessingNotification Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "IncrementalProcessingNotification Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "IncrementalProcessingNotification element"
ms.assetid: bfc9b0a4-4043-4aaf-82d9-67e7f1d1eb81
caps.latest.revision: 16
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# IncrementalProcessingNotification Element (ASSL)
  Contains information for the [ProactiveCaching](../../../analysis-services/scripting/objects/proactivecaching-element-assl.md) element about a query to execute to determine the progress of incremental processing.  
  
## Syntax  
  
```xml  
  
<IncrementalProcessingNotifications>  
   <IncrementalProcessingNotification xsi:type="IncrementalProcessingNotification">...</IncrementalProcessingNotification>  
</IncrementalProcessingNotifications>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[IncrementalProcessingNotification](../../../analysis-services/scripting/data-type/incrementalprocessingnotification-data-type-assl.md)|  
|Default value|None|  
|Cardinality|1-n: Required element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[IncrementalProcessingNotifications](../../../analysis-services/scripting/collections/incrementalprocessingnotifications-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.IncrementalProcessingNotification>.  
  
## See Also  
 [ProactiveCachingQueryBinding Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/proactivecachingquerybinding-data-type-assl.md)   
 [ProactiveCaching Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/proactivecaching-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  