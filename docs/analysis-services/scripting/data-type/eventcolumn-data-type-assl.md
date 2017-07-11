---
title: "EventColumn Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "EventColumn Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "EventColumn"
helpviewer_keywords: 
  - "EventColumn data type"
ms.assetid: c0009f1d-d136-4155-9a1b-7baacda4b552
caps.latest.revision: 41
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# EventColumn Data Type (ASSL)
  Defines a primitive data type that represents a column of information to be captured for an [Event](../../../analysis-services/scripting/objects/event-element-assl.md) element as part of a [Trace](../../../analysis-services/scripting/objects/trace-element-assl.md) element.  
  
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
|Child elements|[ColumnID](../../../analysis-services/scripting/properties/columnid-element-eventcolumn-assl.md)|  
|Derived elements|[Column](../../../analysis-services/scripting/objects/column-element-assl.md) ([Columns](../../../analysis-services/scripting/collections/columns-element-assl.md) collection of [Trace](../../../analysis-services/scripting/objects/trace-element-assl.md))|  
  
## See Also  
 [Events Element &#40;ASSL&#41;](../../../analysis-services/scripting/collections/events-element-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  