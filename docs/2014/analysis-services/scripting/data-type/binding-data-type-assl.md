---
title: "Binding Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
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
|Derived data types|[AttributeBinding](binding-data-type-assl.md), [ColumnBinding](columnbinding-data-type-assl.md), [CubeAttributeBinding](cubeattributebinding-data-type-assl.md), [CubeDimensionBinding](dimensionbinding-data-type-assl.md), [DataSourceViewBinding](datasourceviewbinding-data-type-assl.md), [DimensionBinding](dimensionbinding-data-type-assl.md), [InheritedBinding](inheritedbinding-data-type-assl.md), [MeasureBinding](measurebinding-data-type-assl.md), [MeasureGroupBinding](measuregroupbinding-data-type-assl.md), [MeasureGroupDimensionBinding](measuregroupdimensionbinding-data-type-assl.md), [ProactiveCachingBinding](proactivecachingbinding-data-type-assl.md), [RowBinding](rowbinding-data-type-assl.md), [TabularBinding](tabularbinding-data-type-assl.md), [TimeAttributeBinding](timeattributebinding-data-type-assl.md), [TimeBinding](timebinding-data-type-assl.md), [UserDefinedGroupBinding](userdefinedgroupbinding-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|None|  
|Derived elements|None|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Binding>.  
  
 For more information about data binding, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
## Elements of Type Binding  
 The following table lists elements of type `Binding`.  
  
|Parent Element|Element of type `Binding`|Comments|  
|--------------------|---------------------------------|--------------|  
|[AttributeTranslation](../properties/source-element-binding-assl.md) of [CaptionColumn](../objects/column-element-assl.md) (of type [DataItem](dataitem-data-type-assl.md))|Type of the `Binding` must be [AttributeBinding](binding-data-type-assl.md) or [ColumnBinding](columnbinding-data-type-assl.md)|  
|[Cube](../objects/cube-element-assl.md)|[Source](../properties/source-element-binding-assl.md)|Type of the `Binding` must be [DataSourceViewBinding](datasourceviewbinding-data-type-assl.md)|  
|[CubeBinding (out-of-line)](../objects/group-element-assl.md)|Type of the `Binding` must be [MeasureGroupBinding](measuregroupbinding-data-type-assl.md)|  
|[DataItem](dataitem-data-type-assl.md)|[Source](../properties/source-element-binding-assl.md)|`Binding` may be of any type|  
|[Dimension](../objects/dimension-element-assl.md)|[Source](../properties/source-element-binding-assl.md)|Type of the `Binding` must be [CubeDimensionBinding](dimensionbinding-data-type-assl.md), [DataSourceViewBinding](datasourceviewbinding-data-type-assl.md), [DimensionBinding](dimensionbinding-data-type-assl.md), or [TimeBinding](timebinding-data-type-assl.md)|  
|[DimensionAttribute](dimensionattribute-data-type-assl.md)|[Source](../properties/source-element-binding-assl.md)|Type of the `Binding` must be [AttributeBinding](binding-data-type-assl.md) or [UserDefinedGroupBinding](userdefinedgroupbinding-data-type-assl.md)|  
|[DrillThroughAction](action-data-type-assl.md)|[Column](../objects/column-element-assl.md)|Type of the `Binding` must be [CubeAttributeBinding](cubeattributebinding-data-type-assl.md) or [MeasureBinding](measurebinding-data-type-assl.md)|  
|[Measure](../objects/measure-element-assl.md)|[Source](../properties/source-element-binding-assl.md) (of type [DataItem](dataitem-data-type-assl.md))|Type of the `Binding` must be [ColumnBinding](columnbinding-data-type-assl.md), [CubeDimensionBinding](dimensionbinding-data-type-assl.md), [MeasureBinding](measurebinding-data-type-assl.md), or [RowBinding](rowbinding-data-type-assl.md)|  
|[MeasureGroup](../objects/measuregroup-element-assl.md)|[Source](../properties/source-element-binding-assl.md)|Type of the `Binding` must be [MeasureGroupBinding](measuregroupbinding-data-type-assl.md)|  
|[MeasureGroupAttribute](measuregroupattribute-data-type-assl.md)|[Source](../properties/source-element-binding-assl.md) of [KeyColumn](../objects/keycolumn-element-assl.md) (of type [DataItem](dataitem-data-type-assl.md))|Type of the `Binding` must be [AttributeBinding](binding-data-type-assl.md) or [ColumnBinding](columnbinding-data-type-assl.md), or [InheritedBinding](inheritedbinding-data-type-assl.md)|  
|[MeasureGroupBinding (out-of-line)](measuregroupbinding-data-type-out-of-line-assl.md)|[Dimension](../objects/dimension-element-assl.md)|Type of the `Binding` must be [MeasureGroupDimensionBinding](measuregroupdimensionbinding-data-type-assl.md)|  
|[MeasureGroupBinding (out-of-line)](measuregroupbinding-data-type-out-of-line-assl.md)|[Measure](../objects/measure-element-assl.md)|Type of the `Binding` must be [MeasureBinding](measurebinding-data-type-assl.md)|  
|[MeasureGroupBinding (out-of-line)](../objects/partition-element-assl.md)|Type of the `Binding` must be [PartitionBinding](partitionbinding-data-type-assl.md)|  
|[MeasureGroupBinding (out-of-line)](measuregroupbinding-data-type-out-of-line-assl.md)|[Source](../properties/source-element-binding-assl.md)|Type of the `Binding` must be [TableBinding](tablebinding-data-type-assl.md)|  
|[MeasureGroupDimension](dimension-data-type-assl.md)|[Source](../properties/source-element-binding-assl.md)|Type of the `Binding` must be [MeasureGroupDimensionBinding](measuregroupdimensionbinding-data-type-assl.md)|  
|[MiningStructure](../objects/miningstructure-element-assl.md)|[Source](../properties/source-element-binding-assl.md)|Type of the `Binding` must be [CubeDimensionBinding](dimensionbinding-data-type-assl.md), [DataSourceViewBinding](datasourceviewbinding-data-type-assl.md), or [DimensionBinding](dimensionbinding-data-type-assl.md)|  
|[Partition](../objects/partition-element-assl.md)|[Source](../properties/source-element-binding-assl.md)|Type of the `Binding` must be [TabularBinding](tabularbinding-data-type-assl.md)|  
|[ProactiveCaching](../objects/proactivecaching-element-assl.md)|[Source](../properties/source-element-binding-assl.md)|Type of the `Binding` must be [ProactiveCachingBinding](proactivecachingbinding-data-type-assl.md)|  
|[ScalarMiningStructureColumn](miningstructurecolumn-data-type-assl.md)|[Source](../properties/source-element-binding-assl.md)|Type of the `Binding` must be [AttributeBinding](binding-data-type-assl.md), [CubeAttributeBinding Data Type &#40;ASSL&#41;](cubeattributebinding-data-type-assl.md), or [MeasureBinding Data Type &#40;ASSL&#41;](measurebinding-data-type-assl.md)|  
|[TableMiningStructureColumn](../objects/sourcemeasuregroup-element-assl.md)|Type of the `Binding` must be [MeasureGroupBinding](measuregroupbinding-data-type-assl.md)|  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
