---
title: "OrderBy Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "OrderBy Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "OrderBy"
helpviewer_keywords: 
  - "OrderBy element"
ms.assetid: d7a0564b-430e-44eb-913a-fe1f98917d0f
author: minewiskan
ms.author: owend
manager: craigg
---
# OrderBy Element (ASSL)
  Describes how to order the members contained in the attribute.  
  
## Syntax  
  
```xml  
  
<DimensionAttribute>  
   ...  
      <OrderBy>...</OrderBy>  
   ...  
</DimensionAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Name*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DimensionAttribute](../data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Name*|Order by the member name.|  
|*Key*|Order by the member key.|  
|*AttributeKey*|Order by the member key of the attribute specified in the [OrderByAttributeID](id-element-assl.md) element of `DimensionAttribute`.|  
|*AttributeName*|Order by the member name of the attribute specified in the `OrderByAttributeID` element of `DimensionAttribute`.|  
  
 The enumeration that corresponds to the allowed values for `OrderBy` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.OrderBy>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
