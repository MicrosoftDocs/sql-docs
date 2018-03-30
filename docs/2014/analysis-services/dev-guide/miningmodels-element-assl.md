---
title: "MiningModels Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
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
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# MiningModels Element (ASSL)
  Contains the collection of [MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md) elements associated with a [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md).  
  
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
|Parent elements|[MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md)|  
|Child elements|[MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  