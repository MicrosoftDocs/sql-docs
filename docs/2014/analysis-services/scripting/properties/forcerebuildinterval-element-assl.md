---
title: "ForceRebuildInterval Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ForceRebuildInterval Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ForceRebuildInterval"
helpviewer_keywords: 
  - "ForceRebuildInterval element"
ms.assetid: d068f92e-4213-471c-a3a4-aec5af4b8ebf
author: minewiskan
ms.author: owend
manager: craigg
---
# ForceRebuildInterval Element (ASSL)
  Determines the amount of time, starting when a fresh multidimensional OLAP (MOLAP) image becomes available, after which MOLAP imaging unconditionally starts.  
  
## Syntax  
  
```xml  
  
<ProactiveCaching>  
   ...  
   <ForceRebuildInterval>...</ForceRebuildInterval>  
   ...  
</ProactiveCaching>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|XML duration|  
|Default value|Infinite|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ProactiveCaching](../objects/proactivecaching-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `ForceRebuildInterval` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ProactiveCaching>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
