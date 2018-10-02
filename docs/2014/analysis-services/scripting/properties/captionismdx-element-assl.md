---
title: "CaptionIsMdx Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CaptionIsMdx Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CaptionIsMdx"
helpviewer_keywords: 
  - "CaptionIsMdx element"
ms.assetid: 7569a75e-b3e0-4332-97d3-585abc546ada
author: minewiskan
ms.author: owend
manager: craigg
---
# CaptionIsMdx Element (ASSL)
  Defines whether the caption for the [Action](../objects/action-element-assl.md) element is a Multidimensional Expressions (MDX) expression.  
  
## Syntax  
  
```xml  
  
<Action>  
   ...  
   <CaptionIsMdx>...</CaptionIsMdx>  
   ...  
</Action>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|`False`|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Action](../objects/action-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `CaptionIsMdx` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Action>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
