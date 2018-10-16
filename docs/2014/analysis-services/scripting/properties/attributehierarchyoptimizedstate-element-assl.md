---
title: "AttributeHierarchyOptimizedState Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AttributeHierarchyOptimizedState Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AttributeHierarchyOptimizedState"
helpviewer_keywords: 
  - "AttributeHierarchyOptimizedState element"
ms.assetid: d87148c8-2011-45ae-94c3-851f48babc5f
author: minewiskan
ms.author: owend
manager: craigg
---
# AttributeHierarchyOptimizedState Element (ASSL)
  Determines the level of optimization applied to the attribute hierarchy.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute> <!-- or CubeAttribute -->  
   ...  
   <AttributeHierarchyOptimizedState>...  
   </AttributeHierarchyOptimizedState>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*FullyOptimized*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[CubeAttribute](../data-type/cubeattribute-data-type-assl.md), [DimensionAttribute](../data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*FullyOptimized*|The instance builds indexes for the attribute hierarchy to improve query performance.|  
|*NotOptimized*|No additional indexes are built by the instance.|  
  
 The enumeration corresponding to the allowed values for `AttributeHierarchyOptimizedState` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.OptimizationType>. The elements that correspond to the parents of `AttributeHierarchyOptimizedState` in the AMO object model are <xref:Microsoft.AnalysisServices.CubeAttribute> and <xref:Microsoft.AnalysisServices.DimensionAttribute>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
