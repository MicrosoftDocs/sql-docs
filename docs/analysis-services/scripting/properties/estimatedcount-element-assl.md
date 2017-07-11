---
title: "EstimatedCount Element (ASSL) | Microsoft Docs"
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
  - "EstimatedCount Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "EstimatedCount"
helpviewer_keywords: 
  - "EstimatedCount element"
ms.assetid: ce84b54a-8ab2-42f4-a7dd-e10a3d41cb4d
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# EstimatedCount Element (ASSL)
  Contains the estimated number of members for an attribute, as defined by the user.  
  
## Syntax  
  
```xml  
  
<AggregationDesignAttribute> <!-- or DimensionAttribute -->  
      ...  
   <EstimatedCount>...</EstimatedCount>  
   ...  
</AggregationDesignAttribute>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Long|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AggregationDesignAttribute](../../../analysis-services/scripting/data-type/aggregationdesignattribute-data-type-assl.md), [DimensionAttribute](../../../analysis-services/scripting/data-type/dimensionattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 This value is assigned by the user and is used by the [AggregationDesign Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/aggregationdesign-element-assl.md).  
  
 The elements that correspond to the parents of **EstimatedCount** in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.AggregationDesignAttribute> and <xref:Microsoft.AnalysisServices.DimensionAttribute>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  