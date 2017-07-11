---
title: "ReportFormatParameters Element (ASSL) | Microsoft Docs"
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
  - "ReportFormatParameters Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "ReportFormatParameters"
helpviewer_keywords: 
  - "ReportFormatParameters element"
ms.assetid: f2e677bf-7b6b-4ce4-b0ec-75a4999306c9
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# ReportFormatParameters Element (ASSL)
  Contains the collection of [ReportFormatParameter](../../../analysis-services/scripting/objects/reportformatparameter-element-asl.md) elements for a [ReportAction](../../../analysis-services/scripting/data-type/reportaction-data-type-assl.md) element.  
  
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
|Parent elements|[Action](../../../analysis-services/scripting/objects/action-element-assl.md) of type [ReportAction](../../../analysis-services/scripting/data-type/reportaction-data-type-assl.md)|  
|Child elements|[ReportFormatParameter](../../../analysis-services/scripting/objects/reportformatparameter-element-asl.md)|  
  
## Remarks  
 The element that corresponds to the parent of **ReportFormatParameters** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ReportAction>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  