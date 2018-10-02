---
title: "ReportParameters Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# ReportParameters Element (ASSL)
  Contains the collection of [ReportParameter](../objects/reportparameter-element-assl.md) elements for a [ReportAction](../data-type/action-data-type-assl.md) element.  
  
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
|Parent elements|[Action](../objects/action-element-assl.md) of type [ReportAction](../data-type/action-data-type-assl.md)|  
|Child elements|[ReportParameter](../objects/reportparameter-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ReportParameterCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
