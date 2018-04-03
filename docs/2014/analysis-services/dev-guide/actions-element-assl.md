---
title: "Actions Element (ASSL) | Microsoft Docs"
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
  - "Actions Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Actions"
helpviewer_keywords: 
  - "Actions element"
ms.assetid: 100a4209-2c22-4902-a8ca-f2bd99bf8fbb
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Actions Element (ASSL)
  Contains the collection of actions for a [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) or [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Cube> <!-- or Perspective -->  
   ...  
   <Actions>  
      <Action xsi:type="DrillThroughAction">...</Action> <!-- ancestor: Cube -->  
      <Action xsi:type="ReportAction">...</Action> <!-- ancestor: Cube -->  
      <Action xsi:type="StandardAction">...</Action> <!-- ancestor: Cube -->  
      <!-- or -->  
      <Action xsi:type="PerspectiveAction">...</Action> <!-- ancestor: Perspective -->  
      </Actions>  
      ...  
</Cube>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None (collection)|  
|Default value|None (collection)|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md), [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md)|  
  
|Ancestor or Parent|Child Elements|  
|------------------------|--------------------|  
|[Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md)|[DrillThroughAction](../../../2014/analysis-services/dev-guide/drillthroughaction-data-type-assl.md), [ReportAction](../../../2014/analysis-services/dev-guide/reportaction-data-type-assl.md), [StandardAction](../../../2014/analysis-services/dev-guide/standardaction-data-type-assl.md)|  
|[Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md)|[PerspectiveAction](../../../2014/analysis-services/dev-guide/perspectiveaction-data-type-assl.md)|  
  
## Remarks  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.ActionCollection> and <xref:Microsoft.AnalysisServices.PerspectiveActionCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  