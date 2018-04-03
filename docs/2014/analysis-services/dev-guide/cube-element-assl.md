---
title: "Cube Element (ASSL) | Microsoft Docs"
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
  - "Cube Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Cube"
helpviewer_keywords: 
  - "Cube element"
ms.assetid: 2d801066-6cca-4a99-bbd8-56a38d762108
caps.latest.revision: 40
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Cube Element (ASSL)
  Defines a regular, virtual, or linked cube in a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Cubes>  
   <Cube>  
            <Name>...</Name>  
      <ID>...</ID>  
      <CreatedTimestamp>...</CreatedTimestamp>  
      <LastSchemaUpdate>...</LastSchemaUpdate>  
            <Description>...</Description>  
            <Language>...</Language>  
            <Collation>...</Collation>  
            <Translations>...</Translations>  
      <Dimensions>...</Dimensions>  
            <CubePermissions>...</CubePermissions>  
      <MdxScripts>...</MdxScripts>  
      <Perspectives>...</Perspectives>  
      <State>...</State>  
      <DefaultMeasure>...</DefaultMeasure>  
      <Visible>...</Visible>  
      <MeasureGroups>...</MeasureGroups>  
      <DataSourceView >...</Source>  
      <AggregationPrefix>...</AggregationPrefix>  
      <ProcessingPriority>...</ProcessingPriority>  
            <StorageMode>...</StorageMode>  
      <ProcessingMode>...</ProcessingMode>  
      <ScriptCacheProcessingMode>...</ScriptCacheProcessingMode>  
      <ProactiveCaching>...</ProactiveCaching>  
            <Kpis>...</Kpis>  
            <ErrorConfiguration>...</ErrorConfiguration>  
            <Actions>...</Actions>  
      <StorageLocation>...</StorageLocation>  
      <EstimatedRows>...</EstimatedRows>  
      <LastProcessed>...</LastProcessed>  
      <Annotations>...</Annotations>  
   </Cube>  
</Cubes>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Cubes](../../../2014/analysis-services/dev-guide/cubes-element-assl.md)|  
|Child elements|[Actions](../../../2014/analysis-services/dev-guide/actions-element-assl.md), [AggregationPrefix](../../../2014/analysis-services/dev-guide/aggregationprefix-element-assl.md), [Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Collation](../../../2014/analysis-services/dev-guide/collation-element-assl.md), [CreatedTimestamp](../../../2014/analysis-services/dev-guide/createdtimestamp-element-assl.md), [CubePermissions](../../../2014/analysis-services/dev-guide/cubepermissions-element-assl.md), [DefaultMeasure](../../../2014/analysis-services/dev-guide/defaultmeasure-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [Dimensions](../../../2014/analysis-services/dev-guide/dimensions-element-assl.md), [ErrorConfiguration](../../../2014/analysis-services/dev-guide/errorconfiguration-element-assl.md), [EstimatedRows](../../../2014/analysis-services/dev-guide/estimatedrows-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [Kpis](../../../2014/analysis-services/dev-guide/kpis-element-assl.md), [Language](../../../2014/analysis-services/dev-guide/language-element-assl.md), [LastProcessed](../../../2014/analysis-services/dev-guide/lastprocessed-element-assl.md), [LastSchemaUpdate](../../../2014/analysis-services/dev-guide/lastschemaupdate-element-assl.md), [MdxScripts](../../../2014/analysis-services/dev-guide/mdxscripts-element-assl.md), [MeasureGroups](../../../2014/analysis-services/dev-guide/measuregroups-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Perspectives](../../../2014/analysis-services/dev-guide/perspectives-element-assl.md), [ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md), [ProcessingMode](../../../2014/analysis-services/dev-guide/processingmode-element-assl.md), [ProcessingPriority](../../../2014/analysis-services/dev-guide/processingpriority-element-assl.md), [ScriptCacheProcessingMode](../../../2014/analysis-services/dev-guide/scriptcacheprocessingmode-element-assl.md), [State](../../../2014/analysis-services/dev-guide/state-element-assl.md), [StorageLocation](../../../2014/analysis-services/dev-guide/storagelocation-element-assl.md), [StorageMode](../../../2014/analysis-services/dev-guide/storagemode-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md), [Visible](../../../2014/analysis-services/dev-guide/visible-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Cube>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  