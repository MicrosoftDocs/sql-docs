---
title: "ActionID Element (ASSL) | Microsoft Docs"
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
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ActionID Element (ASSL)
  Contains the name of an [Action](../../../2014/analysis-services/dev-guide/action-element-assl.md) element defined on a [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) element that is made available in a [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md) element as a [PerspectiveAction](../../../2014/analysis-services/dev-guide/perspectiveaction-data-type-assl.md) element.  
  
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
|Parent elements|[PerspectiveAction](../../../2014/analysis-services/dev-guide/perspectiveaction-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `ActionID` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveAction>.  
  
## See Also  
 [Actions Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/actions-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  