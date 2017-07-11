---
title: "AttributeHierarchyVisible Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "AttributeHierarchyVisible Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "AttributeHierarchyVisible"
helpviewer_keywords: 
  - "AttributeHierarchyVisible element"
ms.assetid: a3289a9a-dbd6-43e8-a7ca-ee8a1da92a32
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Default value|**True**|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[CubeAttribute](../../../analysis-services/scripting/data-type/cubeattribute-data-type-assl.md), [DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md), [PerspectiveAttribute](../../../analysis-services/scripting/data-type/perspectiveattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The **AttributeHierarchyVisible** element determines whether the attribute hierarchy associated with the attribute is visible to client applications. If this element is set to **False**, the attribute hierarchy can still be used to create user-defined hierarchies and can be referenced by Multidimensional Expressions (MDX) statements.  
  
 The elements that correspond to the parents of **AttributeHierarchyVisible** in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CubeAttribute>, <xref:Microsoft.AnalysisServices.DimensionAttribute>, and <xref:Microsoft.AnalysisServices.PerspectiveAttribute>.  
  
## See Also  
 [AttributeHierarchyEnabled Element &#40;ASSL&#41;](../../../analysis-services/scripting/properties/attributehierarchyenabled-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  