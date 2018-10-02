---
title: "AggregationInstanceAttribute Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AggregationInstanceAttribute Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "AggregationInstanceAttribute data type"
ms.assetid: fc5a98bf-7c3c-4faf-b9f8-16eb073d7dc7
author: minewiskan
ms.author: owend
manager: craigg
---
# AggregationInstanceAttribute Data Type (ASSL)
  Defines a primitive data type that represents information about an attribute used by an aggregation instance.  
  
## Syntax  
  
```xml  
  
<AggregationInstanceAttribute>  
   <AttributeID>...</AttributeID>  
   <KeyColumns>...</KeyColumns>  
</AggregationInstanceAttribute>  
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
|Child elements|[AttributeID](../properties/id-element-assl.md), [KeyColumns](../collections/columns-element-assl.md)|  
|Derived elements|[Attribute](../objects/attribute-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationInstanceAttribute>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
