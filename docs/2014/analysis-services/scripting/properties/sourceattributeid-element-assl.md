---
title: "SourceAttributeID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "SourceAttributeID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "SourceAttributeID"
helpviewer_keywords: 
  - "SourceAttributeID element"
ms.assetid: 8973eb62-6142-4ce2-ad42-c8be2b43c04f
author: minewiskan
ms.author: owend
manager: craigg
---
# SourceAttributeID Element (ASSL)
  Contains the identifier (ID) of the source attribute on which the [Level](../objects/level-element-assl.md) element is based.  
  
## Syntax  
  
```xml  
  
<Level>  
   ...  
   <SourceAttributeID>...</SourceAttributeID>  
   ...  
</Level>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Level](../objects/level-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element corresponding to the parent of `SourceAttributeID` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Level>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
