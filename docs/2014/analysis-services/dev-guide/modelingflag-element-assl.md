---
title: "ModelingFlag Element (ASSL) | Microsoft Docs"
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
  - "ModelingFlag Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ModelingFlag"
helpviewer_keywords: 
  - "ModelingFlag element"
ms.assetid: c9af1b9a-506f-4cc1-acd7-e57698cb672c
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ModelingFlag Element (ASSL)
  Contains a modeling flag for a column in a mining structure or a mining model.  
  
## Syntax  
  
```xml  
  
<ModelingFlags>  
   <ModelingFlag xsi:type="MiningModelingFlag">...</ModelingFlag>  
</ModelingFlags>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[MiningModelingFlag](../../../2014/analysis-services/dev-guide/miningmodelingflag-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[ModelingFlags](../../../2014/analysis-services/dev-guide/modelingflags-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 A closely related element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelingFlags>.  
  
## See Also  
 [MiningModel Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md)   
 [MiningStructure Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  