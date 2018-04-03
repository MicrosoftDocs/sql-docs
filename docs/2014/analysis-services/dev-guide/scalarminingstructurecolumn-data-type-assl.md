---
title: "ScalarMiningStructureColumn Data Type (ASSL) | Microsoft Docs"
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
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ScalarMiningStructureColumn Data Type (ASSL)
  Defines a derived data type that represents a [MiningStructureColumn](../../../2014/analysis-services/dev-guide/miningstructurecolumn-data-type-assl.md) element that contains scalar values, as opposed to the nested tables associated with the [TableMiningStructureColumn](../../../2014/analysis-services/dev-guide/tableminingstructurecolumn-data-type-assl.md) element that contains nested tables.  
  
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
|Base data types|[MiningStructureColumn](../../../2014/analysis-services/dev-guide/miningstructurecolumn-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[ClassifiedColumnID](../../../2014/analysis-services/dev-guide/classifiedcolumnid-element-assl.md), [Content](../../../2014/analysis-services/dev-guide/content-element-assl.md), [DiscretizationBucketCount](../../../2014/analysis-services/dev-guide/discretizationbucketcount-element-assl.md), [DiscretizationMethod](../../../2014/analysis-services/dev-guide/discretizationmethod-element-assl.md), [Distribution](../../../2014/analysis-services/dev-guide/distribution-element-assl.md), [IsKey](../../../2014/analysis-services/dev-guide/iskey-element-assl.md), [KeyColumns](../../../2014/analysis-services/dev-guide/keycolumns-element-assl.md), [ModelingFlags](../../../2014/analysis-services/dev-guide/modelingflags-element-assl.md), [NameColumn](../../../2014/analysis-services/dev-guide/namecolumn-element-assl.md), [Source](../../../2014/analysis-services/dev-guide/source-element-binding-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md)|  
|Derived elements|[Column](../../../2014/analysis-services/dev-guide/column-element-assl.md) ([Columns](../../../2014/analysis-services/dev-guide/columns-element-assl.md) collection of [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  