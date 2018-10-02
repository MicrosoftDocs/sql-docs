---
title: "AggregationInstanceSource Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AggregationInstanceSource Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "AggregationInstanceSource element"
ms.assetid: ab58c817-eb2b-4974-8470-2946ca5affea
author: minewiskan
ms.author: owend
manager: craigg
---
# AggregationInstanceSource Element (ASSL)
  Identifies the source of data for user-defined aggregation instances bound to a [Partition](../objects/partition-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Partition>  
   ...  
   <AggregationInstanceSource xsi:type="DataSourceViewBinding">...</AggregationInstanceSource>  
   ...  
</Partition>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[DataSourceViewBinding](../data-type/binding-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Partition](../objects/partition-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 If this element is missing or set to a blank string, the data source view of the cube that owns the partition is used by default.  
  
 For additional information about the `Binding` type, including tables of Analysis Services Scripting Language (ASSL) objects of the `Binding` type and the inheritance hierarchy of `Binding` types, see [Binding Data Type &#40;ASSL&#41;](../data-type/binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
