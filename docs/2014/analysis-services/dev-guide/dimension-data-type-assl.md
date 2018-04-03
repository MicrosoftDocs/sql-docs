---
title: "Dimension Data Type (ASSL) | Microsoft Docs"
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
  - "Dimension Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Dimension data type"
ms.assetid: 3fe6adc2-5206-44c3-a689-a731705f43ca
caps.latest.revision: 17
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Dimension Data Type (ASSL)
  Defines a primitive data type that represents a database dimension.  
  
## Syntax  
  
```xml  
  
<Dimension>  
   <Name>...</Name>  
   <ID>...</ID>  
   <CreatedTimestamp>...</CreatedTimestamp>  
   <LastSchemaUpdate>...</LastSchemaUpdate>  
   <Description>...</Description>  
   <Source>...</Source>  
   <MiningModelID>...</MiningModelID>  
   <Type>...</Type>  
   <UnknownMember>...</UnknownMember>  
   <MdxMissingMemberMode>...</MdxMissingMemberMode>  
   <ErrorConfiguration>...</ErrorConfiguration>  
   <StorageMode>...</StorageMode>  
   <WriteEnabled>...</WriteEnabled>  
   <ProcessingPriority>...</ProcessingPriority>  
   <LastProcessed>...</LastProcessed>  
   <DimensionPermissions>...</DimensionPermissions>  
   <DependsOnDimensionID>...</DependsOnDimensionID>  
   <Language>...</Language>  
   <Collation>...</Collation>  
   <UnknownMemberName>...</UnknownMemberName>  
   <UnknownMemberTranslations>...</UnknownMemberTranslations>  
   <State>...</State>  
   <ProactiveCaching>...</ProactiveCaching>  
   <ProcessingMode>...</ProcessingMode>  
   <CurrentStorageMode>...</CurrentStorageMode>  
   <Translations>...</Translations>  
   <Attributes>...</Attributes>  
   <AttributeAllMemberName>...</AttributeAllMemberName>  
   <AttributeAllMemberTranslations>...</AttributeAllMemberTranslations>  
   <Hierarchies>...</Hierarchies>  
   <Relationships>...</Annotations>  
   <Annotations>...</Annotations>  
</Dimension>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [AttributeAllMemberName](../../../2014/analysis-services/dev-guide/attributeallmembername-element-assl.md), [AttributeAllMemberTranslations](../../../2014/analysis-services/dev-guide/attributeallmembertranslations-element-assl.md), [Attributes](../../../2014/analysis-services/dev-guide/attributes-element-assl.md), [Collation](../../../2014/analysis-services/dev-guide/collation-element-assl.md), [CreatedTimestamp](../../../2014/analysis-services/dev-guide/createdtimestamp-element-assl.md), [CurrentStorageMode](../../../2014/analysis-services/dev-guide/currentstoragemode-element-assl.md), [DependsOnDimensionID](../../../2014/analysis-services/dev-guide/dependsondimensionid-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [DimensionPermissions](../../../2014/analysis-services/dev-guide/dimensionpermissions-element-assl.md), [ErrorConfiguration](../../../2014/analysis-services/dev-guide/errorconfiguration-element-assl.md), [Hierarchies](../../../2014/analysis-services/dev-guide/hierarchies-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [Language](../../../2014/analysis-services/dev-guide/language-element-assl.md), [LastProcessed](../../../2014/analysis-services/dev-guide/lastprocessed-element-assl.md), [LastSchemaUpdate](../../../2014/analysis-services/dev-guide/lastschemaupdate-element-assl.md), [MdxMissingMemberMode](../../../2014/analysis-services/dev-guide/mdxmissingmembermode-element-assl.md), [MiningModelID](../../../2014/analysis-services/dev-guide/miningmodelid-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [ProactiveCaching](../../../2014/analysis-services/dev-guide/proactivecaching-element-assl.md), [ProcessingMode](../../../2014/analysis-services/dev-guide/processingmode-element-assl.md), [ProcessingPriority](../../../2014/analysis-services/dev-guide/processingpriority-element-assl.md), [Relationships](../../../2014/analysis-services/dev-guide/relationships-element-assl.md), [Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md), [State](../../../2014/analysis-services/dev-guide/state-element-assl.md), [StorageMode](../../../2014/analysis-services/dev-guide/storagemode-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md), [Type](../../../2014/analysis-services/dev-guide/type-element-dimension-assl.md), [UnknownMember](../../../2014/analysis-services/dev-guide/unknownmember-element-assl.md), [UnknownMemberName](../../../2014/analysis-services/dev-guide/unknownmembername-element-assl.md), [UnknownMemberTranslations](../../../2014/analysis-services/dev-guide/unknownmembertranslations-element-assl.md), [WriteEnabled](../../../2014/analysis-services/dev-guide/writeenabled-element-assl.md)|  
|Derived elements|[Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md)|  
  
## Remarks  
 This data type has the following validations under DeploymentMode values 1 (SharePoint) and 2 (Tabular).  
  
-   *WriteEnabled* child element must be set to `False`  
  
-   *UnknownMember* child element must be set to `AutomaticNull`  
  
-   All unique attributes must have *NullProcessing* child element set to `Error`  
  
 The following child attributes are not supported under DeploymentMode values 1 (SharePoint) and 2 (Tabular).  
  
-   *DimensionPermissions*  
  
-   *MiningModelID*  
  
-   *ProactiveCaching*  
  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Dimension>, <xref:Microsoft.AnalysisServices.AggregationDimension>, <xref:Microsoft.AnalysisServices.AggregationDesignDimension>, <xref:Microsoft.AnalysisServices.CubeDimension>, <xref:Microsoft.AnalysisServices.MeasureGroupDimension>, and <xref:Microsoft.AnalysisServices.PerspectiveDimension>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  