---
title: "DefaultScript Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DefaultScript Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DefaultScript"
helpviewer_keywords: 
  - "DefaultScript element"
ms.assetid: 60716e63-2d64-4774-9ac9-253efe612fa5
author: minewiskan
ms.author: owend
manager: craigg
---
# DefaultScript Element (ASSL)
  Identifies the default [MdxScript](../objects/mdxscript-element-assl.md) element in the [MdxScripts](../collections/mdxscripts-element-assl.md) collection.  
  
## Syntax  
  
```xml  
  
<MdxScript>  
   ...  
   <DefaultScript>...</DefaultScript>  
   ...  
</MdxScript>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|`True`|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[MdxScript](../objects/mdxscript-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 Setting the value of `DefaultScript` to `True` for one script sets the value of `DefaultScript` to `False` for all other `MdxScript` elements in the `MdxScripts` collection.  
  
 The element that corresponds to the parent of `DefaultScript` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MdxScript>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
