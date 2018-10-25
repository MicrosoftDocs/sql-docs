---
title: "AggregationDesignAttribute Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AggregationDesignAttribute Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AggregationDesignAttribute"
helpviewer_keywords: 
  - "AggregationDesignAttribute data type"
ms.assetid: 03d29d76-e4bd-4035-92cc-35149d83fbf9
author: minewiskan
ms.author: owend
manager: craigg
---
# AggregationDesignAttribute Data Type (ASSL)
  Defines a primitive data type that represents the association between an attribute and an [AggregationDesignDimension](dimension-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<AggregationDesignAttribute>  
   <AttributeID>...</AttributeID>  
      <EstimatedCount>...</EstimatedCount>  
</AggregationDesignAttribute>  
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
|Child elements|[AttributeID](../properties/id-element-assl.md), [EstimatedCount](../properties/estimatedcount-element-assl.md)|  
|Derived elements|[Attribute](../objects/attribute-element-assl.md) ([Attributes](../collections/attributes-element-assl.md) collection of [AggregationDesignDimension](dimension-data-type-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationDesignAttribute>.  
  
## See Also  
 [AggregationDesignDimension Data Type &#40;ASSL&#41;](dimension-data-type-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
