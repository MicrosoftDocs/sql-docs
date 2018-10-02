---
title: "Kpis Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Kpis Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Kpis"
helpviewer_keywords: 
  - "Kpis element"
ms.assetid: da4e32a0-1416-4d32-8b7f-7d74be23c9d4
author: minewiskan
ms.author: owend
manager: craigg
---
# Kpis Element (ASSL)
  Contains the collection of key performance indicators ([Kpi](../objects/kpi-element-assl.md) elements) associated with the parent element.  
  
## Syntax  
  
```xml  
  
<Cube><!-- or Perspective -->  
   ...  
   <Kpis>  
      <Kpi>...</Kpi> <!-- parent: Cube -->  
<!-- or -->  
      <Kpi xsi:type="PerspectiveKpi">...</Kpi> <!-- parent: Perspective -->  
   ...  
</Cube>  
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
|Parent elements|[Cube](../objects/cube-element-assl.md), [Perspective](../objects/perspective-element-assl.md)|  
  
|Ancestor or Parent|Child Element|  
|------------------------|-------------------|  
|[Cube](../objects/cube-element-assl.md)|[Kpi](../objects/kpi-element-assl.md)|  
|[Perspective](../objects/perspective-element-assl.md)|[Kpi](../objects/kpi-element-assl.md) of type [PerspectiveKpi](../data-type/perspectivekpi-data-type-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.KpiCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
