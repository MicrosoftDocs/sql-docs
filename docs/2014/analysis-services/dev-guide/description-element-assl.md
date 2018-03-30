---
title: "Description Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Description Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Description"
helpviewer_keywords: 
  - "Description element"
ms.assetid: 34d67e7c-e79a-429b-8cc3-6ca13b9cf9c3
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Description Element (ASSL)
  Contains the description of the parent element.  
  
## Syntax  
  
```xml  
  
<Action> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <Description>...</Description>  
   ...  
</Action>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Action](../../../2014/analysis-services/dev-guide/action-element-assl.md), [Aggregation](../../../2014/analysis-services/dev-guide/aggregation-element-assl.md), [AggregationDesign](../../../2014/analysis-services/dev-guide/aggregationdesign-element-assl.md), [Assembly](../../../2014/analysis-services/dev-guide/assembly-element-assl.md), [AttributePermission](../../../2014/analysis-services/dev-guide/attributepermission-element-assl.md), [CalculationProperty](../../../2014/analysis-services/dev-guide/calculationproperty-element-assl.md), [CellPermission](../../../2014/analysis-services/dev-guide/cellpermission-element-assl.md), [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md), [CubeDimensionPermission](../../../2014/analysis-services/dev-guide/cubedimensionpermission-data-type-assl.md), [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md), [DataSource](../../../2014/analysis-services/dev-guide/datasource-element-assl.md), [DataSourceView](../../../2014/analysis-services/dev-guide/datasourceview-element-assl.md), [Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md), [DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md), [Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md), [Kpi](../../../2014/analysis-services/dev-guide/kpi-element-assl.md), [Level](../../../2014/analysis-services/dev-guide/level-element-assl.md), [MdxScript](../../../2014/analysis-services/dev-guide/mdxscript-element-assl.md), [Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md), [MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md), [MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md), [MiningModelColumn](../../../2014/analysis-services/dev-guide/miningmodelcolumn-data-type-assl.md), [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md), [MiningStructureColumn](../../../2014/analysis-services/dev-guide/miningstructurecolumn-data-type-assl.md), [Partition](../../../2014/analysis-services/dev-guide/partition-element-assl.md), [Permission](../../../2014/analysis-services/dev-guide/permission-data-type-assl.md), [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md), [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md), [Server](../../../2014/analysis-services/dev-guide/server-element-assl.md), [Trace](../../../2014/analysis-services/dev-guide/trace-element-assl.md), [Translation](../../../2014/analysis-services/dev-guide/translation-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of a `Description` element has the following restrictions:  
  
-   The value cannot contain leading or trailing spaces. If leading or trailing spaces are included in the value of a `Description` element, those spaces will be implicitly removed by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
-   The value cannot contain control characters. If control characters are included in the value of a `Description` element, those characters will be implicitly removed by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
## See Also  
 [Name Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/name-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  