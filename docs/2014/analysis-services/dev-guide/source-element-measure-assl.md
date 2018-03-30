---
title: "Source Element (Measure) (ASSL) | Microsoft Docs"
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
  - "Source Element (Measure)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Source"
helpviewer_keywords: 
  - "Source element"
ms.assetid: 9bae7ba4-3065-4623-b3e0-d54cebea7503
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Source Element (Measure) (ASSL)
  Contains the details of the source containing the value of the [Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Measure>  
      ...  
   <Source xsi:type="DataItem">...</Source>  
      ...  
</Measure>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[DataItem](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `Source` of the `DataItem`, which serves as the `Source` of the `Measure`, can in turn be of type [RowBinding](../../../2014/analysis-services/dev-guide/rowbinding-data-type-assl.md), [ColumnBinding](../../../2014/analysis-services/dev-guide/columnbinding-data-type-assl.md), [MeasureBinding](../../../2014/analysis-services/dev-guide/measurebinding-data-type-assl.md), or [CubeDimensionBinding](../../../2014/analysis-services/dev-guide/cubedimensionbinding-data-type-assl.md).  
  
 For additional information about the `DataItem` type, including a table of ASSL objects and properties of the `DataItem` type, see [DataItem Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md).  
  
 The element corresponding to the parent of `Source` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Measure>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  