---
title: "MiningStructure Element (ASSL) | Microsoft Docs"
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
  - "MiningStructure Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MiningStructure"
helpviewer_keywords: 
  - "MiningStructure element"
ms.assetid: b943cd92-0ed8-4bd8-8fbc-7dab0534aede
caps.latest.revision: 48
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# MiningStructure Element (ASSL)
  Defines the structure for a set of mining models.  
  
## Syntax  
  
```xml  
  
<MiningStructures>  
   <MiningStructure>  
      <Name>...</Name>  
      <ID>...</ID>  
      <Description>...</<Description>  
      <Source>...</Source>  
      <CreatedTimestamp>...</<CreatedTimestamp>  
      <LastSchemaUpdate>...</LastSchemaUpdate>  
      <LastProcessed>...</LastProcessed>  
      <Translations>...</Translations>  
      <Language>...</Language>  
            <Collation>...</Collation>  
      <ErrorConfiguration>...</ErrorConfiguration>  
      <CacheMode>...</CacheMode>  
            <Columns>...</Columns>  
      <State>...</State>  
      <HoldoutActualSize>...</HoldoutActualSize>  
      <HoldoutMaxCases>...</HoldoutMaxCases>  
      <HoldoutMaxPercent>...</HoldoutMaxPercent>  
      <HoldoutSeed>...</HoldoutSeed>        
            <MiningStructurePermissions>...</<MiningStructurePermissions>  
            <MiningModels>...</MiningModels>  
            <Annotations>...</Annotations>  
   </MiningStructure>  
</MiningStructures>  
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
|Parent elements|[MiningStructures](../../../2014/analysis-services/dev-guide/miningstructures-element-assl.md)|  
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [CacheMode](../../../2014/analysis-services/dev-guide/cachemode-element-assl.md), [Collation](../../../2014/analysis-services/dev-guide/collation-element-assl.md), [Columns](../../../2014/analysis-services/dev-guide/columns-element-assl.md), [CreatedTimestamp](../../../2014/analysis-services/dev-guide/createdtimestamp-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [ErrorConfiguration](../../../2014/analysis-services/dev-guide/errorconfiguration-element-assl.md),<br /><br /> [HoldoutActualSize](../../../2014/analysis-services/dev-guide/holdoutactualsize-element.md),<br /><br /> [HoldoutMaxCases](../../../2014/analysis-services/dev-guide/holdoutmaxcases-element.md),<br /><br /> [HoldoutMaxPercent](../../../2014/analysis-services/dev-guide/holdoutmaxpercent-element.md),<br /><br /> [HoldoutSeed](../../../2014/analysis-services/dev-guide/holdoutseed-element.md),<br /><br /> [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [Language](../../../2014/analysis-services/dev-guide/language-element-assl.md), [LastProcessed](../../../2014/analysis-services/dev-guide/lastprocessed-element-assl.md), [LastSchemaUpdate](../../../2014/analysis-services/dev-guide/lastschemaupdate-element-assl.md), [MiningModels](../../../2014/analysis-services/dev-guide/miningmodels-element-assl.md), [MiningStructurePermissions](../../../2014/analysis-services/dev-guide/miningstructurepermissions-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md), [State](../../../2014/analysis-services/dev-guide/state-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md)|  
  
## Remarks  
 The mining structure defines the columns and the bindings. After defining a mining structure, you can use that structure to define many mining models. The mining structure, and each mining model it contains, can be processed independently.  
  
> [!NOTE]  
>  The holdout properties, `HoldoutMaxCases`, `HoldoutMaxPercent`, `HoldoutSeed`, and `HoldoutActualSize`, were introduced in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]. They enable you to define a partition on a mining structure that acts as the test set for all the mining models that are associated with the structure. [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] does not support these properties. Therefore, if you try to use these properties on an instance of [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will return an error.  
  
## Drillthrough to Structure Columns  
 In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], a new permission element has been added to the [MiningStructurePermissions Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/miningstructurepermissions-element-assl.md) collection. If you add `AllowDrillthrough` permission to both the [MiningStructurePermissions](../../../2014/analysis-services/dev-guide/miningstructurepermissions-element-assl.md) and [MiningModelPermission](../../../2014/analysis-services/dev-guide/miningmodelpermission-element-assl.md) collections, drillthrough is enabled from the mining model to the structure, in such a way that members of a role that has `AllowDrillthrough` permissions on the model can query the data mining model, and return structure columns that were not included in the model.  
  
 Therefore, to protect sensitive data or personal information, you should construct your data source view to mask sensitive information, and grant `AllowDrillthrough` permission on a mining structure only when necessary. For more information, see [AllowDrillThrough Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/allowdrillthrough-element-assl.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningStructure>.  
  
## See Also  
 [MiningModel Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)   
 [SELECT &#40;DMX&#41;](~/dmx/select-dmx.md)  
  
  