---
title: "Algorithm Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Algorithm Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Algorithm"
helpviewer_keywords: 
  - "Algorithm element"
ms.assetid: 188bf7ce-c5c9-406a-af75-5a026c92a569
author: minewiskan
ms.author: owend
manager: craigg
---
# Algorithm Element (ASSL)
  Defines the algorithm used by a [MiningModel](../objects/miningmodel-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MiningModel>  
      ...  
   <Algorithm>...</Algorithm>  
      ...  
</MiningModel>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[MiningModel](../objects/miningmodel-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the `Algorithm` element is a string that identifies the algorithm. For example, the string could be *Microsoft_Naive_Bayes*, *Microsoft_Decision_Trees*, or *Microsoft_Clustering.* The string identifies algorithms supplied by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] and custom algorithms supplied by the user. Available values for the `Algorithm` element can be retrieved from the SERVICE_NAME column of the [DMSCHEMA_MINING_SERVICES](../../schema-rowsets/data-mining/dmschema-mining-services-rowset.md) schema rowset.  
  
 The element that corresponds to the parent of `Algorithm` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModel>. A closely related element in the AMO object model is <xref:Microsoft.AnalysisServices.MiningModelAlgorithms>.  
  
## See Also  
 [AlgorithmParameter Element &#40;ASSL&#41;](../objects/algorithmparameter-element-assl.md)   
 [AlgorithmParameters Element &#40;ASSL&#41;](../collections/algorithmparameters-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
