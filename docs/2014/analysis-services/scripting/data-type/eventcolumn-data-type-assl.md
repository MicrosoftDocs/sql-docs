---
title: "EventColumn Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "EventColumn Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "EventColumn"
helpviewer_keywords: 
  - "EventColumn data type"
ms.assetid: c0009f1d-d136-4155-9a1b-7baacda4b552
author: minewiskan
ms.author: owend
manager: craigg
---
# EventColumn Data Type (ASSL)
  Defines a primitive data type that represents a column of information to be captured for an [Event](../objects/event-element-assl.md) element as part of a [Trace](../objects/trace-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<EventColumn>  
   <ColumnID>...</ColumnID>  
</EventColumn>  
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
|Child elements|[ColumnID](../properties/columnid-element-eventcolumn-assl.md)|  
|Derived elements|[Column](../objects/column-element-assl.md) ([Columns](../collections/columns-element-assl.md) collection of [Trace](../objects/trace-element-assl.md))|  
  
## See Also  
 [Events Element &#40;ASSL&#41;](../collections/events-element-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
