---
title: "ManufacturingFirstMonth Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ManufacturingFirstMonth Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ManufacturingFirstMonth"
helpviewer_keywords: 
  - "ManufacturingFirstMonth element"
ms.assetid: 3b2fb440-662b-4d88-a133-1e098b9c8169
author: minewiskan
ms.author: owend
manager: craigg
---
# ManufacturingFirstMonth Element (ASSL)
  Defines the first manufacturing month for a [TimeBinding](../data-type/binding-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<TimeBinding>  
   ...  
   <ManufacturingFirstMonth>...</ManufacturingFirstMonth>  
   ...  
</TimeBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Integer (1 through 12, where 1 = January and 12 = December)|  
|Default value|`1`|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[TimeBinding](../data-type/binding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `ManufacturingFirstMonth` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TimeBinding>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
