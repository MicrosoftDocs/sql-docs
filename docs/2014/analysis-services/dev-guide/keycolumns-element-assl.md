---
title: "KeyColumns Element (ASSL) | Microsoft Docs"
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
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# KeyColumns Element (ASSL)
  Contains the collection of [KeyColumn](../../../2014/analysis-services/dev-guide/keycolumn-element-assl.md) element definitions for a parent object.  
  
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
|Parent elements|[AggregationInstanceAttribute](../../../2014/analysis-services/dev-guide/aggregationinstanceattribute-data-type-assl.md), [AggregationInstanceCubeDimension](../../../2014/analysis-services/dev-guide/aggregationinstancecubedimension-data-type-assl.md), [DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md), [MeasureGroupAttribute](../../../2014/analysis-services/dev-guide/measuregroupattribute-data-type-assl.md), [ScalarMiningStructureColumn](../../../2014/analysis-services/dev-guide/scalarminingstructurecolumn-data-type-assl.md)|  
|Child elements|[KeyColumn](../../../2014/analysis-services/dev-guide/keycolumn-element-assl.md) of type [DataItem](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md)|  
  
## Remarks  
 The `KeyColumns` collection can contain multiple `KeyColumn` elements that represent a multipart key for an attribute or mining structure column.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  