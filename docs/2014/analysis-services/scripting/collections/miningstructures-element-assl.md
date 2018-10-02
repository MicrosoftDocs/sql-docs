---
title: "MiningStructures Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MiningStructures Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MiningStructures"
helpviewer_keywords: 
  - "MiningStructures element"
ms.assetid: d8144b85-c836-4dcf-96b4-36b9dfd4211d
author: minewiskan
ms.author: owend
manager: craigg
---
# MiningStructures Element (ASSL)
  Contains the collection of [MiningStructure](../objects/miningstructure-element-assl.md) elements in a [Database](../objects/database-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Database>  
   ...  
   <MiningStructures>  
      <MiningStructure>...</MiningStructure>  
   </MiningStructures>  
   ...  
</Database>  
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
|Parent elements|[Database](../objects/database-element-assl.md)|  
|Child elements|[MiningStructure](../objects/miningstructure-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningStructureCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
