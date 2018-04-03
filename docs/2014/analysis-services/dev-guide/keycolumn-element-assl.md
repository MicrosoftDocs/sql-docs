---
title: "KeyColumn Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "KeyColumn Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "KeyColumn"
helpviewer_keywords: 
  - "KeyColumn element"
ms.assetid: 7b03eeb3-d478-4c38-822e-8cdfcc485039
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# KeyColumn Element (ASSL)
  Contains the definition of a column that is, or is part of, the key for an attribute.  
  
## Syntax  
  
```xml  
  
<KeyColumns>  
   <KeyColumn xsi:type="DataItem">...</KeyColumn>  
<KeyColumns>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[DataItem](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md)|  
|Default value|None|  
|Cardinality|1-n: Required element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[KeyColumns](../../../2014/analysis-services/dev-guide/keycolumns-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 For more information about the `DataItem` type, including a table of Analysis Services Scripting Language (ASSL) objects and properties of the `DataItem` type, see [DataItem Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md).  
  
 The elements that correspond to the parents of the `KeyColumns` collection in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AggregationInstanceAttribute>, <xref:Microsoft.AnalysisServices.DimensionAttribute>, <xref:Microsoft.AnalysisServices.MeasureGroupAttribute>, and <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn>.  
  
## See Also  
 [AggregationInstanceAttribute Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/aggregationinstanceattribute-data-type-assl.md)   
 [AggregationInstanceCubeDimension Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/aggregationinstancecubedimension-data-type-assl.md)   
 [DimensionAttribute Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)   
 [MeasureGroupAttribute Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/measuregroupattribute-data-type-assl.md)   
 [ScalarMiningStructureColumn Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/scalarminingstructurecolumn-data-type-assl.md)   
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  