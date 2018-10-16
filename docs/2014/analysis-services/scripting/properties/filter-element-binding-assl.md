---
title: "Filter Element (Binding) (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Filter Element (Binding)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "filter"
helpviewer_keywords: 
  - "Filter element"
ms.assetid: 3d4cd169-2903-4266-8541-540ece424b7b
author: minewiskan
ms.author: owend
manager: craigg
---
# Filter Element (Binding) (ASSL)
  Contains a Multidimensional Expressions (MDX) expression that filters the contents of the parent element.  
  
## Syntax  
  
```xml  
  
<CubeDimensionBinding> <!-- or MeasureGroupBinding -->  
   ...  
   <Filter>...</Filter>  
   ...  
</CubeDimensionBinding>  
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
|Parent elements|[CubeDimensionBinding](../data-type/binding-data-type-assl.md), [MeasureGroupBinding](../data-type/measuregroupbinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 For more information about the `Binding` type, including tables of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] Scripting Language (ASSL) objects of the `Binding` type and the inheritance hierarchy of `Binding` types, see [Binding Data Type &#40;ASSL&#41;](../data-type/binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The elements that correspond to the parents of `Filter` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CubeDimensionBinding> and <xref:Microsoft.AnalysisServices.MeasureGroupBinding>.  
  
## See Also  
 [Binding Data Type &#40;ASSL&#41;](../data-type/binding-data-type-assl.md)   
 [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
