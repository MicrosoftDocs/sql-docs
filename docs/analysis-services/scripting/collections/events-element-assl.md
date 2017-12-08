---
title: "Events Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Events Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Events"
helpviewer_keywords: 
  - "Events element"
ms.assetid: de887998-dc4b-44dc-8fec-08d67b92f96d
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "kfile"
ms.workload: "Inactive"
---
# Events Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Defines the collection of [Event](../../../analysis-services/scripting/objects/event-element-assl.md) elements to be captured by a [Trace](../../../analysis-services/scripting/objects/trace-element-assl.md).  
  
## Syntax  
  
```xml  
  
<Trace>  
   ...  
   <Events>  
      <Event>...</Event>  
   </Events>  
   ...  
</Trace>  
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
|Parent element|[Trace](../../../analysis-services/scripting/objects/trace-element-assl.md)|  
|Child elements|[Event](../../../analysis-services/scripting/objects/event-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TraceEventCollection>.  
  
## See Also  
 [Traces Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/traces-element-assl.md)   
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  
