---
title: "ReportParameter Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "ReportParameter Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "ReportParameter"
helpviewer_keywords: 
  - "ReportParameter element"
ms.assetid: 653a5c64-f1af-4796-bb7b-b44a40e52901
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# ReportParameter Element - ASSL
  Contains the name and value of a parameter that is passed to a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] report at run time.  
  
## Syntax  
  
```xml  
  
<ReportParameters>  
      <ReportParameter>  
      <Name>...</Name>  
      <Value>...</Value>  
   </ReportParameter>  
</ReportParameters>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[ReportParameters](../../../analysis-services/scripting/collections/reportparameters-element-assl.md)|  
|Child elements|[Name](../../../analysis-services/scripting/properties/name-element-assl.md), [Value](../../../analysis-services/scripting/properties/value-element-assl.md)|  
  
## Remarks  
 The **Value** element must contain a Multidimensional Expressions (MDX) expression.  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ReportParameter>.  
  
## See Also  
 [ReportAction Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/reportaction-data-type-assl.md)   
 [Action Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/action-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  