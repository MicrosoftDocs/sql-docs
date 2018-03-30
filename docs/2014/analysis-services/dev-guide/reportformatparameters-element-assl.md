---
title: "ReportFormatParameters Element (ASSL) | Microsoft Docs"
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
  - "ReportFormatParameters Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ReportFormatParameters"
helpviewer_keywords: 
  - "ReportFormatParameters element"
ms.assetid: f2e677bf-7b6b-4ce4-b0ec-75a4999306c9
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ReportFormatParameters Element (ASSL)
  Contains the collection of [ReportFormatParameter](../../../2014/analysis-services/dev-guide/reportformatparameter-element-assl.md) elements for a [ReportAction](../../../2014/analysis-services/dev-guide/reportaction-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Action xsi:type="ReportAction">  
   ...  
   <ReportFormatParameters>  
      <ReportFormatParameter>...</ReportFormatParameter>  
   </ReportFormatParameters>  
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
|Child elements|[ReportFormatParameter](../../../2014/analysis-services/dev-guide/reportformatparameter-element-assl.md)|  
  
## Remarks  
 The element that corresponds to the parent of `ReportFormatParameters` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ReportAction>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  