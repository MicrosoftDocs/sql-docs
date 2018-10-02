---
title: "MiningModelColumn Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# MiningModelColumn Data Type (ASSL)
  Defines a primitive data type that represents information about a column in a [MiningModel](../objects/miningmodel-element-assl.md) element.  
  
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
|Child elements|[Annotations](../collections/annotations-element-assl.md), [Columns](../collections/columns-element-assl.md), [Description](../properties/description-element-assl.md), [ID](../properties/id-element-assl.md), [ModelingFlags](../collections/modelingflags-element-assl.md), [Name](../properties/name-element-assl.md), [SourceColumnID](../properties/sourcecolumnid-element-assl.md), [Translations](../collections/translations-element-assl.md), [Usage](../properties/usage-element-dimensionattribute-assl.md)|  
|Derived elements|[Column](../objects/column-element-assl.md) ([Columns](../collections/columns-element-assl.md), collection of [MiningModel](../objects/miningmodel-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelColumn>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
