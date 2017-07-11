---
title: "MiningModelColumn Data Type (ASSL) | Microsoft Docs"
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
  - "MiningModelColumn Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "MiningModelColumn"
helpviewer_keywords: 
  - "MiningModelColumn data type"
ms.assetid: de8bf815-43b4-4983-bdb9-b67e8563be0e
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MiningModelColumn Data Type (ASSL)
  Defines a primitive data type that represents information about a column in a [MiningModel](../../../analysis-services/scripting/objects/miningmodel-element-assl.md) element.  
  
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
|Child elements|[Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md), [Columns](../../../analysis-services/scripting/collections/columns-element-assl.md), [Description](../../../analysis-services/scripting/properties/description-element-assl.md), [ID](../../../analysis-services/scripting/properties/id-element-assl.md), [ModelingFlags](../../../analysis-services/scripting/collections/modelingflags-element-assl.md), [Name](../../../analysis-services/scripting/properties/name-element-assl.md), [SourceColumnID](../../../analysis-services/scripting/properties/sourcecolumnid-element-assl.md), [Translations](../../../analysis-services/scripting/collections/translations-element-assl.md), [Usage](../../../analysis-services/scripting/properties/usage-element-dimensionattribute-assl.md)|  
|Derived elements|[Column](../../../analysis-services/scripting/objects/column-element-assl.md) ([Columns](../../../analysis-services/scripting/collections/columns-element-assl.md), collection of [MiningModel](../../../analysis-services/scripting/objects/miningmodel-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelColumn>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  