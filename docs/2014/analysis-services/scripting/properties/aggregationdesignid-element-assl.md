---
title: "AggregationDesignID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AggregationDesignID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AggregationDesignID"
helpviewer_keywords: 
  - "AggregationDesignID element"
ms.assetid: e7f1f7ae-3169-4c0c-aadb-f7465155d652
author: minewiskan
ms.author: owend
manager: craigg
---
# AggregationDesignID Element (ASSL)
  Identifies the [AggregationDesign](../objects/aggregationdesign-element-assl.md) element associated with the [Partition](../objects/partition-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Partition>  
      ...  
   <AggregationDesignID>...</AggregationDesignID>  
   ...  
</Partition>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Partition](../objects/partition-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `AggregationDesignID` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Partition>. See also <xref:Microsoft.AnalysisServices.AggregationDesign>.  
  
## See Also  
 [AggregationDesign Element &#40;ASSL&#41;](../objects/aggregationdesign-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
