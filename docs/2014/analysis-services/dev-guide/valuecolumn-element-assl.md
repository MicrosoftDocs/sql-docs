---
title: "ValueColumn Element (ASSL) | Microsoft Docs"
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
  - "ValueColumn Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "ValueColumn element"
ms.assetid: 6c2d6822-8ecc-46df-9fa9-bb92ac716c36
caps.latest.revision: 12
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# ValueColumn Element (ASSL)
  Identifies the column that provides the value of the parent element.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute>  
   ...  
   <ValueColumn xsi:type="DataItem">...</ValueColumn>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[DataItem](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md)|  
|Default value|Varies (see Remarks)|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 If the [NameColumn](../../../2014/analysis-services/dev-guide/namecolumn-element-assl.md) element of `DimensionAttribute` is specified, the same `DataItem` values are used as default values for the `ValueColumn` element. If the `NameColumn` element of `DimensionAttribute` is not specified and the [KeyColumns](../../../2014/analysis-services/dev-guide/keycolumns-element-assl.md) collection of `DimensionAttribute` contains a single [KeyColumn](../../../2014/analysis-services/dev-guide/keycolumn-element-assl.md) element representing a key column with a string data type, the same `DataItem` values are used as default values for the `ValueColumn` element.  
  
 For more information about the `DataItem` type, including a table of Analysis Services Scripting Language (ASSL) objects and properties of the `DataItem` type, see [DataItem Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md).  
  
 The elements that correspond to the parents of `NameColumn` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.DimensionAttribute> and <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  