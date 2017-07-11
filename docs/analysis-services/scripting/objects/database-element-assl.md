---
title: "Database Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Database Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "DATABASE"
helpviewer_keywords: 
  - "Database element"
ms.assetid: c3bc7eaf-ed0d-4395-a3b7-8d9cfacfe911
caps.latest.revision: 41
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Database Element (ASSL)
  Defines a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database.  
  
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
|Parent elements|[Databases](../../../analysis-services/scripting/collections/databases-element-assl.md)|  
|Child elements|[Accounts](../../../analysis-services/scripting/collections/accounts-element-assl.md), [AggregationPrefix](../../../analysis-services/scripting/properties/aggregationprefix-element-assl.md), [Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [Assemblies](../../../analysis-services/scripting/collections/assemblies-element-assl.md), [Collation](../../../analysis-services/scripting/properties/collation-element-assl.md), [CreatedTimestamp](../../../analysis-services/scripting/properties/createdtimestamp-element-assl.md), [Cubes](../../../analysis-services/scripting/collections/cubes-element-assl.md), [DatabasePermissions](../../../analysis-services/scripting/collections/databasepermissions-element-assl.md), [DataSources](../../../analysis-services/scripting/collections/datasources-element-assl.md), [DataSourceViews](../../../analysis-services/scripting/collections/datasourceviews-element-assl.md), [Description](../../../analysis-services/scripting/properties/description-element-assl.md), [Dimensions](../../../analysis-services/scripting/collections/dimensions-element-assl.md), [EstimatedSize](../../../analysis-services/scripting/properties/estimatedsize-element-assl.md), [ID](../../../analysis-services/scripting/properties/id-element-assl.md), [Language](../../../analysis-services/scripting/properties/language-element-assl.md), [LastProcessed](../../../analysis-services/scripting/properties/lastprocessed-element-assl.md), [LastSchemaUpdate](../../../analysis-services/scripting/properties/lastschemaupdate-element-assl.md), [LastUpdate](../../../analysis-services/scripting/properties/lastupdate-element-assl.md), [MasterDatasourceID](../../../analysis-services/scripting/properties/masterdatasourceid-element-assl.md), [MiningStructures](../../../analysis-services/scripting/collections/miningstructures-element-assl.md), [Name](../../../analysis-services/scripting/properties/name-element-assl.md), [Roles](../../../analysis-services/scripting/collections/roles-element-assl.md), [State](../../../analysis-services/scripting/properties/state-element-assl.md), [Translations](../../../analysis-services/scripting/collections/translations-element-assl.md), [Visible](../../../analysis-services/scripting/properties/visible-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Database>.  
  
## See Also  
 [Server Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/server-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  