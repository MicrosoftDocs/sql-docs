---
title: "MdxScripts Element (ASSL) | Microsoft Docs"
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
  - "MdxScripts Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "MdxScripts"
helpviewer_keywords: 
  - "MdxScripts element"
ms.assetid: 0aaa849f-e723-4245-8c16-7ed049590fd2
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# MdxScripts Element (ASSL)
  Contains the collection of [MdxScript](../../../analysis-services/scripting/objects/mdxscript-element-assl.md) elements associated with a [Cube](../../../analysis-services/scripting/objects/cube-element-assl.md) element.  
  
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
|Parent elements|[Cube](../../../analysis-services/scripting/objects/cube-element-assl.md)|  
|Child elements|[MdxScript](../../../analysis-services/scripting/objects/mdxscript-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MdxScriptCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  