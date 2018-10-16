---
title: "ActionID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ActionID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ActionID"
helpviewer_keywords: 
  - "ActionID element"
ms.assetid: 2c9c66b2-a7ea-4874-a0ed-020ce3feab20
author: minewiskan
ms.author: owend
manager: craigg
---
# ActionID Element (ASSL)
  Contains the name of an [Action](../objects/action-element-assl.md) element defined on a [Cube](../objects/cube-element-assl.md) element that is made available in a [Perspective](../objects/perspective-element-assl.md) element as a [PerspectiveAction](../data-type/action-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<PerspectiveAction>  
   <ActionID>...</ActionID  
</PerspectiveAction>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[PerspectiveAction](../data-type/action-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `ActionID` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveAction>.  
  
## See Also  
 [Actions Element &#40;ASSL&#41;](../collections/actions-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
