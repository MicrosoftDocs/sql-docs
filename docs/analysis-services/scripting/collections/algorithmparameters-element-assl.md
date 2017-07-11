---
title: "AlgorithmParameters Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "AlgorithmParameters Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "AlgorithmParameters"
helpviewer_keywords: 
  - "AlgorithmParameters element"
ms.assetid: 240cbb60-7fa3-46ef-b5be-cd14c9ec10de
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# AlgorithmParameters Element (ASSL)
  Contains the collection of parameters for the algorithm used by a [MiningModel](../../../analysis-services/scripting/objects/miningmodel-element-assl.md) element.  
  
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
|Parent element|[MiningModel](../../../analysis-services/scripting/objects/miningmodel-element-assl.md)|  
|Child elements|[AlgorithmParameter](../../../analysis-services/scripting/objects/algorithmparameter-element-assl.md)|  
  
## Remarks  
 The **AlgorithmParameters** collection contains an extensible set of parameters, represented as name/value pairs, for a mining model algorithm. The set of applicable parameters is algorithm-dependent. For more information about algorithm parameters for a given algorithm, see the appropriate documentation for that algorithm.  
  
 Available algorithm parameters, including validation and display information, can be retrieved from the DMSCHEMA_MINING_SERVICE_PARAMETERS schema rowset.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AlgorithmParameterCollection>.  
  
## See Also  
 [Algorithm Element &#40;ASSL&#41;](../../../analysis-services/scripting/properties/algorithm-element-assl.md)   
 [DMSCHEMA_MINING_SERVICE_PARAMETERS Rowset](../../../analysis-services/schema-rowsets/data-mining/dmschema-mining-service-parameters-rowset.md)   
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  