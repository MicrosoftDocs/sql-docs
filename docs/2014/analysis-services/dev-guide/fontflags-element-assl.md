---
title: "FontFlags Element (ASSL) | Microsoft Docs"
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
  - "FontFlags Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "FontFlags"
helpviewer_keywords: 
  - "FontFlags element"
ms.assetid: ea608da9-ab05-42ab-8872-c52cd9f3f546
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# FontFlags Element (ASSL)
  Describes font-related display characteristics of the [CalculationProperty](../../../2014/analysis-services/dev-guide/calculationproperty-element-assl.md) or [Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md) parent element.  
  
## Syntax  
  
```xml  
  
<CalculationProperty> <!-- or Measure -->  
   ...  
      <FontFlags>...</FontFlags>  
      ...  
</CalculationProperty>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CalculationProperty](../../../2014/analysis-services/dev-guide/calculationproperty-element-assl.md), [Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `FontFlags` property contains a Multidimensional Expressions (MDX) expression and applies to `CalculationProperty` elements that have a [CalculationType](../../../2014/analysis-services/dev-guide/calculationtype-element-assl.md) of *Member* or *Cells*.  
  
 The elements that correspond to the parents of `FontFlags` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CalculationProperty> and <xref:Microsoft.AnalysisServices.Measure>.  
  
## See Also  
 [CalculationProperties Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/calculationproperties-element-assl.md)   
 [MdxScript Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/mdxscript-element-assl.md)   
 [MdxScripts Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/mdxscripts-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  