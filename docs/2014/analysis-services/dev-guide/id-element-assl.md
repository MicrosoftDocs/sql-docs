---
title: "ID Element (ASSL) | Microsoft Docs"
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
  - "ID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ID"
helpviewer_keywords: 
  - "ID element"
ms.assetid: ea3ce0f4-9084-45d0-8150-73afb7005af2
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ID Element (ASSL)
  Contains the unique identifier (ID) of the parent element.  
  
## Syntax  
  
```xml  
  
<Action> <!-- or one of the elements listed in the Element Relationships table -->  
   ...  
   <ID>...</ID>  
   ...  
</Action>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (up to 100 characters)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Action](../../../2014/analysis-services/dev-guide/action-element-assl.md), [Aggregation](../../../2014/analysis-services/dev-guide/aggregation-element-assl.md), [AggregationDesign](../../../2014/analysis-services/dev-guide/aggregationdesign-element-assl.md), [Assembly](../../../2014/analysis-services/dev-guide/assembly-element-assl.md), [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md), [CubeBinding](../../../2014/analysis-services/dev-guide/cubebinding-data-type-out-of-line-assl.md), [CubeDimension](../../../2014/analysis-services/dev-guide/cubedimension-data-type-assl.md), [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md), [DataSource](../../../2014/analysis-services/dev-guide/datasource-element-assl.md), [DataSourceView](../../../2014/analysis-services/dev-guide/datasourceview-element-assl.md), [Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md), [DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md), [Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md), [Kpi](../../../2014/analysis-services/dev-guide/kpi-element-assl.md), [Level](../../../2014/analysis-services/dev-guide/level-element-assl.md), [MdxScript](../../../2014/analysis-services/dev-guide/mdxscript-element-assl.md), [Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md), [MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md), [MeasureGroupBinding](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-assl.md), [MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md), [MiningModelColumn](../../../2014/analysis-services/dev-guide/miningmodelcolumn-data-type-assl.md), [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md), [MiningStructureColumn](../../../2014/analysis-services/dev-guide/miningstructurecolumn-data-type-assl.md), [Partition](../../../2014/analysis-services/dev-guide/partition-element-assl.md), [Permission](../../../2014/analysis-services/dev-guide/permission-data-type-assl.md), [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md), [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md), [Server](../../../2014/analysis-services/dev-guide/server-element-assl.md), [Trace](../../../2014/analysis-services/dev-guide/trace-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 Every major object in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] has an `ID` element as a property. The value of an `ID` element has the following restrictions:  
  
-   The value cannot contain leading or trailing spaces. [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] will implicitly remove leading or trailing spaces from the value of an `ID` element.  
  
-   The value cannot contain control characters. [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] will implicitly remove control characters from the value of an `ID` element.  
  
-   The following reserved values cannot be used:  
  
    -   AUX  
  
    -   CLOCK$  
  
    -   COM1 through COM9 (COM1, COM2, COM3, and so on)  
  
    -   CON  
  
    -   LPT1 through LPT9 (LPT1, LPT2, LPT3, and so on)  
  
    -   NUL  
  
    -   PRN  
  
 The following table lists additional characters that cannot be used within the value of an `ID` element, depending on the parent element.  
  
|Parent element|Characters|  
|--------------------|----------------|  
|[Server](../../../2014/analysis-services/dev-guide/server-element-assl.md)|The value must follow the rules for [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows computer names. (IP addresses are not valid.)|  
|[DataSource](../../../2014/analysis-services/dev-guide/datasource-element-assl.md)|:/\\*&#124;?"()[]{}<>|  
|[Level](../../../2014/analysis-services/dev-guide/level-element-assl.md), [Attribute Element](../../../2014/analysis-services/dev-guide/attribute-element-assl.md)|.,;'`:/\\*&#124;?"&%$!+=[]{}<>|  
|All other parent elements|.,;'`:/\\*&#124;?"&%$!+=()[]{}<>|  
  
## See Also  
 [Name Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/name-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  