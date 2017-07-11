---
title: "KpiID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "KpiID Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "KpiID"
helpviewer_keywords: 
  - "KpiID element"
ms.assetid: a76395bc-bc84-40f8-9770-6275842f93b5
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# KpiID Element (ASSL)
  Contains an identifier (ID) that associates a [Kpi](../../../analysis-services/scripting/objects/kpi-element-assl.md) element with a [Perspective](../../../analysis-services/scripting/objects/perspective-element-assl.md) element.  
  
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
|Parent element|[PerspectiveKpi](../../../analysis-services/scripting/data-type/perspectivekpi-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of **KpiID** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveKpi>.  
  
## See Also  
 [Properties (ASSL)](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  