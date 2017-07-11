---
title: "CubeHierarchy Data Type (ASSL) | Microsoft Docs"
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
  - "CubeHierarchy Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "CubeHierarchy"
helpviewer_keywords: 
  - "CubeHierarchy data type"
ms.assetid: cd633409-0c14-4dd9-97cc-3d30e25df24f
caps.latest.revision: 44
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# CubeHierarchy Data Type (ASSL)
  Defines a primitive data type that represents information about a [Hierarchy](../../../analysis-services/scripting/objects/hierarchy-element-assl.md) element in a [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md) element.  
  
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
|Child elements|[Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [Enabled](../../../analysis-services/scripting/properties/enabled-element-assl.md), [HierarchyID](../../../analysis-services/scripting/properties/hierarchyid-element-assl.md), [Name](../../../analysis-services/scripting/properties/name-element-assl.md), [OptimizedState](../../../analysis-services/scripting/properties/optimizedstate-element-assl.md), [Visible](../../../analysis-services/scripting/properties/visible-element-assl.md)|  
|Derived elements|[Hierarchy](../../../analysis-services/scripting/objects/hierarchy-element-assl.md) ([Hierarchies](../../../analysis-services/scripting/collections/hierarchies-element-assl.md) collection of [CubeDimension](../../../analysis-services/scripting/data-type/cubedimension-data-type-assl.md))|  
  
## Remarks  
 This data type has no restrictions and can be used under any deployment mode: 0-Multidimensional and Data Mining, 1-SharePoint, and 2-Tabular.  
  
 In SQL Server 2016 Analysis Services and later, the *Enabled* property cannot be set to **False** for *CubeHierarchy*.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CubeHierarchy>.  
  
## See Also  
 [Discover Method &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-methods-discover.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  