---
title: "ForeignKeyColumns Element (ASSL) | Microsoft Docs"
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
  - "ForeignKeyColumns Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ForeignKeyColumns"
helpviewer_keywords: 
  - "ForeignKeyColumns element"
ms.assetid: 0a673c1a-73dd-4217-aa41-56b340b5e1ab
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ForeignKeyColumns Element (ASSL)
  Contains the collection of columns that identify the join to the parent table for a relational data source.  
  
## Syntax  
  
```xml  
  
<MiningStructureColumn xsi:type="TableMiningStructureColumn">  
   ...  
   <ForeignKeyColumns>  
      <ForeignKeyColumn xsi:type="DataItem">...</ForeignKeyColumn>  
...</ForeignKeyColumns>  
   ...  
</MiningStructureColumn>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[MiningStructureColumn](../../../2014/analysis-services/dev-guide/miningstructurecolumn-data-type-assl.md) of type [TableMiningStructureColumn](../../../2014/analysis-services/dev-guide/tableminingstructurecolumn-data-type-assl.md)|  
|Child elements|[ForeignKeyColumn](../../../2014/analysis-services/dev-guide/foreignkeycolumn-element-assl.md) of type [DataItem](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md)|  
  
## See Also  
 [MiningStructure Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md)   
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  