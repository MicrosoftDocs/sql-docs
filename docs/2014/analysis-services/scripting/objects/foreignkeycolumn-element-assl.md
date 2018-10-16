---
title: "ForeignKeyColumn Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ForeignKeyColumn Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ForeignKeyColumn"
helpviewer_keywords: 
  - "ForeignKeyColumn element"
ms.assetid: 6c00dcc6-8d5b-4293-8b72-c7a22e298c8d
author: minewiskan
ms.author: owend
manager: craigg
---
# ForeignKeyColumn Element (ASSL)
  Identifies the join to a parent table for a relational data source.  
  
## Syntax  
  
```xml  
  
<ForeignKeyColumns>  
   <ForeignKeyColumn xsi:type="DataItem">...</ForeignKeyColumn>  
</ForeignKeyColumns>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[DataItem](../data-type/dataitem-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[ForeignKeyColumns](../collections/columns-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 For additional information about the `DataItem` type, including a table of Analysis Services Scripting Language (ASSL) objects and properties of the `DataItem` type, see [DataItem Data Type &#40;ASSL&#41;](../data-type/dataitem-data-type-assl.md).  
  
 The element that corresponds to the parent of the `ForeignKeyColumns` collection in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TableMiningStructureColumn>.  
  
## See Also  
 [TableMiningStructureColumn Data Type &#40;ASSL&#41;](../data-type/miningstructurecolumn-data-type-assl.md)   
 [MiningStructureColumn Data Type &#40;ASSL&#41;](../data-type/miningstructurecolumn-data-type-assl.md)   
 [MiningStructure Element &#40;ASSL&#41;](miningstructure-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
