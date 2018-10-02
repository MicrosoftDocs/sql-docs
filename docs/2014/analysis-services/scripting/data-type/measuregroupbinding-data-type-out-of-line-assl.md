---
title: "MeasureGroupBinding Data Type (out-of-line) (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MeasureGroupBinding Data Type (out-of-line)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MeasureGroupBinding"
helpviewer_keywords: 
  - "MeasureGroupBinding data type"
ms.assetid: e6c65597-89ac-457e-a0e5-01d85449a1b7
author: minewiskan
ms.author: owend
manager: craigg
---
# MeasureGroupBinding Data Type (out-of-line) (ASSL)
  Defines a primitive data type that represents a binding to a measure group.  
  
## Syntax  
  
```xml  
  
<MeasureGroupBinding>  
   <ID>...</ID>  
   <Source>...</Source>  
   <EstimatedRows>...</EstimatedRows>  
   <Dimensions>...</Dimensions>  
   <Measures>...</Measures>  
      <Partitions>...</Partitions>  
</MeasureGroupBinding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Dimensions](../collections/dimensions-element-assl.md), [EstimatedRows](../properties/estimatedrows-element-assl.md), [ID](../properties/id-element-assl.md), [Measures](../collections/measures-element-assl.md), [Partitions](../collections/partitions-element-assl.md), [Source](../properties/source-element-binding-assl.md)|  
|Derived elements|See [Binding](binding-data-type-assl.md)|  
  
## Remarks  
 For more information, see the section, Out of Line Bindings, in [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
