---
title: "DisplayFlag Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "DisplayFlag Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DisplayFlag"
helpviewer_keywords: 
  - "DisplayFlag element"
ms.assetid: a6750477-0763-46da-9add-1f4448146a6b
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# DisplayFlag Element (ASSL)
  Contains a read-only hint that indicates whether user interface components should display the associated [ServerProperty](../../../2014/analysis-services/dev-guide/serverproperty-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<ServerProperty>  
   ...  
   <DisplayFlag>...</DisplayFlag>  
   ...  
</ServerProperty>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[ServerProperty](../../../2014/analysis-services/dev-guide/serverproperty-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element corresponding to the parent of `DisplayFlag` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ServerProperty>.  
  
## See Also  
 [ServerProperties Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/serverproperties-element-assl.md)   
 [Server Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/server-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  