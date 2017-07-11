---
title: "AggregationAttribute Data Type (ASSL) | Microsoft Docs"
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
  - "AggregationAttribute Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "AggregationAttribute"
helpviewer_keywords: 
  - "AggregationAttribute data type"
ms.assetid: 636827c7-938d-4b7d-9827-46da3bc60d9a
caps.latest.revision: 42
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# AggregationAttribute Data Type (ASSL)
  Defines a primitive data type that represents the association between an [Aggregation](../../../analysis-services/scripting/objects/aggregation-element-assl.md) element and an attribute.  
  
## Syntax  
  
```xml  
  
<AggregationAttribute>  
   <AttributeID>...</AttributeID>  
      <Annotations>...</Annotations>  
</AggregationAttribute>  
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
|Child elements|[AttributeID](../../../analysis-services/scripting/properties/attributeid-element-assl.md), [Annotations](../../../analysis-services/scripting/collections/annotations-element-assl.md)|  
|Derived elements|[Attribute](../../../analysis-services/scripting/objects/attribute-element-assl.md) ([Attributes](../../../analysis-services/scripting/collections/attributes-element-assl.md) collection of [AggregationDimension](../../../analysis-services/scripting/data-type/aggregationdimension-data-type-assl.md))|  
  
## Remarks  
 The corresponding class in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationAttribute>.  
  
## See Also  
 [Aggregation Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/aggregation-element-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  