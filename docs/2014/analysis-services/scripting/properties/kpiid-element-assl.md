---
title: "KpiID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "KpiID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "KpiID"
helpviewer_keywords: 
  - "KpiID element"
ms.assetid: a76395bc-bc84-40f8-9770-6275842f93b5
author: minewiskan
ms.author: owend
manager: craigg
---
# KpiID Element (ASSL)
  Contains an identifier (ID) that associates a [Kpi](../objects/kpi-element-assl.md) element with a [Perspective](../objects/perspective-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<PerspectiveKpi>  
      ...  
   <KpiID>...</KpiID>  
   ...  
</PerspectiveKpi>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[PerspectiveKpi](../data-type/perspectivekpi-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `KpiID` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveKpi>.  
  
## See Also  
 [Properties (ASSL)](properties-assl.md)  
  
  
