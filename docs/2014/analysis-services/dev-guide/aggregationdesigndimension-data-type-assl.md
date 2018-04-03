---
title: "AggregationDesignDimension Data Type (ASSL) | Microsoft Docs"
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
  - "AggregationDesignDimension Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AggregationDesignDimension"
helpviewer_keywords: 
  - "AggregationDesignDimension data type"
ms.assetid: 06a0d418-014c-4f40-a63a-5cfeee3f6a41
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# AggregationDesignDimension Data Type (ASSL)
  Defines a primitive data type that represents the relationship between a cube dimension and an [AggregationDesign](../../../2014/analysis-services/dev-guide/aggregationdesign-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<AggregationDesignDimension>  
   <CubeDimensionID>...</CubeDimensionID>  
   <Attributes>...</Attributes>  
      <Annotations>...</Annotations>  
</AggregationDesignDimension>  
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
|Derived elements|[Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md) ([Dimensions](../../../2014/analysis-services/dev-guide/dimensions-element-assl.md) collection of [AggregationDesign](../../../2014/analysis-services/dev-guide/aggregationdesign-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationDesignDimension>.  
  
## See Also  
 [AggregationDesign Element &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/aggregationdesign-element-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  