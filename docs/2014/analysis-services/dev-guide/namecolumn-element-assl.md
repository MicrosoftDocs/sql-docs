---
title: "NameColumn Element (ASSL) | Microsoft Docs"
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
  - "NameColumn Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "NameColumn"
helpviewer_keywords: 
  - "NameColumn element"
ms.assetid: 9ff79f2e-26d7-4ab9-a166-14c2c2d1fc07
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# NameColumn Element (ASSL)
  Identifies the column that provides the name of the parent element.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute> <!-- or ScalarMiningStructureColumn -->  
   ...  
   <NameColumn xsi:type="DataItem">...</NameColumn>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[DataItem](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md)|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
|Ancestor or Parent|Default Value|  
|------------------------|-------------------|  
|[DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md)|Varies (see Remarks)|  
|[ScalarMiningStructureColumn](../../../2014/analysis-services/dev-guide/scalarminingstructurecolumn-data-type-assl.md)|None|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md), [ScalarMiningStructureColumn](../../../2014/analysis-services/dev-guide/scalarminingstructurecolumn-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 If the [KeyColumns](../../../2014/analysis-services/dev-guide/keycolumns-element-assl.md) collection of `DimensionAttribute` contains a single [KeyColumn](../../../2014/analysis-services/dev-guide/keycolumn-element-assl.md) element representing a key column with a string data type, the same `DataItem` values are used as default values for the `NameColumn` element.  
  
 For more information about the `DataItem` type, including a table of Analysis Services Scripting Language (ASSL) objects and properties of the `DataItem` type, see [DataItem Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/dataitem-data-type-assl.md).  
  
 The elements that correspond to the parents of `NameColumn` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.DimensionAttribute> and <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  