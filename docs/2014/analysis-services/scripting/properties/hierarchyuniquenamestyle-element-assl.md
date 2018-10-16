---
title: "HierarchyUniqueNameStyle Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "HierarchyUniqueNameStyle Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "HierarchyUniqueNameStyle element"
ms.assetid: 2ac57825-e9e5-4ec4-9856-fa2326d2c43f
author: minewiskan
ms.author: owend
manager: craigg
---
# HierarchyUniqueNameStyle Element (ASSL)
  Determines how unique names are generated for hierarchies that are contained within the [CubeDimension](../data-type/dimension-data-type-assl.md).  
  
## Syntax  
  
```xml  
  
<CubeDimension>  
   ...  
   <HierarchyUniqueNameStyle>...</HierarchyUniqueNameStyle>  
   ...  
</CubeDimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*IncludeDimensionName*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[CubeDimension](../data-type/dimension-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*IncludeDimensionName*|The name of the dimension is included as part of the name of the hierarchy.|  
|*ExcludeDimensionName*|The name of the dimension is not included as part of the name of the hierarchy.|  
  
 The element that corresponds to the parent of `HierarchyUniqueNameStyle` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CubeDimension>.  
  
## See Also  
 [Cube Element &#40;ASSL&#41;](../objects/cube-element-assl.md)   
 [Dimension Element &#40;ASSL&#41;](../objects/dimension-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
