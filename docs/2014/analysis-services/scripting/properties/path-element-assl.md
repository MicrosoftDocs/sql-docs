---
title: "Path Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Path Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Path"
helpviewer_keywords: 
  - "Path element"
ms.assetid: 0edc59ac-1671-4fe1-9b7c-6c1548df5c63
author: minewiskan
ms.author: owend
manager: craigg
---
# Path Element (ASSL)
  Contains the path, as provided by an instance of [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)], of a report used by the [ReportAction](../data-type/action-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<ReportAction>  
   ...  
   <Path>...</Path>  
   ...  
</ReportAction>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that occurs once and only once|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ReportAction](../data-type/action-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `Path` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ReportAction>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
