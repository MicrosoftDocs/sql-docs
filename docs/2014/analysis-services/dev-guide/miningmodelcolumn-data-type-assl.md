---
title: "MiningModelColumn Data Type (ASSL) | Microsoft Docs"
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
  - "MiningModelColumn Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MiningModelColumn"
helpviewer_keywords: 
  - "MiningModelColumn data type"
ms.assetid: de8bf815-43b4-4983-bdb9-b67e8563be0e
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# MiningModelColumn Data Type (ASSL)
  Defines a primitive data type that represents information about a column in a [MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MiningModelColumn>  
      <Name>...</Name>  
      <ID>...</ID>  
      <Description>...</<Description>  
      <SourceColumnID>...</SourceColumnID>  
            <Usage>...</Usage>  
            <Translations>...</Translations>  
      <Columns>...</Columns>  
      <ModelingFlags>...</ModelingFlags>  
      <Annotations>...</Annotations>  
</MiningModelColumn>  
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
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Columns](../../../2014/analysis-services/dev-guide/columns-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [ModelingFlags](../../../2014/analysis-services/dev-guide/modelingflags-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [SourceColumnID](../../../2014/analysis-services/dev-guide/sourcecolumnid-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md), [Usage](../../../2014/analysis-services/dev-guide/usage-element-dimensionattribute-assl.md)|  
|Derived elements|[Column](../../../2014/analysis-services/dev-guide/column-element-assl.md) ([Columns](../../../2014/analysis-services/dev-guide/columns-element-assl.md), collection of [MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelColumn>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  