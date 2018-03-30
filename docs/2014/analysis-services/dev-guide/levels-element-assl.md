---
title: "Levels Element (ASSL) | Microsoft Docs"
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
  - "Levels Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Levels"
helpviewer_keywords: 
  - "Levels element"
ms.assetid: a9dd4890-a5da-48e7-9bbf-f857107cde8d
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Levels Element (ASSL)
  Contains the collection of [Level](../../../2014/analysis-services/dev-guide/level-element-assl.md) elements in a [Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Hierarchy>  
      ...  
   <Levels>  
      <Level>...</Level>  
   </Levels>  
   ...  
</Hierarchy>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md)|  
|Child elements|[Level](../../../2014/analysis-services/dev-guide/level-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.LevelCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  