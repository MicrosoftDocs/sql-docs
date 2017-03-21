---
title: "Algorithm Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Algorithm Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Algorithm"
helpviewer_keywords: 
  - "Algorithm element"
ms.assetid: 188bf7ce-c5c9-406a-af75-5a026c92a569
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Algorithm Element (ASSL)
  Defines the algorithm used by a [MiningModel](../../../analysis-services/scripting/objects/miningmodel-element-assl.md) element.  
  
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
|Parent element|[MiningModel](../../../analysis-services/scripting/objects/miningmodel-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of the **Algorithm** element is a string that identifies the algorithm. For example, the string could be *Microsoft_Naive_Bayes*, *Microsoft_Decision_Trees*, or *Microsoft_Clustering.* The string identifies algorithms supplied by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] and custom algorithms supplied by the user. Available values for the **Algorithm** element can be retrieved from the SERVICE_NAME column of the [DMSCHEMA_MINING_SERVICES](../../../analysis-services/schema-rowsets/data-mining/dmschema-mining-services-rowset.md) schema rowset.  
  
 The element that corresponds to the parent of **Algorithm** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModel>. A closely related element in the AMO object model is <xref:Microsoft.AnalysisServices.MiningModelAlgorithms>.  
  
## See Also  
 [AlgorithmParameter Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/algorithmparameter-element-assl.md)   
 [AlgorithmParameters Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/algorithmparameters-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  