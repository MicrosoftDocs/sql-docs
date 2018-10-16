---
title: "AlgorithmParameter Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AlgorithmParameter Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AlgorithmParameter"
helpviewer_keywords: 
  - "AlgorithmParameter element"
ms.assetid: 73211495-065c-43c6-a486-be6044617263
author: minewiskan
ms.author: owend
manager: craigg
---
# AlgorithmParameter Element (ASSL)
  Defines a parameter for the algorithm used by a [MiningModel](miningmodel-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<AlgorithmParameters>  
   <AlgorithmParameter>  
      <Name>...</Name>  
      <Value>...</Value>  
   </AlgorithmParameter>  
</AlgorithmParameters>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AlgorithmParameters](../collections/algorithmparameters-element-assl.md)|  
|Child elements|[Name](../properties/name-element-assl.md), [Value](../properties/value-element-assl.md)|  
  
## Remarks  
 An `AlgorithmParameter` is a parameter for a mining model algorithm. The `AlgorithmParameter` represents this parameter as a name/value pair. The set of applicable parameters that an `AlgorithmParameter` can represent is algorithm-dependent. For more information about algorithm parameters for a given algorithm, see the appropriate documentation for that algorithm.  
  
 Available algorithm parameters, including validation and display information, can be retrieved from the [DMSCHEMA_MINING_SERVICE_PARAMETERS](../../schema-rowsets/data-mining/dmschema-mining-service-parameters-rowset.md) schema rowset.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AlgorithmParameter>.  
  
## See Also  
 [MiningModel Element &#40;ASSL&#41;](miningmodel-element-assl.md)   
 [Algorithm Element &#40;ASSL&#41;](../properties/algorithm-element-assl.md)   
 [Objects &#40;ASSL&#41;](objects-assl.md)  
  
  
