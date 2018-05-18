---
title: "LogFileSize Element (ASSL) | Microsoft Docs"
ms.date: 5/8/2018
ms.prod: sql
ms.custom: assl
ms.reviewer: owend
ms.technology: analysis-services
ms.topic: reference
author: minewiskan
ms.author: owend
manager: kfile
---
# LogFileSize Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Specifies the maximum log file size, in megabytes.  
  
## Syntax  
  
```xml  
  
<Trace>  
  
   <LogFileSize>...</LogFileSize>  
  
</Trace>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer|  
|Default value|**5** (megabytes)|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Trace](../../../analysis-services/scripting/objects/trace-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of **LogFileSize** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Trace>.  
  
## See Also  
 [Traces Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/traces-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
