---
title: "MiningStructure Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
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
|Parent elements|[MiningStructures](../collections/miningstructures-element-assl.md)|  
|Child elements|[Annotations](../collections/annotations-element-assl.md), [CacheMode](../properties/cachemode-element-assl.md), [Collation](../properties/collation-element-assl.md), [Columns](../collections/columns-element-assl.md), [CreatedTimestamp](../properties/createdtimestamp-element-assl.md), [Description](../properties/description-element-assl.md), [ErrorConfiguration](errorconfiguration-element-assl.md),<br /><br /> [HoldoutActualSize](../properties/holdoutactualsize-element.md),<br /><br /> [HoldoutMaxCases](../properties/holdoutmaxcases-element.md),<br /><br /> [HoldoutMaxPercent](../properties/holdoutmaxpercent-element.md),<br /><br /> [HoldoutSeed](../properties/holdoutseed-element.md),<br /><br /> [ID](../properties/id-element-assl.md), [Language](../properties/language-element-assl.md), [LastProcessed](../properties/lastprocessed-element-assl.md), [LastSchemaUpdate](../properties/lastschemaupdate-element-assl.md), [MiningModels](../collections/miningmodels-element-assl.md), [MiningStructurePermissions](../collections/miningstructurepermissions-element-assl.md), [Name](../properties/name-element-assl.md), [Source](../properties/source-element-binding-assl.md), [State](../properties/state-element-assl.md), [Translations](../collections/translations-element-assl.md)|  
  
## Remarks  
 The mining structure defines the columns and the bindings. After defining a mining structure, you can use that structure to define many mining models. The mining structure, and each mining model it contains, can be processed independently.  
  
> [!NOTE]  
>  The holdout properties, `HoldoutMaxCases`, `HoldoutMaxPercent`, `HoldoutSeed`, and `HoldoutActualSize`, were introduced in [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)]. They enable you to define a partition on a mining structure that acts as the test set for all the mining models that are associated with the structure. [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] does not support these properties. Therefore, if you try to use these properties on an instance of [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] will return an error.  
  
## Drillthrough to Structure Columns  
 In [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)], a new permission element has been added to the [MiningStructurePermissions Element &#40;ASSL&#41;](../collections/miningstructurepermissions-element-assl.md) collection. If you add `AllowDrillthrough` permission to both the [MiningStructurePermissions](../collections/miningstructurepermissions-element-assl.md) and [MiningModelPermission](miningmodelpermission-element-assl.md) collections, drillthrough is enabled from the mining model to the structure, in such a way that members of a role that has `AllowDrillthrough` permissions on the model can query the data mining model, and return structure columns that were not included in the model.  
  
 Therefore, to protect sensitive data or personal information, you should construct your data source view to mask sensitive information, and grant `AllowDrillthrough` permission on a mining structure only when necessary. For more information, see [AllowDrillThrough Element &#40;ASSL&#41;](../properties/allowdrillthrough-element-assl.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningStructure>.  
  
## See Also  
 [MiningModel Element &#40;ASSL&#41;](miningmodel-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)   
 [SELECT &#40;DMX&#41;](/sql/dmx/select-dmx)  
  
  
