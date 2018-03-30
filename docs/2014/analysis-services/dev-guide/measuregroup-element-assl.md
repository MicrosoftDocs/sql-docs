---
title: "MeasureGroup Element (ASSL) | Microsoft Docs"
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
  - "MeasureGroup Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MeasureGroup"
helpviewer_keywords: 
  - "MeasureGroup element"
ms.assetid: 7aa099db-5dc7-4cac-b437-f73fc0921b24
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# MeasureGroup Element (ASSL)
  Defines a set of measures at the same level of granularity.  
  
## Syntax  
  
```xml  
  
<MeasureGroups>  
   <MeasureGroup> <!-- ancestor: Cube -->  
      <Name>...</Name>  
      <ID>...</ID>  
      <CreatedTimestamp>...</<Create  
      <LastSchemaUpdate>...</LastSchemaUpdate>  
      <Description>...</Description>  
      <LastProcessed>...</LastProcessed>  
      <Translations>...</Translations>  
      <Type>...</Type>  
      <State>...</State>  
      <MeasureQualification>...</MeasureQualification>  
      <Measures>...</Measures>  
      <DataAggregation>...</DataAggregation>  
      <Source>...</Source>  
            <StorageMode>...</StorageMode>  
      <StorageLocation>...</StorageLocation>  
      <IgnoreUnrelatedDimensions>...</IgnoreUnrelatedDimensions>  
            <ProactiveCaching>...</ProactiveCaching>  
      <EstimatedRows>...</EstimatedRows>  
      <ErrorConfiguration>...</ErrorConfiguration>  
      <EstimatedSize>...</EstimatedSize>  
      <ProcessingMode>...</ProcessingMode>  
      <Dimensions>...</Dimensions>  
      <Partitions>...</Partitions>  
      <AggregationPrefix>...</AggregationPrefix>  
      <ProcessingPriority>...</ProcessingPriority>  
            <AggregationDesigns>...</AggregationDesigns>  
      <Annotations>...</Annotations>  
   </MeasureGroup>  
   <!-- or  -->  
   <MeasureGroup xsi:type="MeasureGroupBinding">...</MeasureGroup> <!-- ancestor: CubeBinding -->  
   <!-- or  -->  
   <MeasureGroup xsi:type="PerspectiveMeasureGroup">...</MeasureGroup> <!-- ancestor: Perspective -->  
</MeasureGroups>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
|Ancestor or Parent|Data Type|  
|------------------------|---------------|  
|[Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md)|None|  
|[CubeBinding](../../../2014/analysis-services/dev-guide/cubebinding-data-type-out-of-line-assl.md)|[MeasureGroupBinding](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-assl.md)|  
|[Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md)|[PerspectiveMeasureGroup](../../../2014/analysis-services/dev-guide/perspectivemeasuregroup-data-type-assl.md)|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[MeasureGroups](../../../2014/analysis-services/dev-guide/measuregroups-element-assl.md)|  
|Child elements||  
  
|Ancestor or Parent|Child elements|  
|------------------------|--------------------|  
|[Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md)|[AggregationDesigns](../../../2014/analysis-services/dev-guide/aggregationdesigns-element-assl.md), [AggregationPrefix](../../../2014/analysis-services/dev-guide/aggregationprefix-element-assl.md), [Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [CreatedTimestamp](../../../2014/analysis-services/dev-guide/createdtimestamp-element-assl.md), [DataAggregation](../../../2014/analysis-services/dev-guide/dataaggregation-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [Dimensions](../../../2014/analysis-services/dev-guide/dimensions-element-assl.md), [ErrorConfiguration](../../../2014/analysis-services/dev-guide/errorconfiguration-element-assl.md), [EstimatedRows](../../../2014/analysis-services/dev-guide/estimatedrows-element-assl.md), [EstimatedSize](../../../2014/analysis-services/dev-guide/estimatedsize-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [IgnoreUnrelatedDimensions](../../../2014/analysis-services/dev-guide/ignoreunrelateddimensions-element-assl.md), [LastProcessed](../../../2014/analysis-services/dev-guide/lastprocessed-element-assl.md), [LastSchemaUpdate](../../../2014/analysis-services/dev-guide/lastschemaupdate-element-assl.md), [MeasureQualification](../../../2014/analysis-services/dev-guide/measurequalificaton-element-assl.md), [Measures](../../../2014/analysis-services/dev-guide/measures-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Partitions](../../../2014/analysis-services/dev-guide/partitions-element-assl.md), [ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md), [ProcessingMode](../../../2014/analysis-services/dev-guide/processingmode-element-assl.md), [ProcessingPriority](../../../2014/analysis-services/dev-guide/processingpriority-element-assl.md), [Source](../../../2014/analysis-services/dev-guide/source-element-measure-assl.md), [State](../../../2014/analysis-services/dev-guide/state-element-assl.md), [StorageLocation](../../../2014/analysis-services/dev-guide/storagelocation-element-assl.md), [StorageMode](../../../2014/analysis-services/dev-guide/storagemode-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md), [Type](../../../2014/analysis-services/dev-guide/type-element-measuregroup-assl.md)|  
|[CubeBinding](../../../2014/analysis-services/dev-guide/cubebinding-data-type-out-of-line-assl.md)|None|  
|[Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md)|None|  
  
## Remarks  
 All the measures of a measure group must be sourced from a single table. A measure group can define default bindings that can be overridden for each partition.  
  
 The `MeasureGroup` element defines details common to measure groups on regular cubes and virtual cubes. Separate subtypes define the details specific to each type.  
  
 The `State` property of a `MeasureGroup` has the following values:  
  
-   *FullyProcessed* if all partitions are processed.  
  
-   *PartiallyProcessed* if at least one partition is processed.  
  
-   *Unprocessed* if no partitions are processed.  
  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.MeasureGroup> and <xref:Microsoft.AnalysisServices.PerspectiveMeasureGroup>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  