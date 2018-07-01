---
title: "IgnoreUnrelatedDimensions Element (ASSL) | Microsoft Docs"
ms.date: 5/8/2018
ms.prod: sql
ms.custom: assl
ms.reviewer: owend
ms.technology: analysis-services
ms.topic: reference
author: minewiskan
ms.author: owend
manager: kfile
---
# IgnoreUnrelatedDimensions Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Determines whether unrelated dimensions are forced to their top level when members of dimensions that are unrelated to the measure group are included in a query.  
  
## Syntax  
  
```xml  
  
<MeasureGroup>  
   ...  
   <IgnoreUnrelatedDimensions>...</IgnoreUnrelatedDimensions>  
   ...  
</MeasureGroup>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|True|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[MeasureGroup](../../../analysis-services/scripting/objects/measuregroup-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 When **IgnoreUnrelatedDimensions** is **true**, unrelated dimensions are forced to their top level; when the value is **false**, dimensions are not forced to their top level. This property is similar to the Multidimensional Expressions (MDX) [ValidMeasure](../../../mdx/validmeasure-mdx.md) function.  
  
 The element that corresponds to the parent of **IgnoreUnrelatedDimensions** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MeasureGroup>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
