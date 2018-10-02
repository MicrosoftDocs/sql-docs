---
title: "ID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
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
|Parent elements|[Action](../objects/action-element-assl.md), [Aggregation](../objects/aggregation-element-assl.md), [AggregationDesign](../objects/aggregationdesign-element-assl.md), [Assembly](../objects/assembly-element-assl.md), [Cube](../objects/cube-element-assl.md), [CubeBinding](../data-type/cubebinding-data-type-out-of-line-assl.md), [CubeDimension](../data-type/dimension-data-type-assl.md), [Database](../objects/database-element-assl.md), [DataSource](../objects/datasource-element-assl.md), [DataSourceView](../objects/datasourceview-element-assl.md), [Dimension](../objects/dimension-element-assl.md), [DimensionAttribute](../data-type/dimensionattribute-data-type-assl.md), [Hierarchy](../objects/hierarchy-element-assl.md), [Kpi](../objects/kpi-element-assl.md), [Level](../objects/level-element-assl.md), [MdxScript](../objects/mdxscript-element-assl.md), [Measure](../objects/measure-element-assl.md), [MeasureGroup](../objects/group-element-assl.md), [MeasureGroupBinding](../data-type/binding-data-type-assl.md), [MiningModel](../objects/miningmodel-element-assl.md), [MiningModelColumn](../data-type/miningmodelcolumn-data-type-assl.md), [MiningStructure](../objects/miningstructure-element-assl.md), [MiningStructureColumn](../data-type/miningstructurecolumn-data-type-assl.md), [Partition](../objects/partition-element-assl.md), [Permission](../data-type/permission-data-type-assl.md), [Perspective](../objects/perspective-element-assl.md), [Role](../objects/role-element-assl.md), [Server](../objects/server-element-assl.md), [Trace](../objects/trace-element-assl.md)|  
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
|[Server](../objects/server-element-assl.md)|The value must follow the rules for [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows computer names. (IP addresses are not valid.)|  
|[DataSource](../objects/datasource-element-assl.md)|:/\\*&#124;?"()[]{}<>|  
|[Level](../objects/level-element-assl.md), [Attribute Element](../objects/attribute-element-assl.md)|.,;'`:/\\*&#124;?"&%$!+=[]{}<>|  
|All other parent elements|.,;'`:/\\*&#124;?"&%$!+=()[]{}<>|  
  
## See Also  
 [Name Element &#40;ASSL&#41;](name-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
