---
title: "Database Element (ASSL) | Microsoft Docs"
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
  - "Database Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DATABASE"
helpviewer_keywords: 
  - "Database element"
ms.assetid: c3bc7eaf-ed0d-4395-a3b7-8d9cfacfe911
caps.latest.revision: 40
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Database Element (ASSL)
  Defines a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database.  
  
## Syntax  
  
```xml  
  
<Databases>  
   <Database>  
      <Name>...</Name>  
      <ID>...</ID>  
      <CreatedTimestamp>...</CreatedTimestamp>  
      <LastSchemaUpdate>...</LastSchemaUpdate>  
      <LastUpdate>...</LastUpdate>  
      <Description>...</Description>  
      <State>   </State>  
      <AggregationPrefix>...</AggregationPrefix>  
      <EstimatedSize>...</EstimatedSize>  
      <LastProcessed>...</LastProcessed>  
      <Language>...</Language>  
            <Collation>...</Collation>  
      <Visible>...</Visible>  
      <MasterDatasourceID>...</MasterDataSourceID>  
      <Accounts>...</Accounts>  
      <DataSources>...</DataSources>  
      <DataSourceViews>...</DataSourceViews>  
      <Dimensions>...</Dimensions>  
      <Cubes>...</Cubes>  
      <MiningStructures>...</MiningStructures>  
            <Roles>...</Roles>  
      <Assemblies>...</Assemblies>  
      <DatabasePermissions>...</DatabasePermissions>  
            <Translations>...</Translations>  
      <Annotations>...</Annotations>  
   </Database>  
</Databases>  
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
|Parent elements|[Databases](../../../2014/analysis-services/dev-guide/databases-element-assl.md)|  
|Child elements|[Accounts](../../../2014/analysis-services/dev-guide/accounts-element-assl.md), [AggregationPrefix](../../../2014/analysis-services/dev-guide/aggregationprefix-element-assl.md), [Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Assemblies](../../../2014/analysis-services/dev-guide/assemblies-element-assl.md), [Collation](../../../2014/analysis-services/dev-guide/collation-element-assl.md), [CreatedTimestamp](../../../2014/analysis-services/dev-guide/createdtimestamp-element-assl.md), [Cubes](../../../2014/analysis-services/dev-guide/cubes-element-assl.md), [DatabasePermissions](../../../2014/analysis-services/dev-guide/databasepermissions-element-assl.md), [DataSources](../../../2014/analysis-services/dev-guide/datasources-element-assl.md), [DataSourceViews](../../../2014/analysis-services/dev-guide/datasourceviews-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [Dimensions](../../../2014/analysis-services/dev-guide/dimensions-element-assl.md), [EstimatedSize](../../../2014/analysis-services/dev-guide/estimatedsize-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [Language](../../../2014/analysis-services/dev-guide/language-element-assl.md), [LastProcessed](../../../2014/analysis-services/dev-guide/lastprocessed-element-assl.md), [LastSchemaUpdate](../../../2014/analysis-services/dev-guide/lastschemaupdate-element-assl.md), [LastUpdate](../../../2014/analysis-services/dev-guide/lastupdate-element-assl.md), [MasterDatasourceID](../../../2014/analysis-services/dev-guide/masterdatasourceid-element-assl.md), [MiningStructures](../../../2014/analysis-services/dev-guide/miningstructures-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Roles](../../../2014/analysis-services/dev-guide/roles-element-assl.md), [State](../../../2014/analysis-services/dev-guide/state-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md), [Visible](../../../2014/analysis-services/dev-guide/visible-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Database>.  
  
## See Also  
 [Server Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/server-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  