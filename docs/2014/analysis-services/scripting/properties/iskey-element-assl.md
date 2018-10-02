---
title: "IsKey Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "IsKey Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "IsKey"
helpviewer_keywords: 
  - "IsKey element"
ms.assetid: 523b26c8-5cce-415d-a360-9a0d8724b872
author: minewiskan
ms.author: owend
manager: craigg
---
# IsKey Element (ASSL)
  Indicates whether the column provides the key for the case in a [MiningStructure](../objects/miningstructure-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<ScalarMiningStructureColumn>  
   ...  
   <IsKey>...</IsKey>  
   ...  
</ScalarMiningStructureColumn>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ScalarMiningStructureColumn](../data-type/miningstructurecolumn-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 One or more columns can be designated as key columns for each level of a nested table structure.  
  
 The element that corresponds to the parent of `IsKey` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
