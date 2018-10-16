---
title: "HierarchyID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "HierarchyID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "HierarchyID"
helpviewer_keywords: 
  - "HierarchyID element"
ms.assetid: 6effe47d-e598-41f8-993a-b2bce49fd7dd
author: minewiskan
ms.author: owend
manager: craigg
---
# HierarchyID Element (ASSL)
  Contains the identifier (ID) for a [CubeHierarchy](../data-type/hierarchy-data-type-assl.md), [MeasureGroupHierarchy](../data-type/measuregrouphierarchy-data-type-assl.md), or [PerspectiveHierarchy](../data-type/perspectivehierarchy-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<CubeHierarchy> <!-- or MeasureGroupHierarchy, PerspectiveHierarchy -->  
      ...  
   <HierarchyID>...</HierarchyID>  
      ...  
</CubeHierarchy>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CubeHierarchy](../data-type/hierarchy-data-type-assl.md), [MeasureGroupHierarchy](../data-type/measuregrouphierarchy-data-type-assl.md), [PerspectiveHierarchy](../data-type/perspectivehierarchy-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of `HierarchyID` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.CubeHierarchy> and <xref:Microsoft.AnalysisServices.PerspectiveHierarchy>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
