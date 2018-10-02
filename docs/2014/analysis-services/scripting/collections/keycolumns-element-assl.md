---
title: "KeyColumns Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "KeyColumns Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "KeyColumns"
helpviewer_keywords: 
  - "KeyColumns element"
ms.assetid: 03f3ad21-25cb-4afd-9287-cbf942ac1ad9
author: minewiskan
ms.author: owend
manager: craigg
---
# KeyColumns Element (ASSL)
  Contains the collection of [KeyColumn](../objects/column-element-assl.md) element definitions for a parent object.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute> <!-- or AggregationInstanceAttribute, AggregationInstanceCubeDimension, MeasureGroupAttribute, ScalarMiningStructureColumn -->  
   ...  
   <KeyColumns>  
      <KeyColumn xsi:type="DataItem"...</KeyColumn>  
   </KeyColumns>  
   ...  
</Dimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|1-n: Required element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AggregationInstanceAttribute](../data-type/aggregationinstanceattribute-data-type-assl.md), [AggregationInstanceCubeDimension](../data-type/dimension-data-type-assl.md), [DimensionAttribute](../data-type/dimensionattribute-data-type-assl.md), [MeasureGroupAttribute](../data-type/measuregroupattribute-data-type-assl.md), [ScalarMiningStructureColumn](../data-type/miningstructurecolumn-data-type-assl.md)|  
|Child elements|[KeyColumn](../objects/column-element-assl.md) of type [DataItem](../data-type/dataitem-data-type-assl.md)|  
  
## Remarks  
 The `KeyColumns` collection can contain multiple `KeyColumn` elements that represent a multipart key for an attribute or mining structure column.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
