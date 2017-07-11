---
title: "ModelingFlag Element (ASSL) | Microsoft Docs"
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
  - "ModelingFlag Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "ModelingFlag"
helpviewer_keywords: 
  - "ModelingFlag element"
ms.assetid: c9af1b9a-506f-4cc1-acd7-e57698cb672c
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Data type and length|[MiningModelingFlag](../../../analysis-services/scripting/data-type/miningmodelingflag-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[ModelingFlags](../../../analysis-services/scripting/collections/modelingflags-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 A closely related element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningModelingFlags>.  
  
## See Also  
 [MiningModel Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/miningmodel-element-assl.md)   
 [MiningStructure Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/miningstructure-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  