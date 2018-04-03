---
title: "AttributeHierarchyVisible Element (ASSL) | Microsoft Docs"
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
  - "AttributeHierarchyVisible Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AttributeHierarchyVisible"
helpviewer_keywords: 
  - "AttributeHierarchyVisible element"
ms.assetid: a3289a9a-dbd6-43e8-a7ca-ee8a1da92a32
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# AttributeHierarchyVisible Element (ASSL)
  Determines whether the attribute hierarchy is visible to client applications.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute> <!-- or CubeAttribute or PerspectiveAttribute -->  
   ...  
   <AttributeHierarchyVisible>...</AttributeHierarchyVisible>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|`True`|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[CubeAttribute](../../../2014/analysis-services/dev-guide/cubeattribute-data-type-assl.md), [DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md), [PerspectiveAttribute](../../../2014/analysis-services/dev-guide/perspectiveattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `AttributeHierarchyVisible` element determines whether the attribute hierarchy associated with the attribute is visible to client applications. If this element is set to `False`, the attribute hierarchy can still be used to create user-defined hierarchies and can be referenced by Multidimensional Expressions (MDX) statements.  
  
 The elements that correspond to the parents of `AttributeHierarchyVisible` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CubeAttribute>, <xref:Microsoft.AnalysisServices.DimensionAttribute>, and <xref:Microsoft.AnalysisServices.PerspectiveAttribute>.  
  
## See Also  
 [AttributeHierarchyEnabled Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/attributehierarchyenabled-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  