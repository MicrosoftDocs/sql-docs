---
title: "CubeHierarchy Data Type (ASSL) | Microsoft Docs"
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
caps.latest.revision: 44
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# CubeHierarchy Data Type (ASSL)
  Defines a primitive data type that represents information about a [Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md) element in a [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) element.  
  
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
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Enabled](../../../2014/analysis-services/dev-guide/enabled-element-assl.md), [HierarchyID](../../../2014/analysis-services/dev-guide/hierarchyid-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [OptimizedState](../../../2014/analysis-services/dev-guide/optimizedstate-element-assl.md), [Visible](../../../2014/analysis-services/dev-guide/visible-element-assl.md)|  
|Derived elements|[Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md) ([Hierarchies](../../../2014/analysis-services/dev-guide/hierarchies-element-assl.md) collection of [CubeDimension](../../../2014/analysis-services/dev-guide/cubedimension-data-type-assl.md))|  
  
## Remarks  
 This data type has no restrictions and can be used under any deployment mode: 0-Multidimensional and Data Mining, 1-SharePoint, and 2-Tabular.  
  
 In [!INCLUDE[ssASCurrent](../../includes/ssascurrent-md.md)], the *Enabled* property cannot be set to `False` for *CubeHierarchy*.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CubeHierarchy>.  
  
## See Also  
 [Discover Method &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/discover-method-xmla.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  