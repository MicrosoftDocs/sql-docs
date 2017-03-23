---
title: "MiningStructure Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "MiningStructure Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "MiningStructure"
helpviewer_keywords: 
  - "MiningStructure element"
ms.assetid: b943cd92-0ed8-4bd8-8fbc-7dab0534aede
caps.latest.revision: 48
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Parent elements|[MiningStructures](../../../analysis-services/scripting/collections/miningstructures-element-assl.md)|  
|Child elements|[Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [CacheMode](../../../analysis-services/scripting/properties/cachemode-element-assl.md), [Collation](../../../analysis-services/scripting/properties/collation-element-assl.md), [Columns](../../../analysis-services/scripting/collections/columns-element-assl.md), [CreatedTimestamp](../../../analysis-services/scripting/properties/createdtimestamp-element-assl.md), [Description](../../../analysis-services/scripting/properties/description-element-assl.md), [ErrorConfiguration](../../../analysis-services/scripting/objects/errorconfiguration-element-assl.md),<br /><br /> [HoldoutActualSize](../../../analysis-services/scripting/properties/holdoutactualsize-element.md),<br /><br /> [HoldoutMaxCases](../../../analysis-services/scripting/properties/holdoutmaxcases-element.md),<br /><br /> [HoldoutMaxPercent](../../../analysis-services/scripting/properties/holdoutmaxpercent-element.md),<br /><br /> [HoldoutSeed](../../../analysis-services/scripting/properties/holdoutseed-element.md),<br /><br /> [ID](../../../analysis-services/scripting/properties/id-element-assl.md), [Language](../../../analysis-services/scripting/properties/language-element-assl.md), [LastProcessed](../../../analysis-services/scripting/properties/lastprocessed-element-assl.md), [LastSchemaUpdate](../../../analysis-services/scripting/properties/lastschemaupdate-element-assl.md), [MiningModels](../../../analysis-services/scripting/collections/miningmodels-element-assl.md), [MiningStructurePermissions](../../../analysis-services/scripting/collections/miningstructurepermissions-element-assl.md), [Name](../../../analysis-services/scripting/properties/name-element-assl.md), [Source](../../../analysis-services/scripting/properties/source-element-binding-assl.md), [State](../../../analysis-services/scripting/properties/state-element-assl.md), [Translations](../../../analysis-services/scripting/collections/translations-element-assl.md)|  
  
## Remarks  
 The mining structure defines the columns and the bindings. After defining a mining structure, you can use that structure to define many mining models. The mining structure, and each mining model it contains, can be processed independently.  
  
> [!NOTE]  
>  The holdout properties, **HoldoutMaxCases**, **HoldoutMaxPercent**, **HoldoutSeed**, and **HoldoutActualSize**, were introduced in [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)]. They enable you to define a partition on a mining structure that acts as the test set for all the mining models that are associated with the structure. [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] does not support these properties. Therefore, if you try to use these properties on an instance of [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] will return an error.  
  
## Drillthrough to Structure Columns  
 In [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)], a new permission element has been added to the [MiningStructurePermissions Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/miningstructurepermissions-element-assl.md) collection. If you add **AllowDrillthrough** permission to both the [MiningStructurePermissions](../../../analysis-services/scripting/collections/miningstructurepermissions-element-assl.md) and [MiningModelPermission](../../../analysis-services/scripting/objects/miningmodelpermission-element-assl.md) collections, drillthrough is enabled from the mining model to the structure, in such a way that members of a role that has **AllowDrillthrough** permissions on the model can query the data mining model, and return structure columns that were not included in the model.  
  
 Therefore, to protect sensitive data or personal information, you should construct your data source view to mask sensitive information, and grant **AllowDrillthrough** permission on a mining structure only when necessary. For more information, see [AllowDrillThrough Element &#40;ASSL&#41;](../../../analysis-services/scripting/properties/allowdrillthrough-element-assl.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningStructure>.  
  
## See Also  
 [MiningModel Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/miningmodel-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)   
 [SELECT &#40;DMX&#41;](../../../dmx/select-dmx.md)  
  
  