---
title: "Text Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Text Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "text"
helpviewer_keywords: 
  - "Text element"
ms.assetid: 0edece73-236f-4661-8102-3fcc29326bf4
author: minewiskan
ms.author: owend
manager: craigg
---
# Text Element (ASSL)
  Contains the text of a [Command](../objects/command-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Command>  
   <Text>...</Text>  
</Command>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Command](../objects/command-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `Text` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Command>.  
  
## See Also  
 [Commands Element &#40;ASSL&#41;](../collections/commands-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
