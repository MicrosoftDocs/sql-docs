---
title: "AggregationDesignAttribute Data Type (ASSL) | Microsoft Docs"
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
caps.latest.revision: 41
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# AggregationDesignAttribute Data Type (ASSL)
  Defines a primitive data type that represents the association between an attribute and an [AggregationDesignDimension](../../../2014/analysis-services/dev-guide/aggregationdesigndimension-data-type-assl.md) element.  
  
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
|Child elements|[AttributeID](../../../2014/analysis-services/dev-guide/attributeid-element-assl.md), [EstimatedCount](../../../2014/analysis-services/dev-guide/estimatedcount-element-assl.md)|  
|Derived elements|[Attribute](../../../2014/analysis-services/dev-guide/attribute-element-assl.md) ([Attributes](../../../2014/analysis-services/dev-guide/attributes-element-assl.md) collection of [AggregationDesignDimension](../../../2014/analysis-services/dev-guide/aggregationdesigndimension-data-type-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationDesignAttribute>.  
  
## See Also  
 [AggregationDesignDimension Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/aggregationdesigndimension-data-type-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  