---
title: "Binding Data Type (ASSL) | Microsoft Docs"
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
  - "Binding Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "BINDING"
helpviewer_keywords: 
  - "Binding data type"
ms.assetid: 0a777219-b885-4961-ac66-b76faeb520db
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Binding Data Type (ASSL)
  Defines an abstract primitive data type that represents a dependent relationship between two objects in which the data or metadata of one object is dependent on the data or metadata of a bound object.  
  
## Syntax  
  
```xml  
  
<Binding>...</Binding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|[AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md), [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md), [CubeAttributeBinding](../../../2014/analysis-services/dev-guide/cubeattributebinding-data-type-assl.md), [CubeDimensionBinding](../../../2014/analysis-services/dev-guide/cubedimensionbinding-data-type-assl.md), [DataSourceViewBinding](../../../2014/analysis-services/dev-guide/datasourceviewbinding-data-type-assl.md), [DimensionBinding](../../../2014/analysis-services/dev-guide/dimensionbinding-data-type-assl.md), [InheritedBinding](../../../2014/analysis-services/dev-guide/inheritedbinding-data-type-assl.md), [MeasureBinding](../../../2014/analysis-services/dev-guide/measurebinding-data-type-assl.md), [MeasureGroupBinding](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-assl.md), [MeasureGroupDimensionBinding](../../../2014/analysis-services/dev-guide/measuregroupdimensionbinding-data-type-assl.md), [ProactiveCachingBinding](../../../2014/analysis-services/dev-guide/proactivecachingbinding-data-type-assl.md), [RowBinding](../../../2014/analysis-services/dev-guide/rowbinding-data-type-assl.md), [TabularBinding](../../../2014/analysis-services/dev-guide/tabularbinding-data-type-assl.md), [TimeAttributeBinding](../../../2014/analysis-services/dev-guide/timeattributebinding-data-type-assl.md), [TimeBinding](../../../2014/analysis-services/dev-guide/timebinding-data-type-assl.md), [UserDefinedGroupBinding](../../../2014/analysis-services/dev-guide/userdefinedgroupbinding-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|None|  
|Derived elements|None|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Binding>.  
  
 For more information about data binding, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../../2014/analysis-services/data-sources-and-bindings-ssas-multidimensional.md).  
  
## Elements of Type Binding  
 The following table lists elements of type `Binding`.  
  
|Parent Element|Element of type `Binding`|Comments|  
|--------------------|---------------------------------|--------------|  
|[AttributeTranslation](../../../2014/analysis-services/dev-guide/attributetranslation-data-type-assl.md)|[Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md) of [CaptionColumn](../../../2014/analysis-services/dev-guide/captioncolumn-element-assl.md) (of type [DataItem](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md))|Type of the `Binding` must be [AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md) or [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md)|  
|[Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md)|[Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md)|Type of the `Binding` must be [DataSourceViewBinding](../../../2014/analysis-services/dev-guide/datasourceviewbinding-data-type-assl.md)|  
|[CubeBinding (out-of-line)](../../../2014/analysis-services/dev-guide/cubebinding-data-type-out-of-line-assl.md)|[MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md)|Type of the `Binding` must be [MeasureGroupBinding](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-assl.md)|  
|[DataItem](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md)|[Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md)|`Binding` may be of any type|  
|[Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md)|[Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md)|Type of the `Binding` must be [CubeDimensionBinding](../../../2014/analysis-services/dev-guide/cubedimensionbinding-data-type-assl.md), [DataSourceViewBinding](../../../2014/analysis-services/dev-guide/datasourceviewbinding-data-type-assl.md), [DimensionBinding](../../../2014/analysis-services/dev-guide/dimensionbinding-data-type-assl.md), or [TimeBinding](../../../2014/analysis-services/dev-guide/timebinding-data-type-assl.md)|  
|[DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)|[Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md)|Type of the `Binding` must be [AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md) or [UserDefinedGroupBinding](../../../2014/analysis-services/dev-guide/userdefinedgroupbinding-data-type-assl.md)|  
|[DrillThroughAction](../../../2014/analysis-services/dev-guide/drillthroughaction-data-type-assl.md)|[Column](../../../2014/analysis-services/dev-guide/column-element-assl.md)|Type of the `Binding` must be [CubeAttributeBinding](../../../2014/analysis-services/dev-guide/cubeattributebinding-data-type-assl.md) or [MeasureBinding](../../../2014/analysis-services/dev-guide/measurebinding-data-type-assl.md)|  
|[Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md)|[Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md) (of type [DataItem](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md))|Type of the `Binding` must be [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md), [CubeDimensionBinding](../../../2014/analysis-services/dev-guide/cubedimensionbinding-data-type-assl.md), [MeasureBinding](../../../2014/analysis-services/dev-guide/measurebinding-data-type-assl.md), or [RowBinding](../../../2014/analysis-services/dev-guide/rowbinding-data-type-assl.md)|  
|[MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md)|[Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md)|Type of the `Binding` must be [MeasureGroupBinding](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-assl.md)|  
|[MeasureGroupAttribute](../../../2014/analysis-services/dev-guide/measuregroupattribute-data-type-assl.md)|[Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md) of [KeyColumn](../../../2014/analysis-services/dev-guide/keycolumn-element-assl.md) (of type [DataItem](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md))|Type of the `Binding` must be [AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md) or [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md), or [InheritedBinding](../../../2014/analysis-services/dev-guide/inheritedbinding-data-type-assl.md)|  
|[MeasureGroupBinding (out-of-line)](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-out-of-line-assl.md)|[Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md)|Type of the `Binding` must be [MeasureGroupDimensionBinding](../../../2014/analysis-services/dev-guide/measuregroupdimensionbinding-data-type-assl.md)|  
|[MeasureGroupBinding (out-of-line)](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-out-of-line-assl.md)|[Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md)|Type of the `Binding` must be [MeasureBinding](../../../2014/analysis-services/dev-guide/measurebinding-data-type-assl.md)|  
|[MeasureGroupBinding (out-of-line)](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-out-of-line-assl.md)|[Partition](../../../2014/analysis-services/dev-guide/partition-element-assl.md)|Type of the `Binding` must be [PartitionBinding](../../../2014/analysis-services/dev-guide/partitionbinding-data-type-assl.md)|  
|[MeasureGroupBinding (out-of-line)](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-out-of-line-assl.md)|[Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md)|Type of the `Binding` must be [TableBinding](../../../2014/analysis-services/dev-guide/tablebinding-data-type-assl.md)|  
|[MeasureGroupDimension](../../../2014/analysis-services/dev-guide/measuregroupdimension-data-type-assl.md)|[Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md)|Type of the `Binding` must be [MeasureGroupDimensionBinding](../../../2014/analysis-services/dev-guide/measuregroupdimensionbinding-data-type-assl.md)|  
|[MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md)|[Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md)|Type of the `Binding` must be [CubeDimensionBinding](../../../2014/analysis-services/dev-guide/cubedimensionbinding-data-type-assl.md), [DataSourceViewBinding](../../../2014/analysis-services/dev-guide/datasourceviewbinding-data-type-assl.md), or [DimensionBinding](../../../2014/analysis-services/dev-guide/dimensionbinding-data-type-assl.md)|  
|[Partition](../../../2014/analysis-services/dev-guide/partition-element-assl.md)|[Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md)|Type of the `Binding` must be [TabularBinding](../../../2014/analysis-services/dev-guide/tabularbinding-data-type-assl.md)|  
|[ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md)|[Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md)|Type of the `Binding` must be [ProactiveCachingBinding](../../../2014/analysis-services/dev-guide/proactivecachingbinding-data-type-assl.md)|  
|[ScalarMiningStructureColumn](../../../2014/analysis-services/dev-guide/scalarminingstructurecolumn-data-type-assl.md)|[Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md)|Type of the `Binding` must be [AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md), [CubeAttributeBinding Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/cubeattributebinding-data-type-assl.md), or [MeasureBinding Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/measurebinding-data-type-assl.md)|  
|[TableMiningStructureColumn](../../../2014/analysis-services/dev-guide/tableminingstructurecolumn-data-type-assl.md)|[SourceMeasureGroup](../../../2014/analysis-services/dev-guide/sourcemeasuregroup-element-assl.md)|Type of the `Binding` must be [MeasureGroupBinding](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-assl.md)|  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  