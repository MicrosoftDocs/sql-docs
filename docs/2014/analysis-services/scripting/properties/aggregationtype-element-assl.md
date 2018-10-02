---
title: "AggregationType Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AggregationType Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "AggregationType element"
ms.assetid: c1393bc6-d715-4397-8bc5-82abdb275330
author: minewiskan
ms.author: owend
manager: craigg
---
# AggregationType Element (ASSL)
  Defines the type of aggregation stored by the [Partition](../objects/partition-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<AggregationInstance>  
   ...  
   <AggregationType>...</AggregationType>  
   ...  
</AggregationInstance>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|1-1: Required element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[AggregationInstance](../objects/aggregationinstance-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*IndexedView*|The aggregation is stored in an indexed view.|  
|*Table*|The aggregation is stored in a table.|  
|*UserDefined*|The aggregation is user-defined.|  
  
 The enumeration that corresponds to the allowed values for `AggregationType` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationInstance>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
