---
title: "Distribution Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Distribution Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Distribution"
helpviewer_keywords: 
  - "Distribution element"
ms.assetid: a1309b90-8ad8-431b-a918-67f0cdd4fd20
author: minewiskan
ms.author: owend
manager: craigg
---
# Distribution Element (ASSL)
  Contains a provider-specific value that describes how scalar values are distributed within a column of a [MiningStructure](../objects/miningstructure-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<ScalarMiningStructureColumn>  
   ...  
   <Distribution>...</Distribution>  
   ...  
</ScalarMiningStructureColumn>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[ScalarMiningStructureColumn](../data-type/miningstructurecolumn-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The values available for the `Distribution` element, such as *Normal* or *Uniform,* are specific to each mining algorithm provider. For more information about valid `Distribution` values, see the mining algorithm provider documentation.  
  
 The element corresponding to the parent of `Distribution` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
