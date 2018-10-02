---
title: "PerspectiveHierarchy Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "PerspectiveHierarchy Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "PerspectiveHierarchy"
helpviewer_keywords: 
  - "PerspectiveHierarchy data type"
ms.assetid: 30366bd0-ee1c-4940-8c1f-ca5e0dd5fe4b
author: minewiskan
ms.author: owend
manager: craigg
---
# PerspectiveHierarchy Data Type (ASSL)
  Defines a primitive data type that represents information about a hierarchy in a [PerspectiveDimension](dimension-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<PerspectiveHierarchy>  
   <HierarchyID>...</HierarchyID>  
      <Annotations>...</Annotations>  
</PerspectiveHierarchy>  
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
|Child elements|[Annotations](../collections/annotations-element-assl.md), [HierarchyID](../properties/id-element-assl.md)|  
|Derived elements|[Hierarchy](../objects/hierarchy-element-assl.md) ([Hierarchies](../collections/hierarchies-element-assl.md) collection of [PerspectiveDimension](dimension-data-type-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveHierarchy>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
