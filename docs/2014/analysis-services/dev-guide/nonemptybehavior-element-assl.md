---
title: "NonEmptyBehavior Element (ASSL) | Microsoft Docs"
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
  - "NonEmptyBehavior Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "NonEmptyBehavior"
helpviewer_keywords: 
  - "NonEmptyBehavior element"
ms.assetid: b4c78af4-b049-4189-a35b-206e3938d1db
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# NonEmptyBehavior Element (ASSL)
  Determines the non-empty behavior associated with the parent of the [CalculationProperty](../../../2014/analysis-services/dev-guide/calculationproperty-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<CalculationProperty>  
  
   <NonEmptyBehavior>...</NonEmptyBehavior>  
  
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
|Parent element|[CalculationProperty](../../../2014/analysis-services/dev-guide/calculationproperty-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `NonEmptyBehavior` property applies to `CalculationProperty` elements with a [CalculationType](../../../2014/analysis-services/dev-guide/calculationtype-element-assl.md) set to *Member*.  
  
 The element that corresponds to the parent of `NonEmptyBehavior` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CalculationProperty>.  
  
## See Also  
 [CalculationProperties Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/calculationproperties-element-assl.md)   
 [MdxScript Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/mdxscript-element-assl.md)   
 [MdxScripts Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/mdxscripts-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  