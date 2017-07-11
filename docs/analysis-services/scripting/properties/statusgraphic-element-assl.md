---
title: "StatusGraphic Element (ASSL) | Microsoft Docs"
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
  - "StatusGraphic Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "StatusGraphic"
helpviewer_keywords: 
  - "StatusGraphic element"
ms.assetid: 14b365bc-924d-4791-ad4a-a38155fec42e
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# StatusGraphic Element (ASSL)
  Contains the recommended graphical representation of the status of the [Kpi](../../../analysis-services/scripting/objects/kpi-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Kpi>  
   ...  
   <StatusGraphic>...</StatusGraphic>  
   ...  
</Kpi>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Kpi](../../../analysis-services/scripting/objects/kpi-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Traffic Light - Single*|Traffic light (single)|  
|*Traffic Light - Multiple*|Traffic light (multiple)|  
|*Road Signs*|Road signs|  
|*Gauge - Ascending*|Gauge|  
|*Gauge - Descending*|Reversed Gauge|  
|*Thermometer*|Thermometer|  
|*Cylinder*|Cylinder|  
|*Smiley Face*|Face|  
  
 The element that corresponds to the parent of **StatusGraphic** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Kpi>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  