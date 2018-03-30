---
title: "DataItem Data Type (ASSL) | Microsoft Docs"
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
  - "DataItem Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DataItem"
helpviewer_keywords: 
  - "DataItem data type"
ms.assetid: f4f5155f-9c3d-49a1-a390-a9c734fafbce
caps.latest.revision: 44
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# DataItem Data Type (ASSL)
  Defines a primitive data type that represents the data-related characteristics of a data item, such as a column or attribute.  
  
## Syntax  
  
```xml  
  
<DataItem>  
   <DataType>...</DataType>  
   <DataSize>...</DataSize>  
   <MimeType>...</MimeType>  
   <NullProcessing>...</NullProcessing>  
   <Trimming>...</Trimming>  
   <InvalidXmlCharacters>...</InvalidXmlCharacters>  
      <Collation>...</Collation>  
   <Format>...</Format>  
   <Source>...</Source>  
   <Annotations>...</Annotations>  
</DataItem>  
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
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Collation](../../../2014/analysis-services/dev-guide/collation-element-assl.md), [DataSize](../../../2014/analysis-services/dev-guide/datasize-element-assl.md), [DataType](../../../2014/analysis-services/dev-guide/datatype-element-assl.md), [Format](../../../2014/analysis-services/dev-guide/format-element-assl.md), [InvalidXmlCharacters](../../../2014/analysis-services/dev-guide/invalidxmlcharacters-element-assl.md), [MimeType](../../../2014/analysis-services/dev-guide/mimetype-element-assl.md), [NullProcessing](../../../2014/analysis-services/dev-guide/nullprocessing-element-assl.md), [Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md), [Trimming](../../../2014/analysis-services/dev-guide/trimming-element-assl.md)|  
|Derived elements|See the table in Remarks.|  
  
## Remarks  
 The `DataItem` data type is used for any data item that can be bound; for example, a measure, attribute key, and attribute name. The details that are relevant, and the defaults that apply, depend on the usage; for example, attribute names must be strings.  
  
 An instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] accepts only a certain set of data types. Use of other data types results in an error, rather than an implicit conversion to one of the valid types.  
  
 The following table lists elements of type `DataItem`.  
  
|Parent Element|Element of type `DataItem`|Comments|  
|--------------------|----------------------------------|--------------|  
|[AttributeTranslation](../../../2014/analysis-services/dev-guide/attributetranslation-data-type-assl.md)|[CaptionColumn](../../../2014/analysis-services/dev-guide/captioncolumn-element-assl.md)|`Source` element of the `DataItem` must be of type [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md) or [AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md)|  
|[DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)|[CustomRollupColumn](../../../2014/analysis-services/dev-guide/customrollupcolumn-element-assl.md)|`Source` element of the `DataItem` must be of type [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md) or [AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md)|  
|[DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)|[CustomRollupPropertiesColumn](../../../2014/analysis-services/dev-guide/customrolluppropertiescolumn-element-assl.md)|`Source` element of the `DataItem` must be of type [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md) or [AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md)|  
|[DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)|[KeyColumn](../../../2014/analysis-services/dev-guide/keycolumn-element-assl.md)|`Source` element of the `DataItem` must be of type [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md), [AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md) or [TimeBinding](../../../2014/analysis-services/dev-guide/timebinding-data-type-assl.md)|  
|[DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)|[NameColumn](../../../2014/analysis-services/dev-guide/namecolumn-element-assl.md)|`Source` element of the `DataItem` must be of type [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md) or [AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md)|  
|[DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)|[SkippedLevelsColumn](../../../2014/analysis-services/dev-guide/skippedlevelscolumn-element-assl.md)|`Source` element of the `DataItem` must be of type [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md) or [AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md)|  
|[DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)|[UnaryOperatorColumn](../../../2014/analysis-services/dev-guide/unaryoperatorcolumn-element-assl.md)|`Source` element of the `DataItem` must be of type [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md) or [AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md)|  
|[Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md)|[Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md)|`Source` element of the `DataItem` must be of type [RowBinding](../../../2014/analysis-services/dev-guide/rowbinding-data-type-assl.md), [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md), [MeasureBinding](../../../2014/analysis-services/dev-guide/measurebinding-data-type-assl.md), or [CubeDimensionBinding](../../../2014/analysis-services/dev-guide/cubedimensionbinding-data-type-assl.md)|  
|[MeasureGroupAttribute](../../../2014/analysis-services/dev-guide/measuregroupattribute-data-type-assl.md)|[KeyColumn](../../../2014/analysis-services/dev-guide/keycolumn-element-assl.md)|`Source` element of the `DataItem` must be of type [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md), [AttributeBinding](../../../2014/analysis-services/dev-guide/attributebinding-data-type-assl.md) or [InheritedBinding](../../../2014/analysis-services/dev-guide/inheritedbinding-data-type-assl.md)|  
|[ScalarMiningStructureColumn](../../../2014/analysis-services/dev-guide/scalarminingstructurecolumn-data-type-assl.md)|[KeyColumn](../../../2014/analysis-services/dev-guide/keycolumn-element-assl.md)|`Source` element of the `DataItem` must be of type [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md)|  
|[ScalarMiningStructureColumn](../../../2014/analysis-services/dev-guide/scalarminingstructurecolumn-data-type-assl.md)|[NameColumn](../../../2014/analysis-services/dev-guide/namecolumn-element-assl.md)|`Source` element of the `DataItem` must be of type [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md)|  
|[TableMiningStructureColumn](../../../2014/analysis-services/dev-guide/tableminingstructurecolumn-data-type-assl.md)|[ForeignKeyColumn](../../../2014/analysis-services/dev-guide/foreignkeycolumn-element-assl.md)|`Source` element of the `DataItem` must be of type [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md)|  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DataItem>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  