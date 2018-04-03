---
title: "MiningStructureColumn Data Type (ASSL) | Microsoft Docs"
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
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# MiningStructureColumn Data Type (ASSL)
  Defines an abstract primitive data type that represents information about a column in a [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md) element.  
  
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
|Derived data types|[ScalarMiningStructureColumn](../../../2014/analysis-services/dev-guide/scalarminingstructurecolumn-data-type-assl.md), [TableMiningStructureColumn](../../../2014/analysis-services/dev-guide/tableminingstructurecolumn-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|None|  
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Description](../../../2014/analysis-services/dev-guide/description-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Type](../../../2014/analysis-services/dev-guide/type-element-miningstructurecolumn-assl.md)|  
|Derived elements|[Column](../../../2014/analysis-services/dev-guide/column-element-assl.md) ([Columns](../../../2014/analysis-services/dev-guide/columns-element-assl.md) collection of [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningStructureColumn>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  