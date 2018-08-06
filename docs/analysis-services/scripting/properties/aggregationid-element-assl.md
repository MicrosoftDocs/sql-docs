---
title: "AggregationID Element (ASSL) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: assl
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# AggregationID Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Identifies the aggregation definition from the [AggregationDesign](../../../analysis-services/scripting/objects/aggregationdesign-element-assl.md) element used to create the aggregation instance.  
  
## Syntax  
  
```xml  
  
<AggregationInstance>  
   ...  
   <AggregationID>...</AggregationID>  
   ...  
</AggregationInstance>  
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
|Parent elements|[AggregationInstance](../../../analysis-services/scripting/objects/aggregationinstance-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 If this element is missing or set to a blank string, the **AggregationInstance** represents a user-defined aggregation.  
  
 The element that corresponds to the parent of **AggregationID** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationInstance>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  
