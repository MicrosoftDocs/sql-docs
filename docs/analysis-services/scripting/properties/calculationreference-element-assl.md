---
title: "CalculationReference Element (ASSL) | Microsoft Docs"
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
# CalculationReference Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Contains the name of the named set or calculated cell referenced by the [CalculationProperty](../../../analysis-services/scripting/objects/calculationproperty-element-assl.md).  
  
## Syntax  
  
```xml  
  
<CalculationProperty>  
   ...  
   <CalculationReference>...</CalculationReference>  
   ...  
</CalculationProperty>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[CalculationProperty](../../../analysis-services/scripting/objects/calculationproperty-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 If the value of the **CalculationReference** does not match the name of an existing named set or calculated cell definition, the **CalculationReference** is ignored.  
  
 The element that corresponds to the parent of **CalculationReference** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CalculationProperty>.  
  
## See Also  
 [CalculationProperties Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/calculationproperties-element-assl.md)   
 [MdxScript Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/mdxscript-element-assl.md)   
 [MdxScripts Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/mdxscripts-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
