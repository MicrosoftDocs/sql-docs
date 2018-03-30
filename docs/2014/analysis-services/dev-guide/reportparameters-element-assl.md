---
title: "ReportParameters Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "ReportParameters Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ReportParameters"
helpviewer_keywords: 
  - "ReportParameters element"
ms.assetid: 0e138e8f-8313-47f2-96e1-cd189eec26bb
caps.latest.revision: 28
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ReportParameters Element (ASSL)
  Contains the collection of [ReportParameter](../../../2014/analysis-services/dev-guide/reportparameter-element-assl.md) elements for a [ReportAction](../../../2014/analysis-services/dev-guide/reportaction-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Action xsi:type="ReportAction">  
   ...  
   <ReportParameters>  
      <ReportParameter>...</ReportParameter>  
   </ReportParameters>  
   ...  
</Action>  
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
|Parent elements|[Action](../../../2014/analysis-services/dev-guide/action-element-assl.md) of type [ReportAction](../../../2014/analysis-services/dev-guide/reportaction-data-type-assl.md)|  
|Child elements|[ReportParameter](../../../2014/analysis-services/dev-guide/reportparameter-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ReportParameterCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  