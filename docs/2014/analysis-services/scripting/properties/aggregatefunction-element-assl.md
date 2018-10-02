---
title: "AggregateFunction Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AggregateFunction Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AggregateFunction"
helpviewer_keywords: 
  - "AggregateFunction element"
ms.assetid: 880b6bd0-d62a-4221-831c-39f748ee84f2
author: minewiskan
ms.author: owend
manager: craigg
---
# AggregateFunction Element (ASSL)
  Defines the type of aggregate function used by a [Measure](../objects/measure-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Measure>  
   ...  
   <AggregateFunction>...</AggregateFunction>  
   ...  
</Measure>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Sum*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Measure](../objects/measure-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Sum*|The measure is aggregated using the `Sum` function.|  
|*Count*|The measure is aggregated using the `Count` function.|  
|*Min*|The measure is aggregated using the `Min` function.|  
|*Max*|The measure is aggregated using the `Max` function.|  
|*DistinctCount*|The measure is aggregated using the `DistinctCount` function.|  
|*None*|The measure is not aggregated.|  
|*ByAccount*|The measure is aggregated by account.|  
|*AverageOfChildren*|The measure is aggregated by returning the average of its children.|  
|*FirstChild*|The measure is aggregated by returning its first child member.|  
|*LastChild*|The measure is aggregated by returning its last child member.|  
|*FirstNonEmpty*|The measure is aggregated by returning its first nonempty member.|  
|*LastNonEmpty*|The measure is aggregated by returning its last nonempty member.|  
  
 The enumeration that corresponds to the allowed values for `AggregateFunction` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationFunction>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
