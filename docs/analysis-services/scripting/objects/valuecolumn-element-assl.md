---
title: "ValueColumn Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "ValueColumn Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "ValueColumn element"
ms.assetid: 6c2d6822-8ecc-46df-9fa9-bb92ac716c36
caps.latest.revision: 12
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Data type and length|[DataItem](../../../analysis-services/scripting/data-type/dataitem-data-type-assl.md)|  
|Default value|Varies (see Remarks)|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 If the [NameColumn](../../../analysis-services/scripting/objects/namecolumn-element-assl.md) element of **DimensionAttribute** is specified, the same **DataItem** values are used as default values for the **ValueColumn** element. If the **NameColumn** element of **DimensionAttribute** is not specified and the [KeyColumns](../../../analysis-services/scripting/collections/keycolumns-element-assl.md) collection of **DimensionAttribute** contains a single [KeyColumn](../../../analysis-services/scripting/objects/keycolumn-element-assl.md) element representing a key column with a string data type, the same **DataItem** values are used as default values for the **ValueColumn** element.  
  
 For more information about the **DataItem** type, including a table of Analysis Services Scripting Language (ASSL) objects and properties of the **DataItem** type, see [DataItem Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/dataitem-data-type-assl.md).  
  
 The elements that correspond to the parents of **NameColumn** in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.DimensionAttribute> and <xref:Microsoft.AnalysisServices.ScalarMiningStructureColumn>.  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  