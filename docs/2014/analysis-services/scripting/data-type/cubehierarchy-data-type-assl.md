---
title: "CubeHierarchy Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CubeHierarchy Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CubeHierarchy"
helpviewer_keywords: 
  - "CubeHierarchy data type"
ms.assetid: cd633409-0c14-4dd9-97cc-3d30e25df24f
author: minewiskan
ms.author: owend
manager: craigg
---
# CubeHierarchy Data Type (ASSL)
  Defines a primitive data type that represents information about a [Hierarchy](../objects/hierarchy-element-assl.md) element in a [Cube](../objects/cube-element-assl.md) element.  
  
## Syntax  
  
```  
<CubeHierarchy>   <HierarchyID>...</HierarchyID>   <Name>...</Name>   <OptimizedState>...</OptimizedState>   <Visible>...</Visible>   <Enabled>...</Enabled>   <Annotations>...</Annotations></CubeHierarchy>  
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
|Child elements|[Annotations](../collections/annotations-element-assl.md), [Enabled](../properties/enabled-element-assl.md), [HierarchyID](../properties/id-element-assl.md), [Name](../properties/name-element-assl.md), [OptimizedState](../properties/state-element-assl.md), [Visible](../properties/visible-element-assl.md)|  
|Derived elements|[Hierarchy](../objects/hierarchy-element-assl.md) ([Hierarchies](../collections/hierarchies-element-assl.md) collection of [CubeDimension](dimension-data-type-assl.md))|  
  
## Remarks  
 This data type has no restrictions and can be used under any deployment mode: 0-Multidimensional and Data Mining, 1-SharePoint, and 2-Tabular.  
  
 In [!INCLUDE[ssASCurrent](../../../includes/ssascurrent-md.md)], the *Enabled* property cannot be set to `False` for *CubeHierarchy*.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CubeHierarchy>.  
  
## See Also  
 [Discover Method &#40;XMLA&#41;](../../xmla/xml-elements-methods-discover.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
