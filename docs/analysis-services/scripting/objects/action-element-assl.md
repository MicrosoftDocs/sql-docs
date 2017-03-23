---
title: "Action Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Action Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Action"
helpviewer_keywords: 
  - "Action element"
ms.assetid: aaee06a2-91c6-4007-b787-79cb08d63c77
caps.latest.revision: 41
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Action Element (ASSL)
  Contains information about an action available in a [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md) element or a [Perspective](../../../analysis-services/scripting/objects/perspective-element-assl.md) element.  
  
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
|Data type and length|See the table below.|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
|Ancestor or Parent|Data Type|  
|------------------------|---------------|  
|[Cube](../../../analysis-services/scripting/objects/cube-element-assl.md)|[DrillThroughAction](../../../analysis-services/scripting/data-type/drillthroughaction-data-type-assl.md), [ReportAction](../../../analysis-services/scripting/data-type/reportaction-data-type-assl.md), [StandardAction](../../../analysis-services/scripting/data-type/standardaction-data-type-assl.md)|  
|[Perspective](../../../analysis-services/scripting/objects/perspective-element-assl.md)|[PerspectiveAction](../../../analysis-services/scripting/data-type/perspectiveaction-data-type-assl.md)|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Actions](../../../analysis-services/scripting/collections/actions-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Action>.  
  
## See Also  
 [Cube Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/cube-element-assl.md)   
 [Perspective Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/perspective-element-assl.md)   
 [PerspectiveAction Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/perspectiveaction-data-type-assl.md)   
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  