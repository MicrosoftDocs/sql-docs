---
title: "Action Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Action Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Action"
helpviewer_keywords: 
  - "Action element"
ms.assetid: aaee06a2-91c6-4007-b787-79cb08d63c77
author: minewiskan
ms.author: owend
manager: craigg
---
# Action Element (ASSL)
  Contains information about an action available in a [Cube](cube-element-assl.md) element or a [Perspective](perspective-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Actions>  
   <Action xsi:type="DrillThroughAction">...</Action> <!-- ancestor: Cube -->  
   <Action xsi:type="ReportAction">...</Action> <!-- ancestor: Cube -->  
   <Action xsi:type="StandardAction">...</Action> <!-- ancestor: Cube -->  
   <!-- or -->  
   <Action xsi:type="PerspectiveAction">...</Action> <!-- ancestor: Perspective -->  
</Actions>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
|Ancestor or Parent|Data Type|  
|------------------------|---------------|  
|[Cube](../data-type/action-data-type-assl.md), [ReportAction](../data-type/reportaction-data-type-assl.md), [StandardAction](../data-type/standardaction-data-type-assl.md)|  
|[Perspective](../data-type/perspectiveaction-data-type-assl.md)|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Actions](../collections/actions-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Action>.  
  
## See Also  
 [Cube Element &#40;ASSL&#41;](cube-element-assl.md)   
 [Perspective Element &#40;ASSL&#41;](perspective-element-assl.md)   
 [PerspectiveAction Data Type &#40;ASSL&#41;](../data-type/perspectiveaction-data-type-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
