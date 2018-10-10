---
title: "ForeignKeyColumns Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
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
|Parent elements|[MiningStructureColumn](../data-type/miningstructurecolumn-data-type-assl.md) of type [TableMiningStructureColumn](../data-type/tableminingstructurecolumn-data-type-assl.md)|  
|Child elements|[ForeignKeyColumn](../objects/column-element-assl.md) of type [DataItem](../data-type/dataitem-data-type-assl.md)|  
  
## See Also  
 [MiningStructure Element &#40;ASSL&#41;](../objects/miningstructure-element-assl.md)   
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
