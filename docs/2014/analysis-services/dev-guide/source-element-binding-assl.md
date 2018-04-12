---
title: "Source Element (Binding) (ASSL) | Microsoft Docs"
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
  - "Source Element (Binding)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Source"
helpviewer_keywords: 
  - "Source element"
ms.assetid: 1032558c-7546-4ca7-888d-8139df23cb62
caps.latest.revision: 40
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Source Element (Binding) (ASSL)
  Identifies the source of data to which the parent element is bound.  
  
## Syntax  
  
```xml  
  
<AggregationInstance> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <Source>...</Source>  
   ...  
</Cube>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|See Data type table below|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
 **Data Type and length**  
  
|||  
|-|-|  
|Ancestor or parent|Data type|  
|[AggregationInstance](../../../2014/analysis-services/dev-guide/aggregationinstance-element-assl.md)|[TabularBinding](../../../2014/analysis-services/dev-guide/tabularbinding-data-type-assl.md)|  
|[AggregationInstanceMeasure](../../../2014/analysis-services/dev-guide/aggregationinstancemeasure-data-type-assl.md)|[ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md)|  
|[Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md)|[DataSourceViewBinding](../../../2014/analysis-services/dev-guide/datasourceviewbinding-data-type-assl.md)|  
|[DataItem](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md)|Any data type derived from [Binding](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md), depending on the parent of `DataItem`. For more information, see Remarks.|  
|[Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md)|[CubeDimensionBinding](../../../2014/analysis-services/dev-guide/cubedimensionbinding-data-type-assl.md), [DataSourceViewBinding](../../../2014/analysis-services/dev-guide/datasourceviewbinding-data-type-assl.md), [DimensionBinding](../../../2014/analysis-services/dev-guide/dimensionbinding-data-type-assl.md), [TimeBinding](../../../2014/analysis-services/dev-guide/timebinding-data-type-assl.md)|  
|[DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)|[AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md), [UserDefinedGroupBinding](../../../2014/analysis-services/dev-guide/userdefinedgroupbinding-data-type-assl.md)|  
|[MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md)|[MeasureGroupBinding](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-assl.md)|  
|[MeasureGroupDimension](../../../2014/analysis-services/dev-guide/measuregroupdimension-data-type-assl.md)|[MeasureGroupDimensionBinding](../../../2014/analysis-services/dev-guide/measuregroupdimensionbinding-data-type-assl.md)|  
|[MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md)|[CubeDimensionBinding](../../../2014/analysis-services/dev-guide/cubedimensionbinding-data-type-assl.md), [DataSourceViewBinding](../../../2014/analysis-services/dev-guide/datasourceviewbinding-data-type-assl.md), [DimensionBinding](../../../2014/analysis-services/dev-guide/dimensionbinding-data-type-assl.md)|  
|[Partition](../../../2014/analysis-services/dev-guide/partition-element-assl.md)|[TabularBinding](../../../2014/analysis-services/dev-guide/tabularbinding-data-type-assl.md)|  
|[ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md)|Any data type derived from [ProactiveCachingBinding](../../../2014/analysis-services/dev-guide/proactivecachingbinding-data-type-assl.md), depending on the processing and notification options used by the parent of the `ProactiveCaching` element.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AggregationInstance](../../../2014/analysis-services/dev-guide/aggregationinstance-element-assl.md), [AggregationInstanceMeasure](../../../2014/analysis-services/dev-guide/aggregationinstancemeasure-data-type-assl.md), [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md), [DataItem](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md), [Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md), [DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md), [MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md), [MeasureGroupDimension](../../../2014/analysis-services/dev-guide/measuregroupdimension-data-type-assl.md), [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md), [Partition](../../../2014/analysis-services/dev-guide/partition-element-assl.md), [ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md).|  
|Child elements|None|  
  
## Remarks  
 In the `Source` element, the derived `Binding` data types that are allowed in the `DataItem` element depend on the parent of the `DataItem` element.  
  
|DataItem Parent|Allowed Data Types|  
|---------------------|------------------------|  
|[DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)|[AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md), [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md), [TimeAttributeBinding](../../../2014/analysis-services/dev-guide/timeattributebinding-data-type-assl.md) (only for [KeyColumns](../../../2014/analysis-services/dev-guide/keycolumns-element-assl.md)).|  
|[MeasureGroupAttribute](../../../2014/analysis-services/dev-guide/measuregroupattribute-data-type-assl.md)|[AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md), [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md), [InheritedBinding](../../../2014/analysis-services/dev-guide/inheritedbinding-data-type-assl.md).|  
|[ScalarMiningStructureColumn](../../../2014/analysis-services/dev-guide/scalarminingstructurecolumn-data-type-assl.md)|[ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md)|  
  
 For more information about the `Binding` type, including tables of Analysis Services Scripting Language (ASSL) objects of the `Binding` type and the inheritance hierarchy of `Binding` types, see [Binding Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md).  
  
 For more information about data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  