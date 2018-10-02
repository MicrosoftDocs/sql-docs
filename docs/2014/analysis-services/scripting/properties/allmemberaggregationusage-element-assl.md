---
title: "AllMemberAggregationUsage Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AllMemberAggregationUsage Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AllMemberAggregationUsage"
helpviewer_keywords: 
  - "AllMemberAggregationUsage element"
ms.assetid: 264fe9d8-8e9a-4642-8cee-7c2804126926
author: minewiskan
ms.author: owend
manager: craigg
---
# AllMemberAggregationUsage Element (ASSL)
  Controls how the Aggregation Designer in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] designs aggregations.  
  
## Syntax  
  
```xml  
  
<CubeDimension>  
   ...  
   <AllMemberAggregationUsage>...</AllMemberAggregationUsage>  
   ...  
</CubeDimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Default*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[CubeDimension](../data-type/dimension-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Full*|Every aggregation for the cube must include the All member.|  
|*None*|No aggregation for the cube should include the All member.|  
|*Unrestricted*|No restrictions are placed on the Aggregation Designer.|  
|*Default*|Same as *Unrestricted*.|  
  
## Remarks  
 The element that corresponds to the parent of `AllMemberAggregationUsage` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CubeDimension>.  
  
## See Also  
 [Cube Element &#40;ASSL&#41;](../objects/cube-element-assl.md)   
 [Dimension Element &#40;ASSL&#41;](../objects/dimension-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
