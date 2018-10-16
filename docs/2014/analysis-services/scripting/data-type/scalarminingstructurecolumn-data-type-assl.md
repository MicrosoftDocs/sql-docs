---
title: "ScalarMiningStructureColumn Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ScalarMiningStructureColumn Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ScalarMiningStructureColumn"
helpviewer_keywords: 
  - "ScalarMiningStructureColumn data type"
ms.assetid: 8f4afc15-601c-4189-bc45-f5a216aed879
author: minewiskan
ms.author: owend
manager: craigg
---
# ScalarMiningStructureColumn Data Type (ASSL)
  Defines a derived data type that represents a [MiningStructureColumn](miningstructurecolumn-data-type-assl.md) element that contains scalar values, as opposed to the nested tables associated with the [TableMiningStructureColumn](tableminingstructurecolumn-data-type-assl.md) element that contains nested tables.  
  
## Syntax  
  
```xml  
  
<ScalarMiningStructureColumn>  
   <!-- The following elements extend MiningStructureColumn -->  
   <IsKey>...</IsKey>  
   <Source>...</Source>  
   <Distribution>...</Distribution>  
   <ModelingFlags>...</ModelingFlags>  
   <Content>...</Content>  
   <ClassifiedColumnID>...</<ClassifiedColumnI  
   <DiscretizationMethod>...</DiscretizationMethod>  
   <DiscretizationBucketCount>...</DiscretizationBucketCount>  
   <KeyColumns>...</KeyColumns>  
   <NameColumn>...</NameColumn>  
   <Translations>...</Translations>  
</ScalarMiningStructureColumn>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[MiningStructureColumn](miningstructurecolumn-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[ClassifiedColumnID](../properties/id-element-assl.md), [Content](../properties/content-element-assl.md), [DiscretizationBucketCount](../properties/discretizationbucketcount-element-assl.md), [DiscretizationMethod](../properties/discretizationmethod-element-assl.md), [Distribution](../properties/distribution-element-assl.md), [IsKey](../properties/iskey-element-assl.md), [KeyColumns](../collections/columns-element-assl.md), [ModelingFlags](../collections/modelingflags-element-assl.md), [NameColumn](../objects/column-element-assl.md), [Source](../properties/source-element-binding-assl.md), [Translations](../collections/translations-element-assl.md)|  
|Derived elements|[Column](../objects/column-element-assl.md) ([Columns](../collections/columns-element-assl.md) collection of [MiningStructure](../objects/miningstructure-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
