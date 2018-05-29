---
title: "AssociatedMeasureGroupID Element (ASSL) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: assl
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# AssociatedMeasureGroupID Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Contains the ID of the [MeasureGroup](../../../analysis-services/scripting/objects/measuregroup-element-assl.md) element associated with a [CalculationProperty](../../../analysis-services/scripting/objects/calculationproperty-element-assl.md) element or a [Kpi](../../../analysis-services/scripting/objects/kpi-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<CalculationProperty> <!-- or Kpi -->  
   ...  
   <AssociatedMeasureGroupID>...</AssociatedMeasureGroupID>  
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
|Parent element|[CalculationProperty](../../../analysis-services/scripting/objects/calculationproperty-element-assl.md), [Kpi](../../../analysis-services/scripting/objects/kpi-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 When applied to **CalculationProperty** elements, the **AssociatedMeasureGroupID** property applies to elements with a [CalculationType](../../../analysis-services/scripting/properties/calculationtype-element-assl.md) of *Member*.  
  
 The elements that correspond to the parents of **AssociatedMeasureGroupID** in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CalculationProperty> and <xref:Microsoft.AnalysisServices.Kpi>.  
  
## See Also  
 [CalculationProperties Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/calculationproperties-element-assl.md)   
 [MdxScript Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/mdxscript-element-assl.md)   
 [MdxScripts Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/mdxscripts-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
