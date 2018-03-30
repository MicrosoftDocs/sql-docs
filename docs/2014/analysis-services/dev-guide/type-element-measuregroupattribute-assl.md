---
title: "Type Element (MeasureGroupAttribute) (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Type Element (MeasureGroupAttribute)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "TYPE"
helpviewer_keywords: 
  - "Type element"
ms.assetid: 93740504-297a-4a06-ab3e-b598e466eebb
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Type Element (MeasureGroupAttribute) (ASSL)
  Contains the type of a [MeasureGroupAttribute](../../../2014/analysis-services/dev-guide/measuregroupattribute-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MeasureGroupAttribute>  
   ...  
   <Type>...</Type>  
   ...  
</MeasureGroupAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Regular*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[MeasureGroupAttribute](../../../2014/analysis-services/dev-guide/measuregroupattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Regular*|Represents a regular attribute.|  
|*Granularity*|Represents a granularity attribute.|  
  
 The enumeration that corresponds to the allowed values for `Type` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MeasureGroupAttributeType>.  
  
 The element that corresponds to the parent of `Type` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MeasureGroupAttribute>.  
  
## See Also  
 [Attributes Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/attributes-element-assl.md)   
 [RegularMeasureGroupDimension Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/regularmeasuregroupdimension-data-type-assl.md)   
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  