---
title: "RefreshPolicy Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "RefreshPolicy Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "RefreshPolicy"
helpviewer_keywords: 
  - "RefreshPolicy element"
ms.assetid: f4c36280-1a39-4f1c-a3ab-fbeb81742d6d
author: minewiskan
ms.author: owend
manager: craigg
---
# RefreshPolicy Element (ASSL)
  Determines how often the dynamic part of the dimension or measure group (as specified by the [Persistence](persistence-element-assl.md) element) is checked for changes.  
  
## Syntax  
  
```xml  
  
<DimensionBinding> <!-- or MeasureGroupBinding -->  
   ...  
   <RefreshPolicy>...</RefreshPolicy>  
   ...  
</DimensionBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
|Ancestor or Parent|Default Value|  
|------------------------|-------------------|  
|[DimensionBinding](../data-type/binding-data-type-assl.md)|*ByQuery*|  
|[MeasureGroupBinding](../data-type/measuregroupbinding-data-type-assl.md)|None|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DimensionBinding](../data-type/binding-data-type-assl.md), [MeasureGroupBinding](../data-type/measuregroupbinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*ByQuery*|Every query checks to see whether the source data has changed.|  
|*ByInterval*|Source data is only checked for changes at the interval specified by [RefreshInterval](refreshinterval-element-assl.md).|  
  
 The enumeration that corresponds to the allowed values for `RefreshPolicy` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.RefreshPolicy>.  
  
## See Also  
 [Persistence Element &#40;ASSL&#41;](persistence-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
