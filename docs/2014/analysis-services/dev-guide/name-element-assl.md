---
title: "Name Element (ASSL) | Microsoft Docs"
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
  - "Name Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Name"
helpviewer_keywords: 
  - "Name element"
ms.assetid: caf2af86-5f9c-4e14-8168-f3a79248b4fe
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Name Element (ASSL)
  Contains the name of the parent element.  
  
## Syntax  
  
```xml  
  
<Action> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <Name>...</Name>  
   ...  
</Action>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (up to 100 characters)|  
|Default value|Varies|  
|Cardinality|1-1: Required element that occurs once and only once|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Action](../../../2014/analysis-services/dev-guide/action-element-assl.md), [Aggregation](../../../2014/analysis-services/dev-guide/aggregation-element-assl.md), [AggregationDesign](../../../2014/analysis-services/dev-guide/aggregationdesign-element-assl.md), [AlgorithmParameter](../../../2014/analysis-services/dev-guide/algorithmparameter-element-assl.md), [Annotation](../../../2014/analysis-services/dev-guide/annotation-element-assl.md), [Assembly](../../../2014/analysis-services/dev-guide/assembly-element-assl.md), [ClrAssemblyFile](../../../2014/analysis-services/dev-guide/clrassemblyfile-data-type-assl.md), [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md), [CubeDimension](../../../2014/analysis-services/dev-guide/cubedimension-data-type-assl.md), [CubeHierarchy](../../../2014/analysis-services/dev-guide/cubehierarchy-data-type-assl.md), [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md), [DataSource](../../../2014/analysis-services/dev-guide/datasource-element-assl.md), [DataSourceView](../../../2014/analysis-services/dev-guide/datasourceview-element-assl.md), [Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md), [DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md), [Group](../../../2014/analysis-services/dev-guide/group-element-assl.md), [Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md), [Kpi](../../../2014/analysis-services/dev-guide/kpi-element-assl.md), [Level](../../../2014/analysis-services/dev-guide/level-element-assl.md), [MdxScript](../../../2014/analysis-services/dev-guide/mdxscript-element-assl.md), [Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md), [MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md), [MemberProperty](../../../2014/analysis-services/dev-guide/attributerelationship-element-assl.md), [MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md), [MiningModelColumn](../../../2014/analysis-services/dev-guide/miningmodelcolumn-data-type-assl.md), [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md), [MiningStructureColumn](../../../2014/analysis-services/dev-guide/miningstructurecolumn-data-type-assl.md), [Partition](../../../2014/analysis-services/dev-guide/partition-element-assl.md), [Permission](../../../2014/analysis-services/dev-guide/permission-data-type-assl.md), [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md), [PerspectiveCalculation](../../../2014/analysis-services/dev-guide/perspectivecalculation-data-type-assl.md), [ReportFormatParameter](../../../2014/analysis-services/dev-guide/reportformatparameter-element-assl.md), [ReportParameter](../../../2014/analysis-services/dev-guide/reportparameter-element-assl.md), [Role](../../../2014/analysis-services/dev-guide/role-element-assl.md), [Server](../../../2014/analysis-services/dev-guide/server-element-assl.md), [ServerProperty](../../../2014/analysis-services/dev-guide/serverproperty-element-assl.md), [Trace](../../../2014/analysis-services/dev-guide/trace-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 Every element that is used to define an object (an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], a hierarchy, an attribute, and so on) has a `Name` element as a property. The value of a `Name` element has the following restrictions:  
  
-   The value cannot contain leading or trailing spaces. If leading or trailing spaces are included in the value of a `Name` element, those spaces will be implicitly removed by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
-   The value should not contain control characters. The presence of control characters in a name is strongly discouraged and can sometimes result in XML validation errors.  
  
     For objects created using the `GetNewName` method in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], AMO checks for and subsequently removes any control characters, leading spaces, or trailing spaces in the name. For this reason, using `GetNewName` is the recommended approach for setting object names.  
  
     However, if you set the `Name` property directly, the same validation checks are not performed, possibly resulting in XML validation errors. Whether an error actually occurs depends on which control character appears in the name.  
  
     Although control characters should never be used in an object name, Analysis Services does not expressly prevent them. Previous releases of Analysis Services sometimes accepted control characters in an object name. For this reason, [!INCLUDE[ssASCurrent](../../includes/ssascurrent-md.md)] will ignore control characters in an object name to avoid breaking older solutions.  
  
-   The following reserved values cannot be used:  
  
    -   AUX  
  
    -   CLOCK$  
  
    -   COM1 through COM9 (COM1, COM2, COM3, and so on)  
  
    -   CON  
  
    -   LPT1 through LPT9 (LPT1, LPT2, LPT3, and so on)  
  
    -   NUL  
  
    -   PRN  
  
 The following table lists additional characters that cannot be used within the value of a `Name` element, depending on the parent element.  
  
|Parent element|Invalid Characters|  
|--------------------|------------------------|  
|[Server](../../../2014/analysis-services/dev-guide/server-element-assl.md)|The name must follow the rules for [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows computer names. IP addresses are not valid.|  
|[DataSource](../../../2014/analysis-services/dev-guide/datasource-element-assl.md)|:/\\*&#124;?"()[]{}<>|  
|[Level](../../../2014/analysis-services/dev-guide/level-element-assl.md), [Attribute Element](../../../2014/analysis-services/dev-guide/attribute-element-assl.md)|.,;'`:/\\*&#124;?"&%$!+=[]{}<>|  
|All other parent elements|.,;'`:/\\*&#124;?"&%$!+=()[]{}<>|  
  
## See Also  
 [ID Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/id-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  