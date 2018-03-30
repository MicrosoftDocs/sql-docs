---
title: "MiningModel Element (ASSL) | Microsoft Docs"
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
  - "MiningModel Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MiningModel"
helpviewer_keywords: 
  - "MiningModel element"
ms.assetid: a61d935f-c8f6-457d-ad0c-44f58bb286f5
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# MiningModel Element (ASSL)
  Defines a single data mining model.  
  
## Syntax  
  
```xml  
  
<MiningModels>  
      <MiningModel>  
      <Name>...</Name>  
            <ID>...</ID>  
      <Description>...</<Descrip  
            <Algorithm>...</Algorithm>  
      <CreatedTimestamp>...</CreatedTimestamp>  
      <LastSchemaUpdate>...</LastSchemaUpdate>  
      <LastProcessed>...</LastProcessed>  
      <AlgorithmParameters>...</AlgorithmParameters>  
      <AllowDrillThrough>...</AllowDrillThrough>  
      <Translations>...</Translations>  
      <Columns>...</Columns>  
      <State>...</State>  
      <MiningModelPermissions>...</MiningModelPermissions>  
      <Language>...</Language>  
            <Collation>...</Collation>  
            <Annotations>...</Annotations>  
            <ddl100_100:FoldingParameters>   </FoldingParameters>  
   </MiningModel>  
</MiningModels>  
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
|Parent elements|[MiningModels](../../../2014/analysis-services/dev-guide/miningmodels-element-assl.md)|  
|Child elements|[Algorithm](../../../2014/analysis-services/dev-guide/algorithm-element-assl.md), [AlgorithmParameters](../../../2014/analysis-services/dev-guide/algorithmparameter-element-assl.md), [AllowDrillThrough](../../../2014/analysis-services/dev-guide/allowdrillthrough-element-assl.md), [Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Collation](../../../2014/analysis-services/dev-guide/collation-element-assl.md), [Columns](../../../2014/analysis-services/dev-guide/columns-element-assl.md), [CreatedTimestamp](../../../2014/analysis-services/dev-guide/createdtimestamp-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [Language](../../../2014/analysis-services/dev-guide/language-element-assl.md), [LastProcessed](../../../2014/analysis-services/dev-guide/lastprocessed-element-assl.md), [LastSchemaUpdate](../../../2014/analysis-services/dev-guide/lastschemaupdate-element-assl.md), [MiningModelPermissions](../../../2014/analysis-services/dev-guide/miningmodelpermissions-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [State](../../../2014/analysis-services/dev-guide/state-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md),<br /><br /> [FoldingParameters](../../../2014/analysis-services/dev-guide/foldingparameters-element-assl.md)|  
  
## Remarks  
 The `FoldingParameters` element of the mining model is for internal use by the server, and is not supported for use in DDL statements.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModel>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  