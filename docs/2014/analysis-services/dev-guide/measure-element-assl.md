---
title: "Measure Element (ASSL) | Microsoft Docs"
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
  - "Measure Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Measure"
helpviewer_keywords: 
  - "Measure element"
ms.assetid: 4c2c2ed1-7f78-4564-982a-132f13bea36f
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Measure Element (ASSL)
  Defines a measure.  
  
## Syntax  
  
```xml  
  
<Measures>  
   <Measure> <!-- ancestor: MeasureGroup -->  
      <Name>...</Name>  
      <ID>...</ID>  
      <Description>...</Description>  
      <AggregateFunction>...</AggregateFunction>  
            <DataType>...</DataType>  
            <Source>...</Source>  
      <Visible>...</Visible>  
      <MeasureExpression>...</MeasureExpression>  
      <DisplayFolder>...</DisplayFolder>  
      <FormatString>...</FormatString>  
      <BackColor>...</BackColor>  
      <ForeColor>...</ForeColor>  
            <FontName>...</FontName>  
            <FontSize>...</FontSize>  
            <FontFlags>...</FontFlags>  
            <Translations>...</Translations>  
      <Annotations>...</Annotations>  
   </Measure>  
   <!-- or  -->  
   <Measure xsi:type="AggregationInstanceMeasure">...</Measure> <!-- parent: AggregationInstance -->  
      <!-- or  -->  
   <Measure xsi:type="MeasureBinding">...</Measure> <!-- ancestor: MeasureGroupBinding (out-of-line) -->  
   <!-- or  -->  
   <Measure xsi:type="PerspectiveMeasure">...</Measure> <!-- ancestor: PerspectiveMeasureGroup -->  
</Measures>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
|Ancestor or Parent|Data Type|  
|------------------------|---------------|  
|[AggregationInstance](../../../2014/analysis-services/dev-guide/aggregationinstance-element-assl.md)|[AggregationInstanceMeasure](../../../2014/analysis-services/dev-guide/measurebinding-data-type-assl.md)|  
|[MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md)|None|  
|[MeasureGroupBinding (out-of-line)](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-out-of-line-assl.md)|[MeasureBinding](../../../2014/analysis-services/dev-guide/measurebinding-data-type-assl.md)|  
|[PerspectiveMeasureGroup](../../../2014/analysis-services/dev-guide/perspectivemeasuregroup-data-type-assl.md)|[PerspectiveMeasure](../../../2014/analysis-services/dev-guide/perspectivemeasure-data-type-assl.md)|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Measures](../../../2014/analysis-services/dev-guide/measures-element-assl.md)|  
  
|Ancestor or Parent|Child elements|  
|------------------------|--------------------|  
|[MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md)|[AggregateFunction](../../../2014/analysis-services/dev-guide/aggregatefunction-element-assl.md), [Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [BackColor](../../../2014/analysis-services/dev-guide/backcolor-element-assl.md), [DataType](../../../2014/analysis-services/dev-guide/datatype-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [DisplayFolder](../../../2014/analysis-services/dev-guide/displayfolder-element-assl.md), [FontFlags](../../../2014/analysis-services/dev-guide/fontflags-element-assl.md), [FontName](../../../2014/analysis-services/dev-guide/fontname-element-assl.md), [FontSize](../../../2014/analysis-services/dev-guide/fontsize-element-assl.md), [ForeColor](../../../2014/analysis-services/dev-guide/forecolor-element-assl.md), [FormatString](../../../2014/analysis-services/dev-guide/formatstring-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [MeasureExpression](../../../2014/analysis-services/dev-guide/measureexpression-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Source](../../../2014/analysis-services/dev-guide/source-element-measure-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md), [Visible](../../../2014/analysis-services/dev-guide/visible-element-assl.md)|  
|All others|None|  
  
## Remarks  
 Binding details can be provided for a measure. These details then act as the defaults per partition.  
  
 In larger cubes, there may be hundreds of measures and hierarchies. The `DisplayFolder` property defines user appearance on the client. The value of the `DisplayFolder` property can contain any one of the following options:  
  
-   Be empty, denoting that the measure does not belong to a folder.  
  
-   Contain a single folder name, denoting that the measure should be rendered as belonging to a folder with the same name.  
  
-   Contain multiple folder names separated by a backslash (\\), denoting an embedded folder hierarchy.  
  
 The `DisplayFolder` property also applies to calculated measures and hierarchies.  
  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Measure> and <xref:Microsoft.AnalysisServices.PerspectiveMeasure>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  