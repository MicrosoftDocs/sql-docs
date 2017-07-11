---
title: "OrderBy Element (ASSL) | Microsoft Docs"
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
  - "OrderBy Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "OrderBy"
helpviewer_keywords: 
  - "OrderBy element"
ms.assetid: d7a0564b-430e-44eb-913a-fe1f98917d0f
caps.latest.revision: 31
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Parent element|[DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Name*|Order by the member name.|  
|*Key*|Order by the member key.|  
|*AttributeKey*|Order by the member key of the attribute specified in the [OrderByAttributeID](../../../analysis-services/scripting/properties/orderbyattributeid-element-assl.md) element of **DimensionAttribute**.|  
|*AttributeName*|Order by the member name of the attribute specified in the **OrderByAttributeID** element of **DimensionAttribute**.|  
  
 The enumeration that corresponds to the allowed values for **OrderBy** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.OrderBy>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  