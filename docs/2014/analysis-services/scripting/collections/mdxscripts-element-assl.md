---
title: "MdxScripts Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MdxScripts Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MdxScripts"
helpviewer_keywords: 
  - "MdxScripts element"
ms.assetid: 0aaa849f-e723-4245-8c16-7ed049590fd2
author: minewiskan
ms.author: owend
manager: craigg
---
# MdxScripts Element (ASSL)
  Contains the collection of [MdxScript](../objects/mdxscript-element-assl.md) elements associated with a [Cube](../objects/cube-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Cube>  
   ...  
   <MdxScripts>  
      <MdxScript>...</MdxScript>  
   </MdxScripts>  
   ...  
</Cube>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Cube](../objects/cube-element-assl.md)|  
|Child elements|[MdxScript](../objects/mdxscript-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MdxScriptCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
