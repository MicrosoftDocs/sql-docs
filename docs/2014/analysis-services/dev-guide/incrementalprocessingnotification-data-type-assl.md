---
title: "IncrementalProcessingNotification Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "IncrementalProcessingNotification Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "IncrementalProcessingNotification data type"
ms.assetid: 66e27f92-65c1-4a34-b9c2-bfbb5aeb7d7c
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# IncrementalProcessingNotification Data Type (ASSL)
  Defines a derived data type that represents information for the [ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md) element about the query to execute to determine the progress of incremental processing.  
  
## Syntax  
  
```xml  
  
<IncrementalProcessingNotification>  
   <!-- The following elements extend QueryNotification -->  
   <TableID>...</TableID>  
   <ProcessingQuery>...</ProcessingQuery>  
</IncrementalProcessingNotification>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[QueryNotification](../../../2014/analysis-services/dev-guide/querynotification-element-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[ProcessingQuery](../../../2014/analysis-services/dev-guide/processingquery-element-assl.md), [TableID](../../../2014/analysis-services/dev-guide/tableid-element-assl.md)|  
|Derived elements|None|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.IncrementalProcessingNotification>.  
  
## See Also  
 [ProactiveCachingQueryBinding Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/proactivecachingquerybinding-data-type-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  