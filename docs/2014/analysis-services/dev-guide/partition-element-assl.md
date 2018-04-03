---
title: "Partition Element (ASSL) | Microsoft Docs"
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
  - "Partition Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "PARTITION"
helpviewer_keywords: 
  - "Partition element"
ms.assetid: 40020840-1bb7-478f-9017-1a30342ac4c6
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Partition Element (ASSL)
  Defines a partition of a [MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md) element or a partition binding in an out-of-line [MeasureGroupBinding](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-out-of-line-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Partitions>  
      <Partition> <!-- ancestor: MeasureGroup -->  
      <Name>...</Name>  
      <ID>...</ID>  
      <CreatedTimestamp>...</CreatedTimestamp>  
      <LastSchemaUpdate>...</LastSchemaUpdate>  
      <Description>...</Description>  
      <Source>...</Source>  
      <ProcessingPriority>...</ProcessingPriority>  
      <AggregationPrefix>...</AggregationPrefix>  
      <StorageMode>...</StorageMode>  
      <ProcessingMode>...</ProcessingMode>  
      <ErrorConfiguration>...</ErrorConfiguration>  
      <StorageLocation>...</StorageLocation>  
      <RemoteDatasourceID>...</RemoteDatasourceID>  
      <Slice>...</Slice>  
      <ProactiveCaching>...</ProactiveCaching>  
      <Type>...</Type>  
      <EstimatedSize>...</EstimatedSize>  
      <EstimatedRows>...</EstimatedRows>  
      <CurrentStorageMode>...</CurrentStorageMode>  
      <AggregationDesignID>...</AggregationDesignID>  
      <AggregationInstances>...</AggregationInstances>  
      <AggregationInstanceSource>...</AggregationInstanceSource>  
      <LastProcessed>...</LastProcessed>  
      <State>...</State>  
      <Annotations>.../Annotations>  
   </Partition>  
   <!-- or -->  
   <Partition xsi:type="PartitionBinding"> <!-- ancestor: MeasureGroupBinding -->  
   </Partition>  
</Partitions>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
|Ancestor or Parent|Data Type|  
|------------------------|---------------|  
|[MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md)|None|  
|[MeasureGroupBinding](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-out-of-line-assl.md)|[PartitionBinding](../../../2014/analysis-services/dev-guide/partitionbinding-data-type-assl.md)|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Partitions](../../../2014/analysis-services/dev-guide/partitions-element-assl.md)|  
  
|Ancestor or Parent|Child elements|  
|------------------------|--------------------|  
|[MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md)|[AggregationDesignID](../../../2014/analysis-services/dev-guide/aggregationdesignid-element-assl.md), [AggregationInstances](../../../2014/analysis-services/dev-guide/aggregationinstances-element-assl.md), [AggregationInstanceSource](../../../2014/analysis-services/dev-guide/aggregationinstancesource-element-assl.md), [AggregationPrefix](../../../2014/analysis-services/dev-guide/aggregationprefix-element-assl.md), [Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [CreatedTimestamp](../../../2014/analysis-services/dev-guide/createdtimestamp-element-assl.md), [CurrentStorageMode](../../../2014/analysis-services/dev-guide/currentstoragemode-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [ErrorConfiguration](../../../2014/analysis-services/dev-guide/errorconfiguration-element-assl.md), [EstimatedRows](../../../2014/analysis-services/dev-guide/estimatedrows-element-assl.md), [EstimatedSize](../../../2014/analysis-services/dev-guide/estimatedsize-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [LastProcessed](../../../2014/analysis-services/dev-guide/lastprocessed-element-assl.md), [LastSchemaUpdate](../../../2014/analysis-services/dev-guide/lastschemaupdate-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md), [ProcessingMode](../../../2014/analysis-services/dev-guide/processingmode-element-assl.md), [ProcessingPriority](../../../2014/analysis-services/dev-guide/processingpriority-element-assl.md), [RemoteDatasourceID](../../../2014/analysis-services/dev-guide/remotedatasourceid-element-assl.md), [Slice](../../../2014/analysis-services/dev-guide/slice-element-assl.md), [Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md), [State](../../../2014/analysis-services/dev-guide/state-element-assl.md), [StorageLocation](../../../2014/analysis-services/dev-guide/storagelocation-element-assl.md), [StorageMode](../../../2014/analysis-services/dev-guide/storagemode-element-assl.md), [Type](../../../2014/analysis-services/dev-guide/type-element-partition-assl.md)|  
|[MeasureGroupBinding](../../../2014/analysis-services/dev-guide/measuregroupbinding-data-type-out-of-line-assl.md)|None|  
  
## Remarks  
 This element has the following validations under DeploymentMode value 2 (tabular server mode):  
  
-   The following children elements are not supported and should not be used:  
  
    -   [ProcessingPriority](../../../2014/analysis-services/dev-guide/processingpriority-element-assl.md)  
  
    -   [AggregationPrefix](../../../2014/analysis-services/dev-guide/aggregationprefix-element-assl.md)  
  
    -   [StorageLocation](../../../2014/analysis-services/dev-guide/storagelocation-element-assl.md)  
  
    -   [ProcessingMode](../../../2014/analysis-services/dev-guide/processingmode-element-assl.md)  
  
    -   [ErrorConfiguration](../../../2014/analysis-services/dev-guide/errorconfiguration-element-assl.md)  
  
    -   [StorageLocation](../../../2014/analysis-services/dev-guide/storagelocation-element-assl.md)  
  
    -   [RemoteDatasourceID](../../../2014/analysis-services/dev-guide/remotedatasourceid-element-assl.md)  
  
    -   [Slice](../../../2014/analysis-services/dev-guide/slice-element-assl.md)  
  
    -   [ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md)  
  
    -   [Type](../../../2014/analysis-services/dev-guide/type-element-partition-assl.md)  
  
    -   [CurrentStorageMode](../../../2014/analysis-services/dev-guide/currentstoragemode-element-assl.md)  
  
    -   [AggregationDesignID](../../../2014/analysis-services/dev-guide/aggregationdesignid-element-assl.md)  
  
    -   [AggregationInstances](../../../2014/analysis-services/dev-guide/aggregationinstances-element-assl.md)  
  
    -   [AggregationInstanceSource](../../../2014/analysis-services/dev-guide/aggregationinstancesource-element-assl.md)  
  
     An error might be thrown if any of the above elements is used.  
  
-   The [Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md) element only accepts **Query** binding.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Partition>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  