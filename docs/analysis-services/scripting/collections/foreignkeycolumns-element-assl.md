---
title: "ForeignKeyColumns Element (ASSL) | Microsoft Docs"
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
  - "ForeignKeyColumns Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "ForeignKeyColumns"
helpviewer_keywords: 
  - "ForeignKeyColumns element"
ms.assetid: 0a673c1a-73dd-4217-aa41-56b340b5e1ab
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Parent elements|[MiningStructureColumn](../../../analysis-services/scripting/data-type/miningstructurecolumn-data-type-assl.md) of type [TableMiningStructureColumn](../../../analysis-services/scripting/data-type/tableminingstructurecolumn-data-type-assl.md)|  
|Child elements|[ForeignKeyColumn](../../../analysis-services/scripting/objects/foreignkeycolumn-element-assl.md) of type [DataItem](../../../analysis-services/scripting/data-type/dataitem-data-type-assl.md)|  
  
## See Also  
 [MiningStructure Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/miningstructure-element-assl.md)   
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  