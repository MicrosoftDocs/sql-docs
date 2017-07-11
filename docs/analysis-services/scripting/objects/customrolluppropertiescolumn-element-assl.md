---
title: "CustomRollupPropertiesColumn Element (ASSL) | Microsoft Docs"
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
  - "CustomRollupPropertiesColumn Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "CustomRollupPropertiesColumn"
helpviewer_keywords: 
  - "CustomRollupPropertiesColumn element"
ms.assetid: 7f4a9825-c768-4523-acb3-511a5160fd44
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# CustomRollupPropertiesColumn Element (ASSL)
  Defines the details of a column that provide the properties of a custom rollup formula.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute>  
   ...  
   <CustomRollupPropertiesColumn xsi:type="DataItem">...</CustomRollupPropertiesColumn>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|[DataItem](../../../analysis-services/scripting/data-type/dataitem-data-type-assl.md)|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 For additional information about the **DataItem** type, including a table of Analysis Services Scripting Language (ASSL) objects and properties of the **DataItem** type, see [DataItem Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/dataitem-data-type-assl.md).  
  
 The element that corresponds to the parent of **CustomRollupPropertiesColumn** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DimensionAttribute>.  
  
## See Also  
 [CustomRollupColumn Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/customrollupcolumn-element-assl.md)   
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  