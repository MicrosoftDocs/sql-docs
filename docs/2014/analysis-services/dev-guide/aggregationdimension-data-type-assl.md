---
title: "AggregationDimension Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "AggregationDimension Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AggregationDimension"
helpviewer_keywords: 
  - "AggregationDimension data type"
ms.assetid: 697e0e09-3210-4a56-882f-80726abc4c68
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# AggregationDimension Data Type (ASSL)
  Defines a primitive data type that represents the relationship between a dimension and an [Aggregation](../../../2014/analysis-services/dev-guide/aggregation-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<AggregationDimension>  
   <CubeDimensionID>...</CubeDimensionID>  
   <Attributes>...</Attributes>  
      <Annotations>...</Annotations>  
</AggregationDimension>  
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
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Attributes](../../../2014/analysis-services/dev-guide/attributes-element-assl.md), [CubeDimensionID](../../../2014/analysis-services/dev-guide/cubedimensionid-element-assl.md)|  
|Derived elements|[Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md) ([Dimensions](../../../2014/analysis-services/dev-guide/dimensions-element-assl.md) collection of [Aggregation](../../../2014/analysis-services/dev-guide/aggregation-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is [AggregationDimension](../../../2014/analysis-services/dev-guide/aggregationdimension-data-type-assl.md).  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  