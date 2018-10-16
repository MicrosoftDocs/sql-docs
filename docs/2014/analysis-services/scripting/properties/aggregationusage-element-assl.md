---
title: "AggregationUsage Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AggregationUsage Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AggregationUsage"
helpviewer_keywords: 
  - "AggregationUsage element"
ms.assetid: af0c2e7f-b659-4fbf-9b1a-66128db669a2
author: minewiskan
ms.author: owend
manager: craigg
---
# AggregationUsage Element (ASSL)
  Controls how the Aggregation Designer in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] designs aggregations.  
  
## Syntax  
  
```xml  
  
<CubeAttribute>  
   ...  
   <AggregationUsage>...</AggregationUsage>  
   ...  
</CubeAttribute>  
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
|Parent element|[CubeAttribute](../data-type/cubeattribute-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Full*|Every aggregation for the cube must include this attribute.|  
|*None*|No aggregation for the cube should include this attribute.|  
|*Unrestricted*|No restrictions are placed on the Aggregation Designer.|  
|*Default*|The Aggregation Designer applies a default rule based on the type of attribute (*Full* for keys, *Unrestricted* for others).|  
  
 The enumeration that corresponds to the allowed values for `AggregationUsage` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationUsage>.  
  
## See Also  
 [Cube Element &#40;ASSL&#41;](../objects/cube-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
