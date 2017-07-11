---
title: "MeasureGroupHierarchy Data Type (ASSL) | Microsoft Docs"
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
  - "MeasureGroupHierarchy Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "MeasureGroupHierarchy"
helpviewer_keywords: 
  - "MeasureGroupHierarchy data type"
ms.assetid: 63c2fd97-d7ad-4715-8c49-24d684bc92d7
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Child elements|[Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [HierarchyID](../../../analysis-services/scripting/properties/hierarchyid-element-assl.md)|  
|Derived elements|[Hierarchy](../../../analysis-services/scripting/objects/hierarchy-element-assl.md) ([Hierarchies](../../../analysis-services/scripting/collections/hierarchies-element-assl.md) collection of [RegularMeasureGroupDimension](../../../analysis-services/scripting/data-type/regularmeasuregroupdimension-data-type-assl.md))|  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  