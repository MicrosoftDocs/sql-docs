---
title: "AlgorithmParameters Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AlgorithmParameters Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AlgorithmParameters"
helpviewer_keywords: 
  - "AlgorithmParameters element"
ms.assetid: 240cbb60-7fa3-46ef-b5be-cd14c9ec10de
author: minewiskan
ms.author: owend
manager: craigg
---
# AlgorithmParameters Element (ASSL)
  Contains the collection of parameters for the algorithm used by a [MiningModel](../objects/miningmodel-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MiningModel>  
   ...  
   <AlgorithmParameters>  
      <AlgorithmParameter>...</AlgorithmParameter>  
   </AlgorithmParameters>  
   ...  
</MiningModel>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None (collection)|  
|Default value|None (collection)|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[MiningModel](../objects/miningmodel-element-assl.md)|  
|Child elements|[AlgorithmParameter](../objects/algorithmparameter-element-assl.md)|  
  
## Remarks  
 The `AlgorithmParameters` collection contains an extensible set of parameters, represented as name/value pairs, for a mining model algorithm. The set of applicable parameters is algorithm-dependent. For more information about algorithm parameters for a given algorithm, see the appropriate documentation for that algorithm.  
  
 Available algorithm parameters, including validation and display information, can be retrieved from the DMSCHEMA_MINING_SERVICE_PARAMETERS schema rowset.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AlgorithmParameterCollection>.  
  
## See Also  
 [Algorithm Element &#40;ASSL&#41;](../properties/algorithm-element-assl.md)   
 [DMSCHEMA_MINING_SERVICE_PARAMETERS Rowset](../../schema-rowsets/data-mining/dmschema-mining-service-parameters-rowset.md)   
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
