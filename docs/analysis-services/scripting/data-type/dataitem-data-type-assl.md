---
title: "DataItem Data Type (ASSL) | Microsoft Docs"
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
  - "DataItem Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "DataItem"
helpviewer_keywords: 
  - "DataItem data type"
ms.assetid: f4f5155f-9c3d-49a1-a390-a9c734fafbce
caps.latest.revision: 44
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Child elements|[Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [Collation](../../../analysis-services/scripting/properties/collation-element-assl.md), [DataSize](../../../analysis-services/scripting/properties/datasize-element-assl.md), [DataType](../../../analysis-services/scripting/properties/datatype-element-assl.md), [Format](../../../analysis-services/scripting/properties/format-element-assl.md), [InvalidXmlCharacters](../../../analysis-services/scripting/properties/invalidxmlcharacters-element-assl.md), [MimeType](../../../analysis-services/scripting/properties/mimetype-element-assl.md), [NullProcessing](../../../analysis-services/scripting/properties/nullprocessing-element-assl.md), [Source](../../../analysis-services/scripting/properties/source-element-binding-assl.md), [Trimming](../../../analysis-services/scripting/properties/trimming-element-assl.md)|  
|Derived elements|See the table in Remarks.|  
  
## Remarks  
 The **DataItem** data type is used for any data item that can be bound; for example, a measure, attribute key, and attribute name. The details that are relevant, and the defaults that apply, depend on the usage; for example, attribute names must be strings.  
  
 An instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] accepts only a certain set of data types. Use of other data types results in an error, rather than an implicit conversion to one of the valid types.  
  
 The following table lists elements of type **DataItem**.  
  
|Parent Element|Element of type **DataItem**|Comments|  
|--------------------|----------------------------------|--------------|  
|[AttributeTranslation](../../../analysis-services/scripting/data-type/attributetranslation-data-type-assl.md)|[CaptionColumn](../../../analysis-services/scripting/objects/captioncolumn-element-assl.md)|**Source** element of the **DataItem** must be of type [ColumnBinding](../../../analysis-services/scripting/data-type/columnbinding-data-type-assl.md) or [AttributeBinding](../../../analysis-services/scripting/data-type/attributebinding-data-type-assl.md)|  
|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|[CustomRollupColumn](../../../analysis-services/scripting/objects/customrollupcolumn-element-assl.md)|**Source** element of the **DataItem** must be of type [ColumnBinding](../../../analysis-services/scripting/data-type/columnbinding-data-type-assl.md) or [AttributeBinding](../../../analysis-services/scripting/data-type/attributebinding-data-type-assl.md)|  
|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|[CustomRollupPropertiesColumn](../../../analysis-services/scripting/objects/customrolluppropertiescolumn-element-assl.md)|**Source** element of the **DataItem** must be of type [ColumnBinding](../../../analysis-services/scripting/data-type/columnbinding-data-type-assl.md) or [AttributeBinding](../../../analysis-services/scripting/data-type/attributebinding-data-type-assl.md)|  
|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|[KeyColumn](../../../analysis-services/scripting/objects/keycolumn-element-assl.md)|**Source** element of the **DataItem** must be of type [ColumnBinding](../../../analysis-services/scripting/data-type/columnbinding-data-type-assl.md), [AttributeBinding](../../../analysis-services/scripting/data-type/attributebinding-data-type-assl.md) or [TimeBinding](../../../analysis-services/scripting/data-type/timebinding-data-type-assl.md)|  
|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|[NameColumn](../../../analysis-services/scripting/objects/namecolumn-element-assl.md)|**Source** element of the **DataItem** must be of type [ColumnBinding](../../../analysis-services/scripting/data-type/columnbinding-data-type-assl.md) or [AttributeBinding](../../../analysis-services/scripting/data-type/attributebinding-data-type-assl.md)|  
|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|[SkippedLevelsColumn](../../../analysis-services/scripting/objects/skippedlevelscolumn-element-assl.md)|**Source** element of the **DataItem** must be of type [ColumnBinding](../../../analysis-services/scripting/data-type/columnbinding-data-type-assl.md) or [AttributeBinding](../../../analysis-services/scripting/data-type/attributebinding-data-type-assl.md)|  
|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|[UnaryOperatorColumn](../../../analysis-services/scripting/objects/unaryoperatorcolumn-element-assl.md)|**Source** element of the **DataItem** must be of type [ColumnBinding](../../../analysis-services/scripting/data-type/columnbinding-data-type-assl.md) or [AttributeBinding](../../../analysis-services/scripting/data-type/attributebinding-data-type-assl.md)|  
|[Measure](../../../analysis-services/scripting/objects/measure-element-assl.md)|[Source](../../../analysis-services/scripting/properties/source-element-binding-assl.md)|**Source** element of the **DataItem** must be of type [RowBinding](../../../analysis-services/scripting/data-type/rowbinding-data-type-assl.md), [ColumnBinding](../../../analysis-services/scripting/data-type/columnbinding-data-type-assl.md), [MeasureBinding](../../../analysis-services/scripting/data-type/measurebinding-data-type-assl.md), or [CubeDimensionBinding](../../../analysis-services/scripting/data-type/cubedimensionbinding-data-type-assl.md)|  
|[MeasureGroupAttribute](../../../analysis-services/scripting/data-type/measuregroupattribute-data-type-assl.md)|[KeyColumn](../../../analysis-services/scripting/objects/keycolumn-element-assl.md)|**Source** element of the **DataItem** must be of type [ColumnBinding](../../../analysis-services/scripting/data-type/columnbinding-data-type-assl.md), [AttributeBinding](../../../analysis-services/scripting/data-type/attributebinding-data-type-assl.md) or [InheritedBinding](../../../analysis-services/scripting/data-type/inheritedbinding-data-type-assl.md)|  
|[ScalarMiningStructureColumn](../../../analysis-services/scripting/data-type/scalarminingstructurecolumn-data-type-assl.md)|[KeyColumn](../../../analysis-services/scripting/objects/keycolumn-element-assl.md)|**Source** element of the **DataItem** must be of type [ColumnBinding](../../../analysis-services/scripting/data-type/columnbinding-data-type-assl.md)|  
|[ScalarMiningStructureColumn](../../../analysis-services/scripting/data-type/scalarminingstructurecolumn-data-type-assl.md)|[NameColumn](../../../analysis-services/scripting/objects/namecolumn-element-assl.md)|**Source** element of the **DataItem** must be of type [ColumnBinding](../../../analysis-services/scripting/data-type/columnbinding-data-type-assl.md)|  
|[TableMiningStructureColumn](../../../analysis-services/scripting/data-type/tableminingstructurecolumn-data-type-assl.md)|[ForeignKeyColumn](../../../analysis-services/scripting/objects/foreignkeycolumn-element-assl.md)|**Source** element of the **DataItem** must be of type [ColumnBinding](../../../analysis-services/scripting/data-type/columnbinding-data-type-assl.md)|  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DataItem>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  