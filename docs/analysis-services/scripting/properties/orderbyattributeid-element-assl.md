---
title: "OrderByAttributeID Element (ASSL) | Microsoft Docs"
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
  - "OrderByAttributeID Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "OrderByAttributeID"
helpviewer_keywords: 
  - "OrderByAttributeID element"
ms.assetid: 41d7b650-ac40-4f1a-850d-2f81a19b28cb
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# OrderByAttributeID Element (ASSL)
  Identifies another attribute by which to order the members of the [Dimension](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md) attribute.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute>  
   ...  
      <OrderByAttributeID>...</OrderByAttributeID>  
   ...  
</DimensionAttribute>  
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
|Parent element|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The **OrderByAttributeID** element is only used when the value of the [OrderBy](../../../analysis-services/scripting/properties/orderby-element-assl.md) element for the **DimensionAttribute** is set to *AttributeKey* or *AttributeName*.  
  
 The element that corresponds to the parent of **OrderByAttributeID** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DimensionAttribute>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  