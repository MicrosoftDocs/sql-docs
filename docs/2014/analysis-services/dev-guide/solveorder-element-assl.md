---
title: "SolveOrder Element (ASSL) | Microsoft Docs"
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
  - "SolveOrder Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "SolveOrder"
helpviewer_keywords: 
  - "SolveOrder element"
ms.assetid: ec43e055-97dd-4f2a-9a7c-2df2e1119e56
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# SolveOrder Element (ASSL)
  Indicates the solve order in which the [CalculationProperty](../../../2014/analysis-services/dev-guide/calculationproperty-element-assl.md) element is applied to a calculated member or calculated cell definition.  
  
## Syntax  
  
```xml  
  
<CalculationProperty>  
   ...  
   <SolveOrder>...</SolveOrder>  
   ...  
</CalculationProperty>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[CalculationProperty](../../../2014/analysis-services/dev-guide/calculationproperty-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `SolveOrder` property applies to `CalculationProperty` elements with a [CalculationType](../../../2014/analysis-services/dev-guide/calculationtype-element-assl.md) of *Member* or *Cells*.  
  
 The element that corresponds to the parent of `SolveOrder` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CalculationProperty>.  
  
## See Also  
 [CalculationProperties Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/calculationproperties-element-assl.md)   
 [MdxScript Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/mdxscript-element-assl.md)   
 [MdxScripts Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/mdxscripts-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  