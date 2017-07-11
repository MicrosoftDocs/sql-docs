---
title: "CacheMode Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "CacheMode Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "CacheMode element"
ms.assetid: bfb8f7bb-ccd3-4dfe-a36a-1cea15edfe40
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# CacheMode Element (ASSL)
  Determines the caching mechanism used for training data retrieved while processing a mining structure.  
  
## Syntax  
  
```xml  
  
<MiningStructure>  
   ...  
   <CacheMode>...</CacheMode>  
   ...  
</MiningStructure>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*KeepTrainingCases*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*KeepTrainingCases*|Training cases are cached during and after processing.|  
|*ClearAfterProcessing*|Training cases are cached during processing, but deleted after processing.|  
  
## Remarks  
 The element that corresponds to the parent of **CacheMode** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MiningStructure>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  