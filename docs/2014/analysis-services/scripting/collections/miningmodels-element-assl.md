---
title: "MiningModels Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MiningModels Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MiningModels"
helpviewer_keywords: 
  - "MiningModels element"
ms.assetid: 19824d92-2e23-4e5e-b329-e46baf709c4a
author: minewiskan
ms.author: owend
manager: craigg
---
# MiningModels Element (ASSL)
  Contains the collection of [MiningModel](../objects/miningmodel-element-assl.md) elements associated with a [MiningStructure](../objects/miningstructure-element-assl.md).  
  
## Syntax  
  
```xml  
  
<MiningStructure>  
   ...  
   <MiningModels>  
      <MiningModel>...</MiningModel>  
   </MiningModels>  
   ...  
</MiningStructure>  
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
|Parent elements|[MiningStructure](../objects/miningstructure-element-assl.md)|  
|Child elements|[MiningModel](../objects/miningmodel-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
