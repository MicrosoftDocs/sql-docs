---
title: "MeasureGroup Element (ASSL) | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: assl
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# MeasureGroup Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
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
|Data type and length|See the table below.|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
|Ancestor or Parent|Data Type|  
|------------------------|---------------|  
|[Cube](../../../analysis-services/scripting/objects/cube-element-assl.md)|None|  
|[CubeBinding](../../../analysis-services/scripting/data-type/cubebinding-data-type-out-of-line-assl.md)|[MeasureGroupBinding](../../../analysis-services/scripting/data-type/measuregroupbinding-data-type-assl.md)|  
|[Perspective](../../../analysis-services/scripting/objects/perspective-element-assl.md)|[PerspectiveMeasureGroup](../../../analysis-services/scripting/data-type/perspectivemeasuregroup-data-type-assl.md)|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[MeasureGroups](../../../analysis-services/scripting/collections/measuregroups-element-assl.md)|  
|Child elements|See the table below.|  
  
|Ancestor or Parent|Child elements|  
|------------------------|--------------------|  
|[Cube](../../../analysis-services/scripting/objects/cube-element-assl.md)|[AggregationDesigns](../../../analysis-services/scripting/collections/aggregationdesigns-element-assl.md), [AggregationPrefix](../../../analysis-services/scripting/properties/aggregationprefix-element-assl.md), [Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [CreatedTimestamp](../../../analysis-services/scripting/properties/createdtimestamp-element-assl.md), [DataAggregation](../../../analysis-services/scripting/properties/dataaggregation-element-assl.md), [Description](../../../analysis-services/scripting/properties/description-element-assl.md), [Dimensions](../../../analysis-services/scripting/collections/dimensions-element-assl.md), [ErrorConfiguration](../../../analysis-services/scripting/objects/errorconfiguration-element-assl.md), [EstimatedRows](../../../analysis-services/scripting/properties/estimatedrows-element-assl.md), [EstimatedSize](../../../analysis-services/scripting/properties/estimatedsize-element-assl.md), [ID](../../../analysis-services/scripting/properties/id-element-assl.md), [IgnoreUnrelatedDimensions](../../../analysis-services/scripting/properties/ignoreunrelateddimensions-element-assl.md), [LastProcessed](../../../analysis-services/scripting/properties/lastprocessed-element-assl.md), [LastSchemaUpdate](../../../analysis-services/scripting/properties/lastschemaupdate-element-assl.md), [MeasureQualification](../../../analysis-services/scripting/properties/measurequalificaton-element-assl.md), [Measures](../../../analysis-services/scripting/collections/measures-element-assl.md), [Name](../../../analysis-services/scripting/properties/name-element-assl.md), [Partitions](../../../analysis-services/scripting/collections/partitions-element-assl.md), [ProactiveCaching](../../../analysis-services/scripting/objects/proactivecaching-element-assl.md), [ProcessingMode](../../../analysis-services/scripting/properties/processingmode-element-assl.md), [ProcessingPriority](../../../analysis-services/scripting/properties/processingpriority-element-assl.md), [Source](../../../analysis-services/scripting/properties/source-element-measure-assl.md), [State](../../../analysis-services/scripting/properties/state-element-assl.md), [StorageLocation](../../../analysis-services/scripting/properties/storagelocation-element-assl.md), [StorageMode](../../../analysis-services/scripting/properties/storagemode-element-assl.md), [Translations](../../../analysis-services/scripting/collections/translations-element-assl.md), [Type](../../../analysis-services/scripting/properties/type-element-measuregroup-assl.md)|  
|[CubeBinding](../../../analysis-services/scripting/data-type/cubebinding-data-type-out-of-line-assl.md)|None|  
|[Perspective](../../../analysis-services/scripting/objects/perspective-element-assl.md)|None|  
  
## Remarks  
 All the measures of a measure group must be sourced from a single table. A measure group can define default bindings that can be overridden for each partition.  
  
 The **MeasureGroup** element defines details common to measure groups on regular cubes and virtual cubes. Separate subtypes define the details specific to each type.  
  
 The **State** property of a **MeasureGroup** has the following values:  
  
-   *FullyProcessed* if all partitions are processed.  
  
-   *PartiallyProcessed* if at least one partition is processed.  
  
-   *Unprocessed* if no partitions are processed.  
  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.MeasureGroup> and <xref:Microsoft.AnalysisServices.PerspectiveMeasureGroup>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  
