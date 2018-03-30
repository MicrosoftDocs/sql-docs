---
title: "MeasureGroupHierarchy Data Type (ASSL) | Microsoft Docs"
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
  - "MeasureGroupHierarchy Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MeasureGroupHierarchy"
helpviewer_keywords: 
  - "MeasureGroupHierarchy data type"
ms.assetid: 63c2fd97-d7ad-4715-8c49-24d684bc92d7
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# MeasureGroupHierarchy Data Type (ASSL)
  Defines a primitive data type that represents information about a hierarchy in a measure group.  
  
## Syntax  
  
```xml  
  
<MeasureGroupHierarchy>  
   <HierarchyID>...</HierarchyID>  
      <Annotations>...</Annotations>  
</MeasureGroupHierarchy>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [HierarchyID](../../../2014/analysis-services/dev-guide/hierarchyid-element-assl.md)|  
|Derived elements|[Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md) ([Hierarchies](../../../2014/analysis-services/dev-guide/hierarchies-element-assl.md) collection of [RegularMeasureGroupDimension](../../../2014/analysis-services/dev-guide/regularmeasuregroupdimension-data-type-assl.md))|  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  