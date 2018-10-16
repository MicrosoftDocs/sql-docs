---
title: "MiningStructureColumn Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MiningStructureColumn Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MiningStructureColumn"
helpviewer_keywords: 
  - "MiningStructureColumn data type"
ms.assetid: b6d6e7a5-9c48-40c4-b147-8fcd5e429ae3
author: minewiskan
ms.author: owend
manager: craigg
---
# MiningStructureColumn Data Type (ASSL)
  Defines an abstract primitive data type that represents information about a column in a [MiningStructure](../objects/miningstructure-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MiningStructureColumn>  
   <Name>...</Name>  
   <ID>...</ID>  
   <Description>...</Description>  
   <Type>...</Type>  
   <Annotations>...</Annotations>  
</MiningStructureColumn>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|[ScalarMiningStructureColumn](miningstructurecolumn-data-type-assl.md), [TableMiningStructureColumn](tableminingstructurecolumn-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|None|  
|Child elements|[Annotations](../collections/annotations-element-assl.md), [Description](../properties/description-element-assl.md), [ID](../properties/id-element-assl.md), [Name](../properties/name-element-assl.md), [Type](../properties/type-element-miningstructurecolumn-assl.md)|  
|Derived elements|[Column](../objects/column-element-assl.md) ([Columns](../collections/columns-element-assl.md) collection of [MiningStructure](../objects/miningstructure-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningStructureColumn>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
