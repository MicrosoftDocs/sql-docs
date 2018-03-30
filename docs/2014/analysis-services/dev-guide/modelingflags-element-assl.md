---
title: "ModelingFlags Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "ModelingFlags Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ModelingFlags"
helpviewer_keywords: 
  - "ModelingFlags element"
ms.assetid: 83968c1e-aae8-4657-aa53-d971de0dc834
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ModelingFlags Element (ASSL)
  Contains the collection of [ModelingFlag](../../../2014/analysis-services/dev-guide/modelingflag-element-assl.md) elements for a column in a [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md) or a [MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md).  
  
## Syntax  
  
```xml  
  
<MiningModelColumn> <!-- or ScalarMiningStructureColumn -->  
   ...  
   <ModelingFlags>  
      <ModelingFlag xsi:type="MiningModelingFlag">...</ModelingFlag>  
   </ModelingFlags>  
   ...  
</MiningModelColumn>  
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
|Parent elements|[MiningModelColumn](../../../2014/analysis-services/dev-guide/miningmodelcolumn-data-type-assl.md), [ScalarMiningStructureColumn](../../../2014/analysis-services/dev-guide/scalarminingstructurecolumn-data-type-assl.md)|  
|Child elements|[ModelingFlag](../../../2014/analysis-services/dev-guide/modelingflag-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelingFlags>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  