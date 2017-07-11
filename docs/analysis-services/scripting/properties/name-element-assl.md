---
title: "Name Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Name Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Name"
helpviewer_keywords: 
  - "Name element"
ms.assetid: caf2af86-5f9c-4e14-8168-f3a79248b4fe
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Parent elements|[Action](../../../analysis-services/scripting/objects/action-element-assl.md), [Aggregation](../../../analysis-services/scripting/objects/aggregation-element-assl.md), [AggregationDesign](../../../analysis-services/scripting/objects/aggregationdesign-element-assl.md), [AlgorithmParameter](../../../analysis-services/scripting/objects/algorithmparameter-element-assl.md), [Annotation](../../../analysis-services/scripting/objects/annotation-element-assl.md), [Assembly](../../../analysis-services/scripting/objects/assembly-element-assl.md), [ClrAssemblyFile](../../../analysis-services/scripting/data-type/clrassemblyfile-data-type-assl.md), [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md), [CubeDimension](../../../analysis-services/scripting/data-type/cubedimension-data-type-assl.md), [CubeHierarchy](../../../analysis-services/scripting/data-type/cubehierarchy-data-type-assl.md), [Database](../../../analysis-services/scripting/objects/database-element-assl.md), [DataSource](../../../analysis-services/scripting/objects/datasource-element-assl.md), [DataSourceView](../../../analysis-services/scripting/objects/datasourceview-element-assl.md), [Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md), [DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md), [Group](../../../analysis-services/scripting/objects/group-element-assl.md), [Hierarchy](../../../analysis-services/scripting/objects/hierarchy-element-assl.md), [Kpi](../../../analysis-services/scripting/objects/kpi-element-assl.md), [Level](../../../analysis-services/scripting/objects/level-element-assl.md), [MdxScript](../../../analysis-services/scripting/objects/mdxscript-element-assl.md), [Measure](../../../analysis-services/scripting/objects/measure-element-assl.md), [MeasureGroup](../../../analysis-services/scripting/objects/measuregroup-element-assl.md), [MemberProperty](../../../analysis-services/scripting/objects/attributerelationship-element-assl.md), [MiningModel](../../../analysis-services/scripting/objects/miningmodel-element-assl.md), [MiningModelColumn](../../../analysis-services/scripting/data-type/miningmodelcolumn-data-type-assl.md), [MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md), [MiningStructureColumn](../../../analysis-services/scripting/data-type/miningstructurecolumn-data-type-assl.md), [Partition](../../../analysis-services/scripting/objects/partition-element-assl.md), [Permission](../../../analysis-services/scripting/data-type/permission-data-type-assl.md), [Perspective](../../../analysis-services/scripting/objects/perspective-element-assl.md), [PerspectiveCalculation](../../../analysis-services/scripting/data-type/perspectivecalculation-data-type-assl.md), [ReportFormatParameter](../../../analysis-services/scripting/objects/reportformatparameter-element-asl.md), [ReportParameter](../../../analysis-services/scripting/objects/reportparameter-element-assl.md), [Role](../../../analysis-services/scripting/objects/role-element-assl.md), [Server](../../../analysis-services/scripting/objects/server-element-assl.md), [ServerProperty](../../../analysis-services/scripting/objects/serverproperty-element-assl.md), [Trace](../../../analysis-services/scripting/objects/trace-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 Every element that is used to define an object (an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], a hierarchy, an attribute, and so on) has a **Name** element as a property. The value of a **Name** element has the following restrictions:  
  
-   The value cannot contain leading or trailing spaces. If leading or trailing spaces are included in the value of a **Name** element, those spaces will be implicitly removed by [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
-   The value should not contain control characters. The presence of control characters in a name is strongly discouraged and can sometimes result in XML validation errors.  
  
     For objects created using the **GetNewName** method in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], AMO checks for and subsequently removes any control characters, leading spaces, or trailing spaces in the name. For this reason, using **GetNewName** is the recommended approach for setting object names.  
  
     However, if you set the **Name** property directly, the same validation checks are not performed, possibly resulting in XML validation errors. Whether an error actually occurs depends on which control character appears in the name.  
  
     Although control characters should never be used in an object name, Analysis Services does not expressly prevent them. Previous releases of Analysis Services sometimes accepted control characters in an object name. For this reason, SQL Server 2016 Analysis Services and later will ignore control characters in an object name to avoid breaking older solutions.  
  
-   The following reserved values cannot be used:  
  
    -   AUX  
  
    -   CLOCK$  
  
    -   COM1 through COM9 (COM1, COM2, COM3, and so on)  
  
    -   CON  
  
    -   LPT1 through LPT9 (LPT1, LPT2, LPT3, and so on)  
  
    -   NUL  
  
    -   PRN  
  
 The following table lists additional characters that cannot be used within the value of a **Name** element, depending on the parent element.  
  
|Parent element|Invalid Characters|  
|--------------------|------------------------|  
|[Server](../../../analysis-services/scripting/objects/server-element-assl.md)|The name must follow the rules for [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows computer names. IP addresses are not valid.|  
|[DataSource](../../../analysis-services/scripting/objects/datasource-element-assl.md)|:/\\*&#124;?"()[]{}<>|  
|[Level](../../../analysis-services/scripting/objects/level-element-assl.md), [Attribute Element](../../../analysis-services/scripting/objects/attribute-element-assl.md)|.,;'`:/\\*&#124;?"&%$!+=[]{}<>|  
|All other parent elements|.,;'`:/\\*&#124;?"&%$!+=()[]{}<>|  
  
## See Also  
 [ID Element &#40;ASSL&#41;](../../../analysis-services/scripting/properties/id-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  