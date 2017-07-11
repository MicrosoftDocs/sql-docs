---
title: "Filter Element (Trace) (ASSL) | Microsoft Docs"
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
  - "Filter Element (Trace)"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "filter"
helpviewer_keywords: 
  - "Filter element"
ms.assetid: 411a598e-3bb1-487b-9f37-cce4b57a67b4
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Filter Element (Trace) (ASSL)
  Contains an XML document fragment that describes the [Trace](../../../analysis-services/scripting/objects/trace-element-assl.md) filter.  
  
## Syntax  
  
```xml  
  
<Trace>  
   ...  
   <Filter>...</Filter>  
   ...  
</Trace>  
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
|Parent element|[Trace](../../../analysis-services/scripting/objects/trace-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of **Filter** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Trace>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)   
 [Traces Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/traces-element-assl.md)  
  
  